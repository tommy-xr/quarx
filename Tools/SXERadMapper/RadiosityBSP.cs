using System;
using System.Collections.Generic;
using System.Text;

using Engine.Source;
using Engine.Source.Client.BSP;
using Engine.Source.Utilities;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SXERadMapper
{
    public class RadiosityBSP : Q3Bsp
    {
        Log log; //log for outputting data

        PatchMap [] patchMaps;
        RadiositySettings Settings = new RadiositySettings();

        //Create a list of objects that receive and take light
        List<ILightEmitter> listEmitters;
        List<ILightReceiver> listReceivers;

        RadiosityEntityManager entManager;

        IServiceProvider services;
        EmitterComparer emitterComparer = new EmitterComparer(); //used to sort the emitters

        public RadiosityBSP()
            : base()
        {
            listEmitters = new List<ILightEmitter>();
            listReceivers = new List<ILightReceiver>();
            services = new ServiceContainer();
        }

        public override void Load(string filename, Microsoft.Xna.Framework.Graphics.GraphicsDevice inDevice, int inTesselation)
        {
            log = new Log("radiosity.txt");

            base.Load(filename, inDevice, services, inTesselation);

            //Create and initialize the lights/radiosity entities we care about
            entManager = new RadiosityEntityManager(this.Services, null);
            entManager.Initialize();
            entManager.ReadCreationString(this.entData);

            AddLights();
            CreatePatches();
            for (int i = 0; i < patchMaps.Length; i++)
            {
                Texture2D tex = new Texture2D(inDevice, Settings.PatchMapSize, Settings.PatchMapSize, 1, TextureUsage.None, SurfaceFormat.Color);
                tex.SetData<Color>(patchMaps[i].TestColorOutput());
                tex.Save("test " + i.ToString() + ".png", ImageFileFormat.Png);

            }

            CalculateRadiosity();

            
        }

        void WriteLightmaps()
        {
            for (int i = 0; i < patchMaps.Length; i++)
            {
                Texture2D tex = new Texture2D(this.device, Settings.PatchMapSize, Settings.PatchMapSize, 1, TextureUsage.None, SurfaceFormat.Color);
                tex.SetData<Color>(patchMaps[i].RawColorOutput());
                tex.Save("test " + i.ToString() + ".png", ImageFileFormat.Png);

            }
        }


        /// <summary>
        /// Adds in all the lights present in the level as light emitters
        /// </summary>
        void AddLights()
        {
            int numLights = entManager.GetNumberOfEntitiesByType("light");
            for (int i = 0; i < numLights; i++)
            {
                LightEntity le = (LightEntity)entManager.GetEntityByType(i, "light");
                listEmitters.Add(le);
                log.Print(String.Format("[Add Lights] Adding light {0} at position {1}, {2}, {3}.",i, le.position.X, le.position.Y, le.position.Z));

            }
        }

        /// <summary>
        /// Traces a line from a start to an end point, and returns true if it makes it
        /// A face to ignore can be passed in (this should be used for the that is emitting)
        /// Returns false if the ray is occluded, true otherwise
        /// </summary>
        public bool RayTrace(Vector3 start, Vector3 end, int faceIgnoreIndex)
        {

            Vector3 dir = end - start;
            
            //The target distance the ray must travel
            float targetDistance = dir.Length();
            const float threshold = 0.1f;

            dir.Normalize();

            //First, lets find the leaf the ray is starting in
            int leaf = findLeaf(start);


            //Loop through leafs, only test the ones that are visible
            for (int i = 0; i < leafs.Length; i++)
            {
               

                //Only test if this leaf is visible
                //if(true)
                if (isClusterVisible(leafs[i].cluster, leafs[leaf].cluster))
                {
                    //Loop through all faces in leaf, test if the ray intersects
                    for (int j = 0; j < leafs[i].n_leaffaces; j++)
                    {
                        int faceIndex = leaffaces[j + leafs[i].leafface].face;

                        if(faceIndex == faceIgnoreIndex)
                            continue;

                        Engine.Source.Q3Faces face = faces[faceIndex];

                        //Now, loop through all the vertices in the face, and check for intersection
                        for (int k = 0; k < face.n_meshverts / 3; k++)
                        {
                            Q3Vertexes vert1 = vertexes[face.vertex + meshverts[face.meshvert + k * 3 + 0].offset];
                            Q3Vertexes vert2 = vertexes[face.vertex + meshverts[face.meshvert + k * 3 + 1].offset];
                            Q3Vertexes vert3 = vertexes[face.vertex + meshverts[face.meshvert + k * 3 + 2].offset];

                            float u, v, t;
                            bool result = Utilities.RayTriangleIntersect(start, dir, vert1.position, vert2.position, vert3.position,
                                out t, out u, out v);

                            //The ray hit something
                            if (result)
                            {
                                if (t < targetDistance - threshold)
                                {
                                    return false;
                                }
                            }

                        }
                    }
                }
            }

            return true;
        }



        /// <summary>
        /// This function iterates through each face and each triangle in the face,
        /// and creates patches for each luxel.
        /// </summary>
        void CreatePatches()
        {
            //Create an array of patch maps
            patchMaps = new PatchMap[this.lightmaps.Length]; //we need same number of patchmaps as there are lightmaps
            //Initialize each patchmap
            for (int i = 0; i < patchMaps.Length; i++)
                patchMaps[i] = new PatchMap(Settings.PatchMapSize);

            //We could iterate from 0 to faces but using the model info is better
            for (int i = base.models[0].face; i < base.models[0].face + base.models[0].n_faces; i++)
            {
                //Get the face
                Q3Faces face = faces[i];

           
               
                //Loop through all the triangles in the face
                for (int j = 0; j < face.n_meshverts / 3; j++)
                {
                    Q3Vertexes v1 = vertexes[face.vertex + meshverts[face.meshvert + j*3].offset];
                    Q3Vertexes v2 = vertexes[face.vertex + meshverts[face.meshvert + j*3 + 1].offset];
                    Q3Vertexes v3 = vertexes[face.vertex + meshverts[face.meshvert + j*3 + 2].offset];


                    //Find the area of the space in world coordinates
                    float faceArea = GetWorldArea(v1, v2, v3);
                    //Find the area of the space in lightmap pixels
                    float lightPixelArea = (float)GetPatchesArea(v1, v2, v3, Settings.PatchMapSize);

                    //Don't add a patch if there are no light pixels
                    if (lightPixelArea <= 0)
                       continue;

                   float patchArea = lightPixelArea / faceArea;

                    //Get the min and max light points area
                    Vector2 l1 = this.NormalizeLightTex(v1.texcoords[1] * Settings.PatchMapSize);
                    Vector2 l2 = this.NormalizeLightTex(v2.texcoords[1] * Settings.PatchMapSize);
                    Vector2 l3 = this.NormalizeLightTex(v3.texcoords[1] * Settings.PatchMapSize);

                    Point p1 = new Point((int)l1.X, (int)l1.Y);
                    Point p2 = new Point((int)l2.X, (int)l2.Y);
                    Point p3 = new Point((int)l3.X, (int)l3.Y);

                    int minX = (int)Utilities.Min<float>(l1.X, l2.X, l3.X);
                    int maxX = (int)Utilities.Max<float>(l1.X, l2.X, l3.X);
                    int minY = (int)Utilities.Min<float>(l1.Y, l2.Y, l3.Y);
                    int maxY = (int)Utilities.Max<float>(l1.Y, l2.Y, l3.Y);
                    
                    //Loop through all points given in the rectangle
                    for(int x = minX; x <= maxX; x++)
                    {

                        for(int y = minY; y <= maxY; y++)
                        {
                            //Verify with barycentric coordinates that the point is indeed inside the rectangle
                            Point p = new Point(x, y);



                            float alpha, beta, gamma;

         

                            Utilities.CalcBarycentric(p, p1, p2, p3, out alpha, out beta, out gamma);

                            if (y == 0)
                                y = 0;

                            const float threshold = 0.00001f;

                            //If the barycentric coordinates are between 0 and 1, we are in the tri
                            if (alpha >= 0.0f - threshold && beta >= 0.0f - threshold && gamma >= 0.0f - threshold
                                && alpha <= 1.0f + threshold && beta <= 1.0f + threshold && gamma <= 1.0f + threshold)
                            {

                                Vector3 worldCoord = alpha * v1.position + beta * v2.position + gamma * v3.position;

                                
                                //Is there already an existing patch here?
                                if (patchMaps[face.lm_index][p.X, p.Y] == null)
                                {

                                    Patch newPatch = new Patch(i, p, worldCoord, patchArea, face.normal);
                                    patchMaps[face.lm_index][p.X, p.Y] = newPatch;

                                    //We need to add this patch to both the emitters and receivers
                                    listEmitters.Add(newPatch);
                                    listReceivers.Add(newPatch);
                                }
                                else
                                {
                                    //alpha = 0.0f;
                                }
                            }
                            

                        }
                    }

                    ////This is a bit shady but loop through the barycentric coordinates of the tri, and rock on
                    //float delta = 1.0f / (float)Settings.PatchMapSize;
                    //for (float alpha = 0.0f; alpha <= 1.0f - delta; alpha += delta)
                    //{
                    //    for (float beta = 0.0f; beta <= 1.0f - alpha - delta; beta += delta)
                    //    {

                    //        Vector2 lightTexCoord = (1.0f - alpha - beta) * v1.texcoords[1]
                    //        + (alpha) * v2.texcoords[1] + (beta) * v3.texcoords[1];

                    //        Vector3 worldCoord = (1.0f - alpha - beta) * v1.position
                    //        + alpha * v2.position + beta * v3.position;

                    //        if (lightTexCoord.X < 0)
                    //            lightTexCoord.X *= -1;

                    //        if (lightTexCoord.Y < 0)
                    //            lightTexCoord.Y *= -1;

                    //        //Convert the lightTexCoord to a point
                    //        Point luxel = new Point((int)(lightTexCoord.X * Settings.PatchMapSize), (int)(lightTexCoord.Y * Settings.PatchMapSize));

                    //        //If we found a brand new point..
                    //        if (luxel != lastPoint)
                    //        {
                    //            //log.Print(String.Format("[Create Patches] Adding patch at lightmap {0}, ({1}, {2}]).", face.lm_index, luxel.X, luxel.Y)); 
                    //            //Initialize the patch to something good
                    //            Patch p= new Patch(i, luxel,worldCoord,patchArea, face.normal) ;
                    //            patchMaps[face.lm_index][luxel.X, luxel.Y] = p;
                    //            lastPoint = luxel;

                    //            //We need to add this patch to both the emitters and receivers
                    //            listEmitters.Add(p);
                    //            listReceivers.Add(p);
                    //        }

                    //    }
                    //}
                }
            }
        }

        /// <summary>
        /// Make sure negative light tex coords get turned positive
        /// </summary>
        Vector2 NormalizeLightTex(Vector2 inVec)
        {
            if (inVec.X < 0)
                inVec.X *= -1;

            if (inVec.Y < 0)
                inVec.Y *= -1;

            return inVec;
        }

        /// <summary>
        /// This is the top-level function for doing radiosity calculations
        /// </summary>
        void CalculateRadiosity()
        {
            float error = float.MaxValue;
            int pass = 0;

     
            //Loop while the error is still unacceptable
            while (error > Settings.LuminanceError)
            {
                log.Print(String.Format("[CalculateRadiosity] Pass: {0} Current Error: {1}", pass, error));
                //Sort emitters based on the energy they have to do distribute
                listEmitters.Sort(emitterComparer);
         

                //Pick the emitter with the most energy (aka, minimize the error)
                ILightEmitter currentEmitter = listEmitters[0];
                
                Vector3 totalEnergy = listEmitters[0].UnusedEmissivity + listEmitters[0].UnusedReflectance;
                error = totalEnergy.Length();
                log.Print(String.Format("   -Highest emitter: {0}", totalEnergy));

                //Create an array to hold form factors for each of the receivers
                //This will prevent us from having to make a second normalization pass
                double[] formFactorArray = new double[listReceivers.Count];

                //normalization value to make sure the form factors add up to 1.0f
                //this will prevent us from adding or removing energy from the scene
                double totalFormFactor = 0.0; 

                //Lets loop through all the receivers, and calculate the form factors, aka shoot our rays to them!
                for (int i = 0; i < listReceivers.Count; i++)
                {
                    //Make sure we aren't trying to emit light to ourselves!
                    if (currentEmitter == listReceivers[i])
                        continue;

                    //Calculate the form factor beween currentEmitter and receiver[i]
                    double formFactor = 
                        Math.Abs((double)currentEmitter.CalculateFormFactor(listReceivers[i], this) * currentEmitter.Area/listReceivers[i].Area);
                    


                    formFactorArray[i] = formFactor;

             

                    //Add the form factor to total form factor

              

                    totalFormFactor += formFactor;

                }

                //Apply the normalized energy from the emitter to each receiver
                for (int i = 0; i < listReceivers.Count; i++)
                {
                    float factor = (float)(formFactorArray[i] / totalFormFactor);

                    listReceivers[i].AddEnergy(factor * totalEnergy);
                }

                //Finally, reset the unused energy of the emitter
                currentEmitter.FinishRadiosity();
                WriteLightmaps();

                pass++;

            }
               

        }

        /// <summary>
        /// Takes a bsp triangle and returns the world area
        /// </summary>
        float GetWorldArea(Q3Vertexes v1, Q3Vertexes v2, Q3Vertexes v3)
        {
            //Compute the cross product of these vectors
            Vector3 BA = v2.position - v1.position;
            Vector3 CA = v3.position - v1.position;
            return Utilities.CalcTriangleAreaFromVecs(BA, CA);
        }

        /// <summary>
        /// Get the number of lightmap pixels from three triangles
        /// </summary>
        float GetPatchesArea(Q3Vertexes v1, Q3Vertexes v2, Q3Vertexes v3, int lightMapSize)
        {
            //Point p1 = new Point((int)(v1.texcoords[1].X * lightMapSize), (int)(v1.texcoords[1].Y * lightMapSize));
            //Point p2 = new Point( (int)(v2.texcoords[1].X * lightMapSize), (int)(v2.texcoords[1].Y * lightMapSize));
            //Point p3 = new Point((int)(v3.texcoords[1].X * lightMapSize), (int)(v3.texcoords[1].Y * lightMapSize));

            //Vector3 BA = new Vector3(p2.X - p1.X, p2.Y - p1.Y, 0.0f);
            //Vector3 CA = new Vector3(p3.X - p1.X, p3.Y - p1.Y, 0.0f);

            Vector3 BA = new Vector3(v2.texcoords[1].X - v1.texcoords[1].X, v2.texcoords[1].Y - v1.texcoords[1].Y, 0.0f);
            Vector3 CA = new Vector3(v3.texcoords[1].X - v1.texcoords[1].X, v3.texcoords[1].Y - v1.texcoords[1].Y, 0.0f);

            float val = Utilities.CalcTriangleAreaFromVecs(BA, CA);

            return val;
        }




    }
}
