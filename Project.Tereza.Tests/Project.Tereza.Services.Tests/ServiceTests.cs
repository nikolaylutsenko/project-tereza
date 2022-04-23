using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentResults;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using Project.Tereza.Core;
using Project.Tereza.Infrastructure.DBContext;
using Serilog;

namespace Project.Tereza.Services.Tests;

public class ServiceTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task GetAsync_PassValid_ReturnValid()
    {
        // Arrange
        var connection = new SqliteConnection("DataSource=:memory:");
        connection.Open();

        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite(connection)
            .Options;

        using (var context = new AppDbContext(options))
        {
            context.Database.EnsureCreated();

            // no need to seed this in memory DB because it seeds in AppDbContext
            // var needs = new List<Need>
            //     {
            //         new("82d257a5-d72b-4f08-bcf2-76ebdc958b5f", "Laptop", "Need laptop for working needs.", 1, false),
            //         new("b47fdb0a-76d4-4b89-bf20-9cecfa4f4f82", "Royal Canin Sphyncx 2 kg", "I need food for my cat, please help!", 1, false),
            //         new("a961067e-c777-42c2-8fee-71180d750bd7", "Aspirin", "Please, I can't find this drug in retail", 3, false)
            //     };
            //await context.Needs.AddRangeAsync(needs);
            //await context.SaveChangesAsync();
        }

        Mock<ILogger> loggerMock = new Mock<ILogger>();

        // Act
        Result<Need> needResult = null;

        using (var context = new AppDbContext(options))
        {
            var service = new Service<Need>(context, loggerMock.Object);
            needResult = await service.GetAsync(Guid.Parse("82d257a5-d72b-4f08-bcf2-76ebdc958b5f"));
        }

        // Assert
        Assert.That(needResult.Value.Name, Is.EqualTo("Laptop"));
    }
}