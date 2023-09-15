using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using Sxe.Engine.UI;
using Sxe.Engine;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Quarx.Editor;
using Quarx.AI;



#if !XBOX
using System.Windows.Forms;
using System.ComponentModel;

namespace Quarx
{
    public class PuzzleEditor : BaseScreen
    {
        private EditorBoard editorBoard1;
        private Quarx.EditorButton editorButton1;
        private Quarx.EditorButton editorButton2;
        private Quarx.EditorButton editorButton3;
        private Quarx.EditorButton editorButton4;
        private Quarx.EditorButton editorButton5;
        private Quarx.EditorButton editorButton6;
        private Quarx.EditorButton editorButton7;
        private EditorButton selectedButton;
        private EditorButton editorButton8;
        private Sxe.Engine.UI.Buttons.ColorButton colorButton1;
        private Sxe.Engine.UI.Buttons.ColorButton colorButton2;
        private Sxe.Engine.UI.Buttons.ColorButton colorButton3;
        private Sxe.Engine.UI.Buttons.ColorButton colorButton4;
        private Quarx.Editor.ClusterEditor clusterEditor1;
        private Quarx.Editor.ClusterEditor clusterEditor2;
        private Quarx.Editor.ClusterEditor clusterEditor3;
        private Quarx.Editor.ClusterEditor clusterEditor4;
        private Quarx.Editor.ClusterEditor clusterEditor5;
        private Quarx.Editor.ClusterEditor clusterEditor6;
        private Quarx.Editor.ClusterEditor clusterEditor7;
        private Quarx.Editor.ClusterEditor clusterEditor8;
        private Sxe.Engine.UI.Label label1;

        private SimpleAIModel simpleAI;

        PuzzleDescription currentDescription;

        private ContentManager content;
        private Sxe.Engine.UI.Label label2;
        private Sxe.Engine.UI.Buttons.ColorButton colorButton5;
        private Sxe.Engine.UI.Buttons.ColorButton colorButton6;
        private Sxe.Engine.UI.Buttons.ColorButton colorButton7;
        private ClusterEditor[] clusters;


        public PuzzleDescription PuzzleDescription
        {
            get { return currentDescription; }
        }

        public EditorButton SelectedButton
        {
            get { return selectedButton; }
            set { selectedButton = value; }
        }

     

    
        public PuzzleEditor()
        {
            InitializeComponent();

            clusters = new ClusterEditor[8];
            clusters[0] = this.clusterEditor1;
            clusters[1] = this.clusterEditor2;
            clusters[2] = this.clusterEditor3;
            clusters[3] = this.clusterEditor4;
            clusters[4] = this.clusterEditor5;
            clusters[5] = this.clusterEditor6;
            clusters[6] = this.clusterEditor7;
            clusters[7] = this.clusterEditor8;

            for (int i = 0; i < clusters.Length; i++)
                clusters[i].Number = i.ToString();

            simpleAI = new SimpleAIModel(8, 18);
            this.editorBoard1.PuzzleChanged += OnPuzzleChanged;

   

        }

        private void OnPuzzleChanged(object sender, EventArgs args)
        {

            ResetAI();
        }

        private void ResetAI()
        {
            AIBoardModel board = new AIBoardModel(8, 18);
            board.SetupFromPuzzleDescription(this.PuzzleDescription);

            //int utility = this.simpleAI.GetUtility(board);


            //ac.AtomColor1 = BlockColor.Red;
            //ac.AtomColor2 = BlockColor.Yellow;

            int formation;
            Point point;

            AIMoveDescription moveDescription = new AIMoveDescription();
            moveDescription.Color1 = BlockColor.Red;
            moveDescription.Color2 = BlockColor.Red;

            int utility = this.simpleAI.GetBestPosition(board, moveDescription.Color1, moveDescription.Color2, out point, out formation);
            this.label2.Caption = "Utility: " + utility.ToString();



         
            moveDescription.Point1 = new Point(point.X + Formations.GetFormation(formation).Offset1.X,
                                                point.Y + Formations.GetFormation(formation).Offset1.Y);
            moveDescription.Point2 = new Point(point.X + Formations.GetFormation(formation).Offset2.X,
                                                point.Y + Formations.GetFormation(formation).Offset2.Y);

            //moveDescription.Point1 = new Point(0, 2);
            //moveDescription.Point2 = new Point(0, 3);

            board[moveDescription.Point1.X, moveDescription.Point1.Y].IsBlock = true;
            board[moveDescription.Point1.X, moveDescription.Point1.Y].Color = moveDescription.Color1;
            board[moveDescription.Point1.X, moveDescription.Point1.Y].Type = BlockType.Atom;
            board[moveDescription.Point2.X, moveDescription.Point2.Y].IsBlock = true;
            board[moveDescription.Point2.X, moveDescription.Point2.Y].Color = moveDescription.Color2;
            board[moveDescription.Point2.X, moveDescription.Point2.Y].Type = BlockType.Atom;


            utility = this.simpleAI.GetUtility(board, moveDescription.Point1, moveDescription.Point2);

            //this.editorBoard1.MoveDescription = moveDescription;
        }

