using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sxe.Engine.Shader
{
    /// <summary>
    /// Base class for all things shader related
    /// </summary>
    public class Shader
    {
        Effect shaderEffect;
        public Effect ShaderEffect
        {
            get { return shaderEffect; }
            set { shaderEffect = value; }
        }

        public Shader(Effect inEffect)
        {
            shaderEffect = inEffect;
        }

        /// <summary>
        /// Returns true if the specified effect can be a shader of this type
        /// Returns false otherwise
        /// </summary>
        public virtual bool Verify(Effect effect)
        {
            return true;
        }
    }
}
