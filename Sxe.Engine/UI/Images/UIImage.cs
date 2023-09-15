using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sxe.Engine.UI
{
    public enum DrawMode
    {
        Stretch = 0,
        BordersFixed,
        VerticalFixed,
        HorizontalFixed
    }

    /// <summary>
    /// Wrapper for images for user interface
    /// Planned to be expanded to handle animated images later on
    /// </summary>
    public class UIImage
    {
        Texture2D image;
        Rectangle sourceRectangle;
        Vector4 color = Color.White.ToVector4();
        DrawMode drawMode;

        bool autoSize = false;

        public SpriteEffects SpriteEffects
        {
            get;
            set;
        }

        public DrawMode DrawMode
        {
            get { return drawMode; }
            set { drawMode = value; }
        }

        public UIImage()
        {
            autoSize = true;
        }   
        

        public UIImage(Texture2D input)
            : this(input, new Rectangle(0, 0, input.Width, input.Height))
        {
            autoSize = true;
        }

        public UIImage(Texture2D input, Rectangle rect)
        {
            image = input;
            sourceRectangle = rect;
        }


        public Texture2D Value
        {
            get
            {
                return image;
            }
            set 
            { 
                image = value;
                if (autoSize && image != null)
                    sourceRectangle = new Rectangle(0, 0, image.Width, image.Height);
            }
        }

        public Color Color
        {
            get { return new Color(color); }
            set { color = value.ToVector4(); }
        }

        public virtual Rectangle SourceRectangle
        {
            get
            {
                return sourceRectangle;
            }
        }

        public virtual void Update(GameTime gameTime) { }

        public void Draw(SpriteBatch batch, Rectangle destination, Color inColor)
        {
            this.Draw(batch, this.SourceRectangle, destination, inColor, this.SpriteEffects);
        }

        public void Draw(SpriteBatch batch, Rectangle sourceRect, Rectangle destination, Color inColor)
        {
            this.Draw(batch, sourceRect, destination, inColor, this.SpriteEffects);
        }

        public virtual void Draw(SpriteBatch batch, Rectangle sourceRect, Rectangle destination, Color inColor, SpriteEffects spriteEffects)
        
        {
            if (image == null)
                return;

            Vector4 colorMultiplier = inColor.ToVector4();
            Vector4 realColor = color * colorMultiplier;
            Color outColor = new Color(realColor);

            switch (DrawMode)
            {
                default:
                case DrawMode.Stretch:

                    batch.Draw(this.image, destination, sourceRect, outColor, 0f, Vector2.Zero, spriteEffects, 1f); 
                    break;
                case DrawMode.BordersFixed:
                    int cornerWidth = (sourceRect.Width - 2) / 2;
                    int cornerHeight = (sourceRect.Height - 2) / 2;
                    //Draw top left corner
                    batch.Draw(this.image, new Rectangle(destination.X, destination.Y, cornerWidth, cornerHeight),
                        new Rectangle(0, 0, cornerWidth, cornerHeight), outColor);

                    //Top middle
                    batch.Draw(this.image, new Rectangle(destination.X + cornerWidth, destination.Y,
                        destination.Width - 2 * cornerWidth, cornerHeight),
                        new Rectangle(cornerWidth, 0, 2, cornerHeight), outColor);

                    //Top right
                    batch.Draw(this.image, new Rectangle(destination.X + destination.Width - cornerWidth,
                        destination.Y, cornerWidth, cornerHeight),
                        new Rectangle(cornerWidth + 2, 0, cornerWidth, cornerHeight), outColor);

                    //Left
                    batch.Draw(this.image, new Rectangle(destination.X, destination.Y + cornerHeight,
                        cornerWidth, destination.Height - 2 * cornerHeight),
                        new Rectangle(0, cornerHeight, cornerWidth, 2), outColor);

                    //Middle
                    batch.Draw(this.image, new Rectangle(destination.X + cornerWidth, destination.Y + cornerHeight,
                        destination.Width - 2 * cornerWidth, destination.Height - 2 * cornerHeight),
                        new Rectangle(cornerWidth, cornerHeight, 2, 2), outColor);

                    //Right
                    batch.Draw(this.image, new Rectangle(destination.X + destination.Width - cornerWidth, destination.Y + cornerHeight,
                        cornerWidth, destination.Height - 2 * cornerHeight),
                        new Rectangle(cornerWidth + 2, cornerHeight, cornerWidth, 2), outColor);

                    //Bottom left
                    batch.Draw(this.image, new Rectangle(destination.X, destination.Y + destination.Height - cornerHeight, cornerWidth, cornerHeight),
                        new Rectangle(0, cornerHeight + 2, cornerWidth, cornerHeight), outColor);
                    
                    //Bottom middle
                    batch.Draw(this.image, new Rectangle(destination.X + cornerWidth, destination.Y + destination.Height - cornerHeight, destination.Width - 2 * cornerWidth, cornerHeight),
                        new Rectangle(cornerWidth, cornerHeight + 2, 2, cornerHeight), outColor);

                    //Bottom right
                    batch.Draw(this.image, new Rectangle(destination.X + destination.Width - cornerWidth, destination.Y + destination.Height - cornerHeight,
                        cornerWidth, cornerHeight), new Rectangle(cornerWidth + 2, cornerHeight + 2, cornerWidth, cornerHeight), outColor);
                    break;
 

            }
        }

        public virtual void UnloadContent()
        {
        }
    }
}
