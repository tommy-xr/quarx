using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Graphics.PackedVector;

using Sxe.Library.Utilities;
using Sxe.Library.MultiModel;

namespace Sxe.Library
{

    /// <summary>
    /// Keep data we need that should be cycled 
    /// </summary>
    public class TextureIndexData
    {
        Texture2D indexTex;
        Texture2D lookupTex;

        public Texture2D IndexTexture
        {
            get { return indexTex; }
            set { indexTex = value; }
        }

        public Texture2D LookupTexture
        {
            get { return lookupTex; }
            set { lookupTex = value; }
        }
    }

    /// <summary>
    /// The indices/textures/colors that have been modified
    /// </summary>
    public class ChangedData
    {
        int index;
        public int Index
        {
            get { return index; }
            set { index = value; }
        }
        Texture2D texture;
        public Texture2D Texture
        {
            get { return texture; }
            set { texture = value; }
        }

        Color[] colors;
        public Color[] ColorData
        {
            get { return colors; }
            set { colors = value; }
        }

        FastTextureSpace.FastTextureSpaceNode node;
        //TextureSpaceNode node;
        public FastTextureSpace.FastTextureSpaceNode Node
        {
            get { return node; }
            set { node = value; }
        }
        
    }


    /// <summary>
    /// A data structure that indexes into a mega texture
    /// Responsible for redirecting certain areas to the mega texture
    /// </summary>
    public class TextureIndex
    {
        class IndexData
        {
            public Texture2D texture = null;
            public FastTextureSpace.FastTextureSpaceNode node = null;
            public int refCount;
            public int lastScale = 0;
            public int newScale = 0;
            public bool referenced = false;
            
        }

        /// <summary>
        /// Function for enabling threading
        /// </summary>
        delegate void SetDataFunction(Texture2D tex, int index, Rectangle rect);
        SetDataFunction setFunction;


        //The megatexture associated with this index
        MegaTexture texture;
        int size; //size of the texture index
        int scale; //scale of the texture index to mega texture

        HalfVector4[] baseIndexData;
        HalfVector4[] lastIndexData;


        GraphicsDevice device;
        SpriteBatch spriteBatch; //batch for drawing lookup texture
        RenderTarget2D lookupTarget;
        DepthStencilBuffer depthBuffer; //depth buffer for lookup texture
        Texture2D renderTexture; //the texture used to render scene and extract visibility 
        Texture2D analyzeTexture; //the texture used for analzying scene
        Texture2D lookupTexture; //the final lookup texture

        //TODO: Are we going to use these?
        Texture2D[] writeTextures;

        //The lookupdata and space
        //TextureSpace lookupSpace;
        Color[] lastLookupData;

        FastTextureSpace fastSpace;

        //In-memory listing of data for all indices
        IndexData[] indexData;
        List<int> changedIndexList; //list of all indicis that were changed
        List<int> processedList; //list of indices processed this frame
        Queue<int> clockQueue; //queue of all the indices

        Texture2D blank;

        //Cycle data
        TextureIndexData[] textureData;
        int currentData = 0;

        Thread workThread;
        float lastThreadTime = 0.0f;

        int currentSubSection = 0;
        int subSize;

        public Texture2D Texture
        {
            get { return CurrentData.IndexTexture; ; }
        }
        public Texture2D LookupTexture
        {
            get { return lookupTexture; }
        }
        public Texture2D RenderTexture
        {
            get { return renderTexture; }
        }
        public Texture2D AnalyzeTexture
        {
            set { analyzeTexture = value; }
        }

        protected TextureIndexData CurrentData
        {
            get { return textureData[currentData]; }
        }
        protected TextureIndexData WorkingData
        {
            get { return textureData[1 - currentData]; }
        }


        bool showDetail = false;
        public bool ShowDetail
        {
            get { return showDetail; }
            set { showDetail = value; }
        }

        public bool IsProcessingComplete
        {
            get { 
#if XBOX
                throw new Exception("FIX ME");
                return false;
#else
                return workThread.ThreadState != ThreadState.Running; 
#endif
            }
        }

        TimeSpan indexGetDataTime;
        TimeSpan indexProcessTime;
        TimeSpan writeLookupTime;
        TimeSpan writeIndexTime;
        float totalThreadTime;

        [SxePerformance("Analyze Texture GetData")]
        public double IndexGetDataTime { get { return indexGetDataTime.TotalMilliseconds; } }
        [SxePerformance("Process color data")]
        public double IndexProcessTime { get { return indexProcessTime.TotalMilliseconds; } }
        [SxePerformance("Write lookup data")]
        public double WriteLookupTime { get { return writeLookupTime.TotalMilliseconds; } }
        [SxePerformance("Write index data")]
        public double WriteIndexTime { get { return writeIndexTime.TotalMilliseconds; } }
        [SxePerformance("Total thread time")]
        public float TotalThreadTime { get { return totalThreadTime; } }



