using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Sxe.Library.Graphics
{
    /// <summary>
    /// The render tree handles top level rendering management
    /// Basically just contains a list of render nodes and iterates through them!
    /// </summary>
    public class RenderTree
    {
        ContentManager content;
        RenderNode prePass;
        RenderNode drawOpaqueScenery;
        RenderNode drawOpaqueEntities;
        RenderNode drawTransparentScenery;
        RenderNode drawTransparentEntities;
        RenderNode postPass;

        RenderNodeCollection nodes = new RenderNodeCollection();

        GlobalParameters parameters = new GlobalParameters();

        public RenderNodeCollection Nodes
        {
            get { return nodes; }
        }

        public RenderNode PrePass { get { return prePass; } }
        public RenderNode DrawOpaqueScenery { get { return drawOpaqueScenery; } }
        public RenderNode DrawOpaqueEntities { get { return drawOpaqueEntities; } }
        public RenderNode DrawTransparentScenery { get { return DrawTransparentScenery; } }
        public RenderNode DrawTransparentEntities { get { return drawTransparentEntities; } }
        public RenderNode PostPass { get { return postPass; } }

        public RenderTree(IServiceProvider services)
        {
            content = new ContentManager(services, "Content");

            prePass = new RenderNode(content);
            drawOpaqueEntities = new RenderNode(content);
            drawOpaqueScenery = new RenderNode(content);
            drawTransparentScenery = new RenderNode(content);
            drawTransparentEntities = new RenderNode(content);
            postPass = new RenderNode(content);

            nodes.Add(prePass);
            nodes.Add(drawOpaqueScenery);
            nodes.Add(drawOpaqueEntities);
            nodes.Add(drawTransparentScenery);
            nodes.Add(drawTransparentEntities);
            nodes.Add(postPass);
        }

        public void Draw(Vector3 position, Matrix view, Matrix projection, GameTime gameTime)
        {
            parameters.Position = position;
            parameters.View = view;
            parameters.Projection = projection;
            parameters.Time = (float)gameTime.TotalGameTime.TotalSeconds;

            for (int i = 0; i < nodes.Count; i++)
            {
                nodes[i].Draw(parameters);
            }
        }

    }
}
