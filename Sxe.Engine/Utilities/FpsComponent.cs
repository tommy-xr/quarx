using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;

using Sxe.Engine.Graphics;
using Sxe.Engine.UI;
using System.Globalization;

namespace Sxe.Engine.Utilities
{
   
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class FpsComponent : Microsoft.Xna.Framework.DrawableGameComponent
    {

        TimeSpan elapsedTime = TimeSpan.Zero;
        int fps; //done at runtime = 0;
        int currentFps; //done at runtime = 0;

        double time = 0.25; //time to track over

        IRender2DService Render2D;
        SpriteFont font;

        public int Fps
        {
            get { return fps; }
        }

        public FpsComponent(Game game, ContentManager content)
            : base(game)
        {
            font = content.Load<SpriteFont>("Calibri11");
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            Render2D = (IRender2DService)Game.Services.GetService(typeof(IRender2DService));

            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            elapsedTime += gameTime.ElapsedGameTime;

            if (elapsedTime > TimeSpan.FromSeconds(time))
            {
                elapsedTime -= TimeSpan.FromSeconds(time);
                fps = (int)(currentFps * 1.0 / time);
                currentFps = 0;
            }



            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            currentFps++;

            Color col = Color.White;
            if (gameTime.IsRunningSlowly)
                col = Color.Red;

            Render2D.RenderText(font, "FPS: " + Fps.ToString(CultureInfo.CurrentCulture), new Vector2(20f, 20f), col, 1.0f);
            base.Draw(gameTime);
        }
    }
}