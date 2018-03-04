using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

using LoggingLib;

namespace ObservatoryCenter
{
    public enum CCDAPCommandType
    {
        cmdInit,
        cmdPHD,
        cmdTargetLoop,
        cmdTargetData,
        cmdTelescopeSlew,
        cmdFocusing,
        cmdSolved

    }

    public class CCDAP_Command_class
    {
        public DateTime CommandDate;
        public string CommandText;
        public CCDAPCommandType CommandType;
        public string CommandTypeData;
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //
    // CCDAP class
    //
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// CCDAP class
    /// </summary>
    public class CCDAP_ExternatApplication : ExternalApplication
    {
        //Path lo log dir
        public string LogPath = @"c:\Users\Emchenko Boris\Documents\CCDWare\CCDAutoPilot5\Images\CCDAutoPilot_Logs";

        //Current log file link
        public FileInfo currentLogFile;

        private IEnumerable<string> contentsLogFile;

        public DateTime prevLogModifiedDate = new DateTime(1970,01,01);

        int _MAX_VALID_CCDAP_LOGFILE_NAMEAGE = 3600 * 12; //how old log file can be (in its name)
        int _MAX_VALID_CCDAP_LOGFILE_MODAGE = 3000; //how old log file can be (from last change)
        
        private Int32 prevLinesCount = 0;

        public Dictionary<string, CCDAP_Command_class> CCDAPKeywordsList = new Dictionary<string, CCDAP_Command_class>();



        public CCDAP_ExternatApplication() : base()
        { }


        /// <summary>
        /// Init CCDAP activity
        /// </summary>
        public void Init()
        {
            //init comand list
            initComandList();

            //more to test, because log is always old on CCDAP start
            GetLastLogFile();
        }
        
        
        /// <summary>
        /// init list of keywords
        /// </summary>
        private void initComandList()
        {
            CCDAPKeywordsList.Add("System Profile:", new CCDAP_Command_class() { CommandType= CCDAPCommandType.cmdInit });
            CCDAPKeywordsList.Add("System Profile", new CCDAP_Command_class() { CommandType = CCDAPCommandType.cmdInit });
        }


        /// <summary>
        /// Return FileInfo on last log file from CCDAP
        /// </summary>
        /// <returns></returns>
        internal FileInfo GetLastLogFile()
        {
            DirectoryInfo objLogDirectory;

            try
            {
                //Get directory where to search logs
                objLogDirectory = new DirectoryInfo(LogPath);

                //Get last file
                currentLogFile = (from f in objLogDirectory.GetFiles() orderby f.LastWriteTime descending select f).First();
            }
            catch (Exception ex)
            {
                Logging.AddLog("CCDAP LogPath is invalid", LogLevel.Debug, Highlight.Error);
                Logging.AddLog(MethodBase.GetCurrentMethod().Name + "error! [" + ex.ToString() + "]", LogLevel.Debug, Highlight.Error);
            }

            //Reset lines count
            //prevLinesCount = 0;

            return currentLogFile;
        }

        /// <summary>
        /// Check - if selected log file is current running log file
        /// </summary>
        /// <returns></returns>
        internal bool checkLogFileIsValid()
        {
            bool resNameAgeValid = false;
            bool resChangeAgeValid = false;
            bool resContentsValid = false;
            bool resFinal = false;
            DateTime LogDate = new DateTime();

            // Get file name only
            // format ccdap20170212_124827
            string curName = Path.GetFileNameWithoutExtension(currentLogFile.Name);


            //1. Check date from name
            if (curName.Substring(0,5) == "ccdap")
            {
                if (DateTime.TryParseExact(curName.Substring(5), "yyyyMMdd_HHmmss", CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal, out LogDate))
                {
                    TimeSpan LogFileAge = DateTime.Now - LogDate;
                    if (LogFileAge.TotalSeconds > _MAX_VALID_CCDAP_LOGFILE_NAMEAGE)
                    {
                        //log file is too old
                        resNameAgeValid = false; 
                    }
                    else
                    {
                        resNameAgeValid = true;
                    }
                }
                else
                {
                    resNameAgeValid = false; //date invalid
                }
            }
            else
            {
                //имя файла не по шаблону
                resNameAgeValid = false;
            }


            //2. Check file change date
            int sinceMod = GetTimeSinceLogFileModified();
            resChangeAgeValid = (sinceMod < _MAX_VALID_CCDAP_LOGFILE_MODAGE) || true;


            //3. Check file contents if all dates are in range
            if (resNameAgeValid && resChangeAgeValid)
            {
                resContentsValid = true;
                contentsLogFile = File.ReadLines(currentLogFile.FullName);
                var LastThreeLines = contentsLogFile.Reverse().Take(3);

                foreach (string StLn in LastThreeLines)
                {
                    if (StLn.Contains("Session completed"))
                    {
                        resContentsValid = false;
                        break;
                    }
                }
            }


            resFinal = resNameAgeValid && resChangeAgeValid && resContentsValid;

            return resFinal;
        }


