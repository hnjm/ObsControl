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

namespace ObservatoryCenter
{
    /// <summary>
    /// Events handler for VISUAL ELEMENTS OF MAIN FORM
    ///  Contains events hadnlers of some controls
    /// </summary>
    public partial class MainForm
    {
        /// <summary>
        /// Update short pannel application status
        /// </summary>
        private void UpdateShortPanelButtonsStatus()
        {
            //RoofStatus
            if (ObsControl.ASCOMDome.Connected_flag)
            {
                chkRoof.Checked = true;
                if (ObsControl.ASCOMDome.curShutterStatus == ShutterState.shutterOpen)
                {
                    chkRoof.BackColor = OnColor;
                }
                else
                {
                    chkRoof.BackColor = OffColor;
                }
            }
            else
            {
                chkRoof.Checked = false;
                chkRoof.BackColor = DefBackColor;
            }


            //Power buttons
            if (ObsControl.ASCOMSwitch.Connected_flag)
            {
                chkPower.Checked = true;
                if (ObsControl.ASCOMSwitch.Telescope_power_flag == true)
                {
                    chkPower.BackColor = OnColor;
                }
                else
                {
                    chkPower.BackColor = OffColor;
                }
            }
            else
            {
                chkPower.Checked = false;
                chkPower.BackColor = DefBackColor;
            }



            //Maxim DL status
            Color tmpMaximColor = new Color();
            if (ObsControl.objMaxim.IsRunning())
            {
                chkMaxim.Checked = true;
                tmpMaximColor = InterColor;
                if (ObsControl.objMaxim.CameraConnected)
                {
                    if (ObsControl.objMaxim.TelescopeConnected)
                    {
                        tmpMaximColor = OnColor;
                    }
                }
            }
            else
            {
                chkMaxim.Checked = false;
                tmpMaximColor = OffColor;
            }
            chkMaxim.BackColor = tmpMaximColor;


            //Cooler Status
            //if (ObsControl.objMaxim.IsRunning())
            //{
            //    if (ObsControl.objMaxim.CameraConnected)
            //    {
            //        //Parameters
            //        txtShortTemp.Text = String.Format("{0:0.0}", ObsControl.objMaxim.CameraTemp);
            //        txtShortSetPoint.Text = String.Format("{0:0}", ObsControl.objMaxim.CameraSetPoint);
            //        txtShortPower.Text = String.Format("{0:0}%", ObsControl.objMaxim.CameraCoolerPower);

            //        if (ObsControl.objMaxim.CameraCoolerOnStatus)
            //        {
            //            chkCoolerFlag.Checked = true;

            //            if (ObsControl.objMaxim.CameraWarmpUpNow)
            //            {
            //                chkCoolerFlag.BackColor = InterColor;
            //            }
            //            else
            //            {
            //                chkCoolerFlag.BackColor = OnColor;
            //            }

            //            //Check temp is set?
            //            if (!ObsControl.objMaxim.checkTempNearSetpoint())
            //            {
            //                txtShortTemp.BackColor = OffColor;
            //                panelShortTemp.BackColor = OffColor;
            //            }
            //            else
            //            {
            //                txtShortTemp.BackColor = DefBackColor;
            //                panelShortTemp.BackColor = DefBackColor;
            //            }

            //            //Check power is to high?
            //            if (ObsControl.objMaxim.CameraCoolerPower >= 99.0)
            //            {
            //                txtShortPower.BackColor = OffColor;
            //                panelShortPower.BackColor = OffColor;
            //            }
            //            else
            //            {
            //                txtShortPower.BackColor = DefBackColor;
            //                panelShortPower.BackColor = DefBackColor;
            //            }

            //        }
            //        else
            //        {
            //            chkCoolerFlag.Checked = false;
            //            chkCoolerFlag.BackColor = OffColor;
            //        }
            //    }
            //    else
            //    {
            //        txtShortTemp.Text = "";
            //        txtShortSetPoint.Text = String.Format("{0:0}", ObsControl.objMaxim.TargetCameraSetTemp);
            //        txtShortPower.Text = "";

            //        //short form
            //        chkCoolerFlag.Checked = false;
            //        chkCoolerFlag.BackColor = DefBackColor;
            //    }

            //}
            //else
            //{
            //    txtShortPower.BackColor = DefBackColor;
            //    panelShortPower.BackColor = DefBackColor;
            //    txtShortTemp.BackColor = DefBackColor;
            //    panelShortTemp.BackColor = DefBackColor;

            //    txtShortTemp.Text = "";
            //    txtShortSetPoint.Text = "";
            //    txtShortPower.Text = "";

            //    //short form
            //    chkCoolerFlag.Checked = false;
            //    chkCoolerFlag.BackColor = DefBackColor;
            //}



            //PHD status
            Color tmpPHDColor = new Color();
            if (ObsControl.objPHD2App.IsRunning())
            {
                chkPHD.Checked = true;
                tmpPHDColor = InterColor;
                if (ObsControl.objPHDBrokerApp.IsRunning())
                {
                    bool res = ObsControl.objPHD2App.EquipmentConnected;
                    if (res)
                    {
                        tmpPHDColor = OnColor;
                    }
                }
            }
            else
            {
                chkPHD.Checked = false;
                tmpPHDColor = OffColor;
            }
            chkPHD.BackColor = tmpPHDColor;



            //CCDC status
            Color tmpCCDCColor = new Color();
            if (ObsControl.objCCDCApp.IsRunning())
            {
                chkCCDC.Checked = true;
                tmpCCDCColor = OnColor;
            }
            else
            {
                chkCCDC.Checked = false;
                tmpCCDCColor = OffColor;
            }
            chkCCDC.BackColor = tmpCCDCColor;


            //Telescope park & track status
            if (ObsControl.ASCOMTelescope.Connected_flag)
            {
                if (ObsControl.ASCOMTelescope.curAtPark)
                {
                    //btnPark.Text = "Parked";
                    chkMountPark.BackColor = OffColor;
                }
                else
                {
                    //btnPark.Text = "UnParked";
                    chkMountPark.BackColor = OnColor;
                }

                if (ObsControl.ASCOMTelescope.curTracking)
                {
                    //btnTrack.Text = "Parked";
                    chkMountTrack.BackColor = OnColor;
                }
                else
                {
                    //btnTrack.Text = "UnParked";
                    chkMountTrack.BackColor = OffColor;
                }

                txtShortAz.Text = String.Format("{0:0}", ObsControl.ASCOMTelescope.curAzimuth);
                txtShortAlt.Text = String.Format("{0:0}", ObsControl.ASCOMTelescope.curAltitude);
                if (ObsControl.ASCOMTelescope.curSlewing)
                {
                    txtShortAz.BackColor = InterColor;
                    txtShortAlt.BackColor = InterColor;
                }
                else
                {
                    txtShortAz.BackColor = DefBackColorTextBoxes;
                    txtShortAlt.BackColor = DefBackColorTextBoxes;
                }
            }
            else
            {
                chkMountPark.BackColor = DefBackColor;
                chkMountTrack.BackColor = DefBackColor;
                txtShortAz.Text = "";
                txtShortAlt.Text = "";
            }

            //IQP status
            if (IQP_monitorTimer)
            {
                chkShort_IQP.BackColor = OnColor;
                chkShort_IQP.Checked = true;
            }
            else
            {
                chkShort_IQP.BackColor = DefBackColor;
                chkShort_IQP.Checked = false;
            }
        }

