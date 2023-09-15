using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;

namespace Sxe.Engine.Utilities.Console
{
    /// <summary>
    /// This component is responsible for doing the actual Console commands
    /// </summary>
    public class BaseCommandHandler : IConsoleCommandHandler
    {
        IConsoleService console;
        Game game;

        protected IConsoleService Console
        {
            get { return console; }
        }
        protected Game Game
        {
            get { return game; }
        }

        public BaseCommandHandler(Game inGame)
        {
            game = inGame;

            //Get Console service
            console = (IConsoleService)game.Services.GetService(typeof(IConsoleService));

            Initialize();

        }


        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public virtual void Initialize()
        {

        }

        public virtual bool HandleConsoleCommand(string[] command)
        {
            return false;
        }
    }
}
