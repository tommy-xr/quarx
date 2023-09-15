using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sxe.Engine.UI
{

    public enum SplitPanelOrientation
    {
        Horizontal = 0,
        Vertical
    }

    /// <summary>
    /// Splitter panel is a container that divides a panel up into two regions
    /// The splitter can be set to be horizontal or vertical 
    /// </summary>
    public class SplitPanel : Panel
    {
        //Default splitter width
        int splitterWidth = 5;
        bool suppressReset = false;

        SplitPanelOrientation orientation = SplitPanelOrientation.Horizontal;

        Panel panel1;
        Panel panel2;
        DraggablePanel splitter;

        public event EventHandler<EventArgs> OrientationChanged;


        public SplitPanelOrientation Orientation
        {
            get { return orientation; }
            set
            {
                orientation = value;

                if (OrientationChanged != null)
                    OrientationChanged(this, EventArgs.Empty);
            }
        }

        public Panel Panel1 { get { return panel1; } }
        public Panel Panel2 { get { return panel2; } }

        public SplitPanel()
        {
            panel1 = new Panel();
            panel1.Parent = this;

            panel2 = new Panel();
            panel2.Parent = this;

            splitter = new DraggablePanel();
            splitter.Parent = this;

            this.SizeChanged += OnResize;
            this.splitter.PositionChanged += OnSplitterMoved;
            this.ParentChanged += OnParentChanged;

            ResetSplitter(true);
        }

        //public override void ApplyScheme(IScheme scheme)
        //{
        //    base.ApplyScheme(scheme);

        //    splitterWidth = scheme.GetInt("splitter_width");

        //    Texture2D blankTex = scheme.GetTexture("blank");
        //    UIImage blank = new UIImage(blankTex);

        //    panel1.Image = blank;
        //    panel1.BackColor = Color.Blue;

        //    panel2.Image = blank;
        //    panel2.BackColor = Color.Red;

        //    splitter.Image = blank;
        //    splitter.BackColor = Color.Yellow;
        //}

        void OnParentChanged(object sender, EventArgs args)
        {
            if (this.Parent != null)
                this.Parent.SizeChanged += OnParentSizeChanged;
        }

        void OnParentSizeChanged(object sender, EventArgs args)
        {
            if (sender == this.Parent)
            {
                this.Size = this.Parent.Size;
                ResetPanelSizes();
                suppressReset = true;
            }
        }


        void OnSplitterMoved(object sender, EventArgs args)
        {
            ResetPanelSizes();
        }

        void OnResize(object sender, EventArgs args)
        {
            ResetSplitter(suppressReset);

            if (suppressReset)
                suppressReset = false;

            ResetPanelSizes();
        }

        public void SetSplitter(int width)
        {
            ResetSplitter(true);

            if (orientation == SplitPanelOrientation.Horizontal)
            {
                splitter.Location = new Point(0, width);
            }
            else
            {
                splitter.Location = new Point(width, 0);
            }
        }

        /// <summary>
        /// Handles the logic of resetting the splitter, when the size or orientation changes
        /// </summary>
        public void ResetSplitter(bool resetLocation)
        {
            if (orientation == SplitPanelOrientation.Horizontal)
            {
                if(resetLocation)
                splitter.Location = new Point(0, this.Size.Y / 2 - splitterWidth / 2);
                splitter.Size = new Point(this.Size.X, splitterWidth);
                splitter.DragMultiplier = new Vector2(0, 1);
            }
            else
            {
                if(resetLocation)
                splitter.Location = new Point(this.Size.X / 2 - splitterWidth / 2, 0);
                splitter.Size = new Point(splitterWidth, this.Size.Y);
                splitter.DragMultiplier = new Vector2(1, 0);
            }
        }

        void ResetPanelSizes()
        {
            if (orientation == SplitPanelOrientation.Horizontal)
            {
                panel1.Location = new Point(0, 0);
                panel1.Size = new Point(this.Size.X, splitter.Location.Y);

                panel2.Location = new Point(0, splitter.Location.Y + splitterWidth);
                panel2.Size = new Point(this.Size.X, this.Size.Y - splitter.Location.Y - splitterWidth);

            }
            else
            {
                panel1.Location = new Point(0, 0);
                panel1.Size = new Point(splitter.Location.X, this.Size.Y);

                panel2.Location = new Point(splitter.Location.X + splitterWidth, 0);
                panel2.Size = new Point(this.Size.X - splitter.Location.X - splitterWidth, this.Size.Y);

            }
        }




    }
}
