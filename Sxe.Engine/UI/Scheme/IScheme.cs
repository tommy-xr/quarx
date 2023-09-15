using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Sxe.Engine.UI
{
    public interface IScheme
    {
        Texture2D GetTexture(string textureName);
        UIImage GetImage(string imageName);
        SpriteFont GetFont(string fontName);
        Color GetColor(string colorName);
        Point GetPoint(string pointName);
        int GetInt(string intName);
        float GetFloat(string floatName);

        IScheme DefaultScheme { get; set; }
    }
}
