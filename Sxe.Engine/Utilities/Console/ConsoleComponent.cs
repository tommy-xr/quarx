using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;

using Sxe.Engine.UI;

namespace Sxe.Engine.Utilities.Console
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class ConsoleComponent : Microsoft.Xna.Framework.GameComponent, IConsoleService
    {
        /// <summary>
        /// Stores info needed for registering a console command
        /// </summary>
        class ConsoleCommandInfo
        {
            public IConsoleCommandHandler Handler;
            public int NumberOfArguments = -1;

            public ConsoleCommandInfo(IConsoleCommandHandler handler,int numberArguments)
            {
                NumberOfArguments = numberArguments;
                Handler = handler;
            }
        }


        ConsoleCommandParser parser = new ConsoleCommandParser();
        ///ConsoleForm console;

        TextWriter writer;
        //TODO: When necessary, make the dictionary store a list of ConsoleCommandInfos,
        //so a command can have multiple handlers
        IDictionary<string, ConsoleCommandInfo> commandDictionary;

        public ConsoleComponent(Game game)
            : base(game)
        {
            game.Services.AddService(typeof(IConsoleService), this);


#if !XBOX
            commandDictionary = new SortedDictionary<string, ConsoleCommandInfo>();
#else
            commandDictionary = new Dictionary<string, ConsoleCommandInfo>();
#endif
            //console = new ConsoleForm(new DefaultScheme(game.Content), 
            //    (IGameScreenService)game.Services.GetService(typeof(IGameScreenService)));
            //console = new ConsoleForm((IGameScreenService)game.Services.GetService(typeof(IGameScreenService)));

            //console.Visible = false;

            //if (console != null)
            //{
            //    console.ExecuteConsoleCommand += ClientCommand;
            //    GlobalForms.FormCollection.AddForm(console);
            //}

            writer = new StringWriter();
            System.Console.SetOut(writer);

            EngineCommandHandler newHandler = new EngineCommandHandler(game);
        }

        public void Print(string sz)
        {
            //if(console != null)
            //console.ConsoleTextBox.Text += TextBox.NewLine + sz;
        }

        public void HookCommand(string sz, IConsoleCommandHandler handler)
        {
            HookCommand(sz, -1, handler);
        }

        public void HookCommand(string sz,int numberOfArgs, IConsoleCommandHandler handler)
        {
            commandDictionary.Add(sz, new ConsoleCommandInfo(handler, numberOfArgs));
        }

        /// <summary>
        /// Wrapper to catch ClientCommand events from console form
        /// </summary>
        void ClientCommand(object sender, ConsoleEventArgs args)
        {
            ClientCommand(args.Command);
        }

        /// <summary>
        /// Parse the command, and find a command handler, and then run the command 
        /// on the command handler
        /// </summary>
        /// <param name="?"></param>
        public void ClientCommand(string command)
        {
            //Tokenize the command
            string[] tokens = parser.ConsoleTokenize(command);

            //Look up the first command - see if we have a handler
            if (commandDictionary.ContainsKey(tokens[0]))
            {
                ConsoleCommandInfo info = commandDictionary[tokens[0]];
                if(info.NumberOfArguments == -1 || info.NumberOfArguments <= tokens.Length)
                info.Handler.HandleConsoleCommand(tokens);
            }
            else
            {
                Print("Invalid command.");
            }

        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            //TODO:
            //Make this more efficient by creating a custom console writer 
            //that only adds text when necessary
            string addText = writer.ToString();
            if (addText != null)
            {
                if (addText.Length > 0)
                    Print(addText);
            }

            base.Update(gameTime);
        }
    }
}