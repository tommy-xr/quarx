using System;
using System.Collections.Generic;
using System.Text;

using Sxe.Engine.UI;

namespace Quarx
{
    public class TwoPlayerWaitScreen : BaseWaitScreen
    {
        private Panel panel2;
        private Panel panel3;
        private Panel panel4;
        private Panel panel1;

        public TwoPlayerWaitScreen()
        {
            InitializeWaitScreen(2);
            InitializeComponent();
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            if (GetPlayerStatus(0))
                panel2.Image = ReadyBright;
            else
                panel2.Image = ReadyDim;

            if (GetPlayerStatus(1))
                panel4.Image = ReadyBright;
            else
                panel4.Image = ReadyDim;

            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }

        void InitializeComponent()
        {
            this.panel1 = new Sxe.Engine.UI.Panel();
            this.panel2 = new Sxe.Engine.UI.Panel();
            this.panel3 = new Sxe.Engine.UI.Panel();
            this.panel4 = new Sxe.Engine.UI.Panel();
            // 
            // panel1
            // 
            this.panel1.BackColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.panel1.BackgroundPath = "player1";
            this.panel1.CanDrag = false;
            this.panel1.Enabled = true;
            this.panel1.Location = new Microsoft.Xna.Framework.Point(4, 171);
            this.panel1.Name = "";
            this.panel1.Parent = this;
            this.panel1.Size = new Microsoft.Xna.Framework.Point(379, 216);
            this.panel1.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.panel1.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.panel1.Visible = true;
            // 
            // panel2
            // 
            this.panel2.BackColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.panel2.BackgroundPath = "readydim";
            this.panel2.CanDrag = false;
            this.panel2.Enabled = true;
            this.panel2.Location = new Microsoft.Xna.Framework.Point(210, 355);
            this.panel2.Name = "";
            this.panel2.Parent = this;
            this.panel2.Size = new Microsoft.Xna.Framework.Point(81, 29);
            this.panel2.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.panel2.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.panel2.Visible = true;
            // 
            // panel3
            // 
            this.panel3.BackColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.panel3.BackgroundPath = "player2";
            this.panel3.CanDrag = false;
            this.panel3.Enabled = true;
            this.panel3.Location = new Microsoft.Xna.Framework.Point(420, 172);
            this.panel3.Name = "";
            this.panel3.Parent = this;
            this.panel3.Size = new Microsoft.Xna.Framework.Point(379, 216);
            this.panel3.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.panel3.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.panel3.Visible = true;
            // 
            // panel4
            // 
            this.panel4.BackColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.panel4.BackgroundPath = "readydim";
            this.panel4.CanDrag = false;
            this.panel4.Enabled = true;
            this.panel4.Location = new Microsoft.Xna.Framework.Point(512, 353);
            this.panel4.Name = "";
            this.panel4.Parent = this;
            this.panel4.Size = new Microsoft.Xna.Framework.Point(81, 29);
            this.panel4.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.panel4.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.panel4.Visible = true;
            // 
            // TwoPlayerWaitScreen
            // 
            this.BackColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(0)), ((byte)(255)));
            this.Panels.Add(this.panel1);
            this.Panels.Add(this.panel2);
            this.Panels.Add(this.panel3);
            this.Panels.Add(this.panel4);

        }


    }
}
