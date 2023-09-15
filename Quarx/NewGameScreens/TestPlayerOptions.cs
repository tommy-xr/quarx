using System;
using System.Collections.Generic;
using System.Text;

using Sxe.Engine.UI;
using Sxe.Engine;

using Quarx.NewPanels;

using Microsoft.Xna.Framework.Graphics;
using Sxe.Engine.Input;
namespace Quarx
{
    public class TestPlayerOptions : MessageScreen
    {
        private Quarx.NewPanels.SwitchOptions switchOptions1;
        private Quarx.NewPanels.SwitchOptions switchOptions2;
        private Quarx.NewPanels.SwitchOptions switchOptions3;
        private Quarx.NewPanels.SwitchOptions switchOptions4;
        private Sxe.Engine.UI.Menu.MenuGroup menuGroup1;
        private List<IAnarchyGamer> computerGamers = new List<IAnarchyGamer>();

        MenuEventArgs dummyMenuEvent = new MenuEventArgs();

        public GlobalSettings GlobalSettings
        {
            get;
            set;
        }


        int singlePlayerIndex = -1;
        private SignInModule signInModule1;
        private Panel panel1;
    
        public int SinglePlayerIndex
        {
            get { return singlePlayerIndex; }
            set { singlePlayerIndex = value; }
        }

        SwitchOptions[] options;
    
        public TestPlayerOptions()
        {
            InitializeComponent();

            options = new SwitchOptions[4];
            options[0] = switchOptions1;
            options[1] = switchOptions2;
            options[2] = switchOptions3;
            options[3] = switchOptions4;


        }

        public override void Activate()
        {

            if (singlePlayerIndex == -1)
            {
                AnarchyGamer.SignedIn += this.OnGamerSignedIn;
                AnarchyGamer.SignedOut += this.OnGamerSignedOut;
            }

            Setup();

            base.Activate();



            //playerOptions2.Enabled = false;
            //playerOptions3.Enabled = false;
            //playerOptions4.Enabled = false;
        }

        //Default setup is not on sign out
        void Setup()
        {
            Setup(false);
        }

        void Setup(bool onSignOut)
        {
            if (singlePlayerIndex != -1)
            {
                options[0].PlayerSelectionPanelVisible = false;

                SetGamer(0, AnarchyGamer.Gamers.GetGamerByPlayerIndex(singlePlayerIndex));
                for (int i = 1; i <= 3; i++)
                    options[i].Visible = false;
              
            }
            else
            {
                //If we joined multiplayer, but we are by ourself... add a friend
                if (AnarchyGamer.Gamers.Count <= 1 && !onSignOut)
                    this.AddAI();

                //First we have to shuffle the AI gamers to back, if present
                computerGamers.Clear();
                for (int i = 0; i < AnarchyGamer.Gamers.Count; i++)
                {
                    if (AnarchyGamer.Gamers[i] is BaseAIGamer)
                    {
                        computerGamers.Add(AnarchyGamer.Gamers[i]);
                    }
                }

                for (int i = 0; i < computerGamers.Count; i++)
                {
                    //AnarchyGamer.Gamers.Remove(computerGamers[i]);
                    //AnarchyGamer.Gamers.Add(computerGamers[i]);
                    AnarchyGamer.Gamers.MoveToEnd(computerGamers[i]);
                }

                //Now, we have to remove extra AI gamers, in case there were maxed out AI gamers and someone signed
                while (AnarchyGamer.Gamers.Count > 4)
                {
                    IAnarchyGamer gamer = AnarchyGamer.Gamers[AnarchyGamer.Gamers.Count - 1];
                    AnarchyGamer.Gamers.Remove(gamer);
                }


                //Set the first switch options to anarch players
                for (int i = 0; i < AnarchyGamer.Gamers.Count; i++)
                {
                    if (i <= 3)
                    {
                        SetGamer(i, AnarchyGamer.Gamers[i]);
                        //Set mode to player
                    }
                }

                //Finally, set the remainder to null
                for (int i = AnarchyGamer.Gamers.Count; i < 4; i++)
                {
                    SetGamer(i, null);

                }

                //Loop through gamers that are players and stuff
                //If they are highlighting a cell that is not valid, use a dummy event to move them up
                //for (int i = 0; i < AnarchyGamer.Gamers.Count; i++)
                //{
                //    IAnarchyGamer gamer = AnarchyGamer.Gamers[i];
                //    if (gamer != null)
                //    {
                //        if (gamer.IsPlayer)
                //        {
                //            //Now, lets get the object the player is selecting
                //            SwitchOptions so = this.menuGroup1.GetSelected(gamer.Index) as SwitchOptions;

                            

                //            //If the gamer for so is null now, lets send a fake move up event
                //            if (so != null && so.Gamer == null)
                //            {
                //                this.dummyMenuEvent.MenuEventType = MenuEventType.Up;
                //                this.dummyMenuEvent.IsSignedIn = true;
                //                this.dummyMenuEvent.PlayerIndex = gamer.Index;
                //                this.menuGroup1.HandleEvent(this.dummyMenuEvent);
                //            }
                //        }
                //    }
                //}

                //Finally, we set all the options ready to null
                for (int i = 0; i < options.Length; i++)
                    options[i].Ready = false;
            }

            
            //So apparently we do need this - puts all the people in the right places, otherwise everyone starts at first option
            menuGroup1.Activate();
        }

