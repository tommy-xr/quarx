using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;


namespace Quarx.AI
{
    /// <summary>
    /// A lightweight version of the gameboard to be easier on the garbage collector
    /// </summary>
    public class AIBoardModel
    {
        Tile[,] tiles;
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

        public Tile this[int x, int y]
        {
            get
            {
                return tiles[x, y];
            }
            
        }

        public AIBoardModel(int inWidth, int inHeight)
        {
            width = inWidth;
            height = inHeight;
            tiles = new Tile[width, height];

            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                    tiles[x, y] = new Tile();
        }

        public AIBoardModel Clone()
        {
            AIBoardModel outModel = new AIBoardModel(this.width, this.height);
            this.CopyTo(outModel);

            return outModel;
        }

        //Copies the data from this board to another
        public void CopyTo(AIBoardModel model)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (model.tiles[x, y] == null)
                        model.tiles[x, y] = new Tile();

                    model.tiles[x, y].IsBlock = this[x, y].IsBlock;
                    model.tiles[x, y].Color = this[x, y].Color;
                    model.tiles[x, y].Type = this[x, y].Type;
                }
            }
        }

        public void SetupFromPuzzleDescription(PuzzleDescription description)
        {
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 18; y++)
                {
                    Tile getTile = description.GetTile(x, y);
                    if (getTile == null)
                    {
                        tiles[x, y] = new Tile();
                        tiles[x, y].IsBlock = false;
                    }
                    else
                        tiles[x, y] = getTile.Copy();
                }
            }
        }



        /// <summary>
        /// Sets the tile info based on a specific board
        /// </summary>
        /// <param name="board"></param>
        public void SetupFromBoard(QuarxGameBoard board)
        {
            for (int x = 0; x < board.Width; x++)
            {
                for (int y = 0; y < board.Height; y++)
                {
                    if (board[x, y] == null)
                        this[x, y].IsBlock = false;
                    else
                    {
                        this[x, y].IsBlock = true;
                        this[x, y].Color = board[x, y].Color;
                        this[x, y].Type = board[x, y].Type;
                    }
                }
            }
        }
    }



    public abstract class BaseAIModel : BaseGameModel
    {

        //Action[] plan;
        //int planLevels;
        //int currentAction = -1;
        int maxLevel = -1;
        float punishThreshold = -1f;
        float nextPunishEvaluation = 1f;
        float punishEvaluationRate = 0.25f;
        const float punishWait = 0.5f;
        float nextPunishTime = punishWait;
        double nextMoveTime = 0.0;


        int difficulty = 0;


        Point desiredPosition;
        int desiredFormation;

        Stack<Action> planQueue;

        bool isDroppingFast = false;
        Random random = new Random();

        protected Random Random
        {
            get { return random; }
        }

        public int Difficulty
        {
            get { return difficulty; }
            set { difficulty = value; }
        }

        public float AdjDifficulty
        {
            get { return (float)difficulty / 5f; }
        }

        public int Seed
        {
            set
            {
                random = new Random(value);
            }
        }

        public Point DesiredPosition
        {
            get { return desiredPosition; }
        }

        public int DesiredFormation
        {
            get { return desiredFormation; }
        }

        public BaseAIModel(int width, int height)
            : base(width, height)
        {
            //planLevels = inPlanLevels;
            //plan = new Action[planLevels];
            planQueue = new Stack<Action>();
        }

        protected virtual bool IsValid(AIBoardModel board, Point point, int formation)
        {
            ClusterFormation f = Formations.GetFormation(formation);

            Point p1 = new Point(point.X + f.Offset1.X, point.Y + f.Offset1.Y);
            Point p2 = new Point(point.X + f.Offset2.X, point.Y + f.Offset2.Y);

            if (p1.X < 0 || p1.X >= board.Width)
                return false;

            if (p2.X < 0 || p2.X >= board.Width)
                return false;

            if (p1.Y < 0 || p1.Y >= board.Height)
                return false;

            if (p2.Y < 0 || p2.Y >= board.Height)
                return false;

            if (board[p1.X, p1.Y].IsBlock || board[p2.X, p2.Y].IsBlock)
                return false;

            return true;
        }

        public abstract int GetUtility(AIBoardModel board, Point p1, Point p2 );

        //protected virtual bool CreatePlan(AIBoardModel board, AtomCluster current, Point desiredPoint, int desiredFormation)
        //{
        //    Point startPosition = current.Position;
        //    int startFormation = current.Formation;

        //    planQueue.Clear();

        //    for (int i = 0; i < plan.Length; i++)
        //        plan[i].ActionType = ActionType.None;

        //    //Enqueue a default first node
        //    Action start = new Action();
        //    start.StartPoint = startPosition;
        //    start.StartFormation = startFormation;
        //    start.ActionType = ActionType.None;
        //    start.EndFormation = startFormation;
        //    start.EndPoint = startPosition;
        //    start.Level = 0;

        //    planQueue.Push(start);

        //    while (true)
        //    {
        //        //If we don't have anything left in the queue, return false
        //        if (planQueue.Count == 0)
        //            return false;

        //        Action currentAction = planQueue.Pop();

        //        //Let's check and see if this action is maxed out
        //        if (currentAction.Level >= planLevels)
        //            return false;

        //        //Ok, its not, so lets add it to the current plan
        //        plan[currentAction.Level] = currentAction;

        //        //Is this a victory conditoin? If so, return true
        //        if (currentAction.EndFormation == desiredFormation &&
        //            currentAction.EndPoint == desiredPoint)
        //        {
        //            maxLevel = currentAction.Level;
        //            return true;

        //        }
        //        //Well, its not bad, but its not a victory condition... Let's try some other actions

        //        Point newStartPoint = new Point(currentAction.EndPoint.X, currentAction.EndPoint.Y - 1);
        //        Action moveLeftAction = new Action();
        //        moveLeftAction = currentAction;
        //        moveLeftAction.Level++;
        //        moveLeftAction.ActionType = ActionType.MoveLeft;
        //        moveLeftAction.StartPoint = newStartPoint;
        //        moveLeftAction.StartFormation = currentAction.EndFormation;
        //        moveLeftAction.EndPoint = new Point(newStartPoint.X - 1, newStartPoint.Y);

        //        Action moveRightAction = new Action();
        //        moveRightAction = currentAction;
        //        moveRightAction.Level++;
        //        moveRightAction.ActionType = ActionType.MoveRight;
        //        moveRightAction.StartPoint = newStartPoint;
        //        moveRightAction.StartFormation = currentAction.EndFormation;
        //        moveRightAction.EndPoint = new Point(newStartPoint.X + 1, newStartPoint.Y);

        //        Action dropAction = new Action();
        //        dropAction = currentAction;
        //        dropAction.Level++;
        //        dropAction.ActionType = ActionType.Down;
        //        dropAction.StartPoint = newStartPoint;
        //        dropAction.StartFormation = currentAction.EndFormation;
        //        dropAction.EndPoint = new Point(currentAction.EndPoint.X, currentAction.EndPoint.Y - 1);

        //        if(IsValid(board, moveLeftAction.EndPoint, moveLeftAction.EndFormation))
        //        planQueue.Push(moveLeftAction);

        //        if(IsValid(board, moveRightAction.EndPoint, moveRightAction.EndFormation))
        //        planQueue.Push(moveRightAction);
        //        //planQueue.Enqueue(dropAction);


        //    }
        //}


        /// <summary>
        /// Handles the logic of finding the best position that has an executable plan
        /// </summary>
        /// 

        public virtual int GetBestPosition(QuarxGameBoard board, AtomCluster current, AtomCluster next, out Point position, out int formation)
        {

            AIBoardModel model = new AIBoardModel(board.Width, board.Height);
            model.SetupFromBoard(board);

            position = Point.Zero;
            formation = 0;

            if (current == null)
                return -1;

            return this.GetBestPosition(model, current.AtomColor1, current.AtomColor2, out position, out formation);
        }

        public virtual int GetBestPosition(AIBoardModel baseModel, BlockColor currentColor1, BlockColor currentColor2, out Point position, out int formation)
        {
            int maxUtility = int.MinValue;
            position = new Point(-1, -1);
            formation = -1;

            //if (current == null)
            //    return -1;

            AIBoardModel model = baseModel.Clone();


            for (int x = -1; x < baseModel.Width; x++)
            {
                //for (int y = 0; y < board.Height; y++)
                //{
                    for (int f = 0; f < 4; f++)
                    {
                        //TODO: Fix as this is a GC nightmare....
                        baseModel.CopyTo(model);

                        int currentPositionY = 17;

                        if (IsValid(model, new Point(x, currentPositionY-1), f))
                        {
                            ClusterFormation form = Formations.GetFormation(f);
                            Point p1 = new Point(x + form.Offset1.X, currentPositionY + form.Offset1.Y);
                            Point p2 = new Point(x + form.Offset2.X, currentPositionY + form.Offset2.Y);

                            
                               
                            int y = GetMaxY(model, p1.X, p2.X);
                          

                            Point fp1 = new Point(x + form.Offset1.X, y + form.Offset1.Y);
                            Point fp2 = new Point(x + form.Offset2.X, y + form.Offset2.Y);

                                model[fp1.X, fp1.Y].IsBlock = true;
                                model[fp2.X, fp2.Y].IsBlock = true;
                            model[fp1.X, fp1.Y].Color = currentColor1;
                            model[fp2.X, fp2.Y].Color = currentColor2;

                            //Set model[x,y] to cluster colors, based on x y and formation offset

                            int utility = GetUtility(model, fp1, fp2);
                            model[fp1.X, fp1.Y].IsBlock = false;
                            model[fp2.X, fp2.Y].IsBlock = false;


                                if (utility >= maxUtility)
                                {
                                    //Can we create a plan for this?

                                    //if (CreatePlan(model, CurrentAtomCluster, new Point(x, y), f))
                                    //{

                                    double randValue = Random.NextDouble() * 0.5;

                                    if ( (randValue < this.AdjDifficulty || maxUtility <= int.MinValue))
                                    {
                                        if ((utility == maxUtility && random.NextDouble() > 0.5) || utility != maxUtility)
                                        {
                                            maxUtility = utility;
                                            position = new Point(x, y);
                                            formation = f;
                                        }
                                    }
                                    //}
                                }
                            }
                    }
                //}
            }

            return maxUtility;
            //position = maxPoint;
            //formation = maxFormation;


        }

        /// <summary>
        /// Gets the highest y for each row
        /// </summary>
        int GetMaxY(AIBoardModel model, int x1, int x2)
        {
            int maxY1 = 0;
            int maxY2 = 0;

            for (int y = 0; y < model.Height; y++)
            {

                if (model[x1, y].IsBlock)
                    maxY1 = y + 1;




                if (model[x2, y].IsBlock)
                    maxY2 = y + 1;
            }

            int val = (int)Math.Max(maxY1, maxY2);
            if (val >= 17)
                val = 17;

            return val;
        }

        public override void SpawnNewBlock()
        {


            base.SpawnNewBlock();

            //Create a plan here
            //Point newPosition;
            //int newFormation;
            
            //AIBoardModel model = new AIBoardModel(Board.Width, Board.Height);
            //model.SetupFromBoard(Board);
            ////int newUtility = GetUtility(model);


            Point position;
            int formation;
            int utility = GetBestPosition(Board, CurrentAtomCluster, NextAtomCluster, out position, out formation);
            this.desiredPosition = position;
            this.desiredFormation = formation;
            //currentAction = 0;

            double randValue = random.NextDouble();
            if (randValue - 0.1 < (double)AdjDifficulty)
                isDroppingFast = true;
            else
                isDroppingFast = false;


        }

        

        public override void Update(GameTime gameTime)
        {
            if (this.PunishModels.Count > 0)
            {
                if (this.nextPunishEvaluation >= 1f)
                {
                    //find the person with the min isotopes
                    int minIsotopes = 100;
                    BaseGameModel targetModel = this.PunishModels[0];
                    for (int i = 0; i < this.PunishModels.Count; i++)
                    {
                        if (this.PunishModels[i].Isotopes < minIsotopes)
                        {
                            minIsotopes = this.PunishModels[i].Isotopes;
                            targetModel = this.PunishModels[i];
                        }
                    }

                    //Now, increment until we get to that dude
                    while (targetModel != this.PunishTarget)
                    {
                        this.IncrementPunishTarget();
                    }

                    this.nextPunishEvaluation = 0f;
                    this.punishEvaluationRate = 5f + ((float)(this.random.NextDouble() - 0.5) * 5f);
                }
                else
                    this.nextPunishEvaluation += (float)gameTime.ElapsedGameTime.TotalSeconds * this.punishEvaluationRate;


                if (this.punishThreshold <= 0)
                {
                    this.punishThreshold = (float)this.random.NextDouble() / (float)difficulty;
                    this.nextPunishTime = punishWait + (float)this.random.NextDouble();
                }


                if (this.PunishLevel >= this.punishThreshold && this.punishThreshold > 0)
                {
                    if (this.nextPunishTime < 0f)
                    {
                        this.punishThreshold = -1f;
                        //TODO: make this better
                        this.Punish();
                    }
                    else
                        this.nextPunishTime -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
            }
 
            base.Update(gameTime);
        }

        public override void HandleInput(GameTime gameTime)
        {


            if(CurrentAtomCluster != null)
            {

                if (desiredFormation == CurrentAtomCluster.Formation
                    && desiredPosition.X == CurrentAtomCluster.Position.X)
                {
                    if (isDroppingFast)
                        this.CurrentAtomCluster.Drop();
                }
                else
                {
                    double time = gameTime.TotalGameTime.TotalSeconds;

                    if (nextMoveTime < time)
                    {

                        if (CurrentAtomCluster.Formation < desiredFormation)
                        {
                            CurrentAtomCluster.RotateRight();
                        }
                        else if (CurrentAtomCluster.Formation > desiredFormation)
                        {
                            CurrentAtomCluster.RotateLeft();
                        }

                        if (CurrentAtomCluster.Position.X < desiredPosition.X)
                        {
                            CurrentAtomCluster.MoveRight(time);
                        }
                        else if (CurrentAtomCluster.Position.X > desiredPosition.X)
                        {
                            CurrentAtomCluster.MoveLeft(time);
                        }



                        //nextMoveTime = time;
                        nextMoveTime = time + 0.1 + (0.5 - 0.3 * AdjDifficulty) * random.NextDouble();
                    }
                }
                
                //if(!didStuff && isDroppingFast)
                //    CurrentAtomCluster.Drop();

   
                
            }

            //if (CurrentAtomCluster != null)
            //{
            //    if (currentAction < plan.Length && currentAction <= maxLevel && currentAction >= 0)
            //    {
            //        Action current = plan[currentAction];

            //            if(CurrentAtomCluster.Position == current.StartPoint 
            //                && CurrentAtomCluster.Formation == current.StartFormation)
            //            {
            //                switch (current.ActionType)
            //                {
            //                    case ActionType.MoveLeft:
            //                        CurrentAtomCluster.MoveLeft(gameTime.TotalGameTime.TotalSeconds);
            //                        break;
            //                    case ActionType.MoveRight:
            //                        CurrentAtomCluster.MoveRight(gameTime.TotalGameTime.TotalSeconds);
            //                        break;
            //                    case ActionType.Down:
            //                        CurrentAtomCluster.Drop();
            //                        break;

            //                }
            //                currentAction++;
            //            }


                    
            //    }

            //    CurrentAtomCluster.Drop();
            //}

            base.HandleInput(gameTime);
        }


    }
}
