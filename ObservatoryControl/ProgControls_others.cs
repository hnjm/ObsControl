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
    // CCDAP class
    //
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// CCDAP class
    /// </summary>
    public class CCDAP_ExternatApplication : ExternalApplication
    {
        public CCDAP_ExternatApplication() : base()
        { }
    }


    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //
    // Focusmax class
    //
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Focusmax class
    /// </summary>
    public class FocusMax_ExternatApplication : ExternalApplication
    {
        public FocusMax_ExternatApplication() : base()
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
                }
                else
                {
                    Logging.AddLog("Astrotortilla Solver didn't found any solution. Returned code: " + Error, LogLevel.Activity, Highlight.Error);
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

    }


}
