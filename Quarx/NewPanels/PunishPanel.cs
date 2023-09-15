using System;
using System.Collections.Generic;
using System.Text;

using Sxe.Engine.UI;

namespace Quarx
{
    public class PunishPanel : CompositePanel
    {
        private Panel panel2;
        private Panel panel3;
        private Panel panel4;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Panel panel5;
        private Panel panel1;


        public BaseGameModel Model
        {
            set 
            {
                for (int i = 0; i < value.PunishModels.Count; i++)
                {
                    SetPunish(i, value.PunishModels[i].Gamer.GamerTag);
                }
            }
        }

            public void SetPunish(int i, string text)
        {
            switch (i)
            {
                case 0:
                    label1.Visible = true;
                    label1.Caption = text;
                    panel1.Visible = true;
                    break;

                case 1:
                    label2.Visible = true;
                    label2.Caption = text;
                    panel2.Visible = true;
                    break;

                case 2:
                    label3.Visible = true;
                    label3.Caption = text;
                    panel3.Visible = true;
                    break;
            }
        }
    
        public PunishPanel()
        {
            InitializeComponent();

            label1.Visible = false;
            label2.Visible = false;
            label3.Visible = false;

            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
        }

        void InitializeComponent()
        {
            this.panel1 = new Sxe.Engine.UI.Panel();
            this.panel2 = new Sxe.Engine.UI.Panel();
            this.panel3 = new Sxe.Engine.UI.Panel();
            this.panel4 = new Sxe.Engine.UI.Panel();
            this.label1 = new Sxe.Engine.UI.Label();
            this.label2 = new Sxe.Engine.UI.Label();
            this.label3 = new Sxe.Engine.UI.Label();
            this.label4 = new Sxe.Engine.UI.Label();
            this.panel5 = new Sxe.Engine.UI.Panel();
            // 
            // panel1
            // 
            this.panel1.BackColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.panel1.BackgroundPath = "ybutton";
            this.panel1.CanDrag = false;
            this.panel1.Enabled = true;
            this.panel1.Location = new Microsoft.Xna.Framework.Point(17, 76);
            this.panel1.Name = "";
            this.panel1.Parent = this;
            this.panel1.Size = new Microsoft.Xna.Framework.Point(30, 30);
            this.panel1.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.panel1.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.panel1.Visible = true;
            // 
            // panel2
            // 
            this.panel2.BackColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.panel2.BackgroundPath = "bbutton";
            this.panel2.CanDrag = false;
            this.panel2.Enabled = true;
            this.panel2.Location = new Microsoft.Xna.Framework.Point(16, 112);
            this.panel2.Name = "";
            this.panel2.Parent = this;
            this.panel2.Size = new Microsoft.Xna.Framework.Point(30, 30);
            this.panel2.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.panel2.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.panel2.Visible = true;
            // 
            // panel3
            // 
            this.panel3.BackColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.panel3.BackgroundPath = "abutton";
            this.panel3.CanDrag = false;
            this.panel3.Enabled = true;
            this.panel3.Location = new Microsoft.Xna.Framework.Point(16, 146);
            this.panel3.Name = "";
            this.panel3.Parent = this;
            this.panel3.Size = new Microsoft.Xna.Framework.Point(30, 30);
            this.panel3.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.panel3.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.panel3.Visible = true;
            // 
            // panel4
            // 
            this.panel4.BackColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.panel4.BackgroundPath = "xbutton";
            this.panel4.CanDrag = false;
            this.panel4.Enabled = true;
            this.panel4.Location = new Microsoft.Xna.Framework.Point(48, 188);
            this.panel4.Name = "";
            this.panel4.Parent = this;
            this.panel4.Size = new Microsoft.Xna.Framework.Point(30, 30);
            this.panel4.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.panel4.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.panel4.Visible = true;
            // 
            // label1
            // 
            this.label1.BackColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.label1.BackgroundPath = null;
            this.label1.CanDrag = false;
            this.label1.Caption = "Player1";
            this.label1.Enabled = true;
            this.label1.FontColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.label1.FontPath = "Neuropol";
            this.label1.HorizontalAlignment = Sxe.Engine.UI.HorizontalAlignment.Left;
            this.label1.Location = new Microsoft.Xna.Framework.Point(57, 77);
            this.label1.Name = "";
            this.label1.Parent = this;
            this.label1.Selection = new Microsoft.Xna.Framework.Point(-1, -1);
            this.label1.Size = new Microsoft.Xna.Framework.Point(101, 26);
            this.label1.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.label1.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.label1.VerticalAlignment = Sxe.Engine.UI.VerticalAlignment.Top;
            this.label1.Visible = true;
            // 
            // label2
            // 
            this.label2.BackColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.label2.BackgroundPath = null;
            this.label2.CanDrag = false;
            this.label2.Caption = "Player2";
            this.label2.Enabled = true;
            this.label2.FontColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.label2.FontPath = "Neuropol";
            this.label2.HorizontalAlignment = Sxe.Engine.UI.HorizontalAlignment.Left;
            this.label2.Location = new Microsoft.Xna.Framework.Point(53, 115);
            this.label2.Name = "";
            this.label2.Parent = this;
            this.label2.Selection = new Microsoft.Xna.Framework.Point(-1, -1);
            this.label2.Size = new Microsoft.Xna.Framework.Point(101, 26);
            this.label2.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.label2.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.label2.VerticalAlignment = Sxe.Engine.UI.VerticalAlignment.Top;
            this.label2.Visible = true;
            // 
            // label3
            // 
            this.label3.BackColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.label3.BackgroundPath = null;
            this.label3.CanDrag = false;
            this.label3.Caption = "Player3";
            this.label3.Enabled = true;
            this.label3.FontColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.label3.FontPath = "Neuropol";
            this.label3.HorizontalAlignment = Sxe.Engine.UI.HorizontalAlignment.Left;
            this.label3.Location = new Microsoft.Xna.Framework.Point(51, 146);
            this.label3.Name = "";
            this.label3.Parent = this;
            this.label3.Selection = new Microsoft.Xna.Framework.Point(-1, -1);
            this.label3.Size = new Microsoft.Xna.Framework.Point(101, 26);
            this.label3.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.label3.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.label3.VerticalAlignment = Sxe.Engine.UI.VerticalAlignment.Top;
            this.label3.Visible = true;
            // 
            // label4
            // 
            this.label4.BackColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.label4.BackgroundPath = null;
            this.label4.CanDrag = false;
            this.label4.Caption = "Save";
            this.label4.Enabled = true;
            this.label4.FontColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.label4.FontPath = "Neuropol";
            this.label4.HorizontalAlignment = Sxe.Engine.UI.HorizontalAlignment.Left;
            this.label4.Location = new Microsoft.Xna.Framework.Point(84, 190);
            this.label4.Name = "";
            this.label4.Parent = this;
            this.label4.Selection = new Microsoft.Xna.Framework.Point(-1, -1);
            this.label4.Size = new Microsoft.Xna.Framework.Point(101, 26);
            this.label4.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.label4.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.label4.VerticalAlignment = Sxe.Engine.UI.VerticalAlignment.Top;
            this.label4.Visible = true;
            // 
            // panel5
            // 
            this.panel5.BackColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.panel5.BackgroundPath = "punish";
            this.panel5.CanDrag = false;
            this.panel5.Enabled = true;
            this.panel5.Location = new Microsoft.Xna.Framework.Point(7, 14);
            this.panel5.Name = "";
            this.panel5.Parent = this;
            this.panel5.Size = new Microsoft.Xna.Framework.Point(139, 50);
            this.panel5.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.panel5.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.panel5.Visible = true;
            // 
            // PunishPanel
            // 
            this.BackgroundPath = "boardback";
            this.Panels.Add(this.panel1);
            this.Panels.Add(this.panel2);
            this.Panels.Add(this.panel3);
            this.Panels.Add(this.panel4);
            this.Panels.Add(this.label1);
            this.Panels.Add(this.label2);
            this.Panels.Add(this.label3);
            this.Panels.Add(this.label4);
            this.Panels.Add(this.panel5);
            this.Size = new Microsoft.Xna.Framework.Point(150, 225);

        }
    }
}
