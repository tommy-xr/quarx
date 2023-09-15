using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WinFormsContentLoading
{
    public class AnarchyShader
    {
        string contentPath;
        public string ContentPath
        {
            get { return contentPath; }
            set { contentPath = value; }
        }

        Effect effect;
        public Effect Effect
        {
            get { return effect; }
            set { effect = value; }
        }
    }

    public class AnarchyShaderCollection : Collection<AnarchyShader>
    {
    }
}
