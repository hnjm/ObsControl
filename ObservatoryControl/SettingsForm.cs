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
    // 3. Add to LoadParams() in MainForm initial var setting from setting provider
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
            LoadDataIntoGrids("scenarioMain"); //dataGridConfig_scenarioMainParams_name




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
        /// 
        /*
            TYPE:
                "command" or omitted    - this is command (will try to run)
                type="parameter"        - this is parameter

            RUN:
                "true" or omitted = run command
                "false" = not run

            example:
            <scenarioMain>
                <POWER_ON type="command" run="true" />
                <PHD2_RUN type="command" run="false" />
                <WAIT argument="2000" />
               	<TTC_RUN type="command" run="true" />
	            <TTC_FANAUTO_ON type="command" run="true" />
	            <TTC_HEATERAUTO_ON type="command" run="true" />
            </scenarioMain>
         */
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
                XmlNode xmlSections = ConfigManagement.configXML.SelectSingleNode("configuration");
                
                //Loop throug all sections (all because it is possible to load all or only specific)
                foreach (System.Xml.XmlNode xSection in xmlSections)
                {
                    if (SectionName == "All" || xSection.Name==SectionName)
                    {
                        //Current NODE to parse and load into grid
                        XmlNode xnlNodes = ConfigManagement.configXML.SelectSingleNode("//" + xSection.Name);

                        //Get grid name, corresponding to current section name
                        foreach (DataGridView DataGridEl in configGridsCollection)
                        {
                            if (DataGridEl.Name == "dataGridConfig_" + xnlNodes.Name) 
                                curDataGrid = DataGridEl;
                        }

                        //clear grid data (for case, where we want to reload data)
                        curDataGrid.Rows.Clear();

                        //Loop throgh all data nodes and load it into grid
                        int curRowIndex = 0;
                        foreach (XmlNode xndNode in xnlNodes.ChildNodes)
                        {
                            //Read data
                            //1. Tag Name (mandatory field!)
                            string name = xndNode.Name;

                            //2. Tag attributes
                            //2.1. type
                            string type_val = "";
                            if (xndNode.Attributes != null && xndNode.Attributes["type"] != null)
                            {
                                type_val = xndNode.Attributes["type"].Value;
                            }
                            else
                            {
                                type_val = "command";
                            }

                            //2.2. run
                            string run_val = (type_val == "command" ? "true" : "false");
                            if (xndNode.Attributes != null && xndNode.Attributes["run"] != null)
                            {
                                run_val = xndNode.Attributes["run"].Value;
                            }

                            //2.3. argument
                            string argument_val = "";
                            if (xndNode.Attributes != null && xndNode.Attributes["argument"] != null)
                            {
                                argument_val = xndNode.Attributes["argument"].Value;
                            }

                            //2.4. value
                            string val = "";
                            if (xndNode.Attributes != null && xndNode.Attributes["value"] != null)
                            {
                                val = xndNode.Attributes["value"].Value; 
                            }

                            //Add data row to to grid
                            curRowIndex = curDataGrid.Rows.Add();
                            curDataGrid.Rows[curRowIndex].Cells["dataGridConfig_" + xnlNodes.Name + "_name"].Value = name;

                            if (curDataGrid.Columns.Contains("dataGridConfig_" + xnlNodes.Name + "_value"))
                            {
                                if (val!="") curDataGrid.Rows[curRowIndex].Cells["dataGridConfig_" + xnlNodes.Name + "_value"].Value = val;
                                //argument will write to value
                                if (argument_val != "") curDataGrid.Rows[curRowIndex].Cells["dataGridConfig_" + xnlNodes.Name + "_value"].Value = argument_val;
                            }
                            if (curDataGrid.Columns.Contains("dataGridConfig_" + xnlNodes.Name + "_argument"))
                            {
                                curDataGrid.Rows[curRowIndex].Cells["dataGridConfig_" + xnlNodes.Name + "_argument"].Value = argument_val;
                            }
                            if (curDataGrid.Columns.Contains("dataGridConfig_" + xnlNodes.Name + "_run"))
                            {
                                curDataGrid.Rows[curRowIndex].Cells["dataGridConfig_" + xnlNodes.Name + "_run"].Value = run_val;
                            }
                            if (curDataGrid.Columns.Contains("dataGridConfig_" + xnlNodes.Name + "_type"))
                            {
                                curDataGrid.Rows[curRowIndex].Cells["dataGridConfig_" + xnlNodes.Name + "_type"].Value = type_val;
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


        /// <summary>
        /// Load settings from XML into GRID
        /// </summary>
        /// <param name="SectionName">Name of the XML SECTION to load. If not specified - try current tab. If specify "All" = reloads all nodes</param>
        /// ВАЖНО! При записи новые узлы не создаются, а только правятся их аттрибуты!
        private void SaveDataFromGrid()
        {
            //Get all grids
            var configGridsCollection = GetAllDataGrdiControls(this);

            string sectionName = "", curDataGridName = "";
            string curName = "",curValue = "", curRunFlag= "falses", curType="", curArgument="";

            try
            {
                //XmlNode xmlSections = ConfigManagement.configXML.SelectSingleNode("configuration");

                //Перебираем все Gridы. И будем искать соответствующие им секции
                foreach (DataGridView curDataGrid in configGridsCollection)
                {
                    //XML section name to write to
                    sectionName = curDataGrid.Name.Substring(15);
                    curDataGridName = curDataGrid.Name;

                    //Перебираем записи текущего Grid'а
                    foreach (DataGridViewRow curDataGridRow in curDataGrid.Rows)
                    {
                        curName = ""; curValue = ""; curRunFlag = "false"; curType = "command"; curArgument = "";

                        // Имя узла                        
                        curName = curDataGridRow.Cells[curDataGrid.Name + "_name"].Value.ToString();

                        // Аттрибут Value (если есть)
                        if (curDataGrid.Columns.Contains(curDataGrid.Name + "_value"))
                        {
                            curValue = curDataGridRow.Cells[curDataGrid.Name + "_value"].Value.ToString();
                        }
                        // Аттрибут Argument (если есть)
                        if (curDataGrid.Columns.Contains(curDataGrid.Name + "_argument"))
                        {
                            //dataGridConfig_scenarioMain_argument
                            curArgument = curDataGridRow.Cells[curDataGrid.Name + "_argument"].Value.ToString();
                        }
                        // Аттрибут Run (если есть)
                        if (curDataGrid.Columns.Contains(curDataGrid.Name + "_run"))
                        {
                            curRunFlag=curDataGridRow.Cells[curDataGrid.Name + "_run"].Value.ToString();
                        }
                        // Аттрибут Type (если есть)
                        if (curDataGrid.Columns.Contains(curDataGrid.Name + "_type"))
                        {
                            curType =curDataGridRow.Cells[curDataGrid.Name + "_type"].Value.ToString();
                        }

                        // Получить перечень Узлов из текущей секции
                        XmlNode xnlNodes = ConfigManagement.configXML.SelectSingleNode("//" + sectionName);
                        // Перебрать их всех и поправить аттрибуты для текущего
                        foreach (XmlNode xndNode in xnlNodes.ChildNodes)
                        {
                            //Update data
                            if (xndNode.Name == curName)
                            {
                                if (curValue != "")
                                {
                                    XmlAttribute att = ConfigManagement.configXML.CreateAttribute("value");
                                    att.Value = curValue;
                                    xndNode.Attributes.SetNamedItem(att);
                                }
                                if (curArgument != "")
                                {
                                    //xndNode.Attributes["argument"].Value = curArgument;
                                    XmlAttribute att = ConfigManagement.configXML.CreateAttribute("argument");
                                    att.Value = curArgument;
                                    xndNode.Attributes.SetNamedItem(att);
                                }
                                if (curRunFlag != "")
                                {
                                    //xndNode.Attributes["run"].Value = curRunFlag;
                                    XmlAttribute att = ConfigManagement.configXML.CreateAttribute("run");
                                    att.Value = curRunFlag;
                                    xndNode.Attributes.SetNamedItem(att);
                                }
                                if (curType != "")
                                {
                                    //xndNode.Attributes["type"].Value = curType;
                                    XmlAttribute att = ConfigManagement.configXML.CreateAttribute("type");
                                    att.Value = curType;
                                    xndNode.Attributes.SetNamedItem(att);
                                }
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
                FullMessage += Environment.NewLine + "Section name: " + sectionName + ", xmlNode: " + curName + ", grid: " + curDataGridName;


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

                if (txtSwitchDriverId.Text != ParentMainForm.ObsControl.ASCOMSwitch.DRIVER_NAME)
                {
                    ParentMainForm.ObsControl.ASCOMSwitch.DRIVER_NAME = txtSwitchDriverId.Text;
                    ParentMainForm.ObsControl.ASCOMSwitch.Reset();
                }

                if (txtDomeDriverId.Text != ParentMainForm.ObsControl.ASCOMDome.DOME_DRIVER_NAME)
                {
                    ParentMainForm.ObsControl.ASCOMDome.DOME_DRIVER_NAME = txtDomeDriverId.Text;
                    ParentMainForm.ObsControl.ASCOMDome.resetDome();
                }

                if (txtTelescopeDriverId.Text != ParentMainForm.ObsControl.ASCOMTelescope.TELESCOPE_DRIVER_NAME)
                {
                    ParentMainForm.ObsControl.ASCOMTelescope.TELESCOPE_DRIVER_NAME = txtTelescopeDriverId.Text;
                    ParentMainForm.ObsControl.ASCOMTelescope.Reset();
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
                ConfigManagement.Save();

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
                ConfigManagement.Load();
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
            ParentMainForm.ObsControl.ASCOMSwitch.DRIVER_NAME = txtSwitchDriverId.Text;
            ParentMainForm.ObsControl.ASCOMSwitch.Reset();
            ParentMainForm.ObsControl.ASCOMSwitch.Connect = true;
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
            ParentMainForm.ObsControl.ASCOMTelescope.TELESCOPE_DRIVER_NAME = txtTelescopeDriverId.Text;
            ParentMainForm.ObsControl.ASCOMTelescope.Reset();
            ParentMainForm.ObsControl.ASCOMTelescope.Connect = true;
        }

        private void btnConnectDomeSettings_Click(object sender, EventArgs e)
        {
            ParentMainForm.ObsControl.ASCOMDome.DOME_DRIVER_NAME = txtDomeDriverId.Text;
            ParentMainForm.ObsControl.ASCOMDome.resetDome();
            ParentMainForm.ObsControl.ASCOMDome.connectDome = true;
        }

    }
}
