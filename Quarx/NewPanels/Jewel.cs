using System;
using System.Collections.Generic;
using System.Text;


using Sxe.Engine.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Quarx.NewPanels
{
    public enum JewelType
    {
        A = 0,
        B,
        X,
        Y
    }


    public class Jewel : Panel
    {
        JewelType jewelType;
        [ReloadContent]
        public JewelType JewelType
        {
            get { return jewelType; }
            set { jewelType = value; }
        }

        public override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            string baseName = "Jewels/";
            switch (jewelType)
            {
                case JewelType.A:
                    baseName += "a";
                    break;
                case JewelType.B:
                    baseName += "b";
                    break;
                case JewelType.X:
                    baseName += "x";
                    break;
                case JewelType.Y:
                default:
                    baseName += "y";
                    break;

            }

            AnimatedImage image = new AnimatedImage();
            Animation animation = new Animation();

            for (int i = 0; i <= 25; i++)
                animation.AddFrame(LoadImage(content, baseName + "0"));

            for (int i = 0; i <= 4; i++)
            {
                animation.AddFrame(LoadImage(content, baseName + i.ToString()));
  
            }
            animation.AnimationInterval = 50;
            animation.Repeat = true;

            image.AddAnimation("jewel", animation);
            image.Play(0);
            //image.

            this.Image = image;

            base.LoadContent(content);
        }
    }
}
