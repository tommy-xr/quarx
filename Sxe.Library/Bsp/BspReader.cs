using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Sxe.Library.Bsp
{
    /// <summary>
    /// This class will be instantiated by the XNA Framework Content
    /// Pipeline to read the specified data type from binary .xnb format.
    /// 
    /// Unlike the other Content Pipeline support classes, this should
    /// be a part of your main game project, and not the Content Pipeline
    /// Extension Library project.
    /// </summary>
    public class BspReader : ContentTypeReader<BspLevel>
    {
        protected override BspLevel Read(ContentReader input, BspLevel existingInstance)
        {
            BspLevel b = new BspLevel(input, input.ContentManager);
            
            return b;
        }
    }

    public class BspTagReader : ContentTypeReader<BspTagData>
    {
        protected override BspTagData Read(ContentReader input, BspTagData existingInstance)
        {
            Dictionary<string, int> dictionary = input.ReadObject<Dictionary<string, int>>();

            int numLightmaps = input.ReadInt32();
            BspLightMap[] lightMaps = new BspLightMap[numLightmaps];
            for (int i = 0; i < numLightmaps; i++)
            {
                lightMaps[i] = new BspLightMap();

                Color[] data = input.ReadObject<Color[]>();

                lightMaps[i].lightMapSize = (int)Math.Sqrt(data.Length);
                lightMaps[i].map = data;
            }

            return new BspTagData(lightMaps, dictionary);
        }
    }
}
