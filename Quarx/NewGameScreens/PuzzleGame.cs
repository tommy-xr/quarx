using System;
using System.Collections.Generic;
using System.Text;

using Sxe.Engine.UI;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.GamerServices;

namespace Quarx
{
    public class PuzzleGame : BaseQuarxGameScreen
    {
        private ScorePanel scorePanel1;
        private QuarxBoard quarxBoard1;
        private PuzzleDescription puzzleDescription;
        private PuzzleSelectionScreen puzzleScreen;
        private PuzzlePreview puzzlePreview1;
        private Panel panel1;
        private HelperPanel helperPanel1;
        private int puzzleIndex;




        public int PuzzleIndex
        {
            get { return puzzleIndex; }
            set { puzzleIndex = value; }
        }

        public PuzzleSelectionScreen PuzzleScreen
        {
            get { return puzzleScreen; }
            set { puzzleScreen = value; }
        }

        public PuzzleDescription PuzzleDescription
        {
            get { return puzzleDescription; }
            set { puzzleDescription = value; }
        }

    
        public PuzzleGame()
        {
            InitializeComponent();


            
        }



        public override void Activate()
        {
            

            InitializeGame(1);

            StartGame();




            base.Activate();
        }

        public override BaseGameModel GetGameModel(Sxe.Engine.IAnarchyGamer gamer)
        {
            return new PuzzleGameModel(8, 18, gamer, puzzleDescription);
            //return base.GetGameModel(gamer);
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
            if (quarxBoard1.Model == null)
                return true;
            else
                return false;


            //return base.ShouldStartNewRound();
        }

        public override void Deactivate()
        {
            if (quarxBoard1.Model.State == QuarxGameState.Won)
            {
                if(this.puzzleScreen != null)
                this.puzzleScreen.Won(this.PuzzleIndex);
            }

            base.Deactivate();
        }


        public override void PreDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            quarxBoard1.PreDraw(gameTime);
            PuzzleGameModel puzzleModel =this.Models[0] as PuzzleGameModel;
            this.puzzlePreview1.SetPreviews(quarxBoard1.Viewer,puzzleModel.AtomClusters );
            base.PreDraw(spriteBatch, gameTime);
        }

        public override void Paint(SpriteBatch sb, Point positionOffset, Vector2 scale)
        {
            //if(quarxBoard1.Viewer != null)
            //previewPanel1.SetTexture(quarxBoard1.Viewer.GetPreviewTexture());

            

            base.Paint(sb, positionOffset, scale);
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            this.scorePanel1.Isotopes = quarxBoard1.Model.Isotopes;
            this.scorePanel1.Score = quarxBoard1.Model.Score;

            

            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }

        void UpdatePreview()
        {
            //for (int i = 0; i < 8; i++)
            //    this.previewPanels[i].Visible = false;

            ////Try and get the model as a PuzzleGameModel
            //PuzzleGameModel model = this.quarxBoard1.Model as PuzzleGameModel;
            //if (model != null)
            //{
            //    int p = 0;
            //    foreach (AtomClusterDescription atomCluster in model.AtomClusters)
            //    {
            //        this.previewPanels[p].Visible = true;
            //        this.previewPanels[p].
            //        p++;
            //    }
            //}
        }



        void InitializeComponent()
        {
            this.quarxBoard1 = new Quarx.QuarxBoard();
            this.scorePanel1 = new Quarx.ScorePanel();
            this.puzzlePreview1 = new Quarx.PuzzlePreview();
            this.panel1 = new Sxe.Engine.UI.Panel();
            this.helperPanel1 = new Quarx.HelperPanel();
            // 
            // quarxBoard1
            // 
            this.quarxBoard1.BackColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(0)), ((byte)(255)));
            this.quarxBoard1.BackgroundPath = null;
            this.quarxBoard1.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowSignedIn;
            this.quarxBoard1.Location = new Microsoft.Xna.Framework.Point(430, 50);
            this.quarxBoard1.Model = null;
            this.quarxBoard1.Parent = this;
            this.quarxBoard1.Size = new Microsoft.Xna.Framework.Point(250, 500);
            this.quarxBoard1.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, -500);
            this.quarxBoard1.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.quarxBoard1.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, -500);
            this.quarxBoard1.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            // 
            // scorePanel1
            // 
            this.scorePanel1.BackgroundPath = "score1";
            this.scorePanel1.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowSignedIn;
            this.scorePanel1.Isotopes = 0;
            this.scorePanel1.Location = new Microsoft.Xna.Framework.Point(710, 151);
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
            // puzzlePreview1
            // 
            this.puzzlePreview1.BackgroundPath = "Puzzle\\puzzlepreview";
            this.puzzlePreview1.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowSignedIn;
            this.puzzlePreview1.Location = new Microsoft.Xna.Framework.Point(354, 177);
            this.puzzlePreview1.Parent = this;
            this.puzzlePreview1.Size = new Microsoft.Xna.Framework.Point(100, 300);
            this.puzzlePreview1.TransitionOffOffset = new Microsoft.Xna.Framework.Point(-500, 0);
            this.puzzlePreview1.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.puzzlePreview1.TransitionOnOffset = new Microsoft.Xna.Framework.Point(-500, 0);
            this.puzzlePreview1.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            // 
            // panel1
            // 
            this.panel1.BackgroundPath = "tvborder";
            this.panel1.Location = new Microsoft.Xna.Framework.Point(-25, -25);
            this.panel1.Parent = this;
            this.panel1.Size = new Microsoft.Xna.Framework.Point(1250, 750);
            this.panel1.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.panel1.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            // 
            // helperPanel1
            // 
            this.helperPanel1.BackgroundPath = "puzzlehelper";
            this.helperPanel1.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowSignedIn;
            this.helperPanel1.Location = new Microsoft.Xna.Framework.Point(733, 414);
            this.helperPanel1.Parent = this;
            this.helperPanel1.Size = new Microsoft.Xna.Framework.Point(174, 86);
            this.helperPanel1.TransitionOffOffset = new Microsoft.Xna.Framework.Point(500, 0);
            this.helperPanel1.TransitionOffTime = System.TimeSpan.Parse("00:00:01");
            this.helperPanel1.TransitionOnOffset = new Microsoft.Xna.Framework.Point(500, 0);
            this.helperPanel1.TransitionOnTime = System.TimeSpan.Parse("00:00:01");
            // 
            // PuzzleGame
            // 
            this.Panels.Add(this.quarxBoard1);
            this.Panels.Add(this.scorePanel1);
            this.Panels.Add(this.puzzlePreview1);
            this.Panels.Add(this.panel1);
            this.Panels.Add(this.helperPanel1);
            this.Size = new Microsoft.Xna.Framework.Point(1200, 700);

        }

    }
}
