using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Graphics.PackedVector;

using Sxe.Library.Utilities;

namespace Sxe.Library
{
    /// <summary>
    /// Manages reading/writing to a mega texture
    /// </summary>
    public class MegaTexture
    {

        const uint DataSize = 4;

        /// <summary>
        /// Creates a new texture atlas. Size is the width & height of the texture atlas
        /// and must be a power of two.
        /// </summary>
        public static MegaTexture Create(FileStream fs, uint size, uint indexSize, Color defaultColor, GraphicsDevice device)
        {
            MegaTexture ta = new MegaTexture(fs, device);
            ta.size = size;
            ta.indexSize = indexSize;
         

            //Write megatexture data out
            ta.bw.Write(size); //size of megatexture data

            int megaTextureAddress = (int)fs.Position;
            ta.bw.Write((int)-1); //position of megatexture data

            //Write index size out
            ta.bw.Write(indexSize); //size of index data
            int indexAddress = (int)fs.Position;
            ta.bw.Write((int)-1); //position of index data
            

            uint indexOffset = (uint)fs.Position;
            //Write out dummy stuff for the indices
            for (int i = 0; i < indexSize * indexSize; i++ )
            {
                //Use ulong because we're going to be writing HalfVector4s
                ta.bw.Write((ulong)0);
            }

            uint megaTextureOffset = (uint)fs.Position;

            //Write the colors out
            for (int i = 0; i < size * size; i++)
            {
                //Don't know why these are out of order! 
                //but it works this way...
                ta.bw.Write(defaultColor.R);
                ta.bw.Write(defaultColor.G);
                ta.bw.Write(defaultColor.B);
                ta.bw.Write(defaultColor.A);
            }


            fs.Position = megaTextureAddress;
            ta.bw.Write(megaTextureOffset);

            fs.Position = indexAddress;
            ta.bw.Write(indexOffset);

            ta.bw.Flush();

            ta.indexOffset = indexOffset;
            ta.megaTextureOffset = megaTextureOffset;

            return ta;
        }

        /// <summary>
        /// Create a mega texture from a binary reader
        /// </summary>
        public static MegaTexture CreateFromFileStream(FileStream fs, GraphicsDevice device)
        {
          
            MegaTexture ta = new MegaTexture(fs, device);
            BinaryReader br = new BinaryReader(fs);

            //Get the size
            ta.size =br.ReadUInt32();
            ta.megaTextureOffset = br.ReadUInt32();
            ta.indexSize = br.ReadUInt32();
            ta.indexOffset = br.ReadUInt32();
           
    

            return ta;
        }


        BinaryWriter bw;
        BinaryReader br;
        Stream fs;
        GraphicsDevice device;
        uint size;
        uint indexSize;
        uint megaTextureOffset;
        uint indexOffset;

        TextureIndex textureIndex;
        Cache<int, Color[][]> textureCache;



        public uint Size
        {
            get { return size; }
        }


        private MegaTexture(Stream inStream, GraphicsDevice graphicsDevice)
        {
            fs = inStream;

            br = new BinaryReader(fs);
#if !Xbox
            if(inStream.CanWrite)
            bw = new BinaryWriter(fs);
#endif
            textureCache = new Cache<int, Color[][]>(1024 * 1024);
            textureCache.LoadCallback = LoadCallback;
            textureCache.UnloadCallback = UnloadCallback;

            device = graphicsDevice;
        }

        public void Close()
        {
            br.Close();
            bw.Close();
            fs.Close();
        }

        public HalfVector4[] GetIndexData()
        {
            fs.Position = indexOffset;

            HalfVector4[] indexData = new HalfVector4[indexSize * indexSize];

            for (int i = 0; i < indexData.Length; i++)
            {
                indexData[i] = new HalfVector4();
                ulong packedValue = br.ReadUInt64();
                indexData[i].PackedValue = packedValue;
            }
            return indexData;
        }

        /// <summary>
        /// Does the work of creating and saving index data
        /// </summary>
        public void CreateIndex()
        {

            //System.Diagnostics.Debugger.Launch();

            fs.Position = this.indexOffset;

            int scale = (int)(size / indexSize);

            for (int x = 0; x < indexSize; x++)
            {
                for (int y = 0; y < indexSize; y++)
                {
                   
                    byte[] data = GetBytes(new Rectangle(x * scale, y * scale, scale, scale));


                    fs.Position = this.indexOffset + (x + y * indexSize) * sizeof(ulong);
                    HalfVector4 tmep = new HalfVector4(ColorUtilities.GetAverageColor(data).ToVector4());
                    
                    //Write the packed value
                    bw.Write(tmep.PackedValue);

                }
            }

        }

