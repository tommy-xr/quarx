using System;
using System.Collections.Generic;
using System.Text;

using Sxe.Library.Bsp;
using Sxe.Library.Services;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Sxe.Library.Bsp
{
    public class BspLevel : IDisposable
    {
                //We'll keep a copy of all the data in the object model
        BspDom DOM;

        BspFaceGroup [] facegroups;
        VertexBuffer vb;
        VertexDeclaration decl;
        IndexBuffer ib;
        IServiceProvider Services;
        GraphicsDevice device;

        Effect testEffect;
        int testNumVerts;
        int testNumIndices;

        Texture2D[] textures;
        Texture2D[] lightmaps;

        public BspLevel(ContentReader cr, ContentManager content)
        {

            this.Services = content.ServiceProvider;
            //Read facegroups
            int numFaceGroups = cr.ReadInt32();
            facegroups = new BspFaceGroup[numFaceGroups];
            for(int i = 0; i < numFaceGroups; i++)
            {
                facegroups[i] = new BspFaceGroup();
                facegroups[i].Read(cr);
            }


            int numVerts = cr.ReadInt32();
            testNumVerts = numVerts;
            BspVertex[] verts = new BspVertex[numVerts];
            for (int i = 0; i < numVerts; i++)
            {
                verts[i] = ReadVertex(cr);
            }

            int numIndices = cr.ReadInt32();
            testNumIndices = numIndices;
            int[] indices = new int[numIndices];
            for (int i = 0; i < numIndices; i++)
            {
                indices[i] = cr.ReadInt32();
            }

            int numTextures = cr.ReadInt32();
            textures = new Texture2D[numTextures];
            for (int i = 0; i < numTextures; i++)
                textures[i] = cr.ReadExternalReference<Texture2D>();


            //Finally, read the DOM
            DOM = BspDom.Read(cr);

            IGraphicsDeviceService Graphics = (IGraphicsDeviceService)Services.GetService(typeof(IGraphicsDeviceService));
            device = Graphics.GraphicsDevice;

            decl = new VertexDeclaration(device, BspVertex.VertexElements);

            //Load up the vertexbuffer and indexbuffer
            vb = new VertexBuffer(device, numVerts * BspVertex.SizeInBytes, BufferUsage.None);
            vb.SetData<BspVertex>(verts);

            ib = new IndexBuffer(device, numIndices * sizeof(int), BufferUsage.None, IndexElementSize.ThirtyTwoBits);
            ib.SetData<int>(indices);

            testEffect = content.Load<Effect>("LevelEffect");

            lightmaps = CreateLightmaps();
        }

        Texture2D [] CreateLightmaps()
        {
            Texture2D [] returnMaps = new Texture2D[DOM.LightMaps.Count];
            for (int i = 0; i < DOM.LightMaps.Count; i++)
            {
                int size = DOM.LightMaps[i].lightMapSize;
                returnMaps[i] = new Texture2D(device, size, size, 1, TextureUsage.None, SurfaceFormat.Color);
                returnMaps[i].SetData<Color>(DOM.LightMaps[i].map);
            }

            return returnMaps;
        }

        public void Render(Matrix view, Matrix projection)
        {
            //device.Clear(Color.Yellow);
            device.VertexDeclaration = decl;

            device.Vertices[0].SetSource(vb, 0, BspVertex.SizeInBytes);
            device.Indices = ib;

            testEffect.Parameters["World"].SetValue(Matrix.Identity);
            testEffect.Parameters["View"].SetValue(view);
            testEffect.Parameters["Projection"].SetValue(projection);
            testEffect.Parameters["WorldViewProjection"].SetValue(view * projection);

            

            testEffect.Begin();

            foreach (BspFaceGroup f in facegroups)
            {
                RenderFaceGroup(f.index);
            }

            //foreach (EffectPass p in testEffect.CurrentTechnique.Passes)
            //{
            //    p.Begin();
            //    device.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, testNumVerts, 0, testNumIndices);
            //    p.End();
            //}
            testEffect.End();
        }

        void RenderFaceGroup(int facegroupIndex)
        {
            BspFaceGroup faces = facegroups[facegroupIndex];
            testEffect.Parameters["g_baseTexture"].SetValue(textures[faces.texIndex]);
            testEffect.Parameters["g_lightMap"].SetValue(lightmaps[faces.lmIndex]);
            testEffect.CommitChanges();
            foreach (EffectPass p in testEffect.CurrentTechnique.Passes)
            {
                p.Begin();
                device.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, faces.startVertex, faces.numberOfIndexes, faces.startOffset, faces.numberOfIndexes / 3);
                p.End();
            }
        }

        public void DebugPrint()
        {
            ILogService log = (ILogService)Services.GetService(typeof(ILogService));
            DOM.DebugPrint(log);
        }

        /// <summary>
        /// Gets the leaf a certain position is located in
        /// </summary>
        /// <param name="camPos"></param>
        /// <returns></returns>
        protected int FindLeaf(Vector3 pos)
        {
            int index = 0;
            while (index >= 0)
            {
                BspNode node = DOM.Nodes[index];
                BspPlane plane = DOM.Planes[node.planeIndex];

                //Get distance from point to plane
                float distance = Vector3.Dot(plane.normal, pos) - plane.distance;

                if (distance >= 0)
                {
                    index = node.children[0];
                }
                else
                {
                    index = node.children[1];
                }
            }

            return -index - 1;
        }

        /// <summary>
        /// Checks if one cluster is visible from another cluster
        /// </summary>
        protected bool IsClusterVisible(int visCluster, int testCluster)
        {

            if (DOM.Visibility == null)
                return true;

            if ((DOM.Visibility.visibilityData == null) || (visCluster < 0) || (testCluster < 0))
                return true;

            int i = (visCluster * DOM.Visibility.sizeOfVectors) + (testCluster >> 3);
            byte visset = DOM.Visibility.visibilityData[i];

            return ((visset & (1 << (testCluster & 7))) != 0);
        }

        static BspVertex ReadVertex(ContentReader cr)
        {
            BspVertex vert = new BspVertex();
            vert.Position = cr.ReadVector3();
            vert.Normal = cr.ReadVector3();
            vert.Binormal = cr.ReadVector3();
            vert.Tangent = cr.ReadVector3();
            vert.DiffuseTexCoords = cr.ReadVector2();
            vert.LightTexCoords = cr.ReadVector2();

            return vert;

        }

        public virtual void Dispose()
        {
            if (vb != null)
                vb.Dispose();

            if (ib != null)
                ib.Dispose();

            if (testEffect != null)
                testEffect.Dispose();

            if (decl != null)
                decl.Dispose();
        }
    }
}
