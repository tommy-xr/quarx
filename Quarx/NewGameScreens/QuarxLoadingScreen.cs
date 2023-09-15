using System;
using System.Collections.Generic;
using System.Text;

using Sxe.Engine.UI;

namespace Quarx
{
    public class QuarxLoadingScreen : LoadingScreen
    {
        private Quarx.NewPanels.LoadingPanel loadingPanel1;
       

        public QuarxLoadingScreen()
        {
            InitializeComponent();
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch, Microsoft.Xna.Framework.GameTime gameTime)
        {
            this.loadingPanel1.LoadPercentage = this.PercentComplete;

            //if (this.loadingPanel1.LoadPercentage < this.PercentComplete)
            //    this.loadingPanel1.LoadPercentage += (float)gameTime.ElapsedGameTime.TotalSeconds / 8f;

            //if (this.loadingPanel1.LoadPercentage > this.PercentComplete)
            //    this.loadingPanel1.LoadPercentage = this.PercentComplete;

            //byte alpha = (byte)(255 * (Math.Sin(gameTime.TotalRealTime.TotalSeconds) + 1) / 2);

            //if(panel1.Image != null)
            //panel1.Image.Color = new Microsoft.Xna.Framework.Graphics.Color(255, 255, 255, alpha);

            base.Draw(spriteBatch, gameTime);
        }

        void InitializeComponent()
        {
            this.loadingPanel1 = new Quarx.NewPanels.LoadingPanel();
            // 
            // loadingPanel1
            // 
            this.loadingPanel1.BackgroundPath = "Loading\\loadingbar";
            this.loadingPanel1.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowSignedIn;
            this.loadingPanel1.LoadPercentage = 0F;
            this.loadingPanel1.Location = new Microsoft.Xna.Framework.Point(490, 300);
            this.loadingPanel1.Parent = this;
            this.loadingPanel1.Size = new Microsoft.Xna.Framework.Point(300, 70);
            this.loadingPanel1.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.loadingPanel1.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.loadingPanel1.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.loadingPanel1.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            // 
            // QuarxLoadingScreen
            // 
            this.Panels.Add(this.loadingPanel1);
            this.PresenceMode = Microsoft.Xna.Framework.GamerServices.GamerPresenceMode.StartingGame;
            this.Size = new Microsoft.Xna.Framework.Point(1280, 720);

        }
    }
}
