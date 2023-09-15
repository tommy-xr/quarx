using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using System.ComponentModel;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

using Sxe.Engine.Input;
using Sxe.Engine.Utilities;
using Sxe.Library.Utilities;
using Sxe.Library.UI;

#if !XBOX
using System.ComponentModel.Design;
using Sxe.Design;
using System.Drawing.Design;
#endif


namespace Sxe.Engine.UI
{



    /// <summary>
    /// Base class for all user interface elements
    /// </summary>
#if !XBOX
    [ToolboxItemFilter("AnarchyUI", ToolboxItemFilterType.Require)]
    [Designer(typeof(PanelDesigner), typeof(IDesigner))]
#endif
    public class Panel : Component, IInputEventReceiver, IVisible
    {
        #region Static Members
        static IServiceProvider services;
        public static IServiceProvider Services
        {
            get { return services; }
            set { services = value; }
        }
        static AudioManager audio;
        public static AudioManager Audio
        {
            get { return audio; }
            set { audio = value; }
        }

        static readonly Point DefaultSize = new Point(50, 50);
        static readonly Point DefaultLocation = new Point(0, 0);

        #endregion

        #region Private Members
        UIImage backImage; //the image for this UI element
        Panel parent; //the parent of this UI element
        Color backColor; //the color of this UI element
        Point size; //the size of this UI element
        Point location; //the position of this UI element, relative to parent
        Point absoluteLocation; //the actual position of this UI element, non relative
        bool visible; //whether this UI element is visible or not
        bool enabled;
        bool previousMouseIn; //true if the mouse was in the control last frame, false otherwise
        bool previousMousePress; //true if the mouse was pressed, false otherwise
        GraphicsDevice device;
        IInputService input;
        bool invalidated = true;
        string name = ""; //name of panel
        IScheme scheme;
        Point transitionOnOffset = Point.Zero;
        Point transitionOffOffset = Point.Zero;
        Vector2 transitionOnScale = Vector2.One;
        Vector2 transitionOffScale = Vector2.One;
        #endregion

        #region Events
        public event EventHandler<MouseEventArgs> MouseEnter;
        public event EventHandler<MouseEventArgs> MouseLeave;
        public event EventHandler<MouseEventArgs> MouseClick;
        public event EventHandler<MouseEventArgs> MouseClickRelease;
        public event EventHandler<MouseEventArgs> MouseClickOutside;
        public event EventHandler<MouseEventArgs> MouseDown;
        public event EventHandler<MouseEventArgs> MouseDoubleClick;
        public event EventHandler GotFocus;
        public event EventHandler LostFocus;
        public event EventHandler<KeyEventArgs> KeyPress;
        public event EventHandler VisibleChanged;
        public event EventHandler PositionChanged;
        public event EventHandler SizeChanged;
        public event EventHandler ParentChanged;
        #endregion

        #region Event Invokers
        public void InvokeMouseLeave(object sender, MouseEventArgs args)
        {
            if (MouseLeave != null)
                MouseLeave(sender, args);
        }
        public void InvokeMouseEnter(object sender, MouseEventArgs args)
        {
            if (MouseEnter != null)
                MouseEnter(sender, args);
        }
        public void InvokeMouseClickOutside(object sender, MouseEventArgs args)
        {
            if (MouseClickOutside != null)
                MouseClickOutside(sender, args);
        }
        public void InvokeGotFocus(object sender, EventArgs args)
        {
            if (GotFocus != null)
                GotFocus(sender, args);
        }
        public void InvokeLostFocus(object sender, EventArgs args)
        {
            if(LostFocus != null)
                LostFocus(sender, args);
        }

        #endregion

        #region Public Properties
        public Panel Parent
        {
            get { return this.parent; }
            set { SetParent(value); }
        }

        //public virtual bool CanFocus
        //{
        //    get { return true; }
        //}

        public Point TransitionOnOffset
        {
            get { return transitionOnOffset; }
            set { transitionOnOffset = value; }
        }

        public Point TransitionOffOffset
        {
            get { return transitionOffOffset; }
            set { transitionOffOffset = value; }
        }

#if !XBOX
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
#endif
        public byte TransitionAlpha
        {
            get { return (byte)(255 - 255 * Math.Abs(TransitionPosition)); }
        }

#if !XBOX
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
#endif
        public virtual float TransitionPosition
        {
            get
            {
                if (parent == null)
                    return 0.0f;
                else
                    return parent.TransitionPosition;
            }
            set { }
        }

#if !XBOX
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
#endif
        public bool Invalidated
        {
            get { return invalidated; }
            set { invalidated = value; }
        }

