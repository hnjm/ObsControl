using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WeatherControl
{
    public partial class FormWeatherFileControl : Form
    {

        public BoltwoodClass BoltwoodObj;
        public BoltwoodFields BoltwoodObj_GoodState;
        public BoltwoodFields BoltwoodObj_BadState;

        internal DateTime CurrentTime = new DateTime();
        internal DateTime LastWriteTime = new DateTime(2017, 02, 17);

        Color WaitingColor = Color.CadetBlue;
        Color OnColor = Color.DarkSeaGreen;
        Color OffColor = Color.Tomato;
        Color DisabledColor = Color.LightGray;

        #region Add About menu
        // P/Invoke constants
        private const int WM_SYSCOMMAND = 0x112;
        private const int MF_STRING = 0x0;
        private const int MF_SEPARATOR = 0x800;

        // P/Invoke declarations
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool AppendMenu(IntPtr hMenu, int uFlags, int uIDNewItem, string lpNewItem);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool InsertMenu(IntPtr hMenu, int uPosition, int uFlags, int uIDNewItem, string lpNewItem);


        // ID for the About item on the system menu
        private int SYSMENU_ABOUT_ID = 0x1;

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            // Get a handle to a copy of this form's system (window) menu
            IntPtr hSysMenu = GetSystemMenu(this.Handle, false);

            // Add a separator
            AppendMenu(hSysMenu, MF_SEPARATOR, 0, string.Empty);

            // Add the About menu item
            AppendMenu(hSysMenu, MF_STRING, SYSMENU_ABOUT_ID, "&About…");
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            // Test if the About item was selected from the system menu
            if ((m.Msg == WM_SYSCOMMAND) && ((int)m.WParam == SYSMENU_ABOUT_ID))
            {

                MessageBox.Show(VersionData.getVersionString(),"About", MessageBoxButtons.OK,MessageBoxIcon.Information);
            }

        }
        #endregion


        public FormWeatherFileControl()
        {
            InitializeComponent();
            VersionData.initVersionData();
        }

        private void FormWeatherFileControl_Load(object sender, EventArgs e)
        {
            // Create BoltwoodObject
            BoltwoodObj = new BoltwoodClass();
            BoltwoodObj_GoodState = new BoltwoodFields();
            BoltwoodObj_BadState = new BoltwoodFields();


            //Init form fields
            txtLastWritten.Text = LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss");

            ignoreEvents = true;

            comboBoxRainFlag.DataSource = Enum.GetNames(typeof(Enum_RainFlag));
            comboBoxWetFlag.DataSource = Enum.GetNames(typeof(Enum_WetFlag));

            comboBoxCloudCond.DataSource = Enum.GetNames(typeof(Enum_CloudCond));
            comboBoxWindCond.DataSource = Enum.GetNames(typeof(Enum_WindCond));
            comboBoxRainCond.DataSource = Enum.GetNames(typeof(Enum_RainCond));
            comboBoxDaylightCond.DataSource = Enum.GetNames(typeof(Enum_DayCond));

            comboBoxRoofCloseFlag.DataSource = Enum.GetNames(typeof(Enum_RoofFlag));
            comboBoxAlertFlag.DataSource = Enum.GetNames(typeof(Enum_AlertFlag));

            comboBoxDecimalSeparator.DataSource = Enum.GetNames(typeof(decimalSeparatorType));

            //Загрузит настройки
            LoadSettings();

            //Update BotlwoodObj from FormFields
            EventArgs evnt = new EventArgs();
            OnFieldUpdate(this, evnt);
            chkUseSmartLogic_CheckedChanged(this, evnt);

            ignoreEvents = false;

            //Saved 
            txtFilePath.Text = (BoltwoodFileClass.BoltwoodFilePath == "" ? BoltwoodFileClass.DefaultFilePath : BoltwoodFileClass.BoltwoodFilePath) + BoltwoodFileClass.BoltwoodFileName;
        }


        private void timerTime_Tick(object sender, EventArgs e)
        {
            //Обновить поля со временем
            //CurrentTime = DateTime.Now;
            //txtDate.Text = CurrentTime.ToString("yyyy-MM-dd");
            //txtTime.Text = CurrentTime.ToString("HH:mm:ss");

            txtDate.Text = BoltwoodObj.Date;
            txtTime.Text = BoltwoodObj.Time;

            txtSecondsSinceWrite.Text = BoltwoodObj.SecondsSince.ToString();
            txtVBANow.Text = BoltwoodObj.Now.ToString("000000.#####");

            txtDebug.Text = BoltwoodObj.getBoltwoodString();

            chkRainNow.Checked = BoltwoodObj.RainNowEvent_Flag;
            txtSinceLastRain.Text = BoltwoodObj.Bolt_RainFlag_sinceLastDetected.ToString();

            chkWetNow.Checked = BoltwoodObj.WetNowEvent_Flag;
            txtSinceLastWet.Text = BoltwoodObj.Bolt_WetFlag_sinceLastDetected.ToString();

        }

        #region Boltwoods data change events

        /// <summary>
        /// Ignore events flag
        /// </summary>
        private bool ignoreEvents = false;

        private void OnFieldUpdate(object sender, EventArgs e)
        {
            //if ignore Events is set, do nothing
            if (ignoreEvents) return;

            //From Form to BoltwoodObj
            UpdateSimpleVars_From_Form();

            //From BoltwoodObj to From
            UpdateFormFields();
        }


        public void UpdateSimpleVars_From_Form()
        {
            try { BoltwoodObj.SkyTemp = Convert.ToDouble(txtSkyTemp.Text); } catch { };
            try { BoltwoodObj.AmbientTemp = Convert.ToDouble(txtAmbTemp.Text); } catch { };
            try { BoltwoodObj.SensorTemp = Convert.ToDouble(txtSensorTemp.Text); } catch { };
            try { BoltwoodObj.WindSpeed = Convert.ToDouble(txtWindSpeed.Text); } catch { };
            try { BoltwoodObj.Humidity = Convert.ToDouble(txtHumidity.Text); } catch { };
            try { BoltwoodObj.Heater = Convert.ToInt16(txtHeating.Text); } catch { };

            //try { BoltwoodObj.RainFlag_DirectSet = (Enum_RainFlag)Enum.Parse(typeof(Enum_RainFlag), comboBoxRainFlag.SelectedItem.ToString()); } catch { };
            //try { BoltwoodObj.WetFlag_DirectSet = (Enum_WetFlag)Enum.Parse(typeof(Enum_WetFlag), comboBoxWetFlag.SelectedItem.ToString()); } catch { };

            try { BoltwoodObj.CloudCond_DirectSet = (Enum_CloudCond)Enum.Parse(typeof(Enum_CloudCond), comboBoxCloudCond.SelectedItem.ToString()); } catch { };
            try { BoltwoodObj.WindCond_DirectSet = (Enum_WindCond)Enum.Parse(typeof(Enum_WindCond), comboBoxWindCond.SelectedItem.ToString()); } catch { };
            //try { BoltwoodObj.RainCond_DirectSet = (Enum_RainCond)Enum.Parse(typeof(Enum_RainCond), comboBoxRainCond.SelectedItem.ToString()); } catch { };
            try { BoltwoodObj.DaylightCond = (Enum_DayCond)Enum.Parse(typeof(Enum_DayCond), comboBoxDaylightCond.SelectedItem.ToString()); } catch { };


            try { BoltwoodObj.RoofCloseFlag = (Enum_RoofFlag)Enum.Parse(typeof(Enum_RoofFlag), comboBoxRoofCloseFlag.SelectedItem.ToString()); } catch { };
            try { BoltwoodObj.AlertFlag = (Enum_AlertFlag)Enum.Parse(typeof(Enum_AlertFlag), comboBoxAlertFlag.SelectedItem.ToString()); } catch { };

        }

        public void UpdateFormFields()
        {
            ignoreEvents = true;

            txtDewPoint.Text = BoltwoodObj.DewPoint.ToString("0.#");
            txtSkyTemp.Text = BoltwoodObj.SkyTemp.ToString();
            txtWindSpeed.Text = BoltwoodObj.WindSpeed.ToString();

            comboBoxCloudCond.SelectedItem = BoltwoodObj.CloudCond.ToString();
            comboBoxWindCond.SelectedItem = BoltwoodObj.WindCond.ToString();
            //comboBoxRainCond.SelectedItem = BoltwoodObj.RainCond.ToString();
            comboBoxDaylightCond.SelectedItem = BoltwoodObj.DaylightCond.ToString();

            comboBoxRoofCloseFlag.SelectedItem = BoltwoodObj.RoofCloseFlag.ToString();
            comboBoxAlertFlag.SelectedItem = BoltwoodObj.AlertFlag.ToString();

            if (!BoltwoodObj.DONT_USE_DIRECT_ACCESS)
            {
                comboBoxRainCond.SelectedItem = BoltwoodObj.RainCond.ToString();
                comboBoxRainFlag.SelectedItem = BoltwoodObj.RainFlag.ToString();
                comboBoxWetFlag.SelectedItem = BoltwoodObj.WetFlag.ToString();
            }

            ignoreEvents = false;
        }

        private void comboBoxRainFlag_SelectedIndexChanged(object sender, EventArgs e)
        {
            ignoreEvents = true;
            //Меняем RainFlag
            try { BoltwoodObj.RainFlag_DirectSet = (Enum_RainFlag)Enum.Parse(typeof(Enum_RainFlag), comboBoxRainFlag.SelectedItem.ToString()); } catch { };
            //Перевычисляем и выводим WetFlag
            comboBoxWetFlag.SelectedItem = BoltwoodObj.WetFlag.ToString();
            //Перевычисляем и выводим RainCond
            comboBoxRainCond.SelectedItem = BoltwoodObj.RainCond.ToString();

            //try { BoltwoodObj.WetFlag_DirectSet = (Enum_WetFlag)Enum.Parse(typeof(Enum_WetFlag), comboBoxWetFlag.SelectedItem.ToString()); } catch { };
            ignoreEvents = false;
        }

        private void comboBoxWetFlag_SelectedIndexChanged(object sender, EventArgs e)
        {
            ignoreEvents = true;

            //Меняем WetFlag
            try { BoltwoodObj.WetFlag_DirectSet = (Enum_WetFlag)Enum.Parse(typeof(Enum_WetFlag), comboBoxWetFlag.SelectedItem.ToString()); } catch { };
            //Перевычисляем и выводим RainFlag
            comboBoxRainFlag.SelectedItem = BoltwoodObj.RainFlag.ToString();
            //Перевычисляем и выводим RainCond
            comboBoxRainCond.SelectedItem = BoltwoodObj.RainCond.ToString();

            ignoreEvents = false;
        }

        private void comboBoxRainCond_SelectedIndexChanged(object sender, EventArgs e)
        {
            ignoreEvents = true;

            //Меняем Rain Cond
            try { BoltwoodObj.RainCond_DirectSet = (Enum_RainCond)Enum.Parse(typeof(Enum_RainCond), comboBoxRainCond.SelectedItem.ToString()); } catch { };

            //Перевычисляем и выводим RainFlag
            comboBoxRainFlag.SelectedItem = BoltwoodObj.RainFlag.ToString();
            //Перевычисляем и выводим WetFlag
            comboBoxWetFlag.SelectedItem = BoltwoodObj.WetFlag.ToString();

            ignoreEvents = false;
        }
        #endregion

        #region Write File commands
        private void btnWriteNow_Click(object sender, EventArgs e)
        {
            BoltwoodObj.SetMeasurement(); //update measured time
            BoltwoodFileClass.WirteBoltwoodData(BoltwoodObj.getBoltwoodString());
            txtLastWritten.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        private void timerBolwoodWrite_Tick(object sender, EventArgs e)
        {
            BoltwoodObj.SetMeasurement(); //update measured time
            BoltwoodFileClass.WirteBoltwoodData(BoltwoodObj.getBoltwoodString());
            txtLastWritten.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            timerBolwoodWrite.Interval = (int)(((NumericUpDown)sender).Value * 1000);
        }

        private void btnTimerControl_Click(object sender, EventArgs e)
        {
            if (((Button)sender).Text == "Старт автозапись")
            {
                timerBolwoodWrite.Enabled = true;
                ((Button)sender).Text = "Стоп автозапись";
                ((Button)sender).BackColor = OnColor;
            }
            else
            {
                timerBolwoodWrite.Enabled = false;
                ((Button)sender).Text = "Старт автозапись";
                ((Button)sender).BackColor = SystemColors.Control;
            }
        }
        #endregion

        #region PRESETS
        private void chkSaveConditions_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ChkB = (CheckBox)sender;

            if (ChkB.Checked)
            {
                //Если нажали сохранить
                //1. Отключаем любые условия
                chkGoodConditions.Checked = false;
                chkBadConditions.Checked = false;
                //2. Цветом обозначаем
                chkGoodConditions.BackColor = WaitingColor;
                chkBadConditions.BackColor = WaitingColor;
            }
            else
            {
                chkGoodConditions.BackColor = SystemColors.ButtonFace;
                chkBadConditions.BackColor = SystemColors.ButtonFace;
            }
        }

        private void chkGoodConditions_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {
                //Если включили
                //Если до этого был включен флаг запись, то записать
                if (chkSaveConditions.Checked)
                {
                    // Записать ХОРОШИЕ УСЛОВИЯ
                    BoltwoodObj_GoodState.CopyEssentialParameters(BoltwoodObj);

                    // Сохранить их на будущее
                    string st2 = BoltwoodObj_GoodState.SerializeToJSON();
                    Properties.Settings.Default.GoodSettings = st2;


                    //Отключить флаг "запись"
                    chkSaveConditions.Checked = false;

                    //Перезакрасить кнопки "условий"
                    chkGoodConditions.BackColor = SystemColors.ButtonFace;
                    chkBadConditions.BackColor = SystemColors.ButtonFace;

                    //выключить текущую кнопку
                    ((CheckBox)sender).Checked = false;

                }
                //Если нее был включен флаг запись, то переключить
                else
                {
                    // Переключить режим а ХОРОШИЕ условия
                    BoltwoodObj.CopyEssentialParameters(BoltwoodObj_GoodState);
                    // Обновить поля
                    UpdateFormFields();

                    //Подкрасить кнопку
                    chkGoodConditions.BackColor = OnColor;

                    //Сбросить другую кнопку
                    chkBadConditions.Checked = false;
                    chkBadConditions.BackColor = SystemColors.ButtonFace;

                }
            }
            //Если выключили
            else
            {
                //Сбросить другую кнопку
                ((CheckBox)sender).BackColor = SystemColors.ButtonFace;
            }

        }

        private void chkBadConditions_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {
                //Если включили
                //Если до этого был включен флаг запись, то записать
                if (chkSaveConditions.Checked)
                {
                    // Записать ПЛОХИЕ УСЛОВИЯ
                    BoltwoodObj_BadState.CopyEssentialParameters(BoltwoodObj);

                    // Сохранить их на будущее
                    string st2 = BoltwoodObj_BadState.SerializeToJSON();
                    Properties.Settings.Default.BadSettings = st2;

                    //Отключить флаг "запись"
                    chkSaveConditions.Checked = false;

                    //Перезакрасить кнопки "условий"
                    chkGoodConditions.BackColor = SystemColors.ButtonFace;
                    chkBadConditions.BackColor = SystemColors.ButtonFace;

                    //выключить текущую кнопку
                    ((CheckBox)sender).Checked = false;
                }
                //Если нее был включен флаг запись, то переключить
                else
                {
                    // Переключить режим на ПЛОХИЕ условия
                    BoltwoodObj.CopyEssentialParameters(BoltwoodObj_BadState);
                    // Обновить поля
                    UpdateFormFields();

                    //Подкрасить кнопку
                    chkBadConditions.BackColor = OffColor;

                    //Сбросить другую кнопку
                    chkGoodConditions.Checked = false;
                    chkGoodConditions.BackColor = SystemColors.ButtonFace;
                }
            }
            //Если выключили
            else
            {
                //Сбросить другую кнопку
                ((CheckBox)sender).BackColor = SystemColors.ButtonFace;
            }
        }
        #endregion



        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
            this.Close();
        }
        private void btnFileDialog_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "Файлы данных (*.txt; *.dat) | *.txt; *.dat";
            saveFileDialog1.InitialDirectory = (BoltwoodFileClass.BoltwoodFilePath == "" ? BoltwoodFileClass.DefaultFilePath : BoltwoodFileClass.BoltwoodFilePath);
            saveFileDialog1.FileName = BoltwoodFileClass.BoltwoodFileName;
            saveFileDialog1.DefaultExt = ".dat";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string dialogFN = saveFileDialog1.FileName;

                BoltwoodFileClass.BoltwoodFilePath = Path.GetDirectoryName(dialogFN) + Path.DirectorySeparatorChar;
                BoltwoodFileClass.BoltwoodFileName = Path.GetFileName(dialogFN);

                txtFilePath.Text = BoltwoodFileClass.BoltwoodFilePath + BoltwoodFileClass.BoltwoodFileName;
            }

        }

        private void chkUseSmartLogic_CheckedChanged(object sender, EventArgs e)
        {
            BoltwoodObj.DONT_USE_DIRECT_ACCESS = chkUseSmartLogic.Checked;
        }

        private void comboBoxDecimalSeparator_SelectedIndexChanged(object sender, EventArgs e)
        {
            try { BoltwoodObj.ForcedDecimalSeparator = (decimalSeparatorType)Enum.Parse(typeof(decimalSeparatorType), comboBoxDecimalSeparator.SelectedItem.ToString()); } catch { };
        }


        void LoadSettings()
        {
            chkUseSmartLogic.Checked = Properties.Settings.Default.UseBoltwoodLogic;
            comboBoxDecimalSeparator.Text = Properties.Settings.Default.decimalPoint;

            // Восстановить JSON obj состояний
            string st1 = Properties.Settings.Default.GoodSettings;
            BoltwoodObj_GoodState.DeserializeFromJSON(st1);

            string st2 = Properties.Settings.Default.BadSettings;
            BoltwoodObj_BadState.DeserializeFromJSON(st2);


        }

    }
}