        public override void Activate()
        {
            //HACK:
            //if (AnarchyGamer.Gamers.GetGamerByPlayerIndex(1) == null)
            //    AnarchyGamerComponent.GamerComponent.StartTemporaryProfile((Microsoft.Xna.Framework.PlayerIndex)1);

            //Handle the corner case, if there are no profiles
            if (AnarchyGamer.Gamers.GetGamerByPlayerIndex(0) == null)
                AnarchyGamerComponent.GamerComponent.StartTemporaryProfile((Microsoft.Xna.Framework.PlayerIndex)0);

            base.Activate();
        }

        public override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            this.content = content;

            this.currentDescription = new PuzzleDescription();
            //try
            //{
            //    this.currentDescription = new PuzzleDescription("_Puzzles/simple2.txt", content);
            //}
            //catch { }


            base.LoadContent(content);
        }

        void InitializeComponent()
        {
            this.editorBoard1 = new Quarx.EditorBoard();
            this.editorButton1 = new Quarx.EditorButton();
            this.editorButton2 = new Quarx.EditorButton();
            this.editorButton3 = new Quarx.EditorButton();
            this.editorButton4 = new Quarx.EditorButton();
            this.editorButton5 = new Quarx.EditorButton();
            this.editorButton6 = new Quarx.EditorButton();
            this.editorButton7 = new Quarx.EditorButton();
            this.editorButton8 = new Quarx.EditorButton();
            this.colorButton1 = new Sxe.Engine.UI.Buttons.ColorButton();
            this.colorButton2 = new Sxe.Engine.UI.Buttons.ColorButton();
            this.colorButton3 = new Sxe.Engine.UI.Buttons.ColorButton();
            this.colorButton4 = new Sxe.Engine.UI.Buttons.ColorButton();
            this.clusterEditor1 = new Quarx.Editor.ClusterEditor();
            this.clusterEditor2 = new Quarx.Editor.ClusterEditor();
            this.clusterEditor3 = new Quarx.Editor.ClusterEditor();
            this.clusterEditor4 = new Quarx.Editor.ClusterEditor();
            this.clusterEditor5 = new Quarx.Editor.ClusterEditor();
            this.clusterEditor6 = new Quarx.Editor.ClusterEditor();
            this.clusterEditor7 = new Quarx.Editor.ClusterEditor();
            this.clusterEditor8 = new Quarx.Editor.ClusterEditor();
            this.label1 = new Sxe.Engine.UI.Label();
            this.label2 = new Sxe.Engine.UI.Label();
            this.colorButton5 = new Sxe.Engine.UI.Buttons.ColorButton();
            this.colorButton6 = new Sxe.Engine.UI.Buttons.ColorButton();
            this.colorButton7 = new Sxe.Engine.UI.Buttons.ColorButton();
            // 
            // editorBoard1
            // 
            this.editorBoard1.BackgroundPath = "Editor\\board";
            this.editorBoard1.CanDrag = false;
            this.editorBoard1.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowAll;
            this.editorBoard1.Location = new Microsoft.Xna.Framework.Point(218, 71);
            this.editorBoard1.MoveDescription = null;
            this.editorBoard1.Parent = this;
            this.editorBoard1.Size = new Microsoft.Xna.Framework.Point(250, 450);
            this.editorBoard1.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.editorBoard1.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.editorBoard1.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.editorBoard1.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            // 
            // editorButton1
            // 
            this.editorButton1.BackgroundPath = "Editor\\blueatom";
            this.editorButton1.BlockColor = Quarx.BlockColor.Null;
            this.editorButton1.BlockType = Quarx.BlockType.Blob;
            this.editorButton1.CanDrag = false;
            this.editorButton1.ClickColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.editorButton1.ColorTransitionTime = System.TimeSpan.Parse("00:00:20");
            this.editorButton1.DefaultColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.editorButton1.FontColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.editorButton1.FontPath = null;
            this.editorButton1.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowAll;
            this.editorButton1.Location = new Microsoft.Xna.Framework.Point(129, 150);
            this.editorButton1.OverColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.editorButton1.OverCue = null;
            this.editorButton1.Parent = null;
            this.editorButton1.PressCue = null;
            this.editorButton1.Size = new Microsoft.Xna.Framework.Point(30, 30);
            this.editorButton1.Text = "";
            this.editorButton1.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.editorButton1.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.editorButton1.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.editorButton1.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            // 
            // editorButton2
            // 
            this.editorButton2.BackgroundPath = "Editor\\blueatom";
            this.editorButton2.BlockColor = Quarx.BlockColor.Blue;
            this.editorButton2.BlockType = Quarx.BlockType.Atom;
            this.editorButton2.CanDrag = false;
            this.editorButton2.ClickColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(192)), ((byte)(192)), ((byte)(192)), ((byte)(255)));
            this.editorButton2.ColorTransitionTime = System.TimeSpan.Parse("00:00:01");
            this.editorButton2.DefaultColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.editorButton2.FontColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.editorButton2.FontPath = null;
            this.editorButton2.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowAll;
            this.editorButton2.Location = new Microsoft.Xna.Framework.Point(174, 144);
            this.editorButton2.OverColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(0)), ((byte)(128)), ((byte)(0)), ((byte)(255)));
            this.editorButton2.OverCue = null;
            this.editorButton2.Parent = this;
            this.editorButton2.PressCue = null;
            this.editorButton2.Size = new Microsoft.Xna.Framework.Point(30, 30);
            this.editorButton2.Text = "";
            this.editorButton2.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.editorButton2.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.editorButton2.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.editorButton2.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            // 
            // editorButton3
            // 
            this.editorButton3.BackgroundPath = "Editor\\blueisotope";
            this.editorButton3.BlockColor = Quarx.BlockColor.Blue;
            this.editorButton3.BlockType = Quarx.BlockType.Isotope;
            this.editorButton3.CanDrag = false;
            this.editorButton3.ClickColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(192)), ((byte)(192)), ((byte)(192)), ((byte)(255)));
            this.editorButton3.ColorTransitionTime = System.TimeSpan.Parse("00:00:01");
            this.editorButton3.DefaultColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.editorButton3.FontColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.editorButton3.FontPath = null;
            this.editorButton3.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowAll;
            this.editorButton3.Location = new Microsoft.Xna.Framework.Point(174, 177);
            this.editorButton3.OverColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(0)), ((byte)(128)), ((byte)(0)), ((byte)(255)));
            this.editorButton3.OverCue = null;
            this.editorButton3.Parent = this;
            this.editorButton3.PressCue = null;
            this.editorButton3.Size = new Microsoft.Xna.Framework.Point(30, 30);
            this.editorButton3.Text = "";
            this.editorButton3.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.editorButton3.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.editorButton3.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.editorButton3.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            // 
            // editorButton4
            // 
            this.editorButton4.BackgroundPath = "Editor\\redatom";
            this.editorButton4.BlockColor = Quarx.BlockColor.Red;
            this.editorButton4.BlockType = Quarx.BlockType.Atom;
            this.editorButton4.CanDrag = false;
            this.editorButton4.ClickColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(192)), ((byte)(192)), ((byte)(192)), ((byte)(255)));
            this.editorButton4.ColorTransitionTime = System.TimeSpan.Parse("00:00:01");
            this.editorButton4.DefaultColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.editorButton4.FontColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.editorButton4.FontPath = null;
            this.editorButton4.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowAll;
            this.editorButton4.Location = new Microsoft.Xna.Framework.Point(173, 211);
            this.editorButton4.OverColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(0)), ((byte)(128)), ((byte)(0)), ((byte)(255)));
            this.editorButton4.OverCue = null;
            this.editorButton4.Parent = this;
            this.editorButton4.PressCue = null;
            this.editorButton4.Size = new Microsoft.Xna.Framework.Point(30, 30);
            this.editorButton4.Text = "";
            this.editorButton4.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.editorButton4.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.editorButton4.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.editorButton4.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            // 
            // editorButton5
            // 
            this.editorButton5.BackgroundPath = "Editor\\redisotope";
            this.editorButton5.BlockColor = Quarx.BlockColor.Red;
            this.editorButton5.BlockType = Quarx.BlockType.Isotope;
            this.editorButton5.CanDrag = false;
            this.editorButton5.ClickColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(192)), ((byte)(192)), ((byte)(192)), ((byte)(255)));
            this.editorButton5.ColorTransitionTime = System.TimeSpan.Parse("00:00:01");
            this.editorButton5.DefaultColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.editorButton5.FontColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.editorButton5.FontPath = null;
            this.editorButton5.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowAll;
            this.editorButton5.Location = new Microsoft.Xna.Framework.Point(172, 246);
            this.editorButton5.OverColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(0)), ((byte)(128)), ((byte)(0)), ((byte)(255)));
            this.editorButton5.OverCue = null;
            this.editorButton5.Parent = this;
            this.editorButton5.PressCue = null;
            this.editorButton5.Size = new Microsoft.Xna.Framework.Point(30, 30);
            this.editorButton5.Text = "";
            this.editorButton5.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.editorButton5.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.editorButton5.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.editorButton5.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            // 
            // editorButton6
            // 
            this.editorButton6.BackgroundPath = "Editor\\yellowatom";
            this.editorButton6.BlockColor = Quarx.BlockColor.Yellow;
            this.editorButton6.BlockType = Quarx.BlockType.Atom;
            this.editorButton6.CanDrag = false;
            this.editorButton6.ClickColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(192)), ((byte)(192)), ((byte)(192)), ((byte)(255)));
            this.editorButton6.ColorTransitionTime = System.TimeSpan.Parse("00:00:01");
            this.editorButton6.DefaultColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.editorButton6.FontColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.editorButton6.FontPath = null;
            this.editorButton6.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowAll;
            this.editorButton6.Location = new Microsoft.Xna.Framework.Point(172, 284);
            this.editorButton6.OverColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(0)), ((byte)(128)), ((byte)(0)), ((byte)(255)));
            this.editorButton6.OverCue = null;
            this.editorButton6.Parent = this;
            this.editorButton6.PressCue = null;
            this.editorButton6.Size = new Microsoft.Xna.Framework.Point(30, 30);
            this.editorButton6.Text = "";
            this.editorButton6.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.editorButton6.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.editorButton6.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.editorButton6.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            // 
            // editorButton7
            // 
            this.editorButton7.BackgroundPath = "Editor\\yellowisotope";
            this.editorButton7.BlockColor = Quarx.BlockColor.Yellow;
            this.editorButton7.BlockType = Quarx.BlockType.Isotope;
            this.editorButton7.CanDrag = false;
            this.editorButton7.ClickColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(192)), ((byte)(192)), ((byte)(192)), ((byte)(255)));
            this.editorButton7.ColorTransitionTime = System.TimeSpan.Parse("00:00:01");
            this.editorButton7.DefaultColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.editorButton7.FontColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.editorButton7.FontPath = null;
            this.editorButton7.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowAll;
            this.editorButton7.Location = new Microsoft.Xna.Framework.Point(172, 322);
            this.editorButton7.OverColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(0)), ((byte)(128)), ((byte)(0)), ((byte)(255)));
            this.editorButton7.OverCue = null;
            this.editorButton7.Parent = this;
            this.editorButton7.PressCue = null;
            this.editorButton7.Size = new Microsoft.Xna.Framework.Point(30, 30);
            this.editorButton7.Text = "";
            this.editorButton7.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.editorButton7.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.editorButton7.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.editorButton7.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            // 
            // editorButton8
            // 
            this.editorButton8.BackgroundPath = "Editor\\tileOutline";
            this.editorButton8.BlockColor = Quarx.BlockColor.Null;
            this.editorButton8.BlockType = Quarx.BlockType.Blob;
            this.editorButton8.CanDrag = false;
            this.editorButton8.ClickColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(192)), ((byte)(192)), ((byte)(192)), ((byte)(255)));
            this.editorButton8.ColorTransitionTime = System.TimeSpan.Parse("00:00:01");
            this.editorButton8.DefaultColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.editorButton8.FontColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.editorButton8.FontPath = null;
            this.editorButton8.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowAll;
            this.editorButton8.Location = new Microsoft.Xna.Framework.Point(173, 359);
            this.editorButton8.OverColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(0)), ((byte)(128)), ((byte)(0)), ((byte)(255)));
            this.editorButton8.OverCue = null;
            this.editorButton8.Parent = this;
            this.editorButton8.PressCue = null;
            this.editorButton8.Size = new Microsoft.Xna.Framework.Point(30, 30);
            this.editorButton8.Text = "";
            this.editorButton8.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.editorButton8.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.editorButton8.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.editorButton8.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            // 
            // colorButton1
            // 
            this.colorButton1.BackColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(128)), ((byte)(128)), ((byte)(128)), ((byte)(128)));
            this.colorButton1.BackgroundPath = "Editor\\slot";
            this.colorButton1.CanDrag = false;
            this.colorButton1.ClickColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(0)), ((byte)(255)), ((byte)(0)), ((byte)(128)));
            this.colorButton1.ColorTransitionTime = System.TimeSpan.Parse("00:00:01");
            this.colorButton1.DefaultColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(128)), ((byte)(128)), ((byte)(128)), ((byte)(128)));
            this.colorButton1.FontColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.colorButton1.FontPath = "Neuropol";
            this.colorButton1.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowAll;
            this.colorButton1.Location = new Microsoft.Xna.Framework.Point(22, 77);
            this.colorButton1.OverColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(0)), ((byte)(0)), ((byte)(255)), ((byte)(128)));
            this.colorButton1.OverCue = null;
            this.colorButton1.Parent = this;
            this.colorButton1.PressCue = null;
            this.colorButton1.Size = new Microsoft.Xna.Framework.Point(100, 50);
            this.colorButton1.Text = "Load";
            this.colorButton1.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.colorButton1.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.colorButton1.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.colorButton1.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.colorButton1.MouseClick += new System.EventHandler<Sxe.Engine.Input.MouseEventArgs>(this.colorButton1_MouseClick);
            // 
            // colorButton2
            // 
            this.colorButton2.BackColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(128)), ((byte)(128)), ((byte)(128)), ((byte)(128)));
            this.colorButton2.BackgroundPath = "Editor\\slot";
            this.colorButton2.CanDrag = false;
            this.colorButton2.ClickColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(0)), ((byte)(255)), ((byte)(0)), ((byte)(128)));
            this.colorButton2.ColorTransitionTime = System.TimeSpan.Parse("00:00:01");
            this.colorButton2.DefaultColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(128)), ((byte)(128)), ((byte)(128)), ((byte)(128)));
            this.colorButton2.FontColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.colorButton2.FontPath = "Neuropol";
            this.colorButton2.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowAll;
            this.colorButton2.Location = new Microsoft.Xna.Framework.Point(25, 139);
            this.colorButton2.OverColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(0)), ((byte)(0)), ((byte)(255)), ((byte)(128)));
            this.colorButton2.OverCue = null;
            this.colorButton2.Parent = this;
            this.colorButton2.PressCue = null;
            this.colorButton2.Size = new Microsoft.Xna.Framework.Point(100, 50);
            this.colorButton2.Text = "Save";
            this.colorButton2.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.colorButton2.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.colorButton2.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.colorButton2.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.colorButton2.ButtonPressed += new System.EventHandler<Sxe.Engine.UI.ButtonPressEventArgs>(this.colorButton2_ButtonPressed);
            // 
            // colorButton3
            // 
            this.colorButton3.BackColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(128)), ((byte)(128)), ((byte)(128)), ((byte)(128)));
            this.colorButton3.BackgroundPath = "Editor\\slot";
            this.colorButton3.CanDrag = false;
            this.colorButton3.ClickColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(0)), ((byte)(255)), ((byte)(0)), ((byte)(128)));
            this.colorButton3.ColorTransitionTime = System.TimeSpan.Parse("00:00:01");
            this.colorButton3.DefaultColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(128)), ((byte)(128)), ((byte)(128)), ((byte)(128)));
            this.colorButton3.FontColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.colorButton3.FontPath = "Neuropol";
            this.colorButton3.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowAll;
            this.colorButton3.Location = new Microsoft.Xna.Framework.Point(24, 196);
            this.colorButton3.OverColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(0)), ((byte)(0)), ((byte)(255)), ((byte)(128)));
            this.colorButton3.OverCue = null;
            this.colorButton3.Parent = this;
            this.colorButton3.PressCue = null;
            this.colorButton3.Size = new Microsoft.Xna.Framework.Point(100, 50);
            this.colorButton3.Text = "Test";
            this.colorButton3.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.colorButton3.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.colorButton3.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.colorButton3.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.colorButton3.MouseClick += new System.EventHandler<Sxe.Engine.Input.MouseEventArgs>(this.colorButton3_MouseClick);
            // 
            // colorButton4
            // 
            this.colorButton4.BackColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(128)), ((byte)(128)), ((byte)(128)), ((byte)(128)));
            this.colorButton4.BackgroundPath = "Editor\\slot";
            this.colorButton4.CanDrag = false;
            this.colorButton4.ClickColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(0)), ((byte)(255)), ((byte)(0)), ((byte)(128)));
            this.colorButton4.ColorTransitionTime = System.TimeSpan.Parse("00:00:01");
            this.colorButton4.DefaultColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(128)), ((byte)(128)), ((byte)(128)), ((byte)(128)));
            this.colorButton4.FontColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.colorButton4.FontPath = "Neuropol";
            this.colorButton4.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowAll;
            this.colorButton4.Location = new Microsoft.Xna.Framework.Point(24, 256);
            this.colorButton4.OverColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(0)), ((byte)(0)), ((byte)(255)), ((byte)(128)));
            this.colorButton4.OverCue = null;
            this.colorButton4.Parent = this;
            this.colorButton4.PressCue = null;
            this.colorButton4.Size = new Microsoft.Xna.Framework.Point(100, 50);
            this.colorButton4.Text = "Exit";
            this.colorButton4.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.colorButton4.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.colorButton4.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.colorButton4.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.colorButton4.MouseClick += new System.EventHandler<Sxe.Engine.Input.MouseEventArgs>(this.colorButton4_MouseClick);
            // 
            // clusterEditor1
            // 
            this.clusterEditor1.BackgroundPath = "previewbox2";
            this.clusterEditor1.CanDrag = false;
            this.clusterEditor1.Cluster = null;
            this.clusterEditor1.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowAll;
            this.clusterEditor1.Location = new Microsoft.Xna.Framework.Point(464, 70);
            this.clusterEditor1.Number = "1";
            this.clusterEditor1.Parent = this;
            this.clusterEditor1.Size = new Microsoft.Xna.Framework.Point(75, 75);
            this.clusterEditor1.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.clusterEditor1.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.clusterEditor1.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.clusterEditor1.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            // 
            // clusterEditor2
            // 
            this.clusterEditor2.BackgroundPath = "previewbox2";
            this.clusterEditor2.CanDrag = false;
            this.clusterEditor2.Cluster = null;
            this.clusterEditor2.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowAll;
            this.clusterEditor2.Location = new Microsoft.Xna.Framework.Point(468, 184);
            this.clusterEditor2.Number = "1";
            this.clusterEditor2.Parent = this;
            this.clusterEditor2.Size = new Microsoft.Xna.Framework.Point(75, 75);
            this.clusterEditor2.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.clusterEditor2.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.clusterEditor2.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.clusterEditor2.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            // 
            // clusterEditor3
            // 
            this.clusterEditor3.BackgroundPath = "previewbox2";
            this.clusterEditor3.CanDrag = false;
            this.clusterEditor3.Cluster = null;
            this.clusterEditor3.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowAll;
            this.clusterEditor3.Location = new Microsoft.Xna.Framework.Point(468, 304);
            this.clusterEditor3.Number = "1";
            this.clusterEditor3.Parent = this;
            this.clusterEditor3.Size = new Microsoft.Xna.Framework.Point(75, 75);
            this.clusterEditor3.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.clusterEditor3.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.clusterEditor3.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.clusterEditor3.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            // 
            // clusterEditor4
            // 
            this.clusterEditor4.BackgroundPath = "previewbox2";
            this.clusterEditor4.CanDrag = false;
            this.clusterEditor4.Cluster = null;
            this.clusterEditor4.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowAll;
            this.clusterEditor4.Location = new Microsoft.Xna.Framework.Point(469, 446);
            this.clusterEditor4.Number = "1";
            this.clusterEditor4.Parent = this;
            this.clusterEditor4.Size = new Microsoft.Xna.Framework.Point(75, 75);
            this.clusterEditor4.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.clusterEditor4.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.clusterEditor4.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.clusterEditor4.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            // 
            // clusterEditor5
            // 
            this.clusterEditor5.BackgroundPath = "previewbox2";
            this.clusterEditor5.CanDrag = false;
            this.clusterEditor5.Cluster = null;
            this.clusterEditor5.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowAll;
            this.clusterEditor5.Location = new Microsoft.Xna.Framework.Point(588, 73);
            this.clusterEditor5.Number = "1";
            this.clusterEditor5.Parent = this;
            this.clusterEditor5.Size = new Microsoft.Xna.Framework.Point(75, 75);
            this.clusterEditor5.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.clusterEditor5.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.clusterEditor5.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.clusterEditor5.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            // 
            // clusterEditor6
            // 
            this.clusterEditor6.BackgroundPath = "previewbox2";
            this.clusterEditor6.CanDrag = false;
            this.clusterEditor6.Cluster = null;
            this.clusterEditor6.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowAll;
            this.clusterEditor6.Location = new Microsoft.Xna.Framework.Point(591, 188);
            this.clusterEditor6.Number = "1";
            this.clusterEditor6.Parent = this;
            this.clusterEditor6.Size = new Microsoft.Xna.Framework.Point(75, 75);
            this.clusterEditor6.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.clusterEditor6.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.clusterEditor6.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.clusterEditor6.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            // 
            // clusterEditor7
            // 
            this.clusterEditor7.BackgroundPath = "previewbox2";
            this.clusterEditor7.CanDrag = false;
            this.clusterEditor7.Cluster = null;
            this.clusterEditor7.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowAll;
            this.clusterEditor7.Location = new Microsoft.Xna.Framework.Point(593, 304);
            this.clusterEditor7.Number = "1";
            this.clusterEditor7.Parent = this;
            this.clusterEditor7.Size = new Microsoft.Xna.Framework.Point(75, 75);
            this.clusterEditor7.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.clusterEditor7.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.clusterEditor7.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.clusterEditor7.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            // 
            // clusterEditor8
            // 
            this.clusterEditor8.BackgroundPath = "previewbox2";
            this.clusterEditor8.CanDrag = false;
            this.clusterEditor8.Cluster = null;
            this.clusterEditor8.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowAll;
            this.clusterEditor8.Location = new Microsoft.Xna.Framework.Point(594, 445);
            this.clusterEditor8.Number = "1";
            this.clusterEditor8.Parent = this;
            this.clusterEditor8.Size = new Microsoft.Xna.Framework.Point(75, 75);
            this.clusterEditor8.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.clusterEditor8.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.clusterEditor8.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.clusterEditor8.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            // 
            // label1
            // 
            this.label1.BackgroundPath = null;
            this.label1.CanDrag = false;
            this.label1.Caption = "Editor";
            this.label1.FontColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.label1.FontPath = "Neuropol";
            this.label1.HorizontalAlignment = Sxe.Engine.UI.HorizontalAlignment.Left;
            this.label1.Location = new Microsoft.Xna.Framework.Point(107, 14);
            this.label1.Parent = this;
            this.label1.Selection = new Microsoft.Xna.Framework.Point(-1, -1);
            this.label1.Size = new Microsoft.Xna.Framework.Point(134, 50);
            this.label1.TransitionOffOffset = new Microsoft.Xna.Framework.Point(-200, 0);
            this.label1.TransitionOnOffset = new Microsoft.Xna.Framework.Point(-200, 0);
            this.label1.VerticalAlignment = Sxe.Engine.UI.VerticalAlignment.Top;
            // 
            // label2
            // 
            this.label2.BackgroundPath = null;
            this.label2.CanDrag = false;
            this.label2.Caption = "Utility:";
            this.label2.FontColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.label2.FontPath = "Neuropol";
            this.label2.HorizontalAlignment = Sxe.Engine.UI.HorizontalAlignment.Left;
            this.label2.Location = new Microsoft.Xna.Framework.Point(239, 8);
            this.label2.Parent = this;
            this.label2.Selection = new Microsoft.Xna.Framework.Point(-1, -1);
            this.label2.Size = new Microsoft.Xna.Framework.Point(201, 50);
            this.label2.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.label2.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.label2.VerticalAlignment = Sxe.Engine.UI.VerticalAlignment.Top;
            // 
            // colorButton5
            // 
            this.colorButton5.BackColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(128)), ((byte)(128)), ((byte)(128)), ((byte)(128)));
            this.colorButton5.BackgroundPath = "Editor\\slot";
            this.colorButton5.CanDrag = false;
            this.colorButton5.ClickColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(0)), ((byte)(255)), ((byte)(0)), ((byte)(128)));
            this.colorButton5.ColorTransitionTime = System.TimeSpan.Parse("00:00:01");
            this.colorButton5.DefaultColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(128)), ((byte)(128)), ((byte)(128)), ((byte)(128)));
            this.colorButton5.FontColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.colorButton5.FontPath = "Neuropol";
            this.colorButton5.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowAll;
            this.colorButton5.Location = new Microsoft.Xna.Framework.Point(23, 382);
            this.colorButton5.OverColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(0)), ((byte)(0)), ((byte)(255)), ((byte)(128)));
            this.colorButton5.OverCue = null;
            this.colorButton5.Parent = this;
            this.colorButton5.PressCue = null;
            this.colorButton5.Size = new Microsoft.Xna.Framework.Point(100, 50);
            this.colorButton5.Text = "Test AI";
            this.colorButton5.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.colorButton5.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.colorButton5.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.colorButton5.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.colorButton5.MouseClick += new System.EventHandler<Sxe.Engine.Input.MouseEventArgs>(this.colorButton4_MouseClick);
            // 
            // colorButton6
            // 
            this.colorButton6.BackColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(128)), ((byte)(128)), ((byte)(128)), ((byte)(128)));
            this.colorButton6.BackgroundPath = "Editor\\slot";
            this.colorButton6.CanDrag = false;
            this.colorButton6.ClickColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(0)), ((byte)(255)), ((byte)(0)), ((byte)(128)));
            this.colorButton6.ColorTransitionTime = System.TimeSpan.Parse("00:00:01");
            this.colorButton6.DefaultColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(128)), ((byte)(128)), ((byte)(128)), ((byte)(128)));
            this.colorButton6.FontColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.colorButton6.FontPath = "Neuropol";
            this.colorButton6.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowAll;
            this.colorButton6.Location = new Microsoft.Xna.Framework.Point(22, 446);
            this.colorButton6.OverColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(0)), ((byte)(0)), ((byte)(255)), ((byte)(128)));
            this.colorButton6.OverCue = null;
            this.colorButton6.Parent = this;
            this.colorButton6.PressCue = null;
            this.colorButton6.Size = new Microsoft.Xna.Framework.Point(100, 50);
            this.colorButton6.Text = "Random";
            this.colorButton6.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.colorButton6.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.colorButton6.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.colorButton6.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.colorButton6.MouseClick += new System.EventHandler<Sxe.Engine.Input.MouseEventArgs>(this.colorButton6_MouseClick);
            // 
            // colorButton7
            // 
            this.colorButton7.BackColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(128)), ((byte)(128)), ((byte)(128)), ((byte)(128)));
            this.colorButton7.BackgroundPath = "Editor\\slot";
            this.colorButton7.CanDrag = false;
            this.colorButton7.ClickColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(0)), ((byte)(255)), ((byte)(0)), ((byte)(128)));
            this.colorButton7.ColorTransitionTime = System.TimeSpan.Parse("00:00:01");
            this.colorButton7.DefaultColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(128)), ((byte)(128)), ((byte)(128)), ((byte)(128)));
            this.colorButton7.FontColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.colorButton7.FontPath = "Neuropol";
            this.colorButton7.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowAll;
            this.colorButton7.Location = new Microsoft.Xna.Framework.Point(20, 507);
            this.colorButton7.OverColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(0)), ((byte)(0)), ((byte)(255)), ((byte)(128)));
            this.colorButton7.OverCue = null;
            this.colorButton7.Parent = this;
            this.colorButton7.PressCue = null;
            this.colorButton7.Size = new Microsoft.Xna.Framework.Point(100, 50);
            this.colorButton7.Text = "Mega Generate";
            this.colorButton7.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.colorButton7.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.colorButton7.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.colorButton7.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.colorButton7.MouseClick += new System.EventHandler<Sxe.Engine.Input.MouseEventArgs>(this.colorButton7_MouseClick);
            // 
            // PuzzleEditor
            // 
            this.BackgroundPath = "background";
            this.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowAll;
            this.Panels.Add(this.editorBoard1);
            this.Panels.Add(this.editorButton2);
            this.Panels.Add(this.editorButton3);
            this.Panels.Add(this.editorButton4);
            this.Panels.Add(this.editorButton5);
            this.Panels.Add(this.editorButton6);
            this.Panels.Add(this.editorButton7);
            this.Panels.Add(this.editorButton8);
            this.Panels.Add(this.colorButton1);
            this.Panels.Add(this.colorButton2);
            this.Panels.Add(this.colorButton3);
            this.Panels.Add(this.colorButton4);
            this.Panels.Add(this.clusterEditor1);
            this.Panels.Add(this.clusterEditor2);
            this.Panels.Add(this.clusterEditor3);
            this.Panels.Add(this.clusterEditor4);
            this.Panels.Add(this.clusterEditor5);
            this.Panels.Add(this.clusterEditor6);
            this.Panels.Add(this.clusterEditor7);
            this.Panels.Add(this.clusterEditor8);
            this.Panels.Add(this.label1);
            this.Panels.Add(this.label2);
            this.Panels.Add(this.colorButton5);
            this.Panels.Add(this.colorButton6);
            this.Panels.Add(this.colorButton7);

        }

        private void colorButton4_MouseClick(object sender, Sxe.Engine.Input.MouseEventArgs e)
        {
            this.GameScreenService.Game.Exit();
        }

        private void colorButton3_MouseClick(object sender, Sxe.Engine.Input.MouseEventArgs e)
        {
            FinalizePuzzle();
            PuzzleGame puzzleGame = new PuzzleGame();
            puzzleGame.PuzzleDescription = this.currentDescription;
            puzzleGame.Settings = RoundSettings.CreateFromQuarxSettings(QuarxGameSettings.Easy, 0);
            //puzzleGame.Settings.GamerSettings[0].Gamer = AnarchyGamer.Gamers[0];
        
            this.GameScreenService.AddScreen(puzzleGame);
        }

        private void colorButton1_MouseClick(object sender, Sxe.Engine.Input.MouseEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog openFile = new System.Windows.Forms.OpenFileDialog();
                        openFile.Multiselect = false;
                        openFile.FileOk += LoadFile;
            openFile.ShowDialog();


        }

        private void FinalizePuzzle()
        {
            this.currentDescription.AtomClusters.Clear();

            for (int i = 0; i < this.clusters.Length; i++)
            {
                AtomClusterDescription ac = this.clusters[i].Cluster;
                if (ac != null)
                    this.currentDescription.AtomClusters.Add(ac);
            }
        }

        private void SaveFile(object sender, CancelEventArgs args)
        {
            PuzzleSaveDialog dialog = sender as PuzzleSaveDialog;


            if (dialog != null && args.Cancel == false)
            {
                FinalizePuzzle();

                FileStream fs = new FileStream(dialog.Path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                StreamWriter sw = new StreamWriter(fs);
                this.currentDescription.Save(dialog.PuzzleName, dialog.IconName, sw);
                sw.Close();
                fs.Close();
            }

            this.Input.Mouse.FreezeMouse = true;

        }

        private void LoadFile(object sender, CancelEventArgs args)
        {
            OpenFileDialog dialog = sender as OpenFileDialog;
            if (dialog != null && args.Cancel == false)
            {
                this.currentDescription = new PuzzleDescription(dialog.FileName, this.content);


                for (int i = 0; i < clusters.Length; i++)
                    clusters[i].Cluster = null;

                for (int i = 0; i < Math.Min(currentDescription.AtomClusters.Count, 8); i++)
                {
                    this.clusters[i].Cluster = currentDescription.AtomClusters[i];
                }
            }


        }

        private void colorButton2_ButtonPressed(object sender, ButtonPressEventArgs e)
        {
            PuzzleSaveDialog saveFileDialog = new PuzzleSaveDialog();
            saveFileDialog.Name = currentDescription.LevelName;
            saveFileDialog.IconName = currentDescription.IconName;
            saveFileDialog.FileOk += SaveFile;
            this.Input.Mouse.FreezeMouse = false;

            saveFileDialog.ShowDialog();

        }

        private void colorButton6_MouseClick(object sender, Sxe.Engine.Input.MouseEventArgs e)
        {

            RandomizePuzzle();
            ResetAI();
        }

        private void GenerateRandomPuzzles()
        {
            int puzzle = 0;

            FileStream log = new FileStream("generate_log.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            StreamWriter logWriter = new StreamWriter(log);

            for (int i = 0; i < 1000000; i++)
            {
                RandomizePuzzle();
                PuzzleGameModel model = new PuzzleGameModel(8, 18, AnarchyGamer.Gamers[0], this.PuzzleDescription);
                model.Start(8, 18, 1, 1);
                model.State = QuarxGameState.Playing;

                int count = 0;
                for(int x = 0; x < model.Board.Width; x++)
                {
                    for (int y = 0; y < model.Board.Height; y++)
                    {
                        if (model.Board[x, y] != null)
                            count++;
                    }
                 }

                if (count < 16)
                    continue;

                double delta = 0.1;
                double seconds = delta;
                model.Update(Sxe.Engine.Utilities.Utilities.CreateGameTime(seconds));
                while (model.State == QuarxGameState.Playing)
                {
                    model.Update(Sxe.Engine.Utilities.Utilities.CreateGameTime(seconds));
                    seconds += delta;
                }

                if (model.State == QuarxGameState.Won)
                {
                    FileStream fs = new FileStream("puzzle" + i.ToString() + ".txt", FileMode.OpenOrCreate,FileAccess.ReadWrite );
                    StreamWriter sw = new StreamWriter(fs);
                    PuzzleDescription.Save("hi", "hi", sw);
                    sw.Close();
                    fs.Close();

                }

                logWriter.WriteLine(String.Format("Puzzle: {0} Result: {1} Time: {2}", i, model.State, seconds));

            }
            logWriter.Close();
            log.Close();
        }


        private void RandomizePuzzle()
        {
            Random random = new Random();

            double occupantProbability = random.NextDouble();

            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 14; y++)
                {
                    Tile tile = new Tile();

                    double isBlock = random.NextDouble();
                    if (isBlock > occupantProbability)
                    {
                        tile.IsBlock = true;

                        tile.Color = (BlockColor)random.Next(0, 3);

                        double isIsotope = random.NextDouble();
                        if (isIsotope > 0.7)
                            tile.Type = BlockType.Isotope;
                        else
                            tile.Type = BlockType.Atom;
                    }

                    this.PuzzleDescription.SetTile(tile, x, y);
                }
            }
        }

        private void colorButton7_MouseClick(object sender, Sxe.Engine.Input.MouseEventArgs e)
        {
            this.GenerateRandomPuzzles();
        }




    }
}
#endif
