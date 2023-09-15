using System;
using System.Collections.Generic;
using System.Text;

namespace Sxe.Engine.Utilities.Console
{
    public class ConsoleEventArgs : EventArgs
    {
        string consoleCommand;

        public string Command
        {
            get { return consoleCommand; }
            set { consoleCommand = value; }
        }
    }
}
