#define USE_LOGGER

using DesktopUpdater.Interfaces;

namespace DesktopUpdater;

public class Logger: ILogger
{
    private const string LastRunResultLog = "last_run_result.log";
    private static readonly string LogFilePath = Path.Combine(AppContext.BaseDirectory, LastRunResultLog);

    public void Create(string logData)
    {
        #if USE_LOGGER
            FileUtils.WriteToTextFile(LogFilePath, GetLogMessage(logData), true, true);
        #endif
    }

    public void Append(string logData)
    {
        #if USE_LOGGER
            FileUtils.WriteToTextFile(LogFilePath, GetLogMessage(logData), false, true);
        #endif
    }

    private static string GetLogMessage(string logData)
    {
        var utcNow = DateTime.UtcNow;
        return $"{utcNow.ToShortDateString()} {utcNow.ToLongTimeString()}: {logData}";
    }
}
