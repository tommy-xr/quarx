using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SXERadMapper
{

    class EmitterComparer : IComparer<ILightEmitter>
    {
        public int Compare(ILightEmitter x, ILightEmitter y)
        {
            float xVal = ((Vector3)(x.UnusedReflectance + x.UnusedEmissivity)).Length();
            float yVal = ((Vector3)(y.UnusedReflectance + y.UnusedEmissivity)).Length();

            if (xVal > yVal)
                return -1;
            else if (xVal < yVal)
                return 1;
            else
                return 0;
        }

    }

    /// <summary>
    /// Interface for objects that receive light
    /// </summary>
    interface ILightEmitter
    {
        /// <summary>
        /// This is the radiosity that has been "shot" by the emitter
        /// </summary>
        Vector3 UsedRadiosity { get; }

        /// <summary>
        /// This is the amount of emissivity that has not been "shot" by the emitter
        /// </summary>
        Vector3 UnusedEmissivity { get; }

        /// <summary>
        /// The unused reflectance radiosity
        /// </summary>
        Vector3 UnusedReflectance { get; }

        /// <summary>
        /// The area of the patch/element
        /// </summary>
        float Area { get; }

        /// <summary>
        /// The normal of the patch/element
        /// </summary>
        Vector3 Normal { get; }

        /// <summary>
        /// The reflectance of the patch, per color
        /// </summary>
        Vector3 Reflectance { get; }

        /// <summary>
        /// Calculates the form factor between this sending patch and a receiving patch
        /// </summary>
        float CalculateFormFactor(ILightReceiver receiver, RadiosityBSP bsp);

        /// <summary>
        /// Called when the emitter is finished shooting. All unused radiosity/reflectance
        /// should be transferred to use radiosity.
        /// </summary>
        void FinishRadiosity();



    }
}
