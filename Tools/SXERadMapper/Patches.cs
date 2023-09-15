using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SXERadMapper
{
    /// <summary>
    /// Like a texture map, but this holds patch daa
    /// </summary>
    public class PatchMap
    {
        Patch[,] patches;
        int size;

        public Patch this[int i, int j]
        {
            get { return patches[i, j]; }
            set { patches[i, j] = value; }
        }

        public PatchMap(int dimensions)
        {
            size = dimensions;
            patches = new Patch[dimensions, dimensions];
        }

        public Color [] TestColorOutput()
        {

            Color[] outColor = new Color[size * size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    int index = i + j * size;
                    if (patches[i, j] == null)
                    {
                        outColor[index] = Color.Black;
                    }
                    else
                        outColor[index] = Color.White;
                }
            }
            return outColor;
        }

        public Color[] RawColorOutput()
        {
            float totalRadiance = 0.0f;
            Color[] outColor = new Color[size * size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    int index = i + j * size;
                    if (patches[i, j] != null)
                    {
                        Vector3 val =patches[i, j].UsedRadiosity + patches[i, j].UnusedEmissivity + patches[i, j].UnusedReflectance;

                        //Scale the output color by the size of the image
                        float scale = (float)size / (float)RadiositySettings.BASE_SIZE;
                        scale *= scale; //find the scale squared

                        //Multiply val by scale
                        val *= scale;

                        outColor[index] = new Color(val);
                        totalRadiance += val.Length();

                    }
                    else
                    {
                        outColor[index] = Color.Black;
                    }
                }
            }

            totalRadiance = 9.0f;

            return outColor;
        }
    }

    /// <summary>
    /// A patch is both an emitter and receiver of radiosity
    /// </summary>
    public class Patch : ILightEmitter, ILightReceiver
    {
        //Intrinsic properties
        public int faceIndex; //the face that the patch belongs to
        public Point lightTexCoord;
        Vector3 worldCoord;
        Vector3 normal;
        float worldArea;

        Vector3 reflectivity = new Vector3(0.5f); //the reflectivity of the surface, for each color
        Vector3 radiosity = Vector3.Zero; //the total radiosity this surface has emitted
        Vector3 emissivity = Vector3.Zero; //the radiosity that this surface intrinsically possesses
        Vector3 illuminationToReflect = Vector3.Zero; //radiosity accumulated from other patches that needs to be reflected

        //Implementation of ILightEmitter interface
        public Vector3 UsedRadiosity { get { return radiosity; } }
        public Vector3 UnusedEmissivity { get { return emissivity; } }
        public Vector3 UnusedReflectance { get { return illuminationToReflect; } }
        public Vector3 Normal { get { return normal; } }
        public float Area { get { return worldArea; } }
        public Vector3 Reflectance { get { return reflectivity; } } 
        //Implementation of ILightReceiver interface
        public Vector3 WorldPosition { get { return worldCoord; } }
        public void AddEnergy(Vector3 energy)
        {
            //Just add the incoming energy as enery to reflect
            this.illuminationToReflect += reflectivity * energy;
        }
       

        public Patch(int face, Point lightPixel, Vector3 coord, float area, Vector3 inNormal)
        {
            faceIndex = face;
            lightTexCoord = lightPixel;
            worldCoord = coord;
            worldArea = area;
            normal = inNormal;
        }

        public float CalculateFormFactor(ILightReceiver receiver, RadiosityBSP bsp)
        {
            const float PATCH_THRESHOLD = 16f;

            //Uses first form factor presented in SIGGRAPH 1993 radiosity overview
            Vector3 emitterToReceiver = receiver.WorldPosition - this.WorldPosition;
            float distanceSquared = emitterToReceiver.LengthSquared();

            if (distanceSquared < PATCH_THRESHOLD)
                return 0.0f;

            emitterToReceiver.Normalize();
            Vector3 receiverToEmitter = -emitterToReceiver;

            float ang1 = Utilities.CosAngleVectors(this.normal, emitterToReceiver);
            float ang2 = Utilities.CosAngleVectors(receiver.Normal, receiverToEmitter);

            float h = CalculateH(receiver, bsp);
            float val = h * ang1 * ang2 / distanceSquared;

            

            return val;



            //As a first test, distribute light equally across scene
            //return 1.0f; //this is wrong (duh)
        }

        /// <summary>
        /// Get the "H" value, or visibility information, through random sampling
        /// </summary>
        /// <param name="receiver"></param>
        /// <param name="bsp"></param>
        /// <returns></returns>
        float CalculateH(ILightReceiver receiver, RadiosityBSP bsp)
        {
            if (bsp.RayTrace(this.WorldPosition, receiver.WorldPosition, this.faceIndex))
                return 1.0f;

            return 0.0f;
        }

        public void FinishRadiosity()
        {
            radiosity = radiosity + emissivity + illuminationToReflect;
            emissivity = Vector3.Zero;
            illuminationToReflect = Vector3.Zero;
        }


    }
}
