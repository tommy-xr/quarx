using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.GamerServices;

using Sxe.Engine.Graphics;

namespace Sxe.Engine.Input
{
    public class KeyInfo
    {
        string name; //name of this key
        //int actionIndex = -1; //the index of the corresponding action (-1 if no corresponding action)
        //As opposed to keeping a single action, we'll keep a list of all actions this is bound to
        List<int> actionIndex = new List<int>();

        public IList<int> Actions
        {
            get { return actionIndex; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public KeyInfo(string inName)
        {
            Name = inName;
        }

        public void AddAction(int action)
        {
            actionIndex.Add(action);
        }

        public void UnbindAll()
        {
            actionIndex.Clear();
        }
    }

    /// <summary>
    /// This stores information for actions
    /// </summary>
    public class ActionInfo
    {
        bool allowRepeat= false;
        float nextRepeat= 0.1f;
        float repeatTime = 0.1f;

        float currentBaseTime = 0.3f;
        float baseTime = 0.3f;

        public bool AllowRepeat
        {
            get { return allowRepeat; }
            set { allowRepeat = value; }
        }

        public float NextRepeat
        {
            get { return nextRepeat; }
            set { nextRepeat = value; }
        }

        public float RepeatTime
        {
            get { return repeatTime; }
            set { repeatTime = value; }
        }

        public float CurrentBaseTime
        {
            get { return currentBaseTime; }
            set { currentBaseTime = value; }
        }

        public float BaseTime
        {
            get { return baseTime; }
            set { baseTime = value; }
        }

        public bool Dirty
        {
            get;
            set;
        }


    }

    /// <summary>
    /// Base class for a game controller
    /// </summary>
    public abstract class BaseGameController : InputEventGenerator, IGameController
    {


        public const int MaxActions = 128;

        List<KeyInfo> keys;
        int numActions;

        Dictionary<string, int> keyNameToIndex;
        Dictionary<string, int> actionNameToIndex;

        float[] lastActionState;
        float[] currentActionState;
        ActionInfo[] actionInfo;

        int playerIndex;

        Cursor cursor;

        IServiceProvider services;
        IInputService input;
        IRender2DService render2D;

        //Preallocate some events so we don't reallocate them every time
        MouseEventArgs moveEventArgs = new MouseEventArgs();
        MouseEventArgs clickEventArgs = new MouseEventArgs();
        MouseEventArgs unClickEventArgs = new MouseEventArgs();
        MenuEventArgs menuArgs = new MenuEventArgs();
        ControllerChangedEventArgs changeEventArgs = new ControllerChangedEventArgs();

        //Width and height of viewport
        int width;
        int height;
        int xOffset;
        int yOffset;

        public static int cursorXAction;
        public static int cursorYAction;
        public static int cursorXDeltaAction;
        public static int cursorYDeltaAction;

        IAnarchyGamer gamer;
        bool isDirty = false;
        float sensitivity = 0.5f;



        float maxThreshold = 0.85f;
        public float Threshold
        {
            get 
            {
                //return 0.95f;
                return maxThreshold - sensitivity * 0.2f;
            }
        }

        public float Sensitivity
        {
            get { return sensitivity; }
            set
            {
                this.sensitivity = value;
                if (sensitivity < 0f)
                    sensitivity = 0f;
                else if (sensitivity > 1f)
                    sensitivity = 1f;
            }

        }

        protected int ViewportWidth
        {
            get { return width; }
        }
        protected int ViewportHeight
        {
            get { return height; }
        }
        protected int ViewportOffsetX
        {
            get { return xOffset; }
        }
        protected int ViewportOffsetY
        {
            get { return yOffset; }
        }

        protected IServiceProvider Services
        {
            get { return services; }
        }
        protected IInputService Input
        {
            get { return input; }
        }
        protected IRender2DService Render2D
        {
            get { return render2D; }
        }

        protected IList<KeyInfo> Keys
        {
            get { return keys; }
        }
        
        public int PlayerIndex
        {
            get { return playerIndex; }
            set { playerIndex = value; }
        }

        public virtual bool Connected
        {
            get { return true; }
        }

        public Point MouseDelta
        {
            get
            {
                return new Point((int)GetDeltaFromActionIndex(cursorXAction), (int)GetDeltaFromActionIndex(cursorYAction));
            }
        }

        public Point MousePos
        {
            get
            {
                return new Point((int)GetCurrentFromActionIndex(cursorXAction), (int)GetCurrentFromActionIndex(cursorYAction));
            }
        }

        public IAnarchyGamer Gamer
        {
            get { return gamer; }
            set { gamer = value; }
        }

        public Cursor Cursor
        {
            get { return cursor; }
        }

        void UpdateViewport()
        {
            IGraphicsDeviceService graphicsService = (IGraphicsDeviceService)services.GetService(typeof(IGraphicsDeviceService));
            
            width = graphicsService.GraphicsDevice.Viewport.Width;
            height = graphicsService.GraphicsDevice.Viewport.Height;
            xOffset = graphicsService.GraphicsDevice.Viewport.X;
            yOffset = graphicsService.GraphicsDevice.Viewport.Y;
        }


        //protected float[] CurrentActionState
        //{
        //    get { return currentActionState; }
        //}

        protected void SetCurrentActionState(int index, float value)
        {
            //This logic is saying, we only want to update if the value is greater than the current value,
            //or if we have not yet updated this round
            if (value > currentActionState[index] || !actionInfo[index].Dirty)
            {
                isDirty = true;
                currentActionState[index] = value;
                actionInfo[index].Dirty = true;
            }
        }


        protected BaseGameController(IServiceProvider inServices, ContentManager content, int inPlayerIndex)
        {
            playerIndex = inPlayerIndex;
            services = inServices;
            input = (IInputService)Services.GetService(typeof(IInputService));
            render2D = (IRender2DService)Services.GetService(typeof(IRender2DService));

            keys = new List<KeyInfo>();

            keyNameToIndex = new Dictionary<string, int>();
            actionNameToIndex = new Dictionary<string, int>();

            lastActionState = new float[MaxActions];
            currentActionState = new float[MaxActions];
            actionInfo = new ActionInfo[MaxActions];

            for (int i = 0; i < MaxActions; i++)
            {
                lastActionState[i] = 0.0f;
                currentActionState[i] = 0.0f;
                actionInfo[i] = new ActionInfo();
            }

            UpdateViewport();

            cursor = new Cursor(new DefaultCursorScheme(content));
            cursor.Visible = false;

            //Add default actions
            //TODO: Use integers for all these actions!
            cursorXAction = AddAction("cursor_x");
            cursorYAction = AddAction("cursor_y");
            AddAction("cursor_leftclick");
            AddAction("cursor_rightclick");

            float initialTime = 0.3f;
            float repeatTime = 0.1f;

            AddAction("menu_up", true, initialTime, repeatTime);
            AddAction("menu_down", true, initialTime, repeatTime);
            AddAction("menu_left", true, initialTime, repeatTime);
            AddAction("menu_right", true, initialTime, repeatTime);
            AddAction("menu_select");
            AddAction("menu_back");


        }

        public virtual void SetRumble(float leftMotor, float rightMotor)
        {
            return;
        }

        /// <summary>
        /// Defines a key that the controller can use. This key can then be bound
        /// to a control.
        /// </summary>
        /// <param name="keyName"></param>
        /// <returns></returns>
        protected int AddKey(string keyName)
        {
            keys.Add(new KeyInfo(keyName));
            int index = keys.Count - 1;
            keyNameToIndex[keyName] = index;
            return index;
        }

        public int AddAction(string actionName)
        {
            return this.AddAction(actionName, false, 0f, 0f);
        }

        public int AddAction(string actionName, bool allowRepeat, float initialTime, float repeatTime)
        {
            if (numActions >= MaxActions)
                return -1;

            if (actionNameToIndex.ContainsKey(actionName))
                return actionNameToIndex[actionName];



            actionNameToIndex[actionName.ToLower(CultureInfo.CurrentCulture)] = numActions;
            int index = numActions;
            actionInfo[index].AllowRepeat = allowRepeat;
            actionInfo[index].NextRepeat = repeatTime;
            actionInfo[index].RepeatTime = repeatTime;
            actionInfo[index].BaseTime = initialTime;
            actionInfo[index].CurrentBaseTime = initialTime;

            numActions++;
            return index;
        }

        protected KeyInfo GetKeyInfoFromName(string keyName)
        {
            return keys[keyNameToIndex[keyName]];
        }

        public void Bind(string key, string actionName)
        {
            KeyInfo k = GetKeyInfoFromName(key.ToLower(CultureInfo.CurrentCulture));
            //k.ActionIndex = actionNameToIndex[actionName];
            k.AddAction(actionNameToIndex[actionName]);
        }

        public void Unbind(string keyName)
        {
            KeyInfo k = GetKeyInfoFromName(keyName.ToLower(CultureInfo.CurrentCulture));
            //k.ActionIndex = -1;
            k.UnbindAll();
        }

        public void ClearPressed(string name)
        {
            ClearPressed(GetActionIndex(name));
        }

        public void ClearPressed(int actionIndex)
        {
            SetCurrentActionState(actionIndex, 0.0f);
        }

        protected int GetActionIndex(string actionName)
        {
            return actionNameToIndex[actionName];
        }

        protected float GetDeltaFromActionIndex(int index)
        {
            return GetCurrentFromActionIndex(index) - GetLastFromActionIndex(index);
        }

        //protected float GetDeltaFromActionName(string actionName)
        //{
        //    return GetDeltaFromActionIndex(GetActionIndex(actionName));
        //    //return GetCurrentFromActionName(actionName) - GetLastFromActionName(actionName);
        //}


        protected float GetCurrentFromActionIndex(int actionIndex)
        {
            return currentActionState[actionIndex];
        }
        //protected float GetCurrentFromActionName(string actionName)
        //{
        //    return GetCurrentFromActionIndex(GetActionIndex(actionName));
        //}

        protected float GetLastFromActionIndex(int actionIndex)
        {
            return lastActionState[actionIndex];
        }
        //protected float GetLastFromActionName(string actionName)
        //{
        //    return GetLastFromActionIndex(GetActionIndex(actionName)); 
        //}

        /// <summary>
        /// If the key passed in is bound to an action, sets the value of
        /// the action to val. Otherwise, does nothing.
        /// </summary>
        protected void SetValue(string keyName, float value)
        {
            KeyInfo k = GetKeyInfoFromName(keyName);

            //if (k.ActionIndex != -1)
            //{
            //    SetCurrentActionState(k.ActionIndex, value);
            //    //CurrentActionState[k.ActionIndex] = value;
            //}
            for(int i = 0; i < k.Actions.Count; i++)
                SetCurrentActionState(k.Actions[i], value);
        }

        protected static float BoolToValue(bool value)
        {
            if (value == true)
                return 1.0f;
            else
                return 0.0f;
        }

        protected static float ButtonToValue(ButtonState state)
        {
            if (state == ButtonState.Pressed)
                return 1.0f;
            else
                return 0.0f;
        }

        public float GetValue(int actionIndex)
        {
            return GetCurrentFromActionIndex(actionIndex);
        }
        public float GetValue(string actionName)
        {
            return GetValue(GetActionIndex(actionName)); ;
        }

        public bool IsKeyDown(int actionIndex)
        {
            return GetValue(actionIndex) > Threshold;
        }
        public bool IsKeyDown(string actionName)
        {
            return IsKeyDown(GetActionIndex(actionName));
            //return GetValue(actionName) > Threshold;
        }

        public bool IsKeyJustPressed(int actionIndex)
        {
            bool justPressed = IsKeyDown(actionIndex) && GetLastFromActionIndex(actionIndex) < Threshold;

            if (!justPressed && IsKeyDown(actionIndex))
            {
                //See if the time has expired
                if (actionInfo[actionIndex].AllowRepeat && actionInfo[actionIndex].NextRepeat <= 0.0f)
                {
                    return true;
                }
            }

            return justPressed;
        }
        public bool IsKeyJustPressed(string actionName)
        {
            return IsKeyJustPressed(GetActionIndex(actionName));
            //return IsKeyDown(actionName) && GetLastFromActionName(actionName) < Threshold;
        }

        public bool IsKeyJustReleased(int actionIndex)
        {
            return !IsKeyDown(actionIndex) && GetLastFromActionIndex(actionIndex) > Threshold;
        }
        public bool IsKeyJustReleased(string actionName)
        {
            return IsKeyJustReleased(GetActionIndex(actionName));
            //return !IsKeyDown(actionName) && GetLastFromActionName(actionName) > Threshold;
        }

        public void Update(GameTime gameTime)
        {
            //Copy old values to last action state
            for (int i = 0; i < currentActionState.Length; i++)
            {
                lastActionState[i] = currentActionState[i];
                //Clear the dirty bits of each individual action
                actionInfo[i].Dirty = false;
            }

            isDirty = false;

            for (int i = 0; i < numActions; i++)
            {
                if (actionInfo[i].AllowRepeat)
                {
                    if (!IsKeyDown(i))
                    {
                        actionInfo[i].CurrentBaseTime = actionInfo[i].BaseTime;
                    }
                    else if (!IsKeyJustPressed(i) && IsKeyDown(i))
                    {
                        float subtract = (float)gameTime.ElapsedGameTime.TotalSeconds;

                        if (actionInfo[i].CurrentBaseTime > 0.0f)
                            actionInfo[i].CurrentBaseTime -= subtract;
                        else
                            actionInfo[i].NextRepeat -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                    }
                    else if (IsKeyJustPressed(i))
                    {
                        actionInfo[i].NextRepeat = actionInfo[i].RepeatTime;
                    }
                }
            }

            //Call virtual controller update function
            ControllerUpdate(gameTime);

            bool signedIn = false;

            //Check if we have a gamer associated with this, if not, then bail on the events
            if (AnarchyGamer.Gamers.GetGamerByPlayerIndex(this.playerIndex) != null)
                signedIn = true;

            //Don't bother firing events if we're not active
            if (!SxeGame.GetGameInstance.IsActive)
                return;
            
            moveEventArgs.Reset();
            unClickEventArgs.Reset();
            clickEventArgs.Reset();
            menuArgs.Reset();



            //Lots of duplicate code in these
            if (isDirty)
            {
                changeEventArgs.IsSignedIn = signedIn;
                changeEventArgs.PlayerIndex = this.PlayerIndex;
                FireEvent(changeEventArgs);
            }


            //Only create mouse events if the cursor is visible
            if (this.Cursor.Visible)
            {
                if (Math.Abs(MouseDelta.X) > 0 || Math.Abs(MouseDelta.Y) > 0)
                {
                    moveEventArgs.Position = MousePos;
                    moveEventArgs.MouseEventType = MouseEventType.Move;
                    moveEventArgs.LeftButtonPressed = IsKeyDown("cursor_leftclick");
                    moveEventArgs.PlayerIndex = this.PlayerIndex;
                    moveEventArgs.IsSignedIn = signedIn;
                    FireEvent(moveEventArgs);
                }

                if (IsKeyJustPressed("cursor_leftclick"))
                {
                    clickEventArgs.Position = MousePos;
                    clickEventArgs.MouseEventType = MouseEventType.Click;
                    clickEventArgs.PlayerIndex = this.PlayerIndex;
                    clickEventArgs.IsSignedIn = signedIn;
                    FireEvent(clickEventArgs);
                }

                if (IsKeyJustReleased("cursor_leftclick"))
                {
                    unClickEventArgs.Position = MousePos;
                    unClickEventArgs.MouseEventType = MouseEventType.Unclick;
                    unClickEventArgs.PlayerIndex = this.PlayerIndex;
                    unClickEventArgs.IsSignedIn = signedIn;
                    FireEvent(unClickEventArgs);
                }
            }

            menuArgs.PlayerIndex = playerIndex;
            menuArgs.IsSignedIn = signedIn;
            if (IsKeyJustPressed("menu_up"))
            {
                menuArgs.MenuEventType = MenuEventType.Up;
                FireEvent(menuArgs);
            }

            if (IsKeyJustPressed("menu_down"))
            {
                menuArgs.MenuEventType = MenuEventType.Down;
                FireEvent(menuArgs);
            }

            if (IsKeyJustPressed("menu_left"))
            {
                menuArgs.MenuEventType = MenuEventType.Left;
                FireEvent(menuArgs);
            }

            if (IsKeyJustPressed("menu_right"))
            {
                menuArgs.MenuEventType = MenuEventType.Right;
                FireEvent(menuArgs);
            }

            if (IsKeyJustPressed("menu_select"))
            {
                menuArgs.MenuEventType = MenuEventType.Select;
                FireEvent(menuArgs);
            }

            if (IsKeyJustPressed("menu_back"))
            {
                menuArgs.MenuEventType = MenuEventType.Back;
                FireEvent(menuArgs);
            }
        }

        public abstract void ControllerUpdate(GameTime gameTime);

        public virtual void Draw(GameTime gameTime)
        {
            if (cursor != null)
            {
                cursor.Position = new Point((int)GetValue("cursor_x"), (int)GetValue("cursor_y"));

                cursor.Draw(this.Render2D);
            }
        }







    }
}
