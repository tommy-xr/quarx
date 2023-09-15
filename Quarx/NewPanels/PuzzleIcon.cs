using System;
using System.Collections.Generic;
using System.Text;

using Sxe.Engine.UI;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Quarx
{
    public class PuzzleIcon : CompositePanel, IMenuEntry
    {
        private Label label1;
        private PuzzleDescription puzzleDescription;
        private UIImage piece;
        private UIImage puzzleIcon;
        private UIImage puzzleIconClosed;
        private Sxe.Engine.UI.Buttons.ColorButton colorButton1;
        float alpha;
        bool over = false;

        public PuzzleDescription PuzzleDescription
        {
            get { return puzzleDescription; }
            set 
            { 
                puzzleDescription = value;
                //if(puzzleDescription.Icon != null)
                //this.panel1.Image = new UIImage(puzzleDescription.Icon);

                this.colorButton1.Text = this.puzzleDescription.LevelName;
                //this.label1.Caption = puzzleDescription.LevelName;
            }
        }


        public override void Paint(SpriteBatch sb, Microsoft.Xna.Framework.Point positionOffset, Microsoft.Xna.Framework.Vector2 scale)
        {
            base.Paint(sb, positionOffset, scale);

            Rectangle destRectangle = this.GetDestinationRectangle(positionOffset, scale);

            int xAmount = 20;
            int yAmount = 10;
            destRectangle.X += xAmount;
            destRectangle.Y += yAmount;
            destRectangle.Width -= xAmount* 2;
            destRectangle.Height -= yAmount * 2;

            float deltaX = (float)destRectangle.Width / (float)QuarxConstants.BoardWidth;
            float deltaY = (float)destRectangle.Height / (float)QuarxConstants.BoardHeight;



            if (this.puzzleDescription != null && this.Enabled)
            {
                for (int x = 0; x < QuarxConstants.BoardWidth; x++)
                {
                    for (int y = 0; y < QuarxConstants.BoardHeight; y++)
                    {
                        int actualY = QuarxConstants.BoardHeight - y - 1;
                        //byte colAlpha = (byte)(255 * alpha);

                        byte alpha = 255;
                        //int actualY = y;
                        if (this.puzzleDescription.GetTile(x, y).IsBlock)
                        {
                           
                            byte filler = 0;
                            Tile tile = this.puzzleDescription.GetTile(x, y);
                            Color color = new Color(filler, filler, 255);

                          
                            if (tile.Color == BlockColor.Red)
                                color = new Color(255, filler, filler);
                            else if (tile.Color == BlockColor.Yellow)
                                color = new Color(255, 255, 0);


                            if (Enabled && !over)
                                color = new Color(150, 150, 150, 150);
                            else if(!Enabled)
                                color = new Color(100, 100, 100, 50);
                            //Color whiteColor = new Color(255, 255, 255, this.colorButton1.BackColor.A);

                            this.piece.Draw(sb, new Rectangle(
                                destRectangle.X + (int)(deltaX * x),
                                destRectangle.Y + (int)(deltaY * actualY), (int)deltaX, (int)deltaY), color);
                        }
 

                    }
                }
            }


        }

        public override void Update(GameTime gameTime)
        {
            if (this.Enabled)
            {
                this.colorButton1.Visible = true;
                this.colorButton1.DefaultColor = Color.Gray;
                this.Image = puzzleIcon;
            }
            else
            {
                this.colorButton1.DefaultColor = new Color(50, 50, 50, 100);
                this.colorButton1.Visible = false;
                this.Image = puzzleIconClosed;
            }



            base.Update(gameTime);
        }

        bool IMenuEntry.AllowPlayerIndex(int playerIndex)
        {
            return true;
        }

        void IButton.PerformClick(int playerIndex)
        {
            
        }

        public void Over(int index)
        {
            this.over = true;
            this.BackColor = Color.White;
            this.colorButton1.Over(index);
        }

        public void Leave(int index)
        {
            this.over = false;
            this.BackColor = Color.Gray;
            this.colorButton1.Leave(index);
        }
    
        public PuzzleIcon()
        {
            InitializeComponent();
        }

        public override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            base.LoadContent(content);

            this.piece = this.LoadImage(content, "Puzzle\\circle");
            this.puzzleIcon = this.LoadImage(content, "Puzzle\\puzzlebox");
            this.puzzleIconClosed = this.LoadImage(content, "Puzzle\\puzzleboxclosed");
        }

        void InitializeComponent()
        {
            this.label1 = new Sxe.Engine.UI.Label();
            this.colorButton1 = new Sxe.Engine.UI.Buttons.ColorButton();
            // 
            // label1
            // 
            this.label1.BackgroundPath = null;
            this.label1.CanDrag = false;
            this.label1.Caption = "";
            this.label1.FontColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.label1.FontPath = "Neuropol";
            this.label1.HorizontalAlignment = Sxe.Engine.UI.HorizontalAlignment.Center;
            this.label1.Location = new Microsoft.Xna.Framework.Point(0, 40);
            this.label1.Parent = this;
            this.label1.Selection = new Microsoft.Xna.Framework.Point(-1, -1);
            this.label1.Size = new Microsoft.Xna.Framework.Point(100, 17);
            this.label1.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.label1.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.label1.VerticalAlignment = Sxe.Engine.UI.VerticalAlignment.Top;
            // 
            // colorButton1
            // 
            this.colorButton1.BackColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(50)), ((byte)(50)), ((byte)(50)), ((byte)(50)));
            this.colorButton1.BackgroundPath = "horizontal_gradient";
            this.colorButton1.CanDrag = false;
            this.colorButton1.ClickColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(0)), ((byte)(255)), ((byte)(0)), ((byte)(255)));
            this.colorButton1.ColorTransitionTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.colorButton1.DefaultColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(50)), ((byte)(50)), ((byte)(50)), ((byte)(50)));
            this.colorButton1.FontColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.colorButton1.FontPath = "Fonts\\Calibri14";
            this.colorButton1.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowSignedIn;
            this.colorButton1.Location = new Microsoft.Xna.Framework.Point(0, 21);
            this.colorButton1.OverColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(0)), ((byte)(200)), ((byte)(0)), ((byte)(100)));
            this.colorButton1.OverCue = null;
            this.colorButton1.Parent = this;
            this.colorButton1.PressCue = null;
            this.colorButton1.Size = new Microsoft.Xna.Framework.Point(100, 26);
            this.colorButton1.Text = "puz";
            this.colorButton1.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.colorButton1.TransitionOffTime = System.TimeSpan.Parse("00:00:02");
            this.colorButton1.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.colorButton1.TransitionOnTime = System.TimeSpan.Parse("00:00:02");
            // 
            // PuzzleIcon
            // 
            this.BackgroundPath = "Puzzle\\puzzlebox";
            this.Panels.Add(this.label1);
            this.Panels.Add(this.colorButton1);
            this.Size = new Microsoft.Xna.Framework.Point(100, 100);

        }
    }
}
