using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;

using Sxe.Engine.Input;

namespace Quarx
{
    /// <summary>
    /// Base class for all droppable items in Quarx
    /// </summary>
    public class Block 
    {
        const double FallTime = 0.075;

        Point position;
        double nextDrop;
        QuarxGameBoard board;
        BaseGameModel model;
        BlockColor color;
        Block linkedAtom = null;
        BlockType type = BlockType.Isotope;
        float transitionPosition = 0.0f;
        double removeTime = 0.0f;
        double removeTransitionTime = 1.0f;
        bool hasDropped = false;
        int nearbyBlocks = 0;
        float rotation = 0f;
        bool punishBlock = false;

        public bool HasDropped
        {
            get { return hasDropped; }
            set { hasDropped = value; }
        }


        public virtual bool UserControlled
        {
            get { return false; }
        }

        protected QuarxGameBoard Board
        {
            get { return board; }
        }

        public Point Position
        {
            get { return position; }
        }

        public BlockColor Color
        {
            get { return color; }
            set { color = value; }
        }

        public BlockType Type
        {
            get { return type; }
        }

        public bool PunishBlock
        {
            get { return punishBlock; }
            set { punishBlock = value; }
        }

        public byte Alpha
        {
            get { return (byte)(255 - (255 * transitionPosition)); }
        }

        public float FadeAmount
        {
            get { return 1.0f - ((float)Alpha / 255f); }
        }

        public float TransitionPosition
        {
            get { return transitionPosition; }
        }

        public bool IsRemoving
        {
            get { return removeTime != 0.0; }
        }

        public int NearbyBlocks
        {
            get { return nearbyBlocks; }
            set { nearbyBlocks = value; }
        }

        public float Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }

        public virtual Block LinkedBlock
        {
            get { return linkedAtom; }
            set { linkedAtom = value;  }
        }

        public bool Drop(GameTime gameTime)
        {
            if(position.Y > 0 && CanMove(new Point(position.X, position.Y - 1)))
            {
                if (nextDrop < gameTime.TotalGameTime.TotalSeconds)
                {
                    Move(position.X, position.Y - 1);

                    nextDrop = gameTime.TotalGameTime.TotalSeconds + model.NonControlledDelay;
                }

                return true;
            }

            return false;
        }

        public bool Move(int x, int y)
        {
            return Move(new Point(x, y));
        }

        public virtual void OnAdd(BaseGameModel model) { }
        public virtual void OnRemove() 
        {
            if (board.IsInBounds(position))
                board[position] = null;

            if (linkedAtom != null)
                linkedAtom.LinkedBlock = null;
        }

        public virtual bool Move(Point movePosition)
        {
            //If we can't move there, don't bother!
            if (!CanMove(movePosition))
                return false;

            SetPosition(movePosition);

            return true;

        }

        protected virtual void SetPosition(Point setPosition)
        {
            //If we have a current position, erase it from the board
            if (position.X >= 0 && position.Y >= 0)
            {
                if (board[position] == this)
                    board[position] = null;

                //if (board[position] != this)
                //    throw new Exception("Error: Positions somehow inconsistent. This should not happen");

                //board[position] = null;
            }

            //Now, set our position to the move position
            position = setPosition;

            //Update the game board with our new position
            board[position] = this;
        }

        public Block(BaseGameModel inModel, BlockColor blockColor, BlockType blockType)
        {
            model = inModel;
            board = model.Board;
            position = new Point(-1, -1);

            color = blockColor;
            type = blockType;

        }

        public Block(BaseGameModel model, BlockColor blockColor, Point startPoint)
            : this(model, blockColor, BlockType.Isotope)
        {


            if (Board.IsPointOccupied(startPoint))
                throw new Exception("Something occupied the start point!");

            SetPosition(startPoint);


        }

        /// <summary>
        /// Returns true if the atom can drop, false otherwise
        /// </summary>
        public virtual bool CanDrop()
        {

            if (position.Y <= 0) return false;


            return CanMove(new Point(position.X, position.Y - 1));
        }

        /// <summary>
        /// Handles updating for removing the atom and fading it out
        /// </summary>
        public bool UpdateTransition(GameTime gameTime)
        {
            double time = gameTime.TotalGameTime.TotalSeconds;

            if (!IsRemoving)
                return false;
 
                transitionPosition = (float)((time - removeTime) / removeTransitionTime);

                if (transitionPosition >= 1.0f)
                {
                    model.Remove(this);
                    return true;
                }

                return false;
        }

        /// <summary>
        /// Returns true if the block can move, false otherwise
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public virtual bool CanMove(Point position)
        {
            if (this.Type != BlockType.Atom)
                return false;
            else
            {
                if (position.X < 0 || position.Y < 0)
                    return false;

                if (position.X >= Board.Width || position.Y >= Board.Height)
                    return false;

                //Is the position occupied?
                Block b = Board[position];

                if (b == null || b == this || b == linkedAtom)
                    return true;

                return false;
            }

        }



        public void StartRemove(GameTime gameTime)
        {
            removeTime = gameTime.TotalGameTime.TotalSeconds;
        }


    }
}
