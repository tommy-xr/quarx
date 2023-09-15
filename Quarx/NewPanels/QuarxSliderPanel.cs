using System;
using System.Collections.Generic;
using System.Text;

using Sxe.Engine.UI;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Quarx
{
    public class QuarxSliderPanel : BaseSliderPanel
    {
        UIImage sliderImage;

        int sliderWidth = 20;

        public UIImage SliderImage
        {
            get { return sliderImage; }
            set { sliderImage = value; }
        }

        public int SliderWidth
        {
            get { return sliderWidth; }
            set { sliderWidth = value; }
        }


        public QuarxSliderPanel()
        {
            InitializeComponent();
        }

        void InitializeComponent()
        {
        }

        public override void Paint(Microsoft.Xna.Framework.Graphics.SpriteBatch sb, Microsoft.Xna.Framework.Point positionOffset, Microsoft.Xna.Framework.Vector2 scale)
        {
            base.Paint(sb, positionOffset, scale);

            Rectangle rect = GetDestinationRectangle(positionOffset, scale);

            double position = ((double)(Value - Minimum))/((double)(Maximum-Minimum));

            int xPosition = (int)(position * rect.Width);

            if (SliderImage != null)
                SliderImage.Draw(sb, new Rectangle(rect.X + xPosition - sliderWidth / 2, rect.Y, sliderWidth, rect.Height), Color.White);

        }

        public override bool Increment()
        {
            bool changed = base.Increment();
                if(changed)
                    Audio.PlayCue("slider");
            return changed;
        }

        public override bool Decrement()
        {
            bool changed = base.Decrement();
            if (changed)
                Audio.PlayCue("slider");
            return changed;
        }
    }
}
