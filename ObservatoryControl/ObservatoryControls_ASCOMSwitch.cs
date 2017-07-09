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
    public class ObservatoryControls_ASCOMSwitch
    {
        public bool Enabled = false;
        public bool Connected_flag = false;

        public string DRIVER_NAME = "";
        private ASCOM.DriverAccess.Switch objSwitch = null;

        int SWITCH_MAX_RECCONNECT_ATTEMPTS = 3; //reconnect attempts

        #region switch ports
        public byte POWER_TELESCOPE_PORT = 6;
        public byte POWER_CAMERA_PORT = 5;
        public byte POWER_FOCUSER_PORT = 3;
        public byte POWER_ROOFPOWER_PORT = 2;
        public byte POWER_ROOFSWITCH_PORT = 4;
        #endregion

        #region ASCOM Device Drivers controlling (connect/disconnect/status)  ///////////////////////////////////////////////////////////////////

        //Power buttons state flags
        public bool? Telescope_power_flag = null;
        public bool? Camera_power_flag = null;
        public bool? Focuser_power_flag = null;
        public bool? Roof_power_flag = null;

        /// <summary>
        /// Constructor
        /// </summary>
        public ObservatoryControls_ASCOMSwitch()
        {
        }



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
                        if (objSwitch == null) objSwitch = new ASCOM.DriverAccess.Switch(DRIVER_NAME);

                        //Connect
                        objSwitch .Connected = value;
                        Connected_flag = value;
                        Logging.AddLog("Switch has been " + (value ? "connected" : "disconnected"), LogLevel.Activity);
                    }
                    catch (Exception ex)
                    {
                        Connected_flag = false;
                        Logging.AddLog("Couldn't " + (value ? "connect to" : "disconnect from") + " switch", LogLevel.Important, Highlight.Error);
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
                    Logging.AddLog("Device is not set. Couldn't " + (value ? "connect to" : "disconnect ") + " switch", LogLevel.Debug, Highlight.Error);
                }
            }
            get
            {
                //Log enter
                Logging.AddLog(MethodBase.GetCurrentMethod().Name +" enter", LogLevel.Trace);
                //if device present at all and its ID is set
                if (Enabled && DRIVER_NAME != "")
                {

                    try
                    {
                        Connected_flag  = objSwitch .Connected;
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
                    Logging.AddLog("Device is not set. Couldn't return status of switch", LogLevel.Debug, Highlight.Error);
                }

                Logging.AddLog(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + Connected_flag, LogLevel.Trace);
                return Connected_flag;
            }
        }


#endregion ASCOM Device Drivers controlling

