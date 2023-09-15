using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Sxe.Engine.Input;



namespace Sxe.Engine.UI
{
    public class VerticalScrollBar : Panel
    {
        Button upButton;
        Button downButton;

        Panel sliderPanel;

        Point arrowSize = new Point(16, 16);

        //Button sliderButton;
        DraggablePanel sliderButton;

        ScrollablePanel target;

        public ScrollablePanel ScrollTarget
        {
            get { return target; }
            set 
            {
                if (target != null)
                    target.DisplaySizeChanged -= OnTargetChanged;

                target = value;
                target.DisplaySizeChanged += OnTargetChanged;
                UpdateScrollPanel(); 
            }
        }

        //public VerticalScrollBar(IPanelContainer parent, Point location, Point size)
        //    : base(parent, location, size, null)
        //{
        //    upButton = new Button(this, Point.Zero, scheme.ArrowSize, scheme.DefaultFont,
        //        scheme.ScrollBarUpArrow, scheme.ScrollBarUpArrowOver, scheme.ScrollBarUpArrowPressed,
        //        Color.Black, BorderPanelMode.Stretch, scheme);

        //    downButton = new Button(this, new Point(0, size.Y - scheme.ArrowSize.Y), scheme.ArrowSize, scheme.DefaultFont,
        //        scheme.ScrollBarDownArrow, scheme.ScrollBarDownArrowOver, scheme.ScrollBarDownArrowPressed,
        //        Color.Black, BorderPanelMode.Stretch, scheme);

        //    sliderPanel = new Panel(this, new Point(0, scheme.ArrowSize.Y), new Point(scheme.ArrowSize.X, size.Y - (2 * scheme.ArrowSize.Y)),
        //        scheme.White);
        //    sliderPanel.BackColor = Color.Gray;

        //    //sliderButton = new Button(sliderPanel, new Point(0, 0), scheme.ArrowSize, scheme.DefaultFont,
        //    //    scheme.CommandButtonImage, scheme.CommandButtonOverImage, scheme.CommandButtonClickImage, 
        //    //    Color.Black, BorderPanelMode.Resize, scheme);

        //    sliderButton = new DraggablePanel(sliderPanel, new Point(0, 0), scheme.ArrowSize, scheme.CommandButtonImage);
        //    sliderButton.CanDrag = true;
        //    sliderButton.DragMultiplier = new Vector2(0.0f, 1.0f);
        //    sliderButton.Drag += OnDrag;

        //    upButton.MouseClick += OnUpClick;
        //    downButton.MouseClick += OnDownClick;
        //    sliderPanel.MouseClick += OnPanelClick;

        //}

        public VerticalScrollBar()
        {
            //upButton = new Button(this, Point.Zero, scheme.ArrowSize, scheme.DefaultFont,
            //    scheme.ScrollBarUpArrow, scheme.ScrollBarUpArrowOver, scheme.ScrollBarUpArrowPressed,
            //    Color.Black, BorderPanelMode.Stretch, scheme);
            upButton = new Button();
            upButton.Parent = this;
            upButton.Location = Point.Zero;
            upButton.Size = arrowSize;


            //downButton = new Button(this, new Point(0, size.Y - scheme.ArrowSize.Y), scheme.ArrowSize, scheme.DefaultFont,
            //    scheme.ScrollBarDownArrow, scheme.ScrollBarDownArrowOver, scheme.ScrollBarDownArrowPressed,
            //    Color.Black, BorderPanelMode.Stretch, scheme);
            downButton = new Button();
            downButton.Parent = this;
            downButton.Location = Point.Zero;
            downButton.Size = arrowSize;

            //sliderPanel = new Panel(this, new Point(0, scheme.ArrowSize.Y), new Point(scheme.ArrowSize.X, size.Y - (2 * scheme.ArrowSize.Y)),
            //    scheme.White);
            //sliderPanel.BackColor = Color.Gray;
            sliderPanel = new Panel();
            sliderPanel.Parent = this;
            sliderPanel.Location = new Point(0, arrowSize.Y);
            sliderPanel.Size = new Point(arrowSize.X, Size.Y - 2 * arrowSize.Y);

            
            //sliderButton = new Button(sliderPanel, new Point(0, 0), scheme.ArrowSize, scheme.DefaultFont,
            //    scheme.CommandButtonImage, scheme.CommandButtonOverImage, scheme.CommandButtonClickImage, 
            //    Color.Black, BorderPanelMode.Resize, scheme);

            //sliderButton = new DraggablePanel(sliderPanel, new Point(0, 0), scheme.ArrowSize, scheme.CommandButtonImage);
            sliderButton = new DraggablePanel();
            sliderButton.Parent = sliderPanel;
            sliderButton.Location = Point.Zero;
            sliderButton.Size = arrowSize;
            
            sliderButton.CanDrag = true;
            sliderButton.DragMultiplier = new Vector2(0.0f, 1.0f);
            sliderButton.Drag += OnDrag;

            upButton.MouseClick += OnUpClick;
            downButton.MouseClick += OnDownClick;
            sliderPanel.MouseClick += OnPanelClick;

            this.SizeChanged += this.OnSizeChanged;

        }

