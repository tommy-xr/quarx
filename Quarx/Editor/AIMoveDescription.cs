using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Quarx
{

    public class AIMoveDescription
    {
        BlockColor color1;
        BlockColor color2;
        Point point1;
        Point point2;

        public BlockColor Color1
        {
            get { return color1; }
            set { color1 = value; }
        }

        public BlockColor Color2
        {
            get { return color2; }
            set { color2 = value; }
        }

        public Point Point1
        {
            get { return point1; }
            set { point1 = value; }
        }

        public Point Point2
        {
            get { return point2; }
            set { point2 = value; }
        }
    }
}
