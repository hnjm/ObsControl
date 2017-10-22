using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ASCOM;
using ASCOM.DeviceInterface;
using ASCOM.Utilities;
using System.Reflection;
using System.Threading;

namespace ObservatoryCenter
{
    public class ObservatoryControls_ASCOMTelescope
    {

        public bool Enabled = false;
        public bool Connected_flag = false;
        public string DRIVER_NAME = "";

        private ASCOM.DriverAccess.Telescope objTelescope = null;

        public PierSide curPierSideStatus = PierSide.pierUnknown;

        public double curAzimuth = -1;
        public double curAltitude = -100;
        public double curRightAscension = -100;
        public double curDeclination = -100;
        public double curSiderealTime = -100;

        public bool curAtPark = false;
        public bool curTracking = false;


        // Threads
        private Thread CheckTelescopeStatusThread;
        private ThreadStart CheckTelescopeStatusThread_startref;


        /// <summary>
        /// Constructor
        /// </summary>
        public ObservatoryControls_ASCOMTelescope()
        {
        }



        private void CheckTelescopeStatus()
        {
            //if device present at all and its ID is set
            if (Enabled && DRIVER_NAME != "" && objTelescope != null)
            {
                try
                {
                    Connected_flag = this.Connect;
                    curAzimuth = this.Azimuth;
                    curAltitude = this.Altitude;
                    curRightAscension = this.RightAscension;
                    curDeclination = this.Declination;
                    curSiderealTime = this.SiderealTime;

                    curPierSideStatus = this.PierSideStatus;

                    curAtPark = this.AtPark;
                    curTracking = this.Tracking;

                }
                catch (Exception ex)
                {
                    Logging.AddLog("CheckTelescopeStatus error [" + ex.ToString() + "]", LogLevel.Important, Highlight.Error);
                }
            }
            else
            {
                //Print if somebody try to connect if device isn't presetn. Mostly for debug
                //Logging.AddLog("Telescope is not set. Couldn't set Park status", LogLevel.Debug, Highlight.Error);
            }
        }

        public void CheckTelescopeStatus_async()
        {
            if (Connected_flag)
            {
                try
                {
                    if (CheckTelescopeStatusThread == null || !CheckTelescopeStatusThread.IsAlive)
                    {
                        CheckTelescopeStatusThread_startref = new ThreadStart(CheckTelescopeStatus);
                        CheckTelescopeStatusThread = new Thread(CheckTelescopeStatusThread_startref);
                        CheckTelescopeStatusThread.Start();
                    }
                }
                catch (Exception ex)
                {
                    Logging.AddLog("Exception in CheckTelescopeStatus_async [" + ex.ToString() + "]", LogLevel.Important, Highlight.Error);
                }
            }
        }


        /// <summary>
        /// SET: Connect/disconnect to telescope Wrapper
        /// GET: Connection status Wrapper
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
                        if (objTelescope == null) objTelescope = new ASCOM.DriverAccess.Telescope(DRIVER_NAME);

                        //Connect/Disconnect
                        objTelescope.Connected = value;
                        Connected_flag = value;
                        Logging.AddLog("Telescope has been " + (value ? "connected" : "disconnected"), LogLevel.Activity);
                    }
                    catch (Exception ex)
                    {
                        Connected_flag = false;
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
            private get
            {
                //Log enter
                Logging.AddLog(MethodBase.GetCurrentMethod().Name + " enter", LogLevel.Trace);

                //if device present at all and its ID is set
                if (Enabled && DRIVER_NAME != "")
                {
                    try
                    {
                        Connected_flag = objTelescope.Connected;
                    }
                    catch (Exception ex)
                    {
                        Connected_flag = false;
                        Logging.AddLog("Telescope get connection error! [" + ex.ToString() + "]", LogLevel.Important, Highlight.Error);
                    }
                }
                else
                {
                    Connected_flag = false;
                    //Print if somebody try to connect if device isn't presetn. Mostly for debug
                    Logging.AddLog("Device is not set. Couldn't return status of telescope", LogLevel.Debug, Highlight.Error);
                }

                Logging.AddLog(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + Connected_flag, LogLevel.Trace);
                return Connected_flag;
            }
        }


