using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;
using System.Threading;

namespace ObservatoryCenter
{

    public class CCDC_request_class
    {
        //Максимальное время на исполнение запроса
        public int RequestTimeout =1500; //sec

        private bool RequestActiveFlag = false;
        public DateTime RequestedTime = new DateTime(2015, 1, 1, 0, 0, 1);
        public DateTime CanceledTime = new DateTime(2015, 1, 1, 0, 0, 1);
        public DateTime FulfiledTime = new DateTime(2015, 1, 1, 0, 0, 1);

        public bool RequestWasFulfiled = false;
        public bool RequestWasSuccessful = false; 

        public bool RequestActive
        { 
        get 
            {
                return RequestActiveFlag;
            }
        set
            {
                RequestActiveFlag = value;
                //Установить потребность
                if (value)
                {
                    RequestedTime = DateTime.Now;
                    RequestWasFulfiled = false;
                    RequestWasSuccessful = false;
                }
                //Отменить потребность
                else
                {
                    CanceledTime = DateTime.Now;
                    RequestWasFulfiled = false;
                    RequestWasSuccessful = false;
                }

            }
        }

        //Пометить выполенным
        public void MarkFulfiled(bool MarkSuccessful = true)
        {
            RequestActiveFlag = false;
            FulfiledTime = DateTime.Now;
            RequestWasFulfiled = true;
            RequestWasSuccessful = MarkSuccessful;
            CanceledTime = new DateTime(2015, 1, 1, 0, 0, 1);
        }
    }


    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //
    // CCD Commander class
    //
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// CCD Commander class
    /// </summary>
    public class CCDC_ExternatApplication : ExternalApplication
    {
        public ObservatoryControls ParentObsControl;

        //Path lo log dir
        public string LogPath = @"c:\CCD Commander\Logs";

        //Current log file link
        public FileInfo currentLogFile;
        private IEnumerable<string> contentsLogFile;
        public DateTime currentLogFileLastModTime = new DateTime(2015, 1, 1, 0, 0, 1);
        //private IEnumerable<string> contentsLogFile_Reversed;

        public int _MAX_VALID_CCDC_LOGFILE_NAME_AGE = 3600 * 12; //how old log file can be (in its name) in seconds
        public int _MAX_VALID_CCDC_LOGFILE_MOD_AGE = 3000;       //how old log file can be (from last change) in seconds

        private Int32 prevLinesCount = 0;
        public int _MAX_CCDC_LOGLINES_PARSE_ATATIME_LIMIT = 100; //parse only 5 last lines (need to be limited if started during session)

        //Path to actions
        public string ActionsPath = @"c:\CCD Commander\Actions";
        //Last action file link
        public FileInfo lastActionFile;
        //Path to guide start script (phd_broker_end_slew.bat)
        public string StartGuideScript;

        public string MessageText;

        //Log parse information
        //pointing coordinates
        public string ObjName = "";
        public string ObjRA_st = "";
        public string ObjDec_st = "";
        public double ObjRA = 0.0;
        public double ObjDec = 0.0;
        private bool bMoveTo_beg = false;
        private bool bMoveTo_end = true;
        private bool bCoordWasntParsed = false;

        //pointing accuracy
        public double LastPointingError = 0.0;  //104.3 arcsec
        //focus info
        public double LastFocusHFD = 0.0;       //2.9 
        public DateTime LastFocusTime = new DateTime(2015, 1, 1, 0, 0, 1);

        //Current image attributes
        public string LastExposure_ImageType = "";      //Light, Dark, ...
        public string LastExposure_bin = "";       //1x1
        public string LastExposure_filter = "";    //Red
        public Int32 LastExposure_ExposureLength = 0;  //60 seconds
        public DateTime LastExposureStartTime = new DateTime(2015,1,1,0,0,1);
        public string LastImageName = "";       //M33_20171119_Red_60s_1x1_-30degC_0,0degN
        public string LastSequenceInfo = "";    //(1 of 10)

        //End imaging exposure event
        public DateTime LastExposureEndTime = new DateTime(2015, 1, 1, 0, 0, 1);
        
