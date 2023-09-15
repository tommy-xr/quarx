using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;

namespace SXERadMapper
{


    public static class Utilities
    {
        /// <summary>
        /// Returns the cosine of the angle between 2 vectors
        /// </summary>
        public static float CosAngleVectors(Vector3 v1, Vector3 v2)
        {
            v1.Normalize();
            v2.Normalize();

            float val = Vector3.Dot(v1, v2);

            return val;
        }

        public static void CalcBarycentric
            (Point p, Point pA, Point pB, Point pC, out float alpha, out float beta, out float gamma)
        {
            float b1 = (float)((pA.Y - pC.Y) * p.X + (pC.X - pA.X) * p.Y +pA.X * pC.Y - pC.X * pA.Y);
            float b2 = (float)((pA.Y - pC.Y) * pB.X  + (pC.X - pA.X) * pB.Y + pA.X * pC.Y - pC.X * pA.Y);
            beta = b1 / b2;


            gamma = (float)((pA.Y - pB.Y) * p.X + (pB.X - pA.X) * p.Y + pA.X * pB.Y - pB.X * pA.Y);
            gamma /= (float)((pA.Y - pB.Y) * pC.X + (pB.X - pA.X) * pC.Y + pA.X * pB.Y - pB.X * pA.Y);

            alpha = 1.0f - gamma - beta;




        }
        public static T Min<T>(params T [] vals) where T : IComparable
        {
            T minValue = vals[0];
            for (int i = 0; i < vals.Length; i++)
            {
                if (vals[i].CompareTo(minValue) < 0)
                    minValue = vals[i];
            }

            return minValue;
        }

        public static T Max<T>(params T[] vals) where T : IComparable
        {
            T maxValue = vals[0];
            for (int i = 0; i < vals.Length; i++)
            {
                if (vals[i].CompareTo(maxValue) > 0)
                    maxValue = vals[i];
            }
            return maxValue;
        }

        /// <summary>
        /// Get the area of a triangle, from its side vectors
        /// </summary>
        public static float CalcTriangleAreaFromVecs(Vector3 BA, Vector3 CA)
        {

            Vector3 AreaVec = Vector3.Cross(BA, CA);
            float twiceArea = AreaVec.Length();
            twiceArea /= 2.0f; //have to divide by two dawg

            return twiceArea;
        }

        //Fabio Policarpo's Ray - Triangle Intersection
        // triangle intersect from http://www.graphics.cornell.edu/pubs/1997/MT97.pdf
        public static bool RayTriangleIntersect(Vector3 ray_origin, Vector3 ray_direction,
                    Vector3 vert0, Vector3 vert1, Vector3 vert2,
                    out float t, out float u, out float v)
        {
            t = 0; u = 0; v = 0;

            Vector3 edge1 = vert1 - vert0;
            Vector3 edge2 = vert2 - vert0;

            Vector3 tvec, pvec, qvec;
            float det, inv_det;

            pvec = Vector3.Cross(ray_direction, edge2);

            det = Vector3.Dot(edge1, pvec);

            if (det > -0.00001f)
                return false;

            inv_det = 1.0f / det;

            tvec = ray_origin - vert0;

            u = Vector3.Dot(tvec, pvec) * inv_det;
            if (u < -0.001f || u > 1.001f)
                return false;

            qvec = Vector3.Cross(tvec, edge1);

            v = Vector3.Dot(ray_direction, qvec) * inv_det;
            if (v < -0.001f || u + v > 1.001f)
                return false;

            t = Vector3.Dot(edge2, qvec) * inv_det;

            if (t <= 0)
                return false;

            return true;
        }
    }
}
