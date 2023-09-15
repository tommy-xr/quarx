using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sxe.Engine.Graphics
{
    /// <summary>
    /// SpritePositionMode describes the position mode of drawing the sprite
    /// If relative coordinates are chosen, the position on screen is represented between 0f and 1f
    /// If absolute coordinates are chosen, the position is the exact pixel
    /// </summary>
    public enum SpritePositionMode
    {
        RelativeCoordinates = 0,
        AbsoluteCoordinates
    }

    /// <summary>
    /// Struct used to hold all necessary data for rendering sprites
    /// Used internally in the Render namespace
    /// </summary>
    struct SpriteInfo
    {
         Texture2D texture;
         Vector2 pos;
         Vector2 size;
         Color color;
         float drawOrder;
         Rectangle? sourceRect;
        SpritePositionMode mode;

        public SpriteInfo(Texture2D inTexture, Vector2 setPos, Vector2 setSize, Color inColor, float inDrawOrder, Rectangle? source, SpritePositionMode mode)
        {
            this.texture = inTexture;
            this.pos = setPos;
            this.size = setSize;
            this.color = inColor;
            this.drawOrder = inDrawOrder;
            this.sourceRect = source;
            this.mode = mode;

        }

        public void Draw(SpriteBatch batch, Vector2 view)
        {

            int x = (int)pos.X;
            int y = (int)pos.Y;
            int width = (int)size.X;
            int height = (int)size.Y;

            if (mode == SpritePositionMode.RelativeCoordinates)
            {
                x = (int)(x * view.X);
                y = (int)(y * view.Y);
                width = (int)(width * view.X);
                height = (int)(height * view.Y);
            }
            Rectangle renderRect = new Rectangle(x, y, width, height);

            batch.Draw(texture, renderRect, sourceRect, color, 0.0f, Vector2.Zero, SpriteEffects.None, drawOrder);
        }



    }
}
