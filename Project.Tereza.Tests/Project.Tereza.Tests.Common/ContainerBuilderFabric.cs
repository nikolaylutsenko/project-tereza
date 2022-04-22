using Autofac;
using AutoMapper;
using FluentResults;
using MDEvents.Services.Specification;
using MDEvents.Services.Specification.SpecificationParameters;
using Moq;
using Project.Tereza.Api.Controllers;
using Project.Tereza.Core;
using Project.Tereza.Core.Interfaces;
using Project.Tereza.Requests.Requests.Specifications;
using Project.Tereza.Requests.Validators;
using Project.Tereza.Requests.Validators.Specifications;
using Project.Tereza.Responses.Responses;
using Project.Tereza.Tests.Project.Tereza.Tests.Common;
using Serilog;

namespace Project.Tereza.Tests.Common;
public class ContainerBuilderFabric
{
    private Mock<IMapper> _mapperMock = new Mock<IMapper>();
    private Mock<ILogger> _serilogMock = new Mock<ILogger>();
    private Mock<IService<Need>> _needServiceMock = new Mock<IService<Need>>();
    private AddNeedRequestValidator _addNeedRequestValidator;
    private UpdateNeedRequestValidator _updateNeedRequestValidator;
    private NeedSpecificationRequestValidator _needSpecificationRequestValidator;

    public ContainerBuilderFabric()
    {
        _addNeedRequestValidator = new AddNeedRequestValidator();
        _updateNeedRequestValidator = new UpdateNeedRequestValidator();
        _needSpecificationRequestValidator = new NeedSpecificationRequestValidator();

        // set up mocks
        _mapperMock.Setup(m => m.Map<SpecificationParameters<Need>>(It.IsAny<NeedSpecificationRequest>()))
            .Returns(TestingEntities.GetSpecificationParameterOfNeed);
        _mapperMock.Setup(m => m.Map<IEnumerable<NeedResponse>>(It.IsAny<IEnumerable<Need>?>()))
            .Returns(TestingEntities.GetListOfNeedResponses);

        // set up Service mocks
        _needServiceMock.Setup(m => m.FindAsync(It.IsAny<NeedSpecification>(), null))
            .Returns(Task.Factory.StartNew(() => Result.Ok<(IEnumerable<Need>, int)>((TestingEntities.GetListOfNeeds, TestingEntities.GetListOfNeeds.Count()))));
    }

    public IContainer GetContainerBuilder()
    {
        var containerBuilder = new ContainerBuilder();

        // setup container
        containerBuilder.RegisterInstance(_mapperMock.Object).As<IMapper>();
        containerBuilder.RegisterInstance(_serilogMock.Object).As<ILogger>();
        containerBuilder.RegisterInstance(_needServiceMock.Object).As<IService<Need>>();
        containerBuilder.RegisterInstance(_addNeedRequestValidator).As<AddNeedRequestValidator>();
        containerBuilder.RegisterInstance(_updateNeedRequestValidator).As<UpdateNeedRequestValidator>();
        containerBuilder.RegisterInstance(_needSpecificationRequestValidator).As<NeedSpecificationRequestValidator>();

        // register services
        containerBuilder.RegisterInstance(new NeedsController(_mapperMock.Object,
            _serilogMock.Object, _needServiceMock.Object, _addNeedRequestValidator, _updateNeedRequestValidator, _needSpecificationRequestValidator)).As<NeedsController>();

        var container = containerBuilder.Build();

        return container;
    }
}
