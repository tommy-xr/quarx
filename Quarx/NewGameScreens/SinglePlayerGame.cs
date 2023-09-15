using System;
using System.Collections.Generic;
using System.Text;

using Sxe.Engine.UI;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.GamerServices;

namespace Quarx
{
    public class SinglePlayerGame : BaseQuarxGameScreen
    {
        private ScorePanel scorePanel1;
        private PreviewPanel previewPanel1;
        private QuarxBoard quarxBoard1;

        bool achievedHighScore = false;
        private Panel panel1;
        private HelperPanel helperPanel1;
        int score = 0;
    
        public SinglePlayerGame()
        {
            InitializeComponent();
            this.scorePanel1.RoundsVisible = false;
        }



        public override void Activate()
        {

            HighScores.Load(this.GameScreenService.Storage);

            InitializeGame(1);

            StartGame();

            //Reset time after long frame
            QuarxGame.GetGameInstance.ResetElapsedTime();



            base.Activate();
        }

        public override void StartGame()
        {
            base.StartGame();
            quarxBoard1.Model = this.Models[0];

            if (quarxBoard1.Marquee.Visible)
                quarxBoard1.Marquee.Hide();

            this.helperPanel1.Show();
        }

        public override bool ShouldStartNewRound()
        {
            if (this.quarxBoard1.Model == null)
                return true;

            if (this.quarxBoard1.Model.State == QuarxGameState.Won)
                return true;
            else
            {
                //TODO: Add single high score list, for single player

                //if (Settings.HighScores != null)
                //{
                //    //Check if we have a high score..
                //    if (Settings.HighScores.IsHighScore(Info[0].TotalScore))
                //    {
                //        GameScreenService.AddScreen(new HighScoreCongrats());
                //        Settings.HighScores.AddHighScore(SignedInGamer.SignedInGamers[0].Gamertag,
                //            Info[0].TotalScore);
                //    }
                //}


                return false;
            }
            //return base.ShouldStartNewRound();
        }

        public override void OnRoundOver()
        {
            QuarxGameSettings settings = Settings.GamerSettings[0].Settings;

            this.score += quarxBoard1.Model.Score;

            if (quarxBoard1.Model.State == QuarxGameState.Won)
            {
                quarxBoard1.Marquee.ShowMarquee(QuarxMarqueeType.Advance);
                if (settings != null)
                    settings.IncrementLevel();

                //TODO: Handle incrementing level
                //Settings.IncrementLevel();
            }
            else
            {
                quarxBoard1.Marquee.ShowMarquee(QuarxMarqueeType.GameOver);

            }

            base.OnRoundOver();
        }

        public override void OnExit()
        {
            QuarxGameSettings settings = Settings.GamerSettings[0].Settings;

            HighScoreList highScores = HighScores.HighScoresFromDifficulty(settings.Difficulty);


            if (highScores != null)
            {
                //This is kind of crappy race conditoin
                //If the player dies SUPER FAST, the high score list might not be loaded, and we might not save a high score for them
                //Then again, if they die that fast, they probably didn't get a high score anyway :-)
                if (highScores.Loaded)
                {
                    int startScore = score;

                    if (quarxBoard1.Model != null)
                        if (quarxBoard1.Model.State == QuarxGameState.Playing)
                            startScore += quarxBoard1.Model.Score;

                    if (highScores.IsHighScore(startScore))
                    {
                        highScores.AddHighScore(quarxBoard1.Model.Gamer.GamerTag, startScore);

                        highScores.Save(this.GameScreenService.Storage);

                        QuarxGame.GetGameInstance.ResetElapsedTime();

                        achievedHighScore = true;

                    }
                }
                else
                {
                    highScores = null;
                }

            }

            if (achievedHighScore)
            {
                HighScoreCongrats congrats = new HighScoreCongrats();
                congrats.Gamer = quarxBoard1.Model.Gamer;

                this.GameScreenService.AddScreen(congrats);
            }
            base.OnExit();
        }

        public override void PreDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            quarxBoard1.PreDraw(gameTime);
            base.PreDraw(spriteBatch, gameTime);
        }

        public override void Paint(SpriteBatch sb, Point positionOffset, Vector2 scale)
        {
            if(quarxBoard1.Viewer != null)
            previewPanel1.SetTexture(quarxBoard1.Viewer.GetPreviewTexture());

            base.Paint(sb, positionOffset, scale);
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            this.scorePanel1.Isotopes = quarxBoard1.Model.Isotopes;

            if (quarxBoard1.Model.State == QuarxGameState.Playing)
                this.scorePanel1.Score = quarxBoard1.Model.Score + score;
            else
                this.scorePanel1.Score = score;
            //this.energyMeter1.Value = quarxBoard1.Model.PunishLevel;
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }



