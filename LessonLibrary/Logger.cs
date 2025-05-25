using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LessonLibrary
{
    public static class Logger
    {
        private static readonly object _lock = new object();
        private static readonly string logFilePath = "log.txt";

        public static void Log(string message)
        {
            lock (_lock)
            {
                try
                {
                    File.AppendAllText(logFilePath, $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}{Environment.NewLine}");
                }
                catch
                {
                    // Если логгирование упало, можно либо молча игнорировать,
                    // либо, например, вывести в Debug консоль
                }
            }
        }

        public static void LogError(string message) => Log("ERROR: " + message);
        public static void LogWarning(string message) => Log("WARNING: " + message);
        public static void LogInfo(string message) => Log("INFO: " + message);
    }
}
