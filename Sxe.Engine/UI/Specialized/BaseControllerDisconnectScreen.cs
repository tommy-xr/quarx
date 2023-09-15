using System;
using System.Collections.Generic;
using System.Text;

using Sxe.Engine.Gamers;



namespace Sxe.Engine.UI
{

    /// <summary>
    /// Base class for any screen that shows what controllers are disconnected
    /// </summary>
    public class BaseControllerDisconnectScreen : MessageScreen
    {
        List<IAnarchyGamer> disconnectedList = new List<IAnarchyGamer>();

        protected List<IAnarchyGamer> DisconnectedList
        {
            get { return disconnectedList; }
        }

        public BaseControllerDisconnectScreen()
        {
        }

        void InitializeComponent()
        {
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            //Loop through anarchy gamer collection and add gamers with disconnected controllers
            disconnectedList.Clear();

            for (int i = 0; i < AnarchyGamer.Gamers.Count; i++)
            {
                IAnarchyGamer gamer = AnarchyGamer.Gamers[i];
                if (gamer.IsPlayer)
                {
                    if (!gamer.Controller.Connected)
                    {
                        disconnectedList.Add(gamer);
                    }
                }
            }

            if (disconnectedList.Count <= 0)
                this.ExitScreen();

            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }


    }
}
