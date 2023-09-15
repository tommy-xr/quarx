using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Sxe.Engine.UI;

namespace Sxe.Engine.Graphics
{
    /// <summary>
    /// Does basic rendering of 2d graphics / sprites in screen space
    /// </summary>
    public class Render2DComponent : DrawableGameComponent, IRender2DService
    {

        public const int MaxSprites = 128;
        public const int MaxText = 128;
        static Color DefaultColor = Color.Green;

        #region Private Members
        SpriteBatch sprites;

        int maxSprites; //maximum number of sprites to render
        int currentSprites; //current sprites to render
        SpriteInfo[] spriteArray; //array of sprite information

        int maxText; //maximum number of text to render
        int currentText; //current text to render
        TextInfo[] textArray; //array of text information
        GraphicsDevice device;

        #endregion

        #region Constructors

        public Render2DComponent(Game game, int inMaxSprites, int inMaxText, GraphicsDevice inDevice)
            : base(game)
        {
            game.Services.AddService(typeof(IRender2DService), this);

            device = inDevice;

            maxSprites = inMaxSprites;
            spriteArray = new SpriteInfo[maxSprites];

            maxText = inMaxText;
            textArray = new TextInfo[maxText];

            sprites = new SpriteBatch(device);
        }

        public Render2DComponent(Game game, GraphicsDevice device)
            : this(game, MaxSprites, MaxText, device)
        {
        }

        #endregion

        public void RenderText(SpriteFont font, string text, Vector2 pos, Color col, float drawOrder)
        {
            RenderText(font, text, pos, Vector2.One, col, drawOrder);
        }

        public void RenderText(SpriteFont font, string text, Vector2 pos, Vector2 scale, Color col, float drawOrder)
        {
            if (currentText >= maxText)
            {
                return;
                //throw new Exception("Overloaded in RenderText function.");
            }
            textArray[currentText] = new TextInfo(font, text, pos, scale, col, drawOrder, SpritePositionMode.AbsoluteCoordinates);
            currentText++;
        }

        public void Render2D(Texture2D tex, Vector2 pos, Vector2 size, Color col, float drawOrder)
        {
            Render2D(tex, pos, size, null, col, drawOrder);
        }

        public void Render2D(Texture2D tex, Vector2 pos, Vector2 size, Rectangle? sourceRect, Color col, float drawOrder)
        {
            if (currentSprites >= maxSprites)
            {
                return;
            }

            spriteArray[currentSprites] = new SpriteInfo(tex, pos, size, col, drawOrder, sourceRect, SpritePositionMode.AbsoluteCoordinates);
            currentSprites++;
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            //TODO
            //Remove this dirty hack
            //The problem was that globalforms were being drawn on top of hte cursor
            //The cursor is drawn with Render2D component... so it needs to have its own spritebatch
            //Also, globalforms should be a component
            GlobalForms.Draw(gameTime);


            float viewWidth = device.Viewport.Width;
            float viewHeight = device.Viewport.Height;
            Vector2 view = new Vector2(viewWidth, viewHeight);

            sprites.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.FrontToBack, SaveStateMode.None);
            //Draw 2d sprites
            for (int i = 0; i < currentSprites; i++)
            {
                spriteArray[i].Draw(sprites, view);
            }

            for (int i = 0; i < currentText; i++)
            {
                textArray[i].Draw(sprites, view);
            }

            sprites.End();
            currentSprites = 0;
            currentText = 0;

        }





    }
}
