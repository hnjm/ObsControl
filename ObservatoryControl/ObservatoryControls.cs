using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using ASCOM;
using ASCOM.DeviceInterface;
using ASCOM.Utilities;

using MaxIm;

namespace ObservatoryCenter
{
    public class ObservatoryControls
    {
        public string PlanetariumPath=@"c:\Program Files (x86)\Diffraction Limited\MaxIm DL V5\MaxIm_DL.exe" ;
        public string MaximDLPath=@"c:\Program Files (x86)\Diffraction Limited\MaxIm DL V5\MaxIm_DL.exe" ;
        public string CCDAPPath=@"c:\Program Files (x86)\CCDWare\CCDAutoPilot5\CCDAutoPilot5.exe";

        public Process MaximDL = new Process();
        public Process CCDAP = new Process();
        public Process Planetarium = new Process();

        public ASCOM.DriverAccess.Telescope objTelescope = null;
        public ASCOM.DriverAccess.Dome objDome = null;
        public ASCOM.DriverAccess.Switch objSwitch = null;
        public ASCOM.DriverAccess.Focuser objFocuser = null;
        public ASCOM.DriverAccess.Camera objCamera = null;

        public string SWITCH_DRIVER_NAME = "";
        public string DOME_DRIVER_NAME = "";
        public string TELESCOPE_DRIVER_NAME = "";

        public MaxIm.CCDCamera CCDCamera;

        /// <summary>
        /// Property holds current shutter status
        /// </summary>
        internal ShutterState CurrentSutterStatus
        {
            get{
                if (objDome != null)
                    return objDome.ShutterStatus;
                else
                {
                    return ShutterState.shutterError;
                }

            }
        }

        #region
        public byte POWER_MOUNT_PORT = 6;
        public byte POWER_CAMERA_PORT = 5;
        public byte POWER_FOCUSER_PORT = 3;
        public byte POWER_ROOFPOWER_PORT = 2;
        public byte POWER_ROOFSWITCH_PORT = 4;
        #endregion

        /// <summary>
        /// Conctructor
        /// </summary>
        public ObservatoryControls()
        {
            //for debug
            SWITCH_DRIVER_NAME = "SwitchSim.Switch";
            DOME_DRIVER_NAME = "ASCOM.Simulator.Dome";
            TELESCOPE_DRIVER_NAME = "EQMOD_SIM.Telescope";
        }

#region Programs Controlling
        public void startPlanetarium()
        {
            Planetarium.StartInfo.FileName = PlanetariumPath;
            Planetarium.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
            Planetarium.StartInfo.UseShellExecute = false;
            Planetarium.Start();
        }

        public void startMaximDL()
        {
            MaximDL.StartInfo.FileName = MaximDLPath;
            MaximDL.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
            MaximDL.StartInfo.UseShellExecute = false;
            MaximDL.Start();
        }

        public void startCCDAP()
        {
            CCDAP.StartInfo.FileName = CCDAPPath;
            CCDAP.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
            CCDAP.StartInfo.UseShellExecute = false;
            CCDAP.Start();
        }
#endregion Program controlling

        public bool connectSwitch()
        {
            objSwitch = new ASCOM.DriverAccess.Switch(SWITCH_DRIVER_NAME);
            objSwitch.Connected = true;

            return objSwitch.Connected;
        }

        public bool connectTelescope()
        {
            objTelescope = new ASCOM.DriverAccess.Telescope(TELESCOPE_DRIVER_NAME);
            objTelescope.Connected = true;

            return objTelescope.Connected;
        }

        public bool connectDome()
        {
            objDome= new ASCOM.DriverAccess.Dome(DOME_DRIVER_NAME);
            objDome.Connected = true;

            return objDome.Connected;
        }

        public bool startCheckSwitch()
        {
            //check if can be connected
            if (!objSwitch.Connected)
            {
                System.Windows.Forms.MessageBox.Show("Cann't connect to switch. Check your settings");
                return false;
            }
            return true;
        }


#region Power controlling

        public bool MountPower
        {
            get
            {
                Logging.Log("Mount power get", 2);
                return objSwitch.GetSwitch(POWER_MOUNT_PORT);
            }
            set{
                Logging.Log("Mount power switching "+(value?"ON":"OFF"),2);
                objSwitch.SetSwitch(POWER_MOUNT_PORT, value);
            }
        }

        public bool CameraPower
        {
            get{
                Logging.Log("Camera power get", 2);
                return objSwitch.GetSwitch(POWER_CAMERA_PORT);
            }
            set{
                Logging.Log("Camera power switching " + (value ? "ON" : "OFF"), 2);
                objSwitch.SetSwitch(POWER_CAMERA_PORT, value);
            }
        }

        public bool FocusPower
        {
            get{
                Logging.Log("Focus power get", 2);
                return objSwitch.GetSwitch(POWER_FOCUSER_PORT);
            }
            set{
                Logging.Log("Focus power switching " + (value ? "ON" : "OFF"), 2);
                objSwitch.SetSwitch(POWER_FOCUSER_PORT, value);
            }
        }
        
        public bool RoofPower
        {
            get{
                Logging.Log("Roof power get", 2);
                return objSwitch.GetSwitch(POWER_ROOFPOWER_PORT);
            }
            set{
                Logging.Log("Roof power switching " + (value ? "ON" : "OFF"), 2);
                objSwitch.SetSwitch(POWER_ROOFPOWER_PORT, value);
            }
        }


#endregion Power controlling

#region Roof control
        public bool RoofOpen()
        {
            Logging.Log("Trying to open roof", 1);

            //Check if power is connected
            if (!objSwitch.GetSwitch(POWER_ROOFPOWER_PORT))
            {
                Logging.Log("Roof power switched off", 1);
                return false;
            }

            objDome.OpenShutter();
            return true;
        }

        public bool RoofClose()
        {
            Logging.Log("Trying to close roof", 1);

            //Check if power is connected
            if (!objSwitch.GetSwitch(POWER_ROOFPOWER_PORT))
            {
                Logging.Log("Roof power switched off", 1);
                return false;
            }

            objDome.CloseShutter();
            return true;
        }

#endregion Roof control end

        #region Maxim controls
        public void ConnectCamera()
        {
            CCDCamera = new MaxIm.CCDCamera();
            CCDCamera.LinkEnabled = true;
        }
        #endregion Maxim controls
    }
}
