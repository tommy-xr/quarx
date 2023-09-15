using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Sxe.Engine.UI;
using Sxe.Engine.Input;

using Quarx.Puzzle;

namespace Quarx
{
    public class EditorBoard : CompositePanel
    {
        
        UIImage redIsotope;
        UIImage blueIsotope;
        UIImage yellowIsotope;
        UIImage redAtom;
        UIImage blueAtom;
        UIImage yellowAtom;
        UIImage tileImage;


        const int xPadding = 20;
        const int yPadding = 20;

        Point selectedTile = new Point(-1, -1);

        public event EventHandler<EventArgs> PuzzleChanged;

        private AIMoveDescription moveDescription;
        public AIMoveDescription MoveDescription
        {
            get { return moveDescription; }
            set { moveDescription = value; }
        }

        private int XDelta
        {
            get { return (this.Size.X - xPadding * 2) / 8; }
        }

        private int YDelta
        {
            get { return (this.Size.Y - yPadding * 2) / 18; }
        }

        public PuzzleDescription Description
        {
            get 
            {
                if (Editor == null)
                    return null;
                return Editor.PuzzleDescription; 
            }
        }

        PuzzleEditor Editor
        {
            get { return Parent as PuzzleEditor; }
        }


        public EditorBoard()
        {
            InitializeComponent();
        }

        public override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            redIsotope = this.LoadImage(content, "Editor/redIsotope");
            redAtom = this.LoadImage(content, "Editor/redAtom");
            yellowAtom = this.LoadImage(content, "Editor/yellowAtom");
            yellowIsotope = this.LoadImage(content, "Editor/yellowIsotope");
            blueAtom = this.LoadImage(content, "Editor/blueAtom");
            blueIsotope = this.LoadImage(content, "Editor/blueIsotope");

            tileImage = this.LoadImage(content, "Editor/tileOutline");

            base.LoadContent(content);
        }


        public override void  Paint(SpriteBatch sb, Point positionOffset, Vector2 scale)
        {
 	         base.Paint(sb, positionOffset, scale);
             DrawDescription(sb, positionOffset, scale);
        }

        void DrawDescription(SpriteBatch spriteBatch, Point positionOffset, Vector2 scale)
        {
            if (Description == null)
                return;




            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 18; y++)
                {
                    Rectangle destination = new Rectangle(
                            xPadding + XDelta * x + positionOffset.X + this.Location.X,
                            yPadding + YDelta * (17-y) + positionOffset.Y + this.Location.Y ,
                            XDelta, YDelta);

              

                    destination = this.AdjustRectangle(destination, scale);

                    Color tileColor = new Color(128, 128, 128, 128);

                    if (x == selectedTile.X && y == selectedTile.Y)
                        tileColor = new Color(0, 255, 0, 255);

                    if (moveDescription != null)
                    {
                        if (moveDescription.Point1.X == x && moveDescription.Point1.Y == y)
                            tileColor = new Color(255, 0, 0, 255);

                        if (moveDescription.Point2.X == x && moveDescription.Point2.Y == y)
                            tileColor = new Color(255, 0, 0, 255);
                    }

                    tileImage.Draw(spriteBatch, destination, tileColor);

                    Tile tile = Description.GetTile(x, y);

                    if (moveDescription != null)
                    {
                        if (moveDescription.Point1.X == x && moveDescription.Point1.Y == y)
                        {
                            tile = new Tile();
                            tile.IsBlock = true;
                            tile.Color = moveDescription.Color1;
                            tile.Type = BlockType.Atom;
                        }
                        else if (moveDescription.Point2.X == x && moveDescription.Point2.Y == y)
                        {
                            tile = new Tile();
                            tile.IsBlock = true;
                            tile.Color = moveDescription.Color2;
                            tile.Type = BlockType.Atom;
                        }
                    }
                    
                    
                    
                    if (tile != null && tile.IsBlock)
                    {
                        
                        Color color = Color.White;
                        if(tile.Type == BlockType.Atom)
                        {
                            switch(tile.Color)
                            {
                                case BlockColor.Blue:
                                    blueAtom.Draw(spriteBatch, destination, color);
                                    break;
                                case BlockColor.Red:
                                    redAtom.Draw(spriteBatch, destination, color);
                                    break;
                                case BlockColor.Yellow:
                                    yellowAtom.Draw(spriteBatch, destination, color);
                                    break;
                            }
                        }
                        else if(tile.Type == BlockType.Isotope)
                        {
                            switch(tile.Color)
                            {
                                case BlockColor.Blue:
                                    blueIsotope.Draw(spriteBatch, destination, color);
                                    break;
                                case BlockColor.Red:
                                    redIsotope.Draw(spriteBatch, destination, color);
                                    break;
                                case BlockColor.Yellow:
                                    yellowIsotope.Draw(spriteBatch, destination, color);
                                    break;
                            }
                        }
                    }
                }
            }

        }

        void OnMouseLeave(object sender, EventArgs args)
        {
            this.selectedTile = new Point(-1, -1);
        }

        public override bool HandleEvent(Sxe.Engine.Input.InputEventArgs inputEvent)
        {
            MouseEventArgs mouseEvent = inputEvent as MouseEventArgs;
            if (mouseEvent != null)
            {
                if (mouseEvent.MouseEventType == MouseEventType.Move)
                {

                    
                    //Find a selected tile
                    int x = mouseEvent.Position.X - (this.Location.X + xPadding);
                    int y = mouseEvent.Position.Y - (this.Location.Y + yPadding);
                    x /= XDelta;
                    y /= YDelta;

                    y = 17 - y;

                    if(x < 0 || x > 7)
                    x = -1;
                    if(y < 0 || y > 17)
                    y = -1;

                    selectedTile = new Point(x, y);


                    if (mouseEvent.LeftButtonPressed)
                    {
                        //Get the tile for this position
                        if (selectedTile.X > -1 && selectedTile.Y > -1)
                        {
                            if (Editor != null && Editor.SelectedButton != null)
                            {
                                //Place the tile
                                Tile tile = Description.GetTile(selectedTile.X, selectedTile.Y);

                                if (tile == null)
                                {
                                    tile = new Tile();
                                }

                                tile.Color = Editor.SelectedButton.BlockColor;
                                tile.Type = Editor.SelectedButton.BlockType;
                                if (Editor.SelectedButton.BlockType == BlockType.Blob)
                                    tile.IsBlock = false;
                                else
                                    tile.IsBlock = true;

                                this.Description.SetTile(tile, selectedTile.X, selectedTile.Y);

                                if (PuzzleChanged != null)
                                    PuzzleChanged(this, EventArgs.Empty);
                            }

                        }
                    }
                }
            }



            return base.HandleEvent(inputEvent);
        }

        void InitializeComponent()
        {
            // 
            // EditorBoard
            // 
            this.BackgroundPath = "Editor\\board";
            this.Size = new Microsoft.Xna.Framework.Point(250, 450);

        }
    }
}
