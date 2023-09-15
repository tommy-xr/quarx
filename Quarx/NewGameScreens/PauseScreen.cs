using System;
using System.Collections.Generic;
using System.Text;

using Sxe.Engine.UI;
using Sxe.Engine.Input;

namespace Quarx
{
    public class PauseScreen : MessageScreen
    {
        private Panel panel1;

        IGameController controller;
        public IGameController Controller
        {
            get { return controller; }
            set { controller = value; }
        }

        BaseQuarxGameScreen gameScreen;
        private Panel panel2;
    
        public BaseQuarxGameScreen GameScreen
        {
            get { return gameScreen; }
            set { gameScreen = value; }
        }

        bool shouldExit = false;

        public PauseScreen()
        {
            InitializeComponent();
        }

        public override void Activate()
        {
            PlayPauseSound();
            base.Activate();
        }

        public override void Deactivate()
        {
            base.Deactivate();
        }

        void PlayPauseSound()
        {
            this.GameScreenService.Audio.PlayCue("pause_show");
        }

        void InitializeComponent()
        {
            this.panel1 = new Sxe.Engine.UI.Panel();
            this.panel2 = new Sxe.Engine.UI.Panel();
            // 
            // panel1
            // 
            this.panel1.BackgroundPath = "pause";
            this.panel1.CanDrag = false;
            this.panel1.Location = new Microsoft.Xna.Framework.Point(263, 210);
            this.panel1.Parent = this;
            this.panel1.Size = new Microsoft.Xna.Framework.Point(259, 135);
            this.panel1.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.panel1.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            // 
            // panel2
            // 
            this.panel2.BackgroundPath = "setupbutton";
            this.panel2.CanDrag = false;
            this.panel2.Location = new Microsoft.Xna.Framework.Point(132, 157);
            this.panel2.Parent = this;
            this.panel2.Size = new Microsoft.Xna.Framework.Point(536, 226);
            this.panel2.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.panel2.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            // 
            // PauseScreen
            // 
            this.Panels.Add(this.panel2);
            this.Panels.Add(this.panel1);
            this.TransitionOffOffset = new Microsoft.Xna.Framework.Point(-500, 0);
            this.TransitionOnOffset = new Microsoft.Xna.Framework.Point(500, 0);

        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            if (shouldExit && !IsExiting)
            {
                PlayPauseSound();
                this.ExitScreen();
            }

            if (controller != null)
            {
                if (controller.IsKeyJustPressed("pause"))
                {
                    shouldExit = true;
                }
                else if (controller.IsKeyJustPressed("exit_pause"))
                {
                    shouldExit = true;
                    this.gameScreen.ExitScreen();
                    this.gameScreen.OnExit();
                }
            }

            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }
    }
}
