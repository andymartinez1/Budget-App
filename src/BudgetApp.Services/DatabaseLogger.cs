using BudgetApp.Data;
using BudgetApp.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BudgetApp.Services;

public class DatabaseLogger : ILogger
{
    private readonly string _categoryName;
    private readonly IServiceProvider _serviceProvider;

    public DatabaseLogger(IServiceProvider serviceProvider, string categoryName)
    {
        _serviceProvider = serviceProvider;
        _categoryName = categoryName;
    }

    public IDisposable? BeginScope<TState>(TState state)
        where TState : notnull
    {
        return null;
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return logLevel != LogLevel.None;
    }

    public void Log<TState>(
        LogLevel logLevel,
        EventId eventId,
        TState state,
        Exception? exception,
        Func<TState, Exception?, string> formatter
    )
    {
        if (!IsEnabled(logLevel))
            return;

        // Prevent circular logging by ignoring EF Core logs
        if (_categoryName.StartsWith("Microsoft.EntityFrameworkCore"))
            return;

        var message = formatter(state, exception);
        if (string.IsNullOrEmpty(message) && exception == null)
            return;

        using var scope = _serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<BudgetDbContext>();

        var log = new Log
        {
            Level = logLevel.ToString(),
            Category = _categoryName,
            Message = message,
            Exception = exception?.ToString() ?? string.Empty,
            CreatedAt = DateTime.UtcNow,
        };

        dbContext.Logs.Add(log);
        dbContext.SaveChanges();
    }
}
