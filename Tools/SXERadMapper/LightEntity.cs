using System;
using System.Collections.Generic;
using System.Text;

using Engine.Source.Shared.Entities;
using Microsoft.Xna.Framework;

namespace SXERadMapper
{
    /// <summary>
    /// Class for light entities that are read from the bsp file
    /// Lights are emitters only, not receivers 
    /// </summary>
    public class LightEntity : Entity, ILightEmitter
    {

        //ILightEmitter interface
        public Vector3 UsedRadiosity { get { return Vector3.Zero; } }
        public Vector3 UnusedEmissivity { get { return emissivity; } }
        public Vector3 UnusedReflectance { get { return Vector3.Zero; } }
        public float Area { get { return (float)MathHelper.Pi; } }
        public Vector3 Normal { get { return Vector3.Zero; } }
        public Vector3 Reflectance { get { return Vector3.Zero; } }

        public float CalculateFormFactor(ILightReceiver receiver, RadiosityBSP bsp)
        {
            //Ray trace from lightpos to receiver
            float h = 0.0f;

            if (bsp.RayTrace(this.position, receiver.WorldPosition, -1))
                h = 1.0f;


            //The form factor for this diffuse light is just the area of the receiver divided by the distance from the light to the receiver
            Vector3 distanceVec = receiver.WorldPosition - this.position;
            float distance = distanceVec.LengthSquared();

            float cutoff = 16f * 16f;
            if (distance < cutoff)
                distance = cutoff;




            float formFactor = h * receiver.Area / distance;

            return formFactor;
        }

        public void FinishRadiosity()
        {
            emissivity = Vector3.Zero;
        }



        KeyValueVector3 keyLightColor;
        KeyValueFloat keyLightIntensity;
        Vector3 emissivity = Vector3.Zero;


        public LightEntity(EntityManager mgr)
            : base(mgr)
        {
        }

        public override void InitializeKeyValues()
        {
            keyLightColor = new KeyValueVector3("rad_color", this);
            keyLightColor.Value = new Vector3(1.0f);

            keyLightIntensity = new KeyValueFloat("rad_intensity", this);
            keyLightIntensity.Value = 1000.0f;

        }

        public override void Spawn()
        {
            base.Spawn();
            emissivity = keyLightColor.Value * keyLightIntensity.Value;
        }

    }
}
