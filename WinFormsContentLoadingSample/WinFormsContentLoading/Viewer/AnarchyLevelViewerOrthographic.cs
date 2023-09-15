using System;
using System.Collections.Generic;
using System.Text;

using Sxe.Engine.Graphics;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WinFormsContentLoading
{
    public enum OrthographicAxis
    {
        XY = 0,
        YZ,
        XZ
    }

    public class AnarchyLevelViewerOrthographic : AnarchyLevelViewer
    {

        Camera camera = new Camera();
        public Camera Camera
        {
            get { return camera; }
            set { camera = value; }
        }


        public override void InitializeMatrices()
        {
            this.View = camera.View;
            this.Projection = Matrix.CreateOrthographic(10.0f, 10.0f, 0.1f, 1000f);
            base.InitializeMatrices();
        }

        public AnarchyLevelViewerOrthographic(OrthographicAxis axis)
        {
            //if (axis = OrthographicAxis.XY)
            //{
               
            //}
        }

        public override void DrawWorld()
        {
            Device.RenderState.FillMode = FillMode.WireFrame;
            // Draw the modl.
            foreach (ModelInfo mi in Editor.Level.Models)
            {
                Model model = Editor.GetModel(mi.ContentPath);

                foreach (ModelMesh mesh in model.Meshes)
                {
                    foreach (BasicEffect effect in mesh.Effects)
                    {
                        effect.World = Matrix.CreateScale(mi.Scale)
                    * Matrix.CreateFromYawPitchRoll(mi.Rotation.Y, mi.Rotation.X, mi.Rotation.Z)
                    * Matrix.CreateTranslation(mi.Translation);
                        effect.View = View;
                        effect.Projection = Projection; 

                        //Matrix.CreateOrthographic(

                        effect.EnableDefaultLighting();
                        effect.PreferPerPixelLighting = true;
                        effect.SpecularPower = 16;
                    }

                    mesh.Draw();
                }
            }

            Device.RenderState.FillMode = FillMode.Solid;

        }


        public override void HandleInput(Sxe.Engine.Input.InputEventArgs inputEvent)
        {
            EditorInputEventArgs editorEvent = inputEvent as EditorInputEventArgs;

            if (editorEvent != null)
            {

                if (editorEvent.EditorInputType == EditorInputType.SlideUp)
                {
                    Camera.Position += Camera.Up * 0.1f;
                }

                if (editorEvent.EditorInputType == EditorInputType.SlideDown)
                {
                    Camera.Position -= Camera.Up * 0.1f;
                }

                if (editorEvent.EditorInputType == EditorInputType.ZoomIn)
                {
                    Camera.Position += Camera.Forward * 1f;
                }

                if (editorEvent.EditorInputType == EditorInputType.ZoomOut)
                {
                    Camera.Position -= Camera.Forward * 1f;
                }

                if (editorEvent.EditorInputType == EditorInputType.SlideRight)
                {
                    Camera.Position += Camera.Right * -0.1f;
                }

                if (editorEvent.EditorInputType == EditorInputType.SlideLeft)
                {
                    Camera.Position += Camera.Right * 0.1f;
                }
            }
            Camera.Update(new Microsoft.Xna.Framework.GameTime());

            base.HandleInput(inputEvent);
        }
    }
}
