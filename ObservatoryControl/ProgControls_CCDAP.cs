using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

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
        public string LogPath = @"c:\Users\Emchenko Boris\Documents\CCDWare\CCDAutoPilot5\Images\CCDAutoPilot_Logs";
        public FileInfo currentLogFile;

        private Int32 prevLinesCount = 0;

        public Dictionary<string, CCDAP_Command_class> CCDAPKeywordsList = new Dictionary<string, CCDAP_Command_class>();

        public CCDAP_ExternatApplication() : base()
        { }

        /// <summary>
        /// init list of keywords
        /// </summary>
        internal void initComandList()
        {
            CCDAPKeywordsList.Add("System Profile:", new CCDAP_Command_class() { CommandType= CCDAPCommandType.cmdInit });
            CCDAPKeywordsList.Add("System Profile", new CCDAP_Command_class() { CommandType = CCDAPCommandType.cmdInit });
        }

        /// <summary>
        /// Return FileInfo on last log file from CCDAP
        /// </summary>
        /// <returns></returns>
        internal FileInfo GetCurrentLogFile()
        {
            var directory = new DirectoryInfo(LogPath);

            //Get last file
            currentLogFile = (from f in directory.GetFiles()
                          orderby f.LastWriteTime descending
                          select f).First();

            //Reset lines count
            prevLinesCount = 0;

            return currentLogFile;

        }

        /// <summary>
        /// Read and parse line by line CCDAP Log file
        /// </summary>
        internal void ParseLogFile()
        {
            //count log file lines
            var lineCount = File.ReadLines(currentLogFile.FullName).Count();

            //Count new (not earlier parsed) lines
            //string line = File.ReadLines(FileName).Skip(14).Take(1).First();
            var notReadLines = File.ReadLines(currentLogFile.FullName).Skip(prevLinesCount);

            //Loop throug all lines
            bool needConcat = false;
            string strDataConcat = "";
            DateTime curLineTime;
            string curLineData = "";

            foreach (string curLine in notReadLines)
            {

                //1. Check date filed
                if (DateTime.TryParse(curLine.Substring(0, 9), out curLineTime))
                {
                    //A. Line starts with time

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

            prevLinesCount = lineCount;
        }


        private void ParseCommandLine()
        {
        
        }

    }

    //something old
    class CCDAPParser
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
