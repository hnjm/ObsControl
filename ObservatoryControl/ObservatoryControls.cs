using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Net;
using System.Threading;

using ASCOM;
using ASCOM.DeviceInterface;
using ASCOM.Utilities;

using MaxIm;

namespace ObservatoryCenter
{
    public class ObservatoryControls
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


        /// <summary>
        /// Command dictionary for interpretator
        /// </summary>
        Dictionary<string, Action> Commands;
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

            InitComandInterpretator();
            CommandParser = new CommandInterpretator(Commands);

            //for debug
            SWITCH_DRIVER_NAME = "SwitchSim.Switch";
            DOME_DRIVER_NAME = "ASCOM.Simulator.Dome";
            TELESCOPE_DRIVER_NAME = "EQMOD_SIM.Telescope";

            MaximObj = new MaximControls(ParentMainForm);
        }

#region Programs Controlling  ///////////////////////////////////////////////////////////////////
        public void startPlanetarium()
        {
            if (Process.GetProcessesByName(CdC_ProcessName).Length ==0)
            {
                CdC_Process.StartInfo.FileName = PlanetariumPath;
                CdC_Process.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
                CdC_Process.StartInfo.UseShellExecute = false;
                CdC_Process.StartInfo.Arguments = "--unique";
                CdC_Process.Start();
                Logging.AddLog("CdC started", 0);

            }
        }

        public void startMaximDL()
        {
            /*MaximDL_Process.StartInfo.FileName = MaximDLPath;
            MaximDL_Process.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
            MaximDL_Process.StartInfo.UseShellExecute = false;
            MaximDL_Process.Start();

            MaximDL_Process.WaitForInputIdle(); //WaitForProcessStartupComplete
            */
            MaximObj.Init();
        }

        public void startCCDAP()
        {
            try
            {
                CCDAP_Process.StartInfo.FileName = CCDAPPath;
                CCDAP_Process.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
                CCDAP_Process.StartInfo.UseShellExecute = false;
                CCDAP_Process.Start();
                Logging.AddLog("CCDAP started", 0);

            }
            catch (Exception Ex)
            {
                Logging.AddLog("CCDAP starting error! " + Ex.Message, 0, Highlight.Error);
            }
        }

        public void startFocusMax()
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

                string FullMessage = "MaximDL set camera cooling failed!" + Environment.NewLine;
                FullMessage += Environment.NewLine + Environment.NewLine + "Debug information:" + Environment.NewLine + "IOException source: " + ex.Data + " " + ex.Message
                        + Environment.NewLine + Environment.NewLine + messstr;
                //MessageBox.Show(this, FullMessage, "Invalid value", MessageBoxButtons.OK);

                Logging.AddLog("FocusMax failed", 0, Highlight.Error);
                Logging.AddLog(FullMessage, 2, Highlight.Error);

            }
        }
#endregion Program controlling

#region ASCOM Device Drivers controlling  ///////////////////////////////////////////////////////////////////

        public bool connectSwitch
        {
            set
            {
                if (objSwitch == null) objSwitch = new ASCOM.DriverAccess.Switch(SWITCH_DRIVER_NAME);
                objSwitch.Connected = true;
                Logging.AddLog(System.Reflection.MethodBase.GetCurrentMethod().Name + (value ? "ON" : "OFF"), 2);
            }
            get
            {
                bool ret = objSwitch.Connected;
                Logging.AddLog(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + ret, 2);
                return ret;
            }
        }

        public bool connectTelescope
        {
            set
            {
                if (objTelescope == null) objTelescope = new ASCOM.DriverAccess.Telescope(TELESCOPE_DRIVER_NAME);
                objTelescope.Connected = value;
                Logging.AddLog(System.Reflection.MethodBase.GetCurrentMethod().Name + (value ? "ON" : "OFF"), 2);
                if (!value)
                {
                    objTelescope.Dispose();
                    objTelescope = null;
                }
            }
            get
            {
                bool ret=objTelescope.Connected;
                Logging.AddLog(System.Reflection.MethodBase.GetCurrentMethod().Name + ": "+ret, 2);
                return ret;
            }
        }

        public bool connectDome
        {
            set
            {
                if (objDome == null) objDome= new ASCOM.DriverAccess.Dome(DOME_DRIVER_NAME);
                objDome.Connected = true;
                Logging.AddLog(System.Reflection.MethodBase.GetCurrentMethod().Name + (value ? "ON" : "OFF"), 2);
            }
            get
            {
                bool ret=objDome.Connected;
                Logging.AddLog(System.Reflection.MethodBase.GetCurrentMethod().Name + ": "+ret, 2);
                return ret;
            }
        }

        public bool startCheckSwitch()
        {
            //check if can be connected
            if (!objSwitch.Connected)
            {
                System.Windows.Forms.MessageBox.Show("Cann't connect to switch. Check your settings");
                return false;
            }
            return true;
        }
#endregion ASCOM Device Drivers controlling

