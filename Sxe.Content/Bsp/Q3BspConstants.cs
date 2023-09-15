using System;
using System.Collections.Generic;
using System.Text;

namespace Sxe.Content.Bsp
{

    /// <summary>
    /// List of all the lumps in a quake 3 map
    /// </summary>
    public enum Lump
    {
        Entities = 0, //important
        Textures = 1, //important
        Planes = 2, //important for PVS
        Nodes = 3,  //important for PVS
        Leafs = 4, //important
        LeafFaces = 5, //important
        LeafBrushes = 6, //NOT for rendering
        Models = 7, //important
        Brushes = 8, //NOT for rendering
        BrushSides = 9, //NOT for rendeirng
        Vertexes = 10, //important
        MeshVertexes = 11,//important
        Effects = 12, //NOT important
        Faces = 13, //important
        LightMaps = 14, //temporarily important
        LightVolumes = 15, //temporarily important
        VisibilityData = 16 //temporarily important
    }

    public static class Q3BspConstants
    {
        public const int NumberOfDirectoryEntries = 17;
        public const int HeaderSize = 4;

        public const int LightMapSize = 128;

        public const int TextureNameSize = 64;
        public const int TextureLumpSize = (64 + 4 + 4);
        public const int PlaneLumpSize = (4 * 3 + 4); //4 bytes each, for each float
        public const int NodeLumpSize = (4 + 2 * 4 + 3 * 4 + 3 * 4); //4 bytes per int
        public const int LeafLumpSize = (4 + 4 + 3 * 4 + 3 * 4 + 4 + 4 + 4 + 4);
        public const int LeafFacesLumpSize = 4;
        public const int ModelLumpSize = (3 * 4 + 3 * 4 + 4 + 4 + 4 + 4);
        public const int VertexLumpSize = (3 * 4 + 3 * 4 + 4 + 4 + 4 + 4);
        public const int MeshVertexesLumpSize = 4;
        public const int FacesLumpSize = (4 + 4 + 4 + 4 + 4 + 4 + 4 + 4 + 2 * 4 + 2 * 4 + 3 * 4 + 2 * 3 * 4 + 3 * 4 + 2 * 4);
        public const int LightMapLumpSize = (128 * 128 * 3);
        public const int LightVolumeLumpSize = (3 * 1 + 3 * 1 + 2 * 1);
    }
}
