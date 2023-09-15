using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Sxe.Engine.UI
{
    public class AnimatedImage : UIImage
    {
        public override Rectangle SourceRectangle
        {
            get
            {
                if (this.CurrentAnimation == null)
                    return Rectangle.Empty;

                if (this.CurrentAnimation.CurrentImage == null)
                    return Rectangle.Empty;

                return this.CurrentAnimation.CurrentImage.SourceRectangle;
            }
        }

        public static AnimatedImage Load(ContentManager content, string baseName, int startIndex, int endIndex, int interval)
        {
            AnimatedImage outImage = new AnimatedImage();
            Animation animation = new Animation();
            
            
            for (int i = startIndex; i <= endIndex; i++)
            {
                animation.AddFrame(new UIImage(content.Load<Texture2D>(baseName + i.ToString())));
            }
            animation.AnimationInterval = interval;

            outImage.AddAnimation(baseName, animation);

            return outImage;
           
        }


        List<Animation> animations = new List<Animation>();
        Dictionary<string, int> animationNames = new Dictionary<string,int>();
        int currentAnimation = 0;

        public Animation CurrentAnimation
        {
            get { return animations[currentAnimation]; }
        }

        public void AddAnimation(string name, Animation animation)
        {
            animations.Add(animation);
            animationNames.Add(name, animations.Count - 1);
        }

        public void Play(int animation)
        {
            currentAnimation = animation;
            CurrentAnimation.Play();
        }

        public override void Update(GameTime gameTime)
        {
            CurrentAnimation.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch batch, Rectangle sourceRectangle, Rectangle destination, Microsoft.Xna.Framework.Color inColor, SpriteEffects spriteEffects)
        {
            CurrentAnimation.CurrentImage.Draw(batch, sourceRectangle, destination, inColor, spriteEffects);
            //base.Draw(batch, destination, inColor);
        }



    }

    public class Animation
    {
        List<UIImage> frames = new List<UIImage>();

        int currentAnimationTicks;
        int animationInterval = 120;
        int currentFrame = 0;
        bool repeat = true;
        bool isPlaying = true;

        public int TotalFrames
        {
            get { return frames.Count; }
        }

        public int CurrentFrame
        {
            get { return currentFrame; }
        }

        public UIImage CurrentImage
        {
            get { return frames[currentFrame]; }
        }

        public int AnimationInterval
        {
            get { return animationInterval; }
            set { animationInterval = value; }
        }

        public void AddFrame(UIImage frame)
        {
            frames.Add(frame);
        }

        public void Play()
        {
            isPlaying = true;
            currentAnimationTicks = 0;
            currentFrame = 0;
        }

        public void Pause()
        {
            isPlaying = false;
        }

        public void Rewind()
        {
            currentFrame = 0;
        }

        public bool Repeat
        {
            get { return repeat; }
            set { repeat = value; }
        }

        public void Update(GameTime gameTime)
        {
            if (isPlaying)
            {
                currentAnimationTicks += (int)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (currentAnimationTicks > animationInterval)
                {
                    currentAnimationTicks = currentAnimationTicks - animationInterval;
                    currentFrame++;

                    if (currentFrame > frames.Count - 1)
                    {
                        if (repeat)
                            currentFrame = 0;
                        else
                            currentFrame = frames.Count - 1;
                    }
                }
            }

        }



    }
}
