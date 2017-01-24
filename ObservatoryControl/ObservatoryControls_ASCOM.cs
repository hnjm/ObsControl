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

                try
                {
                    //If obj doesnot exist - create
                    if (objTelescope == null) objTelescope = new ASCOM.DriverAccess.Telescope(TELESCOPE_DRIVER_NAME);

                    //Connect
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

                //free object
                if (!value)
                {
                    //objTelescope.Dispose();
                    //objTelescope = null;
                }
            }
            get
            {
                //Log enter
                Logging.AddLog(MethodBase.GetCurrentMethod().Name + " enter", LogLevel.Trace);

                try
                {
                    Mount_connected_flag = objTelescope.Connected;
                }
                catch (Exception ex)
                {
                    Mount_connected_flag = false;
                    Logging.AddLog("Telescope get connection error! [" + ex.ToString() + "]", LogLevel.Important, Highlight.Error);
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
                    Logging.AddLog("Couldn't " + (value ? "connect to" : "disconnect from") + " switch", LogLevel.Important, Highlight.Error);
                    Logging.AddLog(MethodBase.GetCurrentMethod().Name + " " + (value ? "ON" : "OFF") + " Error! [" + ex.ToString() + "]", LogLevel.Debug, Highlight.Error);
                }

                //free object
                if (!value)
                {
                    //objDome.Dispose();
                    //objDome = null;
                }
            }
            get
            {
                //Log enter
                Logging.AddLog(MethodBase.GetCurrentMethod().Name + " enter", LogLevel.Trace);

                try
                {
                    Dome_connected_flag = objDome.Connected;
                }
                catch (Exception ex)
                {
                    Dome_connected_flag = false;
                    Logging.AddLog(MethodBase.GetCurrentMethod().Name + "error! [" + ex.ToString() + "]", LogLevel.Important, Highlight.Error);
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

    }
}
