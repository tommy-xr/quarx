using System;
using System.Collections.Generic;
using System.Text;

using Sxe.Engine.UI;
using Sxe.Engine.Input;

namespace Quarx
{
    public class BaseWaitScreen : MessageScreen
    {
        int numPlayers = 1;
        bool[] ready;

        UIImage readyDim;
        UIImage readyBright;

        bool firstFrame = true;

        public UIImage ReadyDim
        {
            get { return readyDim; }
        }

        public UIImage ReadyBright
        {
            get { return readyBright; }
        }


        public override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            base.LoadContent(content);

            readyDim = LoadImage(content, "readydim");
            readyBright = LoadImage(content, "readybright");
        }

        public virtual void InitializeWaitScreen(int players)
        {
            numPlayers = players;
            ready = new bool[players];
            for (int i = 0; i < players; i++)
            {
                ready[i] = false;
            }
        }


        public override void Update(Microsoft.Xna.Framework.GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
            bool allReady = true;
            if (ready != null && !firstFrame)
            {
                for (int i = 0; i < numPlayers; i++)
                {
                    int controllerIndex = Math.Min(i, Input.Controllers.Count - 1);

                    if (!ready[i] && GameScreenService.Input.Controllers[controllerIndex].IsKeyJustPressed("menu_select"))
                        ready[i] = true;
                    else if (ready[i] && GameScreenService.Input.Controllers[controllerIndex].IsKeyJustPressed("menu_back"))
                        ready[i] = false;

                    allReady = allReady && ready[i];
                }
            }

            if (firstFrame)
                firstFrame = false;
            else if (allReady)
                this.ExitScreen();

            //Check and see if everyone is ready
            //If so, hide this shizzz
        }

        public bool GetPlayerStatus(int player)
        {
            return ready[player];
        }

    }
}
