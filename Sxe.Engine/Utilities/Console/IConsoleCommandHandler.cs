using System;
using System.Collections.Generic;
using System.Text;

namespace Sxe.Engine.Utilities.Console
{
    public interface IConsoleCommandHandler
    {
        bool HandleConsoleCommand(string[] command);
    }
}
