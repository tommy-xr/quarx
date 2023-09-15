using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sxe.Engine.Entity
{
    /// <summary>
    /// Simple entity module for drawing .fbx files
    /// loaded from the content pipeline
    /// Not efficient, but does the job
    /// </summary>
    public class FbxRenderEntityModule
    {
        float scale = 1.0f;
        Matrix worldMatrix = Matrix.Identity;

        Model boxModel;

        Matrix[] transforms;

        public Matrix World
        {
            get { return worldMatrix; }
            set { worldMatrix = value; }
        }

        public float Scale
        {
            get { return scale; }
            set { scale = value; }
        }


        public FbxRenderEntityModule(Model model)
        {
            boxModel = model;
            transforms = new Matrix[model.Bones.Count];
            model.CopyAbsoluteBoneTransformsTo(transforms);
        }

        public void Draw(Matrix view, Matrix projection)
        {
            foreach (ModelMesh mesh in boxModel.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();

                    effect.View = view;
                    effect.Projection = projection;
                    effect.World = Matrix.CreateScale(scale) * transforms[mesh.ParentBone.Index] * worldMatrix;
                }
                mesh.Draw();
            }
        }
    }
}
