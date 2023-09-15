using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel;


using Sxe.Engine.UI;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

#if !XBOX
using Sxe.Design;
#endif

namespace Quarx
{
    /// <summary>
    /// Special class for displaying a lightning effect in the background
    /// </summary>
    public class LightningBackgroundPanel : Panel
    {
        UIImage atomNormalImage;
        UIImage atomBrightImage;

        UIImage[] lightningImages;

        Lightning[] lightnings;

        AtomSymbol[] symbols;


        #region Properties
        string atomNormalImagePath;
#if !XBOX
        [TypeConverter(typeof(TextureContentNameConverter))]
#endif
        public string AtomNormalImagePath
        {
            get { return atomNormalImagePath; }
            set { atomNormalImagePath = value; }
        }

        string atomBrightImagePath;
#if !XBOX
        [TypeConverter(typeof(TextureContentNameConverter))]
#endif
        public string AtomBrightImagePath
        {
            get { return atomBrightImagePath; }
            set { atomBrightImagePath = value; }
        }

        string lightningPath1;
#if !XBOX
        [TypeConverter(typeof(TextureContentNameConverter))]
#endif
        public string LightningPath1
        {
            get { return lightningPath1; }
            set { lightningPath1 = value; }
        }

        string lightningPath2;
#if !XBOX
        [TypeConverter(typeof(TextureContentNameConverter))]
#endif
        public string LightningPath2
        {
            get { return lightningPath2; }
            set { lightningPath2 = value; }
        }

        string lightningPath3;
#if !XBOX
        [TypeConverter(typeof(TextureContentNameConverter))]
#endif
        public string LightningPath3
        {
            get { return lightningPath3; }
            set { lightningPath3 = value; }
        }

        string lightningPath4;
#if !XBOX
        [TypeConverter(typeof(TextureContentNameConverter))]
#endif
        public string LightningPath4
        {
            get { return lightningPath4; }
            set { lightningPath4 = value; }
        }

        #endregion

        public override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            atomNormalImage = LoadImage(content, atomNormalImagePath);
            atomBrightImage = LoadImage(content, atomBrightImagePath);

            //lightningImages = new UIImage[4];
            //lightningImages[0] = LoadImage(content, LightningPath1);
            //lightningImages[1] = LoadImage(content, LightningPath2);
            //lightningImages[2] = LoadImage(content, LightningPath3);
            //lightningImages[3] = LoadImage(content, LightningPath4);

            symbols = new AtomSymbol[10];
            for (int i = 0; i < symbols.Length; i++)
            {
                symbols[i] = new AtomSymbol();
                symbols[i].Image = LoadImage(content, "Menu\\molecule");
            }

            lightnings = new Lightning[4];
            for (int i = 0; i < lightnings.Length; i++)
            {
                //lightnings[i] = new Lightning(lightningImages[i], i, this.Size.X);
                lightnings[i] = new Lightning(null, i, this.Size.X);
                lightnings[i].MinDelay = 1.0;
                lightnings[i].MaxDelay = 10.0;
                lightnings[i].MinDuration = 0.1;
                lightnings[i].MaxDuration = 0.5;

            }

            //VideoImage.VideoImage backImage = new VideoImage.VideoImage("AVI/backfullshort", content.ServiceProvider);
            //backImage.Play();
            //this.Image = backImage;


            base.LoadContent(content);
        }

        public override void Paint(SpriteBatch sb, Point positionOffset, Vector2 scale)
        {
            base.Paint(sb, positionOffset, scale);

            Rectangle rect = GetDestinationRectangle(positionOffset, scale);

            //for (int i = 0; i < lightnings.Length; i++)
            //    lightnings[i].Draw(sb, positionOffset, scale);

            for (int i = 0; i < symbols.Length; i++)
                symbols[i].Draw(sb, positionOffset, scale);

            //Get the sum of all intensities
            float intensity = 0.0f;
            for (int i = 0; i < lightnings.Length; i++)
                intensity += lightnings[i].Intensity / 4f;

            intensity = MathHelper.Clamp(intensity, 0.0f, 1.0f);


            Color normalColor = new Color(255, 255, 255, 200);
            Color brightColor = new Color(255, 255, 255, (byte)(255 * intensity));

            if (atomNormalImage != null)
                atomNormalImage.Draw(sb, rect, normalColor);

            if (atomBrightImage != null)
                atomBrightImage.Draw(sb, rect, brightColor);

    

        }


        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            for (int i = 0; i < lightnings.Length; i++)
                lightnings[i].Update(gameTime);

