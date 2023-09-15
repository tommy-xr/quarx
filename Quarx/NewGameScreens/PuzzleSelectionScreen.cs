using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Storage;

using Sxe.Engine;
using Sxe.Engine.UI;
using Sxe.Engine.Input;
using Sxe.Engine.Storage;

using Quarx.Puzzle;


namespace Quarx
{
    public class PuzzleSelectionScreen : MessageScreen 
    {
        private PuzzleContainer puzzleContainer1;
        private List<PuzzleDescription> puzzles = new List<PuzzleDescription>();
        private IAnarchyGamer gamer;

        private PuzzleData puzzleData;
        private LoadResult<PuzzleData> loadResult;
        private Panel panel1;
        private bool shouldLoad = false;
        private Label label2;
        private Panel panel2;
        private Panel panel3;
        private Label label1;

        private UIImage gamerBox;
        private UIImage gamerBoxWarning;

        static readonly string puzzleFileName = "puzzleData.puz";

        public IAnarchyGamer Gamer
        {
            get { return gamer; }
            set { gamer = value;
            //if (gamer != null)
            //{
            //    this.label1.Caption = gamer.GamerTag;
            //}
            }
        }


        public PuzzleSelectionScreen()
        {
            InitializeComponent();

            puzzleContainer1.CreateIcons();
            this.ActiveControl = puzzleContainer1;


        }

        public override void LoadContent(ContentManager content)
        {
            LoadPuzzles(content);
            base.LoadContent(content);

            this.gamerBox = this.LoadImage(content, "Puzzle\\puzzlegamertagbox");
            this.gamerBoxWarning = this.LoadImage(content, "Puzzle\\puzzlewarning");

            //if (gamer != null)
            //{
            //    this.panel2.Image = gamer.GamerIcon;
            //    if (gamer.IsTemporary)
            //        this.label2.Visible = true;
            //    else
            //        this.label2.Visible = false;
            //}

        }

        public void Won(int index)
        {
            this.puzzleData.CurrentPuzzle = Math.Max(this.puzzleData.CurrentPuzzle, index + 2);
            SetPuzzles();

            //Don't save if this is a temporary gamer
            if(!this.gamer.IsTemporary)
            this.gamer.Save(puzzleFileName, this.puzzleData, PuzzleData.Save);
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
           

            //if a gamer signs out, we bail homez
            if (!gamer.IsSignedIn)
                this.ExitScreen();

            if (gamer.IsTemporary)
            {
                byte alpha = (byte)(255 * (0.5f + (Math.Sin(gameTime.TotalGameTime.TotalSeconds) + 1) / 4));
                this.panel2.BackColor = new Microsoft.Xna.Framework.Graphics.Color(255, 255, 255, alpha);
                this.panel2.Image = this.gamerBoxWarning;
                this.panel3.Visible = false;
                this.label1.Visible = false;
            }
            else
            {
                this.panel2.BackColor = Microsoft.Xna.Framework.Graphics.Color.White;
                this.panel2.Image = this.gamerBox;
                this.panel3.Visible = true;
                this.label2.Visible = true;

                this.panel3.Image = gamer.GamerIcon;
                this.label1.Caption = gamer.GamerTag;
            }


            if (!gamer.IsTemporary)
            {
                //Only try and load if this isn't a temporary gamer
                if (loadResult == null)
                {
                    loadResult = this.gamer.Load<PuzzleData>(puzzleFileName, PuzzleData.Load);
                    //loadResult = this.GameScreenService.Storage.Load<PuzzleData>(this.puzzleFileName);
                    shouldLoad = true;
                }
                else if (loadResult.IsComplete && shouldLoad)
                {
                    this.puzzleData = loadResult.LoadedData as PuzzleData;
                    if (this.puzzleData == null)
                        this.puzzleData = new PuzzleData();
                    SetPuzzles();
                    shouldLoad = false;
                    this.puzzleContainer1.Visible = true;
                }
            }
            else if (this.puzzleData == null)
            {
                this.puzzleContainer1.Visible = true;
                this.puzzleData = new PuzzleData();
                SetPuzzles();
            }

            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }

        void SetPuzzles()
        {
            if (this.puzzleData != null)
            {
                puzzleContainer1.SetEnabled(this.puzzleData.CurrentPuzzle);
            }
        }

        void LoadPuzzles(ContentManager content)
        {
            //Get path


            string path = Path.Combine(StorageContainer.TitleLocation, "_Puzzles");
            string puzzleIndexPath = Path.Combine(path, "puzzles.txt");

            FileStream fs = new FileStream(puzzleIndexPath, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);

            while (!sr.EndOfStream)
            {
                string puzzleFile = sr.ReadLine();
                string puzzlePath = Path.Combine(path, puzzleFile);
                PuzzleDescription pd = new PuzzleDescription(puzzlePath, content);
                puzzleContainer1.AddPuzzle(pd);

            }

            sr.Close();
            fs.Close();

        }

