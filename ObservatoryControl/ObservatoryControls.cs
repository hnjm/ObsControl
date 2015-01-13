using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using IP9212_switch;

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

        internal IP9212_switch_class IP9212;

        public ASCOM.DriverAccess.Telescope objTelescope = null;
        public ASCOM.DriverAccess.Dome objDome = null;
        public ASCOM.DriverAccess.Switch objSwitch = null;

        public string DOME_DRIVER_NAME = "";
        public string TELESCOPE_DRIVER_NAME = "";
        public string SWITCH_DRIVER_NAME = "";

        #region
        internal byte POWER_MAIN_PORT=6;
        internal byte POWER_FOCUSER_PORT = 8;
        internal byte POWER_ROOF_PORT = 3;
        #endregion


        public ObservatoryControls(IP9212_switch_class IP9212_ref)
        {
            IP9212=IP9212_ref;
        }

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

        public bool startCheckSwitch()
        {
            //check if can be connected
            if (!IP9212.checkLink_forced())
            {
                //Log.AddMessage("startSwitch","Cann't connect to switch. Check your settings");
                System.Windows.Forms.MessageBox.Show("Cann't connect to switch. Check your settings");
                return false;
            }
            return true;
        }


        public bool connectSwitch()
        {
            //for debug
            SWITCH_DRIVER_NAME = "SwitchSim.Switch";

            objSwitch = new ASCOM.DriverAccess.Switch(SWITCH_DRIVER_NAME);
            objSwitch.Connected = true;
            
            return true;
        }

        public bool connectTelescope()
        {

            
            return true;
        }


        public bool connectDome()
        {


            return true;
        }


        #region Power controlling

        public bool MainPower(bool state)
        {
            Logging.Log("Main power switching "+(state?"ON":"OFF"),2);
            objSwitch.SetSwitch(POWER_MAIN_PORT, state);
            return true;
        }


        #endregion



    }
}
