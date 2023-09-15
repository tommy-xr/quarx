using System;
using System.Collections.Generic;
using System.Text;

using Sxe.Engine.UI;
using Sxe.Engine;

namespace Quarx
{
    public class BoardGamerTag : CompositePanel
    {
        private Panel panel2;
        private Label label1;
        private Panel panel1;

        IAnarchyGamer gamer;
        public IAnarchyGamer Gamer
        {
            get { return gamer; }
            set
            {
                if (gamer != value)
                {
                    gamer = value;
                    this.label1.Caption = gamer.GamerTag;
                    this.panel2.Image = gamer.GamerIcon;
                }

            }
        }
        
    
        public BoardGamerTag()
        {
            InitializeComponent();
        }

        void InitializeComponent()
        {
            this.panel1 = new Sxe.Engine.UI.Panel();
            this.panel2 = new Sxe.Engine.UI.Panel();
            this.label1 = new Sxe.Engine.UI.Label();
            // 
            // panel1
            // 
            this.panel1.BackgroundPath = "boardgamertag";
            this.panel1.CanDrag = false;
            this.panel1.Location = new Microsoft.Xna.Framework.Point(3, 4);
            this.panel1.Parent = this;
            this.panel1.Size = new Microsoft.Xna.Framework.Point(164, 51);
            this.panel1.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.panel1.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            // 
            // panel2
            // 
            this.panel2.BackgroundPath = "abutton";
            this.panel2.CanDrag = false;
            this.panel2.Location = new Microsoft.Xna.Framework.Point(11, 11);
            this.panel2.Parent = this;
            this.panel2.Size = new Microsoft.Xna.Framework.Point(24, 22);
            this.panel2.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.panel2.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            // 
            // label1
            // 
            this.label1.BackgroundPath = null;
            this.label1.CanDrag = false;
            this.label1.Caption = "hey";
            this.label1.FontColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.label1.FontPath = "Calibri11";
            this.label1.HorizontalAlignment = Sxe.Engine.UI.HorizontalAlignment.Left;
            this.label1.Location = new Microsoft.Xna.Framework.Point(45, 12);
            this.label1.Parent = this;
            this.label1.Selection = new Microsoft.Xna.Framework.Point(-1, -1);
            this.label1.Size = new Microsoft.Xna.Framework.Point(109, 19);
            this.label1.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.label1.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.label1.VerticalAlignment = Sxe.Engine.UI.VerticalAlignment.Top;
            // 
            // BoardGamerTag
            // 
            this.Panels.Add(this.panel1);
            this.Panels.Add(this.panel2);
            this.Panels.Add(this.label1);
            this.Size = new Microsoft.Xna.Framework.Point(164, 51);

        }
    }
}
