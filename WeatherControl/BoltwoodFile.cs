using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WeatherControl
{
    static class BoltwoodFileClass
    {
        public static string BoltwoodFileName = "cloudsensor.dat"; //cloud sensor file
        public static string BoltwoodFilePath = "";

        private static TextWriter BoltwoodFile = null;


        public static string DefaultFilePath
        {
            get { return Environment.GetFolderPath(Environment.SpecialFolder.MyDocume‌​nts) + "\\"; }
        }

        /// <summary>
        /// Boltwood data storage procedures
        /// </summary>
        #region Boltwood data section
        private static void OpenBoltwoodFile()
        {
            if (BoltwoodFilePath == "") BoltwoodFilePath = DefaultFilePath;
            string FullFileName = BoltwoodFilePath + BoltwoodFileName;

            try
            {
                BoltwoodFile = File.CreateText(FullFileName);
            }
            catch
            {
                Logging.AddLog("Cannot create boltwood data file");
            }
        }

        private static void CloseBoltwoodFile()
        {
            try
            {
                BoltwoodFile.Close();
            }
            catch
            {
                Logging.AddLog("Cannot close boltwood data file");
            }
            BoltwoodFile = null;
        }

        public static void WirteBoltwoodData(string dataline)
        {
            if (BoltwoodFile == null)
            {
                OpenBoltwoodFile();
            }

            try
            {
                BoltwoodFile.Write(dataline);
            }
            catch
            {
                Logging.AddLog("Cannot write boltwood data file");
            }

            CloseBoltwoodFile();

        }
        #endregion
    }
}
