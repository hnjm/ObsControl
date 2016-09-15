using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace ObservatoryCenter
{
    public static class ObsSettings
    {
        private static ExeConfigurationFileMap configMap;
        private static Configuration config;

        public static string CONFIG_FILENAME = "ObservatoryControl.config";
        public static string CONFIG_PATH = Path.Combine(Environment.CurrentDirectory, "config")+"\\";

        public static bool __Load()
        {
            bool res = false;
            try
            {
                var var1Value = config.AppSettings.Settings["Var1"].Value;
                var var2Value = config.AppSettings.Settings["Var2"].Value;
                //var conn1 = config.ConnectionStrings.Settings["SQLConnectionString01"];
                //var conn2 = config.ConnectionStrings.Settings["SQLConnectionString02"];

                res = true;
            }
            catch (Exception ex)
            {
                Logging.AddLog("Load configuration error: " + ex.Message, LogLevel.Important, Highlight.Error);
                Logging.AddLog("Exception details: " + ex.ToString(), LogLevel.Debug, Highlight.Debug);
                res = false;
            }
            return res;
        }


        public static bool Init()
        {
            bool res = false;
            try
            {
                configMap = new ExeConfigurationFileMap();
                configMap.ExeConfigFilename = CONFIG_PATH + CONFIG_FILENAME;

                config = ConfigurationManager.OpenMappedExeConfiguration(configMap, ConfigurationUserLevel.None);
            }
            catch (Exception ex)
            {
                Logging.AddLog("Init configuration error: " + ex.Message, LogLevel.Important, Highlight.Error);
                Logging.AddLog("Exception details: " + ex.ToString(), LogLevel.Debug, Highlight.Debug);
                res = false;
            }
            return res;
        }


        public static bool Save()
        {
            return false;
        }

        public static string getString(string key)
        {
            string res = null;
            try
            {
                res = config.AppSettings.Settings[key].Value;
            }
            catch (Exception ex)
            {
                Logging.AddLog("getString ["+key+"] parameter error: " + ex.Message, LogLevel.Important, Highlight.Error);
                Logging.AddLog("Exception details: " + ex.ToString(), LogLevel.Debug, Highlight.Debug);
                res = null;
            }
            return res;
        }

        public static bool? getBool(string key)
        {
            bool? res = null;
            try
            {
                string st = config.AppSettings.Settings[key].Value;
                res = Convert.ToBoolean(st);
            }
            catch (Exception ex)
            {
                Logging.AddLog("getBool [" + key + "] parameter error: " + ex.Message, LogLevel.Important, Highlight.Error);
                Logging.AddLog("Exception details: " + ex.ToString(), LogLevel.Debug, Highlight.Debug);
                res = null;
            }
            return res;
        }

        public static int? getInt(string key)
        {
            int? res = null;
            try
            {
                string st = config.AppSettings.Settings[key].Value;
                res = Convert.ToInt32(st);
            }
            catch (Exception ex)
            {
                Logging.AddLog("getInt [" + key + "] parameter error: " + ex.Message, LogLevel.Important, Highlight.Error);
                Logging.AddLog("Exception details: " + ex.ToString(), LogLevel.Debug, Highlight.Debug);
                res = null;
            }
            return res;
        }


        public static double? getDouble(string key)
        {
            double? res = null;
            try
            {
                string st = config.AppSettings.Settings[key].Value;
                res = Convert.ToDouble(st);
            }
            catch (Exception ex)
            {
                Logging.AddLog("getDouble [" + key + "] parameter error: " + ex.Message, LogLevel.Important, Highlight.Error);
                Logging.AddLog("Exception details: " + ex.ToString(), LogLevel.Debug, Highlight.Debug);
                res = null;
            }
            return res;
        }
        public static void setAppSetting(string key, string value)
        {
            //Save AppSettings
            if (config.AppSettings.Settings[key] != null)
            {
                config.AppSettings.Settings.Remove(key);
            }
            config.AppSettings.Settings.Add(key, value);
            config.Save(ConfigurationSaveMode.Modified);
        }


    }
}
