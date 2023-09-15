using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sxe.Library.Graphics.Materials
{
    /// <summary>
    /// Basic material used for rendering objects in the world
    /// Provides view, projection and world matrices
    /// </summary>
    public class WorldMaterial : BaseMaterial
    {
        EffectParameter world;
        EffectParameter view;
        EffectParameter projection;
        Matrix worldMatrix = Matrix.Identity;

        public Matrix World
        {
            get { return worldMatrix; }
            set { worldMatrix = value; }
        }
        


        public override void CacheEffectParameters(Microsoft.Xna.Framework.Graphics.Effect effect)
        {
            world = effect.Parameters["World"];
            view = effect.Parameters["View"];
            projection = effect.Parameters["Projection"];

            base.CacheEffectParameters(effect);
        }

        public override void SetGlobalParameters(GlobalParameters parameters)
        {
            view.SetValue(parameters.View);
            projection.SetValue(parameters.Projection);

            base.SetGlobalParameters(parameters);
        }

        protected override void SetGeometryParameters(bool useConservativeParams)
        {
            world.SetValue(World);

            base.SetGeometryParameters(useConservativeParams);
        }


    }
}