#region Power controlling ////////////////////////////////////////////////////////////////////////////////////////////


        /// <summary>
        /// Get switch power wrapper
        /// </summary>
        public bool? PowerGet(byte PORT_NUM, string PORT_NAME)
        {
            //log enter
            Logging.AddLog(System.Reflection.MethodBase.GetCurrentMethod().Name +" ["+ PORT_NAME +"] enter", LogLevel.Trace);

            bool? val = null;

            //if previously switch was connected, try to get value
            if (Connected_flag)
            {
                try
                {
                    val = objSwitch .GetSwitch(PORT_NUM);
                }
                catch (NotConnectedException ex)
                {
                    //Not connected? try to recconect for N times
                    Logging.AddLog("Switch driver not connected exception [" + ex.Message + "]. Try to reconnect", LogLevel.Activity, Highlight.Error);
                    //MessageBox.Show(ex.ToString());
                    bool reconnF = true; int i = 1;
                    while (reconnF)
                    {
                        Connect = true; //try to connect
                        reconnF = (i++ <= SWITCH_MAX_RECCONNECT_ATTEMPTS) && !Connected_flag;
                    }
                    //reconnect result ok?
                    if (Connected_flag)
                    {
                        //try to get switch again
                        Logging.AddLog("Switch driver reconnected", LogLevel.Activity);
                        try
                        {
                            val = objSwitch .GetSwitch(PORT_NUM);
                        }
                        catch (Exception ex2)
                        {
                            //if again exception - give up
                            val = null;
                            Logging.AddLog("Couldn't get switch ["+PORT_NUM+"] value [" + ex2.Message + "]", LogLevel.Important);
                            Logging.AddLog(MethodBase.GetCurrentMethod().Name + " exception [" + ex2.ToString() + "]", LogLevel.Debug, Highlight.Error);
                        }
                    }
                    else
                    {
                        val = null;
                        Logging.AddLog("Switch driver couldn't be reconnected after " + SWITCH_MAX_RECCONNECT_ATTEMPTS + " attempts", LogLevel.Activity);
                    }
                }
                catch (Exception ex)
                {
                    val = null;
                    Logging.AddLog("Get "+PORT_NAME+" unknown exception [" + ex.Message + "]!", LogLevel.Important, Highlight.Error);
                    Logging.AddLog(""+PORT_NAME+" exception details: " + ex.ToString(), LogLevel.Debug, Highlight.Debug);
                }
            }
            else
            {
                //switch wasn't even connected
                val = null;
                Logging.AddLog(System.Reflection.MethodBase.GetCurrentMethod().Name + " ["+ PORT_NAME +"]: Switch wasn't connected, exiting", LogLevel.Trace);
            }
            
            Logging.AddLog(System.Reflection.MethodBase.GetCurrentMethod().Name +" ["+ PORT_NAME +"] return value: "+val.ToString(), LogLevel.Trace);
            return val;
        }

       
        /// <summary>
        /// Set switch power wrapper
        /// </summary>
        /// <param name="PORT_NUM"></param>
        /// <param name="PORT_NAME"></param>
        /// <param name="set_value"></param>
        /// <returns>true if ok, false if error</returns>
        public bool PowerSet(byte PORT_NUM, string PORT_NAME, bool set_value, out bool? power_flag)
        {
            //log enter
            Logging.AddLog(PORT_NAME + " switching " + ((bool)set_value ? "ON" : "OFF"), LogLevel.Activity);
            bool retval = false;

            //if previously switch was connected, try to set value
            if (Connected_flag)
            {
                try
                {
                    objSwitch .SetSwitch(PORT_NUM, (bool)set_value);
                    retval = true;
                }
                catch (NotConnectedException ex)
                {
                    //MessageBox.Show(ex.ToString());
                    Logging.AddLog("Switch driver notconnected exception [" + ex.Message + "]. Try to reconnect", LogLevel.Activity, Highlight.Error);
                    bool reconnF = true; int i = 1;
                    while (reconnF)
                    {
                        Logging.AddLog("Switch driver reconnect attempt " + i, LogLevel.Activity);
                        Connect = true; //connect
                        reconnF = (i++ <= SWITCH_MAX_RECCONNECT_ATTEMPTS) && !Connected_flag;
                    }
                    if (Connected_flag)
                    {
                        Logging.AddLog("Switch driver reconnected", LogLevel.Activity);
                        try
                        {
                            objSwitch .SetSwitch(PORT_NUM, (bool)set_value);
                            retval = true;
                        }
                        catch (Exception ex2)
                        {
                            //if again exception - give up
                            retval = false;
                            Logging.AddLog("Couldn't get switch [" + PORT_NUM + "] value [" + ex2.Message + "]", LogLevel.Important);
                            Logging.AddLog(MethodBase.GetCurrentMethod().Name + " exception [" + ex2.ToString() + "]", LogLevel.Debug, Highlight.Error);
                        }
                    }
                    else
                    {
                        Logging.AddLog("Switch driver couldn't be reconnected after " + SWITCH_MAX_RECCONNECT_ATTEMPTS + " attempts", LogLevel.Activity);
                        retval = false;
                    }

                }
                catch (Exception ex)
                {
                    Logging.AddLog("Get " + PORT_NAME + " unknown exception [" + ex.Message + "]!", LogLevel.Important, Highlight.Error);
                    Logging.AddLog("" + PORT_NAME + " exception details: " + ex.ToString(), LogLevel.Debug, Highlight.Debug);
                    retval = false;
                }
                
            }
            else
            {
                retval = false;
                Logging.AddLog(System.Reflection.MethodBase.GetCurrentMethod().Name + " [" + PORT_NAME + "]: switch was not connected, exiting" + retval, LogLevel.Trace);
            }


            //set appropriate var
            if (retval)
            {
                power_flag = set_value;
            }
            else
            {
                power_flag = null;
            }

            Logging.AddLog(System.Reflection.MethodBase.GetCurrentMethod().Name + " [" + PORT_NAME + "] exit. Result :" + retval, LogLevel.Trace);
            return retval;
        }


        /// <summary>
        /// Wrapper for mount power on|off. Used in command processor
        /// </summary>
        public string PowerMountOn()
        {
            Logging.AddLog("Switch mount power on", LogLevel.Trace);
            PowerSet(POWER_TELESCOPE_PORT, "POWER_MOUNT_PORT", true, out Telescope_power_flag);
            return "PowerMountOn";
        }
        public string PowerMountOff()
        {
            Logging.AddLog("Switch mount power off", LogLevel.Trace);
            PowerSet(POWER_TELESCOPE_PORT, "POWER_MOUNT_PORT", false, out Telescope_power_flag);
            return "PowerMountOff";
        }
        /// <summary>
        /// Wrapper for camera power on|off. Used in command processor
        /// </summary>
        public string PowerCameraOn()
        {
            Logging.AddLog("Switch camera power on", LogLevel.Trace);
            PowerSet(POWER_CAMERA_PORT, "POWER_CAMERA_PORT", true, out Camera_power_flag);
            return "PowerCameraOn";
        }
        public string PowerCameraOff()
        {
            Logging.AddLog("Switch camera power off", LogLevel.Trace);
            PowerSet(POWER_CAMERA_PORT, "POWER_CAMERA_PORT", false, out Camera_power_flag);
            return "PowerCameraOff";
        }

        /// <summary>
        /// Wrapper for focuse power on|off. Used in command processor
        /// </summary>
        public string PowerFocuserOn()
        {
            Logging.AddLog("Switch focuser power on", LogLevel.Trace);
            PowerSet(POWER_FOCUSER_PORT, "POWER_FOCUSER_PORT", true, out Focuser_power_flag);
            return "PowerFocuserOn";
        }
        public string PowerFocuserOff()
        {
            Logging.AddLog("Switch focuser power off", LogLevel.Trace);
            PowerSet(POWER_FOCUSER_PORT, "POWER_FOCUSER_PORT", false, out Focuser_power_flag);
            return "PowerFocuserOff";
        }

        /// <summary>
        /// Wrapper for roof power on|off. Used in command processor
        /// </summary>
        public string PowerRoofOn()
        {
            Logging.AddLog("Switch roof power on", LogLevel.Trace);
            PowerSet(POWER_ROOFPOWER_PORT, "POWER_ROOFPOWER_PORT", true, out Roof_power_flag);
            return "PowerRoofOn";
        }
        public string PowerRoofOff()
        {
            Logging.AddLog("Switch roof power off", LogLevel.Trace);
            PowerSet(POWER_ROOFPOWER_PORT, "POWER_ROOFPOWER_PORT", false, out Roof_power_flag);
            return "PowerRoofOff";
        }

        public string PowerMainRelaysOn()
        {
            PowerMountOn();
            PowerCameraOn();
            PowerFocuserOn();
            return "PowerMainRelays";
        }

        #endregion Power controlling


