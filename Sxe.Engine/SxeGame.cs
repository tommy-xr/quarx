using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

using Sxe.Engine.UI;
using Sxe.Engine.Input;
using Sxe.Engine.Graphics;
using Sxe.Engine.Utilities;
using Sxe.Engine.Utilities.Console;
using Sxe.Engine.Test.Framework;
using Sxe.Engine.Storage;

//using Sxe.Library.Bsp;
//using Sxe.Library.MultiModel;

using System.ComponentModel;

#if !XBOX
using Sxe.Design;
#endif


namespace Sxe.Engine
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class SxeGame : Microsoft.Xna.Framework.Game
    {

        static SxeGame sxeGameInstance;
        static bool debugMode;



        public static bool DebugMode
        {
            get { return debugMode; }
            set { debugMode = value; }
        }

        public static SxeGame GetGameInstance
        {
            get { return sxeGameInstance; }
        }

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Components
        Render2DComponent render2D;
        ScreenManager gamescreenComponent;
        FpsComponent fpsComponent;
        ConsoleComponent consoleComponent;
        InputComponent inputComponent;
        AnarchyGamerComponent gamerComponent;
        private StorageDeviceComponent storageComponent;
        private XboxMessageComponent xboxMessageComponent;


        List<IGameComponent> sxeComponents = new List<IGameComponent>();

#if !XBOX
        int screenShotCount = 0;
        //ResolveTexture2D screenshotTexture;
        bool canTakeScreenshot = true;
#endif



        IScheme defaultScheme;
        public ScreenManager GameScreenComponent
        {
            get { return gamescreenComponent; }
        }

        public InputComponent InputComponent
        {
            get { return inputComponent; }
        }

        public IRender2DService Render2D
        {
            get { return render2D; }
        }
        public IScheme DefaultScheme
        {
            get { return defaultScheme; }
        }

        public virtual string GetGameTitle()
        {
            return "AnarchyGame";
        }

        public virtual void OnStorageDeviceConnect(StorageDevice device)
        {
        }

        protected virtual Point DefaultResolution
        {
            get { return new Point(1280, 720); }
        }
        protected StorageDeviceComponent StorageDeviceComponent
        {
            get { return this.storageComponent; }
        }


        public SxeGame()
        {

            GlobalContent.Initialize(this.Services);
            //AnarchySettings.ShowTitleSafeBars = true;

            sxeGameInstance = this;
#if DEBUG
            Globals.DebugMode = true;
#endif

            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            //this.IsFixedTimeStep = true;
            this.graphics.SynchronizeWithVerticalRetrace = false;


            DisplayMode m = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode;

            this.graphics.PreferredBackBufferWidth = this.DefaultResolution.X;
            this.graphics.PreferredBackBufferHeight = this.DefaultResolution.Y;
            //this.graphics.IsFullScreen = true;
            this.graphics.ApplyChanges();

            SignedInGamer.SignedIn += this.OnGamerSignedIn;
            SignedInGamer.SignedOut += this.OnGamerSignedOut;

#if !XBOX
           
#endif

            //#if XBOX
            //            this.graphics.PreferMultiSampling = true;
            //            this.graphics.PreferredBackBufferHeight = 1080;
            //            this.graphics.PreferredBackBufferWidth = 1920; 
            //            this.graphics.ApplyChanges();
            //#endif
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            Panel.Services = this.Services;

            GlobalForms.Initialize(this.GraphicsDevice);


            render2D = new Render2DComponent(this, this.GraphicsDevice);
            gamescreenComponent = new ScreenManager(this, "content");
            defaultScheme = gamescreenComponent.Schemes.DefaultScheme;

           
            inputComponent = new Sxe.Engine.Input.InputComponent(this);
            gamerComponent = new AnarchyGamerComponent(this);
            this.storageComponent = new StorageDeviceComponent(this);
            this.xboxMessageComponent = new XboxMessageComponent(this);
            Render3DComponent render3D = new Render3DComponent(this, this.GraphicsDevice, 200);

            if (DebugMode)
            {
                fpsComponent = new FpsComponent(this, Content);
                Components.Add(fpsComponent);
            }
            //Components.Add(gamescreenComponent);
            sxeComponents.Add(gamescreenComponent);
            if (Globals.DebugMode)
            {
                consoleComponent = new ConsoleComponent(this);
                Components.Add(consoleComponent);
            }


            Components.Add(inputComponent);
            Components.Add(render2D);
            Components.Add(render3D);
            Components.Add(this.storageComponent);
            Components.Add(this.xboxMessageComponent);
            Components.Add(gamerComponent);

            base.Initialize();

            for (int i = 0; i < sxeComponents.Count; i++)
                sxeComponents[i].Initialize();


            if (Globals.DebugMode)
            {
                //GraphicalTestForm gtf = new GraphicalTestForm(Services, gamescreenComponent);
                //gtf.Visible = false;
                ////FormCollection.GlobalForms.Add(gtf);
                //GlobalForms.FormCollection.AddForm(gtf);

                //TestForm testForm = new TestForm(gamescreenComponent.Schemes, Content);
                //testForm.Visible = false;
                ////FormCollection.GlobalForms.Add(testForm);
                //GlobalForms.FormCollection.AddForm(testForm);

                //PerformanceForm pf = new PerformanceForm(gamescreenComponent.Schemes, Content);
                //pf.Visible = true;
                //GlobalForms.FormCollection.AddForm(pf);
            }

#if !XBOX
            inputComponent.Mouse.AddEventHandler(GlobalForms.FormCollection);
            inputComponent.Keyboard.AddEventHandler(GlobalForms.FormCollection);
#endif
            for(int i = 0; i < inputComponent.Controllers.Count; i++)
            inputComponent.Controllers[i].AddEventHandler(GlobalForms.FormCollection);
        }

        public virtual void PreCache()
        {
            AnarchyGamer.PreCache(Content);

            //TODO: We shouldn't have to do this here
            //We should consider adding a Load() method to the gamers
            //And call load() when they are added, and unload when they are removed
            //Since we don't have that yet, we have to do this, which is very poor
            AnarchyGamer.ComputerIcon = Content.Load<Texture2D>("icons\\computericon");
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            PreCache();

            // Create a new SpriteBatch, which can be used to draw textures.

            spriteBatch = new SpriteBatch(GraphicsDevice);

            BaseScreen gs = new BaseScreen();
            gamescreenComponent.AddScreen(gs);

            BaseScreen s = new BaseScreen();
            gamescreenComponent.AddScreen(s);

            gamescreenComponent.RemoveScreen(s);


            // TODO: use this.Content to load your game content here
        }

        public virtual void OnGamerSignedIn(object sender, SignedInEventArgs args)
        {
            if (args.Gamer != null)
            {

#if XBOX
                InputComponent.Controllers[(int)args.Gamer.PlayerIndex].Gamer = 
                    AnarchyGamer.Gamers.GetGamerByPlayerIndex((int)args.Gamer.PlayerIndex);
#else
                //InputComponent.Controllers[0].Gamer = args.Gamer;
#endif
                //this.InitializeController(Controller);

                //args.Gamer.Tag = data;
            }
        }

        public virtual void OnGamerSignedOut(object sender, SignedOutEventArgs args)
        {
#if XBOX
                 InputComponent.Controllers[(int)args.Gamer.PlayerIndex].Gamer = null;       
#endif
        }

        /// <summary>
        /// This function is called when a gamer signs in. You should place your bind code here.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="controller"></param>
        public virtual void InitializeController(IGameController controller)
        {
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            //    this.Exit();

            //if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            //    this.Exit();




            GlobalForms.Update(gameTime);

            for (int i = 0; i < sxeComponents.Count; i++)
            {
                SxeGameComponent component = sxeComponents[i] as SxeGameComponent;
                if (component != null)
                    component.BaseUpdate(gameTime);
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {

            for (int i = 0; i < sxeComponents.Count; i++)
            {
                SxeDrawableGameComponent drawable = sxeComponents[i] as SxeDrawableGameComponent;
                if (drawable != null)
                    drawable.BasePreDraw(gameTime);
            }


            GlobalForms.PreDraw(gameTime);
            // TODO: Add your drawing code here

            GraphicsDevice.Clear(Color.Black);


            




            for (int i = 0; i < sxeComponents.Count; i++)
            {
                SxeDrawableGameComponent drawable = sxeComponents[i] as SxeDrawableGameComponent;
                if (drawable != null)
                    drawable.BaseDraw(gameTime);
            }

            base.Draw(gameTime);

#if !XBOX
            //Check if we need to take a screenshot
            if(Keyboard.GetState().IsKeyDown(Keys.PrintScreen))
            {
                if(this.canTakeScreenshot)
                this.TakeScreenshot();
                this.canTakeScreenshot = false;
            }
            else
                this.canTakeScreenshot = true;
#endif


            //GlobalForms.Draw(gameTime);

            

            //spriteBatch.Begin();
            //spriteBatch.Draw(form1.FormTexture, new Rectangle(form1.Location.X, form1.Location.Y, form1.Size.X, form1.Size.Y),
            //    Color.White);
            //spriteBatch.Draw(form2.FormTexture, new Rectangle(form2.Location.X, form2.Location.Y, form2.Size.X, form2.Size.Y),
            //    Color.White);
            //spriteBatch.End();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                GlobalContent.Dispose(disposing);
            }

            base.Dispose(disposing);
        }

#if !XBOX
        private void TakeScreenshot()
        {
            //this.screenshotTexture = new ResolveTexture2D(this.GraphicsDevice, 
            //    this.GraphicsDevice.PresentationParameters.BackBufferWidth, 
            //    this.GraphicsDevice.PresentationParameters.BackBufferHeight, 1, SurfaceFormat.Color);
            //this.GraphicsDevice.ResolveBackBuffer(this.screenshotTexture);

            //this.screenshotTexture.Save("screenshot" + this.screenShotCount.ToString() + ".jpg", ImageFileFormat.Jpg);
            //this.screenShotCount++;
        }
#endif


    }
}
