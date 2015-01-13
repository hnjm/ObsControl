
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Globalization;
using System.Collections;
using System.Windows.Forms;

using ASCOM;
using ASCOM.Utilities;
using ASCOM.DeviceInterface;

namespace IP9212_switch
{
    [ComVisible(false)]					// Form not registered for COM!
    public partial class SetupDialogForm : Form
    {
        public SetupDialogForm()
        {
            InitializeComponent();
            ipaddr.Text = Properties.Settings.Default.ip_addr;
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.ip_addr = ipaddr.Text;

            using(ASCOM.Utilities.Profile p = new Utilities.Profile())
            {
                p.DeviceType = "Dome";
                p.WriteValue(Dome.driverID, "ip_addr222", (string)ipaddr.Text);
            }

            Close();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BrowseToAscom(object sender, EventArgs e)
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


    }
}