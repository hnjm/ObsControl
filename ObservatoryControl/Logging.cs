using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Windows.Forms;

namespace ObservatoryCenter
{
    public static class Logging
    {
        private static TextWriter LogFile = null;
        public static string LogFileName = "weather_station.log"; //Text log
        public static string LogFilePath = "";
        public static bool LogFileFlag = true;


        //DEBUG LEVEL
        public static byte DEBUG_LEVEL = 1;

        private static string ApplicationFilePath
        {
            get { return Application.StartupPath + "\\"; }
        }

        /// <summary>
        /// Error log procedures
        /// </summary>
        #region Error log section
        public static void OpenLogFile()
        {
            if (LogFilePath == "") LogFilePath = ApplicationFilePath;
            LogFile = File.AppendText(LogFilePath + LogFileName);
        }

        public static void CloseLogFile()
        {
            LogFile.Close();
            LogFile = null;
        }

        public static void Log(string logMessage, byte LogLevel=1)
        {
            // if current log level is less then DebugLevel
            if (LogLevel <= DEBUG_LEVEL)
            {

                if (LogFile == null)
                {
                    OpenLogFile();
                }

                LogFile.Write("{0} {1}", DateTime.Now.ToShortDateString(), DateTime.Now.ToLongTimeString());
                LogFile.WriteLine(": {0}", logMessage);
            }
        }
        #endregion
    
    }
}
