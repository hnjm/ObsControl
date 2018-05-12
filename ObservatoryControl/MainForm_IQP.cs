using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms.DataVisualization.Charting;

using LoggingLib;
using IQPEngineLib;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace ObservatoryCenter
{
    /// <summary>
    /// IQP CONTROLS
    ///  All controls to work with IQP
    /// </summary>
    public partial class MainForm
    {
        //Statistics
        private uint IQP_statImagesProcessed = 0;
        private uint IQP_statImagesFound = 0;
        private uint IQP_statImagesWaiting = 0;

        private double curFWHM = 0.0;
        private double prevFWHM = 0.0;

        //flag to block concurent timer run
        private bool IQP_AlreadyRunning = false;

        //Settings
        public bool settingsAutoStartMonitoring = false;
        public List<string> IQP_FileMonitorPath = new List<string>();

        //monitorTimer
        public bool IQP_monitorTimer = false;


        /// <summary>
        /// Update visual elements on given time quant
        /// </summary>
        private void UpdateIQPStatus()
        {
            if (IQP_monitorTimer)
            {
                btnIQPStart.Text = "IQP:Stop";
                btnIQPStart.BackColor = OnColor;

            }
            else
            {
                btnIQPStart.Text = "IQP:Start";
                btnIQPStart.BackColor = DefBackColor;
            }
        }


        public void IQP_PublishFITSData(FileParseResult FileResObj)
        {
            this.Invoke(new Action(() => this.IQP_PublishFITSDataInvoke(FileResObj)));
        }

        private void IQP_PublishFITSDataInvoke(FileParseResult FileResObj)
        {
            //Grid block
            int curRowIndex = dataGridFileData.Rows.Add();
            dataGridFileData.Rows[curRowIndex].Cells["dataGridData_filename"].Value = Path.GetFileName(FileResObj.FITSFileName);
            dataGridFileData.Rows[curRowIndex].Cells["dataGridData_Bg"].Value = FileResObj.QualityData.SkyBackground.ToString("P", CultureInfo.InvariantCulture);
            dataGridFileData.Rows[curRowIndex].Cells["dataGridData_AspectRatio"].Value = FileResObj.QualityData.AspectRatio.ToString("N3", CultureInfo.InvariantCulture);
            dataGridFileData.Rows[curRowIndex].Cells["dataGridData_Stars"].Value = FileResObj.QualityData.StarsNumber.ToString("N0", CultureInfo.InvariantCulture);

            dataGridFileData.Rows[curRowIndex].Cells["dataGridData_Alt"].Value = FileResObj.HeaderData.ObjAlt.ToString("N0", CultureInfo.InvariantCulture);
            dataGridFileData.Rows[curRowIndex].Cells["dataGridData_Exp"].Value = FileResObj.HeaderData.ImageExposure.ToString("N0", CultureInfo.InvariantCulture);

            DateTime DateObsUTC = DateTime.SpecifyKind(FileResObj.HeaderData.DateObsUTC, DateTimeKind.Utc); //set it to UTC
            dataGridFileData.Rows[curRowIndex].Cells["dataGridData_DateTime"].Value = DateObsUTC.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");

            prevFWHM = curFWHM;
            curFWHM = FileResObj.FWHM;
            dataGridFileData.Rows[curRowIndex].Cells["dataGridData_FWHM"].Value = curFWHM.ToString("N2", CultureInfo.InvariantCulture);

            IQP_statImagesProcessed++;

            IQP_UpdateStatistics(); // on every invoke

            //publish to short form
            txtShort_IQP_LastFWHM.Text = curFWHM.ToString("N2", CultureInfo.InvariantCulture);
            txtShort_IQP_LastFWHM.Text = txtShort_IQP_LastFWHM.Text + "(" + ((curFWHM - prevFWHM) > 0 ? "+" : "") + (curFWHM - prevFWHM).ToString("N1", CultureInfo.InvariantCulture)+")";
            if (curFWHM > 5)
            {
                txtShort_IQP_LastFWHM.BackColor = OffColor;
            }
            else if (curFWHM > prevFWHM)
            {
                txtShort_IQP_LastFWHM.BackColor = InterColor;
            }
            else
            {
                txtShort_IQP_LastFWHM.BackColor = DefBackColorTextBoxes;
            }

            txtShort_IQPbg.Text = FileResObj.QualityData.SkyBackground.ToString("P", CultureInfo.InvariantCulture);
            if (FileResObj.QualityData.SkyBackground > 0.05)
            {
                txtShort_IQP_LastFWHM.BackColor = InterColor;
            }
            else if (FileResObj.QualityData.SkyBackground > 0.1)
            {
                txtShort_IQP_LastFWHM.BackColor = OffColor;
            }
            else
            { 
                txtShort_IQP_LastFWHM.BackColor = DefBackColorTextBoxes;
            }

        }

        private void IQP_UpdateStatistics()
        {
            //calc
            IQP_statImagesWaiting = ObsControl.objIQPEngine.ProcessingObj.QuequeLen();
            IQP_statImagesFound = IQP_statImagesProcessed + IQP_statImagesWaiting; //пока так

            //status 
            toolStripStatus_FilesFound.Text = "Files Found: " + IQP_statImagesFound;
            toolStripStatus_FilesProcessed.Text = "Processed: " + IQP_statImagesProcessed;
            toolStripStatus_FilesWaiting.Text = "Waiting: " + IQP_statImagesWaiting;
        }

        private void btnIQPAddFolder_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowNewFolderButton = false;
            //folderBrowserDialog1.RootFolder = Environment.SpecialFolder.Personal;

            DialogResult result = folderBrowserDialog1.ShowDialog();

            if (result == DialogResult.OK)
            {
                //add to combobox
                cmbIQPMonitorPath.Items.Add(folderBrowserDialog1.SelectedPath);
                cmbIQPMonitorPath.SelectedItem = folderBrowserDialog1.SelectedPath;

                //update lists
                IQP_SaveDirList();

                //MonitorObj.ClearFileList(); //очисить список
                Logging.AddLog("IQP start monitoring dir [" + folderBrowserDialog1.SelectedPath + "]", LogLevel.Activity, Highlight.Emphasize);
            }

        }
        /// <summary>
        /// Delete current dir from the list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnIQPDelFolder_Click(object sender, EventArgs e)
        {
            //remove from combobox
            cmbIQPMonitorPath.Items.RemoveAt(cmbIQPMonitorPath.SelectedIndex);
            if (cmbIQPMonitorPath.Items.Count > 0)
                cmbIQPMonitorPath.SelectedIndex = 0;

            //update lists
            IQP_SaveDirList();
        }

        /// <summary>
        /// Updates all dir lists from combobox list
        /// Из элемента формы обновляются данные в XML и в переменных
        /// </summary>
        private void IQP_SaveDirList()
        {
            //1. Empty current lists
            //Monitoring list
            IQP_FileMonitorPath.Clear();
            //Config
            ConfigManagement.ClearSection("monitorPath"); //clear entire section

            //2. Make new lists
            for (int i = 0; i < cmbIQPMonitorPath.Items.Count; i++)
            {
                string curDir = cmbIQPMonitorPath.GetItemText(cmbIQPMonitorPath.Items[i]);

                //Add to monitor list
                IQP_FileMonitorPath.Add(curDir);
                //Add to config list
                ConfigManagement.UpdateConfigValue("monitorPath", "Dir" + (i + 1), curDir);
            }

            //3. Save config
            ConfigManagement.Save();

            //4. Обновить инфо о количестве подакаталогов
            lblDirsMonitoringCount.Text = cmbIQPMonitorPath.Items.Count.ToString();
        }

        private void btnIQPStart_Click(object sender, EventArgs e)
        {
            if (IQP_monitorTimer)
            // if runnig - stop
            {
                //stop timer
                IQP_monitorTimer = false;
                //end monitor thread
                ObsControl.objIQPEngine.MonitorObj.AbortThread();

                Logging.AddLog("IQP: Montoring has been stoped", LogLevel.Activity);
            }
            else
            {
                IQPStart();
            }
        }

        private void chkShort_IQP_Click(object sender, EventArgs e)
        {
            btnIQPStart_Click(sender, e);
        }


        public void IQPStart()
        {
            IQP_monitorTimer = true;
            Logging.AddLog("IQP: Starting monitoring...", LogLevel.Activity);
        }

        private void btnRecheck_Click(object sender, EventArgs e)
        {
            Logging.AddLog("IQP: Resetting data and rereading it from scratch", LogLevel.Activity, Highlight.Emphasize);
            IQP_ResetData();
        }

        private void chkSearchSubdirs_CheckedChanged(object sender, EventArgs e)
        {
            ObsControl.objIQPEngine.MonitorObj.settingsScanSubdirs = chkSearchSubdirs.Checked;
        }

        /// <summary>
        /// Save current settings. And reload them
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnIQPSettings_Save_Click(object sender, EventArgs e)
        {
            IQP_SaveSettingsToConfigFile();

        }

        /// <summary>
        /// Reset all data (on pressing button)
        /// </summary>
        public void IQP_ResetData()
        {
            //Stop current processing activity
            ObsControl.objIQPEngine.MonitorObj.AbortThread();
            //reset already monitored filelist
            ObsControl.objIQPEngine.MonitorObj.ClearFileList();
            //Clear queque
            ObsControl.objIQPEngine.ProcessingObj.Clear();

            //Clear IMS data
            ObsControl.objIQPEngine.MonitorObj.ClearDirIMSData();

            //clear Grid block
            dataGridFileData.Rows.Clear();

            //update statistics
            IQP_statImagesFound = 0;
            IQP_statImagesProcessed = 0;
            IQP_statImagesWaiting = 0;
            IQP_UpdateStatistics();
        }


        private void IQP_LoadParamsFromXML()
        {
            try
            {
                List<string> dirNodesList = ConfigManagement.getAllSectionNamesList("monitorPath");
                foreach (string curDirNode in dirNodesList)
                {
                    IQP_FileMonitorPath.Add(ConfigManagement.getString("monitorPath", curDirNode));
                }

                ObsControl.objIQPEngine.MonitorObj.settingsScanSubdirs = ConfigManagement.getBool("IQP_options", "ScanSubDirs") ?? false;
                settingsAutoStartMonitoring = ConfigManagement.getBool("IQP_options", "AUTOSTARTMONITORING") ?? false;
                ObsControl.objIQPEngine.ProcessingObj.settingsDSSCLPath = ConfigManagement.getString("IQP_options", "DSS_PATH") ?? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), @"\DeepSkyStacker\DeepSkyStackerCL.exe");

                ObsControl.objIQPEngine.ProcessingObj.settingsPublishToGroup = ConfigManagement.getBool("IQP_options", "PUBLISHTOGROUP") ?? true;
                ObsControl.objIQPEngine.WebPublishObj.SetURL(ConfigManagement.getString("publishURL", "url1") ?? "http://localhost");
                ObsControl.objIQPEngine.WebPublishObj.ServerKey = ConfigManagement.getString("publishURL", "key1") ?? "";

                ObsControl.objIQPEngine.ProcessingObj.settingsPublishToPrivate = ConfigManagement.getBool("IQP_options", "PUBLISHTOPRIVATE") ?? true;
                ObsControl.objIQPEngine.WebPublishObj2.SetURL(ConfigManagement.getString("publishURL", "url2") ?? "http://localhost");
                ObsControl.objIQPEngine.WebPublishObj2.ServerKey = ConfigManagement.getString("publishURL", "key2") ?? "";

                //hidden settings
                ObsControl.objIQPEngine.MonitorObj.settingsExtensionToSearch = ConfigManagement.getString("IQP_options", "extensionsToSearch") ?? "*.fit*";
                ObsControl.objIQPEngine.ProcessingObj.settingsMaxThreads = (uint)(ConfigManagement.getInt("IQP_options", "checkThreads_max") ?? 1);
                ObsControl.objIQPEngine.ProcessingObj.settingsSkipIMSfiles = ConfigManagement.getBool("IQP_options", "checkDirIMS") ?? true;
                ObsControl.objIQPEngine.ProcessingObj.settingsDSSForceRecheck = ConfigManagement.getBool("IQP_options", "alwaysRebuildDSSInfoFile") ?? false;
                ObsControl.objIQPEngine.ProcessingObj.settingsDSSForceRunHidden = ConfigManagement.getBool("IQP_options", "RunDSSHidden") ?? false;
                ObsControl.objIQPEngine.ProcessingObj.settingsPublishLightFramesOnly = ConfigManagement.getBool("IQP_options", "publishLightFramesOnly") ?? true;
                ObsControl.objIQPEngine.ProcessingObj.settingsDSSInfoFileAutoDelete = ConfigManagement.getBool("IQP_options", "autoDeleteDSSInfoFile") ?? false;

                //Filter settings
                string st = ConfigManagement.getString("IQP_filters", "excludedirs") ?? "";
                ObsControl.objIQPEngine.MonitorObj.settingsFilterDirName_ExcludeSt = new List<string>(st.Split(';'));
                st = ConfigManagement.getString("IQP_filters", "excludefiles") ?? "";
                ObsControl.objIQPEngine.MonitorObj.settingsFilterFileName_ExcludeSt = new List<string>(st.Split(';'));
                ObsControl.objIQPEngine.ProcessingObj.settingsFilterObserverTag_Contains = ConfigManagement.getString("IQP_filters", "observer") ?? "";
                ObsControl.objIQPEngine.ProcessingObj.settingsFilterTelescopTag_Contains = ConfigManagement.getString("IQP_filters", "telescop") ?? "";
                ObsControl.objIQPEngine.ProcessingObj.settingsFilterInstrumeTag_Contains = ConfigManagement.getString("IQP_filters", "instrume") ?? "";
                //Filter settings: quality
                ObsControl.objIQPEngine.ProcessingObj.settingsFilterHistoryTag_MaxCount = (UInt16)(ConfigManagement.getInt("IQP_filters", "historycount") ?? 1);
                ObsControl.objIQPEngine.ProcessingObj.settingsFilterStarsNum_MinCount = (UInt16)(ConfigManagement.getInt("IQP_filters", "minstars") ?? 1);
                ObsControl.objIQPEngine.ProcessingObj.settingsFilterFWHM_MaxVal = ConfigManagement.getDouble("IQP_filters", "maxfwhm") ?? 10.0;
                ObsControl.objIQPEngine.ProcessingObj.settingsFilterMinAltitude_MinVal = ConfigManagement.getDouble("IQP_filters", "minaltitude") ?? 19.0;
                ObsControl.objIQPEngine.ProcessingObj.settingsFilterBackground_MaxVal = ConfigManagement.getDouble("IQP_filters", "maxbackground") ?? 0.30;

                Logging.AddLog("Program parameters were set according to configuration file", LogLevel.Activity);
            }
            catch (Exception ex)
            {
                Logging.AddLog("Couldn't load options" + ex.Message, LogLevel.Important, Highlight.Error);
                Logging.AddLog("Exception details: " + ex.ToString(), LogLevel.Debug, Highlight.Error);
            }
        }
        private void IQP_SaveSettingsToConfigFile()
        {
            //1. Update ConfigXML

            //Save dirlist
            ConfigManagement.ClearSection("monitorPath"); //clear entire section
            for (int i = 0; i < cmbIQPMonitorPath.Items.Count; i++)
            {
                string curDir = cmbIQPMonitorPath.GetItemText(cmbIQPMonitorPath.Items[i]);
                ConfigManagement.UpdateConfigValue("monitorPath", "Dir" + (i + 1), curDir);
            }

            ConfigManagement.UpdateConfigValue("IQP_options", "ScanSubDirs", chkSearchSubdirs.Checked.ToString());
            //ConfigManagement.UpdateConfigValue("IQP_options", "AUTOSTARTMONITORING", chkSettings_Autostart.Checked.ToString());
            //ConfigManagement.UpdateConfigValue("IQP_options", "DSS_PATH", txtSettings_DSS.Text);
            //ConfigManagement.UpdateConfigValue("IQP_options", "Language", cmbLang.SelectedValue.ToString());

            //ConfigManagement.UpdateConfigValue("IQP_options", "PUBLISHTOGROUP", chkSettings_publishgroup.Checked.ToString());
            //ConfigManagement.UpdateConfigValue("publishURL", "url1", txtSettings_urlgorup.Text);
            //ConfigManagement.UpdateConfigValue("publishURL", "key1", txtServerKey_Group.Text);

            //ConfigManagement.UpdateConfigValue("IQP_options", "PUBLISHTOPRIVATE", chkSettings_publishprivate.Checked.ToString());
            //ConfigManagement.UpdateConfigValue("publishURL", "url2", txtSettings_urlprivate.Text);
            //ConfigManagement.UpdateConfigValue("publishURL", "key2", txtServerKey_Private.Text);

            //hidden settings
            ConfigManagement.UpdateConfigValue("IQP_options", "extensionsToSearch", ObsControl.objIQPEngine.MonitorObj.settingsExtensionToSearch);
            ConfigManagement.UpdateConfigValue("IQP_options", "checkThreads_max", ObsControl.objIQPEngine.ProcessingObj.settingsMaxThreads.ToString());
            ConfigManagement.UpdateConfigValue("IQP_options", "checkDirIMS", ObsControl.objIQPEngine.ProcessingObj.settingsSkipIMSfiles.ToString());
            ConfigManagement.UpdateConfigValue("IQP_options", "alwaysRebuildDSSInfoFile", ObsControl.objIQPEngine.ProcessingObj.settingsDSSForceRecheck.ToString());
            ConfigManagement.UpdateConfigValue("IQP_options", "RunDSSHidden", ObsControl.objIQPEngine.ProcessingObj.settingsDSSForceRunHidden.ToString());
            ConfigManagement.UpdateConfigValue("IQP_options", "autoDeleteDSSInfoFile", ObsControl.objIQPEngine.ProcessingObj.settingsDSSInfoFileAutoDelete.ToString());
            ConfigManagement.UpdateConfigValue("IQP_options", "publishLightFramesOnly", ObsControl.objIQPEngine.ProcessingObj.settingsPublishLightFramesOnly.ToString());

            //Filter settings
            ConfigManagement.UpdateConfigValue("IQP_filters", "excludedirs", String.Join(";", ObsControl.objIQPEngine.MonitorObj.settingsFilterDirName_ExcludeSt.ToArray()));
            ConfigManagement.UpdateConfigValue("IQP_filters", "excludefiles", string.Join(";", ObsControl.objIQPEngine.MonitorObj.settingsFilterFileName_ExcludeSt.ToArray()));
            ConfigManagement.UpdateConfigValue("IQP_filters", "observer", ObsControl.objIQPEngine.ProcessingObj.settingsFilterObserverTag_Contains);
            ConfigManagement.UpdateConfigValue("IQP_filters", "telescop", ObsControl.objIQPEngine.ProcessingObj.settingsFilterTelescopTag_Contains);
            ConfigManagement.UpdateConfigValue("IQP_filters", "instrume", ObsControl.objIQPEngine.ProcessingObj.settingsFilterInstrumeTag_Contains);
            ConfigManagement.UpdateConfigValue("IQP_filters", "historycount", ObsControl.objIQPEngine.ProcessingObj.settingsFilterHistoryTag_MaxCount.ToString());
            ConfigManagement.UpdateConfigValue("IQP_filters", "minstars", ObsControl.objIQPEngine.ProcessingObj.settingsFilterStarsNum_MinCount.ToString());
            ConfigManagement.UpdateConfigValue("IQP_filters", "maxfwhm", ObsControl.objIQPEngine.ProcessingObj.settingsFilterFWHM_MaxVal.ToString());
            ConfigManagement.UpdateConfigValue("IQP_filters", "minaltitude", ObsControl.objIQPEngine.ProcessingObj.settingsFilterMinAltitude_MinVal.ToString());
            ConfigManagement.UpdateConfigValue("IQP_filters", "maxbackground", ObsControl.objIQPEngine.ProcessingObj.settingsFilterBackground_MaxVal.ToString());

            //2. Save ConfigXML to disk
            ConfigManagement.Save();

            //3. Load config from disk
            ConfigManagement.Load();
            IQP_LoadParamsFromXML();

        }

        /// <summary>
        /// Update form fields from actual settings
        /// </summary>
        private void IQP_UpdateSettingsDialogFields()
        {
            //Установить значения из ранее загруженных данных в диалоге настройка
            //перечень подкаталогов
            cmbIQPMonitorPath.Items.Clear();
            foreach (string curDir in IQP_FileMonitorPath)
            {
                cmbIQPMonitorPath.Items.Add(curDir);
            }
            if (cmbIQPMonitorPath.Items.Count >= 1) cmbIQPMonitorPath.SelectedIndex = 0;

            lblDirsMonitoringCount.Text = cmbIQPMonitorPath.Items.Count.ToString();
            chkSearchSubdirs.Checked = ObsControl.objIQPEngine.MonitorObj.settingsScanSubdirs;
        }

    }
}
