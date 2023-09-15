using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace AnarchyEditor
{
    /// <summary>
    /// Class that contains assets used by the editor
    /// </summary>
    public class EditorAssets
    {
        Effect pickingEffect;
        public Effect PickingEffect { get { return pickingEffect; } }

        public EditorAssets(ContentManager content)
        {
            pickingEffect = content.Load<Effect>("Content\\PickingEffect");
        }

    }
}
