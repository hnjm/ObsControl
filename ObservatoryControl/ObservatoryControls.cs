using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Net;
using System.Threading;
using System.Reflection;
using System.Windows.Forms;

using ASCOM;
using ASCOM.DeviceInterface;
using ASCOM.Utilities;

using MaxIm;

namespace ObservatoryCenter
{
    public partial class ObservatoryControls
    {
        /// <summary>
        /// Back link to form
        /// </summary>
        public MainForm ParentMainForm;

        public static string CdC_ProcessName = "skychart.exe";
        public string PlanetariumPath = @"c:\Program Files (x86)\Ciel\" + CdC_ProcessName;
        public string MaximDLPath=@"c:\Program Files (x86)\Diffraction Limited\MaxIm DL V5\MaxIm_DL.exe" ;
        public string CCDAPPath=@"c:\Program Files (x86)\CCDWare\CCDAutoPilot5\CCDAutoPilot5.exe";
        public string FocusMaxPath = @"c:\Program Files (x86)\FocusMax\FocusMax.exe";

        public Process MaximDL_Process = new Process();
        public Process CCDAP_Process = new Process();
        public Process CdC_Process = new Process();
        public Process FocusMax_Process = new Process();

        public MaximControls MaximObj;

        public ASCOM.DriverAccess.Telescope objTelescope = null;
        public ASCOM.DriverAccess.Dome objDome = null;
        public ASCOM.DriverAccess.Switch objSwitch = null;
        public ASCOM.DriverAccess.Focuser objFocuser = null;
        public ASCOM.DriverAccess.Camera objCamera = null;

        public string SWITCH_DRIVER_NAME = "";
        public string DOME_DRIVER_NAME = "";
        public string TELESCOPE_DRIVER_NAME = "";

        public Int32 CdC_PORT = 3292;

        internal DateTime RoofRoutine_StartTime;
        internal int curRoofRoutineDuration_Seconds;

        public ASCOM.Utilities.Util ASCOMUtils=new ASCOM.Utilities.Util();

        /// <summary>
        /// Command dictionary for interpretator
        /// </summary>
        public CommandInterpretator CommandParser;

        #region switch ports
        public byte POWER_MOUNT_PORT = 6;
        public byte POWER_CAMERA_PORT = 5;
        public byte POWER_FOCUSER_PORT = 3;
        public byte POWER_ROOFPOWER_PORT = 2;
        public byte POWER_ROOFSWITCH_PORT = 4;
        #endregion

        /// <summary>
        /// Conctructor
        /// </summary>
        public ObservatoryControls(MainForm MF)
        {
            ParentMainForm=MF;

            CommandParser = new CommandInterpretator();
            InitComandInterpretator();

            //for debug
            //SWITCH_DRIVER_NAME = "SwitchSim.Switch";
            //DOME_DRIVER_NAME = "ASCOM.Simulator.Dome";
            //TELESCOPE_DRIVER_NAME = "EQMOD_SIM.Telescope";

            MaximObj = new MaximControls(ParentMainForm);
        }

#region Programs Controlling  ///////////////////////////////////////////////////////////////////
        public string startPlanetarium()
        {
            if (Process.GetProcessesByName(CdC_ProcessName).Length ==0)
            {
                try
                {
                    CdC_Process.StartInfo.FileName = PlanetariumPath;
                    CdC_Process.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
                    CdC_Process.StartInfo.UseShellExecute = false;
                    CdC_Process.StartInfo.Arguments = "--unique";
                    CdC_Process.Start();
                    Logging.AddLog("CdC started", 0);
                    return "CdC started";
                }
                catch (Exception Ex)
                {
                    Logging.AddLog("CdC starting error! " + Ex.Message, 0, Highlight.Error);
                    return "!!!CdC start failed";
                }
            }
            return "CdC already started";
        }

