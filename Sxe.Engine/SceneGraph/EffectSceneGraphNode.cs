using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sxe.Engine.SceneGraph
{
    public class EffectSceneGraphNode : SceneGraphNode
    {
        Effect effect;

        public EffectSceneGraphNode(Effect inEffect)
        {
            effect = inEffect;
        }

        public override void Draw(Effect currentEffect, Vector3 eyePos, Matrix view, Matrix projection)
        {
            effect.Begin();
            base.Draw(this.effect, eyePos, view, projection);
            effect.End();
        }
    }
}
