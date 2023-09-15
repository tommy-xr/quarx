using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sxe.Library.Materials
{
    public class MaterialManager
    {
        Texture2D defaultDiffuse;
        Texture2D defaultNormal;
        Texture2D defaultGloss;
        Effect defaultShader;

        public Texture2D DefaultDiffuse
        {
            get { return defaultDiffuse; }
            set { defaultDiffuse = value; }
        }

        public Texture2D DefaultNormal
        {
            get { return defaultNormal; }
            set { defaultNormal = value; }
        }

        public Texture2D DefaultSpecular
        {
            get { return defaultGloss; }
            set { defaultGloss = value; }
        }

        public Effect DefaultEffect
        {
            get { return defaultShader; }
            set { defaultShader = value; }
        }
    }
}
