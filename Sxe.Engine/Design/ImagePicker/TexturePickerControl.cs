using System;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using EnvDTE;

using System.Windows.Forms;

using Sxe.Library;

namespace Sxe.Design.ImagePicker
{



    public class TexturePickerControl : GraphicsDeviceControl
    {
        public class TextureCacheEntry
        {
            Texture2D texture;
            ContentManager content;
            int width;
            int height;
            int levels;

            bool loadComplete = false;

            public Texture2D Texture
            {
                get 
                {
                    if (!loadComplete)
                        return null;

                    return texture; 
                }
            }

            public int Width
            {
                get { return width; }
            }

            public int Height
            {
                get { return height; }
            }

            public int Levels
            {
                get { return levels; }
            }

            public TextureCacheEntry(IServiceProvider services, string directory, string textureName)
            {
                this.content = new ContentManager(services, directory);
                this.texture = content.Load<Texture2D>(textureName);
                this.width = this.texture.Width;
                this.height = this.texture.Height;
                this.levels = this.texture.LevelCount;
                this.loadComplete = true;
            }

            public void Unload()
            {
                this.loadComplete = false;
                if (content != null)
                {
                    content.Unload();
                    content.Dispose();
                }
                
            }
        }

        SpriteBatch batch;
        ContentManager content;
        string directory;

        const int imageWidth = 80; //width of actual display texture
        const int imageHeight = 80; //height of actual display texture

        const int borderWidth = 2; //Width of selection border
        const int borderHeight = 2; //Height of selection border

        const int titleHeight = 16; //Height of title

        const int paddingWidth = 1;
        const int paddingHeight = 4;
        const int marginX = 8;
        const int marginY = 8;

        int numX = 0;
        int numY = 0;

        int currentX = 0;

        Texture2D blank;
        SpriteFont font;
        StringCollection stringCollection; //keep a list of all images
        StringCollection currentImages = new StringCollection(); //keep a list of images in view
        private string selectedTexture = string.Empty;


        Cache<string, TextureCacheEntry> textureCache; 

        public event EventHandler<EventArgs> ScrollParamsChanged;

        public int MinScroll
        {
            get { return 0; }
        }
        public int MaxScroll
        {
            get { return stringCollection.Count / numX + 1;}
        }
        public int SmallChange
        {
            get { return 1; }
        }
        public int LargeChange
        {
            get { return numY - 1; }
        }

        public string SelectedValue
        {
            get { return this.selectedTexture; }
            set { this.selectedTexture = value; }
        }


        private int NumImages
        {
            get { return numX * numY; }
        }


        public TexturePickerControl()
        {
            InitializeComponent();
            this.textureCache = new Cache<string, TextureCacheEntry>(30,this.LoadTexture, this.UnloadTexture, true, false);
            //Async unload because we could crash sometime due to a race conditoin:
            //We'd get a good reference to a texture for batch.Draw, and then unload would be called sometime bewteen batch.Draw and batch.End,
            //and we'd now have a reference to disposed object at that point... which is bad

            //this.textureCache.LoadCallback = LoadTexture;
        }

        private TextureCacheEntry LoadTexture(string sz)
        {
            return new TextureCacheEntry(this.Services, this.directory, sz);
        }

        private void UnloadTexture(string sz, TextureCacheEntry entry)
        {
            entry.Unload();
        }

        protected override void Initialize()
        {
            base.Initialize();

            if (DesignMode)
                return;

            //content = new ContentManager(this.Services);
            batch = new SpriteBatch(this.GraphicsDevice);

            //MessageBox.Show(directory);
            content = new ContentManager(this.Services, directory);

            blank = content.Load<Texture2D>(DesignerItems.Blank);
            font = content.Load<SpriteFont>(DesignerItems.Font);

        }

        public void SetImages(StringCollection strings)
        {
            stringCollection = strings;
            currentX = 0;
            SetupCurrentImages();

            

        }

        public void Scroll(int value)
        {
            currentX = value;
            SetupCurrentImages();
        }

        public void SetContentDirectory(string contentDirectory)
        {
            directory = contentDirectory;         
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            SetupSize();
            base.OnSizeChanged(e);
        }


        private void SetupSize()
        {
            numX = (this.Width) / (imageWidth + paddingWidth + borderWidth * 2);
            numY = (this.Height) / (imageHeight + paddingHeight + borderHeight * 2 + titleHeight*2);
            FireScrollChanged();
        }

        private void SetupCurrentImages()
        {

            currentImages.Clear();

            int startIndex = numX * currentX;
            for (int i = startIndex; i < startIndex + NumImages; i++)
            {
                if(stringCollection.Count > i)
                currentImages.Add(stringCollection[i]);
            }

            FireScrollChanged();

            this.Invalidate();
        }

