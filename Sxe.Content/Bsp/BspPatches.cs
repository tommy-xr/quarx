//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace Sxe.Content.Bsp
//{

//    public class BSPPatch
//    {

//        public int numBiPatches;

//        public Q3BiquadraticPatch[] patches;
//    }

//    public class BSPBiquadraticPatch
//    {

//        Q3VertexBSP[] vertices;
//        short[] indices;
//        int trianglesPerRow;

//        GraphicsDevice device;
//        VertexBuffer vb;
//        IndexBuffer ib;

//        int level;

//        public Q3VertexBSP[] controls;

//        public Q3BiquadraticPatch(GraphicsDevice inDevice)
//        {

//            this.controls = new Q3VertexBSP[9];
//            this.device = inDevice;
//        }

//        public void tesellate(int inLevel)
//        {
//            this.level = inLevel;
//            int L1 = level + 1; //number of vertices = number of edges + 1

//            vertices = new Q3VertexBSP[L1 * L1];

//            trianglesPerRow = L1 * 2;

//            //Compute the vertices
//            Q3VertexBSP[] temp = new Q3VertexBSP[3];

//            for (int i = 0; i < L1; i++)
//            {
//                float a0 = (float)i / level;
//                float b0 = 1.0f - a0;

//                vertices[i] =
//                    controls[0].multiply(b0 * b0) +
//                    controls[3].multiply(2 * b0 * a0) +
//                    controls[6].multiply(a0 * a0);
//            }

//            for (int i = 0; i < L1; i++)
//            {
//                float a1 = (float)i / level;
//                float b1 = 1.0f - a1;

//                for (int j = 0; j < 3; j++)
//                {
//                    int k = 3 * j;
//                    temp[j] =
//                        controls[k + 0].multiply(b1 * b1) +
//                        controls[k + 1].multiply(2 * b1 * a1) +
//                        controls[k + 2].multiply(a1 * a1);

//                }

//                for (int j = 0; j < L1; j++)
//                {
//                    float a2 = (float)j / level;
//                    float b2 = 1.0f - a2;

//                    vertices[i * L1 + j] =
//                        temp[0].multiply(b2 * b2) +
//                        temp[1].multiply(2 * b2 * a2) +
//                        temp[2].multiply(a2 * a2);

//                }

//            }

//            //Now, compute indices
//            indices = new short[level * (level + 1) * 2];
//            for (int row = 0; row < level; ++row)
//            {
//                for (int col = 0; col <= level; ++col)
//                {
//                    indices[(row * (L1) + col) * 2 + 1] = (short)(row * L1 + col);
//                    indices[(row * (L1) + col) * 2] = (short)((row + 1) * L1 + col);
//                }
//            }

//            //Create the vertex buffer
//            vb = new VertexBuffer(this.device, vertices.Length * Q3VertexBSP.SizeInBytes, BufferUsage.WriteOnly);
//            vb.SetData<Q3VertexBSP>(vertices);

//            ib = new IndexBuffer(this.device, indices.Length * sizeof(short), BufferUsage.WriteOnly, IndexElementSize.SixteenBits);
//            ib.SetData<short>(indices);


//        }

//        public void Draw()
//        {

//            /*for (int i = 0; i < vertices.Length; i++)
//                RenderManager.RenderSphere(vertices[i].Position, 1.0f, Color.Black);
//            */
//            //for (int i = 0; i < indices.Length; i++)
//            //   RenderManager.RenderSphere(vertices[indices[i]].Position, 1.0f, Color.PeachPuff);

//            //for (int row = 0; row < level; row++)
//            //{
//            //    //for (int i = 0; i < 2 * (level + 1); i++)
//            //    //{
//            //        RenderManager.RenderSphere(vertices[ indices[row  * 2 * (level + 1) + 0]].Position, 2.0f, Color.DarkBlue);
//            //        RenderManager.RenderSphere(vertices[ indices[row * 2 * (level + 1) + (2 * (level + 1)) - 1]].Position, 2.0f, Color.Green);
//            //    //}
//            //}

//            //for (int i = 0; i < controls.Length; i++)
//            //    RenderManager.RenderSphere(controls[i].Position, 1.0f, Color.Red);

//            device.Vertices[0].SetSource(vb, 0, Q3VertexBSP.SizeInBytes);
//            //device.VertexDeclaration = new VertexDeclaration(device, Q3VertexBSP.VertexElements);
//            device.Indices = ib;
//            for (int row = 0; row < level; row++)
//            {
//                int numVertices = 2 * (level + 1) - 1;

//                device.DrawIndexedPrimitives(PrimitiveType.TriangleStrip, 0, 0, vertices.Length, row * 2 * (level + 1), numVertices - 1);
//            }
//        }


//    }
//}
