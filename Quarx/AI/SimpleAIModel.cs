using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;

namespace Quarx.AI
{
    public class SimpleAIModel : BaseAIModel
    {
        public SimpleAIModel(int width, int height)
            : base(width, height)
        {
        }

        public override int GetUtility(AIBoardModel board, Point p1, Point p2)
        {
            int utility = 0;

            //Calculate the utility of items when we they get deleted
            utility += this.GetRemoveUtility(board, p1, p2);

            //Calculate overall utility
            utility += this.GetColumnUtility(board, p1.X, p1.Y);
            utility += this.GetColumnUtility(board, p2.X, p2.Y);

            utility += GetHeightUtility(board);

            return utility;

            //throw new NotImplementedException();
        }

        public int GetHeightUtility(AIBoardModel board)
        {
            int maxY = 0;
            for (int x = 0; x < board.Width; x++)
            {
                for (int y = 0; y < board.Height; y++)
                {
                    Tile tile = board[x, y];
                    if (tile.IsBlock && y > maxY)
                        maxY = y;

                }
            }

            return -maxY;
        }

        /// <summary>
        /// Get the utility of deleted isotopes
        /// </summary>
        public int GetRemoveUtility(AIBoardModel model, Point p1, Point p2)
        {
            int utility = 0;
            //If we are in the same row, it doesn't matter, we'll just use the first one
            utility += this.ClearLines(model, GetHorizontalRunStart(model, p1), new Point(1, 0));
            utility += this.ClearLines(model, GetHorizontalRunStart(model, p2), new Point(1, 0));


        //If we are in the same column, pick the higher one
        if (p1.X == p2.X)
        {
                utility += this.ClearLines(model, p1, new Point(0, -1));
                utility += this.ClearLines(model, p2, new Point(0, -1));   
        }
        //We aren't in the same column, so do this separately
        else
        {
            utility += this.ClearLines(model, p1, new Point(0, -1));
            utility += this.ClearLines(model, p2, new Point(0, -1));
        }

            return utility;
        }

        /// <summary>
        /// Helper function to find the beginning of a horizontal run
        /// </summary>
        public Point GetHorizontalRunStart(AIBoardModel model, Point start)
        {
            int x = start.X-1;
            int count = 0;
            BlockColor startColor = model[start.X, start.Y].Color;
            BlockColor color = startColor;
            while (x >= 0 && color == startColor)
            {

                Tile tile = model[x, start.Y];
                color = tile.Color;

                if (!tile.IsBlock)
                    color = BlockColor.Null;
                else if (color == startColor)
                    count++;

                x--;
            }

            return new Point(start.X - count, start.Y);
        }


        public int ClearLines(AIBoardModel board, Point startPoint, Point delta)
        {
            BlockColor startColor = board[startPoint.X, startPoint.Y].Color;
            BlockColor color = startColor;
            Point point = startPoint;

            point.X += delta.X;
            point.Y += delta.Y;
            int count = 1;

            while (point.X >= 0 && point.X < board.Width && point.Y >= 0 && point.Y < board.Height && color == startColor)
            {
                color = board[point.X, point.Y].Color;
                if (!board[point.X, point.Y].IsBlock)
                    color = BlockColor.Null;

                if (color == startColor) 
                    count++;
                point.X += delta.X;
                point.Y += delta.Y;
            }

            int utility = 0;
            Point iteration = startPoint;
            if (count >= 4)
            {
                //This assumes delta x is always positive and delta y is always n egative. Since i'm the only one using that, this is ok i guess =)
                while (  (iteration.X < point.X && delta.X > 0) || ( iteration.Y > point.Y && Math.Abs(delta.Y) > 0))
                {
                    Tile tile = board[iteration.X, iteration.Y];
                    if (tile.Type == BlockType.Isotope)
                        utility += 5;
                    else
                        utility += 2;

                    //Clear out the tile
                    tile.Color = BlockColor.Null;
                    tile.IsBlock = false;

                    iteration.X += delta.X;
                    iteration.Y += delta.Y;
                }
            }

            return utility;
        }

