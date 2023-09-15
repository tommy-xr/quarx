using System;
using System.Collections.Generic;
using System.Text;

namespace Sxe.Library.Bsp
{
    public class BspTagData
    {
        public Dictionary<string, int> LightDictionary
        {
            get { return lightmapDictionary; }
        }

        public BspLightMap [] LightMaps
        {
            get { return lightmaps; }
        }

        BspLightMap [] lightmaps;
        Dictionary<string, int> lightmapDictionary;

        public BspTagData(BspLightMap [] inLightMaps, Dictionary<string, int> inLightDictionary)

        {
            lightmaps = inLightMaps;
            lightmapDictionary = inLightDictionary;
        }
    }
}
