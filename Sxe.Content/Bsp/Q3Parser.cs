using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Sxe.Library.Bsp;

namespace Sxe.Content.Bsp
{
    public class Q3Parser : IBspParser
    {
        /// <summary>
        /// Reads in header, and populates data.directoryEntry
        /// </summary>
        public BspHeader ReadHeader(FileStream file, BinaryReader reader)
        {
            file.Position = 0;
            BspHeader outHeader = new BspHeader();
            outHeader.header = reader.ReadBytes(Q3BspConstants.HeaderSize);
            outHeader.version = reader.ReadInt32();
            outHeader.directoryEntry = new BspDirectoryEntry[Q3BspConstants.NumberOfDirectoryEntries];

            //Find all the directory entries
            for (int i = 0; i < Q3BspConstants.NumberOfDirectoryEntries; i++)
            {
                outHeader.directoryEntry[i] = new BspDirectoryEntry();
                outHeader.directoryEntry[i].offset = reader.ReadInt32();
                outHeader.directoryEntry[i].length = reader.ReadInt32();
            }
            return outHeader;
        }

        public string ReadEntities(FileStream file, BinaryReader reader, BspHeader header)
        {
            int position = header.directoryEntry[(int)Lump.Entities].offset;
            int length = header.directoryEntry[(int)Lump.Entities].length;
            file.Position = position;
            byte[] entLump;
            entLump = reader.ReadBytes(length-1);
            return System.Text.Encoding.ASCII.GetString(entLump);
        }

        public BspTextures[] ReadTextures(FileStream file, BinaryReader reader,BspHeader header)
        {
            int position = header.directoryEntry[(int)Lump.Textures].offset;
            int length = header.directoryEntry[(int)Lump.Textures].length;
            file.Position = position;
            int numTextures = length / Q3BspConstants.TextureLumpSize;

            BspTextures[] textures = new BspTextures[numTextures];
            for (int i = 0; i < numTextures; i++)
            {
                textures[i] = new BspTextures();
                textures[i].textureName = System.Text.Encoding.ASCII.GetString(reader.ReadBytes(Q3BspConstants.TextureNameSize)).TrimEnd('\0');
                reader.ReadInt32(); //read flags - advance reader
                reader.ReadInt32(); //read contents - advance reader

            }
            return textures;
        }

        public BspPlane[] ReadPlanes(FileStream file, BinaryReader reader,BspHeader header)
        {
            int position = header.directoryEntry[(int)Lump.Planes].offset;
            int length = header.directoryEntry[(int)Lump.Planes].length;
            file.Position = position;
            int numPlanes = length / Q3BspConstants.PlaneLumpSize;
            BspPlane [] planes = new BspPlane[numPlanes];
            for (int i = 0; i < numPlanes; i++)
            {
                planes[i] = new BspPlane();
                planes[i].normal = BspUtilities.ReadQuake3Vector(reader);
                planes[i].distance = reader.ReadSingle();
            }
            return planes;
        }

        public BspNode [] ReadNodes(FileStream file, BinaryReader reader, BspHeader header)
        {
            int position = header.directoryEntry[(int)Lump.Nodes].offset;
            int length = header.directoryEntry[(int)Lump.Nodes].length;
            file.Position = position;
            int numNodes = length / Q3BspConstants.NodeLumpSize;

            BspNode [] nodes = new BspNode[numNodes];
            for (int i = 0; i < numNodes; i++)
            {
                nodes[i] = new BspNode();
                nodes[i].planeIndex = reader.ReadInt32();
                nodes[i].children[0] = reader.ReadInt32();
                nodes[i].children[1] = reader.ReadInt32();

                for (int j = 0; j < 3; j++)
                {
                    nodes[i].min[j] = reader.ReadInt32();
                }

                for (int j = 0; j < 3; j++)
                {
                    nodes[i].max[j] = reader.ReadInt32();
                }
            }
            return nodes;

        }

        public BspLeaf [] ReadLeafs(FileStream file, BinaryReader reader, BspHeader header)
        {
            int position = header.directoryEntry[(int)Lump.Leafs].offset;
            int length = header.directoryEntry[(int)Lump.Leafs].length;
            file.Position = position;
            int numLeafile = length / Q3BspConstants.LeafLumpSize;

            BspLeaf [] leafile = new BspLeaf[numLeafile];
            for (int i = 0; i < numLeafile; i++)
            {
                leafile[i] = new BspLeaf();
                leafile[i].cluster = reader.ReadInt32();
                leafile[i].area = reader.ReadInt32();

                for (int j = 0; j < 3; j++)
                    leafile[i].min[j] = reader.ReadInt32();

                for (int j = 0; j < 3; j++)
                    leafile[i].max[j] = reader.ReadInt32();

                leafile[i].leafFace = reader.ReadInt32();
                leafile[i].numberOfLeafFaces = reader.ReadInt32();
                leafile[i].leafBrush = reader.ReadInt32();
                leafile[i].numberOfLeafBrushes = reader.ReadInt32();
            }
            return leafile;
        }