        public void Park()
        {
            //Log enter
            Logging.AddLog(MethodBase.GetCurrentMethod().Name + " enter", LogLevel.Trace);


            //if device present at all and its ID is set
            if (Enabled && DRIVER_NAME != "" && objTelescope != null)
            {
                try
                {
                    objTelescope.Park();
                }
                catch (Exception ex)
                {
                    Logging.AddLog("Couldn't move to Park status", LogLevel.Important, Highlight.Error);
                    Logging.AddLog(MethodBase.GetCurrentMethod().Name + " error! [" + ex.ToString() + "]", LogLevel.Debug, Highlight.Error);
                }
            }
            else
            {
                //Print if somebody try to connect if device isn't presetn. Mostly for debug
                Logging.AddLog("Telescope is not set. Couldn't set Park status", LogLevel.Debug, Highlight.Error);
            }

            Logging.AddLog(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + "void", LogLevel.Trace);
        }

        public void UnPark()
        {
            //Log enter
            Logging.AddLog(MethodBase.GetCurrentMethod().Name + " enter", LogLevel.Trace);


            //if device present at all and its ID is set
            if (Enabled && DRIVER_NAME != "" && objTelescope != null)
            {
                try
                {
                    objTelescope.Unpark();
                }
                catch (Exception ex)
                {
                    Logging.AddLog("Couldn't move to UnPark status", LogLevel.Important, Highlight.Error);
                    Logging.AddLog(MethodBase.GetCurrentMethod().Name + " error! [" + ex.ToString() + "]", LogLevel.Debug, Highlight.Error);
                }
            }
            else
            {
                //Print if somebody try to connect if device isn't presetn. Mostly for debug
                Logging.AddLog("Telescope is not set. Couldn't set UnPark status", LogLevel.Debug, Highlight.Error);
            }

            Logging.AddLog(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + "void", LogLevel.Trace);
        }

        public void TrackToggle()
        {
            //Log enter
            Logging.AddLog(MethodBase.GetCurrentMethod().Name + " enter", LogLevel.Trace);


            //if device present at all and its ID is set
            if (Enabled && DRIVER_NAME != "" && objTelescope != null)
            {
                try
                {
                    curTracking = objTelescope.Tracking; //read
                    objTelescope.Tracking = !curTracking; //invert
                    curTracking = objTelescope.Tracking; //reread inverted
                }
                catch (Exception ex)
                {
                    Logging.AddLog("Couldn't switch on Tracking", LogLevel.Important, Highlight.Error);
                    Logging.AddLog(MethodBase.GetCurrentMethod().Name + " error! [" + ex.ToString() + "]", LogLevel.Debug, Highlight.Error);
                }
            }
            else
            {
                //Print if somebody try to connect if device isn't presetn. Mostly for debug
                Logging.AddLog("Telescope is not set. Couldn't switch on Tracking", LogLevel.Debug, Highlight.Error);
            }

            Logging.AddLog(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + "void", LogLevel.Trace);
        }

        private PierSide PierSideStatus
        {
            get
            {
                //Log enter
                Logging.AddLog(MethodBase.GetCurrentMethod().Name + " enter", LogLevel.Trace);


                //if device present at all and its ID is set
                if (Enabled && DRIVER_NAME != "" && objTelescope != null)
                {
                    try
                    {
                        curPierSideStatus = objTelescope.SideOfPier;
                    }
                    catch (Exception ex)
                    {
                        curPierSideStatus = PierSide.pierUnknown;
                        Logging.AddLog("Couldn't get SidePier status", LogLevel.Important, Highlight.Error);
                        Logging.AddLog(MethodBase.GetCurrentMethod().Name + " error! [" + ex.ToString() + "]", LogLevel.Debug, Highlight.Error);
                    }
                }
                else
                {
                    curPierSideStatus = PierSide.pierUnknown;
                    //Print if somebody try to connect if device isn't presetn. Mostly for debug
                    Logging.AddLog("Telescope is not set. Couldn't return SideOfPier status", LogLevel.Debug, Highlight.Error);
                }

                Logging.AddLog(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + curPierSideStatus, LogLevel.Trace);
                return curPierSideStatus;
            }
        }


