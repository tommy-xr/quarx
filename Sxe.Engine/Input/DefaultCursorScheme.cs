using System;
using System.Collections.Generic;
using System.Text;

using Sxe.Engine.UI;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Sxe.Engine.Input
{
    public class DefaultCursorScheme : ICursorScheme
    {
        const string defaultCursorPath = "Default/UI/cursor2";
        UIImage defaultImage;

        public UIImage DefaultCursor { get { return defaultImage; } }
        public UIImage MoveCursor { get { return defaultImage; } }
        public UIImage WaitCursor { get { return defaultImage; } }
        public UIImage TextCursor { get { return defaultImage; } }
        public UIImage InvalidCursor { get { return defaultImage; } }
        public Point Size { get { return new Point(32, 32); } }

        public DefaultCursorScheme(ContentManager manager)
        {
            defaultImage = new UIImage(manager.Load<Texture2D>(defaultCursorPath));
        }
        
    }
}