#region Power controlling ////////////////////////////////////////////////////////////////////////////////////////////

        public bool MountPower
        {
            get
            {
                Logging.AddLog("Mount power get", 2);
                return objSwitch.GetSwitch(POWER_MOUNT_PORT);
            }
            set{
                Logging.AddLog("Mount power switching "+(value?"ON":"OFF"),2);
                objSwitch.SetSwitch(POWER_MOUNT_PORT, value);
            }
        }

        public bool CameraPower
        {
            get{
                Logging.AddLog("Camera power get", 3);
                return objSwitch.GetSwitch(POWER_CAMERA_PORT);
            }
            set{
                Logging.AddLog("Camera power switching " + (value ? "ON" : "OFF"), 3);
                objSwitch.SetSwitch(POWER_CAMERA_PORT, value);
            }
        }

        public bool FocusPower
        {
            get{
                Logging.AddLog("Focus power get", 3);
                return objSwitch.GetSwitch(POWER_FOCUSER_PORT);
            }
            set{
                Logging.AddLog("Focus power switching " + (value ? "ON" : "OFF"), 3);
                objSwitch.SetSwitch(POWER_FOCUSER_PORT, value);
            }
        }
        
        public bool RoofPower
        {
            get{
                Logging.AddLog("Roof power get", 3);
                return objSwitch.GetSwitch(POWER_ROOFPOWER_PORT);
            }
            set{
                Logging.AddLog("Roof power switching " + (value ? "ON" : "OFF"), 3);
                objSwitch.SetSwitch(POWER_ROOFPOWER_PORT, value);
            }
        }


        public void PowerMountOn()
        {
            MountPower = true;
        }
        public void PowerMountOff()
        {
            MountPower = false;
        }

        public void PowerCameraOn()
        {
            CameraPower=true;
        }
        public void PowerCameraOff()
        {
            CameraPower=false;
        }

        public void PowerFocuserOn()
        {
            FocusPower=true;
        }
        public void PowerFocuserOff()
        {
            FocusPower=false;
        }
            
        public void PowerRoofOn()
        {
            RoofPower = true;
        }
        public void PowerRoofOff()
        {
            RoofPower = false;
        }

            

#endregion Power controlling

#region Roof control //////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Open roof
        /// </summary>
        /// <returns></returns>
        public bool RoofOpen()
        {
            Logging.AddLog("Trying to open roof", 1);

            //Check if power is connected
            if (!objSwitch.GetSwitch(POWER_ROOFPOWER_PORT))
            {
                Logging.AddLog("Roof power switched off", 1);
                return false;
            }

            RoofRoutine_StartTime=DateTime.Now;

            //open dome
            objDome.OpenShutter();
            return true;
        }

        public bool RoofClose()
        {
            Logging.AddLog("Trying to close roof", 1);

            //Check if power is connected
            if (!objSwitch.GetSwitch(POWER_ROOFPOWER_PORT))
            {
                Logging.AddLog("Roof power switched off", 1);
                return false;
            }

            RoofRoutine_StartTime = DateTime.Now;

            objDome.CloseShutter();
            return true;
        }

#endregion Roof control end

#region CdC controls /////////////////////////////////////////////////////
        public void CdC_connectTelescope()
        {
            ParentMainForm.SocketServer.MakeClientConnectionToServer(IPAddress.Parse("127.0.0.1"), CdC_PORT, "CONNECTTELESCOPE");
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
            ParentMainForm.AppendLogText("MaximDL started");

            //3. Run FocusMax
            CommandParser.ParseSingleCommand("FOCUSMAX_RUN");
            ParentMainForm.AppendLogText("FocusMax started");

            //4. CameraConnect
            CommandParser.ParseSingleCommand("MAXIM_CAMERA_CONNECT");
            ParentMainForm.AppendLogText("Camera connected");

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

            //10. Start CCDAP
            CommandParser.ParseSingleCommand("CCDAP_RUN");
        
        }


#endregion Scenarios

        /// <summary>
        /// Init command interpretator
        /// </summary>
        public void InitComandInterpretator()
        {
            Commands = new Dictionary<string, Action>();
            Commands.Add("MAXIM_RUN", () => this.startMaximDL());
            Commands.Add("FOCUSMAX_RUN", () => this.startFocusMax());
            Commands.Add("CdC_RUN", () => this.startPlanetarium());
            Commands.Add("CCDAP_RUN", () => this.startCCDAP());

            Commands.Add("POWER_MOUNT_ON", () => this.PowerMountOn());
            Commands.Add("POWER_MOUNT_OFF", () => this.PowerMountOff());

            Commands.Add("POWER_CAMERA_ON", () => this.PowerCameraOn());
            Commands.Add("POWER_CAMERA_OFF", () => this.PowerCameraOff());

            Commands.Add("POWER_FOCUSER_ON", () => this.PowerFocuserOn());
            Commands.Add("POWER_FOCUSER_OFF", () => this.PowerFocuserOff());

            Commands.Add("POWER_ROOF_ON", () => this.PowerRoofOn());
            Commands.Add("POWER_ROOF_OFF", () => this.PowerRoofOff());

            Commands.Add("MAXIM_CAMERA_CONNECT", () => MaximObj.ConnectCamera());
            Commands.Add("MAXIM_CAMERA_SETCOOLING", () => MaximObj.SetCameraCooling());
            Commands.Add("MAXIM_TELESCOPE_CONNECT", () => MaximObj.ConnectTelescope());
            Commands.Add("MAXIM_FOCUSER_CONNECT", () => MaximObj.ConnectFocuser());

            Commands.Add("CdC_TELESCOPE_CONNECT", () => this.CdC_connectTelescope());
        }
    
    }
}
