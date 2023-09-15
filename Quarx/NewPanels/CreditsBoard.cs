using System;
using System.Collections.Generic;
using System.Text;

using Sxe.Engine.UI;
using Sxe.Engine.Input;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Quarx
{
    /// <summary>
    /// Class that contains the game board
    /// </summary>
    public class CreditsBoard : CompositePanel
    {
        const int BoardWidth = 8;
        const int BoardHeight = 18;

        GameTime lastGameTime;

        private Panel panel1;
        private Panel panel2;


        private Panel panel4;

        QuarxBoardViewer3D boardViewer;
        BaseGameModel model;
        private MarqueePanel marqueePanel1;

        QuarxGameState lastState;


        public CreditsBoard()
        {
            InitializeComponent();
        }



    
        private void InitializeComponent()
        {
            this.panel1 = new Sxe.Engine.UI.Panel();
            this.panel2 = new Sxe.Engine.UI.Panel();
            this.panel4 = new Sxe.Engine.UI.Panel();
            // 
            // panel1
            // 
            this.panel1.BackColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.panel1.BackgroundPath = "bottom1";
            this.panel1.CanDrag = false;
            this.panel1.Enabled = true;
            this.panel1.Location = new Microsoft.Xna.Framework.Point(5, 473);
            this.panel1.Name = "";
            this.panel1.Parent = this;
            this.panel1.Size = new Microsoft.Xna.Framework.Point(240, 27);
            this.panel1.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 250);
            this.panel1.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 250);
            this.panel1.Visible = true;
            // 
            // panel2
            // 
            this.panel2.BackColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.panel2.BackgroundPath = "top1";
            this.panel2.CanDrag = false;
            this.panel2.Enabled = true;
            this.panel2.Location = new Microsoft.Xna.Framework.Point(5, 0);
            this.panel2.Name = "";
            this.panel2.Parent = this;
            this.panel2.Size = new Microsoft.Xna.Framework.Point(240, 27);
            this.panel2.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, -250);
            this.panel2.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, -250);
            this.panel2.Visible = true;
            // 
            // panel4
            // 
            this.panel4.BackColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.panel4.BackgroundPath = "boardback";
            this.panel4.CanDrag = false;
            this.panel4.Enabled = true;
            this.panel4.Location = new Microsoft.Xna.Framework.Point(0, 0);
            this.panel4.Name = "";
            this.panel4.Parent = this;
            this.panel4.Size = new Microsoft.Xna.Framework.Point(250, 500);
            this.panel4.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.panel4.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.panel4.Visible = true;
            // 
            // CreditsBoard
            // 
            this.Panels.Add(this.panel4);
            this.Panels.Add(this.panel1);
            this.Panels.Add(this.panel2);
            this.Size = new Microsoft.Xna.Framework.Point(250, 500);

        }


        //public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        //{
        //    boardViewer.Draw(gameTime);

        //    panel3.Image.Value = boardViewer.Texture;

        //    base.Draw(spriteBatch, gameTime);
        //}
    }
}
