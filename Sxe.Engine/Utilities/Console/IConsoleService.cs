using System;
using System.Collections.Generic;
using System.Text;

namespace Sxe.Engine.Utilities.Console
{
    public interface IConsoleService
    {
        void HookCommand(string command, IConsoleCommandHandler handler);
        void HookCommand(string command, int numberOfArgs, IConsoleCommandHandler handler);

        /// <summary>
        /// Print to console
        /// </summary>
        /// <param name="sz"></param>
        void Print(string sz);

        /// <summary>
        /// Execute a console command from code
        /// </summary>
        void ClientCommand(string sz);

    }
}
