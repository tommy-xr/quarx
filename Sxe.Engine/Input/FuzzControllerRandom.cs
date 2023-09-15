using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;

namespace Sxe.Engine.Input
{
    /// <summary>
    /// The goal of this controller is to create random input and stress test a game
    /// Basically simulate button mashing and make sure there are no crashes
    /// </summary>
    public class FuzzControllerRandom : KeyboardMouseGameController
    {
        static Random staticRandom = new Random();

        Random random;

        public FuzzControllerRandom(IServiceProvider services, ContentManager content, int playerIndex)
            : base(services, content, playerIndex)
        {
            random = new Random(playerIndex * staticRandom.Next());
        }

        public override void ControllerUpdate(Microsoft.Xna.Framework.GameTime gameTime)
        {
            for (int i = 0; i < Keys.Count; i++)
            {

                if(Keys[i].Name != "b_button")
                this.SetValue(Keys[i].Name, (float)(2.0 * (random.NextDouble() - 0.5)));
            }

            //throw new Exception("The method or operation is not implemented.");
        }
    }
}