        string backgroundPath;
        //[Editor(typeof(System.Windows.Forms.Design.FileNameEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [ReloadContent]
#if !XBOX
        [DefaultValue("")]
        //[Editor(typeof(UIImageTypeEditor), typeof(UITypeEditor))]
        [TypeConverter(typeof(TextureContentNameConverter))]
#endif
        public string BackgroundPath
        {
            get { return backgroundPath; }
            set { backgroundPath = value; }
        }

        [DefaultValue("")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
#if !XBOX
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
#endif
        public string FullName
        {
            get
            {
                if (parent == null)
                    return name;
                else
                    return parent.FullName + "." + name;
            }
        }

        [DefaultValue(true)]
        public bool Visible
        {
            get { return visible; }
            set 
            { 
                visible = value;
                
                if (VisibleChanged != null)
                    VisibleChanged(this, null);
            }
        }
        //public bool HasFocus
        //{
        //    get
        //    {
        //        if (parent == null)
        //            return true;

        //        if (parent.ActivePanel == this && parent.HasFocus)
        //            return true;

        //        return false;
        //    }
        //}

        [DefaultValue(true)]
        public bool Enabled
        {
            get 
            {
                bool isEnabled = enabled;
                if (parent != null)
                    isEnabled = isEnabled && parent.Enabled;
                return isEnabled;
            }
            set { enabled = value; }
        }

#if !XBOX
        [DefaultValue(typeof(Color), "255,255,255,255")]
#endif
        public Color BackColor
        {
            get { return this.backColor; }
            set { this.backColor = value; }
        }
        public IScheme Scheme
        {
            get { return scheme; }
        }

#if !XBOX
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
#endif
        public UIImage Image
        {
            get { return this.backImage; }
            set { this.backImage = value; }
        }

        public Point Size
        {
            get { return size; }
            set 
            { 
                size = value;

                if (SizeChanged != null)
                    SizeChanged(this, null);
            }
        }

#if !XBOX
        [RefreshProperties(RefreshProperties.All)]
#endif
        public Point Location
        {
            get { return location; }
            set 
            { 
                location = value;
                RecalculateAbsoluteLocation();

                //Fire location changed event
                if (PositionChanged != null)
                    PositionChanged(this, null);
            }
        }

#if !XBOX
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
#endif
        public Point AbsoluteLocation
        {
            get { return absoluteLocation; }
        }

        public virtual void Invalidate()
        {
            this.invalidated = true;
            if (parent != null)
                parent.Invalidate();
        }

        protected IInputService Input
        {
            get 
            { 
                if(input == null)
                {
                    input = (IInputService)Services.GetService(typeof(IInputService));
                }
                return input;
            }
        }
        protected GraphicsDevice Device
        {
            get
            {
                if (device == null)
                {
                    device = ((IGraphicsDeviceService)Services.GetService(typeof(IGraphicsDeviceService))).GraphicsDevice;
                }
                return device;
            }
        }


        //public virtual void ApplyScheme(IScheme scheme)
        //{
        //    this.scheme = scheme;

        //    for (int i = 0; i < children.Count; i++)
        //        children[i].ApplyScheme(scheme);
        //}
#if !XBOX
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
#endif
        public virtual bool IsDragging { get { return false; } }

        [DefaultValue(false)]
        public virtual bool CanDrag { get { return false; } set { } }

        #endregion

        public virtual void RecalculateAbsoluteLocation()
        {
            absoluteLocation = location;
            if (parent != null)
            {
                absoluteLocation = new Point(location.X + parent.AbsoluteLocation.X, location.Y + parent.AbsoluteLocation.Y);
            }
            //If our absolute location changed, then our childrens absolute locations did as well
            //foreach (Panel p in children)
            //{
            //    p.RecalculateAbsoluteLocation();
            //}
        }

        void HandleMove(object value, EventArgs args)
        {
            RecalculateAbsoluteLocation();
        }

        #region Constructors

        public Panel()
            : this(null)
        {
        }

        public Panel(IScheme scheme)
        {
            //children = new List<Panel>();
            //SetParent(inParent);
            //this.Location = inPos;
            //this.Size = inSize;
            //backImage = inImage;
            this.Location = DefaultLocation;
            this.Size = DefaultSize;
            backColor = Color.White;
            visible = true;
            enabled = true;
            //previousMouseIn = false;
            //previousMousePress = false;
            //SetDrawOrder(0.0f);

            //if(scheme != null)
            //ApplyScheme(scheme);

            this.PositionChanged += HandleMove;
        }


        //public Panel(IPanelContainer inParent, Point inPos, Point inSize, IScheme scheme)
        //{
        //    children = new List<Panel>();
        //    SetParent(inParent);
        //    this.Location = inPos;
        //    this.Size = inSize;
        //    backImage = inImage;
        //    backColor = Color.White;
        //    visible = true;
        //    enabled = true;
        //    //previousMouseIn = false;
        //    //previousMousePress = false;
        //    SetDrawOrder(0.0f);

        //    this.PositionChanged += HandleMove;

        //}

        #endregion

        #region Public Methods
        
        //public void SetDrawOrder(float order)
        //{
        //    if (this.parent == null)
        //    {
        //        this.drawOrder = order;
        //        return;
        //    }

        //    float orderSpace = 1.0f - this.parent.DrawOrder;
        //    this.drawOrder = this.parent.DrawOrder + this.drawOrder * orderSpace;
        //}

        public virtual void LoadContent(ContentManager content)
        {
            
            //foreach (Panel p in children)
            //{
            //    p.LoadContent(content);
            //}

            //backgroundPath = "health_small_tex";
            //object obj = content.Load<object>("health_small_tex");
            if (backgroundPath != null)
                this.Image = this.LoadImage(content, backgroundPath);

            //this.Image = new UIImage(content.Load<Texture2D>(backgroundPath));

        //this.Image = null;
        }

        public UIImage LoadImage(ContentManager content, string path)
        {
            if (path == null)
                return null;

            //Check and see if global content already has this
            object contentObject = GlobalContent.GetContent<object>(path);
            if (contentObject == null)
                contentObject = content.Load<object>(path);
            //else
            //    path = "yay!";
            
            //Check if the content object is an image
            UIImage newImage = contentObject as UIImage;
            if (newImage == null)
            {
                Texture2D texture = contentObject as Texture2D;
                if (texture != null)
                    newImage = new UIImage(texture);
            }
            return newImage;
        }

        public virtual void Activate()
        {
        }


        //IUIParent interface functions
        //public void AddChildren(Panel child)
        //{
        //    children.Add(child);
        //}

        //public void RemoveChildren(Panel child)
        //{
        //    children.Remove(child);
        //}
        //End interface functions


        /// <summary>
        /// Sets the parent of a panel. This is used in calculating the offset and deciding which UI elements
        /// are subcomponents of other elements
        /// </summary>
        /// <param name="inParent"></param>
        public void SetParent(Panel inParent)
        {
            //Remove from our old parent first, if necessary
            if (this.parent != null)
            {
                //this.parent.RemoveChildren(this);
            }

            //Set new parent
            //if (inParent != null)
             //   if (inParent.ParentTarget != null)
              //      inParent = inParent.ParentTarget;

            this.parent = inParent;
            //Add to parent's list of children
            if (inParent != null)
            {
                //inParent.AddChildren(this);

                //Apply our parents scheme
                //if (inParent.Scheme != null)
                //    this.ApplyScheme(inParent.Scheme);
            }

            if (ParentChanged != null)
                ParentChanged(this, EventArgs.Empty);

            RecalculateAbsoluteLocation();
        }

        
        /// <summary>
        /// Panel event handler - this should handle all primary types of events
        /// </summary>
        public virtual bool HandleEvent(InputEventArgs inputEvent)
        {
                //Is this a mouse event or a keyboard event
                if (inputEvent is MouseEventArgs)
                {
                    MouseEventArgs mouseEvent = inputEvent as MouseEventArgs;
                    bool mouseInside =IsPointInside(mouseEvent.Position);

                    if (mouseEvent.MouseEventType == MouseEventType.Move)
                    {
                        //Return for this, because logically only one control
                        //should get a MouseDown event
                        if (mouseEvent.LeftButtonPressed == true && mouseInside)
                        {
                            if (MouseDown != null)
                                MouseDown(this, mouseEvent);
                            return true;
                        }
                        else if (mouseEvent.LeftButtonPressed == false && previousMousePress)
                            previousMousePress = false;
                    }
                    else if (mouseEvent.MouseEventType == MouseEventType.Click)
                    {
                        if (mouseInside && !previousMousePress)
                        {
                            previousMousePress = true;
                            if (MouseClick != null)
                            {
                                MouseClick(this, mouseEvent);
                                return true;
                            }
                            else
                                return false;


                        }
                    }
                    else if (mouseEvent.MouseEventType == MouseEventType.Unclick)
                    {
                        if (previousMousePress)
                        {
                            previousMousePress = false;
                            if (MouseClickRelease != null)
                                MouseClickRelease(this, mouseEvent);
                            return true;
                        }
                    }

                    //return handled || handledByChild;

                }

                return false;
        }

        public virtual void Update(GameTime gameTime)
        {
            //Don't update if we aren't visible
            if (!Visible)
                return;

            if (Image != null)
                Image.Update(gameTime);

            //Don't update if we aren't enabled
            if (!Enabled)
                return;
        }

        public bool IsPointInside(Point point)
        {
            if (point.X > this.AbsoluteLocation.X &&
                point.X < this.AbsoluteLocation.X + this.Size.X &&
                point.Y > this.AbsoluteLocation.Y &&
                point.Y < this.AbsoluteLocation.Y + this.Size.Y)
            {

                return true;
            }

            return false;
        }

        public Rectangle GetDestinationRectangle(Point positionOffset, Vector2 scale)
        {
            Rectangle rectangle = new Rectangle(this.Location.X + positionOffset.X, this.Location.Y + positionOffset.Y,
    this.Size.X, this.Size.Y);

            Vector2 transitionAmount = GetTransitionAmount();


            rectangle.X = (int)( (rectangle.X + transitionAmount.X) * scale.X + 0.5f);
            rectangle.Y = (int)( (rectangle.Y + transitionAmount.Y) * scale.Y + 0.5f);
            rectangle.Width = (int)(rectangle.Width * scale.X);
            rectangle.Height = (int)(rectangle.Height * scale.Y);
            return rectangle;
        }

        public Rectangle AdjustRectangle(Rectangle rectangle, Vector2 scale)
        {
            rectangle.X = (int)((rectangle.X) * scale.X + 0.5f);
            rectangle.Y = (int)((rectangle.Y) * scale.Y + 0.5f);
            rectangle.Width = (int)(rectangle.Width * scale.X);
            rectangle.Height = (int)(rectangle.Height * scale.Y);
            return rectangle;
        }

        public Vector2 GetTransitionAmount()
        {
            Vector2 amount = Vector2.Zero;
            if (TransitionPosition > 0.0f)
            {
                amount.X += (TransitionOnOffset.X * (TransitionPosition));
                amount.Y += (TransitionOnOffset.Y * (TransitionPosition));
            }
            else if (TransitionPosition < 0.0f)
            {
                amount.X += (TransitionOffOffset.X * (-TransitionPosition));
                amount.Y += (TransitionOffOffset.Y * (-TransitionPosition));
            }
            return amount;
        }

        public virtual void Paint(SpriteBatch sb, Point positionOffset, Vector2 scale)
        {

            //Don't draw if we aren't visible
            if (!Visible)
                return;

            //float scaleX = (float)sb.GraphicsDevice.Viewport.Width / Panel.BaseResolution.X;
            //float scaleY = (float)sb.GraphicsDevice.Viewport.Height / Panel.BaseResolution.Y;

            Rectangle rectangle = GetDestinationRectangle(positionOffset, scale);

            //Rectangle rectangle = new Rectangle(this.location.X + positionOffset.X, this.location.Y + positionOffset.Y,
            //    this.Size.X, this.Size.Y);

            //rectangle.X = (int)(rectangle.X * scale.X);
            //rectangle.Y = (int)(rectangle.Y *scale.Y);
            //rectangle.Width = (int)(rectangle.Width * scale.X);
            //rectangle.Height = (int)(rectangle.Height * scale.Y);

            //rectangle.X = (int)(rectangle.X * scaleX);
            //rectangle.Y = (int)(rectangle.Y * scaleY);
            //rectangle.Width = (int)(rectangle.Width * scaleX);
            //rectangle.Height = (int)(rectangle.Height * scaleY);

            Color drawColor = this.backColor;
            //if (!Enabled)
            //    drawColor = Color.Gray;



            if (this.backImage != null)
            {
                Vector4 adjustedColor = this.backImage.Color.ToVector4() * drawColor.ToVector4();
                drawColor = new Color(adjustedColor);
                //sb.Draw(this.backImage.Value, rectangle, this.backImage.Rect, drawColor);
                backImage.Draw(sb, rectangle, drawColor);
            }

        }

        public virtual void UnloadContent()
        {

        }






        #endregion

    }
}
