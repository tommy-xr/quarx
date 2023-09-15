using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Sxe.Engine.Input;
using Sxe.Engine;

namespace Quarx
{
    public class BaseGameModel
    {
        QuarxGameBoard board;

        double nextDrop = 0.0;
        const double nextDropDelay = 0.1;
        const double nonControlledDelay = 0.25; //how long it should take blocks to move


        double fallDelay = 1.0; //how long it takes controlled blocks to drop
        int isotopes;
        int score;

        int scoreMultiplier = 1;


        //bool hasStartedFadeOut = false;

        //IGameController controller;

        List<Block> blocks = new List<Block>();
        List<Block> blocksToUpdate = new List<Block>();
        List<BlockColor> currentPunish = new List<BlockColor>(); //this is the list of blocks that are being added to the current round of doubles
        List<BlockColor> readyPunish = new List<BlockColor>(); //this is the list of blocks that have already been added to punish
        List<int> punishPositions = new List<int>(); //this list assists in picking out a random position for a punish to happen
        Queue<BlockColor[]> punishQueue = new Queue<BlockColor[]>(); //this is a queue of impending punishes
        //BlockColor[] punishColors;
        List<Block> removeBlocks = new List<Block>();
        Random punishRandom = new Random();
        List<BaseGameModel> punishModels = new List<BaseGameModel>();
        List<BaseGameModel> removePunishModels = new List<BaseGameModel>();
        Random random = new Random();
        QuarxGameState state = QuarxGameState.Waiting;
        Queue<string> sounds = new Queue<string>();
        AtomCluster currentAtomCluster;
        AtomCluster nextAtomCluster;
        IAnarchyGamer gamer;
        bool isFalling = false;
        int currentPunishIndex = 0;
        bool showStart = false;


        #region Properties
        public AtomCluster CurrentAtomCluster
        {
            get { return currentAtomCluster; }
        }

        public AtomCluster NextAtomCluster
        {
            get { return nextAtomCluster; }
        }

        public bool IsFalling
        {
            get { return isFalling; }
        }

        public bool ShowStart
        {
            get { return showStart; }
            set { showStart = value; }
        }

        public virtual IAnarchyGamer Gamer
        {
            get { return gamer; }
            set { gamer = value; }
        }

        public virtual IGameController Controller
        {
            get { return null; }
        }

        double fallDelayModifier = 0.0;
        public double FallDelayModifier
        {
            set { this.fallDelayModifier = value; }
            
        }

        public double FallDelay
        {
            get { return fallDelay - (fallDelay * 0.4 * this.fallDelayModifier); }
            set { fallDelay = value; }
        }

        public GlobalSettings GlobalSettings
        {
            get;
            set;
        }

        public int Isotopes
        {
            get { return isotopes; }
        }

        public float PunishLevel
        {
            get 
            {
                if (this.State == QuarxGameState.Lost)
                    return 0;

                return (float)this.readyPunish.Count / (float)this.GlobalSettings.MaxStorePunish; 
            }
        }

        public QuarxMarqueeType Marquee
        {
            get;
            set;
        }

        public BaseGameModel PunishTarget
        {
            get
            {
                if (this.currentPunishIndex < 0)
                    return null;
    
                if(this.punishModels.Count == 0)
                    return null;

                if (this.currentPunishIndex >= this.punishModels.Count)
                    this.currentPunishIndex = this.punishModels.Count - 1;


                return this.punishModels[this.currentPunishIndex];
            }
        }

        public int Score
        {
            get { return score; }
        }

        public int ScoreMultiplier
        {
            get { return scoreMultiplier; }
        }

        public QuarxGameBoard Board 
        {
            get { return board; }
        }

        public QuarxGameState State
        {
            get { return state; }
            set { state = value; }
        }

        //public bool IsPunishing
        //{
        //    get { return isPunishing; }
        //}

        public double NonControlledDelay
        {
            get { return nonControlledDelay; }
        }


        /// <summary>
        /// Queue of impending punishing
        /// </summary>
        public Queue<BlockColor[]> PunishQueue
        {
            get { return punishQueue; }
        }

        public List<BaseGameModel> PunishModels
        {
            get { return punishModels; }
        }

