using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
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
        internal DateTime LastWriteTime = new DateTime(2017,02,17);
        internal TimeSpan SinceLastWrite;

        Color WaitingColor = Color.CadetBlue;
        Color OnColor = Color.DarkSeaGreen;
        Color OffColor = Color.Tomato;
        Color DisabledColor = Color.LightGray;


        public FormWeatherFileControl()
        {
            InitializeComponent();
        }

        private void FormWeatherFileControl_Load(object sender, EventArgs e)
        {
            // Create BoltwoodObject
            BoltwoodObj = new BoltwoodClass();
            BoltwoodObj_GoodState = new BoltwoodFields();
            BoltwoodObj_BadState = new BoltwoodFields();


            //Init form fields
            txtLastWritten.Text = LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss");

            comboBoxRainFlag.DataSource = Enum.GetNames(typeof(Enum_RainFlag));
            comboBoxWetFlag.DataSource = Enum.GetNames(typeof(Enum_WetFlag));

            comboBoxCloudCond.DataSource = Enum.GetNames(typeof(Enum_CloudCond));
            comboBoxWindCond.DataSource = Enum.GetNames(typeof(Enum_WindCond));
            comboBoxRainCond.DataSource = Enum.GetNames(typeof(Enum_RainCond));
            comboBoxDaylightCond.DataSource = Enum.GetNames(typeof(Enum_DayCond));

            comboBoxRoofCloseFlag.DataSource = Enum.GetNames(typeof(Enum_RoofFlag));
            comboBoxAlertFlag.DataSource = Enum.GetNames(typeof(Enum_AlertFlag));

            //this.comboBoxRainFlag.TextChanged += new System.EventHandler(this.OnFieldUpdate);
            //this.comboBoxWetFlag.TextChanged += new System.EventHandler(this.OnFieldUpdate);
            this.comboBoxAlertFlag.TextChanged += new System.EventHandler(this.OnFieldUpdate);
            this.comboBoxRoofCloseFlag.TextChanged += new System.EventHandler(this.OnFieldUpdate);
            this.comboBoxDaylightCond.TextChanged += new System.EventHandler(this.OnFieldUpdate);
            //this.comboBoxRainCond.TextChanged += new System.EventHandler(this.OnFieldUpdate);
            this.comboBoxWindCond.TextChanged += new System.EventHandler(this.OnFieldUpdate);
            this.comboBoxCloudCond.TextChanged += new System.EventHandler(this.OnFieldUpdate);


            //Special events
            this.comboBoxWetFlag.SelectedIndexChanged += new System.EventHandler(this.comboBoxWetFlag_SelectedIndexChanged);
            this.comboBoxRainCond.SelectedIndexChanged += new System.EventHandler(this.comboBoxRainCond_SelectedIndexChanged);
            this.comboBoxRainFlag.SelectedIndexChanged += new System.EventHandler(this.comboBoxRainFlag_SelectedIndexChanged);

            //Update BotlwoodObj from FormFields
            EventArgs evnt = new EventArgs();
            OnFieldUpdate(this, evnt);

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

            //Пересчитать точку росы
            /*
            try
            {
                txtDewPoint.Text=Math.Round(BolwoodCalculations.calcDewPoint(Convert.ToDouble(txtAmbTemp.Text), Convert.ToDouble(txtHumidity.Text)),1).ToString();
            }
            catch
            {
                txtDewPoint.Text = "error";
            }

            //Since last write
            SinceLastWrite = CurrentTime.Subtract(LastWriteTime);
            txtSecondsSinceWrite.Text = Math.Round(Convert.ToDouble(SinceLastWrite.TotalSeconds),0).ToString();

            txtVBANow.Text = Math.Round(Convert.ToDouble(CurrentTime.ToOADate()),5).ToString();
            */

            txtDebug.Text = BoltwoodObj.getBoltwoodString();

            chkRainNow.Checked = BoltwoodObj.RainNowEvent_Flag;
            txtSinceLastRain.Text = BoltwoodObj.Bolt_RainFlag_sinceLastDetected.ToString();

            chkWetNow.Checked = BoltwoodObj.WetNowEvent_Flag;
            txtSinceLastWet.Text = BoltwoodObj.Bolt_WetFlag_sinceLastDetected.ToString();

        }

        #region Boltwoods data change events
        private void OnFieldUpdate(object sender, EventArgs e)
        {
            //From Form to BoltwoodObj
            UpdateVarsFromForm();
            //From BoltwoodObj to From
            UpdateFormFields();
        }
        public void UpdateVarsFromForm()
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
            txtDewPoint.Text = BoltwoodObj.DewPoint.ToString("0.#");
            txtSkyTemp.Text = BoltwoodObj.SkyTemp.ToString();
            txtWindSpeed.Text = BoltwoodObj.WindSpeed.ToString();
            //comboBoxRainFlag.SelectedItem = BoltwoodObj.RainFlag;
            //comboBoxWetFlag.SelectedItem = BoltwoodObj.WetFlag;

            comboBoxCloudCond.SelectedItem = BoltwoodObj.CloudCond;
            comboBoxWindCond.SelectedItem = BoltwoodObj.WindCond;
            //comboBoxRainCond.SelectedItem = BoltwoodObj.RainCond;
            comboBoxDaylightCond.SelectedItem = BoltwoodObj.DaylightCond;

            comboBoxRoofCloseFlag.SelectedItem = BoltwoodObj.RoofCloseFlag;
            comboBoxAlertFlag.SelectedItem = BoltwoodObj.AlertFlag;
        }

        private void comboBoxRainFlag_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Меняем RainFlag
            try { BoltwoodObj.RainFlag_DirectSet = (Enum_RainFlag)Enum.Parse(typeof(Enum_RainFlag), comboBoxRainFlag.SelectedItem.ToString()); } catch { };
            //Перевычисляем и выводим WetFlag
            comboBoxWetFlag.SelectedItem = BoltwoodObj.WetFlag.ToString();
            //Перевычисляем и выводим RainCond
            comboBoxRainCond.SelectedItem = BoltwoodObj.RainCond.ToString();

            //try { BoltwoodObj.WetFlag_DirectSet = (Enum_WetFlag)Enum.Parse(typeof(Enum_WetFlag), comboBoxWetFlag.SelectedItem.ToString()); } catch { };
        }

        private void comboBoxWetFlag_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Меняем WetFlag
            try { BoltwoodObj.WetFlag_DirectSet = (Enum_WetFlag)Enum.Parse(typeof(Enum_WetFlag), comboBoxWetFlag.SelectedItem.ToString()); } catch { };
            //Перевычисляем и выводим RainFlag
            comboBoxRainFlag.SelectedItem = BoltwoodObj.RainFlag.ToString();
            //Перевычисляем и выводим RainCond
            comboBoxRainCond.SelectedItem = BoltwoodObj.RainCond.ToString();
        }

        private void comboBoxRainCond_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Меняем Rain Cond
            try { BoltwoodObj.RainCond_DirectSet = (Enum_RainCond)Enum.Parse(typeof(Enum_RainCond), comboBoxRainCond.SelectedItem.ToString()); } catch { };

            //Перевычисляем и выводим RainFlag
            comboBoxRainFlag.SelectedItem = BoltwoodObj.RainFlag.ToString();
            //Перевычисляем и выводим WetFlag
            comboBoxWetFlag.SelectedItem = BoltwoodObj.WetFlag.ToString();
        }
        #endregion

        #region Write File commands
        private void btnWriteNow_Click(object sender, EventArgs e)
        {
            BoltwoodFileClass.WirteBoltwoodData(BoltwoodObj.getBoltwoodString());
            txtLastWritten.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        private void timerBolwoodWrite_Tick(object sender, EventArgs e)
        {
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
                    // Переключить режим на ХОРОШИЕ условия
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
                    BoltwoodObj_BadState = BoltwoodObj;

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
                    // Переключить режим
                    BoltwoodObj = (BoltwoodClass)BoltwoodObj_BadState;

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

        private void btnFileDialog_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "Файлы данных (*.txt; *.dat) | *.txt; *.dat";
            saveFileDialog1.InitialDirectory = ( BoltwoodFileClass.BoltwoodFilePath =="" ? BoltwoodFileClass.DefaultFilePath : BoltwoodFileClass.BoltwoodFilePath);
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
            BoltwoodObj.USE_LOGIC = ((CheckBox)sender).Checked;
        }


    }
}
