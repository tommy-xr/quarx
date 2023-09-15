#region File Description
//-----------------------------------------------------------------------------
// ScreenManager.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
#endregion

using Sxe.Engine.Input;
using Sxe.Engine.Storage;

namespace Sxe.Engine.UI
{
    /// <summary>
    /// The screen manager is a component which manages one or more BaseScreen
    /// instances. It maintains a stack of screens, calls their Update and Draw
    /// methods at the appropriate times, and automatically routes input to the
    /// topmost active screen.
    /// </summary>
    public class ScreenManager : SxeDrawableGameComponent, IGameScreenService
    {
        #region Fields

        List<BaseScreen> screens = new List<BaseScreen>();
        List<BaseScreen> screensToUpdate = new List<BaseScreen>();

        SpriteBatch spriteBatch;
        SpriteFont font;
        Texture2D blankTexture;

        bool isInitialized;
        bool traceEnabled = true;

        ContentManager content;
        SchemeManager schemes;
        IInputService input;
        IXboxMessageService xboxMessages;
        AudioManager audio;
        IAnarchyGamerService gamerService;
        private IStorageDeviceService storageService;
        #endregion

        #region Properties


        /// <summary>
        /// A default SpriteBatch shared by all the screens. This saves
        /// each screen having to bother creating their own local instance.
        /// </summary>
        public SpriteBatch SpriteBatch
        {
            get { return spriteBatch; }
        }


        /// <summary>
        /// A default font shared by all the screens. This saves
        /// each screen having to bother loading their own local copy.
        /// </summary>
        public SpriteFont Font
        {
            get { return font; }
        }


        /// <summary>
        /// If true, the manager prints out a list of all the screens
        /// each time it is updated. This can be useful for making sure
        /// everything is being added and removed at the right times.
        /// </summary>
        public bool TraceEnabled
        {
            get { return traceEnabled; }
            set { traceEnabled = value; }
        }

        public IInputService Input { get { return input; } }
        //public I2DRendererService Renderer { get { return renderer; } }
        //public IAudioService Audio { get { return audio; } }
        public ContentManager Content { get { return content; } }
        public SchemeManager Schemes { get { return schemes; } }
        //public GraphicsDevice GraphicsDevice { get { return Game.GraphicsDevice; } }
        public IServiceProvider Services { get { return Game.Services; } }
        public IAnarchyGamerService AnarchyGamers { get { return gamerService; } }
        public IXboxMessageService XboxMessage { get { return this.xboxMessages; } }
        public IStorageDeviceService Storage { get { return storageService; } }
        public AudioManager Audio
        {
            get { return audio; }
            set { audio = value; }
        }

        public Game SxeGame { get { return Game; } }


        #endregion

        #region Initialization


        /// <summary>
        /// Constructs a new screen manager component.
        /// </summary>
        public ScreenManager(SxeGame game, string basePath)
            : base(game)
        {
            content = new ContentManager(game.Services, basePath);
            schemes = new SchemeManager(game.Services);
            game.Services.AddService(typeof(IGameScreenService), this);
            spriteBatch = new SpriteBatch(game.GraphicsDevice);

            //schemes.LoadSchemeFromFile("Data/default_scheme.res");

        }


        /// <summary>
        /// Initializes the screen manager component.
        /// </summary>
        public override void Initialize()
        {
            //Load the input and render components
            this.input = (IInputService)Game.Services.GetService(typeof(IInputService));
            this.gamerService = (IAnarchyGamerService)Game.Services.GetService(typeof(IAnarchyGamerService));
            this.storageService = (IStorageDeviceService)Game.Services.GetService(typeof(IStorageDeviceService));
            this.xboxMessages = (IXboxMessageService)Game.Services.GetService(typeof(IXboxMessageService));
            //renderer = (I2DRendererService)Game.Services.GetService(typeof(I2DRendererService));
            //audio = (IAudioService)Game.Services.GetService(typeof(IAudioService));

#if !XBOX
            input.Mouse.AddEventHandler(this);
            input.Keyboard.AddEventHandler(this);
#endif

            for(int i = 0; i < input.Controllers.Count; i++)
            input.Controllers[i].AddEventHandler(this);

            base.Initialize();

            isInitialized = true;
        }


        /// <summary>
        /// Load your graphics content.
        /// </summary>
        protected override void LoadContent()
        {

            blankTexture = content.Load<Texture2D>("blank");

            // Tell each of the screens to load their content.
            foreach (BaseScreen screen in screens)
            {
                screen.LoadContent();
            }
        }


        /// <summary>
        /// Unload your graphics content.
        /// </summary>
        protected override void UnloadContent()
        {
            // Tell each of the screens to unload their content.
            foreach (BaseScreen screen in screens)
            {
                screen.UnloadContent();
            }
        }


        #endregion

        #region Update and Draw


