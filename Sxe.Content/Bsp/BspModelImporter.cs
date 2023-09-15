using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;

using System.IO;

using Sxe.Library.Bsp;
using Sxe.Library.Utilities;

namespace Sxe.Content.Bsp
{
    /// <summary>
    /// This class will be instantiated by the XNA Framework Content Pipeline
    /// to import a file from disk into the specified type, TImport.
    /// 
    /// This should be part of a Content Pipeline Extension Library project.
    /// 
    /// TODO: change the ContentImporter attribute to specify the correct file
    /// extension, display name, and default processor for this importer.
    /// </summary>
    [ContentImporter(".bsp", DisplayName = "BSP Mesh Importer", DefaultProcessor = "BspModelProcessor")]
    public class BspModelImporter : ContentImporter<NodeContent>
    {
        NodeContent rootNode;
        MeshBuilder meshBuilder;

        int textureCoordinateIndex;
        int lightTextureCoordinateIndex;

        int[] positionIndices;

        public override NodeContent Import(string filename, ContentImporterContext context)
        {

            System.Diagnostics.Debugger.Launch();

            //Get a file stream going on this hizzy
            FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);


            //Create an output DOM to hold bsp input
            BspDom outBsp = BspDom.CreateFromFile(fs, br, new Q3Parser());



            rootNode = new BspNodeContent(outBsp);
            rootNode.Identity = new ContentIdentity(filename);

        

            //Loop through each texture
            for (int i = 0; i < outBsp.Textures.Count; i++)
            {
                for (int l = 0; l < outBsp.LightMaps.Count; l++)
                {
                   
                    //Create a material for this face
                    outBsp.Textures[i].textureName = outBsp.Textures[i].textureName.Replace('+', 'b');
                    string materialPath = FileUtilities.FindExtension(outBsp.Textures[i].textureName, new string[] { ".png", ".tga", ".jpg" });
                    string materialFullPath = Path.GetFullPath(materialPath);
                    context.AddDependency(materialFullPath);



                    //Create a new mesh content
                    MeshContent mesh = new MeshContent();
                    mesh.Name = outBsp.Textures[i].textureName + l.ToString();
                    mesh.OpaqueData.Add("lightmap", l);

                    //Add all the positions ot the mesh content... this is bad
                    //We need to redo this so only vertices that apply to the faces are added
                    for (int k = 0; k < outBsp.Vertexes.Count; k++)
                    {
                        mesh.Positions.Add(outBsp.Vertexes[k].position);
                    }

                    //Loop through all faces, and add their vertex data
                    for (int j = 0; j < outBsp.Faces.Count; j++)
                    {


                        BspFace face = outBsp.Faces[j];

                        //don't add this face if its not the current texture
                        if (face.texture != i || face.lm_index  != l)
                            continue;

                        //StartMesh(outBsp.Textures[i].textureName + l.ToString(), outBsp);
                        GeometryContent geometry = new GeometryContent();
             

                        List<Vector2> texCoords = new List<Vector2>();
                        List<Vector2> lightTexCoords = new List<Vector2>();

                        EffectMaterialContent materialContent = new EffectMaterialContent();
                        materialContent.Identity = new ContentIdentity(outBsp.Textures[i].textureName);
                        materialContent.Name = outBsp.Textures[i].textureName;
                        //materialContent.DiffuseColor = Vector3.One;
                        materialContent.Effect = new ExternalReference<EffectContent>("LevelEffect.fx");
                        materialContent.Textures.Add("g_baseTexture", new ExternalReference<TextureContent>(materialPath));
   
                        //meshBuilder.SetMaterial(materialContent);
                        geometry.Material = materialContent;
                        //geometry.Parent = rootNode;
                        //geometry.OpaqueData.Add("lightmap", l);
                        //geometry.Name = outBsp.Textures[i].textureName + j.ToString();
                        

     
                        //Add all the mesh verts, as well as texture data
                        for (int k = 0; k < face.numberOfMeshVertexes; k++)
                        {
                            int vertexIndex = face.vertex + outBsp.MeshVertexes[face.meshVertex + k].offset;
                            geometry.Vertices.Add(vertexIndex);
                            BspVertexes vertex = outBsp.Vertexes[vertexIndex];
                            texCoords.Add(vertex.texCoords[0]);
                            lightTexCoords.Add(vertex.texCoords[1]);
                        }

                        //Add the indices
                        for (int k = 0; k < face.numberOfMeshVertexes; k++)
                        {
                            geometry.Indices.Add(k);
                        }

                        geometry.Vertices.Channels.Add<Vector2>(VertexChannelNames.TextureCoordinate(0), texCoords);
                        geometry.Vertices.Channels.Add<Vector2>(VertexChannelNames.TextureCoordinate(1), lightTexCoords);

                        mesh.Geometry.Add(geometry);

                        if (geometry.Material.Textures.Count < 1)
                            geometry.Name = "butt pirate";
                        ////Loop through all verts, and add their vertex data
                        //for (int k = 0; k < face.numberOfMeshVertexes; k++)
                        //{
                        //    int vertexIndex = face.vertex + outBsp.MeshVertexes[face.meshVertex + k].offset;

                        //    BspVertexes vertex = outBsp.Vertexes[vertexIndex];


                        //    meshBuilder.SetVertexChannelData(textureCoordinateIndex, vertex.texCoords[0]);
                        //    meshBuilder.SetVertexChannelData(lightTextureCoordinateIndex, vertex.texCoords[1]);

                        //    meshBuilder.AddTriangleVertex(vertexIndex);

                        //}
                        //FinishMesh(l);
                    }

                    rootNode.Children.Add(mesh);
                    
                }

            
            }

            return rootNode;
 
        }

        //private void FinishMesh(int lightmap)
        //{
        //    MeshContent meshContent = meshBuilder.FinishMesh();
        //    meshContent.OpaqueData.Add("lightmap", lightmap);

        //    // Groups without any geometry are just for transform
        //    if (meshContent.Geometry.Count > 0)
        //    {
        //        // Add the mesh to the model
        //        rootNode.Children.Add(meshContent);
        //    }
        //    else
        //    {
        //        // Convert to a general NodeContent
        //        NodeContent nodeContent = new NodeContent();
        //        nodeContent.Name = meshContent.Name;

        //        // Add the transform-only node to the model
        //        rootNode.Children.Add(nodeContent);
        //    }

        //    meshBuilder = null;
        //}

        //void StartMesh(string name, BspDom bsp)
        //{
        //    meshBuilder = MeshBuilder.StartMesh(name);

        //    textureCoordinateIndex = meshBuilder.CreateVertexChannel<Vector2>(VertexChannelNames.TextureCoordinate(0));
        //    lightTextureCoordinateIndex = meshBuilder.CreateVertexChannel<Vector2>(VertexChannelNames.TextureCoordinate(1));

        //    positionIndices = new int[bsp.Vertexes.Count];
        //    for (int i = 0; i < bsp.Vertexes.Count; i++)
        //    {
        //        positionIndices[i] = meshBuilder.CreatePosition(bsp.Vertexes[i].position);
        //    }

        //}
    }
}
