using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ASCOM;
using ASCOM.DeviceInterface;
using ASCOM.Utilities;
using System.Reflection;

namespace ObservatoryCenter
{
    public partial class ObservatoryControls
    {

        /// <summary>
        /// ASCOM TELESCOPE DRIVER WRAPPERS
        /// </summary>


        private double curAzimuth = -1;
        private double curAltitude = -100;
        private double curRightAscension = -100;
        private double curDeclination = -100;
        private double curSiderealTime = -100;

        private bool curAtPark = false;
        private bool curTracking = false;


        public void Telescope_Park()
        {
            //Log enter
            Logging.AddLog(MethodBase.GetCurrentMethod().Name + " enter", LogLevel.Trace);


            //if device present at all and its ID is set
            if (TelescopeEnabled && TELESCOPE_DRIVER_NAME != "")
            {
                try
                {
                    objTelescope.Park();
                }
                catch (Exception ex)
                {
                    Logging.AddLog("Couldn't move to Park status", LogLevel.Important, Highlight.Error);
                    Logging.AddLog(MethodBase.GetCurrentMethod().Name + "error! [" + ex.ToString() + "]", LogLevel.Important, Highlight.Error);
                }
            }
            else
            {
                //Print if somebody try to connect if device isn't presetn. Mostly for debug
                Logging.AddLog("Telescope is not set. Couldn't set Park status", LogLevel.Debug, Highlight.Error);
            }

            Logging.AddLog(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + "void", LogLevel.Trace);
        }


        public void Telescope_TrackToggle()
        {
            //Log enter
            Logging.AddLog(MethodBase.GetCurrentMethod().Name + " enter", LogLevel.Trace);


            //if device present at all and its ID is set
            if (TelescopeEnabled && TELESCOPE_DRIVER_NAME != "")
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
                    Logging.AddLog(MethodBase.GetCurrentMethod().Name + "error! [" + ex.ToString() + "]", LogLevel.Important, Highlight.Error);
                }
            }
            else
            {
                //Print if somebody try to connect if device isn't presetn. Mostly for debug
                Logging.AddLog("Telescope is not set. Couldn't switch on Tracking", LogLevel.Debug, Highlight.Error);
            }

