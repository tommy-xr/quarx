using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework.Input;

namespace Sxe.Engine.Graphics
{
    public class FreeRoamingCamera : Camera
    {
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            KeyboardState keyboard = Keyboard.GetState();


            float speed = 1f;
            float multiplier = speed * (float)gameTime.TotalGameTime.TotalSeconds;
            if (keyboard.IsKeyDown(Keys.W))
            {
                this.Position += this.Forward * multiplier;
            }

            if (keyboard.IsKeyDown(Keys.S))
            {
                this.Position -= this.Forward * multiplier;
            }

            if (keyboard.IsKeyDown(Keys.A))
            {
                this.Position -= this.Right * multiplier;
            }

            if (keyboard.IsKeyDown(Keys.D))
            {
                this.Position += this.Right * multiplier;
            }


            base.Update(gameTime);
        }
    }
}
