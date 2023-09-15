using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

using Sxe.Engine.Test.Framework;
using Sxe.Engine.Utilities.Console;
using Sxe.Engine.Graphics;
using Sxe.Engine.Input;

namespace Sxe.Engine.UI
{
    public class TestScreen : FormScreen
    {

        Camera camera;
        protected Camera Camera
        {
            get { return camera; }
        }

        IRender3DService render3D;
        protected IRender3DService Render3D
        {
            get { return render3D; } 
        }

        IRender2DService render2D;
        protected IRender2DService Render2D
        {
            get { return render2D; }
        }

        IGameController controller;
        protected IGameController Controller
        {
            get { return controller; }
        }

        GraphicsDevice device;
        protected GraphicsDevice Device
        {
            get { return device; }
        }
        

        //public TestScreen()
        //    : base(null, null)
        //{
        //}

        //public virtual void TestInitialize(IGameScreenService service,  ContentManager content)
        //{
        
        //}

        //public override sealed void Initialize(IGameScreenService service, ContentManager content)
        //{
        //    base.Initialize(service, content);
        //    render3D = (IRender3DService)service.Services.GetService(typeof(IRender3DService));
        //    render2D = (IRender2DService)service.Services.GetService(typeof(IRender2DService));
        //    camera = new Camera();
        //    controller = service.Input.Controller;
        //    device = service.GraphicsDevice;

        //    TestInitialize(service, content);

        //}

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            camera.Update(gameTime);
            render3D.View = camera.View;
            render3D.Projection = camera.Projection;
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            base.Draw(spriteBatch, gameTime);
        }
    }
}
