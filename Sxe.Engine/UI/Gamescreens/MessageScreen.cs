using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Sxe.Engine.UI
{
    public class MessageScreen : BaseScreen
    {

        public MessageScreen()
        {
            //messagePanel = new Panel(this.Panel, new Point(0, 200), new Point(800, 200),
            //    new UIImage(content.Load<Texture2D>("gradient")));
            //messagePanel.BackColor = Color.White;
            TransitionOnTime = TimeSpan.FromSeconds(0.2);
            TransitionOffTime = TimeSpan.FromSeconds(0.2);
            IsPopup = true;
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (GameScreenService != null)
                GameScreenService.FadeBackBufferToBlack(TransitionAlpha * 2 / 3);
            
            base.Draw(spriteBatch, gameTime);
        }


    }
}
