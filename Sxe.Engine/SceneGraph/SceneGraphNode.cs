using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sxe.Engine.SceneGraph
{
    public delegate void PreDrawFunction(Vector3 eyePos, Matrix view, Matrix projection);
    public delegate void DrawFunction(Vector3 eyePos, Matrix view, Matrix projection);

    public class SceneGraphNode
    {
        SceneGraphNodeCollection children;// = new SceneGraphNodeCollection(this);
        SceneGraphNode parent;
        PreDrawFunction preDrawFunction;
        DrawFunction drawFunction;
        string name;

        public SceneGraphNodeCollection Children { get { return children; } }
        public SceneGraphNode Parent
        {
            get { return parent; }
            set { parent = value; }
        }

        public PreDrawFunction PreDrawFunction
        {
            get { return preDrawFunction; }
            set { preDrawFunction = value; }
        }

        public DrawFunction DrawFunction
        {
            get { return drawFunction; }
            set { drawFunction = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public SceneGraphNode()
        {
            children = new SceneGraphNodeCollection(this);
        }

        /// <summary>
        /// Called to do any rendering of render targets or prep work before the real render pass
        /// </summary>
        public virtual void PreDraw(Vector3 eyePos, Matrix view, Matrix projection)
        {
            if (PreDrawFunction != null)
                PreDrawFunction(eyePos, view, projection);

            for (int i = 0; i < children.Count; i++)
                children[i].PreDraw(eyePos, view, projection);
        }

        public virtual void Draw(Effect currentEffect, Vector3 eyePos, Matrix view, Matrix projection)
        {
            if (DrawFunction != null)
                DrawFunction(eyePos, view, projection);

            for (int i = 0; i < children.Count; i++)
                children[i].Draw(currentEffect, eyePos, view, projection);
        }

    }
}
