using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Reflection;
using System.Configuration;

using ASCOM;
using ASCOM.DeviceInterface;
using ASCOM.Utilities;

namespace ObservatoryCenter
{
    public partial class MainForm : Form
    {
        
        public ObservatoryControls ObsControl;

        /// <summary>
        /// Link to preferences form + functions for loading parameters
        /// </summary>
        public SettingsForm SetForm;

        /// <summary>
        /// Constructor
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            
            ObsControl = new ObservatoryControls();
            SetForm = new SettingsForm(this);
        }

        /// <summary>
        /// Main form load event - startup actions take place here
        /// </summary>
        private void Form1_Load(object sender, EventArgs e)
        {
            //Connect Devices, which are general adapters (no need to power or control something)
            ObsControl.connectSwitch();
            ObsControl.connectDome();

            //Update visual interface statuses
            UpdateStatusbarASCOMStatus();

            //init graphic elements
            ROOF_startPos = rectRoof.Location;
            //Update visual Roof Status
            UpdateRoofPicture();
        }

        /// <summary>
        /// Updates markers in status bar
        /// </summary>
        private void UpdateStatusbarASCOMStatus()
        {
            //SWITCH
            if (ObsControl.objSwitch != null && ObsControl.objSwitch.Connected)
            {
                toolStripStatus_Switch.ForeColor = Color.Black;
            }
            else
            {
                toolStripStatus_Switch.ForeColor = Color.Gray;
            }

            //DOME
            if (ObsControl.objDome != null && ObsControl.objDome.Connected)
            {
                toolStripStatus_Dome.ForeColor = Color.Black;
            }
            else
            {
                toolStripStatus_Dome.ForeColor = Color.Gray;
            }

            //TELESCOPE
            if (ObsControl.objTelescope != null && ObsControl.objTelescope.Connected)
            {
                toolStripStatus_Telescope.ForeColor = Color.Blue;
            }
            else
            {
                toolStripStatus_Telescope.ForeColor = Color.Gray;
            }

            //FOCUSER
            if (ObsControl.objFocuser != null && ObsControl.objFocuser.Connected)
            {
                toolStripStatus_Focuser.ForeColor = Color.Blue;
            }
            else
            {
                toolStripStatus_Focuser.ForeColor = Color.Gray;
            }

            //CAMERA
            if (ObsControl.objCamera != null && ObsControl.objCamera.Connected)
            {
                toolStripStatus_Camera.ForeColor = Color.Blue;
            }
            else
            {
                toolStripStatus_Camera.ForeColor = Color.Gray;
            }

        }


        private void btnStartAll_Click(object sender, EventArgs e)
        {
            //ObsControl.startMaximDL();
            ObsControl.startCCDAP();
            //ObsControl.startPlanetarium();
        }


        private void btnTelescopePower_Click(object sender, EventArgs e)
        {
            //get current state
            bool SwitchState=(ledTelescopePower.Status==ASCOM.Controls.TrafficLight.Green);
            SwitchState=!SwitchState;

            //toggle
            ObsControl.MainPower(SwitchState);

            //display new status
            if (SwitchState)
                ledTelescopePower.Status = ASCOM.Controls.TrafficLight.Green;
            else
                ledTelescopePower.Status = ASCOM.Controls.TrafficLight.Red;

            ledTelescopePower.Status = ASCOM.Controls.TrafficLight.Yellow;
            ledTelescopePower.CadenceUpdate(true);

            /*ledSwitchIndicator.Status = ASCOM.Controls.TrafficLight.Yellow;
            ledSwitchIndicator.Refresh();
            ledSwitchIndicator.Update();
            ledSwitchIndicator.Enabled=true;*/

        }


        private void btnRoofPower_Click(object sender, EventArgs e)
        {

        }

        private void btnCameraPower_Click(object sender, EventArgs e)
        {
            //get current state
            bool SwitchState = (ledTelescopePower.Status == ASCOM.Controls.TrafficLight.Green);
            SwitchState = !SwitchState;

            //toggle
            ObsControl.MainPower(SwitchState);

            //display new status
            if (SwitchState)
                ledTelescopePower.Status = ASCOM.Controls.TrafficLight.Green;
            else
                ledTelescopePower.Status = ASCOM.Controls.TrafficLight.Red;

            ledTelescopePower.Status = ASCOM.Controls.TrafficLight.Yellow;
            ledTelescopePower.CadenceUpdate(true);

        }

        private void btnFocuserPower_Click(object sender, EventArgs e)
        {

        }


        private void buttonChoose_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.SwitchDriverId = ASCOM.DriverAccess.Switch.Choose(Properties.Settings.Default.SwitchDriverId);
        }




        /// <summary>
        /// Used to load all prameters during startup
        /// </summary>
        public void LoadParams()
        {
            Logging.Log("Loading saved parameters",3);
            try
            {
                ObsControl.MaximDLPath = Properties.Settings.Default.MaximDLPath;
                ObsControl.CCDAPPath = Properties.Settings.Default.CCDAPPath;
                ObsControl.PlanetariumPath = Properties.Settings.Default.CartesPath;

                ObsControl.POWER_MOUNT_PORT = Convert.ToByte(Properties.Settings.Default.SwitchMountPort);
                ObsControl.POWER_CAMERA_PORT = Convert.ToByte(Properties.Settings.Default.SwitchCameraPort);
                ObsControl.POWER_FOCUSER_PORT = Convert.ToByte(Properties.Settings.Default.SwitchFocuserPort);
                ObsControl.POWER_ROOFPOWER_PORT = Convert.ToByte(Properties.Settings.Default.SwitchRoofPowerPort);
                ObsControl.POWER_ROOFSWITCH_PORT = Convert.ToByte(Properties.Settings.Default.SwitchRoofSwitchPort);
            }
            catch (Exception ex)
            {
                StackTrace st = new StackTrace(ex, true);
                StackFrame[] frames = st.GetFrames();
                string messstr = "";

                // Iterate over the frames extracting the information you need
                foreach (StackFrame frame in frames)
                {
                    messstr += String.Format("{0}:{1}({2},{3})", frame.GetFileName(), frame.GetMethod().Name, frame.GetFileLineNumber(), frame.GetFileColumnNumber());
                }

                string FullMessage = "Error loading params. ";
                FullMessage += "IOException source: " + ex.Data + " | " + ex.Message + " | " + messstr;

                Logging.Log(FullMessage);
            }
            Logging.Log("Loading saved parameters end", 3);


        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            SetForm.ShowDialog();
        }



    }
}
