using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline.Processors;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

using Sxe.Library.MultiModel;
using Sxe.Library;


namespace Sxe.Content.MultiModel
{
    public class MultiModelContent
    {
        //ModelContent[] models;
        NodeContent[] models;
        GraphicsDevice device;

        MeshContent combinedMesh;
        int positionOffset = 0; //this keeps track of the vertex offset
        int positionIndexOffset = 0;
        int indexOffset = 0;

        ModelContent model;
        List<Triangle> triangles;

        List<Vector2> textureCoordinates;
        //List<Vector2> normalizedTextureCoordinates;

        Dictionary<string, TextureInfo> textureCache = new Dictionary<string, TextureInfo>();
        Dictionary<GeometryContent, GeometryInfo> textureDictionary;
        TextureSpace atlasTexture;

        Color[] lightMap;
        int lightMapSize;

        MegaTexture megaTexture;
        string megaTextureFileName;

        Log log;

        class TextureInfo
        {
            public PixelBitmapContent<Color> pixels;
            public byte[] bytes;
        }

        class GeometryInfo
        {
            public TextureSpaceNode [] TextureSpaceNodes;
            public TextureInfo BitmapContent;
            public Vector2 MaxCoordinates = Vector2.One;
            public Vector2 MinCoordinates = Vector2.Zero;

            /// <summary>
            /// How many tiles this geometry takes up
            /// </summary>
            public Point Tiles
            {
                
                get
                {
                    Point tiles =  new Point((int)Math.Abs(MaxCoordinates.X - MinCoordinates.X),
                    (int)Math.Abs(MaxCoordinates.Y - MinCoordinates.Y));

                    if (tiles.X < 1) tiles.X = 1;
                    if (tiles.Y < 1) tiles.Y = 1;
                    return tiles;
                }
            }

            /// <summary>
            /// The total number of tiles
            /// </summary>
            public int NumberOfTiles
            {
                get { return Tiles.X * Tiles.Y; }
            }

            public TextureSpaceNode GetNodeByCoordinate(Vector2 texCoord)
            {
                int i = (int)((texCoord.X - MinCoordinates.X) / (MaxCoordinates.X - MinCoordinates.X + 0.01f) * Tiles.X);
                int j = (int)((texCoord.Y - MinCoordinates.Y) / (MaxCoordinates.Y - MinCoordinates.Y + 0.01f) * Tiles.Y);

                return TextureSpaceNodes[i + j * Tiles.X];
            }

        }

