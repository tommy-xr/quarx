using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;

namespace Sxe.Library
{
    public static class MathUtilities
    {

        public static void CalculateBarycentric
(Point p, Point pA, Point pB, Point pC, out float alpha, out float beta, out float gamma)
        {
            float b1 = (float)((pA.Y - pC.Y) * p.X + (pC.X - pA.X) * p.Y + pA.X * pC.Y - pC.X * pA.Y);
            float b2 = (float)((pA.Y - pC.Y) * pB.X + (pC.X - pA.X) * pB.Y + pA.X * pC.Y - pC.X * pA.Y);
            beta = b1 / b2;


            gamma = (float)((pA.Y - pB.Y) * p.X + (pB.X - pA.X) * p.Y + pA.X * pB.Y - pB.X * pA.Y);
            gamma /= (float)((pA.Y - pB.Y) * pC.X + (pB.X - pA.X) * pC.Y + pA.X * pB.Y - pB.X * pA.Y);

            alpha = 1.0f - gamma - beta;




        }

#if !XBOX
        public static int GetNextPowerOfTwo(int number)
        {
            double num = Math.Log((double)number, 2.0);
            num = Math.Ceiling(num);
            return (int)Math.Pow(2.0, num);
        }
#endif

        public static bool IsPowerOfTwo(int number)
        {
            throw new Exception("Not implemented yet.");
        }

        public static T Max<T>(params T[] values) where T : IComparable<T>
        {
            T maxVal = values[0];

            for (int i = 0; i < values.Length; i++)
            {
                if (maxVal.CompareTo(values[i]) < 0)
                {
                    maxVal = values[i];
                }
            }
            return maxVal;
        }

        public static T Min<T>(params T[] values) where T : IComparable<T>
        {
            T minVal = values[0];

            for (int i = 0; i < values.Length; i++)
            {
                if (minVal.CompareTo(values[i]) > 0)
                {
                    minVal = values[i];
                }
            }

            return minVal;
        }
    }
}
