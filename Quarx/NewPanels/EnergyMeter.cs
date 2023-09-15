using System;
using System.Collections.Generic;
using System.Text;

using Sxe.Engine.UI;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Quarx
{
    /// <summary>
    /// This is the sweet meter on the side of the boards
    /// </summary>
    public class EnergyMeter : CompositePanel
    {
        private AnimatedImage animatedBar;
        private static string image0 = "Punish\\punishbar0";
        private static string image1 = "Punish\\punishbar1";
        private static string image2 = "Punish\\punishbar2";
        private static string image3 = "Punish\\punishbar3";
        private static string image4 = "Punish\\punishbar4";
        float energyValue = 0.5f;
        float currentEnergyValue = 0.0f;
        float increaseRate = 3f;
        float decreaseRate = 5f;
        //private BaseGameModel model;

        public bool Flipped
        {
            get;
            set;
        }

        public float Value
        {
            get { return energyValue; }
            set { energyValue = value; }
        }





        public EnergyMeter()
        {
            InitializeComponent();
        }

        public override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            base.LoadContent(content);

            animatedBar = new AnimatedImage();

            if (Flipped)
                animatedBar.SpriteEffects = SpriteEffects.FlipHorizontally;

            Animation animation = new Animation();
            animation.AddFrame(this.LoadImage(content, image0));
            animation.AddFrame(this.LoadImage(content, image1));
            animation.AddFrame(this.LoadImage(content, image2));
            animation.AddFrame(this.LoadImage(content, image3));
            animation.AddFrame(this.LoadImage(content, image4));
            animation.AddFrame(this.LoadImage(content, image3));
            animation.AddFrame(this.LoadImage(content, image2));
            animation.AddFrame(this.LoadImage(content, image1));

            animation.Play();
            animatedBar.AddAnimation("meter", animation);
        }

        public override void Paint(Microsoft.Xna.Framework.Graphics.SpriteBatch sb, Point positionOffset, Vector2 scale)
        {
            base.Paint(sb, positionOffset, scale);

            Rectangle destinationRect = this.GetDestinationRectangle(positionOffset, scale);
            //destinationRect.Y += 100;

            float startPercent = 0.25f;
            float endPercent = 0.95f;

            //this.energyValue = 8;
            float percent = (float)this.currentEnergyValue;

            float actualPercent = (endPercent - startPercent) * percent + startPercent;

            Rectangle startRectangle = this.animatedBar.CurrentAnimation.CurrentImage.SourceRectangle;
            int height = (int)(startRectangle.Height * actualPercent);
            int y = startRectangle.Height - height;
            Rectangle sourceRectangle = new Rectangle(0, y, startRectangle.Width, height);

            int destHeight = (int)(destinationRect.Height * actualPercent);
            int destY = destinationRect.Height - destHeight + destinationRect.Y;

            //animatedBar.Draw(sb, startRectangle, destinationRect, Color.White);
            animatedBar.Draw(sb, sourceRectangle, new Rectangle(destinationRect.X, destY, destinationRect.Width, destHeight), Color.White,animatedBar.SpriteEffects );
        }

        public override void Update(GameTime gameTime)
        {

            if (this.currentEnergyValue > this.energyValue)
            {
                this.currentEnergyValue -= (float)gameTime.ElapsedGameTime.TotalSeconds * this.increaseRate;
                if (this.currentEnergyValue < this.energyValue)
                    this.currentEnergyValue = this.energyValue;
            }
            else if (this.currentEnergyValue < this.energyValue)
            {
                this.currentEnergyValue += (float)gameTime.ElapsedGameTime.TotalSeconds * this.decreaseRate;
                if (this.currentEnergyValue > this.energyValue)
                    this.currentEnergyValue = this.energyValue;
            }


            this.animatedBar.Update(gameTime);
            base.Update(gameTime);
        }


        void InitializeComponent()
        {
            // 
            // EnergyMeter
            // 
            this.BackgroundPath = "Punish\\punishbar";
            this.Size = new Microsoft.Xna.Framework.Point(69, 234);

        }
    }
}
