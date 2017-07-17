using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WeatherControl
{
    public partial class FormWeatherFileControl : Form
    {

        internal DateTime CurrentTime = new DateTime();
        internal DateTime LastWriteTime = new DateTime(2017,02,17);
        internal TimeSpan SinceLastWrite;

        public FormWeatherFileControl()
        {
            InitializeComponent();
        }

        private void timerTime_Tick(object sender, EventArgs e)
        {
            //Указать текущее время
            CurrentTime = DateTime.Now;
            txtDate.Text = CurrentTime.ToString("yyyy-MM-dd");
            txtTime.Text = CurrentTime.ToString("HH:mm:ss");

            //Пересчитать точку росы
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
        }

        private void FormWeatherFileControl_Load(object sender, EventArgs e)
        {
            txtLastWritten.Text = LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss");

            comboBoxRainFlag.DataSource = Enum.GetNames(typeof(RainFlag));
            comboBoxWetFlag.DataSource = Enum.GetNames(typeof(WetFlag));

            comboBoxCloudCond.DataSource = Enum.GetNames(typeof(CloudCond));
            comboBoxWindCond.DataSource = Enum.GetNames(typeof(WindCond));
            comboBoxRainCond.DataSource = Enum.GetNames(typeof(RainCond));
            comboBoxDaylightCond.DataSource = Enum.GetNames(typeof(DayCond));

            comboBoxRoofCloseFlag.DataSource = Enum.GetNames(typeof(RoofFlag));
            comboBoxAlertFlag.DataSource = Enum.GetNames(typeof(AlertFlag));
        }

        private void btnSaveStatus_MouseClick(object sender, MouseEventArgs e)
        {

        }

        Color WaitingColor=Color.CadetBlue;
        Color OnColor = Color.DarkSeaGreen;
        Color OffColor = Color.Tomato;
        Color DisabledColor = Color.LightGray;

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
                    // Записать
                    // to do

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
                    // to do

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
                    // Записать
                    // to do

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
                    // to do

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
    }
}
