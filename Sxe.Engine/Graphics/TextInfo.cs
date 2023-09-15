using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sxe.Engine.Graphics
{
    /// <summary>
    /// Class used to hold all necessary data for rendering text
    /// Used internally in the Render namespace
    /// </summary>
    struct TextInfo
    {
         SpriteFont font;
         string text;
         Vector2 pos;
         Vector2 scale;
         Color color;
         float drawOrder;
        SpritePositionMode mode;

        public TextInfo(SpriteFont setFont, string setText, Vector2 setPos, Vector2 setScale, Color setColor, float inDrawOrder, SpritePositionMode inMode)
        {
            this.font = setFont;
            this.text = setText;
            this.pos = setPos;
            this.color = setColor;
            this.drawOrder = inDrawOrder;
            this.scale = setScale;
            this.mode = inMode;
        }

        public void Draw(SpriteBatch batch, Vector2 view)
        {
            int x = (int)pos.X;
            int y = (int)pos.Y;

            if (mode == SpritePositionMode.RelativeCoordinates)
            {
                x = (int)(x * view.X);
                y = (int)(y * view.Y);
            }

            Vector2 textPos = new Vector2(x, y);

            batch.DrawString(font, text, textPos, color, 0.0f, Vector2.Zero,scale, SpriteEffects.None, drawOrder);
        }

        
    }
}
