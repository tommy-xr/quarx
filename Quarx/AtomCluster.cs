using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using Sxe.Engine.Input;

namespace Quarx
{
    public static class Formations
    {
        static ClusterFormation[] formations =
            new ClusterFormation[4]
            {   
                new ClusterFormation(new Point(0, 0), new Point(1, 0)),
                new ClusterFormation(new Point(1, 1), new Point(1, 0)),
                new ClusterFormation(new Point(1, 0), new Point(0,0)),
                new ClusterFormation(new Point(1, 0), new Point(1, 1))

            };

        public static ClusterFormation GetFormation(int i)
        {
            return formations[i];
        }


    }

    /// <summary>
    /// A description of a formation of a cluster
    /// Describes the positions based on the center of the cluster
    /// </summary>
    public class ClusterFormation
    {
        Point p1;
        Point p2;

        public Point Offset1
        {
            get { return p1; }
        }

        public Point Offset2
        {
            get { return p2; }
        }

        public ClusterFormation(Point point1, Point point2)
        {
            p1 = point1;
            p2 = point2;
        }
    }

    /// <summary>
    /// Cluster of atoms
    /// The position is a little different for a cluster
    /// It is the "center of mass"
    /// </summary>
    public class AtomCluster
    {
        const double QuickDropTime = 0.05; //how fast a "quick drop" - when the player presses a down button

        Point position;

        Point atom1Position;
        Point atom2Position;
        BlockColor atomColor1;
        BlockColor atomColor2;

        int currentFormation = 0;

        BaseGameModel model;
        QuarxGameBoard board;

        ClusterFormation formation;

        bool valid = false;
        double nextQuickDropTime = 0.0;
        double nextLateralMoveTime = 0.0;
        double nextDropTime = 0.0;

        bool shouldDrop = false;

        public Point Position
        {
            get { return position; }
        }


        public Point Position1
        {
            get { return atom1Position; }
        }

        public Point Position2
        {
            get { return atom2Position; }
        }

        public BlockColor AtomColor1
        {
            get { return atomColor1; }
        }

        public BlockColor AtomColor2
        {
            get { return atomColor2; }
        }

        public int Formation
        {
            get { return currentFormation; }
        }

        public bool Valid
        {
            get { return valid; }
        }

        public AtomCluster(BaseGameModel gameModel, BlockColor color1, BlockColor color2, Point startPosition)
        {
            model = gameModel;
            board = gameModel.Board;
          

            position = startPosition;
            atomColor1 = color1;
            atomColor2 = color2;

            valid = SetFormation();

        }


        bool Move(Point movePosition)
        {
            if (movePosition.X < -1 || movePosition.X >= board.Width)
                return false;

            if (movePosition.Y < 0 || movePosition.Y >= board.Height)
                return false;

            Point lastPosition = position;
            position = movePosition;
            bool result = SetFormation();

            if (!result)
                position = lastPosition;

            return result;
        }

        bool  SetFormation()
        {
            formation = Formations.GetFormation(currentFormation);
            Point newPosition1 = new Point(position.X + formation.Offset1.X, position.Y + formation.Offset1.Y);
            Point newPosition2 = new Point(position.X + formation.Offset2.X, position.Y + formation.Offset2.Y);

            if (board.IsInBounds(newPosition1) && board.IsInBounds(newPosition2))
            {

                if (board[newPosition1] == null && board[newPosition2] == null)
                {

                    atom1Position = newPosition1;
                    atom2Position = newPosition2;
                    return true;
                }
            }

            return false;
        }



        public void Move(Point position, int formation)
        {
            int oldFormation = currentFormation;

            Move(position);

            currentFormation = formation;
            if (!SetFormation())
                currentFormation = oldFormation;
        }

        public void RotateRight()
        {
            if (position.X <= -1)
                Move(new Point(position.X + 1, position.Y));

            int oldFormation = currentFormation;

            currentFormation++;
            currentFormation = currentFormation % 4;
            if (!SetFormation())
            {
                currentFormation = oldFormation;
            }

            model.PlaySound("atom_revolve");

        }

        public void RotateLeft()
        {
            if (position.X <= -1)
                Move(new Point(position.X + 1, position.Y));

            int oldFormation = currentFormation;

            currentFormation--;
            if (currentFormation < 0)
                currentFormation = 3;
            if (!SetFormation())
            {
                currentFormation = oldFormation;
            }

            model.PlaySound("atom_revolve");
        }

        public void MoveLeft(double time)
        {
            Move(new Point(position.X - 1, position.Y));
        }

        public void MoveRight(double time)
        {
            
            Move(new Point(position.X + 1, position.Y));
        }

        public void Drop()
        {
            shouldDrop = true;
        }


        public void Update(GameTime gameTime)
        {
            double time = gameTime.TotalGameTime.TotalSeconds;

            //if (controller != null)
            //{
            //    if (controller.IsKeyJustPressed("right"))
            //    {
            //        RotateRight();
            //    }

            //    if (controller.IsKeyJustPressed("left"))
            //    {
            //        RotateLeft();

            //    }

            //    if (controller.IsKeyJustPressed("move_left") ||
            //        (controller.IsKeyDown("move_left") && nextLateralMoveTime < time))
            //    {
            //        MoveLeft(time);
            //    }

            //    if (controller.IsKeyJustPressed("move_right") ||
            //        (controller.IsKeyDown("move_right") && nextLateralMoveTime < time))
            //    {
            //        MoveRight(time);
            //    }


            //    if (controller.IsKeyDown("move_down"))
            //        Drop();
            //}


                if( (nextDropTime < time) || ( nextQuickDropTime < time)
                     && shouldDrop)
                {
                    if (shouldDrop)
                        shouldDrop = false;
                    if(!Move(new Point(this.position.X, this.position.Y - 1)))
                    {
                        if (valid)
                        {
                            //If we hit something, lets just create our shizzz
                            Block a1 = new Block(this.model, atomColor1, BlockType.Atom);
                            Block a2 = new Block(this.model, atomColor2, BlockType.Atom);


                            a1.LinkedBlock = a2;
                            a2.LinkedBlock = a1;

                            this.model.Add(a1);
                            this.model.Add(a2);
                            a1.Move(atom1Position);
                            a2.Move(atom2Position);
                            model.OnClusterLanded(this);

               
                        }
                        
                    }
                    else
                    {
                        nextDropTime = time + model.FallDelay;
                        nextQuickDropTime = time + QuickDropTime;
                    }
                }
            }
        }



    
}
