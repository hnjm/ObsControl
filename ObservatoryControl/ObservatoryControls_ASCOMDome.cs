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

using LoggingLib;


namespace ObservatoryCenter
{
    public class ObservatoryControls_ASCOMDome
    {
        public bool Enabled = false;
        public bool Connected_flag = false;

        public string DRIVER_NAME = "";

        public ShutterState curShutterStatus = ShutterState.shutterError;
        public DateTime RoofRoutine_StartTime;
        public int curRoofRoutineDuration_Seconds;

        //ASCOM Object
        private ASCOM.DriverAccess.Dome objDome = null;

        //Optional external reference to switch object to check if roof power switched on
        private ObservatoryControls_ASCOMSwitch ExtASCOMSiwitchObj = null;

        // Threads
        private Thread CheckDomeStatusThread;
        private ThreadStart CheckDomeStatusThread_startref;

        /// <summary>
        /// Constructor 1 variant. Without any reference to external SWITCH OBJECT
        /// </summary>
        public ObservatoryControls_ASCOMDome()
        {
        }
        /// <summary>
        /// Constructor 1 variant. Without any reference to external SWITCH OBJECT
        /// </summary>
        public ObservatoryControls_ASCOMDome(ObservatoryControls_ASCOMSwitch ExtASCOMSiwitch)
        {
            ExtASCOMSiwitchObj = ExtASCOMSiwitch;
        }

        /// <summary>
        /// SET: Dome connect/disconnect wrapper
        /// GET: Dome connection status
        /// </summary>
        public bool Connect
        {
            set
            {
                //Log enter
                Logging.AddLog(MethodBase.GetCurrentMethod().Name + (value ? "ON" : "OFF"), LogLevel.Trace);

                //if device present at all and its ID is set
                if (Enabled && DRIVER_NAME != "")
                {
                    try
                    {
                        //If obj doesnot exist - create
                        if (objDome == null) objDome = new ASCOM.DriverAccess.Dome(DRIVER_NAME);

                        //Connect
                        objDome.Connected = value;
                        Connected_flag = value;
                        Logging.AddLog("Dome has been " + (value ? "connected" : "disconnected"), LogLevel.Activity);
                    }
                    catch (Exception ex)
                    {
                        Connected_flag = false;
                        Logging.AddLog("Couldn't " + (value ? "connect to" : "disconnect from") + " dome", LogLevel.Important, Highlight.Error);
                        Logging.AddLog(MethodBase.GetCurrentMethod().Name + " " + (value ? "ON" : "OFF") + " Error! [" + ex.ToString() + "]", LogLevel.Debug, Highlight.Error);
                    }

                    //free object
                    if (!value)
                    {
                        //objDome.Dispose();
                        //objDome = null;
                    }
                }
                else
                {
                    //Print if somebody try to connect if device isn't presetn. Mostly for debug
                    Logging.AddLog("Device is not set. Couldn't " + (value ? "connect to" : "disconnect ") + " dome", LogLevel.Debug, Highlight.Error);
                }
            }
            get
            {
                //Log enter
                Logging.AddLog(MethodBase.GetCurrentMethod().Name + " enter", LogLevel.Trace);
                //if device present at all and its ID is set
                if (Enabled && DRIVER_NAME != "")
                {
                    try
                    {
                        Connected_flag = objDome.Connected;
                    }
                    catch (Exception ex)
                    {
                        Connected_flag = false;
                        Logging.AddLog(MethodBase.GetCurrentMethod().Name + "error! [" + ex.ToString() + "]", LogLevel.Important, Highlight.Error);
                    }
                }
                else
                {
                    //Print if somebody try to connect if device isn't presetn. Mostly for debug
                    Logging.AddLog("Device is not set. Couldn't return status of dome", LogLevel.Debug, Highlight.Error);
                }

                Logging.AddLog(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + Connected_flag, LogLevel.Trace);
                return Connected_flag;
            }
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
            if (Connected_flag != true)
            {
                Logging.AddLog("Dome driver isn't connected", LogLevel.Important, Highlight.Error);
                return false;
            }

            ////Check if power is connected. Only if we have link to external ASCOM Switch object
            //if (ExtASCOMSiwitchObj != null)
            //{
            //    if (ExtASCOMSiwitchObj.Connected_flag && ExtASCOMSiwitchObj._Roof_power_flag != true)
            //    {
            //        Logging.AddLog("Roof power switched off", LogLevel.Important, Highlight.Error);
            //        return false;
            //    }
            //}
            RoofRoutine_StartTime = DateTime.Now;

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
            if (Connected_flag != true)
            {
                Logging.AddLog("Dome driver isn't connected", LogLevel.Important, Highlight.Error);
                return false;
            }

            //Check if power is connected
            //if (ExtASCOMSiwitchObj != null)
            //{
            //    if (ExtASCOMSiwitchObj.Connected_flag && ExtASCOMSiwitchObj._Roof_power_flag != true)
            //    {
            //        Logging.AddLog("Roof power switched off", LogLevel.Activity);
            //        return false;
            //    }
            //}
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
        private ShutterState getDomeShutterStatus()
        {
            //Log enter
            Logging.AddLog(MethodBase.GetCurrentMethod().Name + " enter", LogLevel.Trace);


            //if device present at all and its ID is set
            if (Enabled && DRIVER_NAME != "" && this.Connect == true)
            {
                try
                {
                    curShutterStatus = objDome.ShutterStatus;
                }
                catch (Exception ex)
                {
                    curShutterStatus = ShutterState.shutterError;
                    Connected_flag = false;
                    Logging.AddLog("Couldn't get shutter state", LogLevel.Important, Highlight.Error);
                    Logging.AddLog(MethodBase.GetCurrentMethod().Name + "error! [" + ex.ToString() + "]", LogLevel.Important, Highlight.Error);
                }
            }
            else
            {
                curShutterStatus = ShutterState.shutterError;
                Connected_flag = false;
                //Print if somebody try to connect if device isn't presetn. Mostly for debug
                Logging.AddLog("Dome is not set. Couldn't return status of shutter", LogLevel.Debug, Highlight.Error);
            }

            Logging.AddLog(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + curShutterStatus, LogLevel.Trace);
            return curShutterStatus;
        }

        /// <summary>
        /// Dummy void method wrapper, because threadstart needs only void
        /// </summary>
        public void CheckDomeShutterStatus()
        {
            this.getDomeShutterStatus();
        }

        /// <summary>
        /// Checking device status in separate thread
        /// </summary>
        public void CheckDomeShutterStatus_async()
        {
            if (Connected_flag)
            {
                try
                {
                    if (CheckDomeStatusThread == null || !CheckDomeStatusThread.IsAlive)
                    {
                        CheckDomeStatusThread_startref = new ThreadStart(CheckDomeShutterStatus);
                        CheckDomeStatusThread = new Thread(CheckDomeStatusThread_startref);
                        CheckDomeStatusThread.Start();
                    }
                }
                catch (Exception ex)
                {
                    Logging.AddLog("Exception in CheckPowerDeviceStatus_async [" + ex.ToString() + "]", LogLevel.Important, Highlight.Error);
                }
            }
        }



        #endregion Roof control end




        /// <summary>
        /// Wrapper to reset dome driver 
        /// Later system would reinitiate it itself
        /// </summary>
        public void Reset()
        {
            Connected_flag = false;
            curShutterStatus = ShutterState.shutterError;
            objDome = null;
        }
    }
}
