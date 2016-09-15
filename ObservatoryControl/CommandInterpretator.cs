using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ObservatoryCenter
{
/// <summary>
/// How to add new command:
/// 1. Create method which would react on call
/// 2. Add to ObservatoryContols.InitComandInterpretator() link from SYMBOL NAME to the method
/// 3. Call it as CommandParser.ParseSingleCommand("SYMBOL NAME");
/// </summary>
    public class CommandInterpretator
    {
        public Dictionary<string, Func<string>> Commands = new Dictionary<string, Func<string>>();

        public CommandInterpretator()
        {
            //Commands = PassedCommands;
        }

        /// <summary>
        /// Method which doesn't return command output 
        /// </summary>
        /// <param name="CommandString">command</param>
        /// <returns>false if command doesn't exist</returns>
        public bool ParseSingleCommand(string CommandString)
        {
            string cmd_output = "";
            bool ret = ParseSingleCommand(CommandString, out cmd_output);
            return ret;
        }

        /// <summary>
        /// Method override which returns command output 
        /// </summary>
        /// <param name="CommandString">command</param>
        /// <param name="cmd_output">command output</param>
        /// <returns>false if command doesn't exist</returns>
        public bool ParseSingleCommand(string CommandString, out string cmd_output)
        {
            Logging.AddLog("Parse command [" + CommandString + "] enter", LogLevel.Trace);
            bool ret = true;
            if (!Commands.ContainsKey(CommandString))
            {
                Logging.AddLog("Команды [" + CommandString + "] не существует", 0, Highlight.Error);
                ret = false;
            }
            cmd_output = Commands[CommandString]();
            Logging.AddLog("Parse command [" + CommandString + "] output: " + cmd_output, LogLevel.Trace);
            Logging.AddLog("Parse command [" + CommandString + "] exit, res=" + ret, LogLevel.Trace);
            return ret;
        }

    }
}
