using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

using Sxe.Engine.Graphics;

namespace Sxe.Engine.Input
{
    public enum CursorState
    {
        Default = 0,
        Move = 1,
        Text = 2,
        Invalid = 3,
        Waiting = 4
    }


    public class Cursor
    {
        ICursorScheme scheme;
        Point position;
        Point size;
        bool visible;

        public bool Visible
        {
            get { return visible; }
            set { visible = value; }
        }

        public Point Position
        {
            get { return position; }
            set { position = value; }
        }

        public Point Size
        {
            get { return size; }
            set { size = value; }
        }

        public Cursor(ICursorScheme inScheme)
        {
            scheme = inScheme;
            size = scheme.Size;
        }



        public void Draw(IRender2DService render)
        {

            if (visible)
            {

                render.Render2D(scheme.DefaultCursor.Value, new Vector2(position.X, position.Y), new Vector2(size.X, size.Y),
                    Color.White, 1.0f);

                //render.Render2D(scheme.DefaultCursor.Value, new Vector2(position.X, position.Y), new Vector2(2, 2),
                //    Color.Red, 1.0f);
            }
        }

    }
}
