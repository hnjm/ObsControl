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
using System.Threading;

using ASCOM;
using ASCOM.DeviceInterface;
using ASCOM.Utilities;
using System.IO;
using System.Xml;
using System.Windows.Forms.DataVisualization.Charting;
using LoggingLib;

namespace ObservatoryCenter
{
    /// <summary>
    /// Events handler for VISUAL ELEMENTS OF MAIN FORM
    ///  Contains events hadnlers of some controls
    /// </summary>
    public partial class MainForm
    {

        // Region block with hadnling power management visual interface
        #region /// POWER BUTTONS HANDLING ///////////////////////////////////////////////////////////////////////////////////////////////////
        private void btnTelescopePower_Click(object sender, EventArgs e)
        {
            Logging.AddLog(MethodBase.GetCurrentMethod().Name + " enter", LogLevel.Trace);

            //get current state
            bool SwitchState = (((Button)sender).BackColor == OnColor);
            SwitchState = !SwitchState;

            //toggle
            if (ObsControl.ASCOMSwitch.PowerSet(ObsControl.ASCOMSwitch.POWER_TELESCOPE_PORT, "POWER_MOUNT_PORT", SwitchState, out ObsControl.ASCOMSwitch.Telescope_power_flag))
            {
                //if switching was successful
                //display new status
                ((Button)sender).BackColor = (SwitchState ? OnColor : OffColor);
                //ObsControl.Mount_power_flag = SwitchState;
            }
            else
            {
                //if switching wasn't proceed
            }
        }

        private void btnCameraPower_Click(object sender, EventArgs e)
        {
            Logging.AddLog(MethodBase.GetCurrentMethod().Name + " enter", LogLevel.Trace);

            //get current state
            bool SwitchState = (((Button)sender).BackColor == OnColor);
            SwitchState = !SwitchState;

            //toggle
            if (ObsControl.ASCOMSwitch.PowerSet(ObsControl.ASCOMSwitch.POWER_CAMERA_PORT, "POWER_CAMERA_PORT", SwitchState, out ObsControl.ASCOMSwitch.Camera_power_flag))
            {
                //if switching was successful

                //display new status
                ((Button)sender).BackColor = (SwitchState ? OnColor : OffColor);
                //ObsControl.Camera_power_flag = SwitchState;
                /////
                //txtCameraName.BackColor = (SwitchState ? OnColor : OffColor);
            }
            else
            {
                //if switching wasn't proceed

            }
        }
        #endregion Power button handling
        // End of block with power buttons handling




        // AppLinks Events 
        #region //// AppLinks Events //////////////////////////////////////
        private void linkCdC_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ObsControl.startPlanetarium();
        }
        private void linkTest_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //ObsControl.objPHD2App.CMD_ConnectEquipment(); //connect equipment
            //ObsControl.GuidePiexelScale=ObsControl.objPHD2App.CMD_GetPixelScale(); //connect equipment

