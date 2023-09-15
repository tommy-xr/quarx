using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

using Sxe.Engine.Graphics;
using Sxe.Engine.Input;

using Microsoft.Xna.Framework.Input;

namespace WinFormsContentLoading
{
    public abstract class AnarchyLevelViewer
    {

        BasicEffect effect;
        RenderTarget2D renderTarget;
        RenderTarget2D pickRenderTarget;
        GraphicsDevice device;
        DepthStencilBuffer depthBuffer;
        DepthStencilBuffer pickDepthBuffer;
        Point position;
        Point size;
        Matrix view;
        Matrix projection;
        
        public Point Position
        {
            get { return position; }
            set { position = value; }
        }

        public Point Size
        {
            get { return size; }
            set { size = value; }
        }

        public Texture2D Texture
        {
            get 
            {
                if(renderTarget != null)
                return renderTarget.GetTexture();

                return null;
            }
        }

        protected Texture2D PickTexture
        {
            get
            {
                if (pickRenderTarget != null)
                    return pickRenderTarget.GetTexture();

                return null;
            }
        }

        protected Matrix View
        {
            get { return view; }
            set { view = value; }
        }

        protected Matrix Projection
        {
            get { return projection; }
            set { projection = value; }
        }

        protected RenderTarget2D RenderTarget
        {
            get { return renderTarget; }
        }

        protected RenderTarget2D PickRenderTarget
        {
            get { return pickRenderTarget; }
        }

        protected GraphicsDevice Device
        {
            get { return device; }
        }

        public virtual void LoadContent(GraphicsDevice inDevice, ContentManager content)
        {
            device = inDevice;
            effect = new BasicEffect(device, null);

            renderTarget = new RenderTarget2D(device, 1024, 1024, 1, SurfaceFormat.Color);
            pickRenderTarget = new RenderTarget2D(device, 256, 256, 1, SurfaceFormat.Color);

            depthBuffer = new DepthStencilBuffer(Device, 1024, 1024, DepthFormat.Depth16);
            pickDepthBuffer = new DepthStencilBuffer(Device, 256, 256, DepthFormat.Depth16);

        }

        public void PreDraw()
        {
            InitializeMatrices();

            DepthStencilBuffer oldBuffer = Device.DepthStencilBuffer;
            Device.DepthStencilBuffer = pickDepthBuffer;
            Device.SetRenderTarget(0, pickRenderTarget);
            Device.Clear(ClearOptions.DepthBuffer | ClearOptions.Target, Color.TransparentBlack, 1.0f, 0);

            DrawPicking();

            Device.SetRenderTarget(0, null);

            Device.DepthStencilBuffer = depthBuffer;
            Device.SetRenderTarget(0, renderTarget);
            Device.Clear(ClearOptions.DepthBuffer | ClearOptions.Target, Color.CornflowerBlue, 1.0f, 0);

            DrawWorld();

            Device.SetRenderTarget(0, null);
            Device.DepthStencilBuffer = oldBuffer;
        }

        public virtual void InitializeMatrices()
        {
        }

        public void Draw(SpriteBatch sb, bool hasFocus)
        {
            Color color = Color.White;
            if (hasFocus)
                color = Color.Red;

            sb.Draw(Texture, new Rectangle(Position.X, Position.Y, Size.X, Size.Y), color);
        }

        public abstract void DrawWorld();

        public void DrawPicking()
        {
        }

        public virtual void Update()
        {
        }

        public virtual void HandleInput(InputEventArgs inputEvent) { }



    }
}
