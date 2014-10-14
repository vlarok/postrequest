using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CallHandler
{
    public static class Config
    {
        private static string _sleeptime;
        private static string _loggingDir;
        private static string _loggFileName;
        private static string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
        private static string _cfg = File.ReadAllText(currentDirectory + "\\config.cfg");
        private static string _incoming;
        static Config()
        {
            _sleeptime = getValue(@"SleepTime (.*)\r\n", 1);
            _loggingDir = getValue(@"LoggingDir (.*)\r\n", 1);
            _loggFileName = getValue(@"LoggFileName (.*)\r\n", 1);
            _incoming = getValue(@"Incoming (.*)\r\n", 1);
        }
        private static string getValue(string pattern, int i)
        {
            Match callIdFind = Regex.Match(_cfg, pattern, RegexOptions.Multiline);
            return callIdFind.Groups[i].Value;
        }
        public static string LoggingDir
        {
            get
            {
                return _loggingDir;
            }

        }
        public static string SleepTime
        {
            get
            {
                return _sleeptime;
            }

        }
        public static string Incoming
        {
            get
            {
                return _incoming;
            }

        }
        public static string LoggFileName
        {
            get
            {
                return _loggFileName;
            }

        }
    }

}
