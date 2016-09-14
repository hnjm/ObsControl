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
                Logging.AddLog("Dome driver isn't connected", LogLevel.Important, Highlight.Error);
                return false;
            }

            //Check if power is connected
            if (Switch_connected_flag && Roof_power_flag != true)
            {
                Logging.AddLog("Roof power switched off", LogLevel.Important, Highlight.Error);
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
                Logging.AddLog("Dome driver isn't connected", LogLevel.Important, Highlight.Error);
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

#region Scenarios section ////////////////////////////////////////////////////////
        /// <summary>
        /// Init observatory activity 
        /// </summary>
        public void StartUpObservatory()
        {

            //1. Switch on power
            Logging.AddLog("StartUp run: Switching power on", LogLevel.Debug);
            CommandParser.ParseSingleCommand("POWER_MOUNT_ON");
            CommandParser.ParseSingleCommand("POWER_CAMERA_ON");
            CommandParser.ParseSingleCommand("POWER_FOCUSER_ON");

            //2.1 Run PHD2
            Logging.AddLog("StartUp run: Start PHD2", LogLevel.Debug);
            CommandParser.ParseSingleCommand("PHD2_RUN");

            //2.2 Connect equipment
            Logging.AddLog("StartUp run: connect equipeun in PHD2", LogLevel.Debug);
            CommandParser.ParseSingleCommand("PHD2_CONNECT");

            //2.3 Rub broker app
            Logging.AddLog("StartUp run: run PHD Broker", LogLevel.Debug);
            CommandParser.ParseSingleCommand("PHDBROKER_RUN");

            //3. Run MaximDL
            Logging.AddLog("StartUp run: Start Maxim DL", LogLevel.Debug);
            CommandParser.ParseSingleCommand("MAXIM_RUN");
            //ParentMainForm.AppendLogText("MaximDL started");

            //3.1. CameraConnect
            Logging.AddLog("StartUp run: Maxim Camera connect", LogLevel.Debug);
            CommandParser.ParseSingleCommand("MAXIM_CAMERA_CONNECT");
            //ParentMainForm.AppendLogText("Camera connected");

            //3.2. Set camera cooler
            CommandParser.ParseSingleCommand("MAXIM_CAMERA_SETCOOLING");

            //3.3. Connect telescope to Maxim
            CommandParser.ParseSingleCommand("MAXIM_TELESCOPE_CONNECT");

            //4. Run FocusMax
            Logging.AddLog("StartUp run: Start Focus Max", LogLevel.Debug);
            CommandParser.ParseSingleCommand("FOCUSMAX_RUN");
            //ParentMainForm.AppendLogText("FocusMax started");

            //5. Connect focuser in Maxim to FocusMax
            CommandParser.ParseSingleCommand("MAXIM_FOCUSER_CONNECT");

            //6. Run Cartes du Ciel
            CommandParser.ParseSingleCommand("CdC_RUN");

            //6.1. Connect telescope in Cartes du Ciel
            CommandParser.ParseSingleCommand("CdC_TELESCOPE_CONNECT");

            //7. Connect telescope in Program
            CommandParser.ParseSingleCommand("OBS_TELESCOPE_CONNECT");

            //8. Start CCDAP
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
            CommandParser.Commands.Add("PHD2_RUN", () => this.startPHD2());
            CommandParser.Commands.Add("PHDBROKER_RUN", () => this.startPHDBroker());
            
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

            CommandParser.Commands.Add("CdC_TELESCOPE_CONNECT", () => this.objCdCApp.ConnectTelescope());

            CommandParser.Commands.Add("PHD2_CONNECT", () => this.objPHD2App.ConnectEquipment());
            
            CommandParser.Commands.Add("OBS_TELESCOPE_CONNECT", () => this.OBS_connectTelescope());
        }

        public double CalcRecommendedCoolerTemp()
        {
            return -20.0;
        }

    }

}
