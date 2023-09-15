using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sxe.Engine.UI
{
    /// <summary>
    /// Oscillating Image bounces the size of the image to give a groovy effect
    /// </summary>
    public class OscillatingImage : UIImage
    {

        public OscillatingImage(Microsoft.Xna.Framework.Graphics.Texture2D texture)
            : base(texture)
        {
        }

        double scaleTime;
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            scaleTime = gameTime.TotalGameTime.TotalSeconds;
            base.Update(gameTime);

        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch batch, Rectangle rectangle, Microsoft.Xna.Framework.Rectangle destination, Microsoft.Xna.Framework.Color inColor, SpriteEffects spriteEffects)
        {
            float frequency = 6;
            
            float pulse = (float)Math.Sin(scaleTime * frequency) + 1;
            float scale = 1 + pulse * 0.05f;

            int newWidth = (int)(destination.Width * scale);
            int newHeight = (int)(destination.Height * scale);

            destination.X -= (newWidth - destination.Width) / 2;
            destination.Y -= (newHeight - destination.Height) / 2;

            destination.Width = newWidth;
            destination.Height = newHeight;

            base.Draw(batch, rectangle, destination, inColor, spriteEffects);
        }
    }
}