        // ShortForm interface events
            #region //// ShortForm interface events  //////////////////////////////////////////////////

        private void chkPower_Click(object sender, EventArgs e)
        {
            if (((CheckBox)sender).BackColor != OnColor)
            {
                ObsControl.CommandParser.ParseSingleCommand2("POWER_ON");
            }
            else
            {
                ObsControl.CommandParser.ParseSingleCommand2("POWER_OFF");
            }
        }

        private void chkMaxim_Click(object sender, EventArgs e)
        {
            //always can press
            //because COM obj and real Maxim live in diffrent way
            if (((CheckBox)sender).BackColor != OnColor || true) 
            {
                LinkLabelLinkClickedEventArgs dummy = new LinkLabelLinkClickedEventArgs(linkMaximDL.Links[0]);
                linkMaximDL_LinkClicked(sender, dummy);
            }
        }

        private void chkPHD_Click(object sender, EventArgs e)
        {
            if (((CheckBox)sender).BackColor != OnColor)
            {
                LinkLabelLinkClickedEventArgs dummy;

                //run php2 && connect equipment
                dummy = new LinkLabelLinkClickedEventArgs(linkPHD2.Links[0]);
                linkPHD2_LinkClicked(sender, dummy);

                //run broker
                dummy = new LinkLabelLinkClickedEventArgs(linkPHDBroker.Links[0]);
                linkPHDBroker_LinkClicked(sender, dummy);

            }

        }

