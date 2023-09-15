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
    public class MiniQuarxBoard : CompositePanel
    {
        const int BoardWidth = 8;
        const int BoardHeight = 18;

        GameTime lastGameTime;

        private Panel panel1;
        private Panel panel2;
        private Panel panel3;


        private Panel panel4;
        private VictoryDefeatPanel victoryDefeatPanel1;
        QuarxBoardViewer3D boardViewer;
        BaseGameModel model;
        private MarqueePanel marqueePanel1;
        private BoardGamerTag boardGamerTag1;

        QuarxGameState lastState;

        public BaseGameModel Model
        {
            get { return model; }
            set 
            { 
                model = value;
                if (model != null)
                {
                    boardViewer.Model = model;
                    //punishPanel1.Model = model;
                }
            }
        }

        public QuarxBoardViewer3D Viewer
        {
            get { return boardViewer; }
        }

        public MarqueePanel Marquee
        {
            get { return marqueePanel1; }
        }

        public MiniQuarxBoard()
        {
            InitializeComponent();
        }

        public void Reset()
        {
            this.victoryDefeatPanel1.Visible = false;
            this.marqueePanel1.Visible = false;
        }

        public override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            base.LoadContent(content);

            IGraphicsDeviceService graphics = content.ServiceProvider.GetService(typeof(IGraphicsDeviceService)) as IGraphicsDeviceService;


            boardViewer = new QuarxBoardViewer3D(graphics.GraphicsDevice, content, BoardWidth, BoardHeight);
        }



        public override void Update(GameTime gameTime)
        {

            if (lastState != model.State)
            {
                if (model.State == QuarxGameState.Won)
                {
                    victoryDefeatPanel1.Victorious = true;
                    victoryDefeatPanel1.Show();
                }
                else if (model.State == QuarxGameState.Lost)
                {
                    victoryDefeatPanel1.Victorious = false;
                    victoryDefeatPanel1.Show();
                }
                else if (model.State == QuarxGameState.Playing && victoryDefeatPanel1.Visible)
                {
                    victoryDefeatPanel1.Hide();
                }


             

                lastState = model.State;
            }

            if (model != null)
            {
                string sound = model.GetNextSound();
                while (sound != null)
                {
                    Audio.PlayCue(sound);
                sound = model.GetNextSound();
                }

            }



            lastGameTime = gameTime;

            base.Update(gameTime);
        }

        public void PreDraw(GameTime gameTime)
        {
            boardViewer.Draw(lastGameTime);
        }

        public override void Paint(SpriteBatch sb, Point positionOffset, Vector2 scale)
        {


            if (this.panel3.Image != null && boardViewer.Texture != null)
                this.panel3.Image.Value = boardViewer.Texture;

            base.Paint(sb, positionOffset, scale);
        }

    
        private void InitializeComponent()
        {
            this.panel1 = new Sxe.Engine.UI.Panel();
            this.panel2 = new Sxe.Engine.UI.Panel();
            this.panel3 = new Sxe.Engine.UI.Panel();
            this.panel4 = new Sxe.Engine.UI.Panel();
            this.victoryDefeatPanel1 = new Quarx.VictoryDefeatPanel();
            this.marqueePanel1 = new Quarx.MarqueePanel();
            this.boardGamerTag1 = new Quarx.BoardGamerTag();
            // 
            // panel1
            // 
            this.panel1.BackColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.panel1.BackgroundPath = "bottom1";
            this.panel1.CanDrag = false;
            this.panel1.Enabled = true;
            this.panel1.Location = new Microsoft.Xna.Framework.Point(28, 341);
            this.panel1.Name = "";
            this.panel1.Parent = this;
            this.panel1.Size = new Microsoft.Xna.Framework.Point(143, 20);
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
            this.panel2.Location = new Microsoft.Xna.Framework.Point(28, 46);
            this.panel2.Name = "";
            this.panel2.Parent = this;
            this.panel2.Size = new Microsoft.Xna.Framework.Point(143, 20);
            this.panel2.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, -250);
            this.panel2.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, -250);
            this.panel2.Visible = true;
            // 
            // panel3
            // 
            this.panel3.BackColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.panel3.BackgroundPath = "background";
            this.panel3.CanDrag = false;
            this.panel3.Enabled = true;
            this.panel3.Location = new Microsoft.Xna.Framework.Point(36, 50);
            this.panel3.Name = "";
            this.panel3.Parent = this;
            this.panel3.Size = new Microsoft.Xna.Framework.Point(128, 300);
            this.panel3.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.panel3.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.panel3.Visible = true;
            // 
            // panel4
            // 
            this.panel4.BackColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.panel4.BackgroundPath = "boardback";
            this.panel4.CanDrag = false;
            this.panel4.Enabled = true;
            this.panel4.Location = new Microsoft.Xna.Framework.Point(25, 51);
            this.panel4.Name = "";
            this.panel4.Parent = this;
            this.panel4.Size = new Microsoft.Xna.Framework.Point(150, 300);
            this.panel4.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.panel4.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.panel4.Visible = true;
            // 
            // victoryDefeatPanel1
            // 
            this.victoryDefeatPanel1.BackColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.victoryDefeatPanel1.BackgroundPath = null;
            this.victoryDefeatPanel1.CanDrag = false;
            this.victoryDefeatPanel1.Enabled = true;
            this.victoryDefeatPanel1.Location = new Microsoft.Xna.Framework.Point(43, 296);
            this.victoryDefeatPanel1.Name = "";
            this.victoryDefeatPanel1.Parent = this;
            this.victoryDefeatPanel1.Size = new Microsoft.Xna.Framework.Point(116, 30);
            this.victoryDefeatPanel1.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.victoryDefeatPanel1.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.victoryDefeatPanel1.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, -300);
            this.victoryDefeatPanel1.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.victoryDefeatPanel1.Visible = false;
            // 
            // marqueePanel1
            // 
            this.marqueePanel1.BackColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.marqueePanel1.BackgroundPath = null;
            this.marqueePanel1.CanDrag = false;
            this.marqueePanel1.Enabled = true;
            this.marqueePanel1.Location = new Microsoft.Xna.Framework.Point(49, 111);
            this.marqueePanel1.Name = "";
            this.marqueePanel1.Parent = this;
            this.marqueePanel1.Size = new Microsoft.Xna.Framework.Point(95, 70);
            this.marqueePanel1.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.marqueePanel1.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.marqueePanel1.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.marqueePanel1.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.marqueePanel1.Visible = false;
 
            // boardGamerTag1
            // 
            this.boardGamerTag1.BackColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.boardGamerTag1.BackgroundPath = null;
            this.boardGamerTag1.CanDrag = false;
            this.boardGamerTag1.Enabled = true;
            this.boardGamerTag1.Gamer = null;
            this.boardGamerTag1.Location = new Microsoft.Xna.Framework.Point(-1, 12);
            this.boardGamerTag1.Name = "";
            this.boardGamerTag1.Parent = this;
            this.boardGamerTag1.Size = new Microsoft.Xna.Framework.Point(164, 51);
            this.boardGamerTag1.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.boardGamerTag1.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.boardGamerTag1.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.boardGamerTag1.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.boardGamerTag1.Visible = true;
            // 
            // MiniQuarxBoard
            // 
            this.Panels.Add(this.panel4);
            this.Panels.Add(this.panel3);
            this.Panels.Add(this.panel1);
            this.Panels.Add(this.panel2);
            this.Panels.Add(this.victoryDefeatPanel1);
            this.Panels.Add(this.marqueePanel1);
            this.Panels.Add(this.boardGamerTag1);
            this.Size = new Microsoft.Xna.Framework.Point(175, 400);

        }


        //public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        //{
        //    boardViewer.Draw(gameTime);

        //    panel3.Image.Value = boardViewer.Texture;

        //    base.Draw(spriteBatch, gameTime);
        //}
    }
}
