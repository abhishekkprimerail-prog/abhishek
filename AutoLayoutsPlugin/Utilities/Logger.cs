using System;
using System.IO;

namespace AutoLayoutsPlugin
{
    /// <summary>
    /// Logging utility for the plugin
    /// </summary>
    public class Logger
    {
        private string logFilePath;

        public Logger()
        {
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string pluginDir = Path.Combine(appDataPath, "AutoLayouts");
            
            if (!Directory.Exists(pluginDir))
                Directory.CreateDirectory(pluginDir);

            logFilePath = Path.Combine(pluginDir, $"AutoLayouts_{DateTime.Now:yyyyMMdd}.log");
        }

        public void Log(string message)
        {
            WriteToLog($"[INFO] {DateTime.Now:HH:mm:ss} - {message}");
        }

        public void Error(string message)
        {
            WriteToLog($"[ERROR] {DateTime.Now:HH:mm:ss} - {message}");
        }

        public void Warning(string message)
        {
            WriteToLog($"[WARNING] {DateTime.Now:HH:mm:ss} - {message}");
        }

        private void WriteToLog(string message)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(logFilePath, true))
                {
                    writer.WriteLine(message);
                }
            }
            catch { }
        }
    }
}
