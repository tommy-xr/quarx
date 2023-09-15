using System;
using System.Collections.Generic;
using System.Text;

using Sxe.Engine.UI;

namespace Quarx
{
    public class HighScorePanel : CompositePanel
    {
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private Label label10;
        private Label label11;
        private Label label12;
        private Label label13;
        private Label label14;
        private Label label15;
        private Label label16;

        bool hasLoaded = false;

        HighScoreList list;
        public HighScoreList SelectedList
        {
            get { return list; }
            set 
            {
                if (this.list != value)
                {
                    list = value;
                    RefreshList();
                }
            }
        }

        void RefreshList()
        {
            if (!HighScores.HasLoaded)
                return;

            if (list != null)
            {
                label1.Caption = list[0].Name;
                label9.Caption = list[0].Score.ToString();
                label2.Caption = list[1].Name;
                label10.Caption = list[1].Score.ToString();
                label3.Caption = list[2].Name;
                label11.Caption = list[2].Score.ToString();
                label4.Caption = list[3].Name;
                label12.Caption = list[3].Score.ToString();
                label5.Caption = list[4].Name;
                label13.Caption = list[4].Score.ToString();
                label6.Caption = list[5].Name;
                label14.Caption = list[5].Score.ToString();
                label7.Caption = list[6].Name;
                label15.Caption = list[6].Score.ToString();
                label8.Caption = list[7].Name;
                label16.Caption = list[7].Score.ToString();

            }
        }
        public HighScorePanel()
        {
            InitializeComponent();
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (!hasLoaded && HighScores.HasLoaded)
            {
                hasLoaded = true;
                RefreshList();
            }

            base.Update(gameTime);
        }

