using System;
using System.Collections.Generic;
using System.Text;

using Sxe.Engine.UI;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Quarx
{
    /// <summary>
    /// This is a little helper thing that pops up to the side of stuff
    /// </summary>
    public class HelperPanel : CompositePanel
    {
        public const float HoldTime = 30f;
        float hold = 0f;

        public override void Show()
        {
            if(hold < 0.1f)
            base.Show();
        }

        public override void Update(GameTime gameTime)
        {
            this.BackColor = new Color(255, 255, 255, this.TransitionAlpha);

            if (this.TransitionAlpha >= 255)
            {
                hold += (float)gameTime.ElapsedGameTime.TotalSeconds / HoldTime;
                if (hold >= 1f)
                    this.Hide();
            }

            base.Update(gameTime);
        }
    }

    
}
