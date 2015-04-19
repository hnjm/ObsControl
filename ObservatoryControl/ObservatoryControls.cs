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

        public bool MainPower(bool state)
        {
            Logging.Log("Main power switching "+(state?"ON":"OFF"),2);
            objSwitch.SetSwitch(POWER_MOUNT_PORT, state);
            return true;
        }


#endregion Power controlling


        #region Maxim controls
        public void ConnectCamera()
        {
            CCDCamera = new MaxIm.CCDCamera();
            CCDCamera.LinkEnabled = true;
        }
        #endregion Maxim controls
    }
}
