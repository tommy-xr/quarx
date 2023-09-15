using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

#if !XBOX
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;
#endif

namespace Sxe.Library.Bsp
{
    /// <summary>
    /// Face groups are groups of faces that share the same texture and lightmap and should be rendered together
    /// These are like MeshParts in models
    /// </summary>
    public class BspFaceGroup
    {
        public int texIndex = -1;
        public int lmIndex = -1;
        public int model = -1; //model that this facegroup belongs to


        public int startOffset; //start offileet into index buffer
        public short numberOfIndexes; //number of indices
        public int startVertex = -1; //starting vertex in the verex buffer

        public int numberOfFaces; //number of faces in this face list
        public int index; //index of this facegroup

        //Min and max vectors
        public Vector3 min;
        public Vector3 max;


#if !XBOX
        public void Write(ContentWriter writer)
        {
            writer.Write((int)texIndex);
            writer.Write((int)lmIndex);
            writer.Write((int)model);

            writer.Write((int)startOffset);
            writer.Write((short)numberOfIndexes);
            writer.Write((int)startVertex);

            writer.Write((int)numberOfFaces);
            writer.Write((int)index);

            writer.Write((Vector3)min);
            writer.Write((Vector3)max);
        }
#endif

        public void Read(ContentReader cr)
        {
            texIndex = cr.ReadInt32();
            lmIndex = cr.ReadInt32();
            model = cr.ReadInt32();

            startOffset = cr.ReadInt32();
            numberOfIndexes = cr.ReadInt16();
            startVertex = cr.ReadInt32();

            numberOfFaces = cr.ReadInt32();
            index = cr.ReadInt32();

            min = cr.ReadVector3();
            max = cr.ReadVector3();
        }
    }
}
