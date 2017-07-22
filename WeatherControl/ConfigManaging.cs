using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace WeatherControl
{

    /// <summary>
    /// Config based on custom XML file
    /// </summary>
    public static class ConfigManagement
    {

        public static string ProgDocumentsFolderName = "ObservatoryControl"; //set this property to change 
        public static string ProgDocumentsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), ProgDocumentsFolderName) + "\\";


        public static XmlDocument configXML = new XmlDocument();

        // Есть одна особенность хранения файла (во вермя разработки, по крайней мере)
        // 1. Чтобы он синхронизировался через GITHUB он должен лежать там, где хранится весь SourceCode ("c:\Users\Emchenko Boris\Source\Repos\ObsControl\ObservatoryControl\ObservatoryControl.config")
        // 2. Но для того, чтоыб он подгружался во время запуска,  он должен лежать в "c:\Users\Emchenko Boris\Source\Repos\ObsControl\ObservatoryControl\bin\Debug\config" (ну или Release\config)
        // 3. И еще есть дефолтный конфиг, который имеет расширение txt так как только такие файлы ClickOnce может добавлять в инсталяцию
        // Поэтому нужно помнить, что их ТРИ и синхронизировать правки (просто копируя их)
        //
        // UPDATE. Начиная с версии 0.7.2 поменялось:
        // 1. Дефольный конфиг под именем ObservatoryControl.defaultconfig.txt" при разработке храниться в паке с исходным кодом (чтобы он синхронизировался через GITHUB он должен лежать там, где хранится весь SourceCode) ("c:\Users\Emchenko Boris\Source\Repos\ObsControl\ObservatoryControl\ObservatoryControl.defaultconfig.txt")
        // 2. При компиляции он копируется в \Source\Repos\ObsControl\ObservatoryControl\bin\Debug\ 
        // 3. A рабочий лежит в \Documents\ObservatoryControl\Config\ObservatoryControl.config 
        // Обновлять лучше так: редактируем дефолтный (txt) в папке с SourceCode (ПОМНИ НЕ .../DEBUG!!!), при компиляции он скопируется сам, а рабочий просто удаляем (при запуске перепишется). Ну или рабочий копировать в текстовый, но опять же - в папку с SourceCode.

        public static string CONFIG_FILENAME = "ObservatoryControl.config";
        public static string CONFIG_PATH = Path.Combine(ProgDocumentsPath, "Config") + "\\";
        public static string DEFAULT_CONFIG_FILENAME = "ObservatoryControl.defaultconfig.txt"; //Default config file

        /// <summary>
        /// Load configuration XML file
        /// </summary>
        /// <returns>true if loaded, false if error</returns>
        public static bool Load()
        {
            bool res = false;
            try
            {
                configXML.Load(Path.Combine(CONFIG_PATH,CONFIG_FILENAME));
                Logging.AddLog("Configuration file ["+ CONFIG_PATH + CONFIG_FILENAME + "] loaded", LogLevel.Activity);
                return true;
            }
            catch (Exception ex)
            {
                if (ex is System.IO.FileNotFoundException || ex is System.IO.DirectoryNotFoundException)
                {
                    Logging.AddLog("No configuration file found", LogLevel.Important, Highlight.Error);

                    CopyDefaultConfig();

                    try
                    {
                        configXML.Load(Path.Combine(CONFIG_PATH, CONFIG_FILENAME));
                        return true;
                    }
                    catch (Exception ex2)
                    {
                        Logging.AddLog("Load configuration error: " + ex2.Message, LogLevel.Important, Highlight.Error);
                        Logging.AddLog("Exception details: " + ex2.ToString(), LogLevel.Debug, Highlight.Debug);
                    }
                }
                else
                {
                    Logging.AddLog("Load configuration error: " + ex.Message, LogLevel.Important, Highlight.Error);
                    Logging.AddLog("Exception details: " + ex.ToString(), LogLevel.Debug, Highlight.Debug);
                }
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
            catch (Exception ex)
            {
                if (ex is System.IO.FileNotFoundException || ex is System.IO.DirectoryNotFoundException)
                {
                    Logging.AddLog("No configuration file found", LogLevel.Important, Highlight.Error);
                    Logging.AddLog("Exception details: " + ex.ToString(), LogLevel.Debug, Highlight.Debug);
                    res = false;
                }
                else
                {
                    Logging.AddLog("Save configuration error: " + ex.Message, LogLevel.Important, Highlight.Error);
                    Logging.AddLog("Exception details: " + ex.ToString(), LogLevel.Debug, Highlight.Debug);
                }
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
                Logging.AddLog("Config parameter [" + section + "][" + key + "] not found: " + ex.Message, LogLevel.Activity, Highlight.Error);
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

        public static XmlNode getXMLNode(string section)
        {
            XmlNode res = null;
            try
            {
                XmlNode nodeAppSet = configXML.SelectSingleNode("//" + section);
                res = nodeAppSet;
            }
            catch (Exception ex)
            {
                Logging.AddLog("getXMLNode [" + section + "] error: " + ex.Message, LogLevel.Important, Highlight.Error);
                Logging.AddLog("Exception details: " + ex.ToString(), LogLevel.Debug, Highlight.Debug);
                res = null;
            }
            return res;
        }



        /// <summary>
        /// Copy default config to CONFIG dir
        /// </summary>
        private static void CopyDefaultConfig()
        {
            if (!File.Exists(Path.Combine(CONFIG_PATH, CONFIG_FILENAME)))
            {
                if (!Directory.Exists(CONFIG_PATH))
                {
                    Logging.AddLog("Config folder [" + CONFIG_PATH + "] not found. Recreating structure", LogLevel.Important, Highlight.Emphasize);
                    //Create dir structure if needed
                    CreateDocumentsDirStructure();
                }

                //Copy default config
                try
                {
                    File.Copy(Path.Combine(Environment.CurrentDirectory, DEFAULT_CONFIG_FILENAME), Path.Combine(CONFIG_PATH, CONFIG_FILENAME));
                    Logging.AddLog("Default config was copied", LogLevel.Important, Highlight.Emphasize);
                }
                catch (Exception ex)
                {
                    Logging.AddLog("Default config copying error: " + ex.Message, LogLevel.Important, Highlight.Error);
                    Logging.AddLog("Exception details: " + ex.ToString(), LogLevel.Debug, Highlight.Debug);
                }
            }

        }



        /// <summary>
        /// Создает папку, в которую будут писаться логи, храниться конфиги и т.д.
        /// </summary>
        /// <returns></returns>
        public static bool CreateDocumentsDirStructure()
        {
            bool wasCreated = false;

            try { 
                //Is - Documents/ObservatoryControl
                if (!Directory.Exists(ProgDocumentsPath))
                {
                    Directory.CreateDirectory(ProgDocumentsPath);
                    Logging.AddLog("Root directory [" + ProgDocumentsPath + "] created", LogLevel.Important, Highlight.Emphasize);
                    wasCreated = true;
                }

                //Is - Documents/ObservatoryControl/Logs
                if (!Directory.Exists(Logging.LogFilePath))
                {
                    Directory.CreateDirectory(Logging.LogFilePath);
                    Logging.AddLog("Log directory [" + Logging.LogFilePath + "] created", LogLevel.Important, Highlight.Emphasize);
                    wasCreated = true;
                }

                //Is - Documents/ObservatoryControl/Config
                if (!Directory.Exists(CONFIG_PATH))
                {
                    Directory.CreateDirectory(CONFIG_PATH);
                    Logging.AddLog("Config directory [" + CONFIG_PATH + "] created", LogLevel.Important, Highlight.Emphasize);
                    wasCreated = true;
                }
            }
            catch (Exception ex)
            {
                Logging.AddLog("Create directory structure error: " + ex.Message, LogLevel.Important, Highlight.Error);
                Logging.AddLog("Exception details: " + ex.ToString(), LogLevel.Debug, Highlight.Debug);
            }

            return wasCreated;
        }
    }





}