        protected override void Draw()
        {
            if (batch != null)
            {
                batch.Begin();

                batch.Draw(blank, new Rectangle(0, 0, 5, 5), Color.Yellow);

                int current = 0;

                for (int y = 0; y < numY; y++)
                {
                    for (int x = 0; x < numX; x++)
                    {


                        if (currentImages.Count > current)
                        {

                            string image = currentImages[current];
                            TextureCacheEntry entry = textureCache.RequestItem(image);

                            //Rectangle rectangle;
                            //rectangle.X = paddingWidth + borderWidth + (imageWidth + paddingWidth + borderWidth*2) * x;
                            //rectangle.Y = paddingHeight + borderHeight+ titleHeight +
                            //    (imageHeight + paddingHeight + borderHeight*2 + titleHeight*2) * y;
                            //rectangle.Width = imageWidth;
                            //rectangle.Height = imageHeight;

                            Rectangle borderRectangle;
                            borderRectangle.X = paddingWidth + (imageWidth + borderWidth * 2 + paddingWidth) * x;
                            borderRectangle.Y = paddingHeight + titleHeight + (imageHeight + borderHeight * 2 + titleHeight * 2 + paddingHeight) * y;
                            borderRectangle.Width = imageWidth + borderWidth * 2;
                            borderRectangle.Height = imageHeight + borderHeight * 2;

                            Rectangle rectangle;
                            rectangle.X = borderRectangle.X + borderWidth;
                            rectangle.Y = borderRectangle.Y + borderHeight;
                            rectangle.Width = imageWidth;
                            rectangle.Height = imageHeight;



                            //
                            //Draw top title
                            Rectangle topRectangle = new Rectangle(borderRectangle.X, borderRectangle.Y - titleHeight, borderRectangle.Width, titleHeight);
                            Rectangle bottomRectangle = new Rectangle(borderRectangle.X, borderRectangle.Y + borderRectangle.Width, borderRectangle.Width, titleHeight);

                            batch.Draw(blank, topRectangle, Color.Red);

                            Vector2 textPosition;
                            string text = "Unknown";
                            float scale;


                            this.GetTextParams(topRectangle, image, font, Sxe.Engine.UI.HorizontalAlignment.Center, Sxe.Engine.UI.VerticalAlignment.Middle,
                                out textPosition, out text, out scale);
                            batch.DrawString(font, text, textPosition, Color.White, 0f, Vector2.One, scale, SpriteEffects.None, 0f);
                            
                            batch.Draw(blank, bottomRectangle, Color.Red);

                            Color color = Color.White;
                            if (image == this.selectedTexture)
                                color = Color.Blue;


                            batch.Draw(blank, borderRectangle, color);
                            
                            Texture2D tex = null;

                            int width = 0;
                            int height = 0;
                            int levels = 0;

                            if (entry != null)
                            {
                                tex = entry.Texture;
                                width = entry.Width;
                                height = entry.Height;
                                levels = entry.Levels;
                            }

                            if (tex == null)
                            {
                                tex = blank;
                            }
                            else
                            {
                                this.GetTextParams(bottomRectangle, String.Format("{0}x{1}x{2}", width, height, levels),
                                    font, Sxe.Engine.UI.HorizontalAlignment.Center, Sxe.Engine.UI.VerticalAlignment.Middle,
                                    out textPosition, out text, out scale);
                                batch.DrawString(font, text, textPosition, Color.White, 0f, Vector2.One, scale, SpriteEffects.None, 0f);
                            }

                            batch.Draw(tex, rectangle, Color.White);

                            //}
                            //catch (Exception ex)
                            //{
                            //    string name = "";
                            //    if (current < currentImages.Count)
                            //        name = currentImages[current];

                            //    MessageBox.Show("[" + name + "]:" + ex.Message);
                            //}

                            current++;
                        }

                    }
                }

                //if(blank != null)
                //    batch.Draw(blank, new Rectangle(0, 0, Width, Height), Color.Red);
                

                batch.End();
            }

            Invalidate();

            base.Draw();
        }

        private void FireScrollChanged()
        {
            if (ScrollParamsChanged != null)
                ScrollParamsChanged(this, EventArgs.Empty);
        }

        //TODO: Refactor this into a common library ! this is a useful damn function
        private void GetTextParams(Rectangle rect, string text, SpriteFont font,
            Sxe.Engine.UI.HorizontalAlignment horizontalAlignment, Sxe.Engine.UI.VerticalAlignment verticalAlignment,
            out Vector2 position, out string croppedText, out float scale)
        {
            scale = 1.0f;

            //First, measure the string, and see where we're at
            Vector2 measurement = font.MeasureString(text);

            if (measurement.Y > rect.Height)
            {
                scale = rect.Height / measurement.Y;
            }

            //We need to scale the x too now, otherwise it'll look bogue
            measurement *= scale;

            croppedText = text;

            //Crop this text til it fits
            while (measurement.X > rect.Width)
            {
                croppedText = croppedText.Substring(0, croppedText.Length - 1);
                measurement = font.MeasureString(croppedText);
                measurement *= scale;
            }

            //Now position the text
            switch (horizontalAlignment)
            {
                case Sxe.Engine.UI.HorizontalAlignment.Left:
                    position.X = rect.X;
                    break;
                case Sxe.Engine.UI.HorizontalAlignment.Right:
                    position.X = rect.X + rect.Width - measurement.X;
                    break;
                case Sxe.Engine.UI.HorizontalAlignment.Center:
                default:
                    position.X = rect.X + rect.Width / 2 - measurement.X / 2;
                    break;
            }

            switch (verticalAlignment)
            {
                case Sxe.Engine.UI.VerticalAlignment.Bottom:
                    position.Y = rect.Y + rect.Height - measurement.Y;
                    break;
                case Sxe.Engine.UI.VerticalAlignment.Top:
                    position.Y = rect.Y;
                    break;
                case Sxe.Engine.UI.VerticalAlignment.Middle:
                default:
                    position.Y = rect.Y + rect.Height / 2 - measurement.Y / 2;
                    break;

            }
            return;
        }


        void InitializeComponent()
        {
        }
    }
}
