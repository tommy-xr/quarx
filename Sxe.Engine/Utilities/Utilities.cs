using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sxe.Engine.Utilities
{
    public static class Utilities
    {
        static Random random = new Random();

        public static GameTime CreateGameTime(double seconds)
        {
            TimeSpan time = TimeSpan.FromSeconds(seconds);
            return new GameTime(time, new TimeSpan(0), time, new TimeSpan(0));
        }

        public static T GetRandomFromList<T>(List<T> list)
        {
            int item = random.Next(0, list.Count);
            return list[item];
        }

        public static void GetTextParams(Rectangle rect, string text, SpriteFont font,
    Sxe.Engine.UI.HorizontalAlignment horizontalAlignment, Sxe.Engine.UI.VerticalAlignment verticalAlignment,
    out Vector2 position, out string croppedText, out float scale)
        {
            GetTextParams(rect, text, font, horizontalAlignment, verticalAlignment, out position, out croppedText, out scale, false);
        }

        public static void GetTextParams(Rectangle rect, string text, SpriteFont font,
    Sxe.Engine.UI.HorizontalAlignment horizontalAlignment, Sxe.Engine.UI.VerticalAlignment verticalAlignment,
    out Vector2 position, out string croppedText, out float scale, bool stretchToFit)
        {
            scale = 1.0f;
            position = Vector2.One;

            //First, measure the string, and see where we're at
            Vector2 measurement = font.MeasureString(text);

            if (measurement.Y > rect.Height || stretchToFit)
            {
                scale = rect.Height / measurement.Y;
            }

            //We need to scale the x too now, otherwise it'll look bogue
            measurement *= scale;

            croppedText = text;

            //Crop this text til it fits
            while (measurement.X > rect.Width)
            {
                croppedText = croppedText.Substring(0, croppedText.Length - 1);
                measurement = font.MeasureString(croppedText);
                measurement *= scale;
            }

            //Now position the text
            switch (horizontalAlignment)
            {
                case Sxe.Engine.UI.HorizontalAlignment.Left:
                    position.X = rect.X;
                    break;
                case Sxe.Engine.UI.HorizontalAlignment.Right:
                    position.X = rect.X + rect.Width - measurement.X;
                    break;
                case Sxe.Engine.UI.HorizontalAlignment.Center:
                default:
                    position.X = rect.X + rect.Width / 2 - measurement.X / 2;
                    break;
            }

            switch (verticalAlignment)
            {
                case Sxe.Engine.UI.VerticalAlignment.Bottom:
                    position.Y = rect.Y + rect.Height - measurement.Y;
                    break;
                case Sxe.Engine.UI.VerticalAlignment.Top:
                    position.Y = rect.Y;
                    break;
                case Sxe.Engine.UI.VerticalAlignment.Middle:
                default:
                    position.Y = rect.Y + rect.Height / 2 - measurement.Y / 2;
                    break;

            }
            return;
        }
    }
}
