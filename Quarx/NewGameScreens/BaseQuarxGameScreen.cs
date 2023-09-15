using System;
using System.Collections.Generic;
using System.Text;

using Sxe.Engine;
using Sxe.Engine.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;

namespace Quarx
{

    public class BaseQuarxGameScreen : BaseScreen
    {
        BaseGameModel [] models;
        QuarxPlayerInfo[] playerInfo;
        List<QuarxPlayerInfo> playerRankings = new List<QuarxPlayerInfo>();
        RoundSettings settings;
        int clickPlayerIndex = 0;
        int maxRounds = 0;
        int numPlayers = 1;

        List<string> quarxFunFax = new List<string>();
        List<string> quarxFunFaxOptions = new List<string>() { "Buy Now", "Cancel" };

        const double funFaxDelay = 240.0;
        double startTime = -1.0;
        bool showedBuyBox = false;

        const double FullSpeedTime = 330; //8 minutes to get to "full speed", which is 0.5 of the start speed
        double speedStartTime = 0.0;

        public BaseGameModel[] Models
        {
            get { return models; }
        }

        public QuarxPlayerInfo[] Info
        {
            get { return playerInfo; }
        }

        public RoundSettings Settings
        {
            get { return settings; }
            set { settings = value; }
        }

        public int MaxRounds
        {
            get { return maxRounds; }
        }

        public int ClickPlayerIndex
        {
            get { return clickPlayerIndex; }
            set { clickPlayerIndex = value; }
        }

        public BaseQuarxGameScreen()
        {
            InitializeComponent();

            //Add some quarx fun fax!! 
            string funFaxStart = "Did you know...\n\n";
            string funFaxEnd = "\n\nQuarx Fun Fax are only available in the trial mode. Buy Quarx now and never see a Fun Fax again!";

            quarxFunFax.Add(funFaxStart + "Nine out of ten mythical creatures give Quarx two-thumbs up!" + funFaxEnd);
            quarxFunFax.Add(funFaxStart + "You shouldn't play with matches, or nunchucks, unless you've had years of training." + funFaxEnd);
            quarxFunFax.Add(funFaxStart + "Quarx was made with lightening. REAL LIGHTENING!" + funFaxEnd);
            quarxFunFax.Add(funFaxStart + "99% of unicorns said that playing Quarx was more fun than playing leapfrog." + funFaxEnd);
            quarxFunFax.Add(funFaxStart + "Chinchillas are a lot like Quarx. Soft, furry, cute, and a fun way to punish your friends." + funFaxEnd);
            quarxFunFax.Add(funFaxStart + "The main colors used in Quarx are red, blue, and yellow." + funFaxEnd);
            quarxFunFax.Add(funFaxStart + "Impress your friends! Intimidate your enemies! String together combos to become the true 'Quarx Master'" + funFaxEnd);
            quarxFunFax.Add(funFaxStart + "The music in Quarx is pretty sweet. Seriously." + funFaxEnd);
            quarxFunFax.Add(funFaxStart + "Ninjas can beat anyone at Quarx, except dinosaurs and robots." + funFaxEnd);
            quarxFunFax.Add(funFaxStart + "I can't beat the computer on difficult. Also, I don't have a girlfriend. I'm not saying these are related." + funFaxEnd);
            quarxFunFax.Add(funFaxStart + "Quarx contains no high fructose corn syrup, no MSG, and no artificial flavors!" + funFaxEnd);
            quarxFunFax.Add(funFaxStart + "No animals were harmed in the making of Quarx." + funFaxEnd);

        }

        bool showingStart = false;


        public override void Activate()
        {
            //Audio.PushMusic("ChillMusic");
            base.Activate();
        }

        public override void Deactivate()
        {
            //Audio.PopMusic();
            AnarchyGamer.ClearAIGamers();
            base.Deactivate();
        }

        /// <summary>
        /// This should be called in the constructor of the derived class
        /// Sets up the models and information for each player
        /// </summary>
        public void InitializeGame(int players)
        {
            numPlayers = players;
            models = new BaseGameModel[numPlayers];
            playerInfo = new QuarxPlayerInfo[numPlayers];
            for (int i = 0; i < numPlayers; i++)
            {
                //models[i] = new QuarxGameModel(8, 18, GameScreenService.Input.Controllers[0]);
                playerInfo[i] = new QuarxPlayerInfo();
            }

            if (numPlayers <= 1)
            {
                maxRounds = 1;
                //GameScreenService.AddScreen(new SingleplayerWaitScreen());
            }
            else
            {
                maxRounds = 3;
                //GameScreenService.AddScreen(new TwoPlayerWaitScreen());
            }
        }

        public virtual bool ShouldStartNewRound()
        {
            for (int i = 0; i < this.Info.Length; i++)
            {
                if (Info[i].RoundsWon >= this.Settings.GlobalSettings.NumberOfRounds)
                    return false;
            }

            return true;
        }

        public virtual void OnExit()
        {
        }

