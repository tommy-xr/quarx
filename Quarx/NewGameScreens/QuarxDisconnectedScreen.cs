using System;
using System.Collections.Generic;
using System.Text;

using Sxe.Engine.UI;

namespace Quarx.NewGameScreens
{
    public class QuarxDisconnectedScreen : BaseControllerDisconnectScreen
    {
        private Label label1;
        private Label label2;
        private Label label3;
        private Panel panel2;
        private Panel panel1;
    
        public QuarxDisconnectedScreen()
        {
            InitializeComponent();
        }

        public override void Activate()
        {
            this.GameScreenService.Audio.PlayCue("disconnect_show");
            base.Activate();
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            if (this.DisconnectedList.Count > 0)
            {
                this.label3.Caption = this.DisconnectedList[0].GamerTag;
                this.panel2.Image = this.DisconnectedList[0].GamerIcon;

                this.label3.Visible = true;
                this.panel2.Visible = true;
            }
            else
            {
                this.label3.Visible = false;
                this.panel2.Visible = false;
            }

            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }

        void InitializeComponent()
        {
            this.panel1 = new Sxe.Engine.UI.Panel();
            this.label1 = new Sxe.Engine.UI.Label();
            this.label2 = new Sxe.Engine.UI.Label();
            this.label3 = new Sxe.Engine.UI.Label();
            this.panel2 = new Sxe.Engine.UI.Panel();
            // 
            // panel1
            // 
            this.panel1.BackgroundPath = "setupbutton";
            this.panel1.Location = new Microsoft.Xna.Framework.Point(176, 210);
            this.panel1.Parent = this;
            this.panel1.Size = new Microsoft.Xna.Framework.Point(445, 198);
            this.panel1.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.panel1.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            // 
            // label1
            // 
            this.label1.BackgroundPath = null;
            this.label1.Caption = "Disconnected!";
            this.label1.FontColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.label1.FontPath = "Fonts\\CreditHeading";
            this.label1.HorizontalAlignment = Sxe.Engine.UI.HorizontalAlignment.Center;
            this.label1.Location = new Microsoft.Xna.Framework.Point(271, 261);
            this.label1.Parent = this;
            this.label1.Selection = new Microsoft.Xna.Framework.Point(-1, -1);
            this.label1.Size = new Microsoft.Xna.Framework.Point(294, 31);
            this.label1.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.label1.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.label1.VerticalAlignment = Sxe.Engine.UI.VerticalAlignment.Top;
            // 
            // label2
            // 
            this.label2.BackgroundPath = null;
            this.label2.Caption = "Please re-connect your controller!";
            this.label2.FontColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.label2.FontPath = "Neuropol";
            this.label2.HorizontalAlignment = Sxe.Engine.UI.HorizontalAlignment.Center;
            this.label2.Location = new Microsoft.Xna.Framework.Point(269, 347);
            this.label2.Parent = this;
            this.label2.Selection = new Microsoft.Xna.Framework.Point(-1, -1);
            this.label2.Size = new Microsoft.Xna.Framework.Point(294, 31);
            this.label2.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.label2.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.label2.VerticalAlignment = Sxe.Engine.UI.VerticalAlignment.Top;
            // 
            // label3
            // 
            this.label3.BackgroundPath = null;
            this.label3.Caption = "LongGamerName";
            this.label3.FontColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.label3.FontPath = "Calibri11";
            this.label3.HorizontalAlignment = Sxe.Engine.UI.HorizontalAlignment.Center;
            this.label3.Location = new Microsoft.Xna.Framework.Point(377, 298);
            this.label3.Parent = this;
            this.label3.Selection = new Microsoft.Xna.Framework.Point(-1, -1);
            this.label3.Size = new Microsoft.Xna.Framework.Point(160, 29);
            this.label3.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.label3.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.label3.VerticalAlignment = Sxe.Engine.UI.VerticalAlignment.Middle;
            // 
            // panel2
            // 
            this.panel2.BackgroundPath = null;
            this.panel2.Location = new Microsoft.Xna.Framework.Point(333, 286);
            this.panel2.Parent = this;
            this.panel2.Size = new Microsoft.Xna.Framework.Point(50, 50);
            this.panel2.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.panel2.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            // 
            // QuarxDisconnectedScreen
            // 
            this.Panels.Add(this.panel1);
            this.Panels.Add(this.label1);
            this.Panels.Add(this.label2);
            this.Panels.Add(this.label3);
            this.Panels.Add(this.panel2);

        }

    }
}