        public BspLeafFaces [] ReadLeafFaces(FileStream file, BinaryReader reader, BspHeader header)
        {
            int position = header.directoryEntry[(int)Lump.LeafFaces].offset;
            int length = header.directoryEntry[(int)Lump.LeafFaces].length;
            file.Position = position;
            int numLeafFaces = length / Q3BspConstants.LeafFacesLumpSize;

            BspLeafFaces [] leaffaces = new BspLeafFaces[numLeafFaces];
            for (int i = 0; i < numLeafFaces; i++)
            {
                leaffaces[i] = new BspLeafFaces();
                leaffaces[i].face = reader.ReadInt32();
            }
            return leaffaces;

        }

        public BspModels [] ReadModels(FileStream file, BinaryReader reader, BspHeader header)
        {
            int position = header.directoryEntry[(int)Lump.Models].offset;
            int length = header.directoryEntry[(int)Lump.Models].length;
            file.Position = position;
            int numModels = length / Q3BspConstants.ModelLumpSize;

            BspModels [] models = new BspModels[numModels];
            for (int i = 0; i < numModels; i++)
            {
                models[i] = new BspModels();
                models[i].min = BspUtilities.ReadQuake3Vector(reader);
                models[i].max = BspUtilities.ReadQuake3Vector(reader);

                //SWAP the max and the min values for these models
                //in the z coordinate. This is because the z sign is flipped
                //See ReadVector function
                float temp = models[i].min.Z;
                models[i].min.Z = models[i].max.Z;
                models[i].max.Z = temp;

                models[i].face = reader.ReadInt32();
                models[i].numberOfFaces = reader.ReadInt32();
                models[i].brush = reader.ReadInt32();
                models[i].numberOfBrushes = reader.ReadInt32();
            }
            return models;
        }

        public BspVertexes [] ReadVertexes(FileStream file, BinaryReader reader, BspHeader header)
        {
            int position = header.directoryEntry[(int)Lump.Vertexes].offset;
            int length = header.directoryEntry[(int)Lump.Vertexes].length;
            file.Position = position;
            int numVertexes = length / Q3BspConstants.VertexLumpSize;

            BspVertexes [] vertexes = new BspVertexes[numVertexes];
            for (int i = 0; i < numVertexes; i++)
            {
                vertexes[i] = new BspVertexes();
                vertexes[i].position = BspUtilities.ReadQuake3Vector(reader);
                //This first texcoord is the diffuse texcoord
                vertexes[i].texCoords[0] = new Vector2(reader.ReadSingle(), reader.ReadSingle());
                //Second texcoord is lightmap texcoord
                vertexes[i].texCoords[1] = new Vector2(-reader.ReadSingle(), reader.ReadSingle());
                vertexes[i].normal = BspUtilities.ReadQuake3Vector(reader);
                vertexes[i].color = reader.ReadBytes(4);
            }
            return vertexes;
        }

        public BspMeshVertexes [] ReadMeshVertexes(FileStream file, BinaryReader reader, BspHeader header)
        {
            int position = header.directoryEntry[(int)Lump.MeshVertexes].offset;
            int length = header.directoryEntry[(int)Lump.MeshVertexes].length;
            file.Position = position;
            int numMeshVerts = length / Q3BspConstants.MeshVertexesLumpSize;

            BspMeshVertexes [] meshVertexs = new BspMeshVertexes[numMeshVerts];
            for (int i = 0; i < numMeshVerts; i++)
            {
                meshVertexs[i] = new BspMeshVertexes();
                meshVertexs[i].offset = reader.ReadInt32();
            }
            return meshVertexs;
        }

