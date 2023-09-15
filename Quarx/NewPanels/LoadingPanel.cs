using System;
using System.Collections.Generic;
using System.Text;

using Sxe.Engine.UI;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Quarx.NewPanels
{
    public class LoadingPanel : CompositePanel
    {
        UIImage barImage;
        private static string barPath = "Loading\\loadingbarfill";

        float loadPercentage = 0f;
        public float LoadPercentage
        {
            get {return loadPercentage; }
            set { this.loadPercentage = value; }
        }

        public LoadingPanel()
        {
            InitializeComponent();
        }

        public override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            base.LoadContent(content);
            barImage = this.LoadImage(content, barPath);
        }

        public override void Paint(SpriteBatch sb, Point positionOffset, Vector2 scale)
        {
            base.Paint(sb, positionOffset, scale);

            if (barImage != null)
            {
                Rectangle destinationRectangle = this.GetDestinationRectangle(positionOffset, scale);
                int actualWidth = (int)(destinationRectangle.Width * LoadPercentage);

                Rectangle actualDestination = new Rectangle(destinationRectangle.X, destinationRectangle.Y, actualWidth, destinationRectangle.Height);

                Rectangle source = barImage.SourceRectangle;
                Rectangle sourceRectangle = new Rectangle(source.X, source.Y, (int)(source.Width * loadPercentage), source.Height);

                barImage.Draw(sb, sourceRectangle, actualDestination, Color.White, SpriteEffects.None);
            }

        }




        void InitializeComponent()
        {
            // 
            // LoadingPanel
            // 
            this.BackgroundPath = "Loading\\loadingbar";
            this.Size = new Microsoft.Xna.Framework.Point(268, 88);

        }
    }
}
