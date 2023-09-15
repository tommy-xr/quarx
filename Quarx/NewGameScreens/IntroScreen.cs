using System;
using System.Collections.Generic;
using System.Text;

using Sxe.Engine.UI;

namespace Quarx
{
    public class IntroScreen : BaseScreen
    {
        private IntroAnimation introAnimation1;
    
        public IntroScreen()
        {
            InitializeComponent();
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            if (introAnimation1.IsComplete && !otherScreenHasFocus)
            {
                this.ExitScreen();
                GameScreenService.AddScreen(new Quarx.GameScreens.MenuScreen());
            }

            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch, Microsoft.Xna.Framework.GameTime gameTime)
        {
            spriteBatch.GraphicsDevice.Clear(Microsoft.Xna.Framework.Graphics.Color.Black);
            base.Draw(spriteBatch, gameTime);
        }

        public void InitializeComponent()
        {
            this.introAnimation1 = new Quarx.IntroAnimation();
            // 
            // introAnimation1
            // 
            this.introAnimation1.BackgroundPath = null;
            this.introAnimation1.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowSignedIn;
            this.introAnimation1.Location = new Microsoft.Xna.Framework.Point(275, 175);
            this.introAnimation1.Parent = this;
            this.introAnimation1.Size = new Microsoft.Xna.Framework.Point(250, 250);
            this.introAnimation1.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.introAnimation1.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.introAnimation1.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.introAnimation1.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            // 
            // IntroScreen
            // 
            this.Panels.Add(this.introAnimation1);
            this.PresenceMode = Microsoft.Xna.Framework.GamerServices.GamerPresenceMode.OnARoll;
            this.TransitionOffTime = System.TimeSpan.Parse("00:00:00");
            this.TransitionOnTime = System.TimeSpan.Parse("00:00:00");

        }
    }
}
