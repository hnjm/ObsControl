using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;

namespace WeatherControl
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
        public static List<LogRecord> LOGLIST;

        public static string LOG_FOLDER_NAME = "Logs";
        public static string LOG_FILE_NAME_ALL = "observatory_trace_"; //Text log
        public static string LOG_FILE_NAME_MAIN = "observatory_"; //Text log
        public static string LOG_FILE_EXT = "log"; //Text log

        public static string LogFilePath = Path.Combine(ConfigManagement.ProgDocumentsPath, LOG_FOLDER_NAME) + "\\";

        public static string currentLogAllFileFullName=""; //путь и имя файла лога текущей сессии
        public static string currentLogMainFileFullName=""; //путь и имя файла лога текущей сессии

        //DEBUG LEVEL
        public static LogLevel DEBUG_LEVEL = LogLevel.All;

        public static Int32 _MAX_DIPSLAYED_PROG_LOG_LINES = 100;
        public static Int32 _MAX_LOGLIST_SIZE = 65000;

        static Logging()
        {
            LOGLIST = new List<LogRecord>();
        }

        /// <summary>
        /// Get current log files names with path (full log file)
        /// </summary>
        private static string LogAllFileFullName
        {
            get
            {
                if (currentLogAllFileFullName == "")
                {
                    LogFilePath = GetLogDirectory();
                    currentLogAllFileFullName = Path.Combine(LogFilePath, LOG_FILE_NAME_ALL + DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss") + "." + LOG_FILE_EXT);
                }
                return currentLogAllFileFullName;
            }
            set
            {
                LogFilePath = GetLogDirectory();
                currentLogAllFileFullName = Path.Combine(LogFilePath, LOG_FILE_NAME_ALL + DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss") + "." + LOG_FILE_EXT);
            }
        }
        /// <summary>
        /// Get current log files names with path (main log file)
        /// </summary>
        private static string LogMainFileFullName
        {
            get
            {
                if (currentLogMainFileFullName == "")
                {
                    LogFilePath = GetLogDirectory();
                    currentLogMainFileFullName = Path.Combine(LogFilePath, LOG_FILE_NAME_MAIN + DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss") + "." + LOG_FILE_EXT);
                }
                return currentLogMainFileFullName;
            }
            set
            {
                LogFilePath = GetLogDirectory();
                currentLogMainFileFullName = Path.Combine(LogFilePath, LOG_FILE_NAME_MAIN + DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss") + "." + LOG_FILE_EXT);
            }
        }

        /// <summary>
        /// Check if log folder exits, and if not - create it
        /// </summary>
        /// <returns>Current log file path</returns>
        private static string GetLogDirectory()
        {
            if (LogFilePath == "")
            {
                string st = "";
                //Check if root folder exists. If not - create it
                if (!Directory.Exists(ConfigManagement.ProgDocumentsPath))
                {
                    ConfigManagement.CreateDocumentsDirStructure();
                }
                //Log folder exists (Creation succeeds)?
                if (Directory.Exists(Path.Combine(ConfigManagement.ProgDocumentsPath, LOG_FOLDER_NAME) + "\\"))
                {
                    //use default folder
                    st = Path.Combine(ConfigManagement.ProgDocumentsPath, LOG_FOLDER_NAME) + "\\";
                }
                else
                {
                    //if not - use app folder
                    st = Application.StartupPath;
                }
                return st;
            }
            else
            {
                return LogFilePath;
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
            LOGLIST.Add(LogRec);
        }

        /// <summary>
        /// Dump to file Log Contents (LogList LIST)
        /// </summary>
        public static void DumpToFile(LogLevel LogLevel=LogLevel.All)
        {
            List<LogRecord> LogListNewAll = new List<LogRecord>();
            List<LogRecord> LogListNewMainOnly = new List<LogRecord>();

            //sort new (not saved) records
            for (var i=0; i < LOGLIST.Count; i++)
            {
                // if current line wasn't written to file
                if (!LOGLIST[i].dumpedToFile)
                {
                    LogListNewAll.Add(LOGLIST[i]); //add to newrecords array
                    if (LOGLIST[i].LogLevel <= LogLevel.Debug)
                        LogListNewMainOnly.Add(LOGLIST[i]); //add Important, Activity, Debug level only to array

                    LOGLIST[i].dumpedToFile = true; //mark as written
                }
            }

            //Save new (not saved) records
            if (LogListNewAll.Count > 0)
            {
                try
                {
                    // Write all (trace) log file 
                    using (StreamWriter LogFile = new StreamWriter(LogAllFileFullName, true))
                    {
                        for (var i = 0; i < LogListNewAll.Count; i++)
                        {
                            // if current log level is less then DebugLevel
                            if (LogListNewAll[i].LogLevel <= LogLevel)
                            {
                                //time
                                LogFile.Write("{0,-12}{1,-14}", LogListNewAll[i].Time.ToString("yyyy-MM-dd"), LogListNewAll[i].Time.ToString("HH:mm:ss.fff"));
                                //LogLevel
                                LogFile.Write("{0,-10}", LogListNewAll[i].LogLevel.ToString());
                                //message
                                LogFile.Write("{0}\t", LogListNewAll[i].Message);
                                LogFile.WriteLine();
                            }
                        }
                    }

                    // Write main (debug, activity, important) log file 
                    using (StreamWriter LogFile2 = new StreamWriter(LogMainFileFullName, true))
                    {
                        for (var i = 0; i < LogListNewMainOnly.Count; i++)
                        {
                            // if current log level is less then DebugLevel
                            if (LogListNewMainOnly[i].LogLevel <= LogLevel)
                            {
                                //time
                                LogFile2.Write("{0,-12}{1,-14}", LogListNewMainOnly[i].Time.ToString("yyyy-MM-dd"), LogListNewMainOnly[i].Time.ToString("HH:mm:ss.fff"));
                                //LogLevel
                                LogFile2.Write("{0,-10}", LogListNewMainOnly[i].LogLevel.ToString());
                                //message
                                LogFile2.Write("{0}\t", LogListNewMainOnly[i].Message);
                                LogFile2.WriteLine();
                            }
                        }
                    }
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("Log write error [" + Ex.Message + "]");
                }
            }

            //Cleanup old records
            CleanupLogList();

        }

        /// <summary>
        /// Dump to screen Log Contents
        /// </summary>
        public static string DumpToString(LogLevel LogLevel=LogLevel.Activity)
        {
            string RetStr = "";
            for (var i = 0; i < LOGLIST.Count; i++)
            {
                // if current line wasn't written to file
                if (!LOGLIST[i].displayed)
                {
                    // if current log level is less then DebugLevel
                    if (LOGLIST[i].LogLevel <= LogLevel)
                    {
                        RetStr += String.Format("{0} {1}", LOGLIST[i].Time.ToString("yyyy-MM-dd"), LOGLIST[i].Time.ToString("HH:mm:ss"));
                        RetStr += String.Format(": {0}", LOGLIST[i].Message) + Environment.NewLine;
                    }
                    LOGLIST[i].displayed = true;
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
            for(var i=0; i < LOGLIST.Count; i++)
            {
                // if current line wasn't displayed
                if (!LOGLIST[i].displayed)
                {
                    LogListNew.Add(LOGLIST[i]); //add to newrecords array
                    LOGLIST[i].displayed = true; //mark as written
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

        /// <summary>
        /// Maintain reasonable LOGLIST size
        /// </summary>
        private static void CleanupLogList()
        {
            if (LOGLIST.Count()> _MAX_LOGLIST_SIZE)
            {
                try
                {
                    //Clean LOGFILE from records already dumped
                    for (var i = _MAX_LOGLIST_SIZE; i < LOGLIST.Count; i++)
                    {
                        if (LOGLIST[i- _MAX_LOGLIST_SIZE].dumpedToFile)
                        {
                            // if current line was written to file remove it
                            LOGLIST.RemoveAt(i- _MAX_LOGLIST_SIZE);
                        }
                    }
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("Log error during cleanup [" + Ex.Message + "]");
                }
            }
        }

    }
}
