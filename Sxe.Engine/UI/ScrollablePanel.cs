using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sxe.Engine.UI
{

    public enum ScrollType
    {
        LargeDecrement = 0,
        SmallDecrement,
        LargeIncrement,
        SmallIncrement
    }

    /// <summary>
    /// Specialized panel that is set up with scrollbars
    /// </summary>
    public class ScrollablePanel : Panel
    {
        Point displayCoordinates;
        Point displaySize;
        Point fullSize;
        VerticalScrollBar verticalScrollBar;
        int scrollBarWidth = 16;
        

        public Point DisplayCoordinates 
        {
            get { return displayCoordinates; }
            set 
            {
                displayCoordinates = value;
                displayCoordinates.X = (int)MathHelper.Clamp(displayCoordinates.X, 0, FullSize.X - DisplaySize.X);
                displayCoordinates.Y = (int)MathHelper.Clamp(displayCoordinates.Y, 0, FullSize.Y - DisplaySize.Y);

                if (Scroll != null)
                    Scroll(this, null);
            }
        }

        public Point DisplaySize 
        {
            get { return displaySize; }
            set { displaySize = value; }
        }

        public Point FullSize 
        { 
            get { return fullSize; }
            set { fullSize = value; FireDisplayChanged(); }
        }

        /// <summary>
        /// Show or hide vertical scroll bars
        /// </summary>
        public bool ShowVerticalScrollBars
        {
            get { return verticalScrollBar.Visible; }
            set { verticalScrollBar.Visible = value; }
        }

        //This event gets fired when the display size or the full size get changed
        public event EventHandler DisplaySizeChanged;
        public event EventHandler Scroll;

        //public ScrollablePanel(IPanelContainer parent, Point location, Point size, UIImage image, IScheme scheme)
        //    : base(parent, location, new Point(size.X, size.Y), image, scheme)
        //{
        //    displayCoordinates = new Point(0, 0);
        //    displaySize = new Point(1, 1);
        //    fullSize = new Point(1, 1);

        //    //We make the vertical scrollbar a child of the parent.. This allows us to use the scrollbar
        //    //even when the control is disabled
        //    verticalScrollBar = new VerticalScrollBar(parent, new Point(location.X + size.X, location.Y), 
        //        new Point(scheme.ScrollBarWidth, size.Y), scheme);

        //    verticalScrollBar.ScrollTarget = this;
        //    verticalScrollBar.Visible = false;

        //    DisplaySizeChanged += OnDisplaySizeChanged;
        //    VisibleChanged += OnVisibleChanged;

        //    //Run this function to decide if scrollbars should be shown
        //    OnDisplaySizeChanged(this, null); 
        //}

        public ScrollablePanel()
        {
            displayCoordinates = new Point(0, 0);
            displaySize = new Point(1, 1);
            fullSize = new Point(1, 1);

            //We make the vertical scrollbar a child of the parent.. This allows us to use the scrollbar
            //even when the control is disabled
            //verticalScrollBar = new VerticalScrollBar(parent, new Point(location.X + size.X, location.Y),
            //    new Point(scheme.ScrollBarWidth, size.Y), scheme);
            verticalScrollBar = new VerticalScrollBar();

            verticalScrollBar.ScrollTarget = this;
            verticalScrollBar.Visible = false;

            DisplaySizeChanged += OnDisplaySizeChanged;
            VisibleChanged += OnVisibleChanged;
            SizeChanged += OnSizeChanged;
            ParentChanged += OnParentChanged;

            //Run this function to decide if scrollbars should be shown
            OnDisplaySizeChanged(this, null);
        }

        void OnParentChanged(object sender, EventArgs args)
        {
            verticalScrollBar.Parent = this.Parent;
        }

        void OnSizeChanged(object sender, EventArgs args)
        {
            verticalScrollBar.Location = new Point(Location.X + Size.X - scrollBarWidth, Location.Y);
            verticalScrollBar.Size = new Point(scrollBarWidth, Size.Y);
        }
        //public override void ApplyScheme(IScheme scheme)
        //{
        //    base.ApplyScheme(scheme);
        //    scrollBarWidth = scheme.GetInt("scroll_width");
        //    OnSizeChanged(null, null);
        //}

        public void DoScroll(ScrollType scroll)
        {
            Point newCoordinates = DisplayCoordinates;
            switch (scroll)
            {
                case ScrollType.LargeDecrement:
                    newCoordinates.Y -= DisplaySize.Y;
                    break;
                case ScrollType.SmallDecrement:
                    newCoordinates.Y -= 1;
                    break;
                case ScrollType.LargeIncrement:
                    newCoordinates.Y += DisplaySize.Y;
                    break;
                case ScrollType.SmallIncrement:
                    newCoordinates.Y += 1;
                    break;
                default:
                    break;
            }

            DisplayCoordinates = newCoordinates;

            
        }

        void OnVisibleChanged(object value, EventArgs args)
        {
            OnDisplaySizeChanged(this, null);
        }

        /// <summary>
        /// Helper function to make firing the event less lines of code
        /// </summary>
        protected void FireDisplayChanged()
        {
            if (DisplaySizeChanged != null)
                DisplaySizeChanged(this, null);
        }

        void OnDisplaySizeChanged(object value, EventArgs args)
        {
            //Only make the scrollbar visible if it is needed
            if (displaySize.Y >= fullSize.Y)
                verticalScrollBar.Visible = false;
            else
                verticalScrollBar.Visible = true && this.Visible;

        }

    }
}