        //int lastChanged;
        //public int LastChanged
        //{
        //    get { return lastChanged; }
        //}

        const int MaxChanges = 200;

        public TextureIndex(MegaTexture megaTexture, int indexSize, GraphicsDevice inDevice)
        {
            texture = megaTexture;

            if (indexSize > megaTexture.Size)
                indexSize = (int)megaTexture.Size;

            size = indexSize;
            scale = (int)(megaTexture.Size / size);

            setFunction = this.SetData;

            indexData = new IndexData[size * size];
            for (int i = 0; i < indexData.Length; i++)
                indexData[i] = new IndexData();

            changedIndexList = new List<int>(indexData.Length);
            processedList = new List<int>(indexData.Length);
            clockQueue = new Queue<int>();



            device = inDevice;
            spriteBatch = new SpriteBatch(device);

            baseIndexData = texture.GetIndexData();
            lastIndexData = new HalfVector4[baseIndexData.Length];
            Array.Copy(baseIndexData, lastIndexData, baseIndexData.Length);

   

           
            textureData = new TextureIndexData[2];
            for(int i = 0; i < textureData.Length; i++)
            textureData[i] = new TextureIndexData();

            for (int i = 0; i < 2; i++)
            {
                textureData[i].IndexTexture = new Texture2D(device, size, size, 1, TextureUsage.None, SurfaceFormat.HalfVector4);
                textureData[i].IndexTexture.SetData<HalfVector4>(baseIndexData);
            }

            //We use index size for now but maybe should use something else later
           

            int lookupSize = 1024;
            depthBuffer = new DepthStencilBuffer(device, lookupSize, lookupSize, DepthFormat.Depth16);
            lookupTarget = new RenderTarget2D(device, lookupSize, lookupSize, 1, SurfaceFormat.Color);
            
            for (int i = 0; i < 2; i++)
            {
                //textureData[i].LookupTarget = new RenderTarget2D(device, lookupSize, lookupSize, 1, SurfaceFormat.Color);
                textureData[i].LookupTexture = new Texture2D(device, lookupSize, lookupSize, 1, TextureUsage.Linear, SurfaceFormat.Color);
            }



            writeTextures = new Texture2D[10000];
            for (int i = 0; i < writeTextures.Length; i++)
            {
                writeTextures[i] = new Texture2D(device, scale, scale, 1, TextureUsage.None, SurfaceFormat.Color);

            }



            lastLookupData = new Color[lookupSize * lookupSize];
            fastSpace = new FastTextureSpace(lookupSize, scale, 2);
            
            blank = new Texture2D(device, 1, 1, 0, TextureUsage.None, SurfaceFormat.Color);
            blank.SetData<Color>(new Color[] { Color.White });
            
    
            renderTexture = new Texture2D(device, size, size, 1, TextureUsage.None, SurfaceFormat.Color);
            renderTexture.SetData<Color>(CreateRenderTexture());

            //workThread = new Thread(new ThreadStart(ThreadRun));

            subSize = 200;
            currentSubSection = 0;
        }