        public int GetColumnUtility(AIBoardModel board, int column, int startY)
        {
            int utility = 0;

            int y = startY;
            Tile tile = board[column, y];
            BlockColor color = tile.Color;

            bool isBlocking = false;

            //First, lets figure out how many isotopes are here
            int isotopes = 0;
            for (int i = 0; i < board.Height; i++)
            {
                if (board[column, i].IsBlock && board[column, i].Type == BlockType.Isotope)
                    isotopes++;
            }

            while ( ((tile.Color == color && tile.IsBlock) || !tile.IsBlock) && y >= 0)
            {
                tile = board[column, y];

                if (tile.IsBlock && tile.Color == color)
                    utility++;
                else if (tile.IsBlock)
                    isBlocking = true;

                y--;
            }

            if (utility == 1 && isBlocking)
                utility = -10;

            //if (utility >= 5)
            //    utility *= 2;

            utility *= isotopes;

            return utility;



        }

        //public override int GetUtility(AIBoardModel board)
        //{
        //    int utility = 0;
        //    int currentRun;
        //    int currentIsotopes;
        //    int reward;
        //    int maxY = 0;
        //    for (int x = 0; x < board.Width; x++)
        //    {
        //        currentRun = 0;
        //        currentIsotopes = 0;
        //        reward = 1;
        //        BlockColor color = BlockColor.Null;
        //        //Start at height, add
        //        for (int y = 0; y < board.Height; y++ )
        //        {
        //            Tile tile = board[x, y];
        //            if (tile.IsBlock && maxY < y)
        //                maxY = y;

        //            if (tile.Type == BlockType.Isotope && tile.IsBlock)
        //            {


        //                //currentIsotopes++;
        //                reward += 2;
        //            }

        //            if (color != BlockColor.Null && (color == board[x, y].Color && board[x, y].IsBlock))
        //            {

        //                //utility += reward;
        //                currentRun++;
        //            }
        //            else if (color != BlockColor.Null && board[x, y].Color == BlockColor.Null)
        //            {
        //                currentRun = 0;
        //            }

        //            else if (color == BlockColor.Null && (board[x, y].Color != BlockColor.Null && board[x, y].IsBlock))
        //            {
        //                color = board[x, y].Color;
        //            }
        //            else if (color != BlockColor.Null && board[x, y].Color != BlockColor.Null
        //                && color != board[x, y].Color && board[x, y].IsBlock)
        //            {
        //                if (currentRun <= 3)
        //                    utility -=  y * reward;
        //                else
        //                    utility += currentRun * reward;

        //                color = board[x, y].Color;

        //                currentIsotopes = 0;
        //                currentRun = 0;
        //            }


        //        }


        //        utility += reward * currentRun * 3;
        //    }

        //    utility -= maxY;


        //    return utility;
        //}


        //protected override int GetUtility(AIBoardModel model)
        //{
        //    int utility = 0;

        //    //Add utility for columns
        //    for (int x = 0; x < model.Width; x++)
        //    {
        //        Point start = new Point(x, 0);
        //        Point end = new Point(x, model.Height - 1);

        //        Point delta = new Point(0, 1);
        //        utility += GetUtilityForLine(model, start, end, delta);
        //    }

        //    //Add utility for rows
        //    for (int y = 0; y < model.Height; y++)
        //    {
        //        Point start = new Point(0, y);
        //        Point end = new Point(model.Width - 1, y);
        //        Point delta = new Point(1, 0);
        //        utility += GetUtilityForLine(model, start, end, delta);
        //    }

        //    return utility;

        //}

        int GetUtilityForLine(AIBoardModel model, Point start, Point end, Point delta)
        {
            Point current = start;

            int utility = 0;
            int currentCount = 0;

            BlockColor color = BlockColor.Null;

            while (current.X != end.X || current.Y != end.Y)
            {

                //Check the block color here
                if (model[current.X, current.Y].IsBlock)
                {
                    if (color == model[current.X, current.Y].Color)
                    {
                        if (model[current.X, current.Y].Type == BlockType.Isotope)
                            utility += 4;

                        utility += 2;

                    }

                        //Color is not equal...
                    else if(color != BlockColor.Null)
                    {
                        utility--;
                    }

                    color = model[current.X, current.Y].Color;

                }
                else
                    color = BlockColor.Null;



                //else
                //{
                //    if (color != BlockColor.Null)
                //    {
                //        if(currentCount > 1)
                //        utility += currentCount;
                //        //currentCount = 0;
                //    }
                //    //else
                //    //{
                //    //color = BlockColor.Null;
                //}

                current = new Point(current.X + delta.X, current.Y + delta.Y);
            }
            return utility;
        }
    }
}
