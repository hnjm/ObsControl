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
    public class ObservatoryControls_ASCOMDome
    {

        public string DOME_DRIVER_NAME = "";
        public bool DomeEnabled = false;
        public bool Dome_connected_flag = false;

        
        /// ASCOM Drivers objects
        public ASCOM.DriverAccess.Dome objDome = null;

        internal ShutterState curShutterStatus = ShutterState.shutterError;

        internal DateTime RoofRoutine_StartTime;
        internal int curRoofRoutineDuration_Seconds;
        internal ObservatoryControls_ASCOMSwitch ExtASCOMSiwitchObj;

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
        public bool connectDome
        {
            set
            {
                //Log enter
                Logging.AddLog(MethodBase.GetCurrentMethod().Name + (value ? "ON" : "OFF"), LogLevel.Trace);

                //if device present at all and its ID is set
                if (DomeEnabled && DOME_DRIVER_NAME != "")
                {
                    try
                    {
                        //If obj doesnot exist - create
                        if (objDome == null) objDome = new ASCOM.DriverAccess.Dome(DOME_DRIVER_NAME);

                        //Connect
                        objDome.Connected = value;
                        Dome_connected_flag = value;
                        Logging.AddLog("Dome has been " + (value ? "connected" : "disconnected"), LogLevel.Activity);
                    }
                    catch (Exception ex)
                    {
                        Dome_connected_flag = false;
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
                if (DomeEnabled && DOME_DRIVER_NAME != "")
                {
                    try
                    {
                        Dome_connected_flag = objDome.Connected;
                    }
                    catch (Exception ex)
                    {
                        Dome_connected_flag = false;
                        Logging.AddLog(MethodBase.GetCurrentMethod().Name + "error! [" + ex.ToString() + "]", LogLevel.Important, Highlight.Error);
                    }
                }
                else
                {
                    //Print if somebody try to connect if device isn't presetn. Mostly for debug
                    Logging.AddLog("Device is not set. Couldn't return status of dome", LogLevel.Debug, Highlight.Error);
                }

                Logging.AddLog(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + Dome_connected_flag, LogLevel.Trace);
                return Dome_connected_flag;
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
            if (Dome_connected_flag != true)
            {
                Logging.AddLog("Dome driver isn't connected", LogLevel.Important, Highlight.Error);
                return false;
            }

            //Check if power is connected. Only if we have link to external ASCOM Switch object
            if (ExtASCOMSiwitchObj != null)
            {
                if (ExtASCOMSiwitchObj.Connected_flag && ExtASCOMSiwitchObj.Roof_power_flag != true)
                {
                    Logging.AddLog("Roof power switched off", LogLevel.Important, Highlight.Error);
                    return false;
                }
            }
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
            if (Dome_connected_flag != true)
            {
                Logging.AddLog("Dome driver isn't connected", LogLevel.Important, Highlight.Error);
                return false;
            }

            //Check if power is connected
            if (ExtASCOMSiwitchObj != null)
            {
                if (ExtASCOMSiwitchObj.Connected_flag && ExtASCOMSiwitchObj.Roof_power_flag != true)
                {
                    Logging.AddLog("Roof power switched off", LogLevel.Activity);
                    return false;
                }
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
                    Logging.AddLog("Dome is not set. Couldn't return status of shutter", LogLevel.Debug, Highlight.Error);
                }

                Logging.AddLog(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + curShutterStatus, LogLevel.Trace);
                return curShutterStatus;
            }
        }

        #endregion Roof control end




        /// <summary>
        /// Wrapper to reset dome driver 
        /// Later system would reinitiate it itself
        /// </summary>
        public void resetDome()
        {
            Dome_connected_flag = false;
            curShutterStatus = ShutterState.shutterError;
            objDome = null;
        }





    }
}