        /// <summary>
        /// Get Telescope Azimuth
        /// </summary>
        private double Azimuth
        {
            get
            {
                //Log enter
                Logging.AddLog(MethodBase.GetCurrentMethod().Name + " enter", LogLevel.Trace);


                //if device present at all and its ID is set
                if (Enabled && DRIVER_NAME != "" && objTelescope != null)
                {
                    try
                    {
                        curAzimuth = objTelescope.Azimuth;
                    }
                    catch (Exception ex)
                    {
                        curAzimuth = -1;
                        Reset();
                        Logging.AddLog("Couldn't get telescope Azimuth", LogLevel.Important, Highlight.Error);
                        Logging.AddLog(MethodBase.GetCurrentMethod().Name + " error! [" + ex.ToString() + "]", LogLevel.Debug, Highlight.Error);
                    }
                }
                else
                {
                    curAzimuth = -1;
                    //Print if somebody try to connect if device isn't presetn. Mostly for debug
                    Logging.AddLog("Telescope is not set. Couldn't return Azimuth", LogLevel.Debug, Highlight.Error);
                }

                Logging.AddLog(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + curAzimuth, LogLevel.Trace);
                return curAzimuth;
            }
        }


        /// <summary>
        /// Get Telescope Altitude
        /// </summary>
        private double Altitude
        {
            get
            {
                //Log enter
                Logging.AddLog(MethodBase.GetCurrentMethod().Name + " enter", LogLevel.Trace);


                //if device present at all and its ID is set
                if (Enabled && DRIVER_NAME != "" && objTelescope != null)
                {
                    try
                    {
                        curAltitude = objTelescope.Altitude;
                    }
                    catch (Exception ex)
                    {
                        curAltitude = -100;
                        Reset();
                        Logging.AddLog("Couldn't get telescope Altitude", LogLevel.Important, Highlight.Error);
                        Logging.AddLog(MethodBase.GetCurrentMethod().Name + " error! [" + ex.ToString() + "]", LogLevel.Debug, Highlight.Error);
                    }
                }
                else
                {
                    curAltitude = -100;
                    //Print if somebody try to connect if device isn't presetn. Mostly for debug
                    Logging.AddLog("Telescope is not set. Couldn't return Altitude", LogLevel.Debug, Highlight.Error);
                }

                Logging.AddLog(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + curAltitude, LogLevel.Trace);
                return curAltitude;
            }
        }


        /// <summary>
        /// Get Telescope Declination
        /// </summary>
        private double Declination
        {
            get
            {
                //Log enter
                Logging.AddLog(MethodBase.GetCurrentMethod().Name + " enter", LogLevel.Trace);


                //if device present at all and its ID is set
                if (Enabled && DRIVER_NAME != "" && objTelescope != null)
                {
                    try
                    {
                        curDeclination = objTelescope.Declination;
                    }
                    catch (Exception ex)
                    {
                        curDeclination = -100;
                        Reset();
                        Logging.AddLog("Couldn't get telescope Declination", LogLevel.Important, Highlight.Error);
                        Logging.AddLog(MethodBase.GetCurrentMethod().Name + " error! [" + ex.ToString() + "]", LogLevel.Debug, Highlight.Error);
                    }
                }
                else
                {
                    curDeclination = -100;
                    //Print if somebody try to connect if device isn't presetn. Mostly for debug
                    Logging.AddLog("Telescope is not set. Couldn't return Declination", LogLevel.Debug, Highlight.Error);
                }

                Logging.AddLog(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + curDeclination, LogLevel.Trace);
                return curDeclination;
            }
        }

        /// <summary>
        /// Get Telescope RightAscension
        /// </summary>
        private double RightAscension
        {
            get
            {
                //Log enter
                Logging.AddLog(MethodBase.GetCurrentMethod().Name + " enter", LogLevel.Trace);


                //if device present at all and its ID is set
                if (Enabled && DRIVER_NAME != "" && objTelescope != null)
                {
                    try
                    {
                        curRightAscension = objTelescope.RightAscension;
                    }
                    catch (Exception ex)
                    {
                        curRightAscension = -100;
                        Reset();
                        Logging.AddLog("Couldn't get telescope RightAscension", LogLevel.Important, Highlight.Error);
                        Logging.AddLog(MethodBase.GetCurrentMethod().Name + " error! [" + ex.ToString() + "]", LogLevel.Debug, Highlight.Error);
                    }
                }
                else
                {
                    curRightAscension = -100;
                    //Print if somebody try to connect if device isn't presetn. Mostly for debug
                    Logging.AddLog("Telescope is not set. Couldn't return RightAscension", LogLevel.Debug, Highlight.Error);
                }

                Logging.AddLog(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + curRightAscension, LogLevel.Trace);
                return curRightAscension;
            }
        }



