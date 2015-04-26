using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ObservatoryCenter
{
    public class CommandInterpretator
    {
        Dictionary<string, Action> Commands;

        public CommandInterpretator(Dictionary<string, Action> PassedCommands)
        {
            Commands = PassedCommands;
        }

        public bool ParseSingleCommand(string CommandString)
        {
            bool ret = true;
            if (!Commands.ContainsKey(CommandString))
            {
                Logging.AddLog("Команды [" + CommandString + "] не существует", 0, Highlight.Error);
                ret = false;
            }
            Commands[CommandString]();
            return ret;
        }

    }
}
