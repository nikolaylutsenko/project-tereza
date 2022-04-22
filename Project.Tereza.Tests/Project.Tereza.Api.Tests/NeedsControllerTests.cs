using System.Linq;
using NUnit.Framework;
using AutoMapper;
using Project.Tereza.Api.Controllers;
using Project.Tereza.Core.Interfaces;
using Project.Tereza.Core;
using Serilog;
using Project.Tereza.Requests.Validators;
using Project.Tereza.Requests.Validators.Specifications;
using Project.Tereza.Requests.Requests.Specifications;
using Moq;
using Project.Tereza.Tests.Common;
using Autofac;
using Microsoft.AspNetCore.Mvc;
using Project.Tereza.Responses.Responses.Paged;
using System.Threading.Tasks;
using Project.Tereza.Tests.Project.Tereza.Tests.Common;
using Project.Tereza.Responses.Responses;

namespace Project.Tereza.Api.Tests;

public class NeedsControllerTests
{
    // here will bi mocks
    private Mock<IMapper> _mapperMock = new Mock<IMapper>();
    private Mock<ILogger> _serilogMock = new Mock<ILogger>();
    private Mock<IService<Need>> _needServiceMock = new Mock<IService<Need>>();
    private AddNeedRequestValidator _addNeedRequestValidator;
    private UpdateNeedRequestValidator _updateNeedRequestValidator;
    private NeedSpecificationRequestValidator _needSpecificationRequestValidator;

    public NeedsControllerTests()
    {
        _addNeedRequestValidator = new AddNeedRequestValidator();
        _updateNeedRequestValidator = new UpdateNeedRequestValidator();
        _needSpecificationRequestValidator = new NeedSpecificationRequestValidator();
    }

    [SetUp]
    public void Setup()
    {
        // here set up mocks
    }

    [Test]
    public async Task Test1()
    {
        // Arrange
        var needSpecificationRequest = TestingEntities.GetNeedSpecificationRequest;
        var containerBuilderFabric = new ContainerBuilderFabric();
        var container = containerBuilderFabric.GetContainerBuilder();

        // Act
        ActionResult<PagedResponse<Need>> actionResult;

        using (var scope = container.BeginLifetimeScope())
        {
            var controller = scope.Resolve<NeedsController>();

            actionResult = await controller.GetAllNeedsAsync(needSpecificationRequest);

        }

        var result = actionResult.Result as OkObjectResult;
        var pagedResult = result?.Value as PagedResponse<NeedResponse>;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.AreEqual(result?.StatusCode, 200);
            Assert.AreEqual(pagedResult?.Data?.Count(), TestingEntities.GetListOfNeedResponses.Count());
        });
    }
}