using System;
using System.Collections.Generic;
using System.Text;

using Sxe.Engine.UI;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Quarx
{
    public class TwoPlayerGame : BaseQuarxGameScreen
    {
        private QuarxBoard quarxBoard2;
        private ScorePanel scorePanel1;
        private PreviewPanel previewPanel1;
        private PreviewPanel previewPanel2;
        private ScorePanel scorePanelFlipped1;
        private EnergyMeter energyMeter1;
        private EnergyMeter energyMeter2;
        private Panel panel1;
        private QuarxBoard quarxBoard1;

    
        public TwoPlayerGame()
        {
            InitializeComponent();
        }



        public override void Activate()
        {
            

            InitializeGame(2);

            StartGame();


            base.Activate();
        }

        public override void StartGame()
        {
            base.StartGame();
            quarxBoard1.Model = this.Models[0];
            quarxBoard2.Model = this.Models[1];
        }

        public override void PreDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            quarxBoard1.PreDraw(gameTime);
            quarxBoard2.PreDraw(gameTime);

            base.PreDraw(spriteBatch, gameTime);
        }

        public override void Paint(SpriteBatch sb, Point positionOffset, Vector2 scale)
        {
            if (quarxBoard1.Viewer != null)
                this.previewPanel1.SetTexture(quarxBoard1.Viewer.GetPreviewTexture());
                //this.previewPanel1.SetTexture(quarxBoard1.Viewer.PreviewTexture);

            if(quarxBoard2.Viewer != null)
            this.previewPanel2.SetTexture(quarxBoard2.Viewer.GetPreviewTexture());
            base.Paint(sb, positionOffset, scale);
        }

        //public override void OnRoundOver()
        //{

        //    base.OnRoundOver();
        //}

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            this.scorePanel1.MaxRounds = this.Settings.GlobalSettings.NumberOfRounds;
            this.scorePanelFlipped1.MaxRounds = this.Settings.GlobalSettings.NumberOfRounds;

            this.scorePanel1.Score = this.Models[0].Score;
            this.scorePanel1.Isotopes = this.Models[0].Isotopes;
            this.scorePanel1.RoundsWon = this.Info[0].RoundsWon;
            this.energyMeter1.Value = this.Models[0].PunishLevel;


            this.scorePanelFlipped1.Score = this.Models[1].Score;
            this.scorePanelFlipped1.Isotopes = this.Models[1].Isotopes;
            this.scorePanelFlipped1.RoundsWon = this.Info[1].RoundsWon;
            this.energyMeter2.Value = this.Models[1].PunishLevel;

            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }


        void InitializeComponent()
        {
            this.quarxBoard1 = new Quarx.QuarxBoard();
            this.quarxBoard2 = new Quarx.QuarxBoard();
            this.scorePanel1 = new Quarx.ScorePanel();
            this.previewPanel1 = new Quarx.PreviewPanel();
            this.previewPanel2 = new Quarx.PreviewPanel();
            this.scorePanelFlipped1 = new Quarx.ScorePanel();
            this.energyMeter1 = new Quarx.EnergyMeter();
            this.energyMeter2 = new Quarx.EnergyMeter();
            this.panel1 = new Sxe.Engine.UI.Panel();
            // 
            // quarxBoard1
            // 
            this.quarxBoard1.BackgroundPath = null;
            this.quarxBoard1.CanDrag = false;
            this.quarxBoard1.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowSignedIn;
            this.quarxBoard1.Location = new Microsoft.Xna.Framework.Point(348, 45);
            this.quarxBoard1.Model = null;
            this.quarxBoard1.Parent = this;
            this.quarxBoard1.Size = new Microsoft.Xna.Framework.Point(250, 500);
            this.quarxBoard1.TransitionOffOffset = new Microsoft.Xna.Framework.Point(-500, 0);
            this.quarxBoard1.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.quarxBoard1.TransitionOnOffset = new Microsoft.Xna.Framework.Point(-500, 0);
            this.quarxBoard1.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            // 
            // quarxBoard2
            // 
            this.quarxBoard2.BackgroundPath = null;
            this.quarxBoard2.CanDrag = false;
            this.quarxBoard2.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowSignedIn;
            this.quarxBoard2.Location = new Microsoft.Xna.Framework.Point(636, 47);
            this.quarxBoard2.Model = null;
            this.quarxBoard2.Parent = this;
            this.quarxBoard2.Size = new Microsoft.Xna.Framework.Point(250, 500);
            this.quarxBoard2.TransitionOffOffset = new Microsoft.Xna.Framework.Point(500, 0);
            this.quarxBoard2.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.quarxBoard2.TransitionOnOffset = new Microsoft.Xna.Framework.Point(500, 0);
            this.quarxBoard2.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            // 
            // scorePanel1
            // 
            this.scorePanel1.BackgroundPath = "score1";
            this.scorePanel1.CanDrag = false;
            this.scorePanel1.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowSignedIn;
            this.scorePanel1.Isotopes = 0;
            this.scorePanel1.Location = new Microsoft.Xna.Framework.Point(149, 163);
            this.scorePanel1.Parent = this;
            this.scorePanel1.Player = 1;
            this.scorePanel1.RoundsVisible = true;
            this.scorePanel1.RoundsWon = 0;
            this.scorePanel1.Score = 0;
            this.scorePanel1.Size = new Microsoft.Xna.Framework.Point(110, 125);
            this.scorePanel1.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 500);
            this.scorePanel1.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.scorePanel1.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 500);
            this.scorePanel1.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            // 
            // previewPanel1
            // 
            this.previewPanel1.BackgroundPath = "previewbox";
            this.previewPanel1.CanDrag = false;
            this.previewPanel1.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowSignedIn;
            this.previewPanel1.Location = new Microsoft.Xna.Framework.Point(293, 438);
            this.previewPanel1.Parent = this;
            this.previewPanel1.Size = new Microsoft.Xna.Framework.Point(75, 75);
            this.previewPanel1.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, -500);
            this.previewPanel1.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.previewPanel1.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, -500);
            this.previewPanel1.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            // 
            // previewPanel2
            // 
            this.previewPanel2.BackgroundPath = "previewbox2";
            this.previewPanel2.CanDrag = false;
            this.previewPanel2.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowSignedIn;
            this.previewPanel2.Location = new Microsoft.Xna.Framework.Point(902, 440);
            this.previewPanel2.Parent = this;
            this.previewPanel2.Size = new Microsoft.Xna.Framework.Point(75, 75);
            this.previewPanel2.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, -500);
            this.previewPanel2.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.previewPanel2.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, -500);
            this.previewPanel2.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            // 
            // scorePanelFlipped1
            // 
            this.scorePanelFlipped1.BackgroundPath = "score2";
            this.scorePanelFlipped1.CanDrag = false;
            this.scorePanelFlipped1.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowSignedIn;
            this.scorePanelFlipped1.Isotopes = 0;
            this.scorePanelFlipped1.Location = new Microsoft.Xna.Framework.Point(983, 154);
            this.scorePanelFlipped1.Parent = this;
            this.scorePanelFlipped1.Player = 1;
            this.scorePanelFlipped1.RoundsVisible = true;
            this.scorePanelFlipped1.RoundsWon = 0;
            this.scorePanelFlipped1.Score = 0;
            this.scorePanelFlipped1.Size = new Microsoft.Xna.Framework.Point(110, 125);
            this.scorePanelFlipped1.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 500);
            this.scorePanelFlipped1.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.scorePanelFlipped1.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 500);
            this.scorePanelFlipped1.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            // 
            // energyMeter1
            // 
            this.energyMeter1.BackgroundPath = "Punish\\punishbarreverse";
            this.energyMeter1.CanDrag = false;
            this.energyMeter1.Flipped = true;
            this.energyMeter1.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowSignedIn;
            this.energyMeter1.Location = new Microsoft.Xna.Framework.Point(295, 106);
            this.energyMeter1.Parent = this;
            this.energyMeter1.Size = new Microsoft.Xna.Framework.Point(75, 240);
            this.energyMeter1.TransitionOffOffset = new Microsoft.Xna.Framework.Point(500, 0);
            this.energyMeter1.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.energyMeter1.TransitionOnOffset = new Microsoft.Xna.Framework.Point(500, 0);
            this.energyMeter1.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.energyMeter1.Value = 5;
            // 
            // energyMeter2
            // 
            this.energyMeter2.BackgroundPath = "Punish\\punishbar";
            this.energyMeter2.CanDrag = false;
            this.energyMeter2.Flipped = false;
            this.energyMeter2.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowSignedIn;
            this.energyMeter2.Location = new Microsoft.Xna.Framework.Point(899, 106);
            this.energyMeter2.Parent = this;
            this.energyMeter2.Size = new Microsoft.Xna.Framework.Point(75, 240);
            this.energyMeter2.TransitionOffOffset = new Microsoft.Xna.Framework.Point(500, 0);
            this.energyMeter2.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.energyMeter2.TransitionOnOffset = new Microsoft.Xna.Framework.Point(500, 0);
            this.energyMeter2.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.energyMeter2.Value = 5;
            // 
            // panel1
            // 
            this.panel1.BackgroundPath = "tvborder";
            this.panel1.CanDrag = false;
            this.panel1.Location = new Microsoft.Xna.Framework.Point(-25, -25);
            this.panel1.Parent = this;
            this.panel1.Size = new Microsoft.Xna.Framework.Point(1330, 770);
            this.panel1.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.panel1.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            // 
            // TwoPlayerGame
            // 
            this.Panels.Add(this.quarxBoard2);
            this.Panels.Add(this.scorePanel1);
            this.Panels.Add(this.scorePanelFlipped1);
            this.Panels.Add(this.previewPanel1);
            this.Panels.Add(this.previewPanel2);
            this.Panels.Add(this.quarxBoard1);
            this.Panels.Add(this.energyMeter1);
            this.Panels.Add(this.energyMeter2);
            this.Panels.Add(this.panel1);
            this.PresenceMode = Microsoft.Xna.Framework.GamerServices.GamerPresenceMode.LocalVersus;
            this.Size = new Microsoft.Xna.Framework.Point(1280, 720);

        }

        
             //this.Panels.Add(quarxBoard2);
             //this.Panels.Add(scorePanel1);
             //this.Panels.Add(scorePanel2);
             //this.Panels.Add(roundPanel1);
             //this.Panels.Add(roundPanel2);
             //this.Panels.Add(previewPanel1);
             //this.Panels.Add(previewPanel2);
             //this.Panels.Add(quarxBoard1);

    }
}
