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

        /// Define - if equpipment is present at all
        public bool DomeEnabled = false;
        public bool SwitchEnabled = false;
        public bool TelescopeEnabled = false;

        /// ASCOM Drivers IDs
        public string SWITCH_DRIVER_NAME = "";
        public string DOME_DRIVER_NAME = "";
        public string TELESCOPE_DRIVER_NAME = "";

        /// ASCOM Drivers objects
        private ASCOM.DriverAccess.Telescope objTelescope = null;
        private ASCOM.DriverAccess.Dome objDome = null;
        private ASCOM.DriverAccess.Switch objSwitch = null;
        //public ASCOM.DriverAccess.Focuser objFocuser = null;
        //public ASCOM.DriverAccess.Camera objCamera = null;

        //Device connection flags
        //outter program layers should use them, not direct query to objDome.Connected!
        public bool Switch_connected_flag = false;
        public bool Mount_connected_flag = false;
        public bool Dome_connected_flag = false;

        private ShutterState curShutterStatus = ShutterState.shutterError;
        private PierSide curPierSideStatus = PierSide.pierUnknown;

        internal DateTime RoofRoutine_StartTime;
        internal int curRoofRoutineDuration_Seconds;

        public double GuiderFocalLen;
        public double CamPixelSizeX;
        public double CamPixelSizeY;
        public double GuidePiexelScale = 1; //value will br received from PHD2

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

            //objMaxim = new MaximControls(ParentMainForm);
        }


#region Roof control //////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Initiate roof Open
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

            try
            {
                //open dome
                objDome.OpenShutter();
                return true;
            }
            catch (Exception ex)
            {
                curShutterStatus = ShutterState.shutterError;
                Logging.AddLog("Couldn't open shutter", LogLevel.Important, Highlight.Error);
                Logging.AddLog(MethodBase.GetCurrentMethod().Name + " Error! [" + ex.ToString() + "]", LogLevel.Debug, Highlight.Error);
                return false; 
            }
        }

        /// <summary>
        /// Initiate roof closing
        /// </summary>
        /// <returns></returns>
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

            try
            {
                //open dome
                objDome.CloseShutter();
                return true;
            }
            catch (Exception ex)
            {
                curShutterStatus = ShutterState.shutterError;
                Logging.AddLog("Couldn't close shutter", LogLevel.Important, Highlight.Error);
                Logging.AddLog(MethodBase.GetCurrentMethod().Name + " Error! [" + ex.ToString() + "]", LogLevel.Debug, Highlight.Error);
                return false;
            }
        }


        /// <summary>
        /// Get shutter status
        /// </summary>
        public ShutterState DomeShutterStatus
        {
            get
            {
                //Log enter
                Logging.AddLog(MethodBase.GetCurrentMethod().Name + " enter", LogLevel.Trace);


                //if device present at all and its ID is set
                if (DomeEnabled && DOME_DRIVER_NAME != "")
                {
                    try
                    {
                        curShutterStatus = objDome.ShutterStatus;
                    }
                    catch (Exception ex)
                    {
                        curShutterStatus = ShutterState.shutterError;
                        Logging.AddLog("Couldn't get shutter state", LogLevel.Important, Highlight.Error);
                        Logging.AddLog(MethodBase.GetCurrentMethod().Name + "error! [" + ex.ToString() + "]", LogLevel.Important, Highlight.Error);
                    }
                }
                else
                {
                    curShutterStatus = ShutterState.shutterError;
                    //Print if somebody try to connect if device isn't presetn. Mostly for debug
                    Logging.AddLog("Domoe is not set. Couldn't return status of shutter", LogLevel.Debug, Highlight.Error);
                }

                Logging.AddLog(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + curShutterStatus, LogLevel.Trace);
                return curShutterStatus;
            }
        }



 

        #endregion Roof control end


        public double CalcRecommendedCoolerTemp()
        {
            return -20.0;
        }


    }

}