        private void chkCCDC_Click(object sender, EventArgs e)
        {
            if (!((CheckBox)sender).Checked && ((CheckBox)sender).BackColor != OnColor)
            {
                LinkLabelLinkClickedEventArgs dummy = new LinkLabelLinkClickedEventArgs(linkCCDC.Links[0]);
                linkCCDC_LinkClicked(sender, dummy);
            }

        }

        private void chkCoolerFlag_Click(object sender, EventArgs e)
        {
            if (ObsControl.objMaxim.IsRunning() && ObsControl.objMaxim.CameraConnected)
            {
                if  (!ObsControl.objMaxim.CameraCoolerOnStatus)
                {
                    ObsControl.objMaxim.CameraCoolingOn(); //switch on
                }
                else if (ObsControl.objMaxim.CameraCoolerOnStatus && ObsControl.objMaxim.CameraWarmpUpNow)
                {
                    ObsControl.objMaxim.CameraCoolingOff(); //switch off cooler
                }
                else if (ObsControl.objMaxim.CameraCoolerOnStatus && ObsControl.objMaxim.checkTempNearSetpoint())
                {
                    ObsControl.objMaxim.CameraCoolingOff(true); //warmup
                }
            }
        }

        private void chkMountPark_Click(object sender, EventArgs e)
        {
            btnPark_Click(sender, e);
        }
        private void chkMountTrack_Click(object sender, EventArgs e)
        {
            btnTrack_Click(sender, e);
        }


        private void chkRun_Click(object sender, EventArgs e)
        {
            if (!((CheckBox)sender).Checked)
            {
                //redirect to start button
                btnStartAll_Click(sender, e);
            }
        }


        /// <summary>
        /// Pause action
        /// </summary>
        private void chkPause_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {
                ((CheckBox)sender).Checked = false;
                ((CheckBox)sender).BackColor = DefBackColor;

                //Switch off abort button
                chkAbort.BackColor = DefBackColor;
                btnEmergencyStop.BackColor = DefBackColor;

                //Resume run
                ObsControl.CommandParser.ParseSingleCommand2("IMAGING_RUN_RESUME",true);

                //toggle main window state
                btnSoftStop.BackColor = DefBackColor;
            }
            else
            {
                ((CheckBox)sender).Checked = true;
                ((CheckBox)sender).BackColor = OffColor;

                //Pause and park
                ObsControl.CommandParser.ParseSingleCommand2("IMAGING_RUN_PAUSE",true);

                //toggle main window state
                btnSoftStop.BackColor = OffColor;
            }
        }


        /// <summary>
        /// Abort action
        /// </summary>
        private void chkAbort_Click(object sender, EventArgs e)
        {
            ((CheckBox)sender).BackColor = OffColor;
            btnEmergencyStop.BackColor = OffColor;

            //toogle pause buttons
            chkPause.Checked = false;
            chkPause_CheckedChanged(chkPause, e);
            ObsControl.CommandParser.ParseSingleCommand2("IMAGING_RUN_ABORT",true);
        }

        private void chkKill_Click(object sender, EventArgs e)
        {
            btnKILL_Click(sender, e);
        }



        #endregion // ShortForm interface events ////////////////////////////////////////////////
        // End of ShortForm interface events





    }
}