        void SetGamer(int optionsIndex, IAnarchyGamer gamer)
        {
            options[optionsIndex].Gamer = gamer;
            if (gamer != null)
            {


                if (options[optionsIndex].Gamer.IsPlayer)
                    options[optionsIndex].AllowedPlayerIndex = options[optionsIndex].Gamer.Index;
                else
                    options[optionsIndex].AllowedPlayerIndex = -1;
            }
            
        }

        void OnGamerSignedIn(object sender, AnarchySignedInEventArgs args)
        {
            Setup(false);
        }

        void OnGamerSignedOut(object sender, AnarchySignedOutEventArgs args)
        {
            Setup(true);
        }

        public void Reset()
        {
            for (int i = 0; i < options.Length; i++)
            {
                if (options[i].Gamer != null)
                    options[i].Ready = false;
            }

        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {

            //Check if all the panels that have people are ready
            bool ready = true;
            int numPlayers = 0;
            for (int i = 0; i < options.Length; i++)
            {
                if (options[i].Gamer != null && !options[i].Ready)
                {
                    ready = false;
                    
                }
                else if(options[i].Gamer != null)
                    numPlayers++;
            }

            //If this is single player, we'll wrap up some default options
            //If we want to re-enable letting players set their own music in singleplayer,
            //we need to take this first if out
            if (ready && numPlayers == 1)
            {
                GlobalSettings = new GlobalSettings();
                Random random = new Random();
                if (random.NextDouble() < 0.5)
                    GlobalSettings.MusicType = "Toped";
                else
                    GlobalSettings.MusicType = "Alienz";

            }
            else if (ready && GlobalSettings == null && !otherScreenHasFocus && !IsExiting)
            {
                MusicSelect select = new MusicSelect();
                select.OptionsScreen = this;

                if (numPlayers == 1)
                    select.SinglePlayerOnly = true;
                else
                    select.SinglePlayerOnly = false;

                this.GameScreenService.AddScreen(select);
               
            }
            
            
            if (ready && GlobalSettings != null)
            {
                //Package up settings
                RoundSettings settings = new RoundSettings();
                settings.GlobalSettings = this.GlobalSettings;

                settings.GamerSettings.Clear();

                for (int i = 0; i < numPlayers; i++)
                {
                    GamerSettings gamerSettings = new GamerSettings();
                    gamerSettings.Gamer = options[i].Gamer;
                    gamerSettings.Option1 = options[i].Option1;
                    gamerSettings.Option2 = (options[i].Option2+1) * 3;

                    if (gamerSettings.Gamer.IsPlayer)
                    {
                        double speed = 1.2f - (0.2f * gamerSettings.Option1);
                        gamerSettings.Settings = new QuarxGameSettings(speed, gamerSettings.Option2, QuarxDifficulty.Multiplayer);
                    }
                    else
                    {
                        gamerSettings.Settings = new QuarxGameSettings(0.4, gamerSettings.Option2, QuarxDifficulty.Multiplayer);
                    }

                    settings.GamerSettings.Add(gamerSettings);


                }
                BaseQuarxGameScreen nextScreen;

                if (numPlayers == 1)
                    nextScreen = new SinglePlayerGame();
                else if (numPlayers == 2)
                    nextScreen = new TwoPlayerGame();
                else if (numPlayers == 3)
                    nextScreen = new ThreePlayerGame();
                else if (numPlayers == 4)
                    nextScreen = new FourPlayerGame();
                else
                    throw new Exception("Invalid number of gamers");
                nextScreen.Settings = settings;

                if (settings.GlobalSettings.MusicType == "None")
                    nextScreen.MusicCue = "none";
                else
                    nextScreen.MusicCue = settings.GlobalSettings.MusicType;

                if (!IsExiting)
                    GameScreenService.AddScreen(nextScreen);
                this.ExitScreen();


                //Pick the right game screen based on number of players

                //Transition to that game screen
            }
            else if (AnarchyGamer.Gamers.IsKeyJustPressed("x_button") && singlePlayerIndex == -1 && !otherScreenHasFocus)
            {
                if (AnarchyGamer.Gamers.Count < 4)
                {
                    AddAI();
                    Setup();
                }
            }
            

            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }

        private void AddAI()
        {
            GameScreenService.AnarchyGamers.AddAI(new Quarx.AI.QuarxAIGamer());

        }

        void InitializeComponent()
        {
            this.menuGroup1 = new Sxe.Engine.UI.Menu.MenuGroup();
            this.switchOptions1 = new Quarx.NewPanels.SwitchOptions();
            this.switchOptions2 = new Quarx.NewPanels.SwitchOptions();
            this.switchOptions3 = new Quarx.NewPanels.SwitchOptions();
            this.switchOptions4 = new Quarx.NewPanels.SwitchOptions();
            this.panel1 = new Sxe.Engine.UI.Panel();
            this.signInModule1 = new Sxe.Engine.SignInModule();
            // 
            // menuGroup1
            // 
            this.menuGroup1.AllowMultiSelect = true;
            this.menuGroup1.BackgroundPath = "gamesetup";
            this.menuGroup1.CanDrag = false;
            this.menuGroup1.ControlType = Sxe.Engine.UI.Menu.MenuControlType.Vertical;
            this.menuGroup1.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowSignedIn;
            this.menuGroup1.Location = new Microsoft.Xna.Framework.Point(82, 46);
            this.menuGroup1.Panels.Add(this.switchOptions1);
            this.menuGroup1.Panels.Add(this.switchOptions2);
            this.menuGroup1.Panels.Add(this.switchOptions3);
            this.menuGroup1.Panels.Add(this.switchOptions4);
            this.menuGroup1.Panels.Add(this.panel1);
            this.menuGroup1.Parent = this;
            this.menuGroup1.Size = new Microsoft.Xna.Framework.Point(592, 486);
            this.menuGroup1.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.menuGroup1.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.menuGroup1.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.menuGroup1.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.menuGroup1.MenuCancel += new System.EventHandler<ButtonPressEventArgs>(this.menuGroup1_MenuCancel);
            // 
            // switchOptions1
            // 
            this.switchOptions1.AllowedPlayerIndex = -1;
            this.switchOptions1.BackgroundPath = "Options\\Empty Slot";
            this.switchOptions1.CanDrag = false;
            this.switchOptions1.Gamer = null;
            this.switchOptions1.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowSignedIn;
            this.switchOptions1.Location = new Microsoft.Xna.Framework.Point(28, 74);
            this.switchOptions1.Parent = this.menuGroup1;
            this.switchOptions1.Ready = false;
            this.switchOptions1.Size = new Microsoft.Xna.Framework.Point(500, 75);
            this.switchOptions1.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.switchOptions1.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.switchOptions1.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.switchOptions1.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            // 
            // switchOptions2
            // 
            this.switchOptions2.AllowedPlayerIndex = -1;
            this.switchOptions2.BackgroundPath = "Options\\Empty Slot";
            this.switchOptions2.CanDrag = false;
            this.switchOptions2.Gamer = null;
            this.switchOptions2.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowSignedIn;
            this.switchOptions2.Location = new Microsoft.Xna.Framework.Point(30, 162);
            this.switchOptions2.Parent = this.menuGroup1;
            this.switchOptions2.Ready = false;
            this.switchOptions2.Size = new Microsoft.Xna.Framework.Point(500, 75);
            this.switchOptions2.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.switchOptions2.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.switchOptions2.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.switchOptions2.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            // 
            // switchOptions3
            // 
            this.switchOptions3.AllowedPlayerIndex = -1;
            this.switchOptions3.BackgroundPath = "Options\\Empty Slot";
            this.switchOptions3.CanDrag = false;
            this.switchOptions3.Gamer = null;
            this.switchOptions3.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowSignedIn;
            this.switchOptions3.Location = new Microsoft.Xna.Framework.Point(33, 251);
            this.switchOptions3.Parent = this.menuGroup1;
            this.switchOptions3.Ready = false;
            this.switchOptions3.Size = new Microsoft.Xna.Framework.Point(500, 75);
            this.switchOptions3.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.switchOptions3.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.switchOptions3.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.switchOptions3.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            // 
            // switchOptions4
            // 
            this.switchOptions4.AllowedPlayerIndex = -1;
            this.switchOptions4.BackgroundPath = "Options\\Empty Slot";
            this.switchOptions4.CanDrag = false;
            this.switchOptions4.Gamer = null;
            this.switchOptions4.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowSignedIn;
            this.switchOptions4.Location = new Microsoft.Xna.Framework.Point(32, 345);
            this.switchOptions4.Parent = this.menuGroup1;
            this.switchOptions4.Ready = false;
            this.switchOptions4.Size = new Microsoft.Xna.Framework.Point(500, 75);
            this.switchOptions4.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.switchOptions4.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.switchOptions4.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.switchOptions4.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            // 
            // panel1
            // 
            this.panel1.BackgroundPath = "optionsbuttons";
            this.panel1.CanDrag = false;
            this.panel1.Location = new Microsoft.Xna.Framework.Point(238, 454);
            this.panel1.Parent = this.menuGroup1;
            this.panel1.Size = new Microsoft.Xna.Framework.Point(115, 22);
            this.panel1.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.panel1.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            // 
            // signInModule1
            // 
            this.signInModule1.BackgroundPath = null;
            this.signInModule1.CanDrag = false;
            this.signInModule1.Location = new Microsoft.Xna.Framework.Point(20, 542);
            this.signInModule1.Parent = this;
            this.signInModule1.Size = new Microsoft.Xna.Framework.Point(50, 50);
            this.signInModule1.TemporaryOnly = true;
            this.signInModule1.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.signInModule1.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            // 
            // TestPlayerOptions
            // 
            this.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowAll;
            this.Panels.Add(this.menuGroup1);
            this.Panels.Add(this.signInModule1);
            this.PresenceMode = Microsoft.Xna.Framework.GamerServices.GamerPresenceMode.SettingUpMatch;
            this.TransitionOffOffset = new Microsoft.Xna.Framework.Point(-500, 0);
            this.TransitionOnOffset = new Microsoft.Xna.Framework.Point(500, 0);

        }

        private void menuGroup1_MenuCancel(object sender, ButtonPressEventArgs e)
        {
            AnarchyGamer.ClearAIGamers();
            this.ExitScreen();
        }
    }
}
