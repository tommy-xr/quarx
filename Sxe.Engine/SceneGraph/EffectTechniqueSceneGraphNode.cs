using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sxe.Engine.SceneGraph
{
    public class EffectTechniqueSceneGraphNode : SceneGraphNode 
    {
        EffectTechnique technique;

        public EffectTechniqueSceneGraphNode(EffectTechnique inTechnique)
        {
            technique = inTechnique;
            this.Name = technique.Name;
        }

        public override void Draw(Effect currentEffect, Vector3 eyePos, Matrix view, Matrix projection)
        {

            foreach (EffectPass pass in technique.Passes)
            {
                pass.Begin();
                base.Draw(currentEffect, eyePos, view, projection);
                pass.End();
            }
        }

    }
}
