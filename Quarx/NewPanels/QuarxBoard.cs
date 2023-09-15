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
    public class QuarxBoard : CompositePanel
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
        private FlashingStart flashingStart1;
        //private PunishPanel punishPanel1;

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
                    if (model.Gamer != null)
                        boardGamerTag1.Gamer = model.Gamer;
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

        public QuarxBoard()
        {
            InitializeComponent();
        }

        public void Reset()
        {
            this.victoryDefeatPanel1.Visible = false;
            this.marqueePanel1.Visible = false;
            this.flashingStart1.Visible = false;
            //this.punishPanel1.Visible = false;
        }

        public override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            base.LoadContent(content);

            IGraphicsDeviceService graphics = content.ServiceProvider.GetService(typeof(IGraphicsDeviceService)) as IGraphicsDeviceService;


            boardViewer = new QuarxBoardViewer3D(graphics.GraphicsDevice, content, BoardWidth, BoardHeight);
        }

        public void ShowFlashingStart()
        {
            this.flashingStart1.Show();
        }



        public override void Update(GameTime gameTime)
        {
            //First, check if our model has a marquee
            if (model.Marquee != QuarxMarqueeType.None)
            {
                this.Marquee.ShowMarquee(model.Marquee);
                model.Marquee = QuarxMarqueeType.None;
            }
            else if (lastState != model.State)
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
                else if (model.State == QuarxGameState.Playing) 
                {
                    if(victoryDefeatPanel1.Visible)
                        victoryDefeatPanel1.Hide();

                    if (flashingStart1.Visible)
                        flashingStart1.Hide();
                }

                lastState = model.State;
            }

            if (  (this.marqueePanel1.Visible || model.ShowStart) && !flashingStart1.Visible && model.State != QuarxGameState.Playing)
            {
                flashingStart1.Show();
                model.ShowStart = false;
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
            this.flashingStart1 = new Quarx.FlashingStart();
            // 
            // panel1
            // 
            this.panel1.BackgroundPath = "bottom1";
            this.panel1.CanDrag = false;
            this.panel1.Location = new Microsoft.Xna.Framework.Point(21, 537);
            this.panel1.Parent = this;
            this.panel1.Size = new Microsoft.Xna.Framework.Point(240, 27);
            this.panel1.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.panel1.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            // 
            // panel2
            // 
            this.panel2.BackgroundPath = "top1";
            this.panel2.CanDrag = false;
            this.panel2.Location = new Microsoft.Xna.Framework.Point(22, 44);
            this.panel2.Parent = this;
            this.panel2.Size = new Microsoft.Xna.Framework.Point(240, 27);
            this.panel2.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.panel2.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            // 
            // panel3
            // 
            this.panel3.BackgroundPath = "background";
            this.panel3.CanDrag = false;
            this.panel3.Location = new Microsoft.Xna.Framework.Point(34, 68);
            this.panel3.Parent = this;
            this.panel3.Size = new Microsoft.Xna.Framework.Point(216, 466);
            this.panel3.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.panel3.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            // 
            // panel4
            // 
            this.panel4.BackgroundPath = "boardback";
            this.panel4.CanDrag = false;
            this.panel4.Location = new Microsoft.Xna.Framework.Point(17, 56);
            this.panel4.Parent = this;
            this.panel4.Size = new Microsoft.Xna.Framework.Point(250, 500);
            this.panel4.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.panel4.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            // 
            // victoryDefeatPanel1
            // 
            this.victoryDefeatPanel1.BackgroundPath = null;
            this.victoryDefeatPanel1.CanDrag = false;
            this.victoryDefeatPanel1.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowSignedIn;
            this.victoryDefeatPanel1.Location = new Microsoft.Xna.Framework.Point(35, 480);
            this.victoryDefeatPanel1.Parent = this;
            this.victoryDefeatPanel1.Size = new Microsoft.Xna.Framework.Point(220, 50);
            this.victoryDefeatPanel1.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, -150);
            this.victoryDefeatPanel1.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.victoryDefeatPanel1.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, -300);
            this.victoryDefeatPanel1.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.victoryDefeatPanel1.Visible = false;
            // 
            // marqueePanel1
            // 
            this.marqueePanel1.BackgroundPath = null;
            this.marqueePanel1.CanDrag = false;
            this.marqueePanel1.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowSignedIn;
            this.marqueePanel1.Location = new Microsoft.Xna.Framework.Point(47, 219);
            this.marqueePanel1.Parent = this;
            this.marqueePanel1.Size = new Microsoft.Xna.Framework.Point(190, 111);
            this.marqueePanel1.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.marqueePanel1.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.marqueePanel1.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.marqueePanel1.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.marqueePanel1.Visible = false;
            // 
            // boardGamerTag1
            // 
            this.boardGamerTag1.BackgroundPath = null;
            this.boardGamerTag1.CanDrag = false;
            this.boardGamerTag1.Gamer = null;
            this.boardGamerTag1.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowSignedIn;
            this.boardGamerTag1.Location = new Microsoft.Xna.Framework.Point(0, 12);
            this.boardGamerTag1.Parent = this;
            this.boardGamerTag1.Size = new Microsoft.Xna.Framework.Point(164, 51);
            this.boardGamerTag1.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.boardGamerTag1.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.boardGamerTag1.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.boardGamerTag1.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            // 
            // flashingStart1
            // 
            this.flashingStart1.BackgroundPath = null;
            this.flashingStart1.CanDrag = false;
            this.flashingStart1.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowSignedIn;
            this.flashingStart1.Location = new Microsoft.Xna.Framework.Point(36, 338);
            this.flashingStart1.Parent = this;
            this.flashingStart1.Size = new Microsoft.Xna.Framework.Point(211, 25);
            this.flashingStart1.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.flashingStart1.TransitionOffTime = System.TimeSpan.Parse("00:00:00.0000000");
            this.flashingStart1.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.flashingStart1.TransitionOnTime = System.TimeSpan.Parse("00:00:05.0000000");
            // 
            // QuarxBoard
            // 
            this.Panels.Add(this.boardGamerTag1);
            this.Panels.Add(this.panel4);
            this.Panels.Add(this.panel3);
            this.Panels.Add(this.panel1);
            this.Panels.Add(this.panel2);
            this.Panels.Add(this.victoryDefeatPanel1);
            this.Panels.Add(this.marqueePanel1);
            this.Panels.Add(this.flashingStart1);
            this.Size = new Microsoft.Xna.Framework.Point(300, 600);

        }

            //        this.Panels.Add(this.panel5);
            //this.Panels.Add(this.panel4);
            //this.Panels.Add(this.panel3);
            //this.Panels.Add(this.panel1);
            //this.Panels.Add(this.panel2);
            //this.Panels.Add(this.victoryDefeatPanel1);
            //this.Panels.Add(this.marqueePanel1);

        //public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        //{
        //    boardViewer.Draw(gameTime);

        //    panel3.Image.Value = boardViewer.Texture;

        //    base.Draw(spriteBatch, gameTime);
        //}
    }
}
