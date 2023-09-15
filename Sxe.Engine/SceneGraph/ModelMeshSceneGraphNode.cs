using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sxe.Engine.SceneGraph
{
    public class ModelMeshSceneGraphNode : SceneGraphNode
    {
        ModelMesh mesh;

        public ModelMeshSceneGraphNode(ModelMesh inMesh)
        {
            mesh = inMesh;
            this.Name = mesh.Name;
        }

        public override void Draw(Effect currentEffect, Vector3 eyePos, Matrix view, Matrix projection)
        {
            base.Draw(currentEffect, eyePos, view, projection);

            currentEffect.GraphicsDevice.Indices = mesh.IndexBuffer;

            currentEffect.Parameters["World"].SetValue(Matrix.Identity);
            currentEffect.Parameters["View"].SetValue(view);
            currentEffect.Parameters["Projection"].SetValue(projection);

            currentEffect.CommitChanges();

            foreach (ModelMeshPart part in mesh.MeshParts)
            {
                currentEffect.GraphicsDevice.Vertices[0].SetSource(mesh.VertexBuffer, part.StreamOffset, part.VertexStride);
                currentEffect.GraphicsDevice.VertexDeclaration = part.VertexDeclaration;
                currentEffect.GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, part.BaseVertex,
                    0, part.NumVertices, part.StartIndex, part.PrimitiveCount);
            }

        }
    }
}
