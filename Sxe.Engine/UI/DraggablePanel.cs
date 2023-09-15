using System;
using System.Collections.Generic;
using System.Text;

using Sxe.Engine.Input;
using Microsoft.Xna.Framework;

namespace Sxe.Engine.UI
{

    public class DragEventArgs : EventArgs
    {
        Point dragAmount;

        public Point DragAmount
        {
            get { return dragAmount; }
            set { dragAmount = value; }
        }
    }
    /// <summary>
    /// A type of panel that can be dragged
    /// </summary>
    public class DraggablePanel : Panel
    {
        bool isDragging = false;
        Point originalPosition;
        Point dragStartPosition;

        Vector2 dragMultiplier = new Vector2(1.0f, 1.0f);
        Point dragMax = new Point(int.MaxValue, int.MaxValue);
        Point dragMin = new Point(int.MinValue, int.MinValue);
        bool canDrag = true;

        public event EventHandler<EventArgs> DragStart;
        public event EventHandler<EventArgs> DragDrop;
        public event EventHandler<DragEventArgs> Drag;

        DragEventArgs dragArgs = new DragEventArgs();

        public DraggablePanel()
        {
        }


        public override bool CanDrag
        {
            get 
            {
                return canDrag;
            }
            set { canDrag = value; }
        }

        public override bool IsDragging
        {
            get { return isDragging; }
        }

        /// <summary>
        /// Specifies the behavior of how the drag should work.
        /// The drag multiplier gets multiplied by the mouse delta
        /// </summary>
        public Vector2 DragMultiplier
        {
            get { return dragMultiplier; }
            set { dragMultiplier = value; }
        }

        public Point DragMax
        {
            get { return dragMax; }
            set { dragMax = value; }
        }

        public Point DragMin
        {
            get { return dragMin; }
            set { dragMin = value; }
        }

        public override bool HandleEvent(Sxe.Engine.Input.InputEventArgs inputEvent)
        {
          
            MouseEventArgs mouseEvent = inputEvent as MouseEventArgs;

            bool handled = false;
            if (mouseEvent != null && Visible)
            {
                //Check if a click event happened inside
                switch (mouseEvent.MouseEventType)
                {
                    case MouseEventType.Click:
                        {
                            if (CanDrag)
                            {
                                if (IsPointInside(mouseEvent.Position))
                                {
                                    if (!isDragging)
                                        StartMove(mouseEvent.Position);
                                }
                            }
                            break;
                        }
                    case MouseEventType.Unclick:
                        {
                            if (isDragging)
                                StopMove();
                            break;
                        }
                    case MouseEventType.Move:
                        {
                            if (isDragging)
                            {
                                    //Only allow a drag to continue IF
                                    //none of our children are dragging (otherwise they get double dragged!)
                                    //and we actually have focus
                                    //TODO: Fix dragging again


                                    //Had to take this out because of removing focus
                                    //if (this.HasFocus && !IsChildrenDragging())
                                    //    UpdateMove(mouseEvent.Position);
                                    //else
                                    //    StopMove();
                            }
                            break;
                        }
                    default:
                        break;
                }
                handled = true;
            }


            handled =  base.HandleEvent(inputEvent) || handled;
            return handled;
        }

        void StartMove(Point position)
        {
            isDragging = true;
            dragStartPosition = position;
            originalPosition = this.Location;

            if (DragStart != null)
                DragStart(this, EventArgs.Empty);
        }

        void UpdateMove(Point position)
        {
            float adjX = (float)(position.X - dragStartPosition.X);
            float adjY = (float)(position.Y - dragStartPosition.Y);
            adjX *= dragMultiplier.X;
            adjY *= dragMultiplier.Y;

            int newX = originalPosition.X + (int)adjX;
            int newY = originalPosition.Y + (int)adjY;

            int actualNewX = Math.Min(newX, DragMax.X);
            actualNewX = Math.Max(actualNewX, DragMin.X);

            int actualNewY = Math.Min(newY, DragMax.Y);
            actualNewY = Math.Max(actualNewY, DragMin.Y);

            this.Location = new Point(
                actualNewX, actualNewY);

            //TODO: Shoudl this be the amount we TRIED to drag, or the amount actually dragged?
            dragArgs.DragAmount = new Point((int)adjX, (int)adjY);

            if (Drag != null)
                Drag(this, dragArgs);
        }

        void StopMove()
        {
            isDragging = false;
            dragStartPosition = new Point(-1, -1);
            originalPosition = new Point(-1, -1);

            if (DragDrop != null)
                DragDrop(this, EventArgs.Empty);
        }

    }


}