        //Flip time
        public DateTime LastFlipStartTime = new DateTime(2015, 1, 1, 0, 0, 1);
        public DateTime LastFlipInternalRoutineStartTime = new DateTime(2015, 1, 1, 0, 0, 1);
        
        //Start/End time of actionlist
        public DateTime ActionRunStartTime  = new DateTime(2015, 1, 1, 0, 0, 1);
        public DateTime ActionRunEndTime = new DateTime(2015, 1, 1, 0, 0, 1);

        //Request events
        public CCDC_request_class Request_StopAfterImage = new CCDC_request_class();
        public CCDC_request_class Request_StartAfterStop = new CCDC_request_class();
        public CCDC_request_class Request_AbortAfterStop = new CCDC_request_class();


        public CCDC_ExternatApplication(ObservatoryControls OCLink) : base()
        {
            ParentObsControl = OCLink;
        }

        /// <summary>
        /// Init CCDC activity
        /// </summary>
        public void Init()
        {

        }

        /// <summary>
        /// Return FileInfo on last CCDC ACTION 
        /// </summary>
        /// <returns></returns>
        public FileInfo GetLastActionFile()
        {
            DirectoryInfo objActionsDirectory;

            try
            {
                //Get directory where to search actions
                objActionsDirectory = new DirectoryInfo(ActionsPath);

                //Get last file
                lastActionFile = (from f in objActionsDirectory.GetFiles() orderby f.LastWriteTime descending select f).First();
            }
            catch (Exception ex)
            {
                Logging.AddLog("CCDC ActionsPath is invalid", LogLevel.Debug, Highlight.Error);
                Logging.AddLog(MethodBase.GetCurrentMethod().Name + "error! [" + ex.ToString() + "]", LogLevel.Debug, Highlight.Error);
                lastActionFile = new FileInfo(ActionsPath + "");
            }

            return lastActionFile;
        }

        /// <summary>
        /// Emulate START | PAUSE | STOP commands
        /// </summary>
        public void Automation_Start()
        {
            Utils.BringWindowToFront("CCDCommander", "CCD Commander", 1);

            SendKeys.SendWait("%R");
            Thread.Sleep(100);
            SendKeys.SendWait("S");
            Thread.Sleep(100);

            SendKeys.Flush();
            Logging.AddLog("CCDC start pressed", LogLevel.Activity);
        }
        public void Automation_Pause()
        {
            Utils.BringWindowToFront("CCDCommander", "CCD Commander", 1);
            SendKeys.SendWait("%R");
            Thread.Sleep(100);
            SendKeys.SendWait("P");
            Thread.Sleep(100);

            SendKeys.Flush();
            Logging.AddLog("CCDC pause pressed", LogLevel.Activity);

        }
        public void Automation_Stop()
        {
            Utils.BringWindowToFront("CCDCommander","CCD Commander",1);
            SendKeys.SendWait("%R");
            Thread.Sleep(100);
            SendKeys.SendWait("T");
            Thread.Sleep(100);

            Logging.AddLog("CCDC stop pressed", LogLevel.Activity);



        }

        /// <summary>
        /// Restart session when flip detected
        /// </summary>
        private void HandleFlip()
        {
            //CCDC на паузу
            this.Automation_Pause();

            //Запустить скрипт: PHD начать гидирование
            Process objProcess = new Process();
            try
            {
                objProcess.StartInfo.FileName = this.StartGuideScript;
                objProcess.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
                objProcess.StartInfo.UseShellExecute = true;
                objProcess.Start();
                Logging.AddLog("Process [" + objProcess.StartInfo.FileName + "] started", LogLevel.Activity);
            }
            catch (Exception Ex)
            {
                ErrorSt = "Process [" + this.StartGuideScript + "] starting error! " + Ex.Message;
                Error = -1;
                Logging.AddLog(ErrorSt, LogLevel.Important, Highlight.Error);
            }
            //подождать, пока он закончится
            objProcess.WaitForExit(10000);
            objProcess.Close();

            Thread.Sleep(5000); //hang the system to emulate Start Image Delay

            //CCDC запустить
            this.Automation_Start();

            //Thread.Sleep(1000);
        }
        
