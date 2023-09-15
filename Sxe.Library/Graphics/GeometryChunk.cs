using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Sxe.Library.Graphics
{
    /// <summary>
    /// A geometry chunk built from a model mesh
    /// </summary>
    public class ModelMeshGeometryChunk : IGeometryChunk
    {
        VertexBuffer vertexBuffer;
        IndexBuffer indexBuffer;

        List<ModelMeshPart> parts = new List<ModelMeshPart>();

        public ModelMeshGeometryChunk(ModelMesh mesh)
        {
            vertexBuffer = mesh.VertexBuffer;
            indexBuffer = mesh.IndexBuffer;

            for (int i = 0; i < mesh.MeshParts.Count; i++)
                parts.Add(mesh.MeshParts[i]);
        }

        public void Draw(GraphicsDevice device)
        {
            device.Indices = indexBuffer;
            for (int i = 0; i < parts.Count; i++)
            {
                ModelMeshPart part = parts[i];
                device.Vertices[0].SetSource(vertexBuffer, part.StreamOffset, part.VertexStride);
                device.VertexDeclaration = part.VertexDeclaration;
                device.DrawIndexedPrimitives(PrimitiveType.TriangleList, part.BaseVertex, 0, part.NumVertices, part.StartIndex, part.PrimitiveCount);
            }
        }
    }
}
