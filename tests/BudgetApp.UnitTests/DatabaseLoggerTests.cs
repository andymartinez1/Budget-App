using BudgetApp.Data;
using BudgetApp.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit;

namespace BudgetApp.UnitTests;

public class DatabaseLoggerTests
{
    [Fact]
    public void Log_WritesToDatabase()
    {
        // Arrange
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddDbContext<BudgetDbContext>(options =>
            options.UseInMemoryDatabase("TestLogDb")
        );
        var serviceProvider = serviceCollection.BuildServiceProvider();
        var logger = new DatabaseLogger(serviceProvider, "TestCategory");
        var message = "Test log message";

        // Act
        logger.Log(LogLevel.Information, 0, message, null, (s, e) => s.ToString()!);

        // Assert
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<BudgetDbContext>();
        var log = dbContext.Logs.FirstOrDefault();

        Assert.NotNull(log);
        Assert.Equal("Information", log.Level);
        Assert.Equal("TestCategory", log.Category);
        Assert.Equal(message, log.Message);
    }

    [Fact]
    public void IsEnabled_ReturnsTrueForValidLogLevels()
    {
        // Arrange
        var serviceCollection = new ServiceCollection();
        var serviceProvider = serviceCollection.BuildServiceProvider();
        var logger = new DatabaseLogger(serviceProvider, "TestCategory");

        // Assert
        Assert.True(logger.IsEnabled(LogLevel.Information));
        Assert.True(logger.IsEnabled(LogLevel.Error));
        Assert.False(logger.IsEnabled(LogLevel.None));
    }

    [Fact]
    public void Log_IgnoresEFCoreLogs()
    {
        // Arrange
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddDbContext<BudgetDbContext>(options =>
            options.UseInMemoryDatabase("TestLogDbIgnore")
        );
        var serviceProvider = serviceCollection.BuildServiceProvider();
        var logger = new DatabaseLogger(
            serviceProvider,
            "Microsoft.EntityFrameworkCore.Database.Command"
        );
        var message = "EF Core log message";

        // Act
        logger.Log(LogLevel.Information, 0, message, null, (s, e) => s.ToString()!);

        // Assert
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<BudgetDbContext>();
        var log = dbContext.Logs.FirstOrDefault();

        Assert.Null(log);
    }
}