        public MultiModelContent(MultiModelCollection collection, ContentProcessorContext context, GraphicsDevice inDevice)
        {

            log = new Log("log.txt");
            //System.Diagnostics.Debugger.Launch();

            device = inDevice;
            atlasTexture = new TextureSpace(1024); //arbitrary default size
            atlasTexture.AllowResize = true;
            textureCoordinates = new List<Vector2>();
            //normalizedTextureCoordinates = new List<Vector2>();
            textureDictionary = new Dictionary<GeometryContent, GeometryInfo>();

            models = new NodeContent[collection.ModelInfo.Length];
            triangles = new List<Triangle>();

            combinedMesh = new MeshContent();
            combinedMesh.Geometry.Add(new GeometryContent());


            NodeContent rootNode = new NodeContent();

            //System.Diagnostics.Debugger.Launch();
            //Loop through collection, process each model

            log.Print("**Processing materials**");

            DateTime start = DateTime.Now;
            for (int i = 0; i < collection.ModelInfo.Length; i++)
            {
                //models[i] = context.BuildAndLoadAsset<NodeContent, ModelContent>(new ExternalReference<NodeContent>(collection.ModelInfo[i].Name),
                //    typeof(ModelProcessor).Name, null, collection.ModelInfo[i].Importer);


     
                models[i] = context.BuildAndLoadAsset<NodeContent, NodeContent>(new ExternalReference<NodeContent>(collection.ModelInfo[i].Name),
                     null, null,collection.ModelInfo[i].Importer);

                //First, process the materials, so we can get texture coordinate information
                ProcessMaterials(models[i], context);
            }
            TimeSpan span = DateTime.Now - start;
            log.Print("-Time taken: " + span.TotalSeconds.ToString());

            lightMapSize = 1024;
            lightMap = new Color[lightMapSize * lightMapSize];

            //System.Diagnostics.Debugger.Launch();

            //HEY HEY HEY

            log.Print("**Processing models**");
            start = DateTime.Now;
            for (int i = 0; i < collection.ModelInfo.Length; i++)
            {
                ProcessNode(models[i]);
            }
            span = start - DateTime.Now;
            log.Print("-Time taken: " + span.TotalSeconds.ToString());

            Texture2D lightTexture = new Texture2D(device, lightMapSize, lightMapSize, 1, TextureUsage.None, SurfaceFormat.Color);
            lightTexture.SetData<Color>(lightMap);
            lightTexture.Save("lightmap.png", Microsoft.Xna.Framework.Graphics.ImageFileFormat.Png);


            log.Print("**Creating atlas texture**");
            start = DateTime.Now;
            megaTextureFileName = CreateAtlasTexture(collection.FileName);
            span = start - DateTime.Now;
            log.Print("-Time taken: " + span.TotalSeconds.ToString());

            combinedMesh.Geometry[0].Vertices.Channels.Add<Vector2>(VertexChannelNames.TextureCoordinate(0), textureCoordinates);

            //foreach (Triangle tri in triangles)
            //{
            //    normalizedTextureCoordinates.Add(tri.Vertex0.TextureCoordinate);
            //    normalizedTextureCoordinates.Add(tri.Vertex1.TextureCoordinate);
            //    normalizedTextureCoordinates.Add(tri.Vertex2.TextureCoordinate);
            //}

            //combinedMesh.Geometry[0].Vertices.Channels.Add<Vector2>(VertexChannelNames.TextureCoordinate(1), normalizedTextureCoordinates);
            ////System.Diagnostics.Debugger.Launch();

            rootNode.Children.Add(combinedMesh);

            model = context.Convert<NodeContent, ModelContent>(rootNode, typeof(ModelProcessor).Name);
        }

        string CreateAtlasTexture(string fileName)
        {
            fileName = Path.GetFileNameWithoutExtension(fileName);
            fileName = fileName + "_megatexture.atlas";
 
            FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite);
            megaTexture = MegaTexture.Create(fs, (uint)atlasTexture.Size, 512, Color.White, device);

            foreach (GeometryInfo info in textureDictionary.Values)
            {
                PixelBitmapContent<Color> bitmap = info.BitmapContent.pixels;
                //if (info.BitmapContent.Width != info.BitmapContent.Height)
                //{
                //    int realSize = (int)MathHelper.Max(info.BitmapContent.Width, info.BitmapContent.Height);
                //    bitmap = new PixelBitmapContent<Color>(realSize, realSize);
                //    BitmapContent.Copy(info.BitmapContent, new Rectangle(0, 0, info.BitmapContent.Width, info.BitmapContent.Height),
                //        bitmap, new Rectangle(0, 0, realSize, realSize));

                //}
                //else
                //    bitmap = info.BitmapContent;

                //byte[] data = bitmap.GetPixelData();
                byte[] data = info.BitmapContent.bytes;
                
                //foreach (TextureSpaceNode node in info.TextureSpaceNodes)
                //{
                //    //Write the data for each node
                //    //ta.Write(data, node.Rectangle);
                //    //This approach didn't work because sometimes the bitmap content is smaller than the node

                    

                //    megaTexture.Write(data, new Rectangle(node.Rectangle.X, node.Rectangle.Y, bitmap.Width, bitmap.Height));
                //}
                TextureSpaceNode node = info.TextureSpaceNodes[0];
                int xDelta = node.Rectangle.Width / bitmap.Width;
                int yDelta = node.Rectangle.Height / bitmap.Height;

                for (int x = 0; x < xDelta; x++)
                {
                    for (int y = 0; y < yDelta; y++)
                    {
                        megaTexture.Write(data, new Rectangle(
                            node.Rectangle.X + bitmap.Width * x,
                            node.Rectangle.Y + bitmap.Height * y,
                            bitmap.Width,
                            bitmap.Height));
                    }
                }

            }

            megaTexture.CreateIndex();
            megaTexture.Close();
            return fileName;
            

        }

        TextureInfo GetBitmapContent(ExternalReference<TextureContent> reference, ContentProcessorContext context)
        
