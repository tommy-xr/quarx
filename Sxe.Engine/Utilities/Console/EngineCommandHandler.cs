using System;
using System.Collections.Generic;
using System.Text;

using Sxe.Engine.UI;

using Microsoft.Xna.Framework;

namespace Sxe.Engine.Utilities.Console
{
    public class EngineCommandHandler : BaseCommandHandler
    {
        const string showFormCommand = "show_form";
        const string hideFormCommand = "hide_form";

        IGameScreenService gameScreenService;


        public EngineCommandHandler(Game game)
            : base(game)
        {
            gameScreenService = (IGameScreenService)game.Services.GetService(typeof(IGameScreenService));
        }

        public override void Initialize()
        {
            Console.HookCommand(showFormCommand, this);
            Console.HookCommand(hideFormCommand, this);

            base.Initialize();
        }

        public override bool HandleConsoleCommand(string[] command)
        {
            FormScreen fs;

            switch (command[0])
            {
                //TODO: 
                    //Fix this so that forms work.
                //case showFormCommand:
                //    fs = gameScreenService.CurrentScreen as FormScreen;
                //    if(fs != null)
                //    fs.Forms.SetFormVisible(command[1], true);
                //    return true;
                //case hideFormCommand:
                //    fs = gameScreenService.CurrentScreen as FormScreen;
                //    if(fs != null)
                //    fs.Forms.SetFormVisible(command[1], false);
                //    return true;
                default:
                    return false;
            }

            return base.HandleConsoleCommand(command);
        }


    }
}
