using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sxe.Engine.Shader
{
    public class WorldShader : Shader
    {
        EffectParameter world;
        EffectParameter view;
        EffectParameter projection;

        Matrix worldMatrix;
        Matrix viewMatrix;
        Matrix projectionMatrix;

        public Matrix World
        {
            get { return worldMatrix; }
            set
            {
                worldMatrix = value;
                world.SetValue(worldMatrix);
            }
        }

        public Matrix View
        {
            get { return viewMatrix; }
            set
            {
                viewMatrix = value;
                view.SetValue(viewMatrix);
            }
        }

        public Matrix Projection
        {
            get { return projectionMatrix; }
            set
            {
                projectionMatrix = value;
                projection.SetValue(projectionMatrix);
            }
        }

        public WorldShader(Effect effect)
            : base(effect)
        {
            world = effect.Parameters["World"];
            view = effect.Parameters["View"];
            projection = effect.Parameters["Projection"];

            worldMatrix = world.GetValueMatrix();
            viewMatrix = view.GetValueMatrix();
            projectionMatrix = projection.GetValueMatrix();

        }

    }
}
