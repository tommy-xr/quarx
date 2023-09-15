using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;

using Sxe.Library.Bsp;

namespace Sxe.Content.Bsp
{
    /// <summary>
    /// Stores information about a bsp
    /// </summary>
    public class BspNodeContent : NodeContent
    {
        public BspDom Bsp
        {
            get { return bsp; }
            set { bsp = value; }
        }

        BspDom bsp;

        public BspNodeContent()
        {
        }

        public BspNodeContent(BspDom inBsp)
        {
            bsp = inBsp;
        }

    }
}