        public BspFace [] ReadFaces(FileStream file, BinaryReader reader, BspHeader header)
        {
            int position = header.directoryEntry[(int)Lump.Faces].offset;
            int length = header.directoryEntry[(int)Lump.Faces].length;
            file.Position = position;
            int numberOfFaces = length / Q3BspConstants.FacesLumpSize;

            BspFace [] faces = new BspFace[numberOfFaces];
            for (int i = 0; i < numberOfFaces; i++)
            {

                faces[i] = new BspFace();
                faces[i].texture = reader.ReadInt32();
                faces[i].effect = reader.ReadInt32();
                faces[i].type = reader.ReadInt32();
                faces[i].vertex = reader.ReadInt32();
                faces[i].numberOfVertexes = reader.ReadInt32();
                faces[i].meshVertex = reader.ReadInt32();
                faces[i].numberOfMeshVertexes = reader.ReadInt32();
                faces[i].lm_index = reader.ReadInt32();
                faces[i].lm_start[0] = reader.ReadInt32();
                faces[i].lm_start[1] = reader.ReadInt32();
                faces[i].lm_size[0] = reader.ReadInt32();
                faces[i].lm_size[1] = reader.ReadInt32();
                faces[i].lm_origin = BspUtilities.ReadQuake3Vector(reader);
                faces[i].lightMapVectors[0] = BspUtilities.ReadQuake3Vector(reader);
                faces[i].lightMapVectors[1] = BspUtilities.ReadQuake3Vector(reader);
                faces[i].normal = BspUtilities.ReadQuake3Vector(reader);
                //float temp = faces[i].normal.Y;
                //faces[i].normal.Y = -faces[i].normal.Z;
                //faces[i].normal.Z = temp;

                faces[i].size[0] = reader.ReadInt32();
                faces[i].size[1] = reader.ReadInt32();

                //Create a patch, if necessary
                //TODO: Redo patch creation for this
                if (faces[i].type == (int)BspFaceType.Patch)
                {
                    //int numPatchesWide = (faces[i].size[0] - 1) / 2;
                    //int numPatchesHigh = (faces[i].size[1] - 1) / 2;


                    //faces[i].patch = new BSPPatch();
                    //faces[i].patch.numBiPatches = numPatchesHigh * numPatchesWide;
                    //faces[i].patch.patches = new Q3BiquadraticPatch[numPatchesHigh * numPatchesWide];

                    //for (int y = 0; y < numPatchesHigh; y++)
                    //{
                    //    for (int x = 0; x < numPatchesWide; x++)
                    //    {
                    //        faces[i].patch.patches[y * numPatchesWide + x] = new Q3BiquadraticPatch(this.device);

                    //        for (int row = 0; row < 3; ++row)
                    //        {
                    //            for (int point = 0; point < 3; ++point)
                    //            {

                    //                int vertindex = faces[i].vertex + (y * 2 * faces[i].size[0] + x * 2) + row * faces[i].size[0] + point;

                    //                faces[i].patch.patches[y * numPatchesWide + x].controls[row * 3 + point] =
                    //                    new Q3VertexBSP(vertexes[vertindex]);
                    //            }
                    //        }

                    //        faces[i].patch.patches[y * numPatchesWide + x].tesellate(this.tesselation);
                    //    }
                    //}

                }

            }

            return faces;


        }

        public BspLightMap [] ReadLightMaps(FileStream file, BinaryReader reader, BspHeader header)
        {
            int position = header.directoryEntry[(int)Lump.LightMaps].offset;
            int length = header.directoryEntry[(int)Lump.LightMaps].length;
            file.Position = position;
            int numLightMaps = length / Q3BspConstants.LightMapLumpSize;
            BspLightMap [] lightmaps = new BspLightMap[numLightMaps];

            for (int i = 0; i < numLightMaps; i++)
            {

                lightmaps[i] = new BspLightMap(Q3BspConstants.LightMapSize);
                for (int x = 0; x < Q3BspConstants.LightMapSize; x++)
                {
                    for (int y = 0; y < Q3BspConstants.LightMapSize; y++)
                    {
                        byte[] temp = new byte[3];
                        for (int j = 0; j < 3; j++)
                        {
                            temp[j] = reader.ReadByte();
                        }
                        lightmaps[i].map[y + x * Q3BspConstants.LightMapSize] = new Color(temp[0], temp[1], temp[2], 255);
                    }
                }
            }

            return lightmaps;
        }

        public BspLightVolume [] ReadLightVolumes(FileStream file, BinaryReader reader, BspHeader header)
        {
            int position = header.directoryEntry[(int)Lump.LightVolumes].offset;
            int length = header.directoryEntry[(int)Lump.LightVolumes].length;
            file.Position = position;
            int numLightVols = length / Q3BspConstants.LightVolumeLumpSize;

            BspLightVolume [] lightvols = new BspLightVolume[numLightVols];
            for (int i = 0; i < numLightVols; i++)
            {
                lightvols[i] = new BspLightVolume();

                for (int j = 0; j < 3; j++)
                    lightvols[i].ambient[j] = reader.ReadByte();

                lightvols[i].AmbientColor = new Vector3(lightvols[i].ambient[0] / 255f, lightvols[i].ambient[1] / 255f, lightvols[i].ambient[2] / 255f);

                for (int j = 0; j < 3; j++)
                    lightvols[i].directional[j] = reader.ReadByte();

                lightvols[i].DirectionalColor = new Vector3(lightvols[i].directional[0] / 255f, lightvols[i].directional[1] / 255f, lightvols[i].directional[2] / 255f);

                for (int j = 0; j < 2; j++)
                    lightvols[i].dir[j] = reader.ReadByte();
            }
            return lightvols;
        }

        public BspVisibilityData ReadVisibilityData(FileStream file, BinaryReader reader, BspHeader header)
        {
            int position = header.directoryEntry[(int)Lump.VisibilityData].offset;
            int length = header.directoryEntry[(int)Lump.VisibilityData].length;
            file.Position = position;
            int size = length;

            //Don't read visdata if it doesn't exist!
            if (size <= 0)
                return null;


            BspVisibilityData visdata = new BspVisibilityData();
            visdata.numberOfVectors = reader.ReadInt32();
            visdata.sizeOfVectors = reader.ReadInt32();

            visdata.visibilityData = new byte[visdata.numberOfVectors * visdata.sizeOfVectors];

            for (int i = 0; i < visdata.numberOfVectors * visdata.sizeOfVectors; i++)
            {
                visdata.visibilityData[i] = reader.ReadByte();
            }

            return visdata;

        }

   
    }
}
