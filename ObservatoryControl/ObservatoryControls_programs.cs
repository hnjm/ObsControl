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



    public partial class ObservatoryControls
    {
        public static string CdC_ProcessName = "skychart.exe";
        public string PlanetariumPath = @"c:\Program Files (x86)\Ciel\" + CdC_ProcessName;
        public string PHD2Path = @"c:\Program Files (x86)\PHDGuiding2\phd2.exe";

        public string MaximDLPath = @"c:\Program Files (x86)\Diffraction Limited\MaxIm DL V5\MaxIm_DL.exe";
        public string CCDAPPath = @"c:\Program Files (x86)\CCDWare\CCDAutoPilot5\CCDAutoPilot5.exe";
        public string FocusMaxPath = @"c:\Program Files (x86)\FocusMax\FocusMax.exe";
        public string PHDBrokerPath = @"c:\Users\Emchenko Boris\Documents\CCDWare\CCDAutoPilot5\Scripts\PHDBroker_Server.exe";

        public CdC_ExternatApplication objCdCApp;
        public PHD_ExternatApplication objPHD2App;
        public PHDBroker_ExternatApplication objPHDBrokerApp;

        public Process MaximDL_Process = new Process();
        public Process CCDAP_Process = new Process();
        public Process FocusMax_Process = new Process();


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
            objPHD2App.Name = "PHDBroker_Server";
            objPHD2App.FullName = PHDBrokerPath;




            //MaximDL
            objPHD2App = new PHD_ExternatApplication();
            objPHD2App.Name = "phd2";
            objPHD2App.FullName = PHD2Path;
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

        public string startMaximDL()
        {
            /*MaximDL_Process.StartInfo.FileName = MaximDLPath;
            MaximDL_Process.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
            MaximDL_Process.StartInfo.UseShellExecute = false;
            MaximDL_Process.Start();

            MaximDL_Process.WaitForInputIdle(); //WaitForProcessStartupComplete
            */
            string output = MaximObj.Run();
            return output;
        }

        public string startCCDAP()
        {
            try
            {
                CCDAP_Process.StartInfo.FileName = CCDAPPath;
                CCDAP_Process.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
                CCDAP_Process.StartInfo.UseShellExecute = false;
                CCDAP_Process.Start();
                Logging.AddLog("CCDAP started", LogLevel.Activity);
                return "CCDAP started";

            }
            catch (Exception Ex)
            {
                Logging.AddLog("CCDAP starting error! " + Ex.Message, LogLevel.Important, Highlight.Error);
                return "!!!CCDAP start failed";
            }
        }

        public string startFocusMax()
        {
            try
            {
                FocusMax_Process.StartInfo.FileName = FocusMaxPath;
                FocusMax_Process.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
                FocusMax_Process.StartInfo.UseShellExecute = false;
                FocusMax_Process.Start();

                FocusMax_Process.WaitForInputIdle(); //WaitForProcessStartupComplete
                Logging.AddLog("FocusMax started", LogLevel.Activity);
                Thread.Sleep(1000);
                return "FocusMax started";
            }
            catch (Exception ex)
            {
                StackTrace st = new StackTrace(ex, true);
                StackFrame[] frames = st.GetFrames();
                string messstr = "";

                // Iterate over the frames extracting the information you need
                foreach (StackFrame frame in frames)
                {
                    messstr += String.Format("{0}:{1}({2},{3})", frame.GetFileName(), frame.GetMethod().Name, frame.GetFileLineNumber(), frame.GetFileColumnNumber());
                }

                string FullMessage = "FocusMax starting failed!" + Environment.NewLine;
                FullMessage += Environment.NewLine + Environment.NewLine + "Debug information:" + Environment.NewLine + "IOException source: " + ex.Data + " " + ex.Message
                        + Environment.NewLine + Environment.NewLine + messstr;
                //MessageBox.Show(this, FullMessage, "Invalid value", MessageBoxButtons.OK);

                Logging.AddLog("FocusMax start failed", LogLevel.Important, Highlight.Error);
                Logging.AddLog(FullMessage, LogLevel.Debug, Highlight.Error);
                return "!!!FocusMax start failed";

            }
        }
        #endregion Program controlling


    }

}
