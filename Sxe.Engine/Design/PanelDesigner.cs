using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;

using Sxe.Engine.UI;

using Drawing = System.Drawing;
using XnaDrawing = Microsoft.Xna.Framework;

namespace Sxe.Design
{
    /// <summary>
    /// Base class for a designer for panels
    /// </summary>
    public class PanelDesigner : ComponentDesigner
    {
        private static readonly Size adornmentDimensions = new Size(7, 7);
        private static readonly Size handleDimensions = new Size(12, 12);

        //Hit test objects
        //These are objects 
        private static readonly object HitMove = new object();
        private static readonly object HitBottom = new object();
        private static readonly object HitTop = new object();
        private static readonly object HitLeft = new object();
        private static readonly object HitRight = new object();
        private static readonly object HitLowerLeft = new object();
        private static readonly object HitLowerRight = new object();
        private static readonly object HitUpperLeft = new object();
        private static readonly object HitUpperRight = new object();

        public override DesignerVerbCollection Verbs
        {
            get
            {
                return new DesignerVerbCollection(
                    new DesignerVerb[] { new DesignerVerb("test", new EventHandler(DefaultEventHandler)) });
            }
        }




        public void DefaultEventHandler(object sender, EventArgs args)
        {
            MessageBox.Show("HI");
        }



        Rectangle preDragRectangle;
        Pen pen;
        Pen dragPen;

        public virtual Sxe.Engine.UI.Panel Panel
        {
            get { return (Sxe.Engine.UI.Panel)Component; }
        }

        bool isDragging = false;
        public bool IsDragging
        {
            get { return isDragging; }
            set { isDragging = value; }
        }

        bool isSelected = false;
        public bool IsSelected
        {
            get { return isSelected; }
            set { isSelected = value; }
        }

        public virtual void DropPanel(PanelDesigner panel)
        {
            Sxe.Engine.UI.Panel test = this.Component as Sxe.Engine.UI.Panel;
            //if (test != null)
            //    test.BackColor = XnaDrawing.Graphics.Color.Yellow;

        }

        public bool Visible
        {
            get { return (bool)ShadowProperties["Visible"]; }
            set { ShadowProperties["Visible"] = value; }
        }


