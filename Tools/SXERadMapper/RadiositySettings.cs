using System;
using System.Collections.Generic;
using System.Text;

namespace SXERadMapper
{
    public class RadiositySettings
    {
        public const int BASE_SIZE = 128;

        public int PatchMapSize = 256; //Size of patch map
        public float LuminanceError = 1.0f; //the error we can terminate on 
    }
}