            ObsControl.objWSApp.CMD_GetBoltwoodString(); //get booltwood string

        }
        private void linkPHD2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ObsControl.CommandParser.ParseSingleCommand2("PHD2_RUN");

            Thread.Sleep(ConfigManagement.getInt("scenarioMainParams", "PHD_CONNECT_PAUSE") ?? 0);

            ObsControl.CommandParser.ParseSingleCommand2("PHD2_CONNECT");
        }
        private void linkMaximDL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ObsControl.CommandParser.ParseSingleCommand2("MAXIM_RUN");
            ObsControl.CommandParser.ParseSingleCommand2("MAXIM_CAMERA_CONNECT");
            ObsControl.CommandParser.ParseSingleCommand2("MAXIM_CAMERA_SETCOOLING");
            ObsControl.CommandParser.ParseSingleCommand2("MAXIM_TELESCOPE_CONNECT");
        }
        private void linkCCDAP_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ObsControl.startCCDAP();
        }
        private void linkPHDBroker_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ObsControl.CommandParser.ParseSingleCommand2("PHDBROKER_RUN");
        }
        private void linkFocusMax_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ObsControl.startFocusMax();
        }
        private void linkWeatherStation_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ObsControl.startWS();
        }
        private void linkTelescopeTempControl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ObsControl.startTTC();
        }

        private void linkCCDC_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ObsControl.CommandParser.ParseSingleCommand2("CCDC_RUN");

            ObsControl.objCCDCApp.Automation_Start();

            Thread.Sleep(2000);
            ObsControl.objCCDCApp.Automation_Pause();

            Thread.Sleep(2000);
            //ObsControl.objCCDCApp.Automation_Stop();
        }
        #endregion //// AppLinks Events //////////////////////////////////////
        // End of AppLinks Events block 



        // Status bar event handling block
        #region //// Status bar events handling //////////////////////////////////////
        private void toolStripStatus_Switch_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                ObsControl.ASCOMSwitch.Connect = !ObsControl.ASCOMSwitch.Connected_flag;
                ObsControl.ASCOMSwitch.CheckPowerDeviceStatus_async();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception in status bar switch connect/disconnect! " + ex.ToString());
            }
        }
        private void toolStripStatus_Dome_Click(object sender, EventArgs e)
        {
            try
            {
                ObsControl.ASCOMDome.Connect = !ObsControl.ASCOMDome.Connected_flag;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception in status bar dome connect/disconnect! " + ex.ToString());
            }

        }
        private void toolStripStatus_Telescope_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                ObsControl.ASCOMTelescope.Connect = !ObsControl.ASCOMTelescope.Connected_flag;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception in telescope connect/disconnect! " + ex.ToString());
            }

        }
        private void toolStripStatus_Camera_Click(object sender, EventArgs e)
        {
            ObsControl.objMaxim.ConnectCamera();
        }
        //Change log level control
        private void toolStripDropDownLogLevel_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            toolStripDropDownLogLevel.Text = e.ClickedItem.Text;
        }
        #endregion /// Status bar event handling //////////////////////////////////////////////
        // End of Status bar event handling block




        // Settings tab ASCOM Devices
        #region /// Settings tab ASCOM Devices ////////////////////////////////////////////////////////////////
        private void chkASCOM_Enable_Switch_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked == false)
            {
                //disconnect
                ObsControl.ASCOMSwitch.Connect = false;
                ObsControl.ASCOMSwitch.Enabled = false;
                ObsControl.ASCOMSwitch.Reset();
            }
            else
            {
                //connect
                ObsControl.ASCOMSwitch.Enabled = true;
                ObsControl.ASCOMSwitch.Connect = true;
                ObsControl.ASCOMSwitch.CheckPowerDeviceStatus_async();
            }
            Update_SWITCH_related_elements();
            Properties.Settings.Default.DeviceEnabled_Switch = ObsControl.ASCOMSwitch.Enabled;
        }
        private void btnASCOM_Choose_Switch_Click(object sender, EventArgs e)
        {
            ObsControl.ASCOMSwitch.DRIVER_NAME = ASCOM.DriverAccess.Switch.Choose(Properties.Settings.Default.SwitchDriverId);
            txtSet_Switch.Text = ObsControl.ASCOMSwitch.DRIVER_NAME;
            if (ObsControl.ASCOMSwitch.DRIVER_NAME != "")
            {
                chkASCOM_Enable_Switch.Checked = true;
            }

            ObsControl.ASCOMSwitch.Reset();
            ObsControl.ASCOMSwitch.Connect = true;
            ObsControl.ASCOMSwitch.CheckPowerDeviceStatus_async();

            Update_SWITCH_related_elements();
        }
        private void chkASCOM_Enable_Dome_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked == false)
            {
                //disconnect
                ObsControl.ASCOMDome.Connect = false;
                ObsControl.ASCOMDome.Enabled = false;
                ObsControl.ASCOMDome.Reset();
            }
            else
            {
                //connect
                ObsControl.ASCOMDome.Enabled = true;
                ObsControl.ASCOMDome.Connect = true;
            }
            Update_DOME_related_elements();
            Properties.Settings.Default.DeviceEnabled_Dome = ObsControl.ASCOMDome.Enabled;
        }
        private void btnASCOM_Choose_Dome_Click(object sender, EventArgs e)
        {
            ObsControl.ASCOMDome.DRIVER_NAME = ASCOM.DriverAccess.Dome.Choose(Properties.Settings.Default.DomeDriverId);
            txtSet_Dome.Text = ObsControl.ASCOMDome.DRIVER_NAME;
            if (ObsControl.ASCOMDome.DRIVER_NAME != "")
            {
                chkASCOM_Enable_Dome.Checked = true;
            }
            ObsControl.ASCOMDome.Reset();
            ObsControl.ASCOMDome.Connect = true;
            Update_DOME_related_elements();
        }
        private void chkASCOM_Enable_Telescope_CheckedChanged(object sender, EventArgs e)
        {

            if (((CheckBox)sender).Checked == false)
            {
                //disconnect
                ObsControl.ASCOMTelescope.Connect = false;
                ObsControl.ASCOMTelescope.Enabled = false;
                ObsControl.ASCOMTelescope.Reset();
            }
            else
            {
                //connect
                ObsControl.ASCOMTelescope.Enabled = true;
                ObsControl.ASCOMTelescope.Connect = true;
            }
            Update_TELESCOPE_related_elements();
            Properties.Settings.Default.DeviceEnabled_Telescope = ObsControl.ASCOMTelescope.Enabled;
        }
        private void btnASCOM_Choose_Telescope_Click(object sender, EventArgs e)
        {
            ObsControl.ASCOMTelescope.DRIVER_NAME = ASCOM.DriverAccess.Telescope.Choose(Properties.Settings.Default.TelescopeDriverId);
            txtSet_Telescope.Text = ObsControl.ASCOMTelescope.DRIVER_NAME;
            if (ObsControl.ASCOMTelescope.DRIVER_NAME != "")
            {
                chkASCOM_Enable_Telescope.Checked = true;
            }
            ObsControl.ASCOMTelescope.Reset();
            ObsControl.ASCOMTelescope.Connect = true;
            Update_TELESCOPE_related_elements();
        }

        #endregion /// Settings tab ASCOM Devices /////////////////////////////////////////////////////////////////
        // End of Settings tab ASCOM Devices block






        // Camera control events 
        #region //// Camera control events //////////////////////////////////////////////////

        private void btnCoolerOn_Click(object sender, EventArgs e)
        {
            ObsControl.objMaxim.CameraCoolingOn();
        }

        private void btnCoolerOff_Click(object sender, EventArgs e)
        {
            ObsControl.objMaxim.CameraCoolingOff();
        }

        private void btnCoolerWarm_Click(object sender, EventArgs e)
        {
            ObsControl.objMaxim.CameraCoolingOff(true);
        }

        private void up_down_SetPoint_ValueChanged(object sender, EventArgs e)
        {
            ObsControl.objMaxim.CameraCoolingOn(Convert.ToDouble(updownCameraSetPoint.Value));
        }
        #endregion // Camera control events ////////////////////////////////////////////////
        // End of Camera control events 






        // Telescope interface events 
        #region //// Telescope interface events  //////////////////////////////////////////////////

        private void btnPark_Click(object sender, EventArgs e)
        {
            if (ObsControl.ASCOMTelescope.curAtPark)
            {
                ObsControl.ASCOMTelescope.UnPark();
            }
            else
            {
                ObsControl.ASCOMTelescope.Park();
            }
        }

        private void btnTrack_Click(object sender, EventArgs e)
        {
            ObsControl.ASCOMTelescope.TrackToggle();
        }
        #endregion // Telescope interface events ////////////////////////////////////////////////
        // End of Telescope interface events 




        /// <summary>
        /// Run TestEquipment Form
        /// </summary>
        private void btnRunTest_Click(object sender, EventArgs e)
        {
            TestForm.Show();
        }

        private void btnAstrotortillaSolve_Click(object sender, EventArgs e)
        {
            //Run async
            ThreadStart RunThreadRef = new ThreadStart(ObsControl.startAstrotortillaSolve);
            Thread childThread = new Thread(RunThreadRef);
            childThread.Start();
            //Logging.AddLog("Command 'Prepare run' was initiated", LogLevel.Debug);

        }


        private void btnKILL_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Будем ждать завершения активностей программ? Если нет - это может привести к непредсказуемым результатам!", "Confirm kill", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3);
            if (confirmResult == DialogResult.Yes)
            {
                //Close all
                ObsControl.objCCDCApp.Stop();
                ObsControl.objFocusMaxApp.Stop();
                ObsControl.objMaxim.Stop();
                ObsControl.objPHDBrokerApp.Stop();
                ObsControl.objPHD2App.Stop();
                ObsControl.objCdCApp.Stop();
            }
            else if (confirmResult == DialogResult.No)
            {
                //Kill all
                ObsControl.objCCDCApp.Kill();
                ObsControl.objFocusMaxApp.Kill();
                ObsControl.objMaxim.Kill();
                ObsControl.objPHDBrokerApp.Kill();
                ObsControl.objPHD2App.Kill();
                ObsControl.objCdCApp.Kill();
            }
        }


        private void btnSoftStop_Click(object sender, EventArgs e)
        {
            chkPause_CheckedChanged(chkPause, e);
        }

        private void btnEmergencyStop_Click(object sender, EventArgs e)
        {
            chkAbort_Click(chkAbort, e);
        }

    }
}
