using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sxe.Engine.Graphics
{
    public class Render3DComponent : Microsoft.Xna.Framework.DrawableGameComponent, IRender3DService
    {

        int maxVertices = 0;
        int usedVertices = 0;
        VertexPositionColor[] vertices;

        BasicEffect effect;
        Matrix view = Matrix.Identity;
        Matrix projection = Matrix.Identity;

        GraphicsDevice device;


        VertexDeclaration vertexDecl;

        public Matrix View
        {
            get { return view; }
            set { view = value; }
        }

        public Matrix Projection
        {
            get { return projection; }
            set { projection = value; }
        }


        public Render3DComponent(SxeGame game, GraphicsDevice inDevice, int maxLines)
            : base(game)
        {
            maxVertices = maxLines * 2;
            vertices = new VertexPositionColor[maxVertices];
            device = inDevice;
            Game.Services.AddService(typeof(IRender3DService), this);
        }


        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            effect = new BasicEffect(device, null);
            effect.VertexColorEnabled = true;
            effect.CommitChanges();

            vertexDecl = new VertexDeclaration(device, VertexPositionColor.VertexElements);


            base.Initialize();
        }

        public void RenderLine(Vector3 v1, Vector3 v2, Color col)
        {
            if (usedVertices >= maxVertices)
                return;

            vertices[usedVertices].Position = v1;
            vertices[usedVertices].Color = col;

            usedVertices++;

            vertices[usedVertices].Position = v2;
            vertices[usedVertices].Color = col;

            usedVertices++;
        }

        public void RenderSphere(Vector3 center, float radius, Color col)
        {
            RenderLine(center, center + Vector3.Up * radius, col);
            RenderLine(center, center - Vector3.Up * radius, col);

            RenderLine(center, center + Vector3.Forward * radius, col);
            RenderLine(center, center - Vector3.Forward * radius, col);

            RenderLine(center, center + Vector3.Left * radius, col);
            RenderLine(center, center - Vector3.Left * radius, col);
        }

        public void RenderFloorGrid(Vector2 start, Vector2 end, float delta, Color col)
        {
            int sizeX = (int)((end.X - start.X) / delta);
            int sizeZ = (int)((end.Y - start.Y) / delta);

            //Render vertical lines
            for (int i = 0; i <= sizeX; i++)
            {
                RenderLine(new Vector3(start.X, 0.0f, start.Y + delta * i), new Vector3(end.X, 0.0f, start.Y + delta * i), col);
            }

            //Render horizontal lines
            for (int i = 0; i <= sizeZ; i++)
            {
                RenderLine(new Vector3(start.X + delta * i, 0.0f, start.Y), new Vector3(start.X + delta * i, 0.0f, end.Y), col);
            }

        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {


            //RenderLine(Vector3.Up * -100.0f, Vector3.Up * 100.0f, Color.Purple);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {

            if (usedVertices < 2)
            {
                base.Draw(gameTime);
                return;
            }

            device.RenderState.DepthBufferEnable = true;

            effect.World = Matrix.Identity;
            effect.View = view;
            effect.Projection = projection;

            effect.Begin();
            for (int i = 0; i < effect.CurrentTechnique.Passes.Count; i++)
            {
                EffectPass p = effect.CurrentTechnique.Passes[i];
                p.Begin();

                device.VertexDeclaration = vertexDecl;
                device.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.LineList, vertices, 0, usedVertices / 2);

                p.End();
            }
            effect.End();
            usedVertices = 0;


            base.Draw(gameTime);
        }
    }
}
