using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Sxe.Engine.Input;

#if !XBOX
using System.ComponentModel.Design;
using Sxe.Design;
#endif

namespace Sxe.Engine.UI
{

    enum PanelMode
    {
        None = 0,
        Show,
        Hide
    }


    /// <summary>
    /// Base class for a container of panels
    /// </summary>
#if !XBOX
    [Designer(typeof(PanelContainerDesigner), typeof(IDesigner))]
    [Designer(typeof(PanelContainerRootDesigner), typeof(IRootDesigner))]
#endif
    public class PanelContainer : Panel
    {
        #region Static
        private static InputFilterMode defaultFilterMode = InputFilterMode.AllowSignedIn;
        public static InputFilterMode DefaultFilterMode
        {
            get { return defaultFilterMode; }
            set { defaultFilterMode = value; }
        }
        #endregion


        //Keep track of both the control that currently has the mouse over,
        //and the control that has focus
        Panel overControl;
        //Panel activeControl;
        PanelMode mode = PanelMode.None;
        float transitionPosition = 0.0f;
        TimeSpan transitionOnTime = TimeSpan.FromSeconds(0.5);
        TimeSpan transitionOffTime = TimeSpan.FromSeconds(0.5);
        //By default, we only allow signed in players
        InputFilterMode filterMode = InputFilterMode.AllowSignedIn;
        List<int> allowedPlayers = new List<int>();

        /// <summary>
        /// We'll keep a dictionary from PlayerIndex to their activeControl
        /// </summary>
        Dictionary<int, Panel> activeControls = new Dictionary<int, Panel>(); 

        //Point drawOffset = Point.Zero;
        //public Point DrawOffset
        //{
        //    get { return drawOffset; }
        //    set { drawOffset = value; }
        //}

        public InputFilterMode InputFilterMode
        {
            get { return filterMode; }
            set { filterMode = value; }
        }

        public List<int> AllowedPlayers
        {
            get { return allowedPlayers; }
        }


        Vector2 scale = Vector2.One;
#if !XBOX
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
#endif
        public Vector2 Scale
        {
            get { return scale; }
            set { scale = value; }
        }

#if !XBOX
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
#endif
        public Panel ActiveControl
        {
            //get { return activeControl; }
            set 
            {
                //HACK:
                //A lot of times when this is called, gamers havent been added to colleciton yet!
                for (int i = 0; i < Math.Max(5, AnarchyGamer.Gamers.Count); i++)
                {
                    if (!this.activeControls.ContainsKey(i))
                        this.activeControls.Add(i, null);

                    this.activeControls[i] = value; 
                }
            }
        }

            /// <summary>
            /// Indicates how long the screen takes to
            /// transition on when it is activated.
            /// </summary>
#if !XBOX
            [TypeConverter(typeof(Sxe.Design.TimeSpanConverter))]
#endif
            public TimeSpan TransitionOnTime
        {
            get { return transitionOnTime; }
            set { transitionOnTime = value; }
        }

        /// <summary>
        /// Indicates how long the screen takes to
        /// transition off when it is deactivated.
        /// </summary>
#if !XBOX
        [TypeConverter(typeof(Sxe.Design.TimeSpanConverter))]
#endif
        public TimeSpan TransitionOffTime
        {
            get { return transitionOffTime; }
            set { transitionOffTime = value; }
        }

        PanelCollection panelCollection;

#if !XBOX
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
#endif
        //[Browsable(false)]
        public PanelCollection Panels
        {
            get { return panelCollection; }
        }

        public override float TransitionPosition
        {
            get
            {
                if (mode == PanelMode.None)
                    return base.TransitionPosition;
                else if (mode == PanelMode.Hide)
                    return -transitionPosition;
                else
                    return transitionPosition;
            }
            set { transitionPosition = value; }
        }

        #region Events
        public event EventHandler PanelAdded;
        public event EventHandler PanelRemoved;

        #endregion
        public PanelContainer()
        {
            panelCollection = new PanelCollection(this);
            Size = new Point(800, 600);
        }

