using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sxe.Engine
{
    public class SxeDrawableGameComponent : SxeGameComponent
    {
        bool visible;
        TimeSpan lastDrawTime;
        GraphicsDevice device;

        public bool Visible
        {
            get { return visible; }
            set { visible = value; }
        }

        public TimeSpan LastDrawTime
        {
            get { return lastDrawTime; }
        }

        public GraphicsDevice GraphicsDevice
        {
            get { return device; }
        }

        /// <summary>
        /// Called when a new instance of the class is created
        /// </summary>
        /// <param name="game"></param>
        public SxeDrawableGameComponent(SxeGame game)
            : base(game)
        {
            device = game.GraphicsDevice;
        }

        public override void  Initialize()
        {
            if(device.GraphicsDeviceStatus == GraphicsDeviceStatus.Normal)
            {
                LoadContent();
            }
            device.DeviceReset += OnDeviceReset;

 	        base.Initialize();
        }

        void OnDeviceReset(object sender, EventArgs args)
        {
            LoadContent();
        }

        /// <summary>
        /// Called when graphical content needs to be loaded
        /// </summary>
        protected virtual void LoadContent()
        {
        }

        protected virtual void UnloadContent()
        {
        }

        public void BasePreDraw(GameTime gameTime)
        {
            PreDraw(gameTime);
        }

        protected virtual void PreDraw(GameTime gameTime)
        {
        }

        public void BaseDraw(GameTime gameTime)
        {
            DateTime start = new DateTime();
            DateTime end = new DateTime();
            if (Globals.DebugMode)
            {
                start = DateTime.Now;
            }

            Draw(gameTime);

            if (Globals.DebugMode)
            {
                end = DateTime.Now;
                lastDrawTime = end - start;
            }


        }

        protected virtual void Draw(GameTime gameTime)
        {
        }
    }
}