            Logging.AddLog(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + "void", LogLevel.Trace);
        }

        public PierSide TelescopePierSideStatus
        {
            get
            {
                //Log enter
                Logging.AddLog(MethodBase.GetCurrentMethod().Name + " enter", LogLevel.Trace);


                //if device present at all and its ID is set
                if (TelescopeEnabled && TELESCOPE_DRIVER_NAME != "")
                {
                    try
                    {
                        curPierSideStatus = objTelescope.SideOfPier;
                    }
                    catch (Exception ex)
                    {
                        curPierSideStatus = PierSide.pierUnknown;
                        Logging.AddLog("Couldn't get SidePier status", LogLevel.Important, Highlight.Error);
                        Logging.AddLog(MethodBase.GetCurrentMethod().Name + "error! [" + ex.ToString() + "]", LogLevel.Important, Highlight.Error);
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
        public double Telescope_Azimuth
        {
            get
            {
                //Log enter
                Logging.AddLog(MethodBase.GetCurrentMethod().Name + " enter", LogLevel.Trace);


                //if device present at all and its ID is set
                if (TelescopeEnabled && TELESCOPE_DRIVER_NAME != "")
                {
                    try
                    {
                        curAzimuth = objTelescope.Azimuth;
                    }
                    catch (Exception ex)
                    {
                        curAzimuth = -1;
                        Logging.AddLog("Couldn't get telescope Azimuth", LogLevel.Important, Highlight.Error);
                        Logging.AddLog(MethodBase.GetCurrentMethod().Name + "error! [" + ex.ToString() + "]", LogLevel.Important, Highlight.Error);
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
        public double Telescope_Altitude
        {
            get
            {
                //Log enter
                Logging.AddLog(MethodBase.GetCurrentMethod().Name + " enter", LogLevel.Trace);


                //if device present at all and its ID is set
                if (TelescopeEnabled && TELESCOPE_DRIVER_NAME != "")
                {
                    try
                    {
                        curAltitude = objTelescope.Altitude;
                    }
                    catch (Exception ex)
                    {
                        curAltitude = -100;
                        Logging.AddLog("Couldn't get telescope Altitude", LogLevel.Important, Highlight.Error);
                        Logging.AddLog(MethodBase.GetCurrentMethod().Name + "error! [" + ex.ToString() + "]", LogLevel.Important, Highlight.Error);
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
        public double Telescope_Declination
        {
            get
            {
                //Log enter
                Logging.AddLog(MethodBase.GetCurrentMethod().Name + " enter", LogLevel.Trace);


                //if device present at all and its ID is set
                if (TelescopeEnabled && TELESCOPE_DRIVER_NAME != "")
                {
                    try
                    {
                        curDeclination = objTelescope.Declination;
                    }
                    catch (Exception ex)
                    {
                        curDeclination = -100;
                        Logging.AddLog("Couldn't get telescope Declination", LogLevel.Important, Highlight.Error);
                        Logging.AddLog(MethodBase.GetCurrentMethod().Name + "error! [" + ex.ToString() + "]", LogLevel.Important, Highlight.Error);
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
        public double Telescope_RightAscension
        {
            get
            {
                //Log enter
                Logging.AddLog(MethodBase.GetCurrentMethod().Name + " enter", LogLevel.Trace);


                //if device present at all and its ID is set
                if (TelescopeEnabled && TELESCOPE_DRIVER_NAME != "")
                {
                    try
                    {
                        curRightAscension = objTelescope.RightAscension;
                    }
                    catch (Exception ex)
                    {
                        curRightAscension = -100;
                        Logging.AddLog("Couldn't get telescope RightAscension", LogLevel.Important, Highlight.Error);
                        Logging.AddLog(MethodBase.GetCurrentMethod().Name + "error! [" + ex.ToString() + "]", LogLevel.Important, Highlight.Error);
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
        public double Telescope_SiderealTime
        {
            get
            {
                //Log enter
                Logging.AddLog(MethodBase.GetCurrentMethod().Name + " enter", LogLevel.Trace);


                //if device present at all and its ID is set
                if (TelescopeEnabled && TELESCOPE_DRIVER_NAME != "")
                {
                    try
                    {
                        curSiderealTime = objTelescope.SiderealTime;
                    }
                    catch (Exception ex)
                    {
                        curSiderealTime = -100;
                        Logging.AddLog("Couldn't get telescope SiderealTime", LogLevel.Important, Highlight.Error);
                        Logging.AddLog(MethodBase.GetCurrentMethod().Name + "error! [" + ex.ToString() + "]", LogLevel.Important, Highlight.Error);
                    }
                }
                else
                {
                    curSiderealTime = -100;
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
        public bool Telescope_AtPark
        {
            get
            {
                //Log enter
                Logging.AddLog(MethodBase.GetCurrentMethod().Name + " enter", LogLevel.Trace);


                //if device present at all and its ID is set
                if (TelescopeEnabled && TELESCOPE_DRIVER_NAME != "")
                {
                    try
                    {
                        curAtPark = objTelescope.AtPark;
                    }
                    catch (Exception ex)
                    {
                        curAtPark = false;
                        Logging.AddLog("Couldn't get telescope AtPark status", LogLevel.Important, Highlight.Error);
                        Logging.AddLog(MethodBase.GetCurrentMethod().Name + "error! [" + ex.ToString() + "]", LogLevel.Important, Highlight.Error);
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
        public bool Telescope_Tracking
        {
            get
            {
                //Log enter
                Logging.AddLog(MethodBase.GetCurrentMethod().Name + " enter", LogLevel.Trace);


                //if device present at all and its ID is set
                if (TelescopeEnabled && TELESCOPE_DRIVER_NAME != "")
                {
                    try
                    {
                        curTracking = objTelescope.Tracking;
                    }
                    catch (Exception ex)
                    {
                        curTracking = false;
                        Logging.AddLog("Couldn't get telescope Tracking status", LogLevel.Important, Highlight.Error);
                        Logging.AddLog(MethodBase.GetCurrentMethod().Name + "error! [" + ex.ToString() + "]", LogLevel.Important, Highlight.Error);
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


    }
}