        void InitializeComponent()
        {
            this.quarxBoard1 = new Quarx.QuarxBoard();
            this.scorePanel1 = new Quarx.ScorePanel();
            this.previewPanel1 = new Quarx.PreviewPanel();
            this.panel1 = new Sxe.Engine.UI.Panel();
            this.helperPanel1 = new Quarx.HelperPanel();
            // 
            // quarxBoard1
            // 
            this.quarxBoard1.BackColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(0)), ((byte)(255)));
            this.quarxBoard1.BackgroundPath = null;
            this.quarxBoard1.CanDrag = false;
            this.quarxBoard1.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowSignedIn;
            this.quarxBoard1.Location = new Microsoft.Xna.Framework.Point(463, 49);
            this.quarxBoard1.Model = null;
            this.quarxBoard1.Parent = this;
            this.quarxBoard1.Size = new Microsoft.Xna.Framework.Point(250, 550);
            this.quarxBoard1.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 500);
            this.quarxBoard1.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.quarxBoard1.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 500);
            this.quarxBoard1.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            // 
            // scorePanel1
            // 
            this.scorePanel1.BackgroundPath = "score1";
            this.scorePanel1.CanDrag = false;
            this.scorePanel1.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowSignedIn;
            this.scorePanel1.Isotopes = 0;
            this.scorePanel1.Location = new Microsoft.Xna.Framework.Point(749, 132);
            this.scorePanel1.MaxRounds = 0;
            this.scorePanel1.Parent = this;
            this.scorePanel1.Player = 1;
            this.scorePanel1.RoundsVisible = true;
            this.scorePanel1.RoundsWon = 0;
            this.scorePanel1.Score = 0;
            this.scorePanel1.Size = new Microsoft.Xna.Framework.Point(110, 125);
            this.scorePanel1.TransitionOffOffset = new Microsoft.Xna.Framework.Point(500, 0);
            this.scorePanel1.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.scorePanel1.TransitionOnOffset = new Microsoft.Xna.Framework.Point(500, 0);
            this.scorePanel1.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            // 
            // previewPanel1
            // 
            this.previewPanel1.BackgroundPath = "previewbox";
            this.previewPanel1.CanDrag = false;
            this.previewPanel1.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowSignedIn;
            this.previewPanel1.Location = new Microsoft.Xna.Framework.Point(410, 441);
            this.previewPanel1.Parent = this;
            this.previewPanel1.Size = new Microsoft.Xna.Framework.Point(75, 75);
            this.previewPanel1.TransitionOffOffset = new Microsoft.Xna.Framework.Point(-500, 0);
            this.previewPanel1.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.previewPanel1.TransitionOnOffset = new Microsoft.Xna.Framework.Point(-500, 0);
            this.previewPanel1.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            // 
            // panel1
            // 
            this.panel1.BackgroundPath = "tvborder";
            this.panel1.CanDrag = false;
            this.panel1.Location = new Microsoft.Xna.Framework.Point(-25, -25);
            this.panel1.Parent = this;
            this.panel1.Size = new Microsoft.Xna.Framework.Point(1250, 750);
            this.panel1.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.panel1.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            // 
            // helperPanel1
            // 
            this.helperPanel1.BackgroundPath = "singlehelper";
            this.helperPanel1.CanDrag = false;
            this.helperPanel1.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowSignedIn;
            this.helperPanel1.Location = new Microsoft.Xna.Framework.Point(288, 156);
            this.helperPanel1.Parent = this;
            this.helperPanel1.Size = new Microsoft.Xna.Framework.Point(174, 86);
            this.helperPanel1.TransitionOffOffset = new Microsoft.Xna.Framework.Point(-500, 0);
            this.helperPanel1.TransitionOffTime = System.TimeSpan.Parse("00:00:01");
            this.helperPanel1.TransitionOnOffset = new Microsoft.Xna.Framework.Point(-500, 0);
            this.helperPanel1.TransitionOnTime = System.TimeSpan.Parse("00:00:01");
            // 
            // SinglePlayerGame
            // 
            this.Panels.Add(this.previewPanel1);
            this.Panels.Add(this.quarxBoard1);
            this.Panels.Add(this.scorePanel1);
            this.Panels.Add(this.panel1);
            this.Panels.Add(this.helperPanel1);
            this.PresenceMode = Microsoft.Xna.Framework.GamerServices.GamerPresenceMode.SinglePlayer;
            this.Size = new Microsoft.Xna.Framework.Point(1200, 700);

        }

    }
}
