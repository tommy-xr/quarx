using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Quarx
{
    public interface IBoardViewer
    {
        Texture2D PreviewTexture { get; }

        Texture2D Texture { get; }

        BaseGameModel Model { get; set; }

        void Draw(GameTime gameTime);

        //Texture2D GetPreviewTexture(BlockColor color1, BlockColor color2, RenderTarget2D);
        
    }
}