        public virtual void StartGame()
        {
            //First, check if a player has won already
            //for (int i = 0; i < playerInfo.Length; i++)
            //{
            //    if (playerInfo[i].RoundsWon >= maxRounds)
            //        this.ExitScreen();
            //}
            if (!ShouldStartNewRound())
            {
                this.ExitScreen();
                this.OnExit();
            }
            else
            {
                speedStartTime = 0.0;

                Random r = new Random();
                int seed = r.Next();

                for (int i = 0; i < models.Length; i++)
                {


                    //int controllerIndex = Math.Min(i, AnarchyGamer.Gamers.Count-1);
                    //AnarchyGamer gamer = AnarchyGamer.Gamers[controllerIndex] as AnarchyGamer;

                    //if (models.Length == 1)
                    //    gamer = Input.Controllers[clickPlayerIndex].Gamer;



                    Models[i] = GetGameModel(Settings.GamerSettings[i].Gamer);


                    AI.BaseAIModel aiModel = Models[i] as AI.BaseAIModel;
                    if (aiModel != null)
                    {
                        aiModel.Difficulty = Settings.GamerSettings[i].Option1;
                        aiModel.Seed = r.Next();
                    }

                    //AI.SimpleAIModel m = new Quarx.AI.SimpleAIModel(

                    QuarxGameSettings gameSettings = Settings.GamerSettings[i].Settings;

                    Models[i].GlobalSettings = Settings.GlobalSettings;
                    Models[i].Start(gameSettings.MaxHeight, gameSettings.Isotopes, gameSettings.Blocks, seed);
                    Models[i].FallDelay = gameSettings.FallSpeed;
                    Models[i].State = QuarxGameState.Waiting;

                    //AtomCluster cluster = new AtomCluster(Models[i], BlockColor.Red, BlockColor.Blue, new Point(2, 16));
                    //Models[i].Add(cluster);

                    Models[i].State = QuarxGameState.Playing;
                }

                //Make sure all models know who to punish!
                for (int i = 0; i < models.Length; i++)
                {
                    for (int j = 0; j < models.Length; j++)
                    {
                        if (i != j)
                            models[i].PunishModels.Add(models[j]);
                    }
                }

                showingStart = false;
                //this.label1.Visible = false;
            }
        }

        public void SetScoreBoardPunishTarget(BaseGameModel model, ScoreBoard scoreBoard)
        {
            if (model.PunishTarget == null)
                scoreBoard.PunishName = string.Empty;
            else if (model.PunishTarget.Gamer == null)
                scoreBoard.PunishName = string.Empty;
            else
                scoreBoard.PunishName = model.PunishTarget.Gamer.GamerTag;

            //this.scoreBoard1.PunishName = this.Models[0].PunishTarget.Gamer.GamerTag;
        }

        public virtual BaseGameModel GetGameModel(IAnarchyGamer gamer)
        {
            BaseGameModel model;
            if (gamer.IsPlayer)
                model = new PlayerGameModel(8, 18, gamer);
            else
            {
                AI.SimpleAIModel aiModel;
                aiModel = new AI.SimpleAIModel(8, 18);
                aiModel.Gamer = gamer;
                model = aiModel;
            }
            return model;
        }

        public virtual void OnRoundOver()
        {
            //Add scores to player's total score
            for (int i = 0; i < Info.Length; i++)
            {
                if(Models[i] != null)
                Info[i].TotalScore += Models[i].Score;
            }

            //If we have more than one model, lets do the rankings
            if (Info.Length > 1)
            {
                playerRankings.Clear();
                for (int i = 0; i < Info.Length; i++)
                    playerRankings.Add(Info[i]);

                playerRankings.Sort();
                playerRankings.Reverse();

                //If the top player has one 3 rounds, then lets show the marquees
                if (playerRankings[0].RoundsWon >= this.Settings.GlobalSettings.NumberOfRounds)
                {
                    for (int i = 0; i < this.playerRankings.Count; i++)
                    {
                        this.playerRankings[i].Marquee = (QuarxMarqueeType)(i+1);
                    }

                    for (int i = 0; i < Info.Length; i++)
                        this.Models[i].Marquee = this.Info[i].Marquee;
                }
            }

        }

