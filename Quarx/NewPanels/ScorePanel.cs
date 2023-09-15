using System;
using System.Collections.Generic;
using System.Text;

using Sxe.Engine.UI;

namespace Quarx
{
    public class ScorePanel : CompositePanel
    {
        private Label label1;
        private Label label2;

        int player = 1;
        public int Player
        {
            get { return player; }
            set { player = value; }
        }

        int score;
        private RoundPanel roundPanel1;
    
        public int Score
        {
            get { return score; }
            set { score = value; this.label2.Caption = this.score.ToString(); }
        }

        int isotopes;
        public int Isotopes
        {
            get { return isotopes; }
            set { isotopes = value; this.label1.Caption = this.isotopes.ToString(); }
        }

        public int RoundsWon
        {
            get { return this.roundPanel1.RoundsWon; }
            set { this.roundPanel1.RoundsWon = value; }
        }

        public int MaxRounds
        {
            get { return this.roundPanel1.MaxRounds; }
            set { this.roundPanel1.MaxRounds = value; }
        }

        public bool RoundsVisible
        {
            get { return this.roundPanel1.Visible; }
            set { this.roundPanel1.Visible = value; }
        }


        public ScorePanel()
        {
            InitializeComponent();
        }

        public override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            base.LoadContent(content);

            if (player == 1)
                this.Image = LoadImage(content, "score1");
            else
                this.Image = LoadImage(content, "score2");
        }

        void InitializeComponent()
        {
            this.label1 = new Sxe.Engine.UI.Label();
            this.label2 = new Sxe.Engine.UI.Label();
            this.roundPanel1 = new Quarx.RoundPanel();
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
            this.label1.Location = new Microsoft.Xna.Framework.Point(26, 89);
            this.label1.Name = "";
            this.label1.Parent = this;
            this.label1.Selection = new Microsoft.Xna.Framework.Point(-1, -1);
            this.label1.Size = new Microsoft.Xna.Framework.Point(23, 19);
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
            this.label2.Caption = "";
            this.label2.Enabled = true;
            this.label2.FontColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.label2.FontPath = "Neuropol";
            this.label2.HorizontalAlignment = Sxe.Engine.UI.HorizontalAlignment.Center;
            this.label2.Location = new Microsoft.Xna.Framework.Point(30, 41);
            this.label2.Name = "";
            this.label2.Parent = this;
            this.label2.Selection = new Microsoft.Xna.Framework.Point(-1, -1);
            this.label2.Size = new Microsoft.Xna.Framework.Point(49, 19);
            this.label2.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.label2.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.label2.VerticalAlignment = Sxe.Engine.UI.VerticalAlignment.Middle;
            this.label2.Visible = true;
            // 
            // roundPanel1
            // 
            this.roundPanel1.BackColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.roundPanel1.BackgroundPath = null;
            this.roundPanel1.CanDrag = false;
            this.roundPanel1.Enabled = true;
            this.roundPanel1.Location = new Microsoft.Xna.Framework.Point(40, 2);
            this.roundPanel1.Name = "";
            this.roundPanel1.Parent = this;
            this.roundPanel1.RoundsWon = 0;
            this.roundPanel1.Size = new Microsoft.Xna.Framework.Point(60, 20);
            this.roundPanel1.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.roundPanel1.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.roundPanel1.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.roundPanel1.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.roundPanel1.Visible = true;
            // 
            // ScorePanel
            // 
            this.BackgroundPath = "score1";
            this.Panels.Add(this.label1);
            this.Panels.Add(this.label2);
            this.Panels.Add(this.roundPanel1);
            this.Size = new Microsoft.Xna.Framework.Point(110, 125);

        }
    }
}