        /// <summary>
        /// Allows each screen to run logic.
        /// </summary>
        protected override void Update(GameTime gameTime)
        {

            // Make a copy of the master screen list, to avoid confusion if
            // the process of updating one screen adds or removes others.
            screensToUpdate.Clear();

            foreach (BaseScreen screen in screens)
                screensToUpdate.Add(screen);

            bool otherScreenHasFocus = !Game.IsActive;
            bool coveredByOtherScreen = false;

            // Loop as long as there are screens waiting to be updated.
            while (screensToUpdate.Count > 0)
            {
                // Pop the topmost screen off the waiting list.
                BaseScreen screen = screensToUpdate[screensToUpdate.Count - 1];

                screensToUpdate.RemoveAt(screensToUpdate.Count - 1);

                // Update the screen.
                screen.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

                if (screen.ScreenState == ScreenState.TransitionOn ||
                    screen.ScreenState == ScreenState.Active)
                {
                    // If this is the first active screen we came across,
                    // give it a chance to handle input.
                    if (!otherScreenHasFocus)
                    {
                        //screen.HandleInput(input);

                        otherScreenHasFocus = true;
                    }

                    // If this is an active non-popup, inform any subsequent
                    // screens that they are covered by it.
                    if (!screen.IsPopup)
                        coveredByOtherScreen = true;
                }
            }

            // Print debug trace?
            if (traceEnabled)
                TraceScreens();
        }

        public bool HandleEvent(InputEventArgs args)
        {
            int i = screens.Count - 1;
            //Changed 10/3/2008 - only allow top screen to take input
            while (i >= 0)
            {
                if (screens[i].IsActive)
                {
                    return screens[i].HandleEvent(args);
                }

                i--;
            }
            //Looks like the ordering was wrong in below code!

            //while (i < screens.Count)
            //{
            //    if (screens[i].IsActive)
            //    {
            //        if (screens[i].HandleEvent(args))
            //            return true;
            //    }
            //    i++;
            //}
            return false;
        }


        /// <summary>
        /// Prints a list of all the screens, for debugging.
        /// </summary>
        void TraceScreens()
        {
            List<string> screenNames = new List<string>();

            foreach (BaseScreen screen in screens)
                screenNames.Add(screen.GetType().Name);

            //Console.WriteLine(string.Join(", ", screenNames.ToArray()));
        }

        protected override void PreDraw(GameTime gameTime)
        {
            foreach (BaseScreen screen in screens)
            {
                if (screen.ScreenState == ScreenState.Hidden)
                    continue;

                screen.PreDraw(spriteBatch, gameTime);
            }
            base.PreDraw(gameTime);
        }

        /// <summary>
        /// Tells each screen to draw itself.
        /// </summary>
        protected override void Draw(GameTime gameTime)
        {
            foreach (BaseScreen screen in screens)
            {
                if (screen.ScreenState == ScreenState.Hidden)
                    continue;

                screen.Draw(spriteBatch, gameTime);
            }


            if (AnarchySettings.ShowTitleSafeBars)
            {
                spriteBatch.Begin();
                int marginY = (int)(this.GraphicsDevice.Viewport.Height * 0.1f);
                int marginX = (int)(this.GraphicsDevice.Viewport.Width * 0.1f);
                int width = this.GraphicsDevice.Viewport.Width;
                int height = this.GraphicsDevice.Viewport.Height;

                spriteBatch.Draw(blankTexture, new Rectangle(0, 0, width, marginY), Color.Red);
                spriteBatch.Draw(blankTexture, new Rectangle(0, 0, marginX, height), Color.Red);
                spriteBatch.Draw(blankTexture, new Rectangle(width - marginX, 0, marginX, height), Color.Red);
                spriteBatch.Draw(blankTexture, new Rectangle(0, height - marginY, width, marginY), Color.Red);
                spriteBatch.End();
            }
        }


        #endregion

        #region Public Methods


        /// <summary>
        /// Adds a new screen to the screen manager.
        /// </summary>
        public void AddScreen(BaseScreen screen)
        {
            screen.GameScreenService = this;
            screen.IsExiting = false;


            // If we have a graphics device, tell the screen to load content.
            if (isInitialized)
            {
                screen.LoadContent();
                //Added, maybe this will fix some animation woes
                Game.ResetElapsedTime();
            }
            screens.Add(screen);
            screen.Activate();
        }


        /// <summary>
        /// Removes a screen from the screen manager. You should normally
        /// use BaseScreen.ExitScreen instead of calling this directly, so
        /// the screen can gradually transition off rather than just being
        /// instantly removed.
        /// </summary>
        public void RemoveScreen(BaseScreen screen)
        {
            screen.Deactivate();
            // If we have a graphics device, tell the screen to unload content.
            if (isInitialized)
            {
                screen.UnloadContent();
            }

            screens.Remove(screen);
            screensToUpdate.Remove(screen);
        }

        public bool ContainsScreen(BaseScreen screen)
        {
            return screens.Contains(screen);
        }



        /// <summary>
        /// Helper draws a translucent black fullscreen sprite, used for fading
        /// screens in and out, and for darkening the background behind popups.
        /// </summary>
        // TODO: Chagne this
        public void FadeBackBufferToBlack(int alpha)
        {
            Viewport viewport = GraphicsDevice.Viewport;

            spriteBatch.Begin();

            spriteBatch.Draw(blankTexture,
                             new Rectangle(0, 0, viewport.Width, viewport.Height),
                             new Color(0, 0, 0, (byte)alpha));

            spriteBatch.End();
        }


        #endregion
    }
}
