using Microsoft.Extensions.Logging;

namespace BudgetApp.Services;

public class DatabaseLoggerProvider : ILoggerProvider
{
    private readonly IServiceProvider _serviceProvider;

    public DatabaseLoggerProvider(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public void Dispose() { }

    public ILogger CreateLogger(string categoryName)
    {
        return new DatabaseLogger(_serviceProvider, categoryName);
    }
}
