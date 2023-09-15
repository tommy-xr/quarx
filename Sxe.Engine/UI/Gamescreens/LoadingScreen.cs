using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Diagnostics;


using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Sxe.Engine.UI
{
    //XNA SAMPLE

    /// <summary>
    /// The loading screen coordinates transitions between the menu system and the
    /// game itself. Normally one screen will transition off at the same time as
    /// the next screen is transitioning on, but for larger transitions that can
    /// take a longer time to load their data, we want the menu system to be entirely
    /// gone before we start loading the game. This is done as follows:
    /// 
    /// - Tell all the existing screens to transition off.
    /// - Activate a loading screen, which will transition on at the same time.
    /// - The loading screen watches the state of the previous screens.
    /// - When it sees they have finished transitioning off, it activates the real
    ///   next screen, which may take a long time to load its data. The loading
    ///   screen will be the only thing displayed while this load is taking place.
    /// </summary>
    public class LoadingScreen : BaseScreen
    {
        #region Fields

        bool loadingIsSlow = true;
        bool otherScreensAreGone;

        ILoadableCollection loadCollection = new ILoadableCollection();

        Thread backgroundThread;
        EventWaitHandle backgroundThreadExit;

        GameTime loadStartTime;
        TimeSpan loadAnimationTimer;

        SpriteFont font;
        SpriteBatch batch;

        BaseScreen nextScreen;

        private int currentItem = 0;

        #endregion

        #region Public Properties
        public ILoadableCollection LoadItems
        {
            get { return loadCollection; }
        }

        public bool IsLoadingSlow
        {
            get { return IsLoadingSlow; }
            set { IsLoadingSlow = value; }
        }

        public BaseScreen NextScreen
        {
            get { return nextScreen; }
            set { nextScreen = value; }
        }

        public float PercentComplete
        {
            get
            {
                if (this.LoadItems.Count == 0)
                    return 1f;
                else
                    return ((float)currentItem) / ((float)this.LoadItems.Count);
            }
        }
        #endregion

        #region Initialization


        /// <summary>
        /// The constructor is private: loading screens should
        /// be activated via the static Load method instead.
        /// </summary>
        public LoadingScreen()
        {

            TransitionOnTime = TimeSpan.FromSeconds(0.5);

            // If this is going to be a slow load operation, create a background
            // thread that will update the network session and draw the load screen
            // animation while the load is taking place.
            if (loadingIsSlow)
            {
                backgroundThread = new Thread(BackgroundWorkerThread);
                backgroundThreadExit = new ManualResetEvent(false);

               
            }

            InitializeComponent();
        }

        public override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            this.font = content.Load<SpriteFont>("Neuropol");
            base.LoadContent(content);
        }


        #endregion

        #region Update and Draw


        /// <summary>
        /// Updates the loading screen.
        /// </summary>
        public override void Update(GameTime gameTime, bool otherScreenHasFocus,
                                                       bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

            //Modified BP : Start loading when we transitioned on all the way
            if (otherScreensAreGone)
            {
                // Start up the background thread, which will update the network
                // session and draw the animation while we are loading.
                if (backgroundThread != null)
                {
                    loadStartTime = gameTime;
                    backgroundThread.Start();
                }

                // Perform the load operation.
                //ScreenManager.RemoveScreen(this);
           

                for (int i = 0; i < loadCollection.Count; i++)
                {
                    loadCollection[i].Load();
                    this.currentItem = i;
                    //System.Threading.Thread.Sleep(1000);
                }

                if (nextScreen != null)
                    GameScreenService.AddScreen(nextScreen);

                //System.Threading.Thread.Sleep(5000);

                //Fire off a garbage collect
                System.GC.Collect();


                //foreach (GameScreen screen in screensToLoad)
                //{
                //    if (screen != null)
                //    {
                //        ScreenManager.AddScreen(screen);
                //    }
                //}

                

                // Signal the background thread to exit, then wait for it to do so.
                if (backgroundThread != null)
                {
                    backgroundThreadExit.Set();
                    backgroundThread.Join();
                }

                GameScreenService.RemoveScreen(this);

                // Once the load has finished, we use ResetElapsedTime to tell
                // the  game timing mechanism that we have just finished a very
                // long frame, and that it should not try to catch up.
                GameScreenService.Game.ResetElapsedTime();
            }
        }


        /// <summary>
        /// Draws the loading screen.
        /// </summary>
        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            // If we are the only active screen, that means all the previous screens
            // must have finished transitioning off. We check for this in the Draw
            // method, rather than in Update, because it isn't enough just for the
            // screens to be gone: in order for the transition to look good we must
            // have actually drawn a frame without them before we perform the load.
            //if ((ScreenState == ScreenState.Active) &&
            //    (GameScreenService.GetScreens().Length == 1))
            //{
            //    otherScreensAreGone = true;
            //}

            if (ScreenState == ScreenState.Active && TransitionPosition == 0.0f)
                otherScreensAreGone = true;

            // The gameplay screen takes a while to load, so we display a loading
            // message while that is going on, but the menus load very quickly, and
            // it would look silly if we flashed this up for just a fraction of a
            // second while returning from the game to the menus. This parameter
            // tells us how long the loading is going to take, so we know whether
            // to bother drawing the message.
            if (loadingIsSlow)
            {


                base.Draw(spriteBatch, gameTime);

                //string message = "Loading";

                //// Center the text in the viewport.
                //Viewport viewport = GameScreenService.GraphicsDevice.Viewport;
                //Vector2 viewportSize = new Vector2(viewport.Width, viewport.Height);
                //Vector2 textSize = font.MeasureString(message);
                //Vector2 textPosition = (viewportSize - textSize) / 2;

                //Color color = new Color(255, 255, 255, TransitionAlpha);

                ////// Animate the number of dots after our "Loading..." message.
                //loadAnimationTimer += gameTime.ElapsedGameTime;

                //int dotCount = (int)(loadAnimationTimer.TotalSeconds * 5) % 10;

                //message += new string('.', dotCount);

               
               
                ////// Draw the text.
                //spriteBatch.Begin();
                //spriteBatch.DrawString(font, message, textPosition, color);
                //spriteBatch.End();

                
            }
        }


        #endregion

        #region Background Thread


        /// <summary>
        /// Worker thread draws the loading animation and updates the network
        /// session while the load is taking place.
        /// </summary>
        void BackgroundWorkerThread()
        {
            long lastTime = Stopwatch.GetTimestamp();

            // EventWaitHandle.WaitOne will return true if the exit signal has
            // been triggered, or false if the timeout has expired. We use the
            // timeout to update at regular intervals, then break out of the
            // loop when we are signalled to exit.
            while (!backgroundThreadExit.WaitOne(1000 / 30, false))
            {
                GameTime gameTime = GetGameTime(ref lastTime);

                DrawLoadAnimation(gameTime);

                //UpdateNetworkSession();
            }
        }


        /// <summary>
        /// Works out how long it has been since the last background thread update.
        /// </summary>
        GameTime GetGameTime(ref long lastTime)
        {
            long currentTime = Stopwatch.GetTimestamp();
            long elapsedTicks = currentTime - lastTime;
            lastTime = currentTime;

            TimeSpan elapsedTime = TimeSpan.FromTicks(elapsedTicks *
                                                      TimeSpan.TicksPerSecond /
                                                      Stopwatch.Frequency);

            return new GameTime(loadStartTime.TotalRealTime + elapsedTime, elapsedTime,
                                loadStartTime.TotalGameTime + elapsedTime, elapsedTime);
        }


        /// <summary>
        /// Calls directly into our Draw method from the background worker thread,
        /// so as to update the load animation in parallel with the actual loading.
        /// </summary>
        void DrawLoadAnimation(GameTime gameTime)
        {
            if ((GameScreenService.GraphicsDevice == null) || GameScreenService.GraphicsDevice.IsDisposed)
                return;

            //try
            //{
                GameScreenService.GraphicsDevice.Clear(Color.Black);


                if (batch == null)
                    batch = new SpriteBatch(GameScreenService.GraphicsDevice);
                // Draw the loading screen.
                Draw(batch, gameTime);

                GameScreenService.GraphicsDevice.Present();
            //}
            //catch
            //{
                // If anything went wrong (for instance the graphics device was lost
                // or reset) we don't have any good way to recover while running on a
                // background thread. Setting the device to null will stop us from
                // rendering, so the main game can deal with the problem later on.
                //graphicsDevice = null;
            //}
        }




        #endregion

        private void InitializeComponent()
        {
            // 
            // LoadingScreen
            // 
            //this.BackgroundPath = "gradient";

        }
    }
}
