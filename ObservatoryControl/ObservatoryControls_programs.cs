using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;

namespace ObservatoryCenter
{
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //
    // Abstract class for all external application
    //
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Abstract class for all external application
    /// </summary>
    public abstract class ExternalApplication
    {
        public string Name = ""; //filename
        public string FullName = ""; //path to run
        public string ParameterString= "";


        /// <summary>
        /// Containg error string message if 
        /// </summary>
        public string ErrorSt = "";
        public int Error = 0;

        public bool CanRunMultipleInstances = false; //true to bypass already run check

        private Process objProcess = new Process();

        /// <summary>
        /// Constructor
        /// </summary>
        public ExternalApplication()
        {        }


        /// <summary>
        /// Run application
        /// </summary>
        /// <returns>true - if process is running (also was run earlier)</returns>
        public bool Run()
        {
            try
            {
                if (!CanRunMultipleInstances && !this.IsRunning())
                { 
                    objProcess.StartInfo.FileName = FullName;
                    objProcess.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
                    objProcess.StartInfo.UseShellExecute = false;
                    objProcess.StartInfo.Arguments = ParameterString;
                    objProcess.Start();
                    Logging.AddLog("Process ["+ Name + "] started", 0);
                    ErrorSt = "";
                    Error = 0;
                    return true;
                }
                else
                {
                    ErrorSt = "Process [" + Name + "] already running";
                    Error = 1;
                    Logging.AddLog(ErrorSt, 0, Highlight.Error);

                    return true;
                }

            }
            catch (Exception Ex)
            {
                ErrorSt = "Process [" + Name + "] starting error! " + Ex.Message;
                Error = -1;
                Logging.AddLog(ErrorSt, 0, Highlight.Error);
               
                return false;
            }
        }

        /// <summary>
        /// Check if process is running.
        ///</summary>
        /// <param name="name">Process name. For Maxim it is "Maxim_DL"</param>
        /// <returns></returns>
        public bool IsRunning()
        {
            foreach (Process clsProcess in Process.GetProcesses())
            {
                if (clsProcess.ProcessName.Contains(Name))
                {
                    return true;
                }
            }
            return false;
        }
    }

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

        public CdC_ExternatApplication (): base()
        { }

