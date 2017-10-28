using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ObservatoryCenter
{
    public class Command
    {
        public Func<string[], string> CommandDelegate;
        public string Description;
        public string Syntax;

        public Command(Func<string[], string> CommandDelegateEx, string DescriptionEx, string SyntaxEx)
        {
            CommandDelegate = CommandDelegateEx;    // link to function run
            Description = DescriptionEx;            // Decription
            Syntax = SyntaxEx;                      // Parameters syntax
        }

        public Command(Func<string[], string> CommandDelegateEx, string DescriptionEx) 
            : this(CommandDelegateEx, DescriptionEx, "")
        {
        }

        public Command(Func<string[], string> CommandDelegateEx)
            : this(CommandDelegateEx, "", "")
        {
        }
    }


    /// <summary>
    /// How to add new command:
    /// 1. Create method which would react on call
    /// 2. Add to ObservatoryContols.InitComandInterpretator() link from SYMBOL NAME to the method
    /// 3. Call it as CommandParser.ParseSingleCommand("SYMBOL_NAME");
    /// 4. if you want a parameter to be passed, call it as CommandParser.ParseSingleCommand("SYMBOL_NAME PARAMETER1 PARAMETER2");
    /// </summary>
    public class CommandInterpretator
    {
        //public Dictionary<string, Func<string>> Commands = new Dictionary<string, Func<string>>();
        public Dictionary<string, Func<string[], string>> Commands__ = new Dictionary<string, Func<string[], string>>(); //with parameters


        public Dictionary<string, Command> Commands = new Dictionary<string, Command>(); //with parameters


        public CommandInterpretator()
        {
            //Commands = PassedCommands;
        }


        #region Legacy part
        /// <summary>
        /// Method which doesn't return command output 
        /// </summary>
        /// <param name="CommandString">command</param>
        /// <returns>false if command doesn't exist</returns>
        public bool ParseSingleCommand__(string CommandString)
        {
            string dummy_cmd_output = "";
            bool ret = ParseSingleCommand__(CommandString, out dummy_cmd_output);
            return ret;
        }

        /// <summary>
        /// Base method which returns command output 
        /// 1. Split command for COMMAND and its PARAMETERS (e.g."SET_FAN 10")
        /// 2. Checks if COMMAND exists
        /// 3. Run command (based on dictionary list)
        /// </summary>
        /// <param name="CommandString">Command string</param>
        /// <param name="cmd_output">OUT - command output</param>
        /// <returns>false if command doesn't exist</returns>
        public bool ParseSingleCommand__(string CommandString, out string cmd_output)
        {
            Logging.AddLog("Parse command [" + CommandString + "] enter", LogLevel.Trace);
            bool ret = true;
            string CommandString_pure = "";
            string[] CommandString_param_arr = new string[0];

            //1. Split command string into COMMAND and PARAMETERS
            if (CommandString.Contains(" "))
            {
                //Split
                string[] CommandString_arr = CommandString.Split(' ');
                CommandString_pure = CommandString_arr[0];
                CommandString_param_arr = CommandString_arr.Skip(1).ToArray();
            }
            else
            {
                CommandString_pure = CommandString;
            }

            //2. Check if COMMAND exists
            if (!Commands__.ContainsKey(CommandString_pure))
            {
                Logging.AddLog("Команды [" + CommandString_pure + "] не существует", 0, Highlight.Error);
                cmd_output = "Команды [" + CommandString + "] не существует";
                ret = false;
            }
            else
            {
                cmd_output = Commands__[CommandString_pure](CommandString_param_arr);
                Logging.AddLog("Parse command [" + CommandString + "]("+ CommandString_param_arr.ToString() + ") output: " + cmd_output, LogLevel.Trace);
            }
            Logging.AddLog("Parse command [" + CommandString + "](" + CommandString_param_arr.ToString() + ") exit, res=" + ret, LogLevel.Trace);
            return ret;
        }

        /// <summary>
        /// List all commands
        /// </summary>
        /// <returns>string list of commands</returns>
        public string ListCommands__()
        {
            string st = "";

            foreach (KeyValuePair<string, Func<string[], string>> entry in Commands__)
            {
                st += entry.Key + ";";
            }

            return st;
        }

        #endregion Legacy part


        public bool ParseSingleCommand2(string CommandString)
        {
            string dummy_cmd_output = "";
            bool ret = ParseSingleCommand2(CommandString, out dummy_cmd_output, false);
            return ret;
        }

        public bool ParseSingleCommand2(string CommandString, out string cmd_output)
        { 
            bool ret = ParseSingleCommand2(CommandString, out cmd_output, false);
            return ret;
        }
        public bool ParseSingleCommand2(string CommandString, bool RunAsyncFlag)
        {
            string dummy_cmd_output = "";
            bool ret = ParseSingleCommand2(CommandString, out dummy_cmd_output, RunAsyncFlag);
            return ret;
        }
        /// <summary>
        /// Base method which returns command output 
        /// 1. Split command for COMMAND and its PARAMETERS (e.g."SET_FAN 10")
        /// 2. Checks if COMMAND exists
        /// 3. Run command (based on dictionary list)
        /// </summary>
        /// <param name="CommandString">Command string</param>
        /// <param name="cmd_output">OUT - command output</param>
        /// <param name="RunAsyncFlag">true - run command in separate thread</param>
        /// <returns>false if command doesn't exist</returns>
        public bool ParseSingleCommand2(string CommandString, out string cmd_output, bool RunAsyncFlag)
        {
            Logging.AddLog("Parse command [" + CommandString + "] enter", LogLevel.Trace);
            bool ret = true;
            string CommandString_pure = "";
            string[] CommandString_param_arr = new string[0];

            //1. Split command string into COMMAND and PARAMETERS
            if (CommandString.Contains(" "))
            {
                //Split
                string[] CommandString_arr = CommandString.Split(' ');
                CommandString_pure = CommandString_arr[0];
                CommandString_param_arr = CommandString_arr.Skip(1).ToArray();
            }
            else
            {
                CommandString_pure = CommandString;
            }

            //2. Check if COMMAND exists
            if (!Commands.ContainsKey(CommandString_pure))
            {
                Logging.AddLog("Команды [" + CommandString_pure + "] не существует", LogLevel.Important, Highlight.Error);
                cmd_output = "Команды [" + CommandString + "] не существует";
                ret = false;
            }
            else
            {
                //3. Run COMMAND
                if (RunAsyncFlag)
                {
                    //3.1. ASYNC
                    Thread childThread = new Thread(delegate () {
                        Commands[CommandString_pure].CommandDelegate(CommandString_param_arr); ;
                    });
                    childThread.Start();
                    cmd_output = "";
                    Logging.AddLog("Command [" + CommandString + "](" + CommandString_param_arr.ToString() + ") was run async", LogLevel.Activity);
                    Logging.AddLog("Parse command [" + CommandString + "](" + CommandString_param_arr.ToString() + ") was run async", LogLevel.Trace);
                }
                else
                {
                    //3.2. OLD SYNC MODE
                    cmd_output = Commands[CommandString_pure].CommandDelegate(CommandString_param_arr);
                    Logging.AddLog("Parse command [" + CommandString + "](" + CommandString_param_arr.ToString() + ") output: " + cmd_output, LogLevel.Trace);
                }
            }

            Logging.AddLog("Parse command [" + CommandString + "](" + CommandString_param_arr.ToString() + ") exit, res=" + ret, LogLevel.Trace);
            return ret;
        }


        public string ListCommands2()
        {
            string st = "";

            foreach (KeyValuePair<string, Command> entry in Commands)
            {
                st += entry.Key + "; ";
            }

            return st;
        }
        public string ListCommandsFormated2()
        {
            string st = "";

            foreach (KeyValuePair<string, Command> entry in Commands)
            {
                st += String.Format("{0}\t{1}\n\r\t{2}", entry.Key, entry.Value.Syntax, entry.Value.Description) + Environment.NewLine;
            }

            return st;
        }
    }

}