        public override void Activate()
        {
            for (int i = 0; i < panelCollection.Count; i++)
                panelCollection[i].Activate();

            base.Activate();
        }

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            //Draw all UI elements
            spriteBatch.Begin();
            Paint(spriteBatch, Point.Zero, Scale);
            //for (int i = 0; i < Panels.Count; i++)
            //{
            //    Panels[i].Paint(spriteBatch, DrawOffset, Scale);
            //}
            
            spriteBatch.End();

            

        }

        public override void RecalculateAbsoluteLocation()
        {
            base.RecalculateAbsoluteLocation();

            if (Panels != null)
            {
                for (int i = 0; i < Panels.Count; i++)
                    Panels[i].RecalculateAbsoluteLocation();
            }
        }

        public override void Paint(SpriteBatch sb, Point positionOffset, Vector2 scale)
        {


            base.Paint(sb, positionOffset, scale);

            if (!Visible)
                return;

            Vector2 transitionAmount = GetTransitionAmount();
            Point adjustedOffset = new Point(
                (int)(positionOffset.X + Location.X + transitionAmount.X), 
                (int)(positionOffset.Y + Location.Y + transitionAmount.Y));

            //Draw children panels
            for (int i = 0; i < Panels.Count; i++)
                Panels[i].Paint(sb, adjustedOffset, scale);
        }

        /// <summary>
        /// Helper for updating the screen transition position.
        /// </summary>
        protected bool UpdateTransition(GameTime gameTime, TimeSpan time, int direction)
        {
            // How much should we move by?
            float transitionDelta;

            if (time == TimeSpan.Zero)
                transitionDelta = 1;
            else
                transitionDelta = (float)(gameTime.ElapsedGameTime.TotalMilliseconds /
                                          time.TotalMilliseconds);

            //TransitionPosition += transitionDelta*direction; is broken -- see below comments
            TransitionPosition = Math.Abs(TransitionPosition) + transitionDelta *direction;

            //So this is working now. The code is not very readable and could stand for a design change.
            //The problem is - we need to know whether a transition is on or off, because we may
            //want to transition differently. We do that by making TransitionPosition return negative
            //if the transition is off, and positive otherwise. This is silly though because the underlying
            //transitionPosition is always between 0 and 1, and the setter reflects that

            //The design challenge is that we really have two types of transitions - GameScreens have
            //transition behavior when activating and exiting, and PanelContainers have Show() and Hide()
            //behavior. They both use this UpdateTransition function and TransitionPosition, but have
            //different underlying TransitionPosition variables
            
            // Did we reach the end of the transition?
            if ((TransitionPosition >= 0 && direction > 0) ||
                (TransitionPosition <= 0 && direction < 0) || (Math.Abs(TransitionPosition) >= 1))
            {
                TransitionPosition = MathHelper.Clamp(TransitionPosition, 0, 1);
                return false;
            }

            // Otherwise we are still busy transitioning.
            return true;
        }

        public virtual void Show()
        {
            mode = PanelMode.Show;
            Visible = true;
            transitionPosition = 1.0f;
        }

        public virtual void Hide()
        {
            mode = PanelMode.Hide;
        }

        public override void Update(GameTime gameTime)
        {
            //Update transitions
            if (mode == PanelMode.Show)
            {
                if (!UpdateTransition(gameTime, this.TransitionOnTime, -1))
                {
                    mode = PanelMode.None;
                }
            }
            else if (mode == PanelMode.Hide)
            {
                if (!UpdateTransition(gameTime, this.TransitionOffTime, 1))
                {
                    mode = PanelMode.None;
                    Visible = false;
                    transitionPosition = 0.0f;
                }
            }
            else
                transitionPosition = 0.0f;


            for (int i = 0; i < Panels.Count; i++)
            {
                Panels[i].Update(gameTime);
            }
            base.Update(gameTime);
        }


        public override void LoadContent(ContentManager content)
        {
            for (int i = 0; i < Panels.Count; i++)
                Panels[i].LoadContent(content);

            base.LoadContent(content);
        }

