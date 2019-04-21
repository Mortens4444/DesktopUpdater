#define USE_LOGGER

using DesktopUpdater.Interfaces;
using System;
using System.IO;
using System.Windows.Forms;

namespace DesktopUpdater
{
    public class Logger: ILogger
    {
        private const string LAST_RUN_RESULT_LOG = "last_run_result.log";
        private static readonly string LOG_FILE_PATH = Path.Combine(Application.StartupPath, "last_run_result.log");

        public void Create(string logData)
        {
            #if USE_LOGGER
                FileUtils.WriteToTextFile(LOG_FILE_PATH, GetLogMessage(logData), true, true);
            #endif
        }

        public void Append(string logData)
        {
            #if USE_LOGGER
                FileUtils.WriteToTextFile(LOG_FILE_PATH, GetLogMessage(logData), false, true);
            #endif
        }

        private static string GetLogMessage(string logData)
        {
            var utcNow = DateTime.UtcNow;
            return $"{utcNow.ToShortDateString()} {utcNow.ToLongTimeString()}: {logData}";
        }
    }
}
