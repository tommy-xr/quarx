using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sxe.Engine.Graphics
{
    /// <summary>
    /// Interface for an object that can render 2d objects (screen objects)
    /// </summary>
    public interface IRender2DService
    {
        void RenderText(SpriteFont font, string text, Vector2 pos, Color col, float drawOrder);
        void Render2D(Texture2D tex, Vector2 pos, Vector2 size, Color col, float drawOrder);
        void Render2D(Texture2D tex, Vector2 pos, Vector2 size, Rectangle? sourceRect, Color col, float drawOrder);

    }
}