        /// <summary>
        /// Called to create a texture used for rendering, such that indices can be examined.
        /// </summary>
        /// <returns></returns>
        Color [] CreateRenderTexture()
        {
            Color[] colors = new Color[size * size];

            int index = 0;
            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    colors[x + y * size] = GetColorFromIndex(index);
                    index++;
                }
            }
            return colors;
        }



        public int[] GetIndicesData(Texture2D analyzeTexture)
        {
            DateTime start = DateTime.Now;

            Color [] colors = new Color[subSize * subSize];
            currentSubSection++;
            int slicesX = analyzeTexture.Width / subSize;
            int slicesY = analyzeTexture.Height / subSize;
            if (currentSubSection >= slicesX * slicesY)
                currentSubSection = 0;

            int x = currentSubSection % (slicesY);
            int y = currentSubSection / slicesX;

            //Random r = new Random();
            //int x = r.Next(0, analyzeTexture.Width - subSize);
            //int y = r.Next(0, analyzeTexture.Height - subSize);

            DateTime getDataStart = DateTime.Now;
            analyzeTexture.GetData<Color>(0, new Rectangle(x * subSize, y*subSize, subSize, subSize), colors, 0, colors.Length);

            int[] indices = new int[colors.Length];
            for (int i = 0; i < indices.Length; i++)
                indices[i] = GetIndexFromColor(colors[i]);
            indexGetDataTime = DateTime.Now - start;
            return indices;


        }

        public ChangedData []  ProcessData(int[] indices)
        {
            
            processedList.Clear();
            StartPass();
            
            for (int i = 0; i < indices.Length; i++)
                ProcessIndex(indices[i]);
            

            DateTime start = DateTime.Now;
            EndPass();
            indexProcessTime = DateTime.Now - start;

           
            ChangedData [] changedData = new ChangedData[changedIndexList.Count];
            for (int i = 0; i < changedData.Length; i++) changedData[i] = new ChangedData();
            for (int i = 0; i < changedData.Length; i++)
            {
              
                changedData[i].Index = changedIndexList[i];
                IndexData data = indexData[changedData[i].Index];
                changedData[i].Node = data.node;
                //changedData[i].Texture = writeTextures[i];
            }

            

            return changedData;
        }

        public ChangedData [] CreateIndexTexture(ChangedData [] changedData)
        {
            DateTime start = DateTime.Now;

            HalfVector4[] data = lastIndexData;
            for (int c = 0; c < changedData.Length; c++)
            {
                int index = changedData[c].Index;
                FastTextureSpace.FastTextureSpaceNode node = changedData[c].Node;
                if(node != null)
                {

                    float xOffset = node.TexturePosition.X;
                    float yOffset = node.TexturePosition.Y;

                    //data[values.Key] = new HalfVector4(xOffset, yOffset, values.Value.TextureSize.X, 1.0f / 255.0f);
                    data[index] = new HalfVector4(xOffset, yOffset, node.TextureSize.X, 1.0f / 255f);
                }
                else
                {
                    data[index] = baseIndexData[index];
                }

            }

            if (!device.IsDisposed)
            {
                this.WorkingData.IndexTexture.SetData<HalfVector4>(data);
            }
            writeIndexTime = DateTime.Now - start;


            return changedData;
        }
        public ChangedData []  CreateLookupTexture(ChangedData [] changedData)
        {
            DateTime start = DateTime.Now;
            if (!device.IsDisposed)
            {
                //device.Textures[1] = null;
            }

            List<IAsyncResult> syncResults = new List<IAsyncResult>();

            Color[] data = lastLookupData;

            for (int c = 0; c < changedData.Length; c++)
            {
                int index = changedData[c].Index;
                FastTextureSpace.FastTextureSpaceNode node = changedData[c].Node;
                if (node != null)
                {
                    //int downsampleAmount = node.Rectangle.Width / scale;


                    Color[][] colorData = GetIndexData(index);

#if !XBOX
                    int scale = (int)Math.Log(node.Rectangle.Width, 2.0);
                    changedData[c].ColorData = colorData[scale];
#endif

                }

            }   

            bool finished = false;
            while (!finished)
            {
                bool temp = true;
                for (int i = 0; i < syncResults.Count; i++)
                {
                    temp = temp & syncResults[i].IsCompleted;
                }
                finished = temp;
            }

            for (int i = 0; i < syncResults.Count; i++)
                setFunction.EndInvoke(syncResults[i]);

            writeLookupTime = start - DateTime.Now;

            return changedData;
        }



        //Called to handle processing of an index render
        public void ProcessData(Color[] colors)
        {
            processedList.Clear();
            StartPass();

            for (int i = 0; i < colors.Length; i++)
            {
                int index = GetIndexFromColor(colors[i]);
                //RequestData(GetIndexFromColor(colors[i]));
                ProcessIndex(index);
 
                
            }

            //DateTime start = DateTime.Now;

            EndPass();

 
        }

        /// <summary>
        /// Called when starting a new pass - resets all the reference counts of the texture index.
        /// </summary>
        void StartPass()
        {
            changedIndexList.Clear();
            for (int i = 0; i < indexData.Length; i++)
            {
                indexData[i].refCount = 0;
                indexData[i].lastScale = indexData[i].newScale;
            }
        }

        void ProcessIndex(int i)
        {

            if (i >= 0 && i < indexData.Length)
            {
                indexData[i].refCount++;

                if (indexData[i].refCount == 1)
                    processedList.Add(i);
            }
  
        }

        /// <summary>
        /// Called when the pass is finished
        /// </summary>
        void EndPass()
        {
            //for (int i = 0; i < indexData.Length; i++)
            //{
            DateTime start = DateTime.Now;
            for(int p = 0; p < processedList.Count; p++)
            {
                int i = processedList[p];

                IndexData data = indexData[i];
                //double sqRoot = Math.Sqrt(indexData[i].refCount * 100);
                double sqRoot = Math.Sqrt(indexData[i].refCount);

#if !XBOX
                double powerOfTwo = Math.Log(sqRoot, 2.0);
                int newScale = (int)powerOfTwo;
                //int newScale = (int)Math.Pow(2, (int)powerOfTwo);
                indexData[i].newScale = newScale;

#endif



                if (indexData[i].newScale > indexData[i].lastScale && indexData[i].newScale >= 2)
                {
                    Promote(i);
                    indexData[i].referenced = true;
                }
                else if (indexData[i].newScale == indexData[i].lastScale)
                {
                    indexData[i].referenced = true;
                }


            }

            TimeSpan span = DateTime.Now - start;

        }

        void Promote(int index)
        {
            IndexData data = indexData[index];
            //If the index is already at max scale, don't do anything
            if (data.node != null)
            {
                if (data.node.Rectangle.Width >= scale)
                    return;
                //Otherwise, give up our current node in favor of a new one
                else
                    //data.node.Occupied = false;
                    data.node.UnOccopy();
            }
            else
                clockQueue.Enqueue(index);

            int newScale = 4;
            if (data.node != null)
                newScale = data.node.Size * 2;

            //lets find a new node, twice the scale
            FastTextureSpace.FastTextureSpaceNode newNode = fastSpace.GetFreeNode(newScale);
            
            //Evict until we make room, baby
            while (newNode == null)
            {
                Evict();
                newNode = fastSpace.GetFreeNode(newScale);
            }

            data.node = newNode;
            data.node.Occupy();

            if (!changedIndexList.Contains(index))
                changedIndexList.Add(index);
        }

        void Demote(int index)
        {
            IndexData data = indexData[index];
            //If we don't have a node anyway, screw it, you can't demote us anymore
            if (data.node == null)
                return;

            if (!changedIndexList.Contains(index))
                changedIndexList.Add(index);

            //Otherwise, find the node
            FastTextureSpace.FastTextureSpaceNode node = data.node;
            //TextureSpaceNode node = data.node;
            int nodeScale = node.Size;
            nodeScale /= 2;
            node.UnOccopy();
            //node.Occupied = false;
            
            //Don't allocate a smaller node at this point...
            if (nodeScale < 4)
            {
 
                data.node = null;
                return;
            }
            //Find a new node, half the size baby
            //data.node = lookupSpace.GetFreeNode(nodeScale);
            //data.node.Occupied = true;
            data.node = fastSpace.GetFreeNode(nodeScale);

            if (data.node == null)
                data.node = fastSpace.GetFreeNode(nodeScale);

            //TODO: Could be source of bugs
            //Why is this osmetiems null? If we are deallocating a larger block,
            //we should be able to get a smaller block...
            //if(data.node != null)
            data.node.Occupy();


        }

        /// <summary>
        /// Removes an item that hasn't been referenced from the texture space
        /// </summary>
        void Evict()
        {
            for (int j = 0; j < 2; j++)
            {
                for (int i = 0; i < clockQueue.Count; i++)
                {
                    //Dequeue an item
                    int itemIndex = clockQueue.Dequeue();
                    IndexData item = indexData[itemIndex];
                    //If the item has not been referenced lately, demote it
                    if (!item.referenced)
                    {
                        //Demote this item
                        Demote(itemIndex);
                        if (item.node != null)
                            clockQueue.Enqueue(itemIndex);
                        return;
                    }
                    else
                    {
                        item.referenced = false;
                        clockQueue.Enqueue(itemIndex);
                    }
                }
            }
        }



        void SetData(Texture2D tex, int index, Rectangle rect)
        {
#if !XBOX
            Color[][] data = GetIndexData(index);


            int scale = (int)Math.Log(rect.Width, 2.0);

            //data = GetColorFromScale(rect.Width);
            //data = ColorUtilities.Downsample(data, scale / rect.Width);
            
            tex.SetData<Color>(0, rect, data[scale], 0, rect.Width * rect.Width, SetDataOptions.None);
#endif
        }

        public void WriteLookupTexture(ChangedData [] changedData)
        {
            DepthStencilBuffer defaultBuffer = device.DepthStencilBuffer;
            device.DepthStencilBuffer = this.depthBuffer;

            device.SetRenderTarget(0, lookupTarget);
            device.Clear(ClearOptions.DepthBuffer | ClearOptions.Target, Color.White, 1.0f, 0);
            spriteBatch.Begin(SpriteBlendMode.None, SpriteSortMode.Immediate, SaveStateMode.None);

            if (lookupTexture != null)
                spriteBatch.Draw(lookupTexture, new Rectangle(0, 0, lookupTarget.Width, lookupTarget.Height), Color.White);

            //for (int index = 0; index < indexData.Length; index++)
            //{
            for (int i = 0; i < changedData.Length; i++)
            {
                FastTextureSpace.FastTextureSpaceNode node = changedData[i].Node;
                Texture2D tex = changedData[i].Texture;

                if (node != null)
                //Draw this out
                {
                    //spriteBatch.Draw(WorkingData.LookupTexture,
                    //    node.Rectangle, node.Rectangle, Color.White);
                    //spriteBatch.Draw(tex,
                    //    node.Rectangle,
                    //    new Rectangle(0, 0, node.Rectangle.Width, node.Rectangle.Height),
                    //     Color.White);

                    for (int x = 0; x < node.Rectangle.Width; x++)
                    {
                        for (int y = 0; y < node.Rectangle.Height; y++)
                        {
                            Color color = changedData[i].ColorData[x + node.Rectangle.Width * y];

                            if(showDetail)
                            color = GetColorFromScale(node.Rectangle.Width);

                            spriteBatch.Draw(blank,
                                new Rectangle(node.Rectangle.X + x, node.Rectangle.Y + y, 1, 1),
                                color);
                        }
                    }
                }
                //spriteBatch.Draw(writeTextures[i],
                //     data.node.Rectangle,
                //    new Rectangle(0, 0, data.node.Rectangle.Width, data.node.Rectangle.Height),
                //    Color.White);
                //spriteBatch.Draw(writeTextures[i], new Rectangle(0, 0, lookupTarget.Width, lookupTarget.Height), Color.White);

            }

            spriteBatch.End();
            device.SetRenderTarget(0, null);

 
            lookupTexture = lookupTarget.GetTexture();

            device.DepthStencilBuffer = defaultBuffer;


            device.Textures[0] = null;
            device.Textures[1] = null;
            device.Textures[2] = null;
        }





        public void Swap()
        {
            if (currentData == 0)
                currentData = 1;
            else
                currentData = 0;
        }

        Point GetPointFromIndex(int index)
        {

            int x = index % size;
            int y = index / size;

            return new Point(x, y);
        }

        public void TestSave()
        {
#if !XBOX
            for (int i = 0; i < writeTextures.Length; i++)
                writeTextures[i].Save("writetexture" + i.ToString() + ".png", ImageFileFormat.Png);

            WorkingData.LookupTexture.Save("working_lookup.png", ImageFileFormat.Png);
            CurrentData.LookupTexture.Save("current_lookup.png", ImageFileFormat.Png);
#endif
        }

        Color[][] GetIndexData(int index)
        {
            return texture.GetIndex(index);
            //Point p = GetPointFromIndex(index);
            //return texture.GetBytes(new Rectangle(p.X * scale, p.Y * scale, scale, scale));
        }

        int GetIndexFromColor(Color c)
        {
            return c.R + c.G * 256 + c.B * 256 * 256 + c.A * 256 * 256 * 256;
        }

        Color GetColorFromIndex(int index)
        {
            int alpha = index / (256 * 256 * 256);
            index = index % (256 * 256 * 256);
            int blue = index / (256 * 256);
            index = index % (256 * 256);
            int green = index / (256);
            index = index % 256;
            int red = index;

            return new Color((byte)red, (byte)green, (byte)blue, (byte)alpha);

        }


        Color GetColorFromScale(int scale)
        {
            Color color = Color.White;
            switch (scale)
            {
                case 1:
                    color = Color.Red;
                    break;
                case 2:
                    color = Color.Blue;
                    break;
                case 4:
                    color = Color.Yellow;
                    break;
                case 8:
                    color = Color.Green;
                    break;
                case 16:
                    color = Color.Black;
                    break;
                case 32:
                    color = Color.Orange;
                    break;

            }



            return color;
        }
        //Color[] GetColorFromScale(int scale)
        //{
        //    Color color = Color.White;
        //    switch (scale)
        //    {
        //        case 1:
        //            color = Color.Red;
        //            break;
        //        case 2:
        //            color = Color.Blue;
        //            break;
        //        case 4:
        //            color = Color.Yellow;
        //            break;
        //        case 8:
        //            color = Color.Green;
        //            break;
        //        case 16:
        //            color = Color.Black;
        //            break;

        //    }

        //    Color [] colors = new Color[scale * scale];
        //    for (int i = 0; i < colors.Length; i++)
        //        colors[i] = color;
        //    return colors;
        //}

    }

}



