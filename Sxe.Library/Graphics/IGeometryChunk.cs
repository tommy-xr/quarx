using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework.Graphics;

namespace Sxe.Library.Graphics
{
    /// <summary>
    /// Provides the interface for a chunk of geometry that
    /// can be rendered
    /// </summary>
    public interface IGeometryChunk
    {
       void Draw(GraphicsDevice device);
    }
}