        public void OnDialogCallback(object sender, XboxMessageEventArgs args)
        { 
            bool returnToMainMenu = true;
            if (args.Result.HasValue)
            {
                if (args.Result.Value == 0)
                {


                    //Get the gamer at this index
                    IAnarchyGamer gamer = AnarchyGamer.Gamers.GetGamerByPlayerIndex((int)args.PlayerIndex);

                    if (gamer != null)
                    {
                        this.GameScreenService.XboxMessage.ShowMarketPlace((PlayerIndex)gamer.Index);
                        returnToMainMenu = false;
                    }
                }
            }

            if (returnToMainMenu)
                this.ExitScreen();
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            //Only let input if we have focus

            if (Guide.IsTrialMode && !showedBuyBox)
            {
                if (startTime < 0)
                    startTime = gameTime.TotalRealTime.TotalSeconds;
                //nextFunFax += gameTime.ElapsedRealTime.TotalSeconds;
                if (gameTime.TotalRealTime.TotalSeconds - startTime > funFaxDelay)
                {
                    //nextFunFax = 0f;
                    showedBuyBox = true;

                    XboxMessageInfo xmi = new XboxMessageInfo(this.quarxFunFaxOptions);
                    xmi.MessageBoxIcon = MessageBoxIcon.Alert;
                    xmi.Message = "The trial mode for Quarx has a limit of 4 minutes of gameplay. Purchase the full version for unrestricted play!\n\n Would you like to buy the full version?";
                    xmi.PlayerIndex = (PlayerIndex)this.models[0].Gamer.Index;
                    xmi.Title = "Trial Time Limit Reached";
                    xmi.Completed += this.OnDialogCallback;

                    this.GameScreenService.XboxMessage.Add(xmi);
                        this.ExitScreen();
                }
            }


            if (showingStart)
            {
                if (Input.Controllers.IsKeyJustPressed("pause"))
                {
                    StartGame();   
                }

                for (int i = 0; i < models.Length; i++)
                {
                    models[i].Update(gameTime);
                }

            }
           
            
            else if (!otherScreenHasFocus)
            {

                speedStartTime += gameTime.ElapsedGameTime.TotalSeconds;
                
                for (int i = 0; i < models.Length; i++)
                {
                    //First, check if any of the models have invalid gamers not signed in
                    if (!models[i].Gamer.IsSignedIn)
                        this.ExitScreen();

                    if (models[i].Controller != null)
                    {
                        //Check if any players hit the pause button
                        if (models[i].Controller.IsKeyJustPressed("pause"))
                        {
                            PauseScreen pauseScreen = new PauseScreen();
                            pauseScreen.Controller = models[i].Controller;
                            pauseScreen.GameScreen = this;
                            this.GameScreenService.AddScreen(pauseScreen);
                            break;
                        }
                    }
                }

                for (int i = 0; i < models.Length; i++)
                {
                    models[i].Update(gameTime);

                    double val = speedStartTime / FullSpeedTime;
                    if (val >= 1.0) val = 1.0;

                    models[i].FallDelayModifier = val;
                    //Set the current speed thing
                }

                //Lets check and see if everyone is finished
                int wonCount = 0;
                int lostCount = 0;
                for (int i = 0; i < models.Length; i++)
                {
                    if (models[i].State == QuarxGameState.Won)
                        wonCount++;
                    else if (models[i].State == QuarxGameState.Lost)
                        lostCount++;
                }

                //We have a winner
                //if (wonCount > 0 || (lostCount >= models.Length - 1 && models.Length > 1))
                if (wonCount > 0 || (lostCount >= models.Length - 1 && models.Length > 1) || (lostCount > 0 && models.Length == 1))
                {
                    //First, if someone won, need to make everyone else lose
                    if (wonCount > 0)
                    {
                        for (int i = 0; i < models.Length; i++)
                        {
                            if (models[i].State != QuarxGameState.Won)
                                models[i].State = QuarxGameState.Lost;
                        }
                    }

                    for (int i = 0; i < models.Length; i++)
                    {
                        //If we are the only person left, and everyone else lost, we won
                        if (lostCount > 0 && models[i].State != QuarxGameState.Lost)
                        {
                            models[i].State = QuarxGameState.Won;
                        }

                        //Decide what sound to play
                        if (models[i].State == QuarxGameState.Won)
                        {
                            playerInfo[i].RoundsWon++;
                            this.GameScreenService.Audio.PlayCue("victory");
                        }
                        else
                        {
                            models[i].State = QuarxGameState.Lost;
                            this.GameScreenService.Audio.PlayCue("defeat");
                        }

                        //Show the flashing start
                        models[i].ShowStart = true;

                        //RE-ENABLE to destroy all blocks
                        //models[i].RemoveAllBlocks(gameTime);
                    }

                    showingStart = true;
                    //label1.Visible = true;
                    OnRoundOver();


                }

               

            }

            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }

        private void InitializeComponent()
        {
            // 
            // BaseQuarxGameScreen
            // 
            this.BackgroundPath = "background";

        }


    }



    public class QuarxPlayerInfo : IComparable<QuarxPlayerInfo>
    {
        int roundsWon = 0;
        int totalScore = 0;

        public int RoundsWon
        {
            get { return roundsWon; }
            set { roundsWon = value; }
        }

        public int TotalScore
        {
            get { return totalScore; }
            set { totalScore = value; }
        }


        public QuarxMarqueeType Marquee
        {
            get;
            set;
        }

        public int CompareTo(QuarxPlayerInfo otherPlayer)
        {
            if (this.roundsWon != otherPlayer.RoundsWon)
                return this.roundsWon - otherPlayer.RoundsWon;
            else if (this.totalScore != otherPlayer.TotalScore)
                return this.totalScore - otherPlayer.TotalScore;
            else
                return 0;
        }

    }
}
