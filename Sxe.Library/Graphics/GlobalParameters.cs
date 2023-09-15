using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sxe.Library.Graphics
{
    public class GlobalParameters
    {
        Vector3 position;
        Matrix view;
        Matrix projection;
        float time;

        public Vector3 Position
        {
            get { return position; }
            set { position = value; }
        }

        public Matrix View
        {
            get { return view; }
            set { view = value; }
        }

        public Matrix Projection
        {
            get { return projection; }
            set { projection = value; }
        }

        public float Time
        {
            get { return time; }
            set { time = value; }
        }
    }
}
