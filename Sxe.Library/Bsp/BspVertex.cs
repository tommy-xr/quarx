using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics.CodeAnalysis;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

#region FxCop
[module: SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Scope = "member", Target = "Sxe.Library.Bsp.BspVertex.#Position")]
[module: SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Scope = "member", Target = "Sxe.Library.Bsp.BspVertex.#Normal")]
[module: SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Scope = "member", Target = "Sxe.Library.Bsp.BspVertex.#Tangent")]
[module: SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Scope = "member", Target = "Sxe.Library.Bsp.BspVertex.#LightTexCoords")]
[module: SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Scope = "member", Target = "Sxe.Library.Bsp.BspVertex.#DiffuseTexCoords")]
[module: SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Scope = "member", Target = "Sxe.Library.Bsp.BspVertex.#Binormal")]
#endregion

namespace Sxe.Library.Bsp
{
    public struct BspVertex
    {
        public Vector3 Position;
        public Vector3 Normal;
        public Vector3 Binormal;
        public Vector3 Tangent;
        public Vector2 DiffuseTexCoords;
        public Vector2 LightTexCoords;

        public BspVertex(Vector3 inPosition, Vector3 inNormal, Vector3 inBinormal, Vector3 inTan, Vector2 inDiffuseTex, Vector2 inLightTex)
        {
            Position = inPosition;
            Normal = inNormal;
            Binormal = inBinormal;
            Tangent = inTan;
            DiffuseTexCoords = inDiffuseTex;
            LightTexCoords = inLightTex;
        }

        public static bool operator ==(BspVertex leftVertex, BspVertex rightVertex)
        {
            if (leftVertex.Position == rightVertex.Position &&
                leftVertex.Normal == rightVertex.Normal &&
                leftVertex.Tangent == rightVertex.Tangent &&
                leftVertex.Binormal == rightVertex.Binormal &&
                leftVertex.DiffuseTexCoords == rightVertex.DiffuseTexCoords &&
                leftVertex.LightTexCoords == rightVertex.LightTexCoords)
                return true;

            return false;
        }

        public static bool operator !=(BspVertex leftVertex, BspVertex rightVertex)
        {
            return !(leftVertex == rightVertex);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is BspVertex))
                return false;

            //UNBOXING HERE: Hopefully this doesn't get called very much!
            BspVertex vertex = (BspVertex)obj;
            return this == vertex;
            //return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return Position.GetHashCode();
        }

        public static BspVertex Add(BspVertex leftVertex, BspVertex rightVertex)
        {
            return leftVertex + rightVertex;
        }

        public static BspVertex operator +(BspVertex leftVertex, BspVertex rightVertex)
        {
            //TODO: What should normal be when adding?
            return new BspVertex(leftVertex.Position + rightVertex.Position, leftVertex.Normal, leftVertex.Binormal,
                leftVertex.Tangent,
                leftVertex.DiffuseTexCoords + rightVertex.DiffuseTexCoords,
                leftVertex.LightTexCoords + rightVertex.LightTexCoords);
        }

        public BspVertex Multiply(float scalar)
        {
            return new BspVertex(this.Position * scalar, this.Normal, this.Binormal, this.Tangent, this.DiffuseTexCoords * scalar, this.LightTexCoords * scalar);
        }


        public static int SizeInBytes = (3 + 3 + 3 + 3 + 2 + 2) * 4;
        public static VertexElement[] VertexElements = new VertexElement[]
     {
         new VertexElement( 0, 0, VertexElementFormat.Vector3, VertexElementMethod.Default, VertexElementUsage.Position, 0 ),
         new VertexElement( 0, sizeof(float) * 3, VertexElementFormat.Vector3, VertexElementMethod.Default, VertexElementUsage.Normal, 0 ),
         new VertexElement( 0, sizeof(float) * 6, VertexElementFormat.Vector3, VertexElementMethod.Default, VertexElementUsage.Binormal, 0),
         new VertexElement( 0, sizeof(float) * 9, VertexElementFormat.Vector3, VertexElementMethod.Default, VertexElementUsage.Tangent, 0),
         new VertexElement( 0, sizeof(float) * 12, VertexElementFormat.Vector2, VertexElementMethod.Default, VertexElementUsage.TextureCoordinate, 0 ),
         new VertexElement( 0, sizeof(float) * 14, VertexElementFormat.Vector2, VertexElementMethod.Default, VertexElementUsage.TextureCoordinate, 1 ),
     };

    }
}
