using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IP9212_switch
{
    public enum externalCallType : byte { switchCall = 0, domeCall = 1 };
    
    public partial class SetupDialog : Form
    {
        externalCallType CalledType;
        ASCOM.DeviceInterface.IDomeV2 domeDriver;

        public SetupDialog(externalCallType calledType_ext = externalCallType.switchCall, ASCOM.DeviceInterface.IDomeV2 domeDriver_ext = null)
        {
            CalledType = calledType_ext;
            
            domeDriver = domeDriver_ext;
            
            InitializeComponent();
        }

        private void SetupDialog_Load(object sender, EventArgs e)
        {
            lblDriverInfo.Text = IP9212_switch_class.DriverInfo;
            if (CalledType == externalCallType.domeCall) {
                lblDriverInfo.Text = lblDriverInfo.Text + "\n\n" + domeDriver.DriverInfo;
            }

            ipaddr.Text = IP9212_switch_class.ip_addr;
            port.Text = IP9212_switch_class.ip_port;
            pass.Text = IP9212_switch_class.ip_pass;
            login.Text = IP9212_switch_class.ip_login;

            opened_port.Text = IP9212_switch_class.opened_sensor_port.ToString();
            opened_port_state_type.Checked = IP9212_switch_class.opened_port_state_type;

            closedstateport.Text = IP9212_switch_class.closed_sensor_port.ToString();
            closed_port_state_type.Checked = IP9212_switch_class.closed_port_state_type;

            switchport.Text = IP9212_switch_class.switch_roof_port.ToString();
            switch_port_type.Checked = IP9212_switch_class.switch_port_state_type;

            txtRoofPowerPort.Text = IP9212_switch_class.roofpower_port.ToString();
            txtHeatingSwitchPort.Text = IP9212_switch_class.heating_port.ToString();
            txtFocuserPowerSwitchPort.Text = IP9212_switch_class.focuser_power_port.ToString();
            txtTelescopePowerSwitchPort.Text = IP9212_switch_class.telescope_power_port.ToString();

            chkRoofPowerPortStateType.Checked = IP9212_switch_class.roofpower_port_state_type;
            chkHeatingSwitchPortStateType.Checked = IP9212_switch_class.heating_port_state_type;
            chkFocuserPowerPortStateType.Checked = IP9212_switch_class.focuser_power_port_state_type;
            chkTelescopePowerPortStateType.Checked = IP9212_switch_class.telescope_power_port_state_type;

            chkTelescopePowerPortStateType.Checked = IP9212_switch_class.telescope_power_port_state_type;

            chkTrace.Checked = IP9212_switch_class.traceState;

        }

        private void cmdOK_Click(object sender, EventArgs ev)
        {
            IP9212_switch_class.ip_addr = ipaddr.Text;
            IP9212_switch_class.ip_port = port.Text;
            IP9212_switch_class.ip_pass = pass.Text;
            IP9212_switch_class.ip_login = login.Text;

            try
            {
                IP9212_switch_class.opened_sensor_port = Convert.ToInt16(opened_port.Text);
            }
            catch (Exception e)
            {
                IP9212_switch_class.opened_sensor_port = Convert.ToInt16(IP9212_switch_class.opened_port_default);
                IP9212_switch_class.tl.LogMessage("SetupDialog_cmdOK", "Input string [opened_sensor_port] is not a sequence of digits [" + e.Message + "]");
            }

            try
            {
                IP9212_switch_class.closed_sensor_port = Convert.ToInt16(closedstateport.Text);
            }
            catch (Exception e)
            {
                IP9212_switch_class.closed_sensor_port = Convert.ToInt16(IP9212_switch_class.closed_port_default);
                IP9212_switch_class.tl.LogMessage("SetupDialog_cmdOK", "Input string [closed_sensor_port] is not a sequence of digits [" + e.Message + "]");
            }

            try
            {
                IP9212_switch_class.switch_roof_port = Convert.ToInt16(switchport.Text);
            }
            catch (Exception e)
            {
                IP9212_switch_class.switch_roof_port = Convert.ToInt16(IP9212_switch_class.switch_port_default);
                IP9212_switch_class.tl.LogMessage("SetupDialog_cmdOK", "Input string [switch_roof_port] is not a sequence of digits [" + e.Message + "]");
            }


            IP9212_switch_class.opened_port_state_type=opened_port_state_type.Checked;
            IP9212_switch_class.closed_port_state_type = closed_port_state_type.Checked;
            IP9212_switch_class.switch_port_state_type=switch_port_type.Checked;

            try
            {
                IP9212_switch_class.telescope_power_port = Convert.ToInt16(txtTelescopePowerSwitchPort.Text );
            }
            catch (Exception e)
            {
                IP9212_switch_class.telescope_power_port = Convert.ToInt16(IP9212_switch_class.telescope_power_port_default); 
                IP9212_switch_class.tl.LogMessage("SetupDialog_cmdOK", "Input string [telescope_power_port] is not a sequence of digits [" + e.Message + "]"); 
            }
            try
            {
                IP9212_switch_class.focuser_power_port = Convert.ToInt16(txtFocuserPowerSwitchPort.Text );
            }
            catch (Exception e)
            {
                IP9212_switch_class.focuser_power_port = Convert.ToInt16(IP9212_switch_class.focuser_power_port_default); 
                IP9212_switch_class.tl.LogMessage("SetupDialog_cmdOK", "Input string [focuser_power_port] is not a sequence of digits [" + e.Message + "]"); }
            try
            {
                IP9212_switch_class.heating_port = Convert.ToInt16(txtHeatingSwitchPort.Text );
            }
            catch (Exception e)
            {
                IP9212_switch_class.heating_port = Convert.ToInt16(IP9212_switch_class.heating_port_default);
                IP9212_switch_class.tl.LogMessage("SetupDialog_cmdOK", "Input string [heating_port] is not a sequence of digits [" + e.Message + "]");
            }
            try
            {
                IP9212_switch_class.roofpower_port = Convert.ToInt16(txtRoofPowerPort.Text);
            }
            catch (Exception e)
            {
                IP9212_switch_class.roofpower_port = Convert.ToInt16(IP9212_switch_class.roof_power_port_default); 
                IP9212_switch_class.tl.LogMessage("SetupDialog_cmdOK", "Input string [roofpower_port] is not a sequence of digits [" + e.Message + "]"); 
            }


            IP9212_switch_class.roofpower_port_state_type=chkRoofPowerPortStateType.Checked ;
            IP9212_switch_class.heating_port_state_type=chkHeatingSwitchPortStateType.Checked ;
            IP9212_switch_class.focuser_power_port_state_type=chkFocuserPowerPortStateType.Checked ;
            IP9212_switch_class.telescope_power_port_state_type=chkTelescopePowerPortStateType.Checked ;
            
            IP9212_switch_class.traceState=chkTrace.Checked ;
        }

        private void BrowseToAscom(object sender, EventArgs e) // Click on ASCOM logo event handler
        {
            try
            {
                System.Diagnostics.Process.Start("http://ascom-standards.org/");
            }
            catch (System.ComponentModel.Win32Exception noBrowser)
            {
                if (noBrowser.ErrorCode == -2147467259)
                    MessageBox.Show(noBrowser.Message);
            }
            catch (System.Exception other)
            {
                MessageBox.Show(other.Message);
            }
        }

        private void BrowseToAstromania(object sender, EventArgs e) // Click on ASCOM logo event handler
        {
            try
            {
                System.Diagnostics.Process.Start("http://astromania.info/");
            }
            catch (System.ComponentModel.Win32Exception noBrowser)
            {
                if (noBrowser.ErrorCode == -2147467259)
                    MessageBox.Show(noBrowser.Message);
            }
            catch (System.Exception other)
            {
                MessageBox.Show(other.Message);
            }
        }

    }
}
