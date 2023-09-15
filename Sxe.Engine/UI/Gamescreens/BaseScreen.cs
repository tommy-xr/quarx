using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;


using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.GamerServices;


using Sxe.Engine.Input;


using Sxe.Engine.Test.Framework;

#if !XBOX
using System.ComponentModel.Design;
using Sxe.Design;
#endif


namespace Sxe.Engine.UI
{
   
    public class BaseScreen : PanelContainer, IGameScreen, IDisposable
    {
        ContentManager content;
        IGameScreenService gameScreenService;


        //Panel backgroundPanel;
        //SpriteBatch spriteBatch;



        ScreenState screenState;
        float transitionPosition = 0.0f;

        bool otherScreenHasFocus;
        bool isPopup = false;
        bool isExiting = false;
        bool allowEscape = true;
        bool hasUpdated = false;

        string musicCueName;
        Cue musicCue;
        bool musicPushed = false;
        bool isCovered = false;

        //Vector2 designResolution = new Vector2(800, 600);



        List<IInputEventReceiver> eventHandlers = new List<IInputEventReceiver>();

        MouseEventArgs normalizedMouseEvent = new MouseEventArgs();



#if !XBOX
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
#endif
        public IGameScreenService GameScreenService
        {
            get { return gameScreenService; }
            set { gameScreenService = value; }
        }

#if !XBOX
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
#endif
        public ScreenState ScreenState
        {
            get { return screenState; }
        }

        private GamerPresenceMode presenceMode = GamerPresenceMode.AtMenu;

        [DefaultValue(GamerPresenceMode.AtMenu)]
        public virtual GamerPresenceMode PresenceMode
        {
            get { return presenceMode; }
            set { presenceMode = value; }
        }




        public bool IsPopup
        {
            get { return isPopup; }
            set { isPopup = value; }
        }

        //public bool IsCovered
        //{
        //    get { return isCovered; }
        //    set { isCovered = value; }
        //}

        
        public bool AllowEscape
        {
            get { return allowEscape; }
            set { allowEscape = value; }
        }

#if !XBOX
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
#endif
        public bool IsExiting
        {
            get { return isExiting; }
            protected internal set { isExiting = value; }
        }





        /// <summary>
        /// Checks whether this screen is active and can respond to user input.
        /// </summary>
#if !XBOX
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
#endif
        public bool IsActive
        {
            get
            {
                return !otherScreenHasFocus &&
                       (screenState == ScreenState.TransitionOn ||
                        screenState == ScreenState.Active);
            }
        }


       
        /// <summary>
        /// Returns state of transition. 0.0f = fully active (no transition),
        /// 1.0f = fully transitioned
        /// </summary>

        public override float TransitionPosition
        {
            get 
            {

                if (this.screenState == ScreenState.TransitionOff)
                    return -transitionPosition;
                else
                    return transitionPosition;
            }
            set { transitionPosition = value; }
        }


        public string MusicCue
        {
            get { return musicCueName; }
            set { musicCueName = value; }
        }

        public BaseScreen()
            : base()
        {
            this.Location = Point.Zero;
            AnarchyGamer.SignedIn += this.OnGamerSignedIn;
            //this.InputFilterMode = InputFilterMode.AllowAll;
        }


        /// <summary>
        /// Called when focus is switched from another game screen to this game screen
        /// </summary>
        public override void Activate()
        {

            if (Audio != null && musicCueName != null && musicCueName != "" && musicCueName != "none")
            {
                Audio.PushMusic(musicCueName);
                musicPushed = true;
                //if (musicCue != null)
                //{
                //    musicCue.Stop(AudioStopOptions.AsAuthored);
                //    musicCue.Dispose();
                //    musicCue = null;
                //}

                //musicCue = Audio.soundBank.GetCue(musicCueName);
                //musicCue.Play();

            }
            else if (Audio != null && musicCueName == "none") 
            {
                Audio.PauseMusic();
            }

            for (int i = 0; i < AnarchyGamer.Gamers.Count; i++)
                AnarchyGamer.Gamers[i].PresenceMode = this.PresenceMode;

            base.Activate();
            //screenState = ScreenState.TransitionOn;
            //forms.Activate();
        }
        /// <summary>
        /// Called when focus is switched from this game screen to another
        /// </summary>
        public virtual void Deactivate()
        {
            if (musicPushed && Audio != null)
            {

                Audio.PopMusic();
                musicPushed = false;
                //musicCue.Stop(AudioStopOptions.AsAuthored);
                //musicCue.Dispose();
                //musicCue = null;
            }
            else if (Audio != null && musicCueName == "none")
            {
                Audio.ResumeMusic();
            }

            //forms.Deactivate();
        }

        public override sealed void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public virtual void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            //Hack - in design mode, we need transition position to be at 0, we don't want to see transitions
            //there. But if we leave it 0 to start, the fly in transition won't play.
            if (!hasUpdated)
            {
                transitionPosition = 1.0f;
                hasUpdated = true;
            }

