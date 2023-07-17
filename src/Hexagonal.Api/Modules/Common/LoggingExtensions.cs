using Serilog.Extensions.Logging;

namespace hexagonal.API.Modules.Common;

/// <summary>
///     LoggingExtensions
/// </summary>
public static class LoggingExtensions
{
    /// <summary>
    ///     CreateLog
    /// </summary>
    /// <param name="providers"></param>
    public static void CreateLog(LoggerProviderCollection providers)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .Enrich.FromLogContext()
            .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
            .WriteTo.Providers(providers)
            .CreateLogger();
    }
}