        /// <summary>
        /// The punish that this model has ready to go
        /// </summary>
        //public BlockColor[] PunishColors
        //{
        //    get
        //    {
        //        //BlockColor[] outArray = punishColors;
        //        //if (outArray != null)
        //        //{
        //        //    punishColors = null;
        //        //}
        //        //return outArray;
        //        return readyPunish.ToArray();
        //    }

        //}


        #endregion

        public void Reset()
        {
            board.Clear();
            score = 0;
            scoreMultiplier = 1;
            punishPositions.Clear();
            punishQueue.Clear();
            removeBlocks.Clear();
            sounds.Clear();
        }

        public void Add(Block b)
        {
            //b.OnAdd(this);
            blocks.Add(b);
        }

        public void Remove(Block b)
        {
            if (blocks.Contains(b))
            {
                blocks.Remove(b);

            }
            b.OnRemove();

            
        }

        public string GetNextSound()
        {
            if (sounds.Count <= 0)
                return null;
            else
            {
                return sounds.Dequeue();
            }
        }

        public BaseGameModel(int width, int height)
        {
            board = new QuarxGameBoard(width, height);
 
        }

        public void PlaySound(string sound)
        {
            sounds.Enqueue(sound);
        }

        public void OnClusterLanded(AtomCluster cluster)
        {
            if (cluster == currentAtomCluster)
                currentAtomCluster = null;

 
        }

        public void Punish()
        {
            if(this.currentPunishIndex >= 0 && this.currentPunishIndex < this.punishModels.Count)
            {

                if (this.readyPunish.Count > 0)
                {
                    punishModels[this.currentPunishIndex].PunishQueue.Enqueue(this.readyPunish.ToArray());
                    this.readyPunish.Clear();
                    this.PlaySound("punish");
                }
                else
                    this.PlaySound("punish_fail");
                
                //this.PunishColors = null;
            }

            //this.isPunishing = false;
        }

        public virtual void Update(GameTime gameTime)
        {
            //if (isPunishing)
            //{
            //    if(punishModels.Count < 1)
            //    {
            //        isPunishing = false;
            //    }
            //    else if (punishModels.Count == 1)
            //    {
            //        Punish(0);
            //    }

            //}
            if (state == QuarxGameState.Lost || state == QuarxGameState.Won)
            {
                for (int x = 0; x < board.Width; x++)
                {
                    for (int y = 0; y < board.Height; y++)
                    {
                        if (board[x, y] != null)
                            board[x, y].UpdateTransition(gameTime);
                    }
                }
            }
            else if (state == QuarxGameState.Playing)
            {
                this.CleanupPunish();

                double time = gameTime.TotalGameTime.TotalSeconds;

                isotopes = CountIsotopes();

                bool isRemoving = false;


                blocksToUpdate.Clear();
                for (int i = 0; i < blocks.Count; i++)
                {
                    blocksToUpdate.Add(blocks[i]);
                }

                bool didDrop = false;


                //Update all the atoms/blocks
                int dropCount; //number of stuff that can fall


                bool firstPass = true;
                do
                {
                    dropCount = 0;
                    removeBlocks.Clear();
                    for (int i = 0; i < blocksToUpdate.Count; i++)
                    {
                        if (firstPass)
                            blocksToUpdate[i].UpdateTransition(gameTime);

                        if (blocksToUpdate[i].IsRemoving)
                            isRemoving = true;


                        if (blocksToUpdate[i].IsRemoving == false)
                        {
                            if (blocksToUpdate[i].LinkedBlock != null)
                            {
                                if (blocksToUpdate[i].CanDrop() && blocksToUpdate[i].LinkedBlock.CanDrop())
                                {
                                    blocksToUpdate[i].Drop(gameTime);
                                    blocksToUpdate[i].LinkedBlock.Drop(gameTime);
                                    dropCount += 2;
                                    removeBlocks.Add(blocksToUpdate[i]);
                                    removeBlocks.Add(blocksToUpdate[i].LinkedBlock);
                                    didDrop = true;
                                }
                            }
                            else
                            {
                                if (blocksToUpdate[i].CanDrop())
                                {
                                    blocksToUpdate[i].Drop(gameTime);
                                    dropCount++;
                                    removeBlocks.Add(blocksToUpdate[i]);
                                    didDrop = true;
                                }
                                else if (blocksToUpdate[i].PunishBlock)
                                {
                                    blocksToUpdate[i].PunishBlock = false;
                                }
                            }
                        }
                    }

                    firstPass = false;

                    //Remove all the blocks we dropped, so they can't get dropped again
                    for (int i = 0; i < removeBlocks.Count; i++)
                        blocksToUpdate.Remove(removeBlocks[i]);

                } while (dropCount > 0);

                isFalling = didDrop;


                //Check victory and loss conditions
                if (CheckVictoryCondition())
                    state = QuarxGameState.Won;
                else if (CheckLossCondition() && !isRemoving)
                    state = QuarxGameState.Lost;




                if (!didDrop)
                {
                    HandleInput(gameTime);

                    if (CurrentAtomCluster != null)
                    {
                        CurrentAtomCluster.Update(gameTime);
                    }
                    else
                    {
                        if (nextDrop == 0.0)
                        {
                            nextDrop = time + nextDropDelay;
                        }
                        else if (nextDrop < time)
                        {
                            if (!isRemoving)
                            {
                                //Check if anything is currently on the punish queue
                                //If so, add those blocks
                                //Otherwise, add new blocks

                                if (punishQueue.Count > 0)
                                {
                                    BlockColor[] data = punishQueue.Dequeue();
                                    //Clear out punish positions, and add all positions in
                                    punishPositions.Clear();
                                    for (int i = 0; i < board.Width; i++)
                                        punishPositions.Add(i);

                                    for (int i = 0; i < data.Length; i++)
                                    {
                                        int index = punishRandom.Next(0, punishPositions.Count);
                                        int x = punishPositions[index];
                                        punishPositions.RemoveAt(index);

                                        Block block = new Block(this, data[i], BlockType.Atom);
                                        block.Move(new Point(x, board.Height - 1));
                                        block.PunishBlock = true;
                                        this.Add(block);
                                    }
                                }
                                else
                                {
                                    //If we have something to currently punish them with, lets take care of that
                                    if (currentPunish.Count > 1)
                                    {
                                        //punishColors = currentPunish.ToArray();
                                        //Add the current punish to the ready punish queue
                                        for (int i = 0; i < currentPunish.Count; i++)
                                        {
                                            if(readyPunish.Count < this.GlobalSettings.MaxStorePunish)
                                            readyPunish.Add(currentPunish[i]);
                                        }
                                        
                                        currentPunish.Clear();
                                    }


                                    SpawnNewBlock();
                                }

                                nextDrop = 0.0;
                            }
                            else
                            {
                                nextDrop = time + nextDropDelay;
                            }

                        }
                    }

                }



                CheckBoardForLines(gameTime);
            }
        }

        /// <summary>
        /// Returns true if we are victorious, false otherwise
        /// </summary>
        /// <returns></returns>
        public virtual bool CheckVictoryCondition()
        {
            //Check for victory/loss conditions
            if (isotopes <= 0)
                return true;

            return false;
        }

        /// <summary>
        /// Returns true if we lost, false otherwise
        /// </summary>
        /// <returns></returns>
        public virtual bool CheckLossCondition()
        {
            if (currentAtomCluster != null)
            {
                if (!currentAtomCluster.Valid)
                {
                    return true;
                    //state = QuarxGameState.Lost;
                    //currentAtomCluster = null;
                    //currentAtomCluster = null;
                }
            }
            return false;
        }

        public virtual void HandleInput(GameTime gameTime)
        {
       
        }

        public virtual AtomCluster GetNewAtomCluster(Point position)
        {
            BlockColor color1 = (BlockColor)random.Next(0, 3);
            BlockColor color2 = (BlockColor)random.Next(0, 3);
           return  new AtomCluster(this, color1, color2, position);

        }

        public virtual void SpawnNewBlock()
        {
            if (state == QuarxGameState.Lost)
                return;

            //if (scoreMultiplier > 1)
            //    PlaySound("combo");

            scoreMultiplier = 1;

            int x = board.Width / 2 - 1;
            int y = board.Height - 1;


            Point position = new Point(board.Width / 2 - 1, board.Height - 1);

            if (nextAtomCluster != null)
                currentAtomCluster = nextAtomCluster;
            else
                currentAtomCluster = GetNewAtomCluster(position);

            nextAtomCluster = GetNewAtomCluster(position);



            //Is either position1 or position2 occupied??
            //if (board[currentAtomCluster.Position1] != null
            //    || board[currentAtomCluster.Position2] != null)
            //{


            //TODO: Add victory condition back in

            //if (ac.Valid)
            //    this.Add(ac);
            //else
            //    state = QuarxGameState.Lost;
        }

        /// <summary>
        /// Loop through the gameboard and count the number of isotopes left
        /// </summary>
        /// <returns></returns>
        public int CountIsotopes()
        {
            int count = 0;
            for (int x = 0; x < board.Width; x++)
            {
                for (int y = 0; y < board.Height; y++)
                {
                    Block b = board[x, y];
                    if (b != null)
                    {
                        if (b.Type == BlockType.Isotope)
                            count++;
                    }
                }
            }
            return count;
        }

        /// <summary>
        /// Loop through board, check for completed lines
        /// Returns number of lines found
        /// </summary>
        public int CheckBoardForLines(GameTime time)
        {
            for (int x = 0; x < board.Width; x++)
            {
                for (int y = 0; y < board.Height; y++)
                {
                    Block block = board[x, y];
                    if (block != null)
                        block.NearbyBlocks = 0;
                }
            }

            int numLines = 0;
            for (int x = 0; x < board.Width; x++)
            {
                for (int y = 0; y < board.Height; y++)
                {
                    Block block = board[x, y];
                    

                    if (block != null)
                    {
                        
                        numLines += ExamineBlock(block, time);
                    }
                }
            }
            return numLines;
        }

        public int ExamineBlock(Block block, GameTime gameTime)
        {
            //Loop through each direction
            //Note : we only have to check right and down,
            //because of the way this function is called

            BlockColor color = block.Color;

            Point startPoint = block.Position;

            int lines = 0;
            lines += DoCheck(block, startPoint, new Point(0, 1), color, gameTime);
            lines += DoCheck(block, startPoint, new Point(1, 0), color, gameTime);

            return lines;
        }

        /// <summary>
        /// Toggles the punish target
        /// </summary>
        public void IncrementPunishTarget()
        {
            if (this.punishModels.Count > 0)
            {
                this.PlaySound("toggle_punish");

                //Now, increment the punishIndex
                this.currentPunishIndex++;

                this.currentPunishIndex = this.currentPunishIndex % this.punishModels.Count;
            }

        }

        private void CleanupPunish()
        {
            this.removePunishModels.Clear();
            //First, go through and prune out any punish models that have lost
            for (int i = 0; i < this.punishModels.Count; i++)
            {
                if (punishModels[i].State != QuarxGameState.Playing)
                    this.removePunishModels.Add(punishModels[i]);
            }

            for (int i = 0; i < this.removePunishModels.Count; i++)
                this.punishModels.Remove(this.removePunishModels[i]);
        }

        public int GetMaxHeight()
        {
            for (int y = board.Height - 1; y>=0 ; y--)
            {
                for (int x = 0; x < board.Width; x++)
                {
                    if (board[x, y] != null)
                        return y;
                }
            }

            return 0;

        }

        public int DoCheck(Block inBlock, Point startPoint, Point delta, BlockColor color, GameTime gameTime)
        {
            BlockColor nextColor = color;
            int x = 0;
            while (nextColor == color)
            {
                
                x++;
                Point nextPoint = new Point(startPoint.X + x * delta.X, startPoint.Y + x * delta.Y);

                if (!board.IsInBounds(nextPoint))
                    break;

                Block block = board[nextPoint];
                if (block == null)
                    break;
                else
                    nextColor = block.Color;

                if (nextColor == color)
                {
                    inBlock.NearbyBlocks++;
                    block.NearbyBlocks++;
                }
            }

            if (x >= 4)
            {
                int isotopes = 0;
                bool playSound = false;
                //Remove stuff
                for (int r = 0; r < x; r++)
                {
                    Point p = new Point(startPoint.X + r * delta.X, startPoint.Y + r * delta.Y);
                    Block block = board[p];
                    if (!block.IsRemoving)
                    {
                        if (block.Type == BlockType.Isotope)
                            isotopes++;

                        block.StartRemove(gameTime);
                        if (!playSound)
                        {
                            
                            playSound = true;
                        }
                    }
                }

                if (playSound)
                {
                    //Clear out old punish if score multiplier == 0
                    if (scoreMultiplier == 1)
                        currentPunish.Clear();

                    int sound = (int)MathHelper.Clamp(scoreMultiplier, 1, 5);
                    PlaySound("atom_dissolve" + sound.ToString());

                    //if (scoreMultiplier == 1)
                    //    PlaySound("atom_dissolve");
                    //else
                    //    PlaySound("combo");

                    score += (isotopes * 20) * scoreMultiplier;
                    scoreMultiplier++;
                    currentPunish.Add(nextColor);
                }


                return 1;
            }
            return 0;
        }

