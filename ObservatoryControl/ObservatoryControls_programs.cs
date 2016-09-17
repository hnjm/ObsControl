using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
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
        public string ParameterString= ""; //parameters if needed


        /// <summary>
        /// Containg error string message if 
        /// </summary>
        public string ErrorSt = "";
        public int Error = 0;

        public bool CanRunMultipleInstances = false; //true to bypass already run check
        private int waitForProgramStartTimeOut = 10000; //how long to wait for program to start

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
                    objProcess.WaitForInputIdle(waitForProgramStartTimeOut); //wait for program to start
                    Logging.AddLog("Process ["+ Name + "] started", LogLevel.Activity);
                    ErrorSt = "";
                    Error = 0;
                    return true;
                }
                else
                {
                    ErrorSt = "Process [" + Name + "] already running";
                    Error = 1;
                    Logging.AddLog(ErrorSt, LogLevel.Important, Highlight.Error);

                    return true;
                }

            }
            catch (Exception Ex)
            {
                ErrorSt = "Process [" + Name + "] starting error! " + Ex.Message;
                Error = -1;
                Logging.AddLog(ErrorSt, LogLevel.Important, Highlight.Error);
               
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
                    Logging.AddLog("Process "+Name+" is found", LogLevel.Trace);
                    return true;
                }
            }
            Logging.AddLog("Process " + Name + " not found in running processes", LogLevel.Trace);
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
            string output = SocketServerClass.ConnectToServerAndSendMessage(IPAddress.Parse("127.0.0.1"), ServerPort, "CONNECTTELESCOPE\r\n", out Error);
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

        Socket ProgramSocket = null;

        public PHD_ExternatApplication() : base()
        { }

        public string ConnectEquipment()
        {
            string message = @"{""method"": ""set_connected"", ""params"": [true], ""id"": 1}";
            message = message + "\r\n";
            string output = SocketServerClass.ConnectToServerAndSendMessage(IPAddress.Parse("127.0.0.1"), ServerPort, message, out Error);
            string attribs = "", id="";
            ParseServerMessages(output, out attribs, out id);
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

        public string EstablishConnection()
        {
            string st = "";
            if (ServerPort == null)
            {
                //Connect
                string output = SocketServerClass.ConnectToServer(IPAddress.Parse("127.0.0.1"), ServerPort, out ProgramSocket, out Error);
                if (Error >= 0)
                {
                    Thread.Sleep(3000);

                    //Read response
                    string output2 = SocketServerClass.ReceiveFromServer(ProgramSocket, out Error);

                    //Parse response
                    string attribs = "", id = "";
                    st = ParseServerMessages(output2, out attribs, out id);
                }
            }
            else
            {
                Logging.AddLog("Already connected",LogLevel.Activity);
            }
            return "";
        }

        public string CMD_ConnectEquipment()
        {
            if (ProgramSocket==null)
            {
                EstablishConnection();
            }
            
            //Send command
            string message = @"{""method"": ""set_connected"", ""params"": [true], ""id"": 1}";
            string output = SocketServerClass.SendToServer(ProgramSocket, message, out Error);

            if (Error >= 0)
            {

                Thread.Sleep(3000);

                //Read response
                string output2 = SocketServerClass.ReceiveFromServer(ProgramSocket, out Error);

                //Parse response
                string attribs = "", id = "";
                ParseServerMessages(output2, out attribs, out id);

                //Check
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
                    Logging.AddLog("Error: " + ErrorSt, LogLevel.Chat, Highlight.Error);

                }
                else
                {
                    Logging.AddLog("PHD2 equipment connected", LogLevel.Activity);
                }
            }
            else
            {
                Logging.AddLog("PHD2 equipment connection error", LogLevel.Important, Highlight.Error);
                Logging.AddLog("Error: " + ErrorSt, LogLevel.Chat, Highlight.Error);
            }
            return output;
        }

        public void CheckProgramEvents()
        {
            //Read response
            string output = SocketServerClass.ReceiveFromServer(ProgramSocket, out Error);

            //Parse response
            string attribs = "", id = "";
            ParseServerMessages(output, out attribs, out id);
        }

        private string ParseServerMessages(string responsestr, out string attributes, out string id)
        {
            //{"Event":"Version","Timestamp":1474143595.908,"Host":"MAIN","Inst":1,"PHDVersion":"2.6.2","PHDSubver":"","MsgVersion":1}
            //{"Event":"CalibrationComplete","Timestamp":1474143595.908,"Host":"MAIN","Inst":1,"Mount":"On Camera"}
            //{"Event":"AppState","Timestamp":1474143595.908,"Host":"MAIN","Inst":1,"State":"Stopped"}
            //good response: {"jsonrpc":"2.0","result":0,"id":2}
            //bad response:  {"jsonrpc":"2.0","error":{"code":1,"message":"cannot connect equipment when Connect Equipment dialog is open"},"id":1}

            string st = "";
            id = "-1";
            attributes = "";

            if (responsestr!=null && responsestr.Length > 7 && responsestr.Substring(2, 5) == "jsonr")
            {
                //Server response
                Match match = Regex.Match(responsestr, @"{""jsonrpc"":""(.+)"",""(.+)"":(.+),""id"":(\d+)}");
                if (match.Success)
                {
                    //Group 0 - all string
                    //Group 1 - JSON RPC protocol version
                    //Group 2 - response tag ("result" or "error")
                    //Group 3 - response text ("0" for result ok, text for error)
                    //Group 4 - id
                    st = match.Groups[2].Value;
                    attributes = match.Groups[3].Value;
                    id = match.Groups[4].Value;
                }

            }
            else if (responsestr != null && responsestr.Length > 7 && responsestr.Substring(2, 5) == "Event")
            {
                //Event message
                Match match = Regex.Match(responsestr, @"{""Event"":""(.+)"",""Timestamp"":(.+),""Host"":""(.+)"",""Inst"":(\d+),(.+)}");
                if (match.Success)
                {
                    //Group 0 - all string
                    //Group 1 - the name of the event
                    //Group 2 - the timesamp of the event in seconds from the epoch, including fractional seconds
                    //Group 3 - the hostname of the machine running PHD
                    //Group 4 - the PHD instance number
                    //Group 5 - attribute string (e.g. for "Version": "PHDVersion":"2.0.4","PHDSubver":"a","MsgVersion":1)
                    st = match.Groups[1].Value;
                    attributes = match.Groups[5].Value;
                    id = match.Groups[4].Value;
                }
            }
            else
            {

            }

            return st;
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
        //public string PlanetariumPath = @"c:\Program Files (x86)\Ciel\skychart.exe";
        //public string PHD2Path = @"c:\Program Files (x86)\PHDGuiding2\phd2.exe";
        //public string PHDBrokerPath = @"c:\Users\Emchenko Boris\Documents\CCDWare\CCDAutoPilot5\Scripts\PHDBroker_Server.exe";
        //public string CCDAPPath = @"c:\Program Files (x86)\CCDWare\CCDAutoPilot5\CCDAutoPilot5.exe";

        //public string MaximDLPath = @"c:\Program Files (x86)\Diffraction Limited\MaxIm DL V5\MaxIm_DL.exe";
        //public string FocusMaxPath = @"c:\Program Files (x86)\FocusMax\FocusMax.exe";

        public CdC_ExternatApplication objCdCApp;
        public PHD_ExternatApplication objPHD2App;
        public PHDBroker_ExternatApplication objPHDBrokerApp;
        public CCDAP_ExternatApplication objCCDAPApp;
        public FocusMax_ExternatApplication objFocusMaxApp;

        public Maxim_ExternatApplication objMaxim;


        //public Process MaximDL_Process = new Process();


        public void InitProgramsObj()
        {
            //Cartes du Ciel
            objCdCApp = new CdC_ExternatApplication();
            objCdCApp.Name = "skychart";
            objCdCApp.FullName = ObsConfig.getString("programsPath", "CdC") ?? @"c:\Program Files (x86)\Ciel\skychart.exe"; 
            objCdCApp.ParameterString = "--unique";

            //PHD2
            objPHD2App = new PHD_ExternatApplication();
            objPHD2App.Name = "phd2";
            objPHD2App.FullName = ObsConfig.getString("programsPath", "PHD2") ?? @"c:\Program Files (x86)\PHDGuiding2\phd2.exe";

            //PHDBroker
            objPHDBrokerApp = new PHDBroker_ExternatApplication();
            objPHDBrokerApp.Name = "PHDBroker_Server";
            //objPHDBrokerApp.FullName = PHDBrokerPath;
            objPHDBrokerApp.FullName = ObsConfig.getString("programsPath", "PHDBROKER") ?? @"c:\Users\Emchenko Boris\Documents\CCDWare\CCDAutoPilot5\Scripts\PHDBroker_Server.exe";

            //CCDAP
            objCCDAPApp = new CCDAP_ExternatApplication();
            objCCDAPApp.Name = "CCDAutoPilot5";
            objCCDAPApp.FullName = ObsConfig.getString("programsPath", "CCDAP") ?? @"c:\Program Files (x86)\CCDWare\CCDAutoPilot5\CCDAutoPilot5.exe";

            //FocusMax
            objFocusMaxApp = new FocusMax_ExternatApplication();
            objFocusMaxApp.Name = "FocusMax";
            objFocusMaxApp.FullName = ObsConfig.getString("programsPath", "FOCUSMAX") ?? @"c:\Program Files (x86)\FocusMax\FocusMax.exe";

            //MaxIm_DL
            objMaxim = new Maxim_ExternatApplication();
            objMaxim.Name = "MaxIm_DL";
            objMaxim.FullName = ObsConfig.getString("programsPath", "MAXIMDL") ?? @"c:\Program Files (x86)\Diffraction Limited\MaxIm DL V5\MaxIm_DL.exe";
            

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
            objPHD2App.EstablishConnection();
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
            objMaxim.Run(); //Run maximdl
            objMaxim.Init(); //Init maxin objects 
            return objMaxim.ErrorSt;
        }

        #endregion Program controlling


    }

}