        public string startMaximDL()
        {
            /*MaximDL_Process.StartInfo.FileName = MaximDLPath;
            MaximDL_Process.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
            MaximDL_Process.StartInfo.UseShellExecute = false;
            MaximDL_Process.Start();

            MaximDL_Process.WaitForInputIdle(); //WaitForProcessStartupComplete
            */
            string output=MaximObj.Init();
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
                Logging.AddLog("CCDAP started", 0);
                return "CCDAP started";

            }
            catch (Exception Ex)
            {
                Logging.AddLog("CCDAP starting error! " + Ex.Message, 0, Highlight.Error);
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
                Logging.AddLog("FocusMax started", 0);
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

                Logging.AddLog("FocusMax failed", 0, Highlight.Error);
                Logging.AddLog(FullMessage, LogLevel.Trace, Highlight.Error);
                return "!!!FocusMax start failed";

            }
        }
#endregion Program controlling

#region Roof control //////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Open roof
        /// </summary>
        /// <returns></returns>
        public bool RoofOpen()
        {
            Logging.AddLog("Trying to open roof", LogLevel.Activity);

            //Check if driver is connected
            if (Dome_connected_flag != true)
            {
                Logging.AddLog("Dome driver isn't connected", LogLevel.Critical, Highlight.Error);
                return false;
            }

            //Check if power is connected
            if (Switch_connected_flag && Roof_power_flag != true)
            {
                Logging.AddLog("Roof power switched off", LogLevel.Critical, Highlight.Error);
                return false;
            }
            RoofRoutine_StartTime=DateTime.Now;

            //open dome
            objDome.OpenShutter();
            return true;
        }

        public bool RoofClose()
        {
            Logging.AddLog("Trying to close roof", LogLevel.Activity);

            //Check if driver is connected
            if (Dome_connected_flag != true)
            {
                Logging.AddLog("Dome driver isn't connected", LogLevel.Critical, Highlight.Error);
                return false;
            }

            //Check if power is connected
            if (Switch_connected_flag && Roof_power_flag != true)
            {
                Logging.AddLog("Roof power switched off", LogLevel.Activity);
                return false;
            }

            RoofRoutine_StartTime = DateTime.Now;

            objDome.CloseShutter();
            return true;
        }

#endregion Roof control end

#region CdC controls /////////////////////////////////////////////////////
        public string CdC_connectTelescope()
        {
            string output=ParentMainForm.SocketServer.MakeClientConnectionToServer(IPAddress.Parse("127.0.0.1"), CdC_PORT, "CONNECTTELESCOPE\r\n");
            Logging.AddLog(output, 0);
            return output;
        }
#endregion CdC controls

#region Scenarios section ////////////////////////////////////////////////////////
        /// <summary>
        /// Init observatory activity 
        /// </summary>
        public void StartUpObservatory()
        {

            //1. Switch on power
            CommandParser.ParseSingleCommand("POWER_MOUNT_ON");
            CommandParser.ParseSingleCommand("POWER_CAMERA_ON");
            CommandParser.ParseSingleCommand("POWER_FOCUSER_ON");

            //2. Run MaximDL
            CommandParser.ParseSingleCommand("MAXIM_RUN");
            //ParentMainForm.AppendLogText("MaximDL started");

            //3. Run FocusMax
            CommandParser.ParseSingleCommand("FOCUSMAX_RUN");
            //ParentMainForm.AppendLogText("FocusMax started");

            //4. CameraConnect
            CommandParser.ParseSingleCommand("MAXIM_CAMERA_CONNECT");
            //ParentMainForm.AppendLogText("Camera connected");

            //5. Set camera cooler
            CommandParser.ParseSingleCommand("MAXIM_CAMERA_SETCOOLING");

            //6. Connect telescope to Maxim
            CommandParser.ParseSingleCommand("MAXIM_TELESCOPE_CONNECT");

            //7. Connect focuser in Maxim to FocusMax
            CommandParser.ParseSingleCommand("MAXIM_FOCUSER_CONNECT");

            //8. Run Cartes du Ciel
            CommandParser.ParseSingleCommand("CdC_RUN");

            //9. Connect telescope in Cartes du Ciel
            CommandParser.ParseSingleCommand("CdC_TELESCOPE_CONNECT");

            //9b. Connect telescope in Program
            CommandParser.ParseSingleCommand("OBS_TELESCOPE_CONNECT");

            //10. Start CCDAP
            CommandParser.ParseSingleCommand("CCDAP_RUN");
        }

        
        public void StartMaximDLroutines()
        {
            //1. Switch on power
            CommandParser.ParseSingleCommand("POWER_MOUNT_ON");
            CommandParser.ParseSingleCommand("POWER_CAMERA_ON");
            CommandParser.ParseSingleCommand("POWER_FOCUSER_ON");

            //2. Run MaximDL
            CommandParser.ParseSingleCommand("MAXIM_RUN");
            //ParentMainForm.AppendLogText("MaximDL started");

            //3. Run FocusMax
            CommandParser.ParseSingleCommand("FOCUSMAX_RUN");
            //ParentMainForm.AppendLogText("FocusMax started");

            //4. CameraConnect
            CommandParser.ParseSingleCommand("MAXIM_CAMERA_CONNECT");
            //ParentMainForm.AppendLogText("Camera connected");

            //5. Set camera cooler
            CommandParser.ParseSingleCommand("MAXIM_CAMERA_SETCOOLING");

            //6. Connect telescope to Maxim
            CommandParser.ParseSingleCommand("MAXIM_TELESCOPE_CONNECT");

            //7. Connect focuser in Maxim to FocusMax
            CommandParser.ParseSingleCommand("MAXIM_FOCUSER_CONNECT");
        }


#endregion Scenarios