        [Browsable(false)]
        [DesignOnly(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected virtual Rectangle BoundingBox
        {
            get
            {
                Drawing.Rectangle rect = new Rectangle(
                    Panel.Location.X, Panel.Location.Y,
                    Panel.Size.X, Panel.Size.Y);
                return rect;
            }
            set
            {
                //We need to adjust both the Location and Size
                Panel.Size = new XnaDrawing.Point(value.Width, value.Height);
                Panel.Location = new XnaDrawing.Point(value.X, value.Y);

            }
        }

        protected virtual Rectangle AbsoluteBoundingBox
        {
            get
            {
                return new Drawing.Rectangle(
                    Panel.AbsoluteLocation.X, Panel.AbsoluteLocation.Y,
                    Panel.Size.X, Panel.Size.Y);
            }
        }

        public void CommitBoundingBoxChanges()
        {
            PropertyDescriptor location = TypeDescriptor.GetProperties(Component)["Location"];
            PropertyDescriptor size = TypeDescriptor.GetProperties(Component)["Size"];

            if (size != null && size.PropertyType == typeof(XnaDrawing.Point))
            {
                size.SetValue(Component, new XnaDrawing.Point(BoundingBox.Width, BoundingBox.Height));
            }

            if (location != null && location.PropertyType == typeof(XnaDrawing.Point))
            {
                location.SetValue(Component, new XnaDrawing.Point(BoundingBox.X, BoundingBox.Y));
            }
        }

        internal DesignerHitResult GetHitTest(int x, int y)
        {
            return GetHitTest(x, y, null);
        }

        /// <summary>
        /// Returns a hit object, which describes where the object was hit
        /// Returns null if x, y are not in the rectangles bounds
        /// </summary>
        internal virtual DesignerHitResult GetHitTest(int x, int y, IList ignoreObject)
        {

            DesignerHitResult result = new DesignerHitResult();
            result.HitObject = null;
            result.HitPanel = this.Panel;

            Size spacing = handleDimensions;
            //spacing.Width *= 2;
            //spacing.Height *= 2;

            Rectangle bounds = AbsoluteBoundingBox;
            bounds.Inflate(spacing);

            //Rule out not being hit first
            if (!bounds.Contains(x, y))
                return result;

            bounds = AbsoluteBoundingBox;

            // return null;

            Size halfSize = new Size(spacing.Width / 2, spacing.Height / 2);

            //Now, create rectangles for each side
            Rectangle top = new Rectangle(bounds.X + halfSize.Width, bounds.Y - halfSize.Height,
                bounds.Width - spacing.Width, spacing.Height);
            Rectangle topLeft = new Rectangle(bounds.X - halfSize.Width, bounds.Y - halfSize.Height,
                spacing.Width, spacing.Height);
            Rectangle topRight = new Rectangle(bounds.Right - halfSize.Width, bounds.Y - halfSize.Height,
                spacing.Width, spacing.Height);

            Rectangle left = new Rectangle(bounds.X - halfSize.Width, bounds.Y + halfSize.Height,
                spacing.Width, bounds.Height - spacing.Height);
            Rectangle right = new Rectangle(bounds.Right - halfSize.Width, bounds.Y + halfSize.Height,
                halfSize.Width, bounds.Height - spacing.Height);

            Rectangle botLeft = new Rectangle(bounds.X - halfSize.Width, bounds.Bottom - halfSize.Height,
                spacing.Width, spacing.Height);
            Rectangle bottom = new Rectangle(bounds.X + halfSize.Width, bounds.Bottom - halfSize.Height,
                bounds.Width - spacing.Width, spacing.Height);
            Rectangle botRight = new Rectangle(bounds.Right - halfSize.Width, bounds.Bottom - halfSize.Height,
                spacing.Width, spacing.Height);



            //Check these rectangles
            if (topLeft.Contains(x, y))
                result.HitObject = HitUpperLeft;
            else if (topRight.Contains(x, y))
                result.HitObject = HitUpperRight;
            else if (top.Contains(x, y))
                result.HitObject = HitTop;
            else if (botLeft.Contains(x, y))
                result.HitObject = HitLowerLeft;
            else if (botRight.Contains(x, y))
                result.HitObject = HitLowerRight;
            else if (left.Contains(x, y))
                result.HitObject = HitLeft;
            else if (right.Contains(x, y))
                result.HitObject = HitRight;
            else if (bottom.Contains(x, y))
                result.HitObject = HitBottom;
            else
                result.HitObject = HitMove;

            return result;
        }

        public Cursor GetCursor(object hitObject)
        {
            if (hitObject == HitLeft || hitObject == HitRight)
                return Cursors.SizeWE;
            if (hitObject == HitTop || hitObject == HitBottom)
                return Cursors.SizeNS;
            if (hitObject == HitUpperLeft || hitObject == HitLowerRight)
                return Cursors.SizeNWSE;
            if (hitObject == HitUpperRight || hitObject == HitLowerLeft)
                return Cursors.SizeNESW;

            return Cursors.IBeam;
        }


        public virtual void DrawAdornments(Drawing.Graphics g, bool primary)
        {
            if (pen == null)
            {
                pen = new Pen(Color.Black, 1.0f);
                pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            }

            if (dragPen == null)
            {
                dragPen = new Pen(Color.Black, 5.0f);
            }

            Rectangle bounds = AbsoluteBoundingBox;

            if (IsDragging)
            {
                DrawBorder(g, dragPen, bounds);
            }

            else if (IsSelected)
            {

                int adornmentOffset = adornmentDimensions.Width / 2;

                Rectangle adornmentRect = new Rectangle(bounds.X - adornmentOffset,
                    bounds.Y - adornmentOffset, adornmentDimensions.Width, adornmentDimensions.Height);

                DrawBorder(g, pen, bounds);
                ControlPaint.DrawGrabHandle(g, adornmentRect, primary, true);

                adornmentRect.X = bounds.X + bounds.Width / 2 - adornmentOffset;
                ControlPaint.DrawGrabHandle(g, adornmentRect, primary, true);

                adornmentRect.X = bounds.Right - adornmentOffset;
                ControlPaint.DrawGrabHandle(g, adornmentRect, primary, true);

                adornmentRect.Y = bounds.Y + bounds.Height / 2 - adornmentOffset;
                ControlPaint.DrawGrabHandle(g, adornmentRect, primary, true);

                adornmentRect.Y = bounds.Bottom - adornmentOffset;
                ControlPaint.DrawGrabHandle(g, adornmentRect, primary, true);

                adornmentRect.X = bounds.X + bounds.Width / 2 - adornmentOffset;
                ControlPaint.DrawGrabHandle(g, adornmentRect, primary, true);

                adornmentRect.X = bounds.X - adornmentOffset;
                ControlPaint.DrawGrabHandle(g, adornmentRect, primary, true);

                adornmentRect.Y = bounds.Y + bounds.Height / 2 - adornmentOffset;
                ControlPaint.DrawGrabHandle(g, adornmentRect, primary, true);


            }

        }

        private void DrawBorder(Graphics g, Pen pen, Rectangle bounds)
        {
            g.DrawLine(pen, new Point(bounds.X, bounds.Y), new Point(bounds.Right, bounds.Y));
            g.DrawLine(pen, new Point(bounds.X, bounds.Y), new Point(bounds.X, bounds.Bottom));
            g.DrawLine(pen, new Point(bounds.Right, bounds.Y), new Point(bounds.Right, bounds.Bottom));
            g.DrawLine(pen, new Point(bounds.X, bounds.Bottom), new Point(bounds.Right, bounds.Bottom));

        }

        /// <summary>
        /// Start a drag operation
        /// Saves the original bounding box
        /// </summary>
        public virtual void StartDrag()
        {
            preDragRectangle = BoundingBox;
            isDragging = true;
        }

        public virtual void EndDrag()
        {
            isDragging = false;
            CommitBoundingBoxChanges();
        }


        public virtual void Drag(object hitObject, int x, int y)
        {
            Rectangle bounds = preDragRectangle;

            //Adjust the x?
            if (hitObject == HitLeft || hitObject == HitUpperLeft || hitObject == HitLowerLeft
                || hitObject == HitMove)
                bounds.X += x;

            //Adjust the y?
            if (hitObject == HitTop || hitObject == HitUpperRight || hitObject == HitUpperLeft
                || hitObject == HitMove)
                bounds.Y += y;

            //Adjust the width?
            if (hitObject == HitUpperRight || hitObject == HitRight || hitObject == HitLowerRight)
                bounds.Width += x;

            if (hitObject == HitUpperLeft || hitObject == HitLeft || hitObject == HitLowerLeft)
                bounds.Width -= x;

            //Adjust the height?
            if (hitObject == HitTop || hitObject == HitUpperLeft || hitObject == HitUpperRight)
                bounds.Height -= y;

            if (hitObject == HitBottom || hitObject == HitLowerLeft || hitObject == HitLowerRight)
                bounds.Height += y;

            BoundingBox = bounds;
        }

        protected override void PreFilterProperties(IDictionary properties)
        {
            // Always call base first!
            //
            base.PreFilterProperties(properties);

            // We add a new property:  BoundingBox.
            //
            properties["BoundingBox"] = TypeDescriptor.CreateProperty(this.GetType(), "BoundingBox", typeof(Rectangle));

            properties["Visible"] = TypeDescriptor.CreateProperty(
                this.GetType(),
                (PropertyDescriptor)properties["Visible"],
                new Attribute[0]);

        }
    }



}