        /// <summary>
        /// Create textures on the hard drive that are viewable. Simply calls
        /// SaveToFile for each section of the atlas texture.
        /// </summary>
        public void CreateTextures(string baseFileName, int textureSize)
        {
            if (textureSize > size) //Add power of two check
                throw new Exception("Texture size must be a power of two and less than size!");

            int numTiles = (int)(size / textureSize);

            for(int x = 0; x < numTiles; x++)
            {
                for(int y = 0; y < numTiles; y++)
                {
                    SaveToFile(baseFileName + x.ToString() + y.ToString(),
                        new Rectangle(x * textureSize, y * textureSize, textureSize, textureSize));
                }
            }
        }

        public TextureIndex GetTextureIndex()
        {
            return new TextureIndex(this, (int)indexSize, device);
        }

        /// <summary>
        /// Save a specified region to file
        /// </summary>
        public void SaveToFile(string fileName, Rectangle area)
        {

            Texture2D tex = GetTexture(area, 1);
            //Save the texture

#if !XBOX
            tex.Save(fileName + ".png", ImageFileFormat.Png);
#endif
            tex.Dispose();


        }

        public Color [][] GetIndex(int index)
        {
            return textureCache.RequestItem(index);
        }

        Color[][] LoadCallback(int index)
        {
            
            
            int scale = (int)(size / indexSize);
            int levels = (int)Math.Log( (double)scale, 2) + 1;
            Color[][] outColors = new Color[levels][];

            Point point = GetPointFromIndex(index);
            byte [] bytes =  GetBytes(new Rectangle(point.X * scale, point.Y * scale, scale, scale));
            Color[] actualColors = ColorUtilities.BytesToColors(bytes);

            outColors[levels - 1] = actualColors;
            Color[] lastColors = actualColors;
            for (int i = levels - 2; i >= 0;  i--)
            {
                outColors[i] = ColorUtilities.Downsample(lastColors, 2);
                lastColors = outColors[i];
            }



            return outColors;
            //int scale = textureIndex.Scale;
  
           
        }

        void UnloadCallback(int index)
        {
        }

        Point GetPointFromIndex(int index)
        {

            int x = (int)(index % indexSize);
            int y = (int)(index / indexSize);

            return new Point(x, y);
        }



        /// <summary>
        /// Gets the raw color values for an area of the mega texture
        /// There is no cache for this so reads are bound to be slow
        /// </summary>
        public byte[] GetBytes(Rectangle area)
        {

            byte[] data = new byte[area.Width * area.Height * DataSize];

            //Loop through each row, reading the data and copying it
            for (int y = 0; y < area.Height; y++)
            {
                lock (fs)
                {
                    //Get the position for this row
                    //fs.Position = HeaderOffset + area.X * DataSize + (area.Y + y) * DataSize * size;
                    fs.Position = megaTextureOffset + area.X * DataSize + (area.Y + y) * DataSize * size;

                    //Read the data for this row
                    byte[] rowdata = br.ReadBytes((int)(area.Width * DataSize));


                    //Copy the row to the data
                    Array.Copy(rowdata, 0, data, y * area.Width * DataSize, area.Width * DataSize);
                }
            }
            return data;
        }

        public Texture2D GetTexture(Rectangle area, int mipLevels)
        {
            byte[] data = GetBytes(area);

            Texture2D tex = new Texture2D(device, area.Width, area.Height, mipLevels, TextureUsage.None, SurfaceFormat.Color);
            tex.SetData<byte>(data);
            return tex;
        }

        /// <summary>
        /// Writes data to the megatexture
        /// </summary>
        public void Write(byte[] data, Rectangle area)
        {
            //Loop through each row of the data, reading the data and copying it
            for (int y = 0; y < area.Height; y++)
            {

                long position = megaTextureOffset + area.X * DataSize + (area.Y + y) * DataSize * size;

                uint xPart = (uint)(area.X * DataSize);

                uint yPart = (uint)((area.Y + y) * DataSize * size);

                long total = megaTextureOffset + xPart + yPart;

                fs.Position = megaTextureOffset + area.X * DataSize + (area.Y + y) * DataSize * size;

                bw.Write(data, (int)(y * area.Width * DataSize), (int)(area.Width * DataSize));

            }

            bw.Flush();
        }
    }
}
