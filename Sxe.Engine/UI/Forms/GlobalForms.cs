using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sxe.Engine.UI
{
    /// <summary>
    /// Singleton for managing forms that are global
    /// 
    /// </summary>
    public static class GlobalForms
    {
        static bool isInitialized = false;
        static FormCollection formCollection;
        static SpriteBatch spriteBatch;

        public static FormCollection FormCollection
        {
            get
            {
                if (!isInitialized)
                    throw new Exception("FormCollection accessed without being initialized.");
                return formCollection;
            }
        }


        public static void Initialize(GraphicsDevice device)
        {
            formCollection = new FormCollection();
            spriteBatch = new SpriteBatch(device);

            isInitialized = true;
        }

        public static void Update(GameTime gameTime)
        {
            formCollection.Update(gameTime);
        }

        public static void PreDraw(GameTime gameTime)
        {
            formCollection.DrawForms(spriteBatch);
        }

        public static void Draw(GameTime gameTime)
        {
            formCollection.Draw(spriteBatch);
        }
    }
}