#region Multithreading ////////////////////////////////////////////////////////////////////////////////////////////
        public void CheckPowerDeviceStatus()
        {
            try
            {
                Telescope_power_flag = PowerGet(POWER_TELESCOPE_PORT,"POWER_MOUNT_PORT");
                Camera_power_flag = PowerGet(POWER_CAMERA_PORT, "POWER_CAMERA_PORT");
                Roof_power_flag = PowerGet(POWER_ROOFPOWER_PORT, "POWER_ROOFPOWER_PORT");
                Focuser_power_flag = PowerGet(POWER_FOCUSER_PORT, "POWER_FOCUSER_PORT");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception in ChechDeviceStatus! "+ex.ToString());
            }
        }

        public void SetDeviceStatus(bool? RoofPower, bool? FocuserPower, bool? MountPower, bool? CameraPower)
        {
            if (RoofPower != null)
            {
                try
                {
                    PowerSet(POWER_ROOFPOWER_PORT, "POWER_ROOFPOWER_PORT", (bool)RoofPower, out Roof_power_flag);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Exception in SetDeviceStatus! " + ex.ToString());
                }
            }
        }


#endregion Multithreading
        
        /// <summary>
        /// Wrapper to reset telescope driver 
        /// Later system would reinitiate it itself
        /// </summary>
        public void Reset()
        {
            Connected_flag = false;

            Telescope_power_flag = null;
            Camera_power_flag = null;
            Focuser_power_flag = null;
            Roof_power_flag = null;

            objSwitch = null;
        }
    }
}
