using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Sxe.Engine.UI;


namespace Sxe.Engine.Input
{
    /// <summary>
    /// This scheme is used to define a set of images to use as the cursor
    /// </summary>
    public interface ICursorScheme
    {
        UIImage DefaultCursor { get; }
        UIImage MoveCursor { get; }
        UIImage WaitCursor { get; }
        UIImage TextCursor { get; }
        UIImage InvalidCursor { get; }

        Point Size { get; }
    }
}
