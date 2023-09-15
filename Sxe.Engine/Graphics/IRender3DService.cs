using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sxe.Engine.Graphics
{
    /// <summary>
    /// Interface for debugging 3d renderer
    /// </summary>
    public interface IRender3DService
    {
        void RenderLine(Vector3 v1, Vector3 v2, Color col);

        void RenderSphere(Vector3 center, float radius, Color col);

        void RenderFloorGrid(Vector2 start, Vector2 end, float delta, Color col);

        Matrix View { get; set;}
        Matrix Projection { get; set; }
    }
}
