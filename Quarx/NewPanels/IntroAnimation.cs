using System;
using System.Collections.Generic;
using System.Text;

using Sxe.Engine.UI;

namespace Quarx 
{
    public class IntroAnimation : CompositePanel
    {

        AnimatedImage intro;
        bool isComplete = false;

        const int MaxFrame = 65;

        public bool IsComplete
        {
            get { return intro.CurrentAnimation.CurrentFrame >= MaxFrame-1; }
        }

        public IntroAnimation()
        {
            InitializeComponent();
        }

        public override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            base.LoadContent(content);

            intro = new AnimatedImage();
            Animation animation = new Animation();
            animation.AnimationInterval = 50; // 1 s / 18 fps = 0.056 s or 56 ms

            for (int i = 0; i <= MaxFrame; i++)
            {
                string name = "Intro\\Ionixx0";
                if(i < 10)
                    name += "0";
                name += i.ToString();
                animation.AddFrame(this.LoadImage(content, name));
            }

            intro.AddAnimation("intro", animation);
            intro.Play(0);

            this.Image = intro;
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {

            base.Update(gameTime);
        }

        public void InitializeComponent()
        {
            // 
            // IntroAnimation
            // 
            this.Size = new Microsoft.Xna.Framework.Point(128, 128);

        }

    }
}