        internal Int32 GetTimeSinceLogFileModified()
        {
            TimeSpan SinceMod = DateTime.Now - currentLogFile.LastWriteTime;
            return (int)SinceMod.TotalSeconds;
        }


        public string MessageText;

        /// <summary>
        /// Read and parse line by line CCDAP Log file
        /// </summary>
        internal void ParseLogFile()
        {
            //count log file lines
            var lineCount = contentsLogFile.Count();

            //Count new (not earlier parsed) lines
            //string line = File.ReadLines(FileName).Skip(14).Take(1).First();
            var notReadLines = contentsLogFile.Skip(prevLinesCount);

            //Loop throug all lines
            bool needConcat = false;
            string strDataConcat = "";
            DateTime curLineTime;
            string curLineData = "";

            string RetStr = "";

            foreach (string curLine in notReadLines)
            {

                //1. Check date filed
                if (curLine.Length >= 8 && DateTime.TryParse(curLine.Substring(0, 8), out curLineTime))
                {
                    //A. Line starts with time

                    RetStr = String.Format("{0}", curLineTime.ToString("HH:mm:ss"));
                    RetStr += String.Format(": {0}", curLine) + Environment.NewLine;

                    //1. check - if previously was concatenate mode
                    if (needConcat)
                    {
                        //use this data for prev line
                        needConcat = false;
                        curLineData = strDataConcat;
                    }
                    
                    //2. Check if there is associated data in this line
                    curLineData = curLine.Substring(9);
                    if (curLineData.Length > 2)
                    {
                        //use this data for cur line
                        //rDataConcat
                    }
                    else
                    {
                        // then prepare to concat next lines
                        needConcat = true;
                        strDataConcat = "";
                    }
                }
                else
                {
                    //B. no date field - concatenate lines!
                    strDataConcat += curLine;
                }



                //2. parse lines
                if (curLine.Contains(""))
                {

                }
                else if (curLine.Contains(""))
                {

                }
            }

            MessageText = RetStr; //for debugging. Acually it will return last line with date in it. But i want to return meaningfull events

            prevLinesCount = lineCount;
        }


        private void ParseCommandLine()
        {
        
        }

    }

    //something old
    class CCDAPParser_____
    {
        const int MAX_LINES = 10000; //max lines in log

        public string PathToLog = "c:\\Users\\admin\\Documents\\Visual Studio 2012\\Projects\\ObservatoryControl\\";
        string FileName = "ccdap20150509_021209.log";

        /// <summary>
        /// Signatures
        /// </summary>
        string Sign_ReadStart = "Session starting on ";
        string Sign_ReadEnd = "Dusk:                 ";
        string[] AllLines = new string[MAX_LINES]; //only allocate memory here

        public void OpenLog()
        {
            bool bStartRead = false;

            using (StreamReader sr = File.OpenText(PathToLog + FileName))
            {
                string s = String.Empty;
                int i = 0;
                while ((s = sr.ReadLine()) != null)
                {
                    //start reading
                    if (s == Sign_ReadStart)
                    {
                        bStartRead = true;
                    }
                    //end reading
                    if (s == Sign_ReadEnd)
                    {
                        bStartRead = true;
                    }

                    if (bStartRead)
                    {
                        AllLines[i++] = s;
                    }
                }
            }
        }

        public void ParseActions()
        {
        }

    }

}
