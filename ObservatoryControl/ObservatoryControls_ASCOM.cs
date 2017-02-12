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
        /// SET: Connect/disconnect to telescope Wrapper
        /// GET: Connection status Wrapper
        /// </summary>
        public bool connectMount
        {
            set
            {
                //Log enter
                Logging.AddLog(MethodBase.GetCurrentMethod().Name + (value ? "ON" : "OFF"), LogLevel.Trace);

                //if device present at all and its ID is set
                if (TelescopeEnabled && TELESCOPE_DRIVER_NAME!="")
                { 
                    try
                    {
                        //If obj doesnot exist - create
                        if (objTelescope == null) objTelescope = new ASCOM.DriverAccess.Telescope(TELESCOPE_DRIVER_NAME);

                        //Connect/Disconnect
                        objTelescope.Connected = value;
                        Mount_connected_flag = value;
                        Logging.AddLog("Telescope has been " + (value ? "connected" : "disconnected"), LogLevel.Activity);
                    }
                    catch (Exception ex)
                    {
                        Mount_connected_flag = false;
                        Logging.AddLog("Couldn't " + (value ? "connect to" : "disconnect ") + " telescope", LogLevel.Important, Highlight.Error);
                        Logging.AddLog(MethodBase.GetCurrentMethod().Name + " " + (value ? "ON" : "OFF") + " Error! [" + ex.ToString() + "]", LogLevel.Debug, Highlight.Error);
                    }

                    //free object if disconnect
                    if (!value)
                    {
                        //objTelescope.Dispose();
                        //objTelescope = null;
                    }
                }
                else
                {
                    //Print if somebody try to connect if device isn't presetn. Mostly for debug
                    Logging.AddLog("Device is not set. Couldn't " + (value ? "connect to" : "disconnect ") + " telescope", LogLevel.Debug, Highlight.Error);
                }
            }
            get
            {
                //Log enter
                Logging.AddLog(MethodBase.GetCurrentMethod().Name + " enter", LogLevel.Trace);

                //if device present at all and its ID is set
                if (TelescopeEnabled && TELESCOPE_DRIVER_NAME != "")
                {
                    try
                    {
                        Mount_connected_flag = objTelescope.Connected;
                    }
                    catch (Exception ex)
                    {
                        Mount_connected_flag = false;
                        Logging.AddLog("Telescope get connection error! [" + ex.ToString() + "]", LogLevel.Important, Highlight.Error);
                    }
                }
                else
                {
                    //Print if somebody try to connect if device isn't presetn. Mostly for debug
                    Logging.AddLog("Device is not set. Couldn't return status of telescope", LogLevel.Debug, Highlight.Error);
                }

                Logging.AddLog(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + Mount_connected_flag, LogLevel.Trace);
                return Mount_connected_flag;
            }
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

        public string OBS_connectTelescope()
        {
            Logging.AddLog(System.Reflection.MethodBase.GetCurrentMethod().Name + " enter", LogLevel.Trace);
            connectMount = true;
            return "Telescope in ObsContrtol connected";
        }





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


        /// <summary>
        /// Wrapper to reset telescope driver 
        /// Later system would reinitiate it itself
        /// </summary>
        public void resetTelescope()
        {
            Mount_connected_flag = false;

            curAzimuth = -1;
            curAltitude = -100;
            curRightAscension = -100;
            curDeclination = -100;
            curSiderealTime = -100;

            curAtPark = false;
            curTracking = false;

            objTelescope = null;
        }

        /// <summary>
        /// Wrapper to reset telescope driver 
        /// Later system would reinitiate it itself
        /// </summary>
        public void resetSwitch()
        {
            Switch_connected_flag = false;

            Mount_power_flag = null;
            Camera_power_flag = null;
            Focuser_power_flag = null;
            Roof_power_flag = null;

            objSwitch = null;
        }
    }
}