        /// <summary>
        /// Get Telescope SiderealTime
        /// </summary>
        private double SiderealTime
        {
            get
            {
                //Log enter
                Logging.AddLog(MethodBase.GetCurrentMethod().Name + " enter", LogLevel.Trace);


                //if device present at all and its ID is set
                if (Enabled && DRIVER_NAME != "" && objTelescope != null)
                {
                    try
                    {
                        curSiderealTime = objTelescope.SiderealTime;
                    }
                    catch (Exception ex)
                    {
                        curSiderealTime = -100;
                        Logging.AddLog("Couldn't get telescope SiderealTime", LogLevel.Important, Highlight.Error);
                        Logging.AddLog(MethodBase.GetCurrentMethod().Name + " error! [" + ex.ToString() + "]", LogLevel.Debug, Highlight.Error);
                    }
                }
                else
                {
                    curSiderealTime = -100;
                    Reset();
                    //Print if somebody try to connect if device isn't presetn. Mostly for debug
                    Logging.AddLog("Telescope is not set. Couldn't return SiderealTime", LogLevel.Debug, Highlight.Error);
                }

                Logging.AddLog(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + curSiderealTime, LogLevel.Trace);
                return curSiderealTime;
            }
        }



        /// <summary>
        /// Get Telescope AtPark status
        /// </summary>
        private bool AtPark
        {
            get
            {
                //Log enter
                Logging.AddLog(MethodBase.GetCurrentMethod().Name + " enter", LogLevel.Trace);


                //if device present at all and its ID is set
                if (Enabled && DRIVER_NAME != "" && objTelescope != null)
                {
                    try
                    {
                        curAtPark = objTelescope.AtPark;
                    }
                    catch (Exception ex)
                    {
                        curAtPark = false;
                        Reset();
                        Logging.AddLog("Couldn't get telescope AtPark status", LogLevel.Important, Highlight.Error);
                        Logging.AddLog(MethodBase.GetCurrentMethod().Name + " error! [" + ex.ToString() + "]", LogLevel.Debug, Highlight.Error);
                    }
                }
                else
                {
                    curAtPark = false;
                    //Print if somebody try to connect if device isn't presetn. Mostly for debug
                    Logging.AddLog("Telescope is not set. Couldn't return SiderealTime", LogLevel.Debug, Highlight.Error);
                }

                Logging.AddLog(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + curAtPark, LogLevel.Trace);
                return curAtPark;
            }
        }


        /// Get Telescope Tracking status
        /// </summary>
        private bool Tracking
        {
            get
            {
                //Log enter
                Logging.AddLog(MethodBase.GetCurrentMethod().Name + " enter", LogLevel.Trace);


                //if device present at all and its ID is set
                if (Enabled && DRIVER_NAME != "" && objTelescope != null)
                {
                    try
                    {
                        curTracking = objTelescope.Tracking;
                    }
                    catch (Exception ex)
                    {
                        curTracking = false;
                        Reset();
                        Logging.AddLog("Couldn't get telescope Tracking status", LogLevel.Important, Highlight.Error);
                        Logging.AddLog(MethodBase.GetCurrentMethod().Name + " error! [" + ex.ToString() + "]", LogLevel.Debug, Highlight.Error);
                    }
                }
                else
                {
                    curTracking = false;
                    //Print if somebody try to connect if device isn't presetn. Mostly for debug
                    Logging.AddLog("Telescope is not set. Couldn't return Tracking status", LogLevel.Debug, Highlight.Error);
                }

                Logging.AddLog(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + curTracking, LogLevel.Trace);
                return curTracking;
            }
        }

        /// <summary>
        /// Wrapper to reset telescope driver 
        /// Later system would reinitiate it itself
        /// </summary>
        public void Reset()
        {
            Connected_flag = false;

            curAzimuth = -1;
            curAltitude = -100;
            curRightAscension = -100;
            curDeclination = -100;
            curSiderealTime = -100;
            curPierSideStatus = PierSide.pierUnknown;

            curAtPark = false;
            curTracking = false;

            objTelescope = null;
        }
    }
}
