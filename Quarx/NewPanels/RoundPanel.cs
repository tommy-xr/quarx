using System;
using System.Collections.Generic;
using System.Text;

using Sxe.Engine.UI;

namespace Quarx
{
    /// <summary>
    /// Panel for displaying the number of rounds won
    /// </summary>
    public class RoundPanel : CompositePanel
    {
        UIImage dimButton;
        UIImage brightButton;

        private Panel panel2;
        private Panel panel3;
        private Panel panel1;

        int roundsWon;
        public int RoundsWon
        {
            get { return roundsWon; }
            set
            {
                roundsWon = value;

                panel1.Image = dimButton;
                panel2.Image = dimButton;
                panel3.Image = dimButton;

                if (roundsWon >= 1)
                    panel1.Image = brightButton;
                if (roundsWon >= 2)
                    panel2.Image = brightButton;
                if(roundsWon >= 3)
                    panel3.Image = brightButton;
            }
        }

        int maxRounds;
        public int MaxRounds
        {
            get { return maxRounds; }
            set
            {
                maxRounds = value;

                panel1.Visible = false;
                panel2.Visible = false;
                panel3.Visible = false;

                if (maxRounds >= 1)
                    panel1.Visible = true;
                if (maxRounds >= 2)
                    panel2.Visible = true;
                if (maxRounds >= 3)
                    panel3.Visible = true;
            }
        }
    
        public RoundPanel()
        {
            InitializeComponent();
        }

        public override void  LoadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
 	         base.LoadContent(content);

            dimButton = LoadImage(content, "buttondim");
            brightButton = LoadImage(content, "buttonbright");
        }

        void InitializeComponent()
        {
            this.panel1 = new Sxe.Engine.UI.Panel();
            this.panel2 = new Sxe.Engine.UI.Panel();
            this.panel3 = new Sxe.Engine.UI.Panel();
            // 
            // panel1
            // 
            this.panel1.BackColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.panel1.BackgroundPath = "buttondim";
            this.panel1.CanDrag = false;
            this.panel1.Enabled = true;
            this.panel1.Location = new Microsoft.Xna.Framework.Point(0, 0);
            this.panel1.Name = "";
            this.panel1.Parent = this;
            this.panel1.Size = new Microsoft.Xna.Framework.Point(20, 20);
            this.panel1.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.panel1.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.panel1.Visible = true;
            // 
            // panel2
            // 
            this.panel2.BackColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.panel2.BackgroundPath = "buttondim";
            this.panel2.CanDrag = false;
            this.panel2.Enabled = true;
            this.panel2.Location = new Microsoft.Xna.Framework.Point(20, 0);
            this.panel2.Name = "";
            this.panel2.Parent = this;
            this.panel2.Size = new Microsoft.Xna.Framework.Point(20, 20);
            this.panel2.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.panel2.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.panel2.Visible = true;
            // 
            // panel3
            // 
            this.panel3.BackColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.panel3.BackgroundPath = "buttondim";
            this.panel3.CanDrag = false;
            this.panel3.Enabled = true;
            this.panel3.Location = new Microsoft.Xna.Framework.Point(40, 0);
            this.panel3.Name = "";
            this.panel3.Parent = this;
            this.panel3.Size = new Microsoft.Xna.Framework.Point(20, 20);
            this.panel3.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.panel3.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.panel3.Visible = true;
            // 
            // RoundPanel
            // 
            this.Panels.Add(this.panel1);
            this.Panels.Add(this.panel2);
            this.Panels.Add(this.panel3);
            this.Size = new Microsoft.Xna.Framework.Point(60, 20);

        }
    }
}
