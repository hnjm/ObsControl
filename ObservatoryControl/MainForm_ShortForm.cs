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
        private void UpdateShortPannelButtonsStatus()
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


        }







        // ShortForm interface events
        #region //// ShortForm interface events  //////////////////////////////////////////////////

        private void chkPower_Click(object sender, EventArgs e)
        {
            if (((CheckBox)sender).BackColor != OnColor)
            {
                ObsControl.CommandParser.ParseSingleCommand("POWER_ON");
            }
            else
            {
                ObsControl.CommandParser.ParseSingleCommand("POWER_OFF");
            }
        }

        private void chkMaxim_Click(object sender, EventArgs e)
        {
            if (((CheckBox)sender).BackColor != OnColor)
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

        #endregion // ShortForm interface events ////////////////////////////////////////////////
        // End of ShortForm interface events





    }
}
