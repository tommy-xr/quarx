using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;

namespace Sxe.Library.Utilities
{
    public static class MeshUtilities
    {
        public static void CalculateTBN(
            Vector3 point1, Vector3 point2, Vector3 point3,
            Vector2 uv1, Vector2 uv2, Vector2 uv3,
            Vector3 normal, out Vector3 tangent, out Vector3 binormal)
        {
            float v1, v2, v3;
            v1 = uv1.Y;
            v2 = uv2.Y;
            v3 = uv3.Y;

            float u1, u2, u3;
            u1 = uv1.X;
            u2 = uv2.X;
            u3 = uv3.X;



            tangent = Vector3.Divide((v3 - v1) * (point2 - point1) - (v2 - v1) * (point3 - point1),
                (u2 - u1) * (v3 - v1) - (v2 - v1) * (u3 - u1));
            binormal = Vector3.Divide((u3 - u1) * (point2 - point1) - (u2 - u1) * (point3 - point1),
                (v2 - v1) * (u3 - u1) - (u2 - u1) * (v3 - v1));
            tangent.Normalize();
            binormal.Normalize();

            //Now, lets orthogonalize
            tangent = tangent - (normal * Vector3.Dot(normal, tangent));
            tangent.Normalize();

            bool isRightHanded = Vector3.Dot(Vector3.Cross(tangent, binormal), normal) >= 0;
            binormal = Vector3.Cross(normal, tangent);
            if (!isRightHanded)
                binormal = -binormal;

            return;

        }
    }
}
