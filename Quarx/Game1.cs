using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

using Sxe.Engine;
using Sxe.Engine.UI;
using Sxe.Engine.Input;

namespace Quarx
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : SxeGame
    {
        //GraphicsDeviceManager graphics;
        //SpriteBatch spriteBatch;

        //public Game1()
        //{
        //    graphics = new GraphicsDeviceManager(this);
        //    Content.RootDirectory = "Content";
        //}

        ///// <summary>
        ///// Allows the game to perform any initialization it needs to before starting to run.
        ///// This is where it can query for any required services and load any non-graphic
        ///// related content.  Calling base.Initialize will enumerate through any components
        ///// and initialize them as well.
        ///// </summary>
        //protected override void Initialize()
        //{
        //    // TODO: Add your initialization logic here

        //    base.Initialize();
        //}

        bool isEditing = false;

        public override string GetGameTitle()
        {
            return "Quarx";
            //return base.GetGameTitle();
        }

        protected override Point DefaultResolution
        {
            get
            {
#if XBOX
                return base.DefaultResolution;
#else
                return new Point(800,600); 
#endif

            }
        }

        public override void OnStorageDeviceConnect(StorageDevice device)
        {
            HighScores.Initialize(device);


            base.OnStorageDeviceConnect(device);
        }

        public void ProcessArguments(string[] args)
        {

            for (int i = 0; i < args.Length; i++)
            {
                //System.Windows.Forms.MessageBox.Show(args[i]);
                if (args[i].Contains("-editor"))
                {
                    isEditing = true;
                }
            }
        }

        //        public override void InitializeController(Sxe.Engine.Input.IGameController controller)
        //        {
        //            controller.AddAction("move_left");
        //            controller.AddAction("move_right");
        //            controller.AddAction("move_down");
        //            controller.AddAction("pause");
        //            controller.AddAction("x_button");
        //#if !XBOX
        //            controller.Bind("a", "left");
        //            controller.Bind("d", "right");
        //            controller.Bind("left", "move_left");
        //            controller.Bind("right", "move_right");
        //            controller.Bind("down", "move_down");
        //            controller.Bind("p", "pause");
        //            controller.Bind("x", "x_button");
        //#else
        //            controller.Bind("left_stick_left", "move_left");
        //            controller.Bind("left_stick_right", "move_right");
        //            controller.Bind("left_stick_down", "move_down");
        //            controller.Bind("dpad_left", "move_left");
        //            controller.Bind("dpad_right", "move_right");
        //            controller.Bind("dpad_down", "move_down");
        //            controller.Bind("a_button", "left");
        //            controller.Bind("b_button", "right");
        //            controller.Bind("start_button", "pause");
        //            controller.Bind("x_button", "x_button");
        //#endif



        //            base.InitializeController(controller);
        //        }

        protected override void Initialize()
        {
            AudioManager audioManager = new AudioManager(this, "Content/Sound", "Quarx");
            Panel.Audio = audioManager;
            Components.Add(audioManager);

            Components.Add(new GamerServicesComponent(this));


            base.Initialize();


            //contentLoader.Add(QuarxStrings.Intro);
            
            

 

            AnarchyGamer.SignInScreen = new NewGameScreens.QuarxSignInScreen();
            AnarchyGamer.DisconnectScreen = new NewGameScreens.QuarxDisconnectedScreen();


            for (int i = 0; i < InputComponent.Controllers.Count; i++)
            {
                InputComponent.Controllers[i].AddAction("move_left");
                InputComponent.Controllers[i].AddAction("move_right");
                InputComponent.Controllers[i].AddAction("move_down");
                InputComponent.Controllers[i].AddAction("pause");
                InputComponent.Controllers[i].AddAction("x_button");
                InputComponent.Controllers[i].AddAction("y_button");
                InputComponent.Controllers[i].AddAction("b_button");
                InputComponent.Controllers[i].AddAction("a_button");
                InputComponent.Controllers[i].AddAction("exit_pause");

#if !XBOX
                if (InputComponent.Controllers[i].GetType() == typeof(KeyboardMouseGameController))
                {
                    InputComponent.Controllers[i].Bind("a", "left");
                    InputComponent.Controllers[i].Bind("d", "right");
                    InputComponent.Controllers[i].Bind("left", "move_left");
                    InputComponent.Controllers[i].Bind("right", "move_right");
                    InputComponent.Controllers[i].Bind("down", "move_down");
                    InputComponent.Controllers[i].Bind("p", "pause");
                    InputComponent.Controllers[i].Bind("q", "exit_pause");
                    InputComponent.Controllers[i].Bind("x", "x_button");
                    InputComponent.Controllers[i].Bind("y", "y_button");
                    InputComponent.Controllers[i].Bind("a", "a_button");
                    InputComponent.Controllers[i].Bind("b", "b_button");


                }
                //else
                //{
#endif

                    InputComponent.Controllers[i].Bind("left_stick_left", "move_left");
                    InputComponent.Controllers[i].Bind("left_stick_right", "move_right");
                    InputComponent.Controllers[i].Bind("left_stick_down", "move_down");
                    InputComponent.Controllers[i].Bind("dpad_left", "move_left");
                    InputComponent.Controllers[i].Bind("dpad_right", "move_right");
                    InputComponent.Controllers[i].Bind("dpad_down", "move_down");
                    InputComponent.Controllers[i].Bind("a_button", "left");
                    InputComponent.Controllers[i].Bind("b_button", "right");
                    InputComponent.Controllers[i].Bind("start_button", "pause");
                    InputComponent.Controllers[i].Bind("x_button", "x_button");
                    InputComponent.Controllers[i].Bind("a_button", "a_button");
                    InputComponent.Controllers[i].Bind("y_button", "y_button");
                    InputComponent.Controllers[i].Bind("b_button", "b_button");
                    InputComponent.Controllers[i].Bind("back_button", "exit_pause");
#if !XBOX
                //}
#endif

            }
            //QuarxScreens.Initialize(GameScreenComponent, Content);

            //GameScreenComponent.AddScreen(new QuarxTestScreen(GameScreenComponent, scheme, Content));
            //GameScreenComponent.AddScreen(new BackgroundScreen());
            //GameScreenComponent.AddScreen(QuarxScreens.MenuScreen);


            GameScreenComponent.Audio = audioManager;

            //GameScreenComponent.AddScreen(new AITest());

            //GameScreenComponent.AddScreen(new Sxe.Engine.Gamers.DefaultScreens.DefaultControllerDisconnectScreen());
            //GameScreenComponent.AddScreen(new TestPlayerOptions());
            //GameScreenComponent.AddScreen(new Quarx.GameScreens.MenuScreen());

#if !XBOX
            if (isEditing)
            {
                GameScreenComponent.AddScreen(new PuzzleEditor());
                for (int i = 0; i < InputComponent.Controllers.Count; i++)
                    InputComponent.Controllers[i].Cursor.Visible = true;

            }
            else
            {
                StartLoadQuarx(GameScreenComponent);
            }
#else
            StartLoadQuarx(GameScreenComponent);
#endif
                //GameScreenComponent.AddScreen(new IntroScreen());



            //GameScreenComponent.AddScreen(new CreditsScreen());
            //GameScreenComponent.AddScreen(new IntroScreen());
            //GameScreenComponent.AddScreen(new PuzzleSelectionScreen());
                //GameScreenComponent.AddScreen(new Quarx.GameScreens.MenuScreen());

            
        }

        void StartLoadQuarx(ScreenManager screenManager)
        {
            //GlobalContentLoader contentLoader = new GlobalContentLoader();

            //    contentLoader.Add(QuarxStrings.Background);
            //    contentLoader.Add("howtoplay_screen");
            //    contentLoader.Add("highscoresbackground");
            //    //contentLoader.Add(QuarxStrings.Score1);
            //    //contentLoader.Add(QuarxStrings.Score2);
            //    //contentLoader.Add(QuarxStrings.Xbutton);
            //    //contentLoader.Add(QuarxStrings.Ybutton);
            //    //contentLoader.Add(QuarxStrings.MenuBack);
            //    //contentLoader.Add(QuarxStrings.AtomsBright);
            //    //contentLoader.Add(QuarxStrings.AtomsNormal);
            //    contentLoader.Add(QuarxStrings.BoardBack);


            LoadingScreen loadingScreen = new LoadingScreen();
            loadingScreen.LoadItems.Add(new GlobalContentLoader("howtoplay_screen"));
            loadingScreen.LoadItems.Add(new GlobalContentLoader("highscoresbackground"));
            loadingScreen.LoadItems.Add(new GlobalContentLoader("gamesetup"));
            loadingScreen.LoadItems.Add(new GlobalContentLoader(QuarxStrings.Score1));
            loadingScreen.LoadItems.Add(new GlobalContentLoader(QuarxStrings.Score2));
            loadingScreen.LoadItems.Add(new GlobalContentLoader(QuarxStrings.Xbutton));
            loadingScreen.LoadItems.Add(new GlobalContentLoader(QuarxStrings.Ybutton));
            loadingScreen.LoadItems.Add(new GlobalContentLoader(QuarxStrings.AtomsBright));
            loadingScreen.LoadItems.Add(new GlobalContentLoader(QuarxStrings.AtomsNormal));
            loadingScreen.LoadItems.Add(new GlobalContentLoader(QuarxStrings.BoardBack));
            loadingScreen.NextScreen = new IntroScreen();
            screenManager.AddScreen(loadingScreen);

            string sz = String.Empty;

            //    QuarxLoadingScreen loadingScreen = new QuarxLoadingScreen();
            //    loadingScreen.NextScreen = new IntroScreen();
            //    loadingScreen.LoadableCollection.Add(contentLoader);
            //    screenManager.AddScreen(loadingScreen);

            //TODO: Fix issue when you have a loading screen, then intro screen breaks!
            //Also, should BaseScreen implement ILoadable? That might be nice...
            //But percent complete/is complete wouldn't be available

            //screenManager.AddScreen(new IntroScreen());

            //screenManager.AddScreen(new Quarx.GameScreens.MenuScreen());
        }

        protected override void Draw(GameTime gameTime)
        {
            this.GraphicsDevice.Clear(Color.White);
            base.Draw(gameTime);
        }
    }
}
