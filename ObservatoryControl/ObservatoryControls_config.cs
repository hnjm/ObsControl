using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace ObservatoryCenter
{

    /// <summary>
    /// Config based on custom XML file
    /// </summary>
    public static class ObsConfig
    {




        public static XmlDocument configXML = new XmlDocument();

        // Есть одна особенность хранения файла (во вермя разработки, по крайней мере)
        // Чтобы он синхронизировался через GITHUB он должен лежать там, где хранится весь SourceCode ("c:\Users\Emchenko Boris\Source\Repos\ObsControl\ObservatoryControl\ObservatoryControl.config")
        // Но для того, чтоыб он подгружался во время запуска,  он должен лежать в "c:\Users\Emchenko Boris\Source\Repos\ObsControl\ObservatoryControl\bin\Debug\config" (ну или Release\config)
        // Поэтому нужно помнить, что из два и синхронизировать правки (просто копируя его)
        public static string CONFIG_FILENAME = "ObservatoryControl.config";
        public static string CONFIG_PATH = Path.Combine(Environment.CurrentDirectory, "config") + "\\";

        
        public static bool Load()
        {
            bool res = false;
            try
            {
                configXML.Load(CONFIG_PATH + CONFIG_FILENAME);
                return true;
            }
            catch (System.IO.FileNotFoundException ex)
            {
                Logging.AddLog("No configuration file found",LogLevel.Important,Highlight.Error);
                Logging.AddLog("Exception details: " + ex.ToString(), LogLevel.Debug, Highlight.Debug);
                res = false;
            }
            catch (Exception ex)
            {
                Logging.AddLog("Load configuration error: " + ex.Message, LogLevel.Important, Highlight.Error);
                Logging.AddLog("Exception details: " + ex.ToString(), LogLevel.Debug, Highlight.Debug);
                res = false;
            }
            return res;
        }

        public static bool Save()
        {
            bool res = false;
            try
            {
                configXML.Save(CONFIG_PATH + CONFIG_FILENAME);
                return true;
            }
            catch (System.IO.FileNotFoundException ex)
            {
                Logging.AddLog("No configuration file found", LogLevel.Important, Highlight.Error);
                Logging.AddLog("Exception details: " + ex.ToString(), LogLevel.Debug, Highlight.Debug);
                res = false;
            }
            catch (Exception ex)
            {
                Logging.AddLog("Save configuration error: " + ex.Message, LogLevel.Important, Highlight.Error);
                Logging.AddLog("Exception details: " + ex.ToString(), LogLevel.Debug, Highlight.Debug);
                res = false;
            }
            return res;
        }

        public static string getString(string section, string key)
        {
            string res = null;
            try
            {
                XmlNode nodeAppSet = configXML.SelectSingleNode("//"+ section);
                res = nodeAppSet[key].Attributes["value"].Value;
            }
            catch (Exception ex)
            {
                Logging.AddLog("getString [" + key + "] error: " + ex.Message, LogLevel.Important, Highlight.Error);
                Logging.AddLog("Exception details: " + ex.ToString(), LogLevel.Debug, Highlight.Debug);
                res = null;
            }
            return res;
        }

        public static bool? getBool(string section, string key)
        {
            bool? res = null;
            try
            {
                XmlNode nodeAppSet = configXML.SelectSingleNode("//" + section);
                string st = nodeAppSet[key].Attributes["value"].Value;
                res = Convert.ToBoolean(st);
            }
            catch (Exception ex)
            {
                Logging.AddLog("getBool [" + key + "] error: " + ex.Message, LogLevel.Important, Highlight.Error);
                Logging.AddLog("Exception details: " + ex.ToString(), LogLevel.Debug, Highlight.Debug);
                res = null;
            }
            return res;
        }

        public static int? getInt(string section, string key)
        {
            int? res = null;
            try
            {
                XmlNode nodeAppSet = configXML.SelectSingleNode("//" + section);
                string st = nodeAppSet[key].Attributes["value"].Value;
                res = Convert.ToInt32(st);
            }
            catch (Exception ex)
            {
                Logging.AddLog("getInt [" + key + "] error: " + ex.Message, LogLevel.Important, Highlight.Error);
                Logging.AddLog("Exception details: " + ex.ToString(), LogLevel.Debug, Highlight.Debug);
                res = null;
            }
            return res;
        }

        public static double? getDouble(string section, string key)
        {
            double? res = null;
            try
            {
                XmlNode nodeAppSet = configXML.SelectSingleNode("//" + section);
                string st = nodeAppSet[key].Attributes["value"].Value;
                res = Convert.ToDouble(st);
            }
            catch (Exception ex)
            {
                Logging.AddLog("getDouble [" + key + "] error: " + ex.Message, LogLevel.Important, Highlight.Error);
                Logging.AddLog("Exception details: " + ex.ToString(), LogLevel.Debug, Highlight.Debug);
                res = null;
            }
            return res;
        }

        private static XmlDocument __loadConfigDocument()
        {
            XmlDocument doc = null;
            try
            {
                doc = new XmlDocument();
                doc.Load(CONFIG_PATH + CONFIG_FILENAME);
                return doc;
            }
            catch (System.IO.FileNotFoundException e)
            {
                throw new Exception("No configuration file found.", e);
            }
            catch (Exception ex)
            {

                return null;
            }
        }


    }





////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// OBSOLETE CLASSS
    /// </summary>
    public static class ObsSettings_old
    {
        private static ExeConfigurationFileMap configMap;
        public static Configuration config;

        public static string CONFIG_FILENAME = "ObservatoryControl.config";
        public static string CONFIG_PATH = Path.Combine(Environment.CurrentDirectory, "config") + "\\";

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
                Logging.AddLog("getString [" + key + "] error: " + ex.Message, LogLevel.Important, Highlight.Error);
                Logging.AddLog("Exception details: " + ex.ToString(), LogLevel.Debug, Highlight.Debug);
                res = null;
            }
            return res;
        }

        public static string[] getArray()
        {
            string[] res = null;
            try
            {
                res = config.AppSettings.Settings.AllKeys;
            }
            catch (Exception ex)
            {
                Logging.AddLog("getArray error: " + ex.Message, LogLevel.Important, Highlight.Error);
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
                Logging.AddLog("getBool [" + key + "] error: " + ex.Message, LogLevel.Important, Highlight.Error);
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
                Logging.AddLog("getInt [" + key + "] error: " + ex.Message, LogLevel.Important, Highlight.Error);
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
                Logging.AddLog("getDouble [" + key + "] error: " + ex.Message, LogLevel.Important, Highlight.Error);
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
