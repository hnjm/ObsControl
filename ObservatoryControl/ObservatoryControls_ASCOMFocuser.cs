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
    public class ObservatoryControls_ASCOMFocuser
    {
        public bool Enabled = false;
        public bool Connected_flag = false;

        public string DRIVER_NAME = "";

        //Main properties
        public int FocuserPos = int.MaxValue;
        public double FocuserTemp = Maxim_ExternalApplication.TEMP_MIN;

        /// <summary>
        /// ASCOM Object
        /// </summary>
        private ASCOM.DriverAccess.Focuser objFcouser = null;
        
        // Threads
        private Thread CheckStatusThread;
        private ThreadStart CheckStatusThread_startref;


        /// <summary>
        /// Constructor
        /// </summary>
        public ObservatoryControls_ASCOMFocuser()
        {
        }



        #region ASCOM Device Drivers controlling (connect/disconnect/status)  ///////////////////////////////////////////////////////////////////

        /// <summary>
        /// Connect to switch / Get connection status
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
                        if (objFcouser == null) objFcouser = new ASCOM.DriverAccess.Focuser(DRIVER_NAME);

                        //Connect
                        objFcouser.Connected = value;
                        Connected_flag = value;
                        Logging.AddLog("Focuser has been " + (value ? "connected" : "disconnected"), LogLevel.Activity);
                    }
                    catch (Exception ex)
                    {
                        Connected_flag = false;
                        Logging.AddLog("Couldn't " + (value ? "connect to" : "disconnect from") + " focuser", LogLevel.Important, Highlight.Error);
                        Logging.AddLog(MethodBase.GetCurrentMethod().Name + " " + (value ? "ON" : "OFF") + " Error! [" + ex.ToString() + "]", LogLevel.Debug, Highlight.Error);
                    }

                    //free object
                    if (!value)
                    {
                        //objSwitch.Dispose();
                        //objSwitch = null;
                    }
                }
                else
                {
                    //Print if somebody try to connect if device isn't presetn. Mostly for debug
                    Logging.AddLog("Device is not set. Couldn't " + (value ? "connect to" : "disconnect ") + " focuser", LogLevel.Debug, Highlight.Error);
                }
            }
            private get
            {
                //Log enter
                Logging.AddLog(MethodBase.GetCurrentMethod().Name +" enter", LogLevel.Trace);
                //if device present at all and its ID is set
                if (Enabled && DRIVER_NAME != "")
                {

                    try
                    {
                        Connected_flag  = objFcouser .Connected;
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
                    Logging.AddLog("Device is not set. Couldn't return status of focuser", LogLevel.Debug, Highlight.Error);
                }

                Logging.AddLog(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + Connected_flag, LogLevel.Trace);
                return Connected_flag;
            }
        }


        #endregion ASCOM Device Drivers controlling



        #region Check Status ////////////////////////////////////////////////////////////////////////////////////////////
        private void CheckDeviceStatus()
        {
            try
            {
                Connected_flag = this.Connect;
                FocuserPos = objFcouser.Position;
                FocuserTemp = objFcouser.Temperature;
            }
            catch (Exception ex)
            {
                Logging.AddLog("Exception in ChechDeviceStatus ["+ex.ToString()+"]",LogLevel.Important,Highlight.Error);
            }
        }

        /// <summary>
        /// Checking device status in separate thread
        /// </summary>
        public void CheckDeviceStatus_async()
        {
            if (Connected_flag)
            {
                try
                {
                    if (CheckStatusThread == null || !CheckStatusThread.IsAlive)
                    {
                        CheckStatusThread_startref = new ThreadStart(CheckDeviceStatus);
                        CheckStatusThread = new Thread(CheckStatusThread_startref);
                        CheckStatusThread.Start();
                    }
                }
                catch (Exception ex)
                {
                    Logging.AddLog("Exception in CheckDeviceStatus_async [" + ex.ToString() + "]", LogLevel.Important, Highlight.Error);
                }
            }
        }

        #endregion Check status



        /// <summary>
        /// Wrapper to reset telescope driver 
        /// Later system would reinitiate it itself
        /// </summary>
        public void Reset()
        {
            Connected_flag = false;

            objFcouser = null;
        }
    }
}
