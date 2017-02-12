using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Reflection;
using System.Globalization;
using System.Threading;
using System.Resources;
using System.Configuration;
using System.Xml;

namespace ObservatoryCenter
{
    ///////////////////////////////////////////
    // How to add new settigs:
    // 1. Add form element
    // 2. Link element through Data/ApplicationSetting/text (or checked, or ...) to setting provider element
    // 3. Add writing from element to var in btnOk_Click event
    // 4. Add to LoadParams() in MainForm initial var setting from setting provider
    // 
    // For combobox with selectindex
    // 2. DO NOT MAKE LINKING TO Application/Settings
    // 3.1 Add writting from element to var in btnOk_Click event
    // 3.2 Add writting from element to Properties.Settings.Default.####
    // 4. Add to LoadParams() in MainForm initial var setting from setting provider
    // 5. Add to SettingsForm.Show() initializing combobox selectedindex from var
    ///////////////////////////////////////////        
    public partial class SettingsForm : Form
    {
        private MainForm ParentMainForm;
        ResourceManager LocRM;

        private double TempRoofDuration=0;

        public SettingsForm(MainForm MF)
        {
            ParentMainForm = MF;
            //LocRM = new ResourceManager("WeatherStation.WinFormStrings", Assembly.GetExecutingAssembly()); //create resource manager
            InitializeComponent(); 
        }


        private void SettingsForm_Load(object sender, EventArgs e)
        {

            //Load XML into grid
            LoadDataIntoGrids("programsPath"); //dataGridConfig_programsPath_name
            LoadDataIntoGrids("scenarioMainParams"); //dataGridConfig_scenarioMainParams_name




            //Workaround about "Controls contained in a TabPage are not created until the tab page is shown, and any data bindings in these controls are not activated until the tab page is shown."
            foreach (TabPage tp in tabSettings.TabPages)
            {
                tp.Show();
            }
            TempRoofDuration = ParentMainForm.RoofDuration;

        }


        //Prevent from unloading the form
        private void SettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }


        private IEnumerable<Control> GetAllDataGrdiControls(Control container)
        {
            List<Control> controlList = new List<Control>();
            foreach (Control c in container.Controls)
            {
                controlList.AddRange(GetAllDataGrdiControls(c));
                if (c is DataGridView)
                    controlList.Add(c);
            }
            return controlList;
        }

        /// <summary>
        /// Load settings from XML into GRID
        /// </summary>
        /// <param name="SectionName">Name of the XML SECTION to load. If not specified - try current tab. If specify "All" = reloads all nodes</param>
        private void LoadDataIntoGrids(string SectionName = "All")
        {
            //If paramter was empty, then try current
            if (SectionName==String.Empty)
            {
                SectionName = tabSettings.SelectedTab.Name;
            }

            //Get all grids
            var configGridsCollection = GetAllDataGrdiControls(this);
            DataGridView curDataGrid=null;

            try
            {
                XmlNode xmlSections = ObsConfig.configXML.SelectSingleNode("configuration");
                
                //Loop throug all sections (all because it is possible to load all or only specific)
                foreach (System.Xml.XmlNode xSection in xmlSections)
                {
                    if (SectionName == "All" || xSection.Name==SectionName)
                    {
                        //Current NODE to parse and load into grid
                        XmlNode xnlNodes = ObsConfig.configXML.SelectSingleNode("//" + xSection.Name);

                        //Get current grid
                        foreach (DataGridView DataGridEl in configGridsCollection)
                        {
                            if (DataGridEl.Name == "dataGridConfig_" + xnlNodes.Name) 
                                curDataGrid = DataGridEl;
                        }

                        //EmpryGrid (for case, where we want to reload data
                        curDataGrid.Rows.Clear();

                        //Loop throgh all data nodes
                        int curRowIndex = 0;
                        foreach (XmlNode xndNode in xnlNodes.ChildNodes)
                        {
                            //Read data
                            string name = xndNode.Name;
                            string val = xndNode.Attributes["value"].Value; ;

                            //Add data row to to grid
                            curRowIndex = curDataGrid.Rows.Add();
                            curDataGrid.Rows[curRowIndex].Cells["dataGridConfig_" + xnlNodes.Name + "_name"].Value = name;
                            curDataGrid.Rows[curRowIndex].Cells["dataGridConfig_" + xnlNodes.Name + "_value"].Value = val;
                        }
                    }
                }
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

                string FullMessage = "Exception when loading CONFIG XML sections" + Environment.NewLine;
                FullMessage += Environment.NewLine + Environment.NewLine + "Debug information:" + Environment.NewLine + "IOException source: " + ex.Data + " " + ex.Message
                        + Environment.NewLine + Environment.NewLine + messstr;
                MessageBox.Show(this, FullMessage, "Invalid value", MessageBoxButtons.OK);

                Logging.AddLog(FullMessage, LogLevel.Important, Highlight.Error);
            }
        }


