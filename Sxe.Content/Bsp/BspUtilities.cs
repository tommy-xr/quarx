using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;

namespace Sxe.Content.Bsp
{
    public static class BspUtilities
    {
        /// <summary>
        /// Special function read Quake 3 vectors. Their values are not the same as XNA coordinate system.
        /// </summary>
        /// <param name="br"></param>
        /// <returns></returns>
        public static Vector3 ReadQuake3Vector(BinaryReader reader)
        {
            float x = reader.ReadSingle();
            float z = -reader.ReadSingle();
            float y = reader.ReadSingle();

            Vector3 retVector = new Vector3(x, y, z);
            return retVector;
        }
    }
}
