using System;
using System.Collections.Generic;
using System.Text;

using Sxe.Engine.UI;

namespace Quarx
{
    public enum QuarxMarqueeType
    {
        None = 0,
        First,
        Second,
        Third,
        Fourth,
        Advance,
        GameOver

    }

    public class MarqueePanel : CompositePanel
    {

        UIImage advanceImage;
        UIImage gameOverImage;
        UIImage firstImage;
        UIImage secondImage;
        UIImage thirdImage;
        UIImage fourthImage;
    
        public MarqueePanel()
        {
            InitializeComponent();
        }

        public void ShowMarquee(QuarxMarqueeType type)
        {
            switch (type)
            {
                case QuarxMarqueeType.Advance:
                    this.Image = advanceImage;
                    break;
                case QuarxMarqueeType.GameOver:
                    this.Image = gameOverImage;
                    break;
                case QuarxMarqueeType.First:
                    this.Image = firstImage;
                    break;
                case QuarxMarqueeType.Second:
                    this.Image = secondImage;
                    break;
                case QuarxMarqueeType.Third:
                    this.Image = thirdImage;
                    break;
                case QuarxMarqueeType.Fourth:
                    this.Image = fourthImage;
                    break;
            }
            this.Show();
        }



        void InitializeComponent()
        {
            // 
            // VictoryDefeatPanel
            // 
            this.Size = new Microsoft.Xna.Framework.Point(220, 50);

        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            this.BackColor = new Microsoft.Xna.Framework.Graphics.Color(
                this.BackColor.R, this.BackColor.G, this.BackColor.B, this.TransitionAlpha);
            base.Update(gameTime);
        }

        public override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            base.LoadContent(content);
            advanceImage = LoadImage(content, "stageclear");
            gameOverImage = LoadImage(content, "gameover");

            firstImage = LoadImage(content, "1st");
            secondImage = LoadImage(content, "2nd");
            thirdImage = LoadImage(content, "3rd");
            fourthImage = LoadImage(content, "4th");

            //this.Image = victoryImage;
        }
    }
}
