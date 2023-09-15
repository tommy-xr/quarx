using System;
using System.Collections.Generic;
using System.Text;

using Sxe.Engine.UI;
using Sxe.Engine;
namespace Quarx
{
    public class HighScoreCongrats : MessageScreen
    {
        private Panel panel1;

        IAnarchyGamer gamer;
        public IAnarchyGamer Gamer
        {
            get { return gamer; }
            set { gamer = value; }
        }


        public HighScoreCongrats()
        {
            InitializeComponent();
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            if (gamer != null)
            {
                if (gamer.Controller.IsKeyJustPressed("menu_select"))
                    this.ExitScreen();
            }
            else
                this.ExitScreen();
            

            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }

        void InitializeComponent()
        {
            this.panel1 = new Sxe.Engine.UI.Panel();
            // 
            // panel1
            // 
            this.panel1.BackgroundPath = "congratulations";
            this.panel1.Location = new Microsoft.Xna.Framework.Point(165, 214);
            this.panel1.Parent = this;
            this.panel1.Size = new Microsoft.Xna.Framework.Point(479, 137);
            this.panel1.TransitionOffOffset = new Microsoft.Xna.Framework.Point(500, 0);
            this.panel1.TransitionOnOffset = new Microsoft.Xna.Framework.Point(-500, 0);
            // 
            // HighScoreCongrats
            // 
            this.Panels.Add(this.panel1);
            this.PresenceMode = Microsoft.Xna.Framework.GamerServices.GamerPresenceMode.TryingForRecord;

        }
    }
}
