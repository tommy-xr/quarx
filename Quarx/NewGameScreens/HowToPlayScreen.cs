using System;
using System.Collections.Generic;
using System.Text;

using Sxe.Engine.UI;

using Microsoft.Xna.Framework.Graphics;

namespace Quarx 
{
    public class HowToPlayScreen : MessageScreen
    {
        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
        bool firstFrame = true;
    
        public HowToPlayScreen()
        {
            InitializeComponent();

            panel2.Visible = true;
            panel3.Visible = false;
            
        }

        private void Toggle()
        {
            if (panel2.Visible)
            {
                panel3.Visible = true;
                panel2.Visible = false;
            }
            else
            {
                panel3.Visible = false;
                panel2.Visible = true;
            }
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            if (!firstFrame)
            {
                //Check if we've received any input
                if (!otherScreenHasFocus)
                {
                    if (Input.Controllers.IsKeyJustPressed("menu_back"))
                        this.ExitScreen();

                    if (Input.Controllers.IsKeyJustPressed("a_button"))
                        this.Toggle();

                    if (Input.Controllers.IsKeyJustPressed("x_button"))
                    {
                        this.ExitScreen();
                        this.GameScreenService.AddScreen(new Quarx.NewCreditScreen());
                    }
                }
            }
            else
                firstFrame = false;

            this.BackColor = new Color(this.BackColor.R, this.BackColor.G, this.BackColor.B, TransitionAlpha);

            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }


        void InitializeComponent()
        {
            this.panel1 = new Sxe.Engine.UI.Panel();
            this.panel2 = new Sxe.Engine.UI.Panel();
            this.panel3 = new Sxe.Engine.UI.Panel();
            // 
            // panel1
            // 
            this.panel1.BackgroundPath = "Howtoplay\\howtoplayback";
            this.panel1.Location = new Microsoft.Xna.Framework.Point(100, 100);
            this.panel1.Parent = this;
            this.panel1.Size = new Microsoft.Xna.Framework.Point(600, 400);
            this.panel1.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.panel1.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            // 
            // panel2
            // 
            this.panel2.BackgroundPath = "Howtoplay\\howtoplay1";
            this.panel2.Location = new Microsoft.Xna.Framework.Point(100, 100);
            this.panel2.Parent = this;
            this.panel2.Size = new Microsoft.Xna.Framework.Point(600, 400);
            this.panel2.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.panel2.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            // 
            // panel3
            // 
            this.panel3.BackgroundPath = "Howtoplay\\howtoplay2";
            this.panel3.Location = new Microsoft.Xna.Framework.Point(100, 100);
            this.panel3.Parent = this;
            this.panel3.Size = new Microsoft.Xna.Framework.Point(600, 400);
            this.panel3.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.panel3.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            // 
            // HowToPlayScreen
            // 
            this.Panels.Add(this.panel1);
            this.Panels.Add(this.panel2);
            this.Panels.Add(this.panel3);
            this.PresenceMode = Microsoft.Xna.Framework.GamerServices.GamerPresenceMode.WastingTime;
            this.TransitionOffOffset = new Microsoft.Xna.Framework.Point(-500, 0);
            this.TransitionOnOffset = new Microsoft.Xna.Framework.Point(500, 0);

        }
    }
}
