using System;
using System.Collections.Generic;
using System.Text;


using Sxe.Library.Utilities;

using System.ComponentModel;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

#if !XBOX
using System.ComponentModel.Design;
using Sxe.Design;
#endif

namespace Sxe.Engine.UI.Buttons
{
#if !XBOX
    [ToolboxItemFilter("AnarchyUI", ToolboxItemFilterType.Require)]
#endif
    public class ColorButton : ButtonBase
    {
        float colorTransition = 1.0f;
        Color defaultColor = Color.White;
        Color overColor = Color.White;
        Color clickColor = Color.White;
        TimeSpan colorTransitionTime = TimeSpan.FromSeconds(0.1);

        public Color DefaultColor
        {
            get { return defaultColor; }
            set { defaultColor = value; }
        }

        public Color OverColor
        {
            get { return overColor; }
            set { overColor = value; }
        }

        public Color ClickColor
        {
            get { return clickColor; }
            set { clickColor = value; }
        }
#if !XBOX
        [TypeConverter(typeof(Sxe.Design.TimeSpanConverter))]
#endif
        public TimeSpan ColorTransitionTime
        {
            get { return colorTransitionTime; }
            set { colorTransitionTime = value; }
        }

        public override void PerformClick(int index)
        {
            base.PerformClick(index);
            colorTransition = 0.0f;
        }

        public override void Over(int index)
        {
            base.Over(index);
            colorTransition = 0.0f;
        }

        public override void Leave(int index)
        {
            base.Leave(index);
            colorTransition = 0.0f;
        }

        public override void PaintNormal(SpriteBatch sb, Microsoft.Xna.Framework.Rectangle destinationRect)
        {
            base.PaintNormal(sb, destinationRect);
            this.BackColor = CalculateColor(this.BackColor, DefaultColor, colorTransition);
            PaintImage(sb, destinationRect, this.BackColor);
        }

        public override void PaintPressed(SpriteBatch sb, Microsoft.Xna.Framework.Rectangle destinationRect)
        {
            base.PaintPressed(sb, destinationRect);
            this.BackColor = CalculateColor(ClickColor, OverColor, colorTransition);
            PaintImage(sb, destinationRect, this.BackColor);

        }

        public override void PaintOver(SpriteBatch sb, Microsoft.Xna.Framework.Rectangle destinationRect)
        {
            base.PaintOver(sb, destinationRect);
            this.BackColor = CalculateColor(this.BackColor, OverColor, colorTransition);
            PaintImage(sb, destinationRect, this.BackColor);

        }

        void PaintImage(SpriteBatch sb, Rectangle destination, Color color)
        {
            if(Image != null)
            Image.Draw(sb, destination, color);
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Update(gameTime);
            this.colorTransition += (float)(gameTime.ElapsedGameTime.TotalSeconds / ColorTransitionTime.TotalSeconds);

            if (this.colorTransition > 1.0f)
                colorTransition = 1.0f;

            //this.Text = this.colorTransition.ToString();
        }

        Color CalculateColor(Color startColor, Color endColor, float amount)
        {
            return Sxe.Library.Utilities.ColorUtilities.Lerp(startColor, endColor, amount);
        }





    }
}
