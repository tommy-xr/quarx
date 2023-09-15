using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sxe.Library.Utilities
{
    public static class ColorUtilities
    {
        public static Color Lerp(Color color1, Color color2, float amount)
        {
            Vector4 vColor1 = color1.ToVector4();
            Vector4 vColor2 = color2.ToVector4();

            Vector4 outColor = Vector4.Lerp(vColor1, vColor2, amount);

            return new Color(outColor);
        }

        public static Color []  Downsample(Color[] colors, int factor)
        {
            int factorSquared =  factor * factor;
            int size = (int)Math.Sqrt(colors.Length);
            int outputSize = size / factor;
            Color[] outputArray = new Color[colors.Length / factorSquared];

            for (int x = 0; x < size / factor; x++)
            {
                for (int y = 0; y < size / factor; y++)
                {
                    Vector4 avgColor = Vector4.Zero;
                    for (int i = 0; i < factor; i++)
                    {
                        for (int j = 0; j < factor; j++)
                        {
                            avgColor += colors[(x * factor + i) + ((y * factor + j) * size)].ToVector4(); 
                        }
                    }
                    outputArray[x + y * outputSize] = new Color(avgColor / factorSquared);
                }
            }

            return outputArray;

        }

        public static Color[] BytesToColors(byte[] bytes)
        {
            Color[] output = new Color[bytes.Length / 4];

            for (int i = 0; i < bytes.Length / 4; i++)
            {
                byte r = bytes[i * 4];
                byte g = bytes[i * 4 + 1];
                byte b = bytes[i * 4 + 2];
                byte a = bytes[i * 4 + 3];

                //TODO: Why is this reversed???
                output[i] = new Color(b, g, r, a);
            }
            return output;
        }

        public static Color GetAverageColor(Color[] colors)
        {
            Vector4 totalColor = Vector4.Zero;
            for (int i = 0; i < colors.Length; i++)
            {
                totalColor += colors[i].ToVector4();
            }
            totalColor /= colors.Length;
            return new Color(totalColor);
        }

        public static Color GetAverageColor(byte[] colors)
        {
            Vector4 totalColor = Vector4.Zero;
            for (int i = 0; i < colors.Length / 4; i++)
            {
                //Don't know why we have to switch these....
                byte b = colors[i * 4];
                byte g = colors[i * 4 + 1];
                byte r = colors[i * 4 + 2];
                byte a = colors[i * 4 + 3];

                Color c = new Color(r, g, b, a);
                totalColor += c.ToVector4();
            }
            totalColor /= (colors.Length /4);
            return new Color(totalColor);
        }
    }
}