        /// <summary>
        /// Async start
        /// </summary>
        private void HandleFlip_async()
        {
            Thread childThread = new Thread(delegate () {
                HandleFlip();
            });
            childThread.Start();

        }


        public void AbortRun(string set)
        // set = 1 abort
        // set = 0 clear
        {
            //HKEY_CURRENT_USER\Software\VB and VBA Program Settings\CCDCommander\Test\Aborted

            // Не готово пока, нужно писать еще и тестировать

            RegistryKey abortKey = Registry.CurrentUser.OpenSubKey("Software\\VB and VBA Program Settings\\CCDCommander\\Test", true);
            if (abortKey != null)
            {
                abortKey.SetValue("Aborted", "1", RegistryValueKind.String);
                abortKey.Close();
            }
        }


        // ************************************************************************************************************************************************************************
        // Логика работы с CCDC логами:
        // 1. Берем последний лог файл и заносим его в [FileInfo currentLogFile]                                    - GetLastLogFile()
        // 2. Проверяем его актуальность                                                                            - checkLogFileIsValid()
        // 3. Читаем его содержимое в [IEnumerable<string> contentsLogFile]                                         - ReadLogFileContents();
        // 4. Парсим содержимое и возвращаем [List<string> NewLines], который должен быть выведен в текст лога      - ParseLogFile()
        // ************************************************************************************************************************************************************************
        #region *** Working with log ****
        /// <summary>
        /// Return FileInfo on last LOG FILE from CCDC
        /// </summary>
        /// <returns></returns>
        public FileInfo GetLastLogFile()
        {
            DirectoryInfo objLogDirectory;

            try
            {
                //Get directory where to search logs
                objLogDirectory = new DirectoryInfo(LogPath);
                objLogDirectory.Refresh(); //need to be run, because file data is cached in system

                //Get last file
                //FileInfo tmpCurrentLogFile = (from f in objLogDirectory.GetFiles() orderby f.LastWriteTime descending select f).First();

                //Get list of all files sorting by LastWriteTime desc
                FileInfo[] logFilesListArr = objLogDirectory.GetFiles().OrderByDescending(p => p.LastWriteTime).ToArray();

                //Loop through list
                foreach (FileInfo curFile in logFilesListArr)
                {
                    if (curFile.Extension == ".log")
                    {
                        //Check - was it changed from previos? (new session was started)
                        if (currentLogFile == null || curFile.FullName != currentLogFile.FullName)
                        {
                            // if yes - reset counter
                            prevLinesCount = 0;
                            //set new current lig file
                            currentLogFile = curFile;
                        }

                        //only one log file is needed, so break
                        break;
                    }
                }
                currentLogFile.Refresh(); //need to be run, because file data is cached in system
            }
            catch (Exception ex)
            {
                Logging.AddLog("CCDC LogPath is invalid", LogLevel.Important, Highlight.Error);
                Logging.AddLog(MethodBase.GetCurrentMethod().Name + "error! [" + ex.ToString() + "]", LogLevel.Debug, Highlight.Error);
            }

            return currentLogFile;
        }

        /// <summary>
        /// Check - if selected log file is current running log file
        /// </summary>
        /// <returns></returns>
        public bool checkLogFileIsValid()
        {
            bool resNameAgeValid = false;
            bool resChangeAgeValid = false;
            bool resFinal = false;
            DateTime LogDate = new DateTime();

            // Get file name only
            // format ccdap20170212_124827
            string curName = Path.GetFileNameWithoutExtension(currentLogFile.Name);


            //1. Check filename (should begin with integer)
            int dummyint;
            if (Int32.TryParse(curName.Substring(0, 5), out dummyint))
            {
                //1.1 Check date from filename age
                if (DateTime.TryParseExact(curName, "yyMMdd_HHmmss", CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal, out LogDate))
                {
                    TimeSpan LogFileAge = DateTime.Now - LogDate;
                    if (LogFileAge.TotalSeconds > _MAX_VALID_CCDC_LOGFILE_NAME_AGE)
                    {
                        //log file is too old
                        resNameAgeValid = false;
                    }
                    else
                    {
                        resNameAgeValid = true;
                    }
                }
                else
                {
                    resNameAgeValid = false; //date invalid
                }

                //1.2. Check file change date
                currentLogFileLastModTime = currentLogFile.LastWriteTime;
                int sinceMod = (int)(DateTime.Now - currentLogFileLastModTime).TotalSeconds;
                resChangeAgeValid = (sinceMod < _MAX_VALID_CCDC_LOGFILE_MOD_AGE);
            }
            else
            {
                //имя файла не по шаблону
                resNameAgeValid = false;
            }

            resFinal = resNameAgeValid && resChangeAgeValid;

            return resFinal;
        }

