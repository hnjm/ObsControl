using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;

namespace ObservatoryCenter
{
    public class LogRecord
    {
        public DateTime Time;
        public LogLevel LogLevel = LogLevel.Activity;
        public string Procedure = "";
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
        Emphasize=2,
        Debug=3
    }

    public enum LogLevel
    {
        Important = 0,
        Activity = 1,
        Debug = 2,
        Chat = 3,
        Trace = 4,
        All=999
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
        public static LogLevel DEBUG_LEVEL = LogLevel.All;

        public static Int32 _MAX_DIPSLAYED_PROG_LOG_LINES = 100;

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
        /// Add log record to DataBase (LogList LIST)
        /// </summary>
        /// <param name="logMessage"></param>
        /// <param name="LogLevel"></param>
        /// <param name="ColorHoghlight"></param>
        public static void AddLog(string logMessage, LogLevel LogLevel = LogLevel.Important, Highlight ColorHighlight = Highlight.Normal,string logProcedure="")
        {
            //Add to list
            LogRecord LogRec = new LogRecord();
            LogRec.Time = DateTime.Now;
            LogRec.Procedure = logProcedure;
            LogRec.Message = logMessage;
            LogRec.LogLevel = LogLevel;
            LogRec.Highlight = ColorHighlight;
            LogList.Add(LogRec);
        }

        /// <summary>
        /// Dump to file Log Contents (LogList LIST)
        /// </summary>
        public static void DumpToFile(LogLevel LogLevel=LogLevel.All)
        {
            List<LogRecord> LogListNew = new List<LogRecord>();

            //sort new (not saved) records
            for(var i=0; i < LogList.Count; i++)
            {
                // if current line wasn't written to file
                if (!LogList[i].dumpedToFile)
                {
                    LogListNew.Add(LogList[i]); //add to newrecords array
                    LogList[i].dumpedToFile = true; //mark as written
                }
            }

            //Save new (not saved) records
            if (LogListNew.Count > 0)
            {
                try
                {
                    using (StreamWriter LogFile = new StreamWriter(LogFileFullName, true))
                    {
                        for (var i = 0; i < LogListNew.Count; i++)
                        {
                            // if current log level is less then DebugLevel
                            if (LogListNew[i].LogLevel <= LogLevel)
                            {
                                //time
                                LogFile.Write("{0,-12}{1,-14}", LogListNew[i].Time.ToString("yyyy-MM-dd"), LogListNew[i].Time.ToString("HH:mm:ss.fff"));
                                //LogLevel
                                LogFile.Write("{0,-10}", LogListNew[i].LogLevel.ToString());
                                //message
                                LogFile.Write("{0}\t", LogListNew[i].Message);
                                LogFile.WriteLine();
                            }
                        }
                    }
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("Log write error [" + Ex.Message + "]");
                }
            }

        }

        /// <summary>
        /// Dump to screen Log Contents
        /// </summary>
        public static string DumpToString(LogLevel LogLevel=LogLevel.Activity)
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
                        RetStr += String.Format("{0} {1}", LogList[i].Time.ToString("yyyy-MM-dd"), LogList[i].Time.ToString("HH:mm:ss"));
                        RetStr += String.Format(": {0}", LogList[i].Message) + Environment.NewLine;
                    }
                    LogList[i].displayed = true;
                }
            }
            return RetStr;
        }

        /// <summary>
        /// Dump to screen Log Contents
        /// </summary>
        public static void DisplayLogInTextBox(RichTextBox LogTextBox, LogLevel LogLevel = LogLevel.Activity)
        {
            List<LogRecord> LogListNew = new List<LogRecord>();

            //sort new (not saved) records
            for(var i=0; i < LogList.Count; i++)
            {
                // if current line wasn't displayed
                if (!LogList[i].displayed)
                {
                    LogListNew.Add(LogList[i]); //add to newrecords array
                    LogList[i].displayed = true; //mark as written
                }
            }

            //check - if logtextbox is too large
            if (LogTextBox.Lines.Length > _MAX_DIPSLAYED_PROG_LOG_LINES)
            {
                string[] lines = LogTextBox.Lines;
                var newLines = lines.Skip(LogTextBox.Lines.Length - _MAX_DIPSLAYED_PROG_LOG_LINES);
                LogTextBox.Lines = newLines.ToArray();
            }

            string RetStr = "";
            //Save new (not saved) records
            if (LogListNew.Count > 0)
            {
                for (var i = 0; i < LogListNew.Count; i++)
                {
                    // if current log level is less then DebugLevel
                    if (LogListNew[i].LogLevel <= LogLevel)
                    {
                        LogTextBox.SelectionStart = LogTextBox.TextLength;
                        LogTextBox.SelectionLength = 0;

                        if (LogListNew[i].Highlight == Highlight.Error)
                        {
                            LogTextBox.SelectionColor = Color.Red;
                        }

                        RetStr = String.Format("{0} {1}", LogListNew[i].Time.ToString("yyyy-MM-dd"), LogListNew[i].Time.ToString("HH:mm:ss"));
                        RetStr += String.Format(": {0}", LogListNew[i].Message) + Environment.NewLine;

                        LogTextBox.AppendText(RetStr);

                        LogTextBox.SelectionColor = LogTextBox.ForeColor;

                        //set cursor to the end
                        LogTextBox.SelectionStart = LogTextBox.TextLength;
                        LogTextBox.SelectionLength = 0;
                        LogTextBox.ScrollToCaret();
                    }
                }
            }
        }

        public static string LogExceptionMessage(Exception Ex, string MESSAGEST, bool ImportantLogFlag = true)
        {
            StackTrace st = new StackTrace(Ex, true);
            StackFrame[] frames = st.GetFrames();
            string messstr = "";

            // Iterate over the frames extracting the information you need
            foreach (StackFrame frame in frames)
            {
                messstr += String.Format("{0}:{1}({2},{3})", frame.GetFileName(), frame.GetMethod().Name, frame.GetFileLineNumber(), frame.GetFileColumnNumber());
            }

            string FullMessage = MESSAGEST + Environment.NewLine;
            FullMessage += Environment.NewLine + Environment.NewLine + "Debug information:" + Environment.NewLine + "Exception source: " + Ex.Data + " " + Ex.Message
                    + Environment.NewLine + Environment.NewLine + messstr;
            //MessageBox.Show(this, FullMessage, "Invalid value", MessageBoxButtons.OK);

            Logging.AddLog(MESSAGEST+", exception: " + Ex.Message, (ImportantLogFlag ? LogLevel.Important : LogLevel.Debug), Highlight.Error);
            Logging.AddLog(FullMessage, LogLevel.Debug, Highlight.Error);

            return FullMessage;
        }

    }
}