            //Update all UI elements
            //backgroundPanel.Update(gameTime);

            this.otherScreenHasFocus = otherScreenHasFocus;
            //this.isCovered = coveredByOtherScreen;

            //if (allowEscape)
            //{
            //    if (GameScreenService.Input.Controllers.IsKeyJustPressed("menu_back"))
            //        this.ExitScreen();
            //}

            if (isExiting)
            {
                // If the screen is going away to die, it should transition off.
                screenState = ScreenState.TransitionOff;

                if (!UpdateTransition(gameTime, TransitionOffTime, 1))
                {
                    // When the transition finishes, remove the screen.
                    GameScreenService.RemoveScreen(this);
                }
            }
            else if (coveredByOtherScreen)
            {
                // If the screen is covered by another, it should transition off.
                if (UpdateTransition(gameTime, TransitionOffTime, 1))
                {
                    // Still busy transitioning.
                    screenState = ScreenState.TransitionOff;
                }
                else
                {
                    // Transition finished!
                    screenState = ScreenState.Hidden;
                }
            }
            else
            {
                // Otherwise the screen should transition on and become active.
                if (UpdateTransition(gameTime, TransitionOnTime, -1))
                {
                    // Still busy transitioning.
                    screenState = ScreenState.TransitionOn;
                }
                else
                {
                    // Transition finished!
                    screenState = ScreenState.Active;
                }
            }


            base.Update(gameTime);
            //forms.Update(gameTime);
        }


        public void ExitScreen()
        {
            if (TransitionOffTime == TimeSpan.Zero)
            {
                // If the screen has a zero transition time, remove it immediately.
                gameScreenService.RemoveScreen(this);
            }
            else
            {
                // Otherwise flag that it should transition off and then exit.
                isExiting = true;
            }
        }

        public virtual void PreDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            
            Vector2 viewSize = new Vector2(spriteBatch.GraphicsDevice.Viewport.Width, spriteBatch.GraphicsDevice.Viewport.Height);
            
            //TODO: Fix this to work right, in designer mode
            Scale = viewSize / new Vector2(this.Size.X, this.Size.Y);

            //Draw all UI elements
            base.Draw(spriteBatch, gameTime);

        }

        /// <summary>
        /// Normalize the mouse coordinates based on our current resolution and the screen design resolution
        /// We have to make a copy of the event, otherwise we're "corrupting" it for other event handlers
        /// that we may not necessarily know about! IE, the global forms event handler does not expect
        /// a normalized coordinate
        /// </summary>

        protected InputEventArgs NormalizeEvent(InputEventArgs inputEvent)
        {
            //This is ghetto... but jump in and intercept a mouse event, and resize it if we are a weird resolution!

            MouseEventArgs mouseEvent = inputEvent as MouseEventArgs;
            if (mouseEvent != null)
            {
                normalizedMouseEvent.LeftButtonPressed = mouseEvent.LeftButtonPressed;
                normalizedMouseEvent.MouseEventType = mouseEvent.MouseEventType;
                normalizedMouseEvent.Position = mouseEvent.Position;
                normalizedMouseEvent.Sender = mouseEvent.Sender;

                Vector2 view = new Vector2(gameScreenService.GraphicsDevice.Viewport.Width,
                    gameScreenService.GraphicsDevice.Viewport.Height);
                Vector2 scale = new Vector2(this.Size.X, this.Size.Y) / view;
                normalizedMouseEvent.Position = new Point((int)(mouseEvent.Position.X * scale.X),
                    (int)(mouseEvent.Position.Y * scale.Y));
                return normalizedMouseEvent;
            }

            return inputEvent;
        }

        public override bool HandleEvent(InputEventArgs inputEvent)
        {

            
            InputEventArgs adjEvent = NormalizeEvent(inputEvent);

            return base.HandleEvent(adjEvent);

            //backgroundPanel.HandleEvent(adjEvent);
            //Loop through each of our panels and see if they handle the event
            //for (int i = 0; i < Panels.Count; i++)
            //    Panels[i].HandleEvent(adjEvent);
            
            ////for (int i = 0; i < eventHandlers.Count; i++)
            ////    eventHandlers[i].HandleEvent(adjEvent);
            
            ////No matter what, we handled the event, so return true
            //return true;
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        //Release managed resources
        //    }
        //    spriteBatch.Dispose();
        //}



        protected override void Dispose(bool disposing)
        {
            AnarchyGamer.SignedIn -= this.OnGamerSignedIn;

            base.Dispose(disposing);
            //spriteBatch.Dispose();
        }

        public virtual void LoadContent()
        {
            if (content == null)
            {
                content = new ContentManager(GameScreenService.Services, "Content");

                LoadContent(content);
            }

        }

        private void OnGamerSignedIn(object sender, AnarchySignedInEventArgs args)
        {
            if (args.Gamer != null)
                args.Gamer.PresenceMode = this.PresenceMode;
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            if (content != null)
            {
                content.Unload();
                content = null;

               
            }
        }

    }
}