        /// <summary>
        /// Read log file contents
        /// </summary>
        internal void ReadLogFileContents()
        {
            List<String> lst = new List<string>();

            try
            { 
                //Прочитаем содержимое файла 
                FileStream fs = new FileStream(currentLogFile.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                StreamReader sr = new StreamReader(fs);

                while (!sr.EndOfStream)
                    lst.Add(sr.ReadLine());
            }
            catch (Exception ex)
            {
                Logging.AddLog("Cant read logfile contents [" + currentLogFile.FullName + "]", LogLevel.Important, Highlight.Error);
                Logging.AddLog(MethodBase.GetCurrentMethod().Name + "error! [" + ex.ToString() + "]", LogLevel.Debug, Highlight.Error);
            }
            contentsLogFile = lst;

        }

        /// <summary>
        /// Check - does log file ended?
        /// </summary>
        /// <returns></returns>
        internal bool ____checkLogFileEnded()
        {
            bool resLogFileEnded = false;

            if (contentsLogFile.Count() == 0) ReadLogFileContents();

            //скопируем и поменяем порядок строк на обратный
            List<string> contentsLogFileReversed = new List<string>(contentsLogFile);
            contentsLogFileReversed.Reverse();

            //Прочитаем последние 3 строки и проверим его
            var LastThreeLines = contentsLogFileReversed.Take(3);

            foreach (string StLn in LastThreeLines)
            {
                if (StLn.Contains("Action complete.") || StLn.Contains("Action stopped.") || StLn.Contains("Stopping."))
                {
                    resLogFileEnded = true;
                    break;
                }
            }

            return resLogFileEnded;
        }

        /// <summary>
        /// Read and parse line by line CCDC Log file
        /// </summary>
        /// <returns>True - if there was new lines, false - in other case</returns>
        public bool ParseLogFile(out List<string> NewLines)
        {
            bool RetRes = true;
            List<string> RetLines = new List<string>();

            //count all log file lines
            int lineCount = contentsLogFile.Count();

            //Сколько строк придется обработать
            int numberOfLinesToWork = (lineCount - prevLinesCount);
            //Если больше лимита - то обрезать до лимита
            if (numberOfLinesToWork > _MAX_CCDC_LOGLINES_PARSE_ATATIME_LIMIT)
            {
                numberOfLinesToWork = _MAX_CCDC_LOGLINES_PARSE_ATATIME_LIMIT;
                ActionRunStartTime = new DateTime(2017, 01, 01, 0, 0, 1); //Лишь не 2015 год. Так как если пропустим строчку - он будет считаться, что начала и не было...
            }
            //Расчитать, сколько строк нужно пропустить
            int numberOfLinesToSkip = (lineCount - numberOfLinesToWork);

            //Count new (not earlier parsed) lines and reverse them
            IEnumerable <string> notReadLines = contentsLogFile.Skip(numberOfLinesToSkip);
            int newLinesCount = notReadLines.Count();


            //Parse new lines
            bool needConcat = false;
            string strDataConcat = "";
            DateTime curLineTime, prevLineTime;
            string curLineData__ ="" , prevLineData = "", curFullLineData = "";
            int cntReadLines = 0;
            string RetStr = "";

            //If there is lines to parse
            if (newLinesCount > 0)
            {
                //Loop throug new lines
                foreach (string curLine in notReadLines)
                {
                    cntReadLines++;
                    //////
                    //1. Check date part of line
                    if (curLine.Length >= 8 && DateTime.TryParse(curLine.Substring(0, 8), out curLineTime))
                    //A. Line starts with time
                    {
                        //Check - if previously was concatenate mode
                        if (needConcat)
                        {
                            //then mark string as combined
                            curFullLineData = prevLineData;

                            needConcat = false;
                            prevLineData = "";
                        }
                        else
                        {
                            curFullLineData = prevLineData;
                        }

                        // Пропустить, если это первая строка (она всегда будет пустой)
                        if (cntReadLines != 1)
                        {
                            ParseLogLine(curFullLineData, curLineTime);

                            RetStr = String.Format("{0}", curLineTime.ToString("HH:mm:ss"));
                            RetStr += String.Format(": {0}", curFullLineData) + Environment.NewLine;

                            RetLines.Add(RetStr);
                        }

                        //Save for next cycle
                        prevLineData = curLine.Substring(10);
                        prevLineTime = curLineTime;

                        // Если это последняя строка, то сделать с ней тоже, что и выше для предыдущей
                        if (cntReadLines == newLinesCount)
                        {
                            curFullLineData = prevLineData;
                            ParseLogLine(curFullLineData, curLineTime);

                            RetStr = String.Format("{0}", curLineTime.ToString("HH:mm:ss"));
                            RetStr += String.Format(": {0}", curFullLineData) + Environment.NewLine;

                            RetLines.Add(RetStr);
                        }

                        // Limit number of parsed lines
                        //if (cntReadLines > _MAX_CCDC_LOGLINES_PARSE_ATATIME_LIMIT)
                        //{
                        //    break;
                        //}
                    }
                    else
                    //B. no date field - concatenate lines!
                    {
                        // then prepare to concat next lines
                        needConcat = true;
                        prevLineData += curLine;
                    }
                }


                //set parsed lines pointer to new value
                prevLinesCount = lineCount;


                MessageText = RetStr; //for debugging. Acually it will return last line with date in it. But i want to return meaningfull events
            }
            else
            //If there is NO lines to parse
            {
                RetRes = false;
            }

            NewLines = RetLines;
            return RetRes;
        }

        /// <summary>
        /// Check line and make an action if detected smth.
        /// </summary>
        /// <param name="LineSt">Line text</param>
        /// <param name="LineTime">Line time</param>
        /// <returns></returns>
        private bool ParseLogLine(string LineSt, DateTime LineTime)
        {
            bool res = false;

            //2. parse lines
            //Pointing accuracy
            //      Pointing error vector = 38.1 arcsec, 315.9 degrees.
            if (LineSt.Contains("Pointing error vector"))
            {
                int beg1 = LineSt.LastIndexOf("=") + 1;
                int end1 = LineSt.LastIndexOf("arcsec");
                string stRes = LineSt.Substring(beg1, end1 - beg1).Trim();
                LastPointingError = Utils.ConvertToDouble(stRes);
                Logging.AddLog("CCDC Pointing error vector detected", LogLevel.Debug);
            }
            //Focusing accuracy
            //      Focus succeeded! HFD = 2.79
            else if (LineSt.Contains("Focus succeeded"))
            {
                int beg1 = LineSt.LastIndexOf("=") + 1;
                string stRes = LineSt.Substring(beg1).Trim();
                LastFocusHFD = Utils.ConvertToDouble(stRes);
                LastFocusTime = LineTime;
                Logging.AddLog("CCDC Focus succeeded detected", LogLevel.Debug);
            }

            //Start imaging
            //23:27:35  Setting image type to Light.
            //23:27:36  Setting imager bin mode to 1x1.
            //23:27:36  Setting filter to R.
            //23:27:36  Setting imager to full frame.
            //23:27:36  Setting imager exposure time to 600 seconds.
            //23:27:37  Starting imager delay...
            //23:27:42  Setting file name prefix to: NGC247_20171020_R_600s_1x1_ - 25degC_0.0degN
            //23:27:42  Starting imager exposure(1 of 1).

            //23:27:35  Setting image type to Light.
            else if (LineSt.Contains("Setting image type to"))
            {
                int beg1 = LineSt.LastIndexOf(" to ") + 4;
                LastExposure_ImageType = LineSt.Substring(beg1, LineSt.Length - beg1 - 1).Trim();
            }
            //      Setting imager bin mode to 1x1.
            else if (LineSt.Contains("Setting imager bin mode to"))
            {
                int beg1 = LineSt.LastIndexOf(" to ") + 4;
                LastExposure_bin = LineSt.Substring(beg1, LineSt.Length-beg1-1).Trim();
            }
            //      Setting filter to R.
            else if (LineSt.Contains("Setting filter to"))
            {
                int beg1 = LineSt.LastIndexOf(" to ") + 4;
                LastExposure_filter = LineSt.Substring(beg1, LineSt.Length - beg1 - 1).Trim();
            }
            //      Setting imager exposure time to 600 seconds.
            else if (LineSt.Contains("Setting imager exposure time to"))
            {
                int beg1 = LineSt.LastIndexOf(" to ") + 4;
                int end1 = LineSt.LastIndexOf("seconds");
                string LastExposure_ExposureLength_st= LineSt.Substring(beg1, end1 - beg1).Trim();
                if (!Int32.TryParse(LastExposure_ExposureLength_st,out LastExposure_ExposureLength))
                {
                    LastExposure_ExposureLength = 0;
                }

            }

            //      Setting file name prefix to: LeoTrio_L_600s_1x1_ - 20degC_0211_0.0degN
            else if (LineSt.Contains("Setting file name prefix to"))
            {
                int beg1 = LineSt.LastIndexOf(":") + 1;
                LastImageName = LineSt.Substring(beg1).Trim();
            }
            //      Starting imager exposure(1 of 10).
            else if (LineSt.Contains("Starting imager exposure"))
            {
                int beg1 = LineSt.LastIndexOf("(") + 1;
                int end1 = LineSt.LastIndexOf(")");
                LastSequenceInfo = LineSt.Substring(beg1, end1 - beg1).Trim();
                LastExposureStartTime = LineTime;
            }


            //21:16:31  Action starting.
            else if (LineSt.Contains("Action starting."))
            {
                ActionRunStartTime = LineTime;
                ActionRunEndTime = new DateTime(2015, 1, 1, 0, 0, 1); //Сбросить "конец"
            }

            //Imaging end
            //23:37:54  Saving image...
            //23:37:55  Imager exposure complete.
            //(if series ends:)
            //23:37:55  Take Images Action complete.

            //      Saving image...
            // Пока отключил, так как он реагирует и на PinPoint
            else if (LineSt.Contains("Saving image..."))
            {
                LastExposureEndTime = LineTime;
            }
            //      Imager exposure complete.
            else if (LineSt.Contains("Imager exposure complete."))
            {
                LastExposureEndTime = LineTime;
            }

            //Пауза через WeatherMonitor
            //Пометим, что экспозиция прервалась
            // 02:26:00  Unknown cloud sensor condition detected!  Pausing Action List.
            else if (LineSt.Contains("Unknown cloud sensor condition detected!  Pausing Action List."))
            {
                LastExposureEndTime = LineTime;
            }

            //The END
            //22:56:10  Action complete.
            //04:28:09  Stopping.
            //21:33:16  Action stopped.
            //НО! Бывает и "Take Images Action complete." в конце блока
            else if ((LineSt.Contains("Action complete.") && !LineSt.Contains("Take Images Action complete.")) || LineSt.Contains("Stopping.") || LineSt.Contains("Action stopped."))
            {
                ActionRunEndTime = LineTime;
            }


            // Type 1
            //19:17:47  Starting move to action.
            //19:17:47  Running script from C:\Users\Administrator\Documents\CCDWare\CCDAutoPilot5\Scripts\Broker_begin_slew.vbs
            //19:17:51  Script complete.
            //19:17:51  Precessing coordinates.
            //19:17:51  J2000 Coordinates: RA: 21h 43m 30.0s Dec: +58°46'49"
            //19:17:51  JNow Coordinates: RA: 21h 44m 02.7s Dec: +58°51'44"
            //19:17:51  Slewing to muCep...
            //19:18:40  Done slewing!

            // Type 2
            //19:15:08  Starting move to action.
            //19:15:08  Running script from D:\ASCOMscripts\phdbroker\auto_begin_slew.vbs
            //19:15:11  Script complete.
            //19:15:12  Slewing to NGC225...
            //19:15:16  Done slewing!

            // Want to parse:
            //      J2000 Coordinates: RA: 23h 36m 57,1s Dec: +05°43'24"
            // Do not forget: this string should be after "Precessing coordinates." or this can be Sync log output.
            // BUT. Sometimes (when Apparent Coordinates are set, there is now coord. output - only "Slewing to ...", 
            //      so we need to use last valud string
            else if (LineSt.Contains("Starting move to action."))
            {
                bMoveTo_beg = true;
                bMoveTo_end = false;
                bCoordWasntParsed = true;
            }
            else if (LineSt.Contains("Done slewing"))
            {
                bMoveTo_beg = false;
                bMoveTo_end = true;
            }
            else if (LineSt.Contains("Precessing coordinates."))
            {
            }
            else if (LineSt.Contains("J2000 Coordinates: RA: "))
            {
                int beg1 = LineSt.LastIndexOf("RA: ") + 4;
                int end1 = LineSt.LastIndexOf(" Dec: ");
                string ObjRA_st_temp = LineSt.Substring(beg1, end1 - beg1).Trim();
                string ObjDec_st_temp = LineSt.Substring(end1 + 6).Trim();

                //need to store? or skip?
                //if inside block - store!
                if (bMoveTo_beg && !bMoveTo_end)
                {
                    ObjRA_st = ObjRA_st_temp;
                    ObjDec_st = ObjDec_st_temp;
                    bCoordWasntParsed = false;
                }
                //if there was no coordinates in block - store also! every time you got it - store!
                else if (bCoordWasntParsed)
                {
                    ObjRA_st = ObjRA_st_temp;
                    ObjDec_st = ObjDec_st_temp;
                }
            }
            //      Slewing to M83...
            else if (LineSt.Contains("Slewing to "))
            {
                int beg1 = LineSt.LastIndexOf("Slewing to ") + 11;
                int end1 = LineSt.LastIndexOf("...");
                ObjName = LineSt.Substring(beg1, end1 - beg1).Trim();
            }


            // MAKE AN Action            

            //            23:13:20  Need to do a meridian flip!
            //            23:13:20  Flipping....
            //            23:14:41  Recentering....
            //            23:14:56  Flip complete.

            //23:55:01  Need to do a meridian flip!
            //23:55:01  Cannot flip the mount yet!
            //23:55:01  Waiting for mount to enter the meridian zone...
            else if (LineSt.Contains("Flipping...."))
            {
                LastFlipStartTime = LineTime;
            }
            else if (LineSt.Contains("Flip complete."))
            {
                //проверить, как давно это было
                if ((DateTime.Now - LineTime).TotalSeconds < 10 && (DateTime.Now - LastFlipInternalRoutineStartTime).TotalSeconds > 10)
                {
                    LastFlipInternalRoutineStartTime = DateTime.Now;
                    Logging.AddLog("CCDC Flip complete detected", LogLevel.Activity);
                    HandleFlip_async();
                }
            }


            //      Starting imager delay...
            //TEST PURPOSE ONLY!!!
            else if (LineSt.Contains("_________Starting imager delay"))
            {
                //проверить, как давно это было
                if ((DateTime.Now - LineTime).TotalSeconds < 10 && (DateTime.Now - LastFlipInternalRoutineStartTime).TotalSeconds > 10)
                {
                    LastFlipInternalRoutineStartTime = DateTime.Now;
                    Logging.AddLog("CCDC Flip complete detected", LogLevel.Activity);
                    HandleFlip_async();
                }
            }

            return res;
        }
        private bool ParseCommandLine(string LineSt)
        {
            return ParseLogLine(LineSt, DateTime.Now);
        }

        #endregion End of Working with log ****


        //******************************************************************************************************************************************************************
        /// <summary>
        /// Check if event flag is ON and run appropriate handler
        /// </summary>
        /// 
        private void CheckEvents()
        {
            //"STOP AFTER IMAGING"
            if (Request_StopAfterImage.RequestActive)
            {
                //Если еще ни разу не запускалась съемка
                //или ранее начатая съемка уже закончилась
                if (
                    (LastExposureEndTime.Year == 2015 && LastExposureStartTime.Year == 2015)
                    || (LastExposureStartTime < LastExposureEndTime)
                    )
                {
                    Automation_Stop();
                    Request_StopAfterImage.MarkFulfiled();
                }
                // не запущена программа, лог файл не обновляется, 
                // по текущему логу уже есть "конец"
                // ну или таймаут
                else if (
                    (DateTime.Now - Request_StopAfterImage.RequestedTime).TotalSeconds > Request_StopAfterImage.RequestTimeout
                    || !IsRunning() || !checkLogFileIsValid() || this.ActionRunEndTime.Year != 2015
                    )
                {
                    Request_StopAfterImage.MarkFulfiled(false);
                }
            }

            //"START IMAGING AFTER FINISH"
            if (Request_StartAfterStop.RequestActive)
            {
                //Если "Конец" уже настал по текущему логу (т.е. уже остановились)
                //и с момента "конца" прошло 6 сек (специальная задаржка)
                //то запускаем
                if (
                    (ActionRunEndTime.Year != 2015 || (DateTime.Now - ActionRunEndTime).TotalSeconds < 30)
                    && (DateTime.Now - ActionRunEndTime).TotalSeconds > 6)
                {
                    Automation_Start();
                    Thread.Sleep(5000); //ждем
                    Automation_Start(); //пробуем запустить опять

                    Request_StartAfterStop.MarkFulfiled();
                }

                // не запущена программа, лог файл не обновляется, 
                // --------------по текущему логу еще нет "конца", но уже есть "начало" --- сначала сделал, а потом подумал, что глупая логика (он отключает "требование", например, когда все ждут окончания экспозиции
                // ну или таймаут
                else if (
                    (DateTime.Now - Request_StartAfterStop.RequestedTime).TotalSeconds > Request_StartAfterStop.RequestTimeout
                    || !IsRunning() || !checkLogFileIsValid() || (false && ActionRunStartTime.Year != 2015 && ActionRunEndTime.Year == 2015)
                    )
                {
                    Request_StartAfterStop.MarkFulfiled(false);
                }
            }

            //"ABORT IMAGING AFTER FINISH"
            if (Request_AbortAfterStop.RequestActive)
            {
                //Если "Конец" уже настал по текущему логу (т.е. уже остановились)
                //и с момента "конца" прошло 5 сек (специальная задаржка)
                //то запускаем
                if (
                    (ActionRunEndTime.Year != 2015 || (DateTime.Now - ActionRunEndTime).TotalSeconds < 30)
                    && (DateTime.Now - ActionRunEndTime).TotalSeconds > 5)
                {
                    ParentObsControl.CommandParser.ParseSingleCommand2("IMAGING_RUN_ABORT", true);

                    Request_AbortAfterStop.MarkFulfiled();
                }

                // не запущена программа, лог файл не обновляется, 
                // --------------по текущему логу еще нет "конца", но уже есть "начало" --- сначала сделал, а потом подумал, что глупая логика (он отключает "требование", например, когда все ждут окончания экспозиции
                // ну или таймаут
                else if (
                    (DateTime.Now - Request_AbortAfterStop.RequestedTime).TotalSeconds > Request_AbortAfterStop.RequestTimeout
                    || !IsRunning() || !checkLogFileIsValid() || (false && ActionRunStartTime.Year != 2015 && ActionRunEndTime.Year == 2015)
                    )
                {
                    Request_AbortAfterStop.MarkFulfiled(false);
                }

            }


        }

        public void CheckEvents_async()
        {
            Thread childThread = new Thread(delegate () {
                CheckEvents();
            });
            childThread.Start();
        }
        //******************************************************************************************************************************************************************




    }
}
