//using System;
//using System.Collections.Generic;
//using System.Text;

//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;

//namespace Sxe.Engine.UI
//{
//    /// <summary>
//    /// This is a special label that has labels for each line. It is not meant to be a control
//    /// in itself, but as a helper to other controls that have arrays of labels.
//    /// </summary>
//    class MultiLineLabel : Panel
//    {
//        Label[] labels;
//        int forceLabelSize = -1;
//        int fontHeight = -1;

//        public Label this[int i]
//        {
//            get { return labels[i]; }
//        }

//        public int Count
//        {
//            get { return labels.Length; }
//        }

//        public int FontHeight
//        {
//            get { return fontHeight; }
//        }

//        public bool HasFont
//        {
//            get { return font != null; }
//        }

//        public MultiLineLabel(IPanelContainer parent, Point location, Point size)
//            : this(parent, location, size, -1)
//        {
//        }

//        //Scheme data
//        SpriteFont font;
//        Color fontColor;

//        public MultiLineLabel(IPanelContainer parent, Point location, Point size, int forceSize)
//           // : base(parent, location, size, null, inScheme)
//        {
//            this.Parent = parent;
//            this.Location = location;
//            this.Size = size;


//            forceLabelSize = forceSize;
//            SetupLabels();
//        }

//        public void Clear()
//        {
//            for (int i = 0; i < labels.Length; i++)
//                labels[i].Caption = "";
//        }

//        public override void ApplyScheme(IScheme scheme)
//        {
//            base.ApplyScheme(scheme);
//            font = scheme.GetFont("default_font");
//            //SetupLabels();
//            if (labels != null)
//            {
//                for (int i = 0; i < labels.Length; i++)
//                    labels[i].FontColor = Color.Black;
//            }
//        }

//        /// <summary>
//        /// This function takes our current size, and creates X labels, 1 for each line
//        /// </summary>
//        void SetupLabels()
//        {

//            int size = 0;
//            if (forceLabelSize != -1)
//                size = forceLabelSize;
//            else if (font == null)
//                size = 16;
//            else
//                size = (int)font.MeasureString("W").Y;


//            fontHeight = size;

//            int numLabels = this.Size.Y / fontHeight;

//            labels = new Label[numLabels];
//            for (int i = 0; i < numLabels; i++)
//            {
//                //labels[i] = new Label(this, new Point(0, i * size), new Point(this.Size.X, size),
//                //    font, Scheme.DefaultFontColor, Scheme);
//                labels[i] = new Label();
//                labels[i].Parent = this;
//                labels[i].Location = new Point(0, i * size);
//                labels[i].Size = new Point(this.Size.X, size);
//                labels[i].Font = font;
//                labels[i].FontColor = Color.Black;



//                //TODO: Fix this - setting this causes problems, no matter what the value!
//                //labels[i].VerticalAlignment = VerticalAlignment.Bottom;
//                //labels[i].Caption = i.ToString();
//            }

//        }

//        /// <summary>
//        /// Returns the string position of the item
//        /// </summary>
//        /// <param name="labelIndex"></param>
//        /// <param name="relativeX"></param>
//        /// <returns></returns>
//        int PositionHelper(int labelIndex, int relativeX)
//        {
//            string sz = labels[labelIndex].Caption;
//            int i = sz.Length;

//            while (relativeX < labels[labelIndex].MeasureStringWidth(sz))
//            {
//                i--;
//                sz = sz.Substring(0, i);
//            }

//            return i;
//        }

//        public Point GetStringCoordinatesFromMouse(Point mouseCoords)
//        {
//            //Figure out which label this corresponds to
//            int yLabel = ((mouseCoords.Y - this.AbsoluteLocation.Y) / fontHeight);
//            if (yLabel >= this.Count)
//                yLabel = this.Count - 1;

//            int xLabel = PositionHelper(yLabel, mouseCoords.X - this.AbsoluteLocation.X);

//            return new Point(xLabel, yLabel);
//        }


//    }


//}
