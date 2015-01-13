using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Windows.Forms;

namespace ObservatoryCenter
{
    public static class Logging
    {
        private static TextWriter LogFile = null;
        public static string LogFileName = "weather_station.log"; //Text log
        public static string LogFilePath = "";
        public static bool LogFileFlag = true;

        private static TextWriter SerialLogFile = null;
        public static string SerialLogFileName = "weather_station_serial_"; //Serial log file
        public static string serialLogExt = ".log";
        public static string SerialLogFilePath = "";
        public static bool SerialLogFileFlag = true;

        private static TextWriter dataLogFile = null;
        public static string dataLogFileName = "weather_station_"; //CSV log data
        public static string dataLogExt = ".csv";
        public static char CSVseparator = ';';
        public static string DataFilePath = "";
        public static bool DataFileFlag = true;

        private static TextWriter BoltwoodFile = null;
        public static string BoltwoodFileName = "cloudsensor"; //cloud sensor file
        public static string BoltwoodFileExt = ".dat";
        public static string BoltwoodFilePath = "";
        public static bool BoltwoodFileFlag = true;

        //DEBUG LEVEL
        public static byte DEBUG_LEVEL = 1;

        private static string ApplicationFilePath
        {
            get { return Application.StartupPath + "\\"; }
        }

        /// <summary>
        /// Error log procedures
        /// </summary>
        #region Error log section
        public static void OpenLogFile()
        {
            if (LogFilePath == "") LogFilePath = ApplicationFilePath;
            LogFile = File.AppendText(LogFilePath + LogFileName);
        }

        public static void CloseLogFile()
        {
            LogFile.Close();
            LogFile = null;
        }

        public static void Log(string logMessage, byte LogLevel=1)
        {
            // if current log level is less then DebugLevel
            if (LogLevel <= DEBUG_LEVEL)
            {

                if (LogFile == null)
                {
                    OpenLogFile();
                }

                LogFile.Write("{0} {1}", DateTime.Now.ToShortDateString(), DateTime.Now.ToLongTimeString());
                LogFile.WriteLine(": {0}", logMessage);
            }
        }
        #endregion

        /// <summary>
        /// Serial log procedures
        /// </summary>
        #region Serial log section
        public static void OpenSerialLogFile()
        {
            if (SerialLogFilePath == "") SerialLogFilePath = ApplicationFilePath;

            string FullFileName = SerialLogFilePath + SerialLogFileName + DateTime.Now.ToShortDateString() + serialLogExt;

            if (!File.Exists(FullFileName))
            {
                SerialLogFile = File.CreateText(FullFileName);
            }
            else
            {
                SerialLogFile = File.AppendText(FullFileName);
            }
        }

        public static void CloseSerialLogFile()
        {
            SerialLogFile.Close();
            SerialLogFile = null;
        }

        public static void LogSerial(string logMessage)
        {
            if (SerialLogFile == null)
            {
                OpenSerialLogFile();
            }

            SerialLogFile.Write("{0} {1}", DateTime.Now.ToShortDateString(), DateTime.Now.ToLongTimeString());
            SerialLogFile.WriteLine(": {0}", logMessage);
        }
        #endregion

        /// <summary>
        /// Data log procedures
        /// </summary>
        #region Data log section
        public static void OpenDataLogFile(string headerline)
        {
            if (DataFilePath == "") DataFilePath = ApplicationFilePath;

            string FullFileName = DataFilePath + dataLogFileName + DateTime.Now.ToShortDateString()+dataLogExt;

            if (!File.Exists(FullFileName))
            {
                dataLogFile = File.CreateText(FullFileName);
                dataLogFile.WriteLine(headerline);
            }
            else
            {
                dataLogFile = File.AppendText(FullFileName);
            }
        }

        public static void CloseDataLogFile()
        {
            dataLogFile.Close();
            dataLogFile = null;
        }

        public static void LogData(string dataline, string headerline)
        {
            if (dataLogFile == null)
            {
                OpenDataLogFile(headerline);
            }
            dataLogFile.WriteLine("{0} {1:H:mm:ss}" + CSVseparator + " {2}", DateTime.Now.ToShortDateString(), DateTime.Now, dataline);

            CloseDataLogFile();

        }
        #endregion

        /// <summary>
        /// Boltwood data storage procedures
        /// </summary>
        #region Boltwood data section
        public static void OpenBoltwoodFile()
        {
            if (BoltwoodFilePath == "") BoltwoodFilePath = ApplicationFilePath;
            string FullFileName = BoltwoodFilePath + BoltwoodFileName + BoltwoodFileExt;

            BoltwoodFile = File.CreateText(FullFileName);
        }

        public static void CloseBoltwoodFile()
        {
            BoltwoodFile.Close();
            BoltwoodFile = null;
        }

        public static void WirteBoltwoodData(string dataline)
        {
            if (BoltwoodFile == null)
            {
                OpenBoltwoodFile();
            }
            BoltwoodFile.Write(dataline);

            CloseBoltwoodFile();

        }
        #endregion
    
    }
}