        /// <summary>
        /// Init command interpretator
        /// </summary>
        public void InitComandInterpretator()
        {
            CommandParser.Commands.Add("MAXIM_RUN", () => this.startMaximDL());
            CommandParser.Commands.Add("FOCUSMAX_RUN", () => this.startFocusMax());
            CommandParser.Commands.Add("CdC_RUN", () => this.startPlanetarium());
            CommandParser.Commands.Add("CCDAP_RUN", () => this.startCCDAP());
            CommandParser.Commands.Add("POWER_MOUNT_ON", () => this.PowerMountOn());
            CommandParser.Commands.Add("POWER_MOUNT_OFF", () => this.PowerMountOff());

            CommandParser.Commands.Add("POWER_CAMERA_ON", () => this.PowerCameraOn());
            CommandParser.Commands.Add("POWER_CAMERA_OFF", () => this.PowerCameraOff());

            CommandParser.Commands.Add("POWER_FOCUSER_ON", () => this.PowerFocuserOn());
            CommandParser.Commands.Add("POWER_FOCUSER_OFF", () => this.PowerFocuserOff());

            CommandParser.Commands.Add("POWER_ROOF_ON", () => this.PowerRoofOn());
            CommandParser.Commands.Add("POWER_ROOF_OFF", () => this.PowerRoofOff());

            CommandParser.Commands.Add("MAXIM_CAMERA_CONNECT", () => MaximObj.ConnectCamera());
            CommandParser.Commands.Add("MAXIM_CAMERA_SETCOOLING", () => MaximObj.SetCameraCooling());
            CommandParser.Commands.Add("MAXIM_TELESCOPE_CONNECT", () => MaximObj.ConnectTelescope());
            CommandParser.Commands.Add("MAXIM_FOCUSER_CONNECT", () => MaximObj.ConnectFocuser());

            CommandParser.Commands.Add("CdC_TELESCOPE_CONNECT", () => this.CdC_connectTelescope());

            CommandParser.Commands.Add("OBS_TELESCOPE_CONNECT", () => this.OBS_connectTelescope());
        }
    
    }
}
