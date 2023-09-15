using System;
using System.Collections.Generic;
using System.Text;

using Sxe.Engine.UI;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Quarx
{
    public class FourPlayerGame : BaseQuarxGameScreen
    {
        //private QuarxBoard quarxBoard2;
        //private ScorePanel scorePanel1;
        //private RoundPanel roundPanel1;
        //private RoundPanel roundPanel2;
        //private PreviewPanel previewPanel1;
        //private PreviewPanel previewPanel2;
        //private ScorePanelFlipped scorePanelFlipped1;
        private QuarxBoard miniQuarxBoard1;
        private QuarxBoard miniQuarxBoard2;
        private QuarxBoard miniQuarxBoard3;
        private ScoreBoard scoreBoard1;
        private ScoreBoard scoreBoard2;
        private ScoreBoard scoreBoard3;
        private ScoreBoard scoreBoard4;
        private EnergyMeter energyMeter1;
        private EnergyMeter energyMeter2;
        private EnergyMeter energyMeter3;
        private EnergyMeter energyMeter4;
        private Panel panel1;
        private QuarxBoard miniQuarxBoard4;


    
        public FourPlayerGame()
        {
            InitializeComponent();
        }



        public override void Activate()
        {
            

            InitializeGame(4);

            StartGame();


            base.Activate();
        }

        public override void StartGame()
        {
            base.StartGame();
            miniQuarxBoard1.Model = this.Models[0];
            miniQuarxBoard2.Model = this.Models[1];
            miniQuarxBoard3.Model = this.Models[2];
            miniQuarxBoard4.Model = this.Models[3];
        }

        public override void PreDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            miniQuarxBoard1.PreDraw(gameTime);
            miniQuarxBoard2.PreDraw(gameTime);
            miniQuarxBoard3.PreDraw(gameTime);
            miniQuarxBoard4.PreDraw(gameTime);

            base.PreDraw(spriteBatch, gameTime);
        }

        public override void Paint(SpriteBatch sb, Point positionOffset, Vector2 scale)
        {
            if (miniQuarxBoard1.Viewer != null)
                this.scoreBoard1.SetPreviewTexture(miniQuarxBoard1.Viewer.PreviewTexture);

            if (miniQuarxBoard2.Viewer != null)
                this.scoreBoard2.SetPreviewTexture(miniQuarxBoard2.Viewer.PreviewTexture);

            if (miniQuarxBoard3.Viewer != null)
                this.scoreBoard3.SetPreviewTexture(miniQuarxBoard3.Viewer.PreviewTexture);

            if (miniQuarxBoard4.Viewer != null)
                this.scoreBoard4.SetPreviewTexture(miniQuarxBoard4.Viewer.PreviewTexture);
            base.Paint(sb, positionOffset, scale);
        }

        public override void OnRoundOver()
        {
            //if (Info[0].RoundsWon >= 3)
            //{
            //    miniQuarxBoard1.Marquee.ShowMarquee(QuarxMarqueeType.First);
            //    miniQuarxBoard2.Marquee.ShowMarquee(QuarxMarqueeType.Second);
            //}
            //else if (Info[1].RoundsWon >= 3)
            //{
            //    quarxBoard1.Marquee.ShowMarquee(QuarxMarqueeType.Second);
            //    quarxBoard2.Marquee.ShowMarquee(QuarxMarqueeType.First);
            //}
            base.OnRoundOver();
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {

            this.scoreBoard1.MaxRounds = this.Settings.GlobalSettings.NumberOfRounds;
            this.scoreBoard2.MaxRounds = this.Settings.GlobalSettings.NumberOfRounds;
            this.scoreBoard3.MaxRounds = this.Settings.GlobalSettings.NumberOfRounds;
            this.scoreBoard4.MaxRounds = this.Settings.GlobalSettings.NumberOfRounds;

            this.energyMeter1.Value = this.Models[0].PunishLevel;
            this.energyMeter2.Value = this.Models[1].PunishLevel;
            this.energyMeter3.Value = this.Models[2].PunishLevel;
            this.energyMeter4.Value = this.Models[3].PunishLevel;

            this.SetScoreBoardPunishTarget(this.Models[0], this.scoreBoard1);
            this.SetScoreBoardPunishTarget(this.Models[1], this.scoreBoard2);
            this.SetScoreBoardPunishTarget(this.Models[2], this.scoreBoard3);
            this.SetScoreBoardPunishTarget(this.Models[3], this.scoreBoard4);

            this.scoreBoard1.Score = this.Models[0].Score;
            this.scoreBoard1.Isotopes = this.Models[0].Isotopes;
            this.scoreBoard2.Score = this.Models[1].Score;
            this.scoreBoard2.Isotopes = this.Models[1].Isotopes;
            this.scoreBoard3.Score = this.Models[2].Score;
            this.scoreBoard3.Isotopes = this.Models[2].Isotopes;
            this.scoreBoard4.Score = this.Models[3].Score;
            this.scoreBoard4.Isotopes = this.Models[3].Isotopes;

            this.scoreBoard1.RoundsWon = this.Info[0].RoundsWon;
            this.scoreBoard2.RoundsWon = this.Info[1].RoundsWon;
            this.scoreBoard3.RoundsWon = this.Info[2].RoundsWon;
            this.scoreBoard4.RoundsWon = this.Info[3].RoundsWon;


            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }


        void InitializeComponent()
        {
            this.miniQuarxBoard1 = new Quarx.QuarxBoard();
            this.miniQuarxBoard2 = new Quarx.QuarxBoard();
            this.miniQuarxBoard3 = new Quarx.QuarxBoard();
            this.miniQuarxBoard4 = new Quarx.QuarxBoard();
            this.scoreBoard1 = new Quarx.ScoreBoard();
            this.scoreBoard2 = new Quarx.ScoreBoard();
            this.scoreBoard3 = new Quarx.ScoreBoard();
            this.scoreBoard4 = new Quarx.ScoreBoard();
            this.energyMeter1 = new Quarx.EnergyMeter();
            this.energyMeter2 = new Quarx.EnergyMeter();
            this.energyMeter3 = new Quarx.EnergyMeter();
            this.energyMeter4 = new Quarx.EnergyMeter();
            this.panel1 = new Sxe.Engine.UI.Panel();
            // 
            // miniQuarxBoard1
            // 
            this.miniQuarxBoard1.BackgroundPath = null;
            this.miniQuarxBoard1.CanDrag = false;
            this.miniQuarxBoard1.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowSignedIn;
            this.miniQuarxBoard1.Location = new Microsoft.Xna.Framework.Point(140, 70);
            this.miniQuarxBoard1.Model = null;
            this.miniQuarxBoard1.Parent = this;
            this.miniQuarxBoard1.Size = new Microsoft.Xna.Framework.Point(300, 600);
            this.miniQuarxBoard1.TransitionOffOffset = new Microsoft.Xna.Framework.Point(-500, 0);
            this.miniQuarxBoard1.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.miniQuarxBoard1.TransitionOnOffset = new Microsoft.Xna.Framework.Point(-500, 0);
            this.miniQuarxBoard1.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            // 
            // miniQuarxBoard2
            // 
            this.miniQuarxBoard2.BackgroundPath = null;
            this.miniQuarxBoard2.CanDrag = false;
            this.miniQuarxBoard2.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowSignedIn;
            this.miniQuarxBoard2.Location = new Microsoft.Xna.Framework.Point(441, 70);
            this.miniQuarxBoard2.Model = null;
            this.miniQuarxBoard2.Parent = this;
            this.miniQuarxBoard2.Size = new Microsoft.Xna.Framework.Point(299, 600);
            this.miniQuarxBoard2.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, -500);
            this.miniQuarxBoard2.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.miniQuarxBoard2.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, -500);
            this.miniQuarxBoard2.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            // 
            // miniQuarxBoard3
            // 
            this.miniQuarxBoard3.BackgroundPath = null;
            this.miniQuarxBoard3.CanDrag = false;
            this.miniQuarxBoard3.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowSignedIn;
            this.miniQuarxBoard3.Location = new Microsoft.Xna.Framework.Point(740, 70);
            this.miniQuarxBoard3.Model = null;
            this.miniQuarxBoard3.Parent = this;
            this.miniQuarxBoard3.Size = new Microsoft.Xna.Framework.Point(300, 600);
            this.miniQuarxBoard3.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 500);
            this.miniQuarxBoard3.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.miniQuarxBoard3.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 500);
            this.miniQuarxBoard3.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            // 
            // miniQuarxBoard4
            // 
            this.miniQuarxBoard4.BackgroundPath = null;
            this.miniQuarxBoard4.CanDrag = false;
            this.miniQuarxBoard4.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowSignedIn;
            this.miniQuarxBoard4.Location = new Microsoft.Xna.Framework.Point(1040, 70);
            this.miniQuarxBoard4.Model = null;
            this.miniQuarxBoard4.Parent = this;
            this.miniQuarxBoard4.Size = new Microsoft.Xna.Framework.Point(300, 600);
            this.miniQuarxBoard4.TransitionOffOffset = new Microsoft.Xna.Framework.Point(500, 0);
            this.miniQuarxBoard4.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.miniQuarxBoard4.TransitionOnOffset = new Microsoft.Xna.Framework.Point(500, 0);
            this.miniQuarxBoard4.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            // 
            // scoreBoard1
            // 
            this.scoreBoard1.BackgroundPath = "scoreboard";
            this.scoreBoard1.CanDrag = false;
            this.scoreBoard1.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowSignedIn;
            this.scoreBoard1.Isotopes = 0;
            this.scoreBoard1.Location = new Microsoft.Xna.Framework.Point(161, 634);
            this.scoreBoard1.Parent = this;
            this.scoreBoard1.PunishName = "test";
            this.scoreBoard1.RoundsWon = 0;
            this.scoreBoard1.Score = 0;
            this.scoreBoard1.Size = new Microsoft.Xna.Framework.Point(240, 120);
            this.scoreBoard1.TransitionOffOffset = new Microsoft.Xna.Framework.Point(-500, 0);
            this.scoreBoard1.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.scoreBoard1.TransitionOnOffset = new Microsoft.Xna.Framework.Point(-500, 0);
            this.scoreBoard1.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            // 
            // scoreBoard2
            // 
            this.scoreBoard2.BackgroundPath = "scoreboard";
            this.scoreBoard2.CanDrag = false;
            this.scoreBoard2.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowSignedIn;
            this.scoreBoard2.Isotopes = 0;
            this.scoreBoard2.Location = new Microsoft.Xna.Framework.Point(461, 634);
            this.scoreBoard2.Parent = this;
            this.scoreBoard2.PunishName = "test";
            this.scoreBoard2.RoundsWon = 0;
            this.scoreBoard2.Score = 0;
            this.scoreBoard2.Size = new Microsoft.Xna.Framework.Point(240, 120);
            this.scoreBoard2.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, -500);
            this.scoreBoard2.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.scoreBoard2.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, -500);
            this.scoreBoard2.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            // 
            // scoreBoard3
            // 
            this.scoreBoard3.BackgroundPath = "scoreboard";
            this.scoreBoard3.CanDrag = false;
            this.scoreBoard3.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowSignedIn;
            this.scoreBoard3.Isotopes = 0;
            this.scoreBoard3.Location = new Microsoft.Xna.Framework.Point(761, 634);
            this.scoreBoard3.Parent = this;
            this.scoreBoard3.PunishName = "test";
            this.scoreBoard3.RoundsWon = 0;
            this.scoreBoard3.Score = 0;
            this.scoreBoard3.Size = new Microsoft.Xna.Framework.Point(240, 120);
            this.scoreBoard3.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 500);
            this.scoreBoard3.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.scoreBoard3.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 500);
            this.scoreBoard3.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            // 
            // scoreBoard4
            // 
            this.scoreBoard4.BackgroundPath = "scoreboard";
            this.scoreBoard4.CanDrag = false;
            this.scoreBoard4.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowSignedIn;
            this.scoreBoard4.Isotopes = 0;
            this.scoreBoard4.Location = new Microsoft.Xna.Framework.Point(1064, 631);
            this.scoreBoard4.Parent = this;
            this.scoreBoard4.PunishName = "test";
            this.scoreBoard4.RoundsWon = 0;
            this.scoreBoard4.Score = 0;
            this.scoreBoard4.Size = new Microsoft.Xna.Framework.Point(240, 120);
            this.scoreBoard4.TransitionOffOffset = new Microsoft.Xna.Framework.Point(500, 0);
            this.scoreBoard4.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.scoreBoard4.TransitionOnOffset = new Microsoft.Xna.Framework.Point(500, 0);
            this.scoreBoard4.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            // 
            // energyMeter1
            // 
            this.energyMeter1.BackgroundPath = "Punish\\punishbar";
            this.energyMeter1.CanDrag = false;
            this.energyMeter1.Flipped = false;
            this.energyMeter1.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowSignedIn;
            this.energyMeter1.Location = new Microsoft.Xna.Framework.Point(394, 139);
            this.energyMeter1.Parent = this;
            this.energyMeter1.Size = new Microsoft.Xna.Framework.Point(69, 234);
            this.energyMeter1.TransitionOffOffset = new Microsoft.Xna.Framework.Point(-500, 0);
            this.energyMeter1.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.energyMeter1.TransitionOnOffset = new Microsoft.Xna.Framework.Point(-500, 0);
            this.energyMeter1.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.energyMeter1.Value = 5;
            // 
            // energyMeter2
            // 
            this.energyMeter2.BackgroundPath = "Punish\\punishbar";
            this.energyMeter2.CanDrag = false;
            this.energyMeter2.Flipped = false;
            this.energyMeter2.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowSignedIn;
            this.energyMeter2.Location = new Microsoft.Xna.Framework.Point(694, 139);
            this.energyMeter2.Parent = this;
            this.energyMeter2.Size = new Microsoft.Xna.Framework.Point(69, 234);
            this.energyMeter2.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, -500);
            this.energyMeter2.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.energyMeter2.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, -500);
            this.energyMeter2.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.energyMeter2.Value = 5;
            // 
            // energyMeter3
            // 
            this.energyMeter3.BackgroundPath = "Punish\\punishbar";
            this.energyMeter3.CanDrag = false;
            this.energyMeter3.Flipped = false;
            this.energyMeter3.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowSignedIn;
            this.energyMeter3.Location = new Microsoft.Xna.Framework.Point(994, 139);
            this.energyMeter3.Parent = this;
            this.energyMeter3.Size = new Microsoft.Xna.Framework.Point(69, 234);
            this.energyMeter3.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 500);
            this.energyMeter3.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.energyMeter3.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 500);
            this.energyMeter3.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.energyMeter3.Value = 5;
            // 
            // energyMeter4
            // 
            this.energyMeter4.BackgroundPath = "Punish\\punishbar";
            this.energyMeter4.CanDrag = false;
            this.energyMeter4.Flipped = false;
            this.energyMeter4.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowSignedIn;
            this.energyMeter4.Location = new Microsoft.Xna.Framework.Point(1294, 139);
            this.energyMeter4.Parent = this;
            this.energyMeter4.Size = new Microsoft.Xna.Framework.Point(69, 234);
            this.energyMeter4.TransitionOffOffset = new Microsoft.Xna.Framework.Point(500, 0);
            this.energyMeter4.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.energyMeter4.TransitionOnOffset = new Microsoft.Xna.Framework.Point(500, 0);
            this.energyMeter4.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.energyMeter4.Value = 5;
            // 
            // panel1
            // 


            this.panel1.BackgroundPath = null;
            this.panel1.BackgroundPath = "tvborder";
            this.panel1.CanDrag = false;
            this.panel1.Location = new Microsoft.Xna.Framework.Point(-60, -60);
            this.panel1.Parent = this;
            this.panel1.Size = new Microsoft.Xna.Framework.Point(1600, 970);
            this.panel1.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.panel1.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            // 
            // FourPlayerGame
            // 
            this.Panels.Add(this.energyMeter1);
            this.Panels.Add(this.scoreBoard4);
            this.Panels.Add(this.energyMeter2);
            this.Panels.Add(this.energyMeter3);
            this.Panels.Add(this.energyMeter4);
            this.Panels.Add(this.miniQuarxBoard1);
            this.Panels.Add(this.miniQuarxBoard2);
            this.Panels.Add(this.miniQuarxBoard3);
            this.Panels.Add(this.miniQuarxBoard4);
            this.Panels.Add(this.scoreBoard1);
            this.Panels.Add(this.scoreBoard2);
            this.Panels.Add(this.scoreBoard3);
            this.Panels.Add(this.panel1);
            this.PresenceMode = Microsoft.Xna.Framework.GamerServices.GamerPresenceMode.LocalVersus;
            this.Size = new Microsoft.Xna.Framework.Point(1480, 850);

        }

        
            //this.Panels.Add(miniQuarxBoard1);
            //this.Panels.Add(miniQuarxBoard2);
            //this.Panels.Add(miniQuarxBoard3);
            //this.Panels.Add(miniQuarxBoard4);

            //this.Panels.Add(scorePanel1);
            //this.Panels.Add(scorePanel2);
            //this.Panels.Add(scorePanel3);
            //this.Panels.Add(scorePanel4);

            //this.Panels.Add(previewPanel1);
            //this.Panels.Add(previewPanel2);
            //this.Panels.Add(previewPanel3);
            //this.Panels.Add(previewPanel4);
   

    }
}
