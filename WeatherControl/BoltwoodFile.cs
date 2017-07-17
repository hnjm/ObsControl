using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WeatherControl
{
    class BoltwoodFileClass
    {
        private static TextWriter BoltwoodFile = null;
        public static string BoltwoodFileName = "cloudsensor"; //cloud sensor file
        public static string BoltwoodFileExt = ".dat";
        public static string BoltwoodFilePath = "";
        public static bool BoltwoodFileFlag = true;


        private static string ApplicationFilePath
        {
            get { return Application.StartupPath + "\\"; }
        }

        /// <summary>
        /// Boltwood data storage procedures
        /// </summary>
        #region Boltwood data section
        public static void OpenBoltwoodFile()
        {
            if (BoltwoodFilePath == "") BoltwoodFilePath = ApplicationFilePath;
            string FullFileName = BoltwoodFilePath + BoltwoodFileName + BoltwoodFileExt;

            try
            {
                BoltwoodFile = File.CreateText(FullFileName);
            }
            catch
            {
                Logging.AddLog("Cannot create boltwood data file");
            }
        }

        public static void CloseBoltwoodFile()
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