        public void RemoveAllBlocks(GameTime gameTime)
        {
            for (int x = 0; x < board.Width; x++)
            {
                for (int y = 0; y < board.Height; y++)
                {
                    if (board[x, y] != null)
                        board[x, y].StartRemove(gameTime);
                }
            }
        }


        /// <summary>
        /// Add random ness to a game board.
        /// Max height is the maximum height of pieces
        /// </summary>
        public virtual void Start(int maxHeight, int numberOfIsotopes, int numberOfAtoms, int seed)
        {
            List<Point> positionList = new List<Point>(Board.Width * maxHeight);
            for (int x = 0; x < Board.Width; x++)
            {
                for (int y = 0; y < maxHeight; y++)
                {
                    positionList.Add(new Point(x, y));
                }
            }

            random = new Random(seed);

            for (int i = 0; i < numberOfIsotopes + numberOfAtoms; i++)
            {
                int index = random.Next(0, positionList.Count - 1);
                Point position = positionList[index];
                positionList.RemoveAt(index);

                if (i < numberOfIsotopes)
                {
                    AddIsotope(GetRandomColor(random), position);
                }
                else
                {
                    AddBlock(GetRandomColor(random), position);
                }
            }

            VerifyBoard(random);
        }

        public void AddIsotope(BlockColor color, Point position)
        {
            Block block = new Block(this, color, position);
            this.Add(block);
        }

        public void AddBlock(BlockColor color, Point position)
        {
            Block atom = new Block(this, color, BlockType.Atom);
            atom.Move(position);
            this.Add(atom);

        }

        /// <summary>
        /// This function goes through and makes sure we don't have any 3 in a rows. If we don't have any 3 in a rows,
        /// we definitely can't have any 4 in a rows
        /// </summary>
        private void VerifyBoard(Random random)
        {
            
            for (int y = 0; y < board.Height; y++)
            {
                BlockColor lastColor = BlockColor.Null;
                int currentCount = 0;
                for (int x = 0; x < board.Width; x++)
                {
                    DoCheck(ref lastColor, ref currentCount, x, y);
                }
            }

            for (int x = 0; x < board.Width; x++)
            {
                BlockColor lastColor = BlockColor.Null;
                int currentCount = 0;
                for (int y = 0; y < board.Height; y++)
                {
                    DoCheck(ref lastColor, ref currentCount, x, y);
                }
            }
        }

        private void DoCheck(ref BlockColor lastColor, ref int currentCount, int x, int y)
        {
            Block b = board[x, y];
            if (b != null)
            {
                if (b.Color != lastColor)
                {
                    currentCount = 1;
                    lastColor = b.Color;
                }
                else
                    currentCount++;

                if (currentCount >= 3)
                {
                    if (b.Color == BlockColor.Red)
                        b.Color = BlockColor.Blue;
                    else if (b.Color == BlockColor.Blue)
                        b.Color = BlockColor.Yellow;
                    else if (b.Color == BlockColor.Yellow)
                        b.Color = BlockColor.Red;
                    //b.Color = BlockColor.Null;

                    currentCount = 0;

                }
            }
            else
            {
                lastColor = BlockColor.Null;
                currentCount = 0;
            }
        }

        private BlockColor GetRandomColor(Random random)
        {
            return (BlockColor)random.Next(0, 3);
        }

    }
}
