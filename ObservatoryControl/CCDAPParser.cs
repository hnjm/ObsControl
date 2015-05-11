using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ObservatoryCenter
{
    class CCDAPParser
    {
        const int MAX_LINES=10000; //max lines in log
        
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
                int i=0;
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