        {
            string name = reference.Filename;
            //Do we have an entry for this in our texture cache?
            if (!textureCache.ContainsKey(name))
            {
                TextureContent texContent = context.BuildAndLoadAsset<TextureContent, TextureContent>(reference, typeof(TextureProcessor).Name);
                TextureInfo info = new TextureInfo();

                //Just look at the first level of the content
                texContent.ConvertBitmapType(typeof(PixelBitmapContent<Color>));
                PixelBitmapContent<Color> pixels = texContent.Faces[0][0] as PixelBitmapContent<Color>;

                if (pixels.Width != pixels.Height)
                {
                    int realSize = (int)MathHelper.Max(pixels.Width, pixels.Height);
                    PixelBitmapContent<Color> bitmap = new PixelBitmapContent<Color>(realSize, realSize);
                    BitmapContent.Copy(pixels, new Rectangle(0, 0, pixels.Width, pixels.Height),
                        bitmap, new Rectangle(0, 0, realSize, realSize));
                    pixels = bitmap;
                }

                info.pixels = pixels;
                info.bytes = pixels.GetPixelData();

                textureCache.Add(name, info);
            }

            return textureCache[name];



        }

        void ProcessMaterials(NodeContent content, ContentProcessorContext context)
        {
            MeshContent mesh = content as MeshContent;

        

            if (mesh != null)
            {
                //We'll keep a list of used textures, because if different geometry contents
                //use the same texture, we don't want it added multiple times
                Dictionary<string, GeometryInfo> usedDictionary = new Dictionary<string, GeometryInfo>();


                //Loop through all the geometry in the mesh
                foreach (GeometryContent geometry in mesh.Geometry)
                {
                   
                    //Get min and max texture coordinate
                    Vector2 min, max;
                    GetMinMaxTextureCoordinates(
                        geometry.Vertices.Channels[VertexChannelNames.TextureCoordinate(0)].ReadConvertedContent<Vector2>(),
                        out min, out max);

                    ExternalReference<TextureContent> texture = null;
                    //System.Diagnostics.Debugger.Launch();

                    //This is a hack to get the first texture in the dictionary
                    foreach (ExternalReference<TextureContent> t in geometry.Material.Textures.Values)
                    {
                        if (texture == null)
                        {
                            texture = t;
                            break;
                        }
                    }

                    //Erase all the references to textures/materials
                    geometry.Material.Textures.Clear();

                    //Did we already use this texture?
                    GeometryInfo gi;
                    //if (usedDictionary.ContainsKey(texture.Filename))
                    //{
                    //    gi = usedDictionary[texture.Filename];
                    //}
                    //else
                    //{


                    TextureInfo pixels = GetBitmapContent(texture, context);

                    int size = Math.Max(pixels.pixels.Width, pixels.pixels.Height);

                    gi = new GeometryInfo();
                    gi.MinCoordinates = min;
                    gi.MaxCoordinates = max;
                    //gi.TextureSpaceNodes = new TextureSpaceNode[gi.NumberOfTiles];
                    //for (int i = 0; i < gi.NumberOfTiles; i++)
                    //{
                    //    gi.TextureSpaceNodes[i] = atlasTexture.GetFreeNode(size);
                    //    gi.TextureSpaceNodes[i].Occupied = true;
                    //}
                    gi.TextureSpaceNodes = new TextureSpaceNode[1];

                    int requestSize = size * (int)MathHelper.Max(gi.Tiles.X, gi.Tiles.Y);
                    int powSize = MathUtilities.GetNextPowerOfTwo(requestSize);

                    gi.TextureSpaceNodes[0] = atlasTexture.GetFreeNode(powSize);
                    gi.TextureSpaceNodes[0].Occupied = true;

                    gi.BitmapContent = pixels;



                        //usedDictionary.Add(texture.Filename, gi);

                    //}

                    

                    textureDictionary.Add(geometry, gi);

                }
                

            }

            foreach (NodeContent child in content.Children)
                ProcessMaterials(child, context);
        }

