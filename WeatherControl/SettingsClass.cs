using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WeatherControl
{
    static class SettingsClass
    {
        static public string CREATEREPORT_FILENAME = "CreateReport4.xls";
        static public string CREATEREPORT_PATH = @"d:\_DC\";

        /*
        static public void Load()
        {
            string st = "";


            //CREATEREPORT_FILENAME
            try
            { 
                st = Properties.Settings.Default.CreateReport_FileName;
            }
            catch {}
            
            if (st !="" )
            {
                CREATEREPORT_FILENAME = st;
            } //else, leave default name


            //CREATEREPORT_PATH
            try
            {
                st = Properties.Settings.Default.CreateReport_Path;
            }
            catch { }

            if (st != "")
            {
                CREATEREPORT_PATH = st;
            }
            else
            {
                CREATEREPORT_PATH = getAssemblyPath();
            }
        }

        internal static string getAssemblyPath()
        {
            var StPath = System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase;
            var directory = System.IO.Path.GetDirectoryName(StPath) + "\\";
            var path = new Uri(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase)).LocalPath + "\\";

            return path;
        }



        internal static void CopyCreateReportFile(string destinationFile)
        {
            try { 
                File.Copy(CREATEREPORT_PATH+CREATEREPORT_FILENAME, destinationFile);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Ошибка сохранения файла отчета [" + CREATEREPORT_FILENAME + "] в ["+ destinationFile + "]", "Ошибка сохранения файла обработчика отчета", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            MessageBox.Show("Файл [" + CREATEREPORT_FILENAME + "] сохранен в [" + destinationFile + "]", "Файл сохранен", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

    */


        static internal void LoadSettings()
        {

        }
    }
}
