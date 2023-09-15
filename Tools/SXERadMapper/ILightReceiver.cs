using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;

namespace SXERadMapper
{
    /// <summary>
    /// Interface for objects that receive light
    /// </summary>
    public interface ILightReceiver
    {
        /// <summary>
        /// Representative of the world position for this object
        /// </summary>
        Vector3 WorldPosition { get; }

        /// <summary>
        /// Representative of the normal for this patch
        /// </summary>
        Vector3 Normal { get; }

        /// <summary>
        /// Representative of the area for this patch
        /// </summary>
        float Area { get; }

        /// <summary>
        /// Add energy to the receiver
        /// </summary>
        void AddEnergy(Vector3 energy);

    }
}