        void ProcessNode(NodeContent content)
        {

            MeshContent mesh = content as MeshContent;

            if (mesh != null)
            {
                //Add all positions from the parent to the mesh builder
                foreach (Vector3 position in mesh.Positions)
                {
                    combinedMesh.Positions.Add(position);
                }

                foreach (GeometryContent geometry in mesh.Geometry)
                {
                    VertexChannel textureChannel = geometry.Vertices.Channels[VertexChannelNames.TextureCoordinate(0)];

                    //Loop through all the triangles and add them to the triangles list
                    for (int i = 0; i < geometry.Indices.Count / 3; i++)
                    {
                        int index0 = geometry.Vertices.PositionIndices[geometry.Indices[i * 3 + 0]];
                        int index1 = geometry.Vertices.PositionIndices[geometry.Indices[i * 3 + 1]];
                        int index2 = geometry.Vertices.PositionIndices[geometry.Indices[i * 3 + 2]];

                        Vector3 p0 = mesh.Positions[index0];
                        Vector3 p1 = mesh.Positions[index1];
                        Vector3 p2 = mesh.Positions[index2];

                        Vector2 t0 = (Vector2)textureChannel[geometry.Indices[i * 3]];
                        Vector2 t1 = (Vector2)textureChannel[geometry.Indices[i * 3 + 1]];
                        Vector2 t2 = (Vector2)textureChannel[geometry.Indices[i * 3 + 2]];

                        Triangle tri = new Triangle(
                            new TriangleVertex(p0, t0), new TriangleVertex(p1, t1), new TriangleVertex(p2, t2));
                        triangles.Add(tri);
                    }

                    if(combinedMesh.Geometry[0].Material == null)
                    combinedMesh.Geometry[0].Material = geometry.Material;



                    //Add all the position indices
                    foreach (int index in geometry.Indices)
                    {
                        combinedMesh.Geometry[0].Indices.Add(positionIndexOffset + index);
                    }
                    foreach (int index in geometry.Vertices.PositionIndices)
                    {
                        combinedMesh.Geometry[0].Vertices.Add(positionOffset + index);
                    }



                    //Add all the texture coordinates

                    GeometryInfo gi = textureDictionary[geometry];

                    Vector2 min, max;
                    GetMinMaxTextureCoordinates(textureChannel.ReadConvertedContent<Vector2>(),
                        out min, out max);


                    int count1 = 0;
                    foreach (Vector2 texCoord in textureChannel.ReadConvertedContent<Vector2>())
                    {
                        if (max.Y > gi.MaxCoordinates.Y)
                            throw new Exception("Max Y greater for some reason!");

                        //Remap texture coordinate
                        Vector2 remappedCoord = texCoord;

                        //Find the normalized coordinate

                        TextureSpaceNode node = gi.TextureSpaceNodes[0];
                       
                        //First, remap the x and y coord to 0 and 1
                        remappedCoord = (remappedCoord - gi.MinCoordinates) / (gi.MaxCoordinates - gi.MinCoordinates);

                        //remappedCoord.X *= node.TextureSize.X;
                        //remappedCoord.Y *= node.TextureSize.Y;
                        //remappedCoord += node.TexturePosition;

                        remappedCoord *= node.TextureSize;
                        //Then add in the offset to that tiles
                        remappedCoord += node.TexturePosition;

                        if (remappedCoord.X > 1.0f || remappedCoord.Y >= 1.0f)
                            count1 -= 1;

                        textureCoordinates.Add(remappedCoord);
                        count1++;
                    }



                    int count2 = 0;
                    //Loop through combined mesh, use indexOffset and positionIndexOffset
                    for (int i = 0; i < geometry.Indices.Count / 3; i++ )
                    {
                        int index0 = combinedMesh.Geometry[0].Indices[i * 3 + 0 + indexOffset];
                        int index1 = combinedMesh.Geometry[0].Indices[i * 3 + 1 + indexOffset];
                        int index2 = combinedMesh.Geometry[0].Indices[i * 3 + 2 + indexOffset];

                        int positionIndex0 = combinedMesh.Geometry[0].Vertices.PositionIndices[index0];
                        int positionIndex1 = combinedMesh.Geometry[0].Vertices.PositionIndices[index1];
                        int positionIndex2 = combinedMesh.Geometry[0].Vertices.PositionIndices[index2];

                        Vector3 position0 = combinedMesh.Positions[positionIndex0];
                        Vector3 position1 = combinedMesh.Positions[positionIndex1];
                        Vector3 position2 = combinedMesh.Positions[positionIndex2];

                        Vector2 uv0 = textureCoordinates[index0];
                        Vector2 uv1 = textureCoordinates[index1];
                        Vector2 uv2 = textureCoordinates[index2];

                        Vector2 l1 = uv0 * (lightMapSize-1);
                        Vector2 l2 = uv1 * (lightMapSize-1);
                        Vector2 l3 = uv2 * (lightMapSize-1);

                        Point p1 = new Point((int)l1.X, (int)l1.Y);
                        Point p2 = new Point((int)l2.X, (int)l2.Y);
                        Point p3 = new Point((int)l3.X, (int)l3.Y);

                        int minX = (int)MathUtilities.Min<float>(l1.X, l2.X, l3.X);
                        int maxX = (int)MathUtilities.Max<float>(l1.X, l2.X, l3.X);
                        int minY = (int)MathUtilities.Min<float>(l1.Y, l2.Y, l3.Y);
                        int maxY = (int)MathUtilities.Max<float>(l1.Y, l2.Y, l3.Y);

                        if (maxY >= 1024)
                            maxY = 1023;

                        //Loop through each texel
                        //Note that the min and max make a square, so, we have to check each one
                        for(int x = minX; x <= maxX; x++)
                        {
                            for(int y = minY; y <= maxY; y++)
                            {

                                
                                Point point = new Point(x, y);
                                //Let's see if the point is in the triangle

                                
                                float alpha, beta, gamma;

                                MathUtilities.CalculateBarycentric(point, p1, p2, p3, out alpha, out beta, out gamma);

                                const float threshold = 0.00001f;

                                if (alpha >= 0.0f - threshold && beta >= 0.0f - threshold && gamma >= 0.0f - threshold
                                    && alpha <= 1.0f + threshold && beta <= 1.0f + threshold && gamma <= 1.0f + threshold)
                                {
                                    //float test = MathUtilities.Min<float>(
                                    //    Math.Abs(1.0f - alpha), Math.Abs(1.0f - beta), Math.Abs(1.0f - gamma));
                                    //if (test < 0.5f)
                                    //    lightMap[x + y * lightMapSize] = Color.Green;
                                    //else

           
                                    Vector3 worldCoord = alpha * position0 + beta * position1 + gamma * position2;

                                    if (lightMap[x + y * lightMapSize] != Color.Red)
                                        lightMap[x + y * lightMapSize] = Color.Red;
                                    else
                                        lightMap[x + y * lightMapSize] = Color.Blue;

                                    Vector3 color = new Vector3(100.0f / worldCoord.Length());
                                    lightMap[x + y * lightMapSize] = new Color(color);
                                }
                            }
                        }

                        //to go through the remapped triangles.
                        //Then, loop through each texel of the triangle based on the remapped U,V
                        //coordinate and the desired resolution of hte lightmaps.
                        //Create a Luxel object for each of these and save in the tag entry
                        count2+=3;
                    }

                    //if (count1 != count2)
                    //    throw new Exception("SUPER MISMATCH GAY " + count1.ToString() + " " + count2.ToString() + " " + geometry.Indices.Count.ToString());


                    indexOffset += geometry.Indices.Count;
                    positionIndexOffset += geometry.Vertices.PositionIndices.Count;
                }

                
                positionOffset += mesh.Positions.Count;

            }

            foreach (NodeContent child in content.Children)
                ProcessNode(child);
        }


        /// <summary>
        /// Gets the minimum and maximum texture coordinate form a list of texture coordinates
        /// </summary>
        void GetMinMaxTextureCoordinates(IEnumerable<Vector2> vectorList, out Vector2 min, out Vector2 max)
        {
            min = new Vector2(float.MaxValue);
            max = new Vector2(float.MinValue);
            foreach(Vector2 vec in vectorList)
            {
                if (vec.X < min.X)
                    min.X = vec.X;
                if (vec.X > max.X)
                    max.X = vec.X;

                if (vec.Y < min.Y)
                    min.Y = vec.Y;
                if (vec.Y > max.Y)
                    max.Y = vec.Y;
            }
        }

        public void Write(ContentWriter writer)
        {
            writer.WriteObject(model);
            writer.Write(megaTextureFileName);
            //megaTexture.Save(writer);
            //writer.WriteObject(atlasContent);
        }
    }
}
