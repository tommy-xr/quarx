using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;

namespace Quarx
{
    /// <summary>
    /// The game board for a quarx game
    /// </summary>
    public class QuarxGameBoard
    {

        Block[,] blocks; //array of blocks for the game

        int width;
        int height;

        public int Width
        {
            get { return width; }
        }

        public int Height
        {
            get { return height; }
        }

        public Block this[int x, int y]
        {
            get { return blocks[x, y]; }
            set 
            { 
                blocks[x, y] = value;

                if (x == 0 && y == 0)
                {
                    int i = 0;
                }
            }
        }

        public Block this[Point p]
        {
            get { return this[p.X, p.Y]; }
            set { this[p.X, p.Y] = value; }
        }

        public QuarxGameBoard(int boardWidth, int boardHeight)
        {
            width = boardWidth;
            height = boardHeight;

            blocks = new Block[width, height];
        }

        public bool IsInBounds(Point p)
        {
            if (p.X >= 0 && p.Y >= 0)
            {
                if (p.X < Width && p.Y < Height)
                    return true;
            }

            return false;
        }

        public void Clear()
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    this[x, y] = null;
                }
            }
        }

        public bool IsPointOccupied(Point p)
        {
            if (this[p] == null)
                return false;

            return true;
        }

    }
}