        void OnSizeChanged(object sender, EventArgs args)
        {
            sliderPanel.Size = new Point(arrowSize.X, Size.Y - 2 * arrowSize.Y);

            downButton.Location = new Point(0, Size.Y - arrowSize.Y);
        }

        //public override void ApplyScheme(IScheme scheme)
        //{
        //    base.ApplyScheme(scheme);
        //    UIImage scrollbarUpArrow = scheme.GetImage("scrollbar_uparrow");
        //    UIImage scrollbarUpArrowOver = scheme.GetImage("scrollbar_uparrow_over");
        //    UIImage scrollbarUpArrowClick = scheme.GetImage("scrollbar_uparrow_pressed");
        //    UIImage scrollbarDownArrow = scheme.GetImage("scrollbar_downarrow");
        //    UIImage scrollbarDownArrowOver = scheme.GetImage("scrollbar_downarrow_over");
        //    UIImage scrollbarDownArrowClick = scheme.GetImage("scrollbar_downarrow_pressed");
        //    UIImage blank = new UIImage(scheme.GetTexture("blank"));

        //    sliderButton.Image = scheme.GetImage("button_default");
        //    sliderPanel.Image = blank;
        //    sliderPanel.BackColor = Color.Gray;

        //    upButton.DefaultImage = scrollbarUpArrow;
        //    upButton.OverImage = scrollbarUpArrowOver;
        //    upButton.ClickImage = scrollbarUpArrowClick;

        //    downButton.DefaultImage = scrollbarDownArrow;
        //    downButton.OverImage = scrollbarDownArrowOver;
        //    downButton.ClickImage = scrollbarDownArrowClick;

        //    arrowSize = scheme.GetPoint("scrollbar_arrow_size");
        //    upButton.Size = arrowSize;
        //    downButton.Size = arrowSize;
        //    sliderButton.Size = arrowSize;

        //    OnSizeChanged(null, null);

            
        //}

        void OnTargetChanged(object value, EventArgs args)
        {
            UpdateScrollPanel();
        }

        void OnPanelClick(object value, MouseEventArgs args)
        {
            if (ScrollTarget != null)
            {
                float adjPosition = args.Position.Y - sliderPanel.AbsoluteLocation.Y;
                //See if the click was above the sliderButton or below
                if (adjPosition < sliderButton.Location.Y)
                {
                    ScrollTarget.DoScroll(ScrollType.LargeDecrement);
                }
                else if(adjPosition > sliderButton.Location.Y + sliderButton.Size.Y)
                {
                    ScrollTarget.DoScroll(ScrollType.LargeIncrement);
                }
            }
        }

        void OnDrag(object value, DragEventArgs dragArgs)
        {
            if (dragArgs.DragAmount.Y < 0)
                ScrollTarget.DoScroll(ScrollType.SmallDecrement);
            else
                ScrollTarget.DoScroll(ScrollType.SmallIncrement);
        }

        void OnUpClick(object value, EventArgs args)
        {
            if (ScrollTarget != null)
                ScrollTarget.DoScroll(ScrollType.SmallDecrement);
        }

        void OnDownClick(object value, EventArgs args)
        {
            if(ScrollTarget != null)
            ScrollTarget.DoScroll(ScrollType.SmallIncrement);
        }



        void UpdateScrollPanel()
        {
            if (ScrollTarget == null)
                return;

            float sliderPanelHeight = sliderPanel.Size.Y;
            if(ScrollTarget.FullSize.Y > 0)
                sliderPanelHeight *= (float)ScrollTarget.DisplaySize.Y / (float)ScrollTarget.FullSize.Y;

            float sliderPanelY = 0.0f;
            if(ScrollTarget.FullSize.Y > 0)
             sliderPanelY = (float)sliderPanel.Size.Y * (float)ScrollTarget.DisplayCoordinates.Y / (float)ScrollTarget.FullSize.Y;

            sliderButton.Location = new Point(sliderPanel.Location.X, (int)sliderPanelY);
            sliderButton.Size = new Point(sliderPanel.Size.X, (int)sliderPanelHeight);

        }
    }
}
