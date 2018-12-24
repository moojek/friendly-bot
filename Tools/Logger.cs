using System;
using System.IO;
using System.Threading.Tasks;
using Discord;

namespace FriendlyBot.Tools
{
    public static class Logger
    {
        private static string currentLog;
        private static DateTimeOffset currentLogDate;
        private static string logFile;

        static Logger()
        {
            currentLogDate = DateTimeOffset.Now;
            logFile = "Logs/" + currentLogDate.ToString("dMMMyyyy");
            if (!File.Exists(logFile))
            {
                currentLog = "";
                File.WriteAllText(logFile, currentLog);
            }
            else
            {
                currentLog = File.ReadAllText(logFile);
            }
        }

        public static void ValidateLog()
        {
            if (currentLogDate == DateTimeOffset.Now)
                return;
            UpdateLog();
        }
        public static void UpdateLog()
        {
            File.WriteAllText(logFile, currentLog);
            currentLogDate = DateTimeOffset.Now;
            logFile = "Logs/" + currentLogDate.ToString("dMMMyyyy");
            currentLog = "";
            File.WriteAllText(logFile, currentLog);
        }
        public static void SaveLog()
        {
            File.WriteAllText(logFile, currentLog);
        }
        public static Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.Message);

            ValidateLog();
            currentLog += (DateTime.Now.ToString("T") + ": " + msg.Message + "\n");
            SaveLog();
            return Task.CompletedTask;
        }
    }
}