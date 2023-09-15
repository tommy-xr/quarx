using System;
using System.Collections.Generic;
using System.Text;

using Sxe.Engine.UI;
using Microsoft.Xna.Framework.Graphics;

namespace Quarx
{
    public class ScoreBoard : CompositePanel
    {
        private Label label1;
        private Label label2;
        private Label label3;
        private Panel panel1;

        int isotopes;
        private Panel panel2;
        private Panel panel3;
        private Panel panel4;
        int score;
        int roundsWon = 0;

        UIImage buttonDim;
        UIImage buttonBright;

        public int RoundsWon
        {
            get { return roundsWon; }
            set
            {
                roundsWon = value;
                this.SetRoundsWon(value);
            }
        }

        public int MaxRounds
        {
            set { this.SetMaxRounds(value); }
        }

        public int Isotopes
        {
            get { return this.isotopes; }
            set { this.isotopes = value; this.label3.Caption = this.isotopes.ToString(); }
        }

        public int Score
        {
            get { return this.score; }
            set { this.score = value; this.label2.Caption = this.score.ToString(); }
        }

        public string PunishName
        {
            get { return label1.Caption; }
            set { label1.Caption = value; }
        }

        public ScoreBoard()
        {
            InitializeComponent();
        }

        public override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            //this.BackgroundPath = null;

            base.LoadContent(content);

            this.buttonBright = this.LoadImage(content, "buttonbright");
            this.buttonDim = this.LoadImage(content, "buttondim");
        }
        public void SetPreviewTexture(Texture2D previewTexture)
        {
            if (this.panel1.Image == null)
                this.panel1.Image = new UIImage();

            panel1.Image.Value = previewTexture;
        }

        public void SetMaxRounds(int maxRounds)
        {
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;

            if (maxRounds >= 1)
                panel4.Visible = true;
            if (maxRounds >= 2)
                panel3.Visible = true;
            if (maxRounds >= 3)
                panel2.Visible = true;
        }

        public void SetRoundsWon(int roundsWon)
        {
            this.panel2.Image = this.buttonDim;
            this.panel3.Image = this.buttonDim;
            this.panel4.Image = this.buttonDim;

            if (roundsWon >= 1)
                this.panel4.Image = this.buttonBright;

            if (roundsWon >= 2)
                this.panel3.Image = this.buttonBright;

            if (roundsWon >= 3)
                this.panel2.Image = this.buttonBright;
            
        }

        public override void Paint(SpriteBatch sb, Microsoft.Xna.Framework.Point positionOffset, Microsoft.Xna.Framework.Vector2 scale)
        {
            string text = this.label3.Caption;
            base.Paint(sb, positionOffset, scale);
        }

        void InitializeComponent()
        {
            this.label1 = new Sxe.Engine.UI.Label();
            this.label2 = new Sxe.Engine.UI.Label();
            this.label3 = new Sxe.Engine.UI.Label();
            this.panel1 = new Sxe.Engine.UI.Panel();
            this.panel2 = new Sxe.Engine.UI.Panel();
            this.panel3 = new Sxe.Engine.UI.Panel();
            this.panel4 = new Sxe.Engine.UI.Panel();
            // 
            // label1
            // 
            this.label1.BackgroundPath = null;
            this.label1.Caption = "test";
            this.label1.FontColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.label1.FontPath = "Calibri11";
            this.label1.HorizontalAlignment = Sxe.Engine.UI.HorizontalAlignment.Center;
            this.label1.Location = new Microsoft.Xna.Framework.Point(19, 30);
            this.label1.Parent = this;
            this.label1.Selection = new Microsoft.Xna.Framework.Point(-1, -1);
            this.label1.Size = new Microsoft.Xna.Framework.Point(201, 21);
            this.label1.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.label1.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.label1.VerticalAlignment = Sxe.Engine.UI.VerticalAlignment.Middle;
            // 
            // label2
            // 
            this.label2.BackgroundPath = null;
            this.label2.Caption = "100";
            this.label2.FontColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.label2.FontPath = "Calibri11";
            this.label2.HorizontalAlignment = Sxe.Engine.UI.HorizontalAlignment.Center;
            this.label2.Location = new Microsoft.Xna.Framework.Point(52, 54);
            this.label2.Parent = this;
            this.label2.Selection = new Microsoft.Xna.Framework.Point(-1, -1);
            this.label2.Size = new Microsoft.Xna.Framework.Point(63, 24);
            this.label2.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.label2.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.label2.VerticalAlignment = Sxe.Engine.UI.VerticalAlignment.Middle;
            // 
            // label3
            // 
            this.label3.BackgroundPath = null;
            this.label3.Caption = "10";
            this.label3.FontColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.label3.FontPath = "Calibri11";
            this.label3.HorizontalAlignment = Sxe.Engine.UI.HorizontalAlignment.Center;
            this.label3.Location = new Microsoft.Xna.Framework.Point(80, 89);
            this.label3.Parent = this;
            this.label3.Selection = new Microsoft.Xna.Framework.Point(-1, -1);
            this.label3.Size = new Microsoft.Xna.Framework.Point(52, 24);
            this.label3.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.label3.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.label3.VerticalAlignment = Sxe.Engine.UI.VerticalAlignment.Middle;
            // 
            // panel1
            // 
            this.panel1.BackgroundPath = null;
            this.panel1.Location = new Microsoft.Xna.Framework.Point(194, 67);
            this.panel1.Parent = this;
            this.panel1.Size = new Microsoft.Xna.Framework.Point(33, 32);
            this.panel1.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.panel1.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            // 
            // panel2
            // 
            this.panel2.BackgroundPath = "buttondim";
            this.panel2.Location = new Microsoft.Xna.Framework.Point(170, 55);
            this.panel2.Parent = this;
            this.panel2.Size = new Microsoft.Xna.Framework.Point(17, 17);
            this.panel2.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.panel2.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            // 
            // panel3
            // 
            this.panel3.BackgroundPath = "buttondim";
            this.panel3.Location = new Microsoft.Xna.Framework.Point(170, 74);
            this.panel3.Parent = this;
            this.panel3.Size = new Microsoft.Xna.Framework.Point(17, 17);
            this.panel3.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.panel3.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            // 
            // panel4
            // 
            this.panel4.BackgroundPath = "buttondim";
            this.panel4.Location = new Microsoft.Xna.Framework.Point(170, 93);
            this.panel4.Parent = this;
            this.panel4.Size = new Microsoft.Xna.Framework.Point(17, 17);
            this.panel4.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.panel4.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            // 
            // ScoreBoard
            // 
            this.BackgroundPath = "scoreboard";
            this.Panels.Add(this.panel1);
            this.Panels.Add(this.panel2);
            this.Panels.Add(this.panel3);
            this.Panels.Add(this.panel4);
            this.Panels.Add(this.label1);
            this.Panels.Add(this.label2);
            this.Panels.Add(this.label3);
            this.Size = new Microsoft.Xna.Framework.Point(240, 120);

        }
    }
}
