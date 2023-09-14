using Serilog;

namespace GraphFlix.Helper;

/// <summary>
/// Class used for logging
/// TODO: Add Audit log and exception log
/// </summary>
public class Log
{
    public static Serilog.ILogger Logger = new LoggerConfiguration()
    .WriteTo.File("log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();
}