        /// <summary>
        /// Load settings from XML into GRID
        /// </summary>
        /// <param name="SectionName">Name of the XML SECTION to load. If not specified - try current tab. If specify "All" = reloads all nodes</param>
        private void SaveDataFromGrid()
        {
            //Get all grids
            var configGridsCollection = GetAllDataGrdiControls(this);
            string sectionName = "";
            string curName = "",curValue = "";

            try
            {
                XmlNode xmlSections = ObsConfig.configXML.SelectSingleNode("configuration");


                //Loop through all grids
                foreach (DataGridView curDataGrid in configGridsCollection)
                {
                    sectionName = curDataGrid.Name.Substring(15);

                    //Loop through grid
                    foreach (DataGridViewRow curDataGridRow in curDataGrid.Rows)
                    {
                        curName = curDataGridRow.Cells[curDataGrid.Name + "_name"].Value.ToString();
                        curValue = curDataGridRow.Cells[curDataGrid.Name + "_value"].Value.ToString();

                        //Current NODE to parse and load into grid
                        XmlNode xnlNodes = ObsConfig.configXML.SelectSingleNode("//" + sectionName);
                        int curRowIndex = 0;

                        foreach (XmlNode xndNode in xnlNodes.ChildNodes)
                        {
                            //Update data
                            if (xndNode.Name == curName)
                            {
                                xndNode.Attributes["value"].Value = curValue;
                            }

                        }
                    }
                }
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

                string FullMessage = "Exception when loading CONFIG XML sections" + Environment.NewLine;
                FullMessage += Environment.NewLine + Environment.NewLine + "Debug information:" + Environment.NewLine + "IOException source: " + ex.Data + " " + ex.Message
                        + Environment.NewLine + Environment.NewLine + messstr;
                MessageBox.Show(this, FullMessage, "Invalid value", MessageBoxButtons.OK);

                Logging.AddLog(FullMessage, LogLevel.Important, Highlight.Error);
            }
        }


        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
/*                
                ParentMainForm.ObsControl.MaximDLPath = Properties.Settings.Default.MaximDLPath;
                ParentMainForm.ObsControl.CCDAPPath = Properties.Settings.Default.CCDAPPath;
                ParentMainForm.ObsControl.PlanetariumPath = Properties.Settings.Default.CartesPath;

                //driver id
                ParentMainForm.ObsControl.DOME_DRIVER_NAME = Properties.Settings.Default.DomeDriverId;
                ParentMainForm.ObsControl.TELESCOPE_DRIVER_NAME = Properties.Settings.Default.TelescopeDriverId;
                ParentMainForm.ObsControl.SWITCH_DRIVER_NAME = Properties.Settings.Default.SwitchDriverId;

                //switch settings
                ParentMainForm.ObsControl.POWER_MOUNT_PORT = Convert.ToByte(Properties.Settings.Default.SwitchMountPort);
                ParentMainForm.ObsControl.POWER_CAMERA_PORT = Convert.ToByte(Properties.Settings.Default.SwitchCameraPort);
                ParentMainForm.ObsControl.POWER_FOCUSER_PORT = Convert.ToByte(Properties.Settings.Default.SwitchFocuserPort);
                ParentMainForm.ObsControl.POWER_ROOFPOWER_PORT = Convert.ToByte(Properties.Settings.Default.SwitchRoofPowerPort);
                ParentMainForm.ObsControl.POWER_ROOFSWITCH_PORT = Convert.ToByte(Properties.Settings.Default.SwitchRoofSwitchPort);

                ParentMainForm.RoofDuration = Convert.ToInt16(Properties.Settings.Default.RoofDuration);
 */

                if (txtSwitchDriverId.Text != ParentMainForm.ObsControl.SWITCH_DRIVER_NAME)
                {
                    ParentMainForm.ObsControl.SWITCH_DRIVER_NAME = txtSwitchDriverId.Text;
                    ParentMainForm.ObsControl.resetSwitch();
                }