        void InitializeComponent()
        {
            this.label1 = new Sxe.Engine.UI.Label();
            this.label2 = new Sxe.Engine.UI.Label();
            this.label3 = new Sxe.Engine.UI.Label();
            this.label4 = new Sxe.Engine.UI.Label();
            this.label5 = new Sxe.Engine.UI.Label();
            this.label6 = new Sxe.Engine.UI.Label();
            this.label7 = new Sxe.Engine.UI.Label();
            this.label8 = new Sxe.Engine.UI.Label();
            this.label9 = new Sxe.Engine.UI.Label();
            this.label10 = new Sxe.Engine.UI.Label();
            this.label11 = new Sxe.Engine.UI.Label();
            this.label12 = new Sxe.Engine.UI.Label();
            this.label13 = new Sxe.Engine.UI.Label();
            this.label14 = new Sxe.Engine.UI.Label();
            this.label15 = new Sxe.Engine.UI.Label();
            this.label16 = new Sxe.Engine.UI.Label();
            // 
            // label1
            // 
            this.label1.BackColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.label1.BackgroundPath = null;
            this.label1.CanDrag = false;
            this.label1.Caption = "";
            this.label1.Enabled = true;
            this.label1.FontColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.label1.FontPath = "Neuropol";
            this.label1.HorizontalAlignment = Sxe.Engine.UI.HorizontalAlignment.Left;
            this.label1.Location = new Microsoft.Xna.Framework.Point(139, 75);
            this.label1.Name = "";
            this.label1.Parent = this;
            this.label1.Selection = new Microsoft.Xna.Framework.Point(-1, -1);
            this.label1.Size = new Microsoft.Xna.Framework.Point(274, 29);
            this.label1.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.label1.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.label1.VerticalAlignment = Sxe.Engine.UI.VerticalAlignment.Middle;
            this.label1.Visible = true;
            // 
            // label2
            // 
            this.label2.BackColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.label2.BackgroundPath = null;
            this.label2.CanDrag = false;
            this.label2.Caption = "";
            this.label2.Enabled = true;
            this.label2.FontColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.label2.FontPath = "Neuropol";
            this.label2.HorizontalAlignment = Sxe.Engine.UI.HorizontalAlignment.Left;
            this.label2.Location = new Microsoft.Xna.Framework.Point(137, 123);
            this.label2.Name = "";
            this.label2.Parent = this;
            this.label2.Selection = new Microsoft.Xna.Framework.Point(-1, -1);
            this.label2.Size = new Microsoft.Xna.Framework.Point(274, 29);
            this.label2.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.label2.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.label2.VerticalAlignment = Sxe.Engine.UI.VerticalAlignment.Middle;
            this.label2.Visible = true;
            // 
            // label3
            // 
            this.label3.BackColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.label3.BackgroundPath = null;
            this.label3.CanDrag = false;
            this.label3.Caption = "";
            this.label3.Enabled = true;
            this.label3.FontColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.label3.FontPath = "Neuropol";
            this.label3.HorizontalAlignment = Sxe.Engine.UI.HorizontalAlignment.Left;
            this.label3.Location = new Microsoft.Xna.Framework.Point(137, 165);
            this.label3.Name = "";
            this.label3.Parent = this;
            this.label3.Selection = new Microsoft.Xna.Framework.Point(-1, -1);
            this.label3.Size = new Microsoft.Xna.Framework.Point(274, 29);
            this.label3.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.label3.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.label3.VerticalAlignment = Sxe.Engine.UI.VerticalAlignment.Middle;
            this.label3.Visible = true;
            // 
            // label4
            // 
            this.label4.BackColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.label4.BackgroundPath = null;
            this.label4.CanDrag = false;
            this.label4.Caption = "";
            this.label4.Enabled = true;
            this.label4.FontColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.label4.FontPath = "Neuropol";
            this.label4.HorizontalAlignment = Sxe.Engine.UI.HorizontalAlignment.Left;
            this.label4.Location = new Microsoft.Xna.Framework.Point(137, 211);
            this.label4.Name = "";
            this.label4.Parent = this;
            this.label4.Selection = new Microsoft.Xna.Framework.Point(-1, -1);
            this.label4.Size = new Microsoft.Xna.Framework.Point(274, 29);
            this.label4.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.label4.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.label4.VerticalAlignment = Sxe.Engine.UI.VerticalAlignment.Middle;
            this.label4.Visible = true;
            // 
            // label5
            // 
            this.label5.BackColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.label5.BackgroundPath = null;
            this.label5.CanDrag = false;
            this.label5.Caption = "";
            this.label5.Enabled = true;
            this.label5.FontColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.label5.FontPath = "Neuropol";
            this.label5.HorizontalAlignment = Sxe.Engine.UI.HorizontalAlignment.Left;
            this.label5.Location = new Microsoft.Xna.Framework.Point(138, 258);
            this.label5.Name = "";
            this.label5.Parent = this;
            this.label5.Selection = new Microsoft.Xna.Framework.Point(-1, -1);
            this.label5.Size = new Microsoft.Xna.Framework.Point(274, 29);
            this.label5.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.label5.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.label5.VerticalAlignment = Sxe.Engine.UI.VerticalAlignment.Middle;
            this.label5.Visible = true;
            // 
            // label6
            // 
            this.label6.BackColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.label6.BackgroundPath = null;
            this.label6.CanDrag = false;
            this.label6.Caption = "";
            this.label6.Enabled = true;
            this.label6.FontColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.label6.FontPath = "Neuropol";
            this.label6.HorizontalAlignment = Sxe.Engine.UI.HorizontalAlignment.Left;
            this.label6.Location = new Microsoft.Xna.Framework.Point(138, 303);
            this.label6.Name = "";
            this.label6.Parent = this;
            this.label6.Selection = new Microsoft.Xna.Framework.Point(-1, -1);
            this.label6.Size = new Microsoft.Xna.Framework.Point(274, 29);
            this.label6.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.label6.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.label6.VerticalAlignment = Sxe.Engine.UI.VerticalAlignment.Middle;
            this.label6.Visible = true;
            // 
            // label7
            // 
            this.label7.BackColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.label7.BackgroundPath = null;
            this.label7.CanDrag = false;
            this.label7.Caption = "";
            this.label7.Enabled = true;
            this.label7.FontColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.label7.FontPath = "Neuropol";
            this.label7.HorizontalAlignment = Sxe.Engine.UI.HorizontalAlignment.Left;
            this.label7.Location = new Microsoft.Xna.Framework.Point(138, 347);
            this.label7.Name = "";
            this.label7.Parent = this;
            this.label7.Selection = new Microsoft.Xna.Framework.Point(-1, -1);
            this.label7.Size = new Microsoft.Xna.Framework.Point(274, 29);
            this.label7.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.label7.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.label7.VerticalAlignment = Sxe.Engine.UI.VerticalAlignment.Middle;
            this.label7.Visible = true;
            // 
            // label8
            // 
            this.label8.BackColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.label8.BackgroundPath = null;
            this.label8.CanDrag = false;
            this.label8.Caption = "";
            this.label8.Enabled = true;
            this.label8.FontColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.label8.FontPath = "Neuropol";
            this.label8.HorizontalAlignment = Sxe.Engine.UI.HorizontalAlignment.Left;
            this.label8.Location = new Microsoft.Xna.Framework.Point(140, 393);
            this.label8.Name = "";
            this.label8.Parent = this;
            this.label8.Selection = new Microsoft.Xna.Framework.Point(-1, -1);
            this.label8.Size = new Microsoft.Xna.Framework.Point(274, 29);
            this.label8.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.label8.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.label8.VerticalAlignment = Sxe.Engine.UI.VerticalAlignment.Middle;
            this.label8.Visible = true;
            // 
            // label9
            // 
            this.label9.BackColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.label9.BackgroundPath = null;
            this.label9.CanDrag = false;
            this.label9.Caption = "";
            this.label9.Enabled = true;
            this.label9.FontColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.label9.FontPath = "Neuropol";
            this.label9.HorizontalAlignment = Sxe.Engine.UI.HorizontalAlignment.Left;
            this.label9.Location = new Microsoft.Xna.Framework.Point(486, 72);
            this.label9.Name = "";
            this.label9.Parent = this;
            this.label9.Selection = new Microsoft.Xna.Framework.Point(-1, -1);
            this.label9.Size = new Microsoft.Xna.Framework.Point(84, 34);
            this.label9.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.label9.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.label9.VerticalAlignment = Sxe.Engine.UI.VerticalAlignment.Middle;
            this.label9.Visible = true;
            // 
            // label10
            // 
            this.label10.BackColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.label10.BackgroundPath = null;
            this.label10.CanDrag = false;
            this.label10.Caption = "";
            this.label10.Enabled = true;
            this.label10.FontColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.label10.FontPath = "Neuropol";
            this.label10.HorizontalAlignment = Sxe.Engine.UI.HorizontalAlignment.Left;
            this.label10.Location = new Microsoft.Xna.Framework.Point(486, 117);
            this.label10.Name = "";
            this.label10.Parent = this;
            this.label10.Selection = new Microsoft.Xna.Framework.Point(-1, -1);
            this.label10.Size = new Microsoft.Xna.Framework.Point(84, 34);
            this.label10.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.label10.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.label10.VerticalAlignment = Sxe.Engine.UI.VerticalAlignment.Middle;
            this.label10.Visible = true;
            // 
            // label11
            // 
            this.label11.BackColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.label11.BackgroundPath = null;
            this.label11.CanDrag = false;
            this.label11.Caption = "";
            this.label11.Enabled = true;
            this.label11.FontColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.label11.FontPath = "Neuropol";
            this.label11.HorizontalAlignment = Sxe.Engine.UI.HorizontalAlignment.Left;
            this.label11.Location = new Microsoft.Xna.Framework.Point(486, 163);
            this.label11.Name = "";
            this.label11.Parent = this;
            this.label11.Selection = new Microsoft.Xna.Framework.Point(-1, -1);
            this.label11.Size = new Microsoft.Xna.Framework.Point(84, 34);
            this.label11.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.label11.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.label11.VerticalAlignment = Sxe.Engine.UI.VerticalAlignment.Middle;
            this.label11.Visible = true;
            // 
            // label12
            // 
            this.label12.BackColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.label12.BackgroundPath = null;
            this.label12.CanDrag = false;
            this.label12.Caption = "";
            this.label12.Enabled = true;
            this.label12.FontColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.label12.FontPath = "Neuropol";
            this.label12.HorizontalAlignment = Sxe.Engine.UI.HorizontalAlignment.Left;
            this.label12.Location = new Microsoft.Xna.Framework.Point(486, 209);
            this.label12.Name = "";
            this.label12.Parent = this;
            this.label12.Selection = new Microsoft.Xna.Framework.Point(-1, -1);
            this.label12.Size = new Microsoft.Xna.Framework.Point(84, 34);
            this.label12.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.label12.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.label12.VerticalAlignment = Sxe.Engine.UI.VerticalAlignment.Middle;
            this.label12.Visible = true;
            // 
            // label13
            // 
            this.label13.BackColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.label13.BackgroundPath = null;
            this.label13.CanDrag = false;
            this.label13.Caption = "";
            this.label13.Enabled = true;
            this.label13.FontColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.label13.FontPath = "Neuropol";
            this.label13.HorizontalAlignment = Sxe.Engine.UI.HorizontalAlignment.Left;
            this.label13.Location = new Microsoft.Xna.Framework.Point(486, 253);
            this.label13.Name = "";
            this.label13.Parent = this;
            this.label13.Selection = new Microsoft.Xna.Framework.Point(-1, -1);
            this.label13.Size = new Microsoft.Xna.Framework.Point(84, 34);
            this.label13.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.label13.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.label13.VerticalAlignment = Sxe.Engine.UI.VerticalAlignment.Middle;
            this.label13.Visible = true;
            // 
            // label14
            // 
            this.label14.BackColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.label14.BackgroundPath = null;
            this.label14.CanDrag = false;
            this.label14.Caption = "";
            this.label14.Enabled = true;
            this.label14.FontColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.label14.FontPath = "Neuropol";
            this.label14.HorizontalAlignment = Sxe.Engine.UI.HorizontalAlignment.Left;
            this.label14.Location = new Microsoft.Xna.Framework.Point(486, 299);
            this.label14.Name = "";
            this.label14.Parent = this;
            this.label14.Selection = new Microsoft.Xna.Framework.Point(-1, -1);
            this.label14.Size = new Microsoft.Xna.Framework.Point(84, 34);
            this.label14.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.label14.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.label14.VerticalAlignment = Sxe.Engine.UI.VerticalAlignment.Middle;
            this.label14.Visible = true;
            // 
            // label15
            // 
            this.label15.BackColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.label15.BackgroundPath = null;
            this.label15.CanDrag = false;
            this.label15.Caption = "";
            this.label15.Enabled = true;
            this.label15.FontColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.label15.FontPath = "Neuropol";
            this.label15.HorizontalAlignment = Sxe.Engine.UI.HorizontalAlignment.Left;
            this.label15.Location = new Microsoft.Xna.Framework.Point(486, 343);
            this.label15.Name = "";
            this.label15.Parent = this;
            this.label15.Selection = new Microsoft.Xna.Framework.Point(-1, -1);
            this.label15.Size = new Microsoft.Xna.Framework.Point(84, 34);
            this.label15.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.label15.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.label15.VerticalAlignment = Sxe.Engine.UI.VerticalAlignment.Middle;
            this.label15.Visible = true;
            // 
            // label16
            // 
            this.label16.BackColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.label16.BackgroundPath = null;
            this.label16.CanDrag = false;
            this.label16.Caption = "";
            this.label16.Enabled = true;
            this.label16.FontColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.label16.FontPath = "Neuropol";
            this.label16.HorizontalAlignment = Sxe.Engine.UI.HorizontalAlignment.Left;
            this.label16.Location = new Microsoft.Xna.Framework.Point(486, 388);
            this.label16.Name = "";
            this.label16.Parent = this;
            this.label16.Selection = new Microsoft.Xna.Framework.Point(-1, -1);
            this.label16.Size = new Microsoft.Xna.Framework.Point(84, 34);
            this.label16.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.label16.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.label16.VerticalAlignment = Sxe.Engine.UI.VerticalAlignment.Middle;
            this.label16.Visible = true;
            // 
            // HighScorePanel
            // 
            this.BackgroundPath = "highscoresbackground";
            this.Panels.Add(this.label1);
            this.Panels.Add(this.label2);
            this.Panels.Add(this.label3);
            this.Panels.Add(this.label4);
            this.Panels.Add(this.label5);
            this.Panels.Add(this.label6);
            this.Panels.Add(this.label7);
            this.Panels.Add(this.label8);
            this.Panels.Add(this.label9);
            this.Panels.Add(this.label10);
            this.Panels.Add(this.label11);
            this.Panels.Add(this.label12);
            this.Panels.Add(this.label13);
            this.Panels.Add(this.label14);
            this.Panels.Add(this.label15);
            this.Panels.Add(this.label16);
            this.Size = new Microsoft.Xna.Framework.Point(625, 450);

        }
    }
}