        public string ConnectTelescope()
        {
            string output = SocketServerClass.MakeClientConnectionToServer(IPAddress.Parse("127.0.0.1"), ServerPort, "CONNECTTELESCOPE\r\n", out Error);
            ErrorSt = output;
            //Error = 0;
            if (Error<0)
            {
                Logging.AddLog("Telescope connection error in CdC", LogLevel.Important, Highlight.Error);
            }
            else
            {
                Logging.AddLog("Telescope connected in CdC",LogLevel.Activity);
            }
            return output;
        }
    }



    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //
    // PhD2 class
    //
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// PHD2 class
    /// </summary>
    public class PHD_ExternatApplication : ExternalApplication
    {
        public Int32 ServerPort = 4400; //port to connect socket server

        public PHD_ExternatApplication() : base()
        { }

        public string ConnectEquipment()
        {
            string message = @"{""method"": ""set_connected"", ""params"": [true], ""id"": 1}";
            message = message + "\r\n";
            string output = SocketServerClass.MakeClientConnectionToServer(IPAddress.Parse("127.0.0.1"), ServerPort, message, out Error);
            if (!output.Contains("{\"jsonrpc\":\"2.0\",\"result\":0,\"id\":1}\r\n"))
            {
                Error = -2;
            }
            //good response: {"jsonrpc":"2.0","result":0,"id":2}
            //bad response:  {"jsonrpc":"2.0","error":{"code":1,"message":"cannot connect equipment when Connect Equipment dialog is open"},"id":1}
            ErrorSt = output;
            //Error = 0;
            if (Error < 0)
            {
                Logging.AddLog("PHD2 equipment connection error", LogLevel.Important, Highlight.Error);
                Logging.AddLog("Error: "+ ErrorSt, LogLevel.Chat, Highlight.Error);
                
            }
            else
            {
                Logging.AddLog("PHD2 equipment connected", LogLevel.Activity);
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

    public partial class ObservatoryControls
    {
        public static string CdC_ProcessName = "skychart.exe";
        public string PlanetariumPath = @"c:\Program Files (x86)\Ciel\" + CdC_ProcessName;
        public string PHD2Path = @"c:\Program Files (x86)\PHDGuiding2\phd2.exe";
        public string PHDBrokerPath = @"c:\Users\Emchenko Boris\Documents\CCDWare\CCDAutoPilot5\Scripts\PHDBroker_Server.exe";
        public string CCDAPPath = @"c:\Program Files (x86)\CCDWare\CCDAutoPilot5\CCDAutoPilot5.exe";

        public string MaximDLPath = @"c:\Program Files (x86)\Diffraction Limited\MaxIm DL V5\MaxIm_DL.exe";
        public string FocusMaxPath = @"c:\Program Files (x86)\FocusMax\FocusMax.exe";

        public CdC_ExternatApplication objCdCApp;
        public PHD_ExternatApplication objPHD2App;
        public PHDBroker_ExternatApplication objPHDBrokerApp;
        public CCDAP_ExternatApplication objCCDAPApp;
        public FocusMax_ExternatApplication objFocusMaxApp;

        public Maxim_ExternatApplication objMaxim;


        public Process MaximDL_Process = new Process();


        public void InitProgramsObj()
        {
            //Cartes du Ciel
            objCdCApp = new CdC_ExternatApplication();
            objCdCApp.Name = "skychart";
            objCdCApp.FullName = PlanetariumPath;
            objCdCApp.ParameterString = "--unique";

            //PHD2
            objPHD2App = new PHD_ExternatApplication();
            objPHD2App.Name = "phd2";
            objPHD2App.FullName = PHD2Path;

            //PHDBroker
            objPHDBrokerApp = new PHDBroker_ExternatApplication();
            objPHDBrokerApp.Name = "PHDBroker_Server";
            objPHDBrokerApp.FullName = PHDBrokerPath;

            //CCDAP
            objCCDAPApp = new CCDAP_ExternatApplication();
            objCCDAPApp.Name = "CCDAutoPilot5";
            objCCDAPApp.FullName = CCDAPPath;

            //FocusMax
            objFocusMaxApp = new FocusMax_ExternatApplication();
            objFocusMaxApp.Name = "FocusMax";
            objFocusMaxApp.FullName = FocusMaxPath;

            //MaxIm_DL
            objMaxim = new Maxim_ExternatApplication();
            objMaxim.Name = "MaxIm_DL";
            objMaxim.FullName = MaximDLPath;
            

        }

        #region Programs Controlling  ///////////////////////////////////////////////////////////////////
        public string startPlanetarium()
        {
            objCdCApp.Run();
            return objCdCApp.ErrorSt;
        }

        public string startPHD2()
        {
            objPHD2App.Run();
            return objPHD2App.ErrorSt;
        }

        public string startPHDBroker()
        {
            objPHDBrokerApp.Run();
            return objPHDBrokerApp.ErrorSt;
        }

        public string startCCDAP()
        {
            objCCDAPApp.Run();
            return objCCDAPApp.ErrorSt;
        }

        public string startFocusMax()
        {
            objFocusMaxApp.Run();
            return objFocusMaxApp.ErrorSt;
        }

        public string startMaximDL()
        {
            /*MaximDL_Process.StartInfo.FileName = MaximDLPath;
            MaximDL_Process.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
            MaximDL_Process.StartInfo.UseShellExecute = false;
            MaximDL_Process.Start();

            MaximDL_Process.WaitForInputIdle(); //WaitForProcessStartupComplete
            */
            //string output = objMaxim.Run();
            //return output;


            objMaxim.Run();
            objMaxim.Init();
            return objMaxim.ErrorSt;
        }

        #endregion Program controlling


    }

}
