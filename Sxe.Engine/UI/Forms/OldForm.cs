using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Sxe.Engine.Input;

namespace Sxe.Engine.UI
{
    public class OldForm : PanelContainer, IDisposable
    {
        static Point nullPoint = new Point(-1, -1);

        #region Fields
        RenderTarget2D target;

        FormTitleBarPanel titlePanel;
        Panel contentPanel;
        Panel clientAreaPanel;
        FormCollection formCollection;

        Point startDrag;
        Point startPos;
        Texture2D formTexture;

        //Scheme stuff
        int formTitleHeight = 16;
        Point formPadding = new Point(8, 8);

        bool isLocalOnly = true; 
        #endregion


        #region Events
        public event EventHandler<MouseEventArgs> TitleBarClick;
        public event EventHandler<MouseEventArgs> TitleBarClickRelease;
        #endregion

        public Texture2D FormTexture
        {
            get { return formTexture; }
        }

        public string Text
        {
            get { return titlePanel.Text; }
            set { titlePanel.Text = value; }
        }

        public bool IsBeingDragged()
        {
            return startDrag != nullPoint;
        }

        public Panel ClientArea
        {
            get { return clientAreaPanel; }
        }


        /// <summary>
        /// Get and set the form collection that owns this form
        /// </summary>
        public FormCollection FormCollection
        {
            get { return formCollection; }
            set { formCollection = value; }
        }

        //public Form(Point location, Point size, IScheme formScheme)
        //    : this(location, size, formScheme,null)
        //{
        //}

        //public Form(Point location, Point size, IScheme formScheme, UIImage icon)
        //    : base(null, location, size, null, formScheme)
        //{
        //    //isLocalOnly = localOnly;

        //    //spriteBatch = new SpriteBatch(Device);
        //    target = new RenderTarget2D(Device, size.X, size.Y, 1, SurfaceFormat.Color);


        //    titlePanel = new FormTitleBarPanel(this, new Point(0, 0), new Point(size.X, Scheme.FormTitleBarHeight), Scheme, icon);
        //    contentPanel = new BorderPanel(this, new Point(0, Scheme.FormTitleBarHeight), new Point(size.X, size.Y - Scheme.FormTitleBarHeight), Scheme.FormBackground, BorderPanelMode.Resize);


        //    startDrag = new Point(-1, -1);

        //    titlePanel.MouseClick += TitleClick;
        //    titlePanel.MouseClickRelease += TitleClickRelease;

        //    titlePanel.CloseClick += OnCloseClick;


        //}


        public OldForm(ISchemeManager schemes)
        {
            //isLocalOnly = localOnly;

            //spriteBatch = new SpriteBatch(Device);



            //titlePanel = new FormTitleBarPanel(this, new Point(0, 0), new Point(size.X, Scheme.FormTitleBarHeight), Scheme, icon);
            titlePanel = new FormTitleBarPanel();
            titlePanel.Parent = this;
            titlePanel.Location = Point.Zero;
            titlePanel.Size = new Point(Size.X, formTitleHeight);
            
            //contentPanel = new BorderPanel(this, new Point(0, Scheme.FormTitleBarHeight), new Point(size.X, size.Y - Scheme.FormTitleBarHeight), Scheme.FormBackground, BorderPanelMode.Resize);
            contentPanel = new Panel();
            contentPanel.Parent = this;
            contentPanel.Location = new Point(0, formTitleHeight);
            contentPanel.Size = new Point(Size.X, Size.Y - formTitleHeight);

            clientAreaPanel = new Panel();
            clientAreaPanel.Parent = contentPanel;

            ResetSizes();

            startDrag = new Point(-1, -1);

            titlePanel.MouseClick += TitleClick;
            titlePanel.MouseClickRelease += TitleClickRelease;

            titlePanel.CloseClick += OnCloseClick;

            SizeChanged += OnSizeChanged;

            //ApplyScheme(schemes.DefaultScheme);
        }

        void ResetSizes()
        {
            clientAreaPanel.Location = new Point(formPadding.X, formPadding.Y);
            clientAreaPanel.Size = new Point(contentPanel.Size.X - formPadding.X * 2, contentPanel.Size.Y - formPadding.Y * 2);

        }

        //public override void ApplyScheme(IScheme scheme)
        //{
        //    base.ApplyScheme(scheme);
        //    contentPanel.Image = scheme.GetImage("form_background");
        //    formPadding = scheme.GetPoint("form_padding");
        //    ResetSizes();
        //}

        void OnSizeChanged(object sender, EventArgs args)
        {
            titlePanel.Size = new Point(Size.X, formTitleHeight);
            contentPanel.Size = new Point(Size.X, Size.Y - formTitleHeight);
            ResetRenderTarget();
            ResetSizes();
        }

        void ResetRenderTarget()
        {
            if (target != null)
                target.Dispose();
            target = new RenderTarget2D(Device, (int)Size.X, (int)Size.Y, 1, SurfaceFormat.Color);
        }

        void OnCloseClick(object sender, MouseEventArgs args)
        {
            this.Visible = false;
            formCollection.SetFormVisible(this.Name, false);
        }

        public override void  Paint(SpriteBatch sb, Point positionOffset, Vector2 scale)
        {
            //Vector2 screenSize = new Vector2(Device.Viewport.Width, Device.Viewport.Height);

            if (target == null)
                return;

            if (!Invalidated)
                return;

            Device.SetRenderTarget(0, target);
            sb.Begin();
            //Paint our object, and all children
            //HACK: We use a negative of our location because we are painting to a rendertarget,
            //and it should start at 0,0, but its actually going to start at the top corner of the form
            base.Paint(sb, new Point(-this.Location.X, -this.Location.Y), scale);
            sb.End();
            Device.SetRenderTarget(0, null);

            formTexture = target.GetTexture();

            this.Invalidated = false;

        }

        public override void Update(GameTime time)
        {
            //TODO: Revisit - do we really need to be accessing the input controller directly??
            if (startDrag.X != -1 && startDrag.Y != -1)
            {
                //if (Input.Mouse.IsLeftButtonJustReleased())
                if(Input.Controller.IsKeyJustReleased("cursor_leftclick"))
                    startDrag = nullPoint;
                else
                {

                    //Check the delta
                    Point delta = new Point((int)Input.Controller.GetValue("cursor_x") - startDrag.X,
                        (int)Input.Controller.GetValue("cursor_y") - startDrag.Y);
                    this.Location = new Point(startPos.X + delta.X, startPos.Y + delta.Y);
                }
            }
            base.Update(time);
        }

        void TitleClick(object value, MouseEventArgs args)
        {
            if (TitleBarClick != null)
                TitleBarClick(value, args);

            startDrag = args.Position;
            startPos = this.Location;
        }

        void TitleClickRelease(object value, MouseEventArgs args)
        {
            if (TitleBarClickRelease != null)
                TitleBarClickRelease(value, args);

            startDrag = nullPoint;
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                
            }
            //Free unmanaged resources
            target.Dispose();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