        public override void UnloadContent()
        {
            for (int i = 0; i < Panels.Count; i++)
                Panels[i].UnloadContent();
            base.UnloadContent();
        }

        protected bool DoesEventPassFilter(InputEventArgs inputEvent)
        {
            //If a player is not signed in, but we only allow signed in players, don't handle it
            if (this.filterMode == InputFilterMode.AllowSignedIn && inputEvent.IsSignedIn == false)
                return false;

            if (this.filterMode == InputFilterMode.AllowSpecified && !this.allowedPlayers.Contains(inputEvent.PlayerIndex))
                return false;

            return true;
        }

        public override bool HandleEvent(Sxe.Engine.Input.InputEventArgs inputEvent)
        {
            //We loop backwards through the Panels collection for handling input,
            //because the higher panels are drawn on top of the lower ones
            //This gives us automatic ordering control

            if (!this.Visible)
                return false;

            if (!DoesEventPassFilter(inputEvent))
                return false;


            MouseEventArgs mouseEvent = inputEvent as MouseEventArgs;
            KeyEventArgs keyEvent = inputEvent as KeyEventArgs;
            MenuEventArgs menuEvent = inputEvent as MenuEventArgs;

            int playerIndex = inputEvent.PlayerIndex;
            Panel activeControl = null;
            if (activeControls.ContainsKey(inputEvent.PlayerIndex))
                activeControl = activeControls[playerIndex];

            if (mouseEvent != null)
            {
                //If the event is a mouse move event, figure out which control we are over
                //If it is different, fire the appropriate mouse leave/enter events

                if (mouseEvent.MouseEventType == MouseEventType.Move)
                {
                    Panel over = null;
                    for (int i = Panels.Count - 1; i >= 0; i--)
                    {

                        if (Panels[i].IsPointInside(mouseEvent.Position))
                        {
                            over = Panels[i];
                            break;
                        }
                    }

                    //If the control we are over is not the same as the one we were over, fire vents
                    if (over != overControl)
                    {
                        if (overControl != null)
                            overControl.InvokeMouseLeave(this, mouseEvent);

                        overControl = over;
                        if(overControl != null)
                            overControl.InvokeMouseEnter(this, mouseEvent);
                    }
                }
                else if (mouseEvent.MouseEventType == MouseEventType.Unclick)
                {
                    for (int i = 0; i < Panels.Count; i++)
                    {
                        Panels[i].HandleEvent(mouseEvent);
                    }
                }
                //If it is a click, we need to loop through all the objects that weren't
                //clicked, and fire there mouse click outside event
                else if (mouseEvent.MouseEventType == MouseEventType.Click)
                {
                    bool foundFirst = false;
                    //    //We'll actually do two things at once - see if a control needs focus
                    //    //And fire the mouse click outside event otherwise

                    for (int i = Panels.Count - 1; i >= 0; i--)
                    {
                        Panel panel = Panels[i];


                        if (!Panels[i].IsPointInside(mouseEvent.Position))
                            Panels[i].InvokeMouseClickOutside(this, mouseEvent);
                        else if (!foundFirst)
                        {
                            Panel newActive = Panels[i];


                            if (activeControl != newActive)
                            {
                                if (activeControl != null)
                                    activeControl.InvokeLostFocus(this, EventArgs.Empty);

                                activeControl = newActive;

                                if (activeControl != null)
                                    activeControl.InvokeGotFocus(this, EventArgs.Empty);

                                if (!activeControls.ContainsKey(playerIndex))
                                    activeControls.Add(playerIndex, activeControl);
                                else
                                    activeControls[playerIndex] = activeControl;
                            }



                            foundFirst = true;
                        }



                    }

                    bool activeControlHandled = false;
                    if (activeControl != null)
                        activeControlHandled = activeControl.HandleEvent(inputEvent);

                    if (!activeControlHandled)
                        return base.HandleEvent(inputEvent);
                    else
                        return true;
                }
            }
            //If this is a key event, only send it to the active control
            else if(keyEvent != null)
            {
                //We'll simply return true if the active control handled it
                //Otherwise we'll return whether we handled it or not
                bool handled = false;
                if(activeControl != null)
                    handled = activeControl.HandleEvent(keyEvent);

                if(handled)
                    return true;

                return base.HandleEvent(inputEvent);
            }
            else if (menuEvent != null)
            {
                bool handled = false;
                if (activeControl != null)
                    handled = activeControl.HandleEvent(menuEvent);

                if (handled)
                    return true;

                return base.HandleEvent(inputEvent);
            }

            //We need to loop through and give our controls a chance at the event
            //Again, we go backwards
            for(int i = Panels.Count - 1; i >= 0; i--)
            {
                bool handled = Panels[i].HandleEvent(inputEvent);
                if(handled)
                    return true;
            }

            //If none of our panels handed it, we'll try and take care of it...
            return base.HandleEvent(inputEvent);


            
        }

