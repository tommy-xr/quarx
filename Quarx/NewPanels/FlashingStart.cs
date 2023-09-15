using System;
using System.Collections.Generic;
using System.Text;

using Sxe.Engine.UI;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Quarx
{
    public class FlashingStart : CompositePanel
    {
        SpriteFont font;
        string text;
        float scale;

        public override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            font = content.Load<SpriteFont>("Neuropol");
            text = "Press Start";

            base.LoadContent(content);
        }

        public override void Update(GameTime gameTime)
        {
            scale = (float)(Math.Sin(gameTime.TotalGameTime.TotalSeconds * 8f)+1) / 5f + 0.6f;

            base.Update(gameTime);
        }

        public override void Show()
        {
            base.Show();
        }

        public override void Paint(SpriteBatch sb, Microsoft.Xna.Framework.Point positionOffset, Microsoft.Xna.Framework.Vector2 scale)
        {
            if (Visible)
            {
                Rectangle destRectangle = this.GetDestinationRectangle(positionOffset, scale);

                Vector2 position;
                string croppedText;
                float textScale = 0f;

                float desiredWidth = destRectangle.Width * this.scale;
                float desiredHeight = destRectangle.Height * this.scale;

                int hAmount = ((int)(desiredWidth) - destRectangle.Width) / 2;
                int vAmount = ((int)(desiredHeight) - destRectangle.Height) / 2;

                destRectangle.Inflate(hAmount, vAmount);

                Sxe.Engine.Utilities.Utilities.GetTextParams(destRectangle, text, font,
                    HorizontalAlignment.Center, VerticalAlignment.Middle, out position, out croppedText, out textScale, true);

                float alphaAmount = (float)Math.Pow(1f - TransitionPosition, 3.0);

                Color textColor = new Color(255, 255, 255, (byte)(255 * alphaAmount));

                sb.DrawString(font, text, position, textColor, 0.0f, Vector2.One, textScale, SpriteEffects.None, 1.0f);

                //float width = font.MeasureString(text).X;

                //float mid = positionOffset.X + this.Location.X + this.Size.X / 2;
                //mid -= (width / 2f);

                //sb.DrawString(font, text, new Vector2(mid, positionOffset.Y + Location.Y), Color.White,
                //    0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 1.0f);
            }

            base.Paint(sb, positionOffset, scale);
        }


    }
}
