using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Windows.Forms;

namespace ObservatoryCenter
{
    public class LogRecord
    {
        public DateTime Time;
        public byte LogLevel = 1;
        public string Message = "";
        public Highlight Highlight = 0;
        public string Caller = "";
        public bool dumpedToFile = false;
        public bool displayed = false;
    }

    public enum Highlight
    {
        Normal=0,
        Error=1,
        Hoghlight=2
    }
    
    public class Logging
    {
        /// <summary>
        /// Log text
        /// </summary>
        private static List<LogRecord> LogList;


        public static bool LogFileFlag = true;
        public static string LogFilePath = "";
        public static string LogFileName = "observatory_"; //Text log
        public static string LogFileExt = "log"; //Text log

        //DEBUG LEVEL
        public static byte DEBUG_LEVEL = 1;

        static Logging()
        {
            LogList = new List<LogRecord>();
        }

        /// <summary>
        /// Error log procedures
        /// </summary>
        private static string LogFileFullName
        {
            get{
                if (LogFilePath == "") LogFilePath = Application.StartupPath;
                return Path.Combine(LogFilePath, LogFileName + DateTime.Now.ToString("yyyy-MM-dd") + "." + LogFileExt);
            }
        }

        /// <summary>
        /// Add log recoed
        /// </summary>
        /// <param name="logMessage"></param>
        /// <param name="LogLevel"></param>
        /// <param name="ColorHoghlight"></param>
        public static void AddLog(string logMessage, byte LogLevel = 1, Highlight ColorHoghlight = Highlight.Normal)
        {
            //Add to list
            LogRecord LogRec = new LogRecord();
            LogRec.Time = DateTime.Now;
            LogRec.Message = logMessage;
            LogRec.LogLevel = LogLevel;
            LogRec.Highlight = ColorHoghlight;
            LogList.Add(LogRec);
        }

        /// <summary>
        /// Dump to file Log Contents
        /// </summary>
        public static void DumpToFile(byte LogLevel=1)
        {
            List<LogRecord> LogListNew = new List<LogRecord>();

            for(var i=0; i < LogList.Count; i++)
            {
                // if current line wasn't written to file
                if (!LogList[i].dumpedToFile)
                {
                    LogListNew.Add(LogList[i]); //add to newrecords array
                    LogList[i].dumpedToFile = true; //mark as written
                }
            }

            if (LogListNew.Count > 0)
            {
                using (StreamWriter LogFile = new StreamWriter(LogFileFullName, true))
                {
                    for (var i = 0; i < LogListNew.Count; i++)
                    {
                        // if current log level is less then DebugLevel
                        if (LogListNew[i].LogLevel <= LogLevel)
                        {
                            LogFile.Write("{0} {1}", LogListNew[i].Time.ToShortDateString(), LogListNew[i].Time.ToLongTimeString());
                            LogFile.WriteLine(": {0}", LogListNew[i].Message);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Dump to screen Log Contents
        /// </summary>
        public static string DumpToString(byte LogLevel=1)
        {
            string RetStr = "";
            for (var i = 0; i < LogList.Count; i++)
            {
                // if current line wasn't written to file
                if (!LogList[i].displayed)
                {
                    // if current log level is less then DebugLevel
                    if (LogList[i].LogLevel <= LogLevel)
                    {
                        RetStr+=String.Format("{0} {1}", LogList[i].Time.ToShortDateString(), LogList[i].Time.ToLongTimeString());
                        RetStr += String.Format(": {0}", LogList[i].Message) + Environment.NewLine;
                    }
                    LogList[i].displayed = true;
                }
            }
            return RetStr;
        }
    
    }
}