            for (int i = 0; i < symbols.Length; i++)
                symbols[i].Update(gameTime);
        }

        public class AtomSymbol
        {
            UIImage image;

            public UIImage Image
            {
                get { return image; }
                set { image = value; }
            }

            double nextAppearTime = 0.0;

            double minDelay = 1.0;
            double maxDelay = 5.0f;

            double minDuration = 1.0;
            double maxDuration = 2.0;

            int minSize = 75;
            int maxSize = 200;

            Point position;
            int size = 50;
            double appearDuration;
            float intensity = 0.0f;
            bool isRampUp = true;

            static Random random = new Random();

            public byte Alpha
            {
                get { return ((byte)(255 * intensity)); }
            }

            public void Update(GameTime gameTime)
            {
                if (nextAppearTime == 0.0 || intensity < 0.0f )
                {
                    nextAppearTime = gameTime.TotalGameTime.TotalSeconds + (random.NextDouble() * (maxDelay - minDelay)) + minDelay;
                    appearDuration = minDuration + (random.NextDouble() * (maxDuration - minDuration));
                    size = random.Next(minSize, maxSize);
                    intensity = 0.0f;
                    int xPos = random.Next(0, 800);
                    int yPos = random.Next(0, 600);

                    isRampUp = true;
                    position = new Point(xPos, yPos);

                }
                else
                {
                    double amount = gameTime.ElapsedGameTime.TotalSeconds;

                    if (isRampUp)
                    {
                        amount /= appearDuration;
                        intensity += (float)amount;

                        if (intensity >= 1.0)
                            isRampUp = false;
                    }
                    else
                    {
                        amount /= (appearDuration);
                        intensity -= (float)amount;
                    }
                }
            }

            public void Draw(SpriteBatch sb, Point positionOffset, Vector2 scale)
            {
                if(image != null)
                image.Draw(sb, new Rectangle(position.X, position.Y, size, size),
                    new Color(255, 255, 255, Alpha));
            }

        }

        public class Lightning
        {
            UIImage image;
            double nextStrike;
            float intensity;
            public float Intensity
            {
                get { return intensity; }
            }

            double minDelay;
            public double MinDelay
            {
                get { return minDelay; }
                set { minDelay = value; }
            }

            double maxDelay;
            public double MaxDelay
            {
                get { return maxDelay; }
                set { maxDelay = value; }
            }

            double minDuration;
            public double MinDuration
            {
                get { return minDuration; }
                set { minDuration = value; }
            }

            double maxDuration;
            public double MaxDuration
            {
                get { return maxDuration; }
                set { maxDuration = value; }
            }

            float currentDuration;
            Random random;
            int xCoord = 0;
            int maxSize;

            bool isRampUp = true;

            byte Alpha
            {
                get { return (byte)(255 * intensity); }
            }

            public Lightning(UIImage lightningImage, int seed, int inSize)
            {
                image = lightningImage;
                nextStrike = 0.0f;
                intensity = 0.0f;
                maxSize = inSize;


                random = new Random(seed);



            }

            public void Update(GameTime gameTime)
            {
                if (nextStrike == 0.0f || intensity < 0.0f)
                {
                    nextStrike = (random.NextDouble() * (maxDelay - minDelay)) + minDelay + (float)gameTime.TotalGameTime.TotalSeconds;
                    intensity = 0.0f;
                    isRampUp = true;
                }



                if (nextStrike < gameTime.TotalGameTime.TotalSeconds)
                {
                    //Start a strike
                    if (intensity == 0.0f && isRampUp)
                    {
                        currentDuration = (float)(random.NextDouble() * (maxDuration - minDuration) + minDuration);
                        xCoord = random.Next(0, this.maxSize);

                    }
                    else if (intensity >= 1.0f)
                    {
                        isRampUp = false;
                    }

                    if (isRampUp)
                        intensity += (float)gameTime.ElapsedGameTime.TotalSeconds / currentDuration;
                    else
                        intensity -= (float)gameTime.ElapsedGameTime.TotalSeconds / (currentDuration * 2);

                }


            }

            public void Draw(SpriteBatch sb, Point positionOffset, Vector2 scale)
            {
                int xSize = 50;

                //if(image != null)
                //image.Draw(sb, new Rectangle(xCoord, 0, xSize, (int)(600 * scale.Y)), new Color(255, 255, 255, Alpha));
                //sb.Draw(image, new Rectangle(50, 0), new Random(200, 600), new Color(255, 255, 255, Alpha));
            }
        }
    }
}
