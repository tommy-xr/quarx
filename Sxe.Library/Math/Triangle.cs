using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;

namespace Sxe.Library
{
    public class TriangleVertex
    {
        Vector3 position;
        Vector2 textureCoordinate;

        public Vector3 Position
        {
            get { return position; }
        }

        public Vector2 TextureCoordinate
        {
            get { return textureCoordinate; }
        }

        public TriangleVertex(Vector3 pos, Vector2 texCoord)
        {
            position = pos;
            textureCoordinate = texCoord;
        }
    }

    public class Triangle
    {
        #region Fields
        TriangleVertex[] vertexes;
        #endregion

        public TriangleVertex Vertex0
        {
            get { return vertexes[0]; }
        }

        public TriangleVertex Vertex1
        {
            get { return vertexes[1]; }
        }

        public TriangleVertex Vertex2
        {
            get { return vertexes[2]; }
        }


        public Triangle(TriangleVertex v1, TriangleVertex v2, TriangleVertex v3)
        {
            vertexes = new TriangleVertex[3];
            vertexes[0] = v1;
            vertexes[1] = v2;
            vertexes[2] = v3;
        }

        public Triangle(Vector3 v1, Vector2 texCoord1, 
            Vector3 v2, Vector2 texCoord2, Vector3 v3, Vector2 texCoord3)
            : this(new TriangleVertex(v1, texCoord1), new TriangleVertex(v2, texCoord2),
            new TriangleVertex(v3, texCoord3))
        
        {

        }
        
    }
}