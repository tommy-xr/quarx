using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

using Sxe.Library.Utilities;



namespace Sxe.Library.MultiModel
{
    public class MultiModel
    {
        Model model;
        TextureIndex index;

        MegaTexture ta;

        Effect megaEffect;
        Effect preEffect;

        RenderTarget2D preTarget1;
        //RenderTarget2D preTarget2;
        //RenderTarget2D current;
        int frames = 0;

        Texture2D currentTexture;
        Texture2D lastTexture;
        Texture2D lightmap;
        //RenderTarget2D CurrentTarget
        //{
        //    get { return current; }
        //    set { current = value; }
        //}
        //RenderTarget2D LastTarget
        //{
        //    get
        //    {
        //        if (CurrentTarget == preTarget1)
        //            return preTarget2;
        //        else
        //            return preTarget1;
        //    }


        //}

        public TextureIndex TextureIndex
        {
            get { return index; }
        }

        TimeSpan timeTaken = TimeSpan.FromSeconds(0.5);
        [SxePerformance("Thread Time Spread")]
        public double TimeTaken
        {
            get {
                
                return timeTaken.TotalMilliseconds; }
        }

        Texture temp;
        GraphicsDevice device;
        public MultiModel(ContentReader cr)
        {
            megaEffect = cr.ContentManager.Load<Effect>("MegaTexture");
            preEffect = cr.ContentManager.Load<Effect>("PreMegaTexture");

            model = cr.ReadObject<Model>();


            temp = cr.ContentManager.Load<Texture2D>("NULL");
            //texture = cr.ReadObject<Texture2D>();

            device = ((IGraphicsDeviceService)cr.ContentManager.ServiceProvider.GetService(typeof(IGraphicsDeviceService))).GraphicsDevice;

            //texture.Save("supertest.png", ImageFileFormat.Png);
            //MegaTexture ta = MegaTexture.Create(new FileStream("test.atlas", FileMode.Create, FileAccess.ReadWrite),
            //    1024, new Color(64, 0, 128, 255), device);
            DateTime start = DateTime.Now;
            string fileName = cr.ReadString();

            FileStream contentStream = cr.BaseStream as FileStream;
            string path = Path.GetDirectoryName(contentStream.Name);

            FileStream fs = new FileStream(path + Path.DirectorySeparatorChar + fileName, FileMode.Open, FileAccess.ReadWrite);
            ta = MegaTexture.CreateFromFileStream(fs, device);
            DateTime end = DateTime.Now;
            TimeSpan span = end - start;
            //ta = MegaTexture.Load(FileSystem.OpenFile("test.atlas", FileMode.Open, FileAccess.ReadWrite), device);
            //ta = MegaTexture.CreateFromReader(cr, 
           
            //ta.SaveToFile("atlas1", new Rectangle(0, 0, 512, 512));
            //ta.SaveToFile("atlas_whole", new Rectangle(0, 0, 1024, 1024));
            lightmap = cr.ContentManager.Load<Texture2D>("lightmap");


            preTarget1 = new RenderTarget2D(device, 800, 600, 1, SurfaceFormat.Color);
            preTarget1.Name = "PreTarget1";
            //preTarget2 = new RenderTarget2D(device, 512, 512, 1, SurfaceFormat.Color);
            //preTarget2.Name = "PreTarget2";
            //CurrentTarget = preTarget1;

            index = ta.GetTextureIndex();
            

#if !XBOX
            index.Texture.Save("texture_index.png", ImageFileFormat.Png);
            //index.Texture.Save("texture_index.png", ImageFileFormat.Png);
#endif

            //for (int i = 0; i < 64 * 64; i++)
            //    index.RequestData(i);
                //index.RequestDataTest();

            //index.WriteLookupTexture();
#if !XBOX
            //index.LookupTexture.Save("lookup_texture.png", ImageFileFormat.Png);
#endif
            //index.WriteIndexTexture();

#if !XBOX
            //index.Texture.Save("texture_index_after.png", ImageFileFormat.Png);
#endif

     
            //texture = cr.ContentManager.Load<Texture2D>("megatest_index");
        }

        void TestVerifyIndexLookupTexture(Texture2D index, Texture2D lookup)
        {

            Color[] indexData = new Color[index.Width * index.Height];
            Color[] lookupData = new Color[lookup.Width * lookup.Height];

            index.GetData<Color>(indexData);
            lookup.GetData<Color>(lookupData);

            //loop through each color in index data
            for (int i = 0; i < indexData.Length; i++)
            {
                //Check if we need to do a lookup
                if (indexData[i].A == 1)
                {
                    float xOffset = ((float)(indexData[i].R / 256.0f) * 2.0f) - 1.0f;
                    float yOffset = ((float)(indexData[i].G / 256.0f) * 2.0f) - 1.0f;
                    float size = ((float)(indexData[i].B / 256.0f));
                }
            }
        }

        //public void PreDraw(Matrix view, Matrix projection)
        //{

        //    Matrix newProjection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4,
        //        1.0f, 1.0f, 1000.0f);
        //    Model m = model;

        //    device.SetRenderTarget(0, preTarget1);
        //    device.RenderState.AlphaBlendEnable = false;
        //    device.RenderState.AlphaTestEnable = false;

        //    foreach (ModelMesh mesh in m.Meshes)
        //    {
        //        foreach (BasicEffect effect in mesh.Effects)
        //        {
        //            //effect.EnableDefaultLighting();
        //            effect.TextureEnabled = true;
        //            effect.Texture = index.RenderTexture;
        //            effect.View = view;
        //            effect.Projection = newProjection;
        //            effect.World = Matrix.Identity;
        //        }
        //        mesh.Draw();
        //    }
        //    device.SetRenderTarget(0, null);

        //    lastTexture = currentTexture;
        //    currentTexture = preTarget1.GetTexture();

        //    if (lastTexture != null)
        //    {
        //        //If we don't have a thread running
        //        if (index.IsProcessingComplete)
        //        {

        //            index.WriteLookupTexture();
        //            index.Swap();

        //            index.Start(lastTexture);
        //        }
        //    }


            

        //}

        delegate int[] GetDataDelegate(Texture2D texture);
        delegate ChangedData[] GetChangedData(int[] indices);
        delegate ChangedData[] WriteDelegate(ChangedData[] data);

        GetDataDelegate getDataFunction;
        GetChangedData processDataFunction;
        WriteDelegate writeIndexFunction;
        WriteDelegate writeLookupFunction;

        IAsyncResult getDataResult;
        IAsyncResult processResult;
        IAsyncResult writeIndexResult;
        IAsyncResult writeLookupResult;

        int[] currentIndexData;
        ChangedData[] currentChangedData;

        DateTime lastTime;

        void PreDraw(Matrix view, Matrix projection)
        {
            //Set up functions
            if(getDataFunction == null)
                getDataFunction = index.GetIndicesData;

            if(processDataFunction == null)
                processDataFunction = index.ProcessData;

            if(writeIndexFunction == null)
                writeIndexFunction = index.CreateIndexTexture;

            if(writeLookupFunction == null)
                writeLookupFunction = index.CreateLookupTexture;

            if (getDataResult == null)
            {
                Texture2D analyzeTexture = DrawScene(view, projection);
                if(analyzeTexture != null)
                getDataResult = getDataFunction.BeginInvoke(analyzeTexture, null, null);
            }
            else if (getDataResult.IsCompleted && processResult == null)
            {
                 currentIndexData =  getDataFunction.EndInvoke(getDataResult);

                     processResult = processDataFunction.BeginInvoke(currentIndexData, null, null);
                     getDataResult = null;
            }

            if (processResult != null)
            {
                if (processResult.IsCompleted && writeIndexResult == null && writeLookupResult == null)
                {
                    currentChangedData = processDataFunction.EndInvoke(processResult);
                    processResult = null;
                    writeIndexResult = writeIndexFunction.BeginInvoke(currentChangedData, null, null);
                    writeLookupResult = writeLookupFunction.BeginInvoke(currentChangedData, null, null);
                }
            }

            if (writeIndexResult != null && writeLookupResult != null)
            {
                if (writeIndexResult.IsCompleted && writeLookupResult.IsCompleted)
                {
                    //Do the swap and everything
                    ChangedData[] data;
                    data = writeIndexFunction.EndInvoke(writeIndexResult);
                    writeLookupFunction.EndInvoke(writeLookupResult);

                    if (lastTime != null)
                        timeTaken = DateTime.Now - lastTime;
                    
                    lastTime = DateTime.Now; 

                    index.WriteLookupTexture(data);
                    index.Swap();

                    writeLookupResult = null;
                    writeIndexResult = null;
                }

            }


        }

        Texture2D DrawScene(Matrix view, Matrix projection)
        {
            Matrix newProjection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4,
                1.0f, 1.0f, 1000.0f);
            newProjection = projection;

            Random r = new Random();

            //Rotate the view vector
            Vector3 forward = view.Forward;
            Vector3 up = view.Up;
            Vector3 right = view.Right;

            //Rotate the forward around the up
            double rotYaw = 0.5 * (r.NextDouble() - 0.5);
            double rotPitch = 0.5 * (r.NextDouble() - 0.5);
            double xOffset = 1.0 * (r.NextDouble() - 0.5);
            double yOffset = 1.0 * (r.NextDouble() - 0.5);
            double zOffset = 1.0 * (r.NextDouble() - 0.5);

            Matrix rotYawMatrix = Matrix.CreateFromAxisAngle(up, (float)rotYaw);
            forward = Vector3.Transform(forward, rotYawMatrix);

            right = Vector3.Cross(forward, up);

            Matrix rotPitchMatrix = Matrix.CreateFromAxisAngle(right, (float)rotPitch);
            forward = Vector3.Transform(forward, rotPitchMatrix);

            up = Vector3.Cross(right, forward);

            view.Forward = forward;
            view.Right = right;
            view.Up = up;


            view.Translation += up * (float)yOffset;
            view.Translation += right * (float)zOffset;
            view.Translation += forward * (float)xOffset;



            Model m = model;
            device.SetRenderTarget(0, preTarget1);
            device.RenderState.AlphaBlendEnable = false;
            device.RenderState.AlphaTestEnable = false;


            preEffect.Parameters["renderTexture"].SetValue(index.RenderTexture);
            preEffect.Parameters["View"].SetValue(view);
            preEffect.Parameters["Projection"].SetValue(projection);
            preEffect.Parameters["World"].SetValue(Matrix.Identity);
            preEffect.Begin();
            foreach (ModelMesh mesh in m.Meshes)
            {
                //foreach (BasicEffect effect in mesh.Effects)
                //{
                //    //effect.EnableDefaultLighting();
                //    effect.TextureEnabled = true;
                //    effect.Texture = index.Texture;
                //    effect.View = view;
                //    effect.Projection = projection;
                //    effect.World = Matrix.Identity ;
                // }

                preEffect.Techniques[0].Passes[0].Begin();
                preEffect.GraphicsDevice.Indices = mesh.IndexBuffer;

                foreach (ModelMeshPart part in mesh.MeshParts)
                {
                    preEffect.GraphicsDevice.Vertices[0].SetSource(mesh.VertexBuffer, part.StreamOffset, part.VertexStride);
                    preEffect.GraphicsDevice.VertexDeclaration = part.VertexDeclaration;
                    preEffect.GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, part.BaseVertex, 0, part.NumVertices, part.StartIndex, part.PrimitiveCount);
                }
                preEffect.Techniques[0].Passes[0].End();
            }
            preEffect.End();


            //foreach (ModelMesh mesh in m.Meshes)
            //{
            //    foreach (BasicEffect effect in mesh.Effects)
            //    {
            //        //effect.EnableDefaultLighting();
            //        effect.TextureEnabled = true;
            //        effect.Texture = index.RenderTexture;
            //        effect.View = view;
            //        effect.Projection = newProjection;
            //        effect.World = Matrix.Identity;
            //    }
            //    mesh.Draw();
            //}
            device.SetRenderTarget(0, null);

            lastTexture = currentTexture;
            currentTexture = preTarget1.GetTexture();
            return lastTexture;
        }


        public void Draw(Matrix view, Matrix projection)
        {

            if (Keyboard.GetState().IsKeyDown(Keys.G))
            {
                index.ShowDetail = !index.ShowDetail;
            }

            PreDraw(view, projection );



            Model m = model;

            megaEffect.GraphicsDevice.RenderState.AlphaBlendEnable = false;
            megaEffect.GraphicsDevice.RenderState.DepthBufferEnable = true;
            //megaEffect.GraphicsDevice.RenderState.FillMode = FillMode.WireFrame;

            megaEffect.GraphicsDevice.Clear(ClearOptions.DepthBuffer | ClearOptions.Target, Color.Violet, 1.0f,
                0);

            megaEffect.Parameters["indexTexture"].SetValue(index.Texture);
            megaEffect.Parameters["lightMapTexture"].SetValue(lightmap);
            megaEffect.Parameters["lookupTexture"].SetValue(index.LookupTexture);
            megaEffect.Parameters["View"].SetValue(view);
            megaEffect.Parameters["Projection"].SetValue(projection);
            megaEffect.Parameters["World"].SetValue(Matrix.Identity);
            megaEffect.Begin();
            foreach (ModelMesh mesh in m.Meshes)
            {
                //foreach (BasicEffect effect in mesh.Effects)
                //{
                //    //effect.EnableDefaultLighting();
                //    effect.TextureEnabled = true;
                //    effect.Texture = index.Texture;
                //    effect.View = view;
                //    effect.Projection = projection;
                //    effect.World = Matrix.Identity ;
                // }

                megaEffect.Techniques[0].Passes[0].Begin();
                megaEffect.GraphicsDevice.Indices = mesh.IndexBuffer;

                foreach (ModelMeshPart part in mesh.MeshParts)
                {
                    megaEffect.GraphicsDevice.Vertices[0].SetSource(mesh.VertexBuffer, part.StreamOffset, part.VertexStride);
                    megaEffect.GraphicsDevice.VertexDeclaration = part.VertexDeclaration;
                    megaEffect.GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, part.BaseVertex, 0, part.NumVertices, part.StartIndex, part.PrimitiveCount);
                }
                megaEffect.Techniques[0].Passes[0].End();
            }
            megaEffect.End();

        }

    }
}
