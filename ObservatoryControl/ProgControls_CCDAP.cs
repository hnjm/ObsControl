using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ObservatoryCenter
{
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

        public CCDAP_ExternatApplication() : base()
        { }


        internal FileInfo GetCurrentLogFile()
        {
            var directory = new DirectoryInfo(LogPath);
            currentLogFile = (from f in directory.GetFiles()
                          orderby f.LastWriteTime descending
                          select f).First();
            return currentLogFile;

        }

        internal void ReadLogFile()
        {
            var lineCount = File.ReadLines(currentLogFile.FullName).Count();

            //string line = File.ReadLines(FileName).Skip(14).Take(1).First();
            var notReadLines = File.ReadLines(currentLogFile.FullName).Skip(prevLinesCount);

            foreach(string curLine in notReadLines)
            {
                //parse lines
                if (curLine.Contains(""))
                {

                }
                else if (curLine.Contains(""))
                {

                }
            }

            prevLinesCount = lineCount;
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