        public class PanelCollection : IList
        {
            ArrayList panels = new ArrayList();
            PanelContainer parent;

            public PanelCollection(PanelContainer inParent)
            {
                parent = inParent;

            }

            object IList.this[int index]
            {
                get { return panels[index]; }
                set { panels[index] = value; }
            }

            public Panel this[int index]
            {
                get { return (Panel)panels[index]; }
                set { panels[index] = (Panel)value; }
            }

            public int Count
            {
                get { return panels.Count; }
            }

            public bool IsReadOnly
            {
                get { return false; }
            }

            bool IList.IsFixedSize
            {
                get { return panels.IsFixedSize; }
            }

            public void CopyTo(object[] panels, int index)
            {
                panels.CopyTo(panels, index);

            }

            public void Remove(Panel panel)
            {

                panels.Remove(panel);
                OnRemove();
            }


            IEnumerator IEnumerable.GetEnumerator()
            {
                return panels.GetEnumerator();
            }


            //public IEnumerator GetEnumerator()
            //{
            //    return panels.GetEnumerator();
            //}

            public int Add(object panel)
            {
                int position = panels.Count;
                Insert(position, panel);
                return position;

            }

            int IList.IndexOf(object panel)
            {
                return panels.IndexOf(panel);
            }


            void IList.Remove(object panel)
            {
                OnRemove();
                panels.Remove(panel);
            }

            object ICollection.SyncRoot
            {
                get { return panels.SyncRoot; }
            }

            bool ICollection.IsSynchronized
            {
                get { return panels.IsSynchronized; }
            }

            void ICollection.CopyTo(System.Array array, int index)
            {
                panels.CopyTo(array, index);
            }

            public void Clear()
            {
                panels.Clear();
            }

            public int IndexOf(Panel panel)
            {
                return panels.IndexOf(panel);
            }

            public void Insert(int index, object obj)
            {
                Panel panel = obj as Panel;

                if (panel == null)
                    return;

                if (panel.Parent != null && panel.Parent != this.parent)
                {
                    PanelContainer parentContainer = panel.Parent as PanelContainer;
                    if (parentContainer != null)
                        parentContainer.Panels.Remove(panel);
                }


                if (!panels.Contains(panel))
                    panels.Insert(index, panel);

                OnAdded();

                panel.Parent = parent;
            }

            public void RemoveAt(int index)
            {
                panels.RemoveAt(index);
                OnRemove();
            }

            public bool Contains(object panel)
            {
                return panels.Contains(panel);
            }

            public override string ToString()
            {
                string output = "List:" + Environment.NewLine;
                for (int i = 0; i < panels.Count; i++)
                {
                    output += panels[i].GetType().ToString() + Environment.NewLine;
                }

                return output;

                //return panels.ToString();
                //return base.ToString();
            }

            void OnAdded()
            {
                if (parent != null)
                {
                    if (parent.PanelAdded != null)
                        parent.PanelAdded(parent, EventArgs.Empty);
                }
            }

            void OnRemove()
            {
                if (parent != null)
                {
                    if (parent.PanelRemoved != null)
                        parent.PanelRemoved(parent, EventArgs.Empty);
                }
            }

        }
    }


}
