using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ObservatoryCenter
{

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
        //Path lo log dir
        public string LogPath = @"c:\CCD Commander\Logs";

        //Current log file link
        public FileInfo currentLogFile;
        private IEnumerable<string> contentsLogFile;
        //private IEnumerable<string> contentsLogFile_Reversed;

        public int _MAX_VALID_CCDC_LOGFILE_NAME_AGE = 3600 * 12; //how old log file can be (in its name) in seconds
        public int _MAX_VALID_CCDC_LOGFILE_MOD_AGE = 3000;       //how old log file can be (from last change) in seconds

        private Int32 prevLinesCount = 0;
        public int _MAX_CCDC_LOGLINES_PARSE_ATATIME_LIMIT = 5; //parse only 5 last lines (need to be limited if started during session)

        //Path to actions
        public string ActionsPath = @"c:\CCD Commander\Actions";
        //Last action file link
        public FileInfo lastActionFile;


        public string MessageText;

        public Dictionary<string, CCDAP_Command_class> CCDAPKeywordsList = new Dictionary<string, CCDAP_Command_class>();

        /// <summary>
        /// Init CCDC activity
        /// </summary>
        public void Init()
        {

        }


        public void CheckEmergengyRunAbortFlag()
        // set = 1 abort
        // set = 0 clear
        {
            //HKEY_CURRENT_USER\Software\VB and VBA Program Settings\CCDCommander\Test\Aborted

            RegistryKey abortKey = Registry.CurrentUser.OpenSubKey("Software\\VB and VBA Program Settings\\CCDCommander\\Test", true);
            if (abortKey != null)
            {
                abortKey.SetValue("Aborted", "1", RegistryValueKind.String);
                abortKey.Close();
            }
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
                lastActionFile = new FileInfo(ActionsPath+"");
            }

            return lastActionFile;
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
                int sinceMod = GetTimeSinceLogFileModified();
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
        internal bool checkLogFileEnded()
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
        /// Seconds past last file modification
        /// </summary>
        /// <returns></returns>
        internal Int32 GetTimeSinceLogFileModified()
        {
            TimeSpan SinceMod = DateTime.Now - currentLogFile.LastWriteTime;
            return (int)SinceMod.TotalSeconds;
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
            }
            //Расчитать, сколько строк нужно пропустить
            int numberOfLinesToSkip = (lineCount - numberOfLinesToWork);

            //Count new (not earlier parsed) lines and reverse them
            IEnumerable <string> notReadLines = contentsLogFile.Skip(numberOfLinesToSkip);
            int newLinesCount = notReadLines.Count();

            bool needConcat = false;
            string strDataConcat = "";
            DateTime curLineTime, prevLineTime;
            string curLineData__ ="" , prevLineData = "", curFullLineData = "";
            int cntReadLines = 0;
            string RetStr = "";

            if (newLinesCount > 0)
            //If there is lines to parse
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
                            ParseCommandLine(curFullLineData);

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
                            ParseCommandLine(curFullLineData);

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


        private bool ParseCommandLine(string LineSt)
        {
            bool res = false;

            //2. parse lines
            if (LineSt.Contains(""))
            {

            }
            else if (LineSt.Contains(""))
            {

            }

            return res;
        }

#endregion End of Working with log ****
    }
}