        public override bool HandleEvent(Sxe.Engine.Input.InputEventArgs inputEvent)
        {
            MenuEventArgs menuEvent = inputEvent as MenuEventArgs;
            if (menuEvent != null)
            {
                if (menuEvent.PlayerIndex == gamer.Index)
                {
                    if (menuEvent.MenuEventType == MenuEventType.Select)
                    {
                        PuzzleGame game = new PuzzleGame();
                        game.PuzzleScreen = this;
                        game.PuzzleIndex = puzzleContainer1.SelectedIndex;
                        game.PuzzleDescription = puzzleContainer1.Selected.PuzzleDescription;
                        game.Settings = RoundSettings.CreateFromQuarxSettings(QuarxGameSettings.Easy, menuEvent.PlayerIndex);
                        GameScreenService.AddScreen(game);
                    }
                    else if (menuEvent.MenuEventType == MenuEventType.Back)
                    {
                        this.ExitScreen();
                    }
                }
            }

            return base.HandleEvent(inputEvent);
        }

        void InitializeComponent()
        {
            this.puzzleContainer1 = new Quarx.PuzzleContainer();
            this.panel1 = new Sxe.Engine.UI.Panel();
            this.label2 = new Sxe.Engine.UI.Label();
            this.panel2 = new Sxe.Engine.UI.Panel();
            this.panel3 = new Sxe.Engine.UI.Panel();
            this.label1 = new Sxe.Engine.UI.Label();
            // 
            // puzzleContainer1
            // 
            this.puzzleContainer1.BackgroundPath = null;
            this.puzzleContainer1.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowSignedIn;
            this.puzzleContainer1.Location = new Microsoft.Xna.Framework.Point(111, 151);
            this.puzzleContainer1.Parent = this;
            this.puzzleContainer1.Size = new Microsoft.Xna.Framework.Point(556, 363);
            this.puzzleContainer1.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.puzzleContainer1.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.puzzleContainer1.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.puzzleContainer1.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.puzzleContainer1.Visible = false;
            // 
            // panel1
            // 
            this.panel1.BackgroundPath = "Puzzle\\puzzlebackground";
            this.panel1.Location = new Microsoft.Xna.Framework.Point(79, 73);
            this.panel1.Parent = this;
            this.panel1.Size = new Microsoft.Xna.Framework.Point(615, 470);
            this.panel1.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.panel1.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            // 
            // label2
            // 
            this.label2.BackgroundPath = "Options\\slidersbackempty";
            this.label2.Caption = "Warning: Puzzles will not be saved for temporary gamers";
            this.label2.Font = null;
            this.label2.FontColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.label2.FontPath = "Calibri11";
            this.label2.HorizontalAlignment = Sxe.Engine.UI.HorizontalAlignment.Center;
            this.label2.Location = new Microsoft.Xna.Framework.Point(209, 529);
            this.label2.Parent = this;
            this.label2.Selection = new Microsoft.Xna.Framework.Point(-1, -1);
            this.label2.Size = new Microsoft.Xna.Framework.Point(376, 55);
            this.label2.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.label2.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.label2.VerticalAlignment = Sxe.Engine.UI.VerticalAlignment.Middle;
            // 
            // panel2
            // 
            this.panel2.BackgroundPath = "Puzzle\\puzzlegamertagbox";
            this.panel2.Location = new Microsoft.Xna.Framework.Point(392, 47);
            this.panel2.Parent = this;
            this.panel2.Size = new Microsoft.Xna.Framework.Point(201, 48);
            this.panel2.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.panel2.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            // 
            // panel3
            // 
            this.panel3.BackgroundPath = "background";
            this.panel3.Location = new Microsoft.Xna.Framework.Point(402, 55);
            this.panel3.Parent = this;
            this.panel3.Size = new Microsoft.Xna.Framework.Point(27, 28);
            this.panel3.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.panel3.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            // 
            // label1
            // 
            this.label1.BackgroundPath = null;
            this.label1.Caption = "SuPeRLOnGTaG9";
            this.label1.FontColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.label1.FontPath = "Calibri11";
            this.label1.HorizontalAlignment = Sxe.Engine.UI.HorizontalAlignment.Left;
            this.label1.Location = new Microsoft.Xna.Framework.Point(432, 56);
            this.label1.Parent = this;
            this.label1.Selection = new Microsoft.Xna.Framework.Point(-1, -1);
            this.label1.Size = new Microsoft.Xna.Framework.Point(149, 29);
            this.label1.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.label1.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.label1.VerticalAlignment = Sxe.Engine.UI.VerticalAlignment.Middle;
            // 
            // PuzzleSelectionScreen
            // 
            this.Panels.Add(this.panel1);
            this.Panels.Add(this.puzzleContainer1);
            this.Panels.Add(this.panel2);
            this.Panels.Add(this.panel3);
            this.Panels.Add(this.label1);
            this.TransitionOffOffset = new Microsoft.Xna.Framework.Point(-500, 0);
            this.TransitionOnOffset = new Microsoft.Xna.Framework.Point(500, 0);

        }
    }
}
