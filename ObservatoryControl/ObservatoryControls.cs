using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Net;

using ASCOM;
using ASCOM.DeviceInterface;
using ASCOM.Utilities;

using MaxIm;

namespace ObservatoryCenter
{
    public class ObservatoryControls
    {
        /// <summary>
        /// Back link to form
        /// </summary>
        public MainForm ParentMainForm;
        
        public string PlanetariumPath=@"c:\Program Files (x86)\Ciel\skychart.exe";
        public string MaximDLPath=@"c:\Program Files (x86)\Diffraction Limited\MaxIm DL V5\MaxIm_DL.exe" ;
        public string CCDAPPath=@"c:\Program Files (x86)\CCDWare\CCDAutoPilot5\CCDAutoPilot5.exe";
        public string FocusMaxPath = @"c:\Program Files (x86)\FocusMax\FocusMax.exe";

        public Process MaximDL_Process = new Process();
        public Process CCDAP_Process = new Process();
        public Process CdC_Process = new Process();
        public Process FocusMax_Process = new Process();

        public MaximControls MaximObj;

        public ASCOM.DriverAccess.Telescope objTelescope = null;
        public ASCOM.DriverAccess.Dome objDome = null;
        public ASCOM.DriverAccess.Switch objSwitch = null;
        public ASCOM.DriverAccess.Focuser objFocuser = null;
        public ASCOM.DriverAccess.Camera objCamera = null;

        public string SWITCH_DRIVER_NAME = "";
        public string DOME_DRIVER_NAME = "";
        public string TELESCOPE_DRIVER_NAME = "";

        public Int32 CdC_PORT = 3292;

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
        public ObservatoryControls(MainForm MF)
        {
            ParentMainForm=MF;

            //for debug
            SWITCH_DRIVER_NAME = "SwitchSim.Switch";
            DOME_DRIVER_NAME = "ASCOM.Simulator.Dome";
            TELESCOPE_DRIVER_NAME = "EQMOD_SIM.Telescope";

            MaximObj = new MaximControls();
        }

#region Programs Controlling  ///////////////////////////////////////////////////////////////////
        public void startPlanetarium()
        {
            CdC_Process.StartInfo.FileName = PlanetariumPath;
            CdC_Process.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
            CdC_Process.StartInfo.UseShellExecute = false;
            CdC_Process.Start();
        }

        public void startMaximDL()
        {
            MaximDL_Process.StartInfo.FileName = MaximDLPath;
            MaximDL_Process.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
            MaximDL_Process.StartInfo.UseShellExecute = false;
            MaximDL_Process.Start();

            MaximObj.Init();
        }

        public void startCCDAP()
        {
            CCDAP_Process.StartInfo.FileName = CCDAPPath;
            CCDAP_Process.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
            CCDAP_Process.StartInfo.UseShellExecute = false;
            CCDAP_Process.Start();
        }

        public void startFocusMax()
        {
            try
            {
                FocusMax_Process.StartInfo.FileName = FocusMaxPath;
                FocusMax_Process.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
                FocusMax_Process.StartInfo.UseShellExecute = false;
                FocusMax_Process.Start();
                Logging.Log("FocusMax started", 0);
            }
            catch (Exception Ex)
            {
                Logging.Log("FocusMax failed", 0);
            }
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


#region Power controlling ////////////////////////////////////////////////////////////////////////////////////////////

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

#region Roof control //////////////////////////////////////////////////////////////////////////////////////////
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

#region CdC controls /////////////////////////////////////////////////////
        public void CdC_connectTelescope()
        {
            ParentMainForm.SocketServer.MakeClientConnectionToServer(IPAddress.Parse("127.0.0.1"), CdC_PORT, "CONNECTTELESCOPE");
        }
#endregion CdC controls

#region Scenarios section ////////////////////////////////////////////////////////
        /// <summary>
        /// Init observatory activity 
        /// </summary>
        public void StartUpObservatory()
        {

            //1. Switch on power
            MountPower = true;
            CameraPower = true;
            FocusPower = true;

            //2. Run MaximDL
            startMaximDL();

            //3. Run FocusMax
            startFocusMax();

            //4. CameraConnect
            MaximObj.ConnectCamera();

            //5. Set camera cooler
            MaximObj.SetCameraCooling();

            //6. Connect telescope to Maxim
            MaximObj.ConnectTelescope();

            //7. Connect focuser in Maxim to FocusMax
            MaximObj.ConnectFocuser();

            //8. Run Cartes du Ciel
            startPlanetarium();

            //9. Connect telescope in Cartes du Ciel
            CdC_connectTelescope();

            //10. Connect telescope in Cartes du Ciel
            startCCDAP();
        
        }


#endregion Scenarios
    }
}
