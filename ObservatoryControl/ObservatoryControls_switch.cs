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
        int SWITCH_MAX_RECCONNECT_ATTEMPTS = 3; //reconnect attempts

#region ASCOM Device Drivers controlling (connect/disconnect/status)  ///////////////////////////////////////////////////////////////////

        //Device connection flags
        public bool Switch_connected_flag = false;
        public bool Mount_connected_flag = false;
        public bool Dome_connected_flag = false;

        //Power buttons state flags
        public bool? Mount_power_flag = null;
        public bool? Camera_power_flag = null;
        public bool? Focuser_power_flag = null;
        public bool? Roof_power_flag = null;
        
        /// <summary>
        /// Connect to switch / Get connection status
        /// </summary>
        public bool connectSwitch
        {
            set
            {
                //Log enter
                Logging.AddLog(MethodBase.GetCurrentMethod().Name + (value ? "ON" : "OFF"), LogLevel.Trace);

                //If obj doesnot exist - create
                if (objSwitch == null) objSwitch = new ASCOM.DriverAccess.Switch(SWITCH_DRIVER_NAME);
                
                //Connect
                try
                {
                    objSwitch.Connected = value;
                    Switch_connected_flag = value;
                    Logging.AddLog("Switch has been " + (value ? "connected" : "disconnected"), LogLevel.Activity);
                }
                catch (Exception ex)
                {
                    Switch_connected_flag = false;
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
            get
            {
                //Log enter
                Logging.AddLog(MethodBase.GetCurrentMethod().Name +" enter", LogLevel.Trace);

                try
                {
                    Switch_connected_flag  = objSwitch.Connected;
                }
                catch (Exception ex)
                {
                    Switch_connected_flag = false;
                    Logging.AddLog(MethodBase.GetCurrentMethod().Name + "error! [" + ex.ToString() + "]", LogLevel.Important, Highlight.Error);
                }
                Logging.AddLog(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + Switch_connected_flag, LogLevel.Trace);
                return Switch_connected_flag;
            }
        }

        
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

                //If obj doesnot exist - create
                if (objTelescope == null) objTelescope = new ASCOM.DriverAccess.Telescope(TELESCOPE_DRIVER_NAME);

                //Connect
                try
                {
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
                Logging.AddLog(MethodBase.GetCurrentMethod().Name +" enter", LogLevel.Trace);

                try
                {
                    Mount_connected_flag =objTelescope.Connected;
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

                //If obj doesnot exist - create
                if (objDome == null) objDome = new ASCOM.DriverAccess.Dome(DOME_DRIVER_NAME);

                //Connect
                try
                {
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
            if (Switch_connected_flag)
            {
                try
                {
                    val = objSwitch.GetSwitch(PORT_NUM);
                }
                catch (NotConnectedException ex)
                {
                    //Not connected? try to recconect for N times
                    Logging.AddLog("Switch driver not connected exception [" + ex.Message + "]. Try to reconnect", LogLevel.Activity, Highlight.Error);
                    //MessageBox.Show(ex.ToString());
                    bool reconnF = true; int i = 1;
                    while (reconnF)
                    {
                        connectSwitch = true; //try to connect
                        reconnF = (i++ <= SWITCH_MAX_RECCONNECT_ATTEMPTS) && !Switch_connected_flag;
                    }
                    //reconnect result ok?
                    if (Switch_connected_flag)
                    {
                        //try to get switch again
                        Logging.AddLog("Switch driver reconnected", LogLevel.Activity);
                        try
                        {
                            val = objSwitch.GetSwitch(PORT_NUM);
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
            if (Switch_connected_flag)
            {
                try
                {
                    objSwitch.SetSwitch(PORT_NUM, (bool)set_value);
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
                        connectSwitch = true; //connect
                        reconnF = (i++ <= SWITCH_MAX_RECCONNECT_ATTEMPTS) && !Switch_connected_flag;
                    }
                    if (Switch_connected_flag)
                    {
                        Logging.AddLog("Switch driver reconnected", LogLevel.Activity);
                        try
                        {
                            objSwitch.SetSwitch(PORT_NUM, (bool)set_value);
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
        /// Get or set mount power
        /// </summary>
        public bool? MountPower__
        {
            get
            {
                //log enter
                Logging.AddLog(System.Reflection.MethodBase.GetCurrentMethod().Name + " enter", LogLevel.Trace);

                bool? val = null;

                //if previously switch was connected, try to get value
                if (Switch_connected_flag)
                {
                    try
                    {
                        val = objSwitch.GetSwitch(POWER_MOUNT_PORT);
                    }
                    catch (NotConnectedException ex)
                    {
                        //Not connected? try to recconect for N times
                        Logging.AddLog("Switch driver not connected exception [" + ex.Message + "]. Try to reconnect", LogLevel.Activity, Highlight.Error);
                        //MessageBox.Show(ex.ToString());
                        bool reconnF = true; int i = 1;
                        while (reconnF)
                        {
                            connectSwitch = true; //try to connect
                            reconnF = (i++ <= SWITCH_MAX_RECCONNECT_ATTEMPTS) && !Switch_connected_flag;
                        }
                        //reconnect result ok?
                        if (Switch_connected_flag)
                        {
                            //try to get switch again
                            Logging.AddLog("Switch driver reconnected", LogLevel.Activity);
                            try 
                            {
                                val = objSwitch.GetSwitch(POWER_MOUNT_PORT);
                            }
                            catch (Exception ex2)
                            {
                                //if again exception - give up
                                val = null;
                                Logging.AddLog("Couldn't get switch value ["+ex2.Message+"]", LogLevel.Important);
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
                        Logging.AddLog("Get mount power unknown exception [" + ex.Message+"]!", LogLevel.Important, Highlight.Error);
                        Logging.AddLog("Get mount power exception: " + ex.ToString(), LogLevel.Debug, Highlight.Debug);
                    }
                }
                else
                {
                    val = null;
                    Logging.AddLog(System.Reflection.MethodBase.GetCurrentMethod().Name + ": Switch has not been connected", LogLevel.Trace);
                }
                Logging.AddLog(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + val, LogLevel.Trace);
                return val;
            }
            set
            {
                Logging.AddLog("Mount power switching " + ((bool)value ? "ON" : "OFF"), LogLevel.Activity);

                try
                {
                    objSwitch.SetSwitch(POWER_MOUNT_PORT, (bool)value);
                }
                catch (NotConnectedException ex)
                {
                    //MessageBox.Show(ex.ToString());
                    Logging.AddLog("Switch driver notconnected exception [" + ex.Message + "]. Try to reconnect", LogLevel.Activity, Highlight.Error);
                    bool reconnF = true; int i = 1;
                    while (reconnF)
                    {
                        Logging.AddLog("Switch driver reconnection attempt "+i, LogLevel.Activity);
                        connectSwitch = true;
                        reconnF = (i++ <= SWITCH_MAX_RECCONNECT_ATTEMPTS) && !Switch_connected_flag;
                    }
                    if (Switch_connected_flag)
                    {
                        Logging.AddLog("Switch driver reconnected", LogLevel.Activity);
                    }
                    else
                    {
                        Logging.AddLog("Switch driver couldn't be reconnected after " + SWITCH_MAX_RECCONNECT_ATTEMPTS + " attempts", LogLevel.Activity);
                    }

                }
                catch (Exception ex)
                {
                    Logging.AddLog("Set mount power exception: " + ex.Message, LogLevel.Important, Highlight.Error);
                    Logging.AddLog("Set mount power exception: " + ex.ToString(), LogLevel.Debug, Highlight.Error);
                }
                Logging.AddLog(System.Reflection.MethodBase.GetCurrentMethod().Name + " exit", LogLevel.Trace);
            }
        }

        /// <summary>
        /// Get or set camera power
        /// </summary>
        public bool CameraPower___
        {
            get{
                Logging.AddLog(System.Reflection.MethodBase.GetCurrentMethod().Name + " enter", LogLevel.Trace);
                bool val = false;
                try
                {
                    val = objSwitch.GetSwitch(POWER_CAMERA_PORT);
                }
                catch (ASCOM.NotConnectedException ex)
                {
                    Logging.AddLog("Get camera power exception: " + ex.Message, LogLevel.Important, Highlight.Error);
                    MessageBox.Show(ex.ToString());
                }
                Logging.AddLog(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + val, LogLevel.Trace);
                return val;
            }
            set{
                Logging.AddLog("Camera power switching " + (value ? "ON" : "OFF"), LogLevel.Activity);
                objSwitch.SetSwitch(POWER_CAMERA_PORT, value);
            }
        }

        /// <summary>
        /// Get or set focuser power
        /// </summary>
        public bool FocusPower____
        {
            get{
                Logging.AddLog(System.Reflection.MethodBase.GetCurrentMethod().Name + " enter", LogLevel.Trace);
                bool val = false;
                try
                {
                    val = objSwitch.GetSwitch(POWER_FOCUSER_PORT);
                }
                catch (Exception ex)
                {
                    Logging.AddLog("Get focus power exception: " + ex.Message, LogLevel.Important, Highlight.Error);
                    MessageBox.Show(ex.ToString());
                }
                Logging.AddLog(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + val, LogLevel.Trace);
                return val;
            }
            set{
                Logging.AddLog("Focus power switching " + (value ? "ON" : "OFF"), LogLevel.Activity);
                objSwitch.SetSwitch(POWER_FOCUSER_PORT, value);
            }
        }

        /// <summary>
        /// Get or set roof power
        /// </summary>
        public bool RoofPower___
        {
            get{
                Logging.AddLog(System.Reflection.MethodBase.GetCurrentMethod().Name + " enter", LogLevel.Trace);
                bool val = false;
                try
                {
                    val = objSwitch.GetSwitch(POWER_ROOFPOWER_PORT);
                }
                catch (Exception ex)
                {
                    Logging.AddLog("Get roof power exception: " + ex.Message, 0, Highlight.Error);
                    MessageBox.Show(ex.ToString());
                }
                Logging.AddLog(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + val, LogLevel.Trace);
                return val;
            }
            set{
                Logging.AddLog("Roof power switching " + (value ? "ON" : "OFF"), LogLevel.Activity);
                objSwitch.SetSwitch(POWER_ROOFPOWER_PORT, value);
            }
        }

        /// <summary>
        /// Wrapper for mount power on|off. Used in command processor
        /// </summary>
        public string PowerMountOn()
        {
            Logging.AddLog("Switch mount power on", LogLevel.Activity);
            PowerSet(POWER_MOUNT_PORT, "POWER_MOUNT_PORT", true, out Mount_power_flag);
            return "PowerMountOn";
        }
        public string PowerMountOff()
        {
            Logging.AddLog("Switch mount power off", LogLevel.Activity);
            PowerSet(POWER_MOUNT_PORT, "POWER_MOUNT_PORT", false, out Mount_power_flag);
            return "PowerMountOff";
        }
        /// <summary>
        /// Wrapper for camera power on|off. Used in command processor
        /// </summary>
        public string PowerCameraOn()
        {
            Logging.AddLog("Switch camera power on", LogLevel.Activity);
            PowerSet(POWER_CAMERA_PORT, "POWER_CAMERA_PORT", true, out Camera_power_flag);
            return "PowerCameraOn";
        }
        public string PowerCameraOff()
        {
            Logging.AddLog("Switch camera power off", LogLevel.Activity);
            PowerSet(POWER_CAMERA_PORT, "POWER_CAMERA_PORT", false, out Camera_power_flag);
            return "PowerCameraOff";
        }

        /// <summary>
        /// Wrapper for focuse power on|off. Used in command processor
        /// </summary>
        public string PowerFocuserOn()
        {
            Logging.AddLog("Switch focuser power on", LogLevel.Activity);
            PowerSet(POWER_FOCUSER_PORT, "POWER_FOCUSER_PORT", true, out Focuser_power_flag);
            return "PowerFocuserOn";
        }
        public string PowerFocuserOff()
        {
            Logging.AddLog("Switch focuser power off", LogLevel.Activity);
            PowerSet(POWER_FOCUSER_PORT, "POWER_FOCUSER_PORT", false, out Focuser_power_flag);
            return "PowerFocuserOff";
        }

        /// <summary>
        /// Wrapper for roof power on|off. Used in command processor
        /// </summary>
        public string PowerRoofOn()
        {
            Logging.AddLog("Switch roof power on", LogLevel.Activity);
            PowerSet(POWER_ROOFPOWER_PORT, "POWER_ROOFPOWER_PORT", true, out Roof_power_flag);
            return "PowerRoofOn";
        }
        public string PowerRoofOff()
        {
            Logging.AddLog("Switch roof power off", LogLevel.Activity);
            PowerSet(POWER_ROOFPOWER_PORT, "POWER_ROOFPOWER_PORT", false, out Roof_power_flag);
            return "PowerRoofOff";
        }
#endregion Power controlling


#region Multithreading ////////////////////////////////////////////////////////////////////////////////////////////
        public void CheckPowerDeviceStatus()
        {
            try
            {
                Mount_power_flag = PowerGet(POWER_MOUNT_PORT,"POWER_MOUNT_PORT");
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

    }
}