                if (txtDomeDriverId.Text != ParentMainForm.ObsControl.DOME_DRIVER_NAME)
                {
                    ParentMainForm.ObsControl.DOME_DRIVER_NAME = txtDomeDriverId.Text;
                    ParentMainForm.ObsControl.resetDome();
                }

                if (txtTelescopeDriverId.Text != ParentMainForm.ObsControl.TELESCOPE_DRIVER_NAME)
                {
                    ParentMainForm.ObsControl.TELESCOPE_DRIVER_NAME = txtTelescopeDriverId.Text;
                    ParentMainForm.ObsControl.resetTelescope();
                }

                //reset automatic duration count if duration was manually changed
                if (TempRoofDuration != ParentMainForm.RoofDuration) { Properties.Settings.Default.RoofDurationMeasurementsCount = 1; } 

                //Commit changes
                Properties.Settings.Default.Save();
                
                //Load params into vars
                ParentMainForm.LoadParams();


                //Update XML
                SaveDataFromGrid();
                //Write config file to disk
                ObsConfig.Save();

                this.Close();
            }
            catch (FormatException ex)
            {
                StackTrace st = new StackTrace(ex, true);
                StackFrame[] frames = st.GetFrames();
                string messstr = "";

                // Iterate over the frames extracting the information you need
                foreach (StackFrame frame in frames)
                {
                    messstr += String.Format("{0}:{1}({2},{3})", frame.GetFileName(), frame.GetMethod().Name, frame.GetFileLineNumber(), frame.GetFileColumnNumber());
                }

                string FullMessage = "Some of the fields has invalid values" + Environment.NewLine;
                FullMessage += Environment.NewLine + "Hint: look for incorrect decimal point ( \".\" instead of \",\" ) or a accidential letter in textbox";
                FullMessage += Environment.NewLine + "Hint 2: clicking in every field could help";
                FullMessage += Environment.NewLine + Environment.NewLine + "Debug information:" + Environment.NewLine + "IOException source: " + ex.Data + " " + ex.Message
                        + Environment.NewLine + Environment.NewLine + messstr;
                MessageBox.Show(this, FullMessage, "Invalid value", MessageBoxButtons.OK);

                Logging.AddLog(FullMessage,LogLevel.Important,Highlight.Error);
            }


        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Reload(); 
        }

        private void btnRestoreDefaults_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to reset all settings to their default values (this can't be undone)?", "Reset to default values", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                //for classic settings, at least до тех пор, пока полностью не перейдем на CUSTOM XML
                Properties.Settings.Default.Reset();

                //Загрузить из файла
                ObsConfig.Load();
                //Перерисовать
                LoadDataIntoGrids("All");

            }
        }


        private void btnChooseSwitch_Click(object sender, EventArgs e)
        {
            txtSwitchDriverId.Text = ASCOM.DriverAccess.Switch.Choose(Properties.Settings.Default.SwitchDriverId);
        }

        private void btnConnectSwitchSettings_Click(object sender, EventArgs e)
        {
            ParentMainForm.ObsControl.SWITCH_DRIVER_NAME = txtSwitchDriverId.Text;
            ParentMainForm.ObsControl.resetSwitch();
            ParentMainForm.ObsControl.connectSwitch = true;
            ParentMainForm.CheckPowerSwitchStatus_caller();
        }

        private void btnChooseDome_Click(object sender, EventArgs e)
        {
            txtDomeDriverId.Text = ASCOM.DriverAccess.Dome.Choose(Properties.Settings.Default.DomeDriverId);
        }

        private void btnChooseTelescope_Click(object sender, EventArgs e)
        {
            txtTelescopeDriverId.Text = ASCOM.DriverAccess.Telescope.Choose(Properties.Settings.Default.TelescopeDriverId);

        }

        private void btnConnectTelescopeSettings_Click(object sender, EventArgs e)
        {
            ParentMainForm.ObsControl.TELESCOPE_DRIVER_NAME = txtTelescopeDriverId.Text;
            ParentMainForm.ObsControl.resetTelescope();
            ParentMainForm.ObsControl.connectMount = true;
        }

        private void btnConnectDomeSettings_Click(object sender, EventArgs e)
        {
            ParentMainForm.ObsControl.DOME_DRIVER_NAME = txtDomeDriverId.Text;
            ParentMainForm.ObsControl.resetDome();
            ParentMainForm.ObsControl.connectDome = true;
        }

    }
}
