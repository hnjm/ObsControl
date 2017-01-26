using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;

namespace ObservatoryCenter
{
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //
    // Cartes du Ciel class
    //
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Cartes Du Ciel class
    /// </summary>
    public class CdC_ExternatApplication : ExternalApplication
    {
        public Int32 ServerPort = 3292; //port to connect socket server

        public CdC_ExternatApplication() : base()
        { }

        public string ConnectTelescope()
        {
            string output = SocketServerClass.ConnectToServerAndSendMessage(IPAddress.Parse("127.0.0.1"), ServerPort, "CONNECTTELESCOPE\r\n", out Error);
            ErrorSt = output;
            //Error = 0;
            if (Error < 0)
            {
                Logging.AddLog("Telescope connection error in CdC", LogLevel.Important, Highlight.Error);
            }
            else
            {
                Logging.AddLog("Telescope connected in CdC", LogLevel.Activity);
            }
            return output;
        }

        public string GET_TelescopePos()
        {
            string output = SocketServerClass.ConnectToServerAndSendMessage(IPAddress.Parse("127.0.0.1"), ServerPort, "GETSCOPERADEC\r\n", out Error);
            ErrorSt = output;
            Logging.AddLog("GET_TelescopePos: "+ output, LogLevel.Debug, Highlight.Error);

            return output;
        }

    }




    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //
    // PHD Broker class
    //
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// PHD Broker class
    /// </summary>
    public class PHDBroker_ExternatApplication : ExternalApplication
    {
        public PHDBroker_ExternatApplication() : base()
        { }
    }



    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //
    // AstroTortilla class
    //
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// AstroTortilla class
    /// </summary>
    public class AstroTortilla : ExternalApplication
    {
        public AstroTortilla() : base()
        { }


        private Process objProcessAutoIt = new Process();
        public string FullNameAutoIt = ""; //path to run

        internal bool? LastAttemptSolvedFlag = null;
        internal string LastAttemptMessage = "";

        public string ResultFilename = "astrotortilla_result.txt";

        internal string ResFlag, Solution_RA, Solution_Dec, Current_RA, Current_Dec, Target_RA, Target_Dec;

        public int Solve()
        {
            Error = -1;

            try
            {
                objProcessAutoIt.StartInfo.FileName = FullNameAutoIt;
                objProcessAutoIt.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                objProcessAutoIt.StartInfo.UseShellExecute = true;
                objProcessAutoIt.Start();
                objProcessAutoIt.WaitForInputIdle(10000); //wait for program to start
                Logging.AddLog("Astrotortilla Solver started", LogLevel.Activity);
                ErrorSt = "";
                Error = 0;

                do
                {
                    if (!objProcessAutoIt.HasExited)
                    {
                        if (!objProcessAutoIt.Responding)
                        {
                            Logging.AddLog("Astrotortilla Solver not responding", LogLevel.Debug, Highlight.Error);
                        }
                        Logging.AddLog("Astrotortilla Solver still running", LogLevel.Trace);
                    }
                }
                while (!objProcessAutoIt.WaitForExit(1000));

                Error = objProcessAutoIt.ExitCode;
                ErrorSt = objProcessAutoIt.ExitCode.ToString();

                if (Error == 0)
                {
                    Logging.AddLog("Astrotortilla Solver has found solution!", LogLevel.Activity);

                    ReadResultDetails();

                    LastAttemptSolvedFlag = true;
                    LastAttemptMessage = "Attempt run at " + DateTime.Now.ToString("HH:mm:ss") + ". Solved";
                }
                else
                {
                    Logging.AddLog("Astrotortilla Solver didn't found any solution. Returned code: " + Error, LogLevel.Activity, Highlight.Error);

                    ReadResultDetails();

                    LastAttemptSolvedFlag = false;
                    LastAttemptMessage = "Attempt run at "+ DateTime.Now.ToString("HH:mm:ss") + ". No solution was found";
                }

                return Error;
            }
            catch (Exception Ex)
            {
                ErrorSt = "Astrotortilla Solver starting error! " + Ex.Message;
                Error = -1;
                Logging.AddLog(ErrorSt, LogLevel.Important, Highlight.Error);

                return Error;
            }
        }

        private void ReadResultDetails()
        {
            string FullResultFilename = System.IO.Path.GetTempPath() + ResultFilename;

            try
            {
                string[] lines = System.IO.File.ReadAllLines(FullResultFilename);


                // Check contents
                if (lines[0].Substring(0, 5) == "Time:")
                {
                    //Seems to be the right file
                    ResFlag = lines[1].Substring(8);
                    Solution_RA = lines[2].Substring(12);
                    Solution_Dec = lines[3].Substring(13);
                    Current_RA = lines[4].Substring(11);
                    Current_Dec = lines[5].Substring(12);
                    Target_RA = lines[6].Substring(10);
                    Target_Dec = lines[7].Substring(11);
                }
            }
            catch (System.IO.IOException Ex)
            {
                Logging.AddLog("Astrotortilla result file read error [" + Ex.Message + "]", LogLevel.Important, Highlight.Error);
            }
            catch (Exception Ex)
            {
                Logging.AddLog("Astrotortilla result file read error [" + Ex.Message + "]", LogLevel.Important, Highlight.Error);
            }

        }
    }


}
