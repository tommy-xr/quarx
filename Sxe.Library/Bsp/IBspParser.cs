using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Sxe.Library.Bsp
{

    public interface IBspParser
    {
        BspHeader ReadHeader(FileStream file, BinaryReader reader);
        string ReadEntities(FileStream file, BinaryReader reader, BspHeader header);
        BspTextures [] ReadTextures(FileStream file, BinaryReader reader, BspHeader header);
        BspPlane [] ReadPlanes(FileStream file, BinaryReader reader, BspHeader header);
        BspNode [] ReadNodes(FileStream file, BinaryReader reader, BspHeader header);
        BspLeaf [] ReadLeafs(FileStream file, BinaryReader reader, BspHeader header);
        BspLeafFaces [] ReadLeafFaces(FileStream file, BinaryReader reader, BspHeader header);
        BspModels [] ReadModels(FileStream file, BinaryReader reader, BspHeader header);
        BspVertexes [] ReadVertexes(FileStream file, BinaryReader reader, BspHeader header);
        BspMeshVertexes [] ReadMeshVertexes(FileStream file, BinaryReader reader, BspHeader header);
        BspFace [] ReadFaces(FileStream file, BinaryReader reader, BspHeader header);
        BspLightMap [] ReadLightMaps(FileStream file, BinaryReader reader, BspHeader header);
        BspLightVolume [] ReadLightVolumes(FileStream file, BinaryReader reader, BspHeader header);
        BspVisibilityData ReadVisibilityData(FileStream file, BinaryReader reader, BspHeader header);
    }
}
