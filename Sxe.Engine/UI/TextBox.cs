//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Globalization;

//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;
//using Microsoft.Xna.Framework.Input;

//using Sxe.Engine.Input;

//namespace Sxe.Engine.UI
//{
//    public class TextBox : ScrollablePanel
//    {
//        MultiLineLabel text;
//        Panel textBox;
//        Panel activeTextBox;

        
//        public const char NewLineChar = '\n';
//        public static string NewLine = NewLineChar.ToString();

//        string textString;
//        bool clicked; //=false

//        Point cursorPosition = Point.Zero; //position of cursor
//        Point endSelection = new Point(-1, -1); //selection end

//        double nextBlink = 0.0;
//        bool showCursor = false;
//        bool readOnly = false;

//        List<string> labelData;

//        int maxLines;

//        //Scheme stuff
//        float cursorBlinkDelay = 0.1f;

//        SpriteFont font = null;


//        public Point StartSelection
//        {
//            get 
//            {
//                if (endSelection.X >= 0 && endSelection.Y >= 0)
//                {
//                    if (cursorPosition.Y < endSelection.Y)
//                    {
//                        return cursorPosition;
//                    }
//                    else if (endSelection.Y < cursorPosition.Y)
//                    {
//                        return endSelection;
//                    }
//                    else if (endSelection.X < cursorPosition.X)
//                    {
//                        return endSelection;
//                    }
//                    return cursorPosition;
//                }
//                return new Point(-1, -1);
//            } 
//        }

//        public Point EndSelection
//        {
//            get
//            {
//                if (endSelection.X >= 0 && endSelection.Y >= 0)
//                {
//                    if (cursorPosition.Y < endSelection.Y)
//                    {
//                        return endSelection;
//                    }
//                    else if (endSelection.Y < cursorPosition.Y)
//                    {
//                        return cursorPosition;
//                    }
//                    else if (endSelection.X < cursorPosition.X)
//                    {
//                        return cursorPosition;
//                    }
//                    return endSelection;
//                }
//                return new Point(-1, -1);
//            }
//        }

//        public bool HasSelection
//        {
//            get
//            {
//                return (StartSelection.X >= 0 && StartSelection.Y >= 0 && EndSelection.X >= 0 && EndSelection.Y >= 0);
//            }
//        }

//        public string Text
//        {
//            get { return textString; }
//            set { textString = value; Refresh(); }
//        }
//        public bool ReadOnly
//        {
//            get { return readOnly; }
//            set { readOnly = value; }
//        }


//        //public TextBox(IPanelContainer parent, Point location, Point size, IScheme inScheme, int inMaxLines)
//        //    : base(parent, location, size, null, inScheme)
//        //{
//        //    //labelData = new string[maxLines];
//        //    //for (int i = 0; i < labelData.Length; i++)
//        //    //    labelData[i] = "";
//        //    labelData = new List<string>();
//        //    maxLines = inMaxLines;

//        //    textBox = new BorderPanel(this, Point.Zero, size, Scheme.TextBoxImage, BorderPanelMode.Resize);
//        //    textBox.Visible = true;

//        //    activeTextBox = new BorderPanel(this, Point.Zero, size, Scheme.TextBoxActiveImage, BorderPanelMode.Resize);
//        //    activeTextBox.Visible = false;

//        //    text = new MultiLineLabel(this, Point.Zero, size, Scheme);

//        //    this.DisplaySize = new Point(1, text.Count);
//        //    this.FullSize = new Point(1, maxLines);

//        //    this.KeyPress += OnKeyPress;
//        //    this.MouseClick += OnMouseClick;
//        //    this.MouseClickRelease += OnUnClick;
//        //    this.MouseDown += OnMouseDown;
//        //    this.Scroll += OnScroll;

//        //    FireDisplayChanged();

//        //    Text = "Hey\nHey\nYou\nYou\n";
//        //}

//        public TextBox(int inMaxLines)
//        {
//            //labelData = new string[maxLines];
//            //for (int i = 0; i < labelData.Length; i++)
//            //    labelData[i] = "";
//            labelData = new List<string>();
//            maxLines = inMaxLines;

//           // textBox = new Panel(this, Point.Zero, size, Scheme.TextBoxImage, BorderPanelMode.Resize);
//            textBox = new Panel();
//            textBox.Parent = this;
//            textBox.Location = Point.Zero;
//            textBox.Visible = true;

//            //activeTextBox = new Panel(this, Point.Zero, size, Scheme.TextBoxActiveImage, BorderPanelMode.Resize);
//            activeTextBox = new Panel();
//            activeTextBox.Parent = this;
//            activeTextBox.Location = Point.Zero;
//            activeTextBox.Visible = false;


//            this.DisplaySize = new Point(1, 1);
//            this.FullSize = new Point(1, 1);

//            this.KeyPress += OnKeyPress;
//            this.MouseClick += OnMouseClick;
//            this.MouseClickRelease += OnUnClick;
//            this.MouseDown += OnMouseDown;
//            this.Scroll += OnScroll;
//            this.SizeChanged += OnResize;

//            FireDisplayChanged();

//            Text = "Hey\nHey\nYou\nYou\n";
//        }

//        void OnResize(object sender, EventArgs args)
//        {
//            textBox.Size = this.Size;
//            activeTextBox.Size = this.Size;

//            if (text != null)
//                text.Parent = null;

//            text = new MultiLineLabel(this, Point.Zero, Size);

//            if (text.HasFont)
//            {
//                this.DisplaySize = new Point(1, text.Count);
//                Refresh();
//            }

//            //text = new MultiLineLabel(this, Point.Zero, Size);

//            //text.Size = this.Size;
            
//        }

//        public override void ApplyScheme(IScheme scheme)
//        {
//            base.ApplyScheme(scheme);

//            textBox.Image = scheme.GetImage("textbox_default");
//            activeTextBox.Image = scheme.GetImage("textbox_active");
//            this.font = scheme.GetFont("default_font");

   
            

            
//        }

//        void OnMouseDown(object value, MouseEventArgs args)
//        {
//            if (clicked && Enabled && text != null)
//            {
//                Point temp = GetStringCoordinatesFromMouse(args.Position);

//                endSelection = temp;
//                UpdateSelection();
//                Invalidate();
//            }
//        }

//        void OnMouseClick(object value, MouseEventArgs arg)
//        {

            

//            if (Enabled && text != null)
//            {
//                cursorPosition = GetStringCoordinatesFromMouse(arg.Position);

//                clicked = true;
//                endSelection = new Point(-1, -1);
//                UpdateSelection();

//                Invalidate();
//            }
//        }

//        Point GetStringCoordinatesFromMouse(Point position)
//        {
//            Point start = text.GetStringCoordinatesFromMouse(position);
//            return new Point(start.X + DisplayCoordinates.X, start.Y + DisplayCoordinates.Y);
//        }

//        void OnUnClick(object value, MouseEventArgs args)
//        {
//            clicked = false;
//        }

//        void OnKeyPress(object value, KeyEventArgs args)
//        {
//            //textString += args.Key.ToString();

//            if (Enabled && !ReadOnly)
//            {
//                if (args.IsSpecialKey)
//                {
//                    if (args.Key == Keys.Enter)
//                    {
//                        HandleKeyInput("\n");
//                    }
//                    else if (args.Key == Keys.Space)
//                    {
//                        HandleKeyInput(" ");
//                    }

//                }
//                else
//                {

//                    HandleKeyInput(args.KeyString);

//                }

//            }
//        }

//        void HandleKeyInput(string input)
//        {

//            int newCursorIndex = InsertString(input);
//            Refresh();

//            cursorPosition = GetStringCoordinatesFromIndex(newCursorIndex);

//            //Scroll down
//            if (cursorPosition.Y >= DisplayCoordinates.Y + DisplaySize.Y)
//            {
//                DoScroll(ScrollType.LargeIncrement);
//                Refresh();
//            }
//        }



//        void OnScroll(object value, EventArgs args)
//        {
//            Refresh();
//        }

//        void Refresh()
//        {
//            if (text == null)
//                return;

//            DistributeText();
//            UpdateLabels();
//            endSelection = new Point(-1, -1);
//            UpdateSelection();
//        }

//        void UpdateLabels()
//        {
//            for (int i = 0; i < text.Count; i++)
//                text[i].Caption = "";

//            for (int i = 0; i < DisplaySize.Y; i++)
//            {
//                if (i + DisplayCoordinates.Y < labelData.Count)
//                    text[i].Caption = labelData[i + DisplayCoordinates.Y];
//                else
//                    text[i].Caption = "";
//            }
//        }




//        Point GetStringCoordinatesFromIndex(int index)
//        {
//            Point returnPoint = new Point(0, 0);
//            for (int i = 0; i < labelData.Count; i++)
//            {
//                //If we are looking at a line that has nothing, put the cursor at the beginning of that line
//                if (labelData[i].Length == 0)
//                {
//                    returnPoint = new Point(0, i);
//                    break;
//                }
//                //Otherwise, if our index is past the current line, subtract the length of the line,
//                //and move on to the next
//                else if (index > labelData[i].Length)
//                {
//                    index -= labelData[i].Length;
//                }
//                //If our index is less then the current line, then our position is in the current line,
//                //with X = index
//                else
//                {
//                    returnPoint = new Point(index, i);
//                    break;
//                }
//            }

//            return returnPoint;
//        }

//        /// <summary>
//        /// Gets the position (index) of a character based on a row and column of the charcter
//        /// </summary>
//        /// <param name="textPoint"></param>
//        /// <returns></returns>
//        int GetStringPosition(Point textPoint)
//        {
//            //textPoint = new Point(textPoint.X + DisplayCoordinates.X, textPoint.Y + DisplayCoordinates.Y);

//            int returnValue = 0;
//            if (textPoint.X >= 0 && textPoint.Y >= 0)
//            {
//                for (int i = 0; i <= textPoint.Y; i++)
//                {
//                    if (i != textPoint.Y)
//                    {
//                        if (i < labelData.Count)
//                        {
//                            returnValue += labelData[i].Length;

//                            //New line characters get double counted, so subtract one if one is present
//                            if (labelData[i].Contains(NewLine))
//                                returnValue -= NewLine.Length - 1;
//                        }
//                    }
//                    else
//                        returnValue += textPoint.X;
//                }
//            }
//            return returnValue;
//        }

//        /// <summary>
//        /// Inserts a string at the cursor position. Returns the index where the new cursor should be.
//        /// </summary>
//        int InsertString(string sz)
//        {
//            //TODO: Refactor the logic for finding the string index into a function

//            int start = 0;
//            int end = 0;

//            if(HasSelection)
//            {
//                start = GetStringPosition(StartSelection);
//                end = GetStringPosition(EndSelection);
//            }
//            else
//            {
//                end = start = GetStringPosition(cursorPosition);
//            }


//            string beginning = textString.Substring(0, start);
//            string endString = textString.Substring(end, textString.Length - end);

//            textString = beginning + sz + endString;

//            return beginning.Length + sz.Length;

//        }



//        void UpdateSelection()
//        {
//            //Loop through all textboxes and unselect
//            for (int i = 0; i < text.Count; i++)
//                text[i].Unselect();

//            if (StartSelection.X >= 0 && StartSelection.Y >= 0
//                && EndSelection.X >= 0 && EndSelection.Y >= 0)
//            {
//                int normalizedStartY = StartSelection.Y - DisplayCoordinates.Y;
//                int normalizedEndY = EndSelection.Y - DisplayCoordinates.Y;

//                if (normalizedEndY < 0)
//                    return;

//                //Hack to fix crash when you scroll with a selection - not sure what causes it yet
//                if (normalizedEndY >= DisplaySize.Y)
//                {
//                    normalizedEndY = DisplaySize.Y - 1;
//                }

//                for (int i = (int)MathHelper.Max(0, normalizedStartY); i <= normalizedEndY; i++)
//                {
//                    if (i == normalizedStartY && i == normalizedEndY)
//                    {
//                        text[i].Selection = new Point(StartSelection.X, EndSelection.X);
//                    }
//                    else if (i == normalizedStartY)
//                    {
//                        text[i].SelectAll(StartSelection.X);
//                    }
//                    else if (i == normalizedEndY)
//                    {
//                        text[i].Selection = new Point(0, EndSelection.X);
//                    }
//                    else
//                    {
//                        text[i].SelectAll();
//                    }
//                }

//            }
//        }

//        /// <summary>
//        /// This function distributes the text, taking into account newlines
//        /// </summary>
//        void DistributeText()
//        {
//             //Loop through and clear all labels first
//            //for (int i = 0; i < labelData.Count; i++)
//            //    labelData[i] = "";

//            if (text == null)
//                return;

//            labelData.Clear();

//            //First, split the string based on newline characters
//            string[] strings = textString.Split(new char[] { NewLineChar });

//            int startIndex = 0;

//            for (int i = 0; i < strings.Length; i++)
//            {
//                string wholeString = strings[i];
//                if (i != 0)
//                    wholeString = NewLine + wholeString;

//                startIndex = DistributeLine(wholeString, startIndex);
//                startIndex++; //skip to next line because there is a newline character in there
//            }

//            //Now, loop through labeldata, and remove any previous lines, and remove them from textString as well
//            while (labelData.Count > maxLines)
//            {
//                //TODO: Test if this is correct!
//                textString = textString.Substring(labelData[0].Length, textString.Length - labelData[0].Length);

//                labelData.RemoveAt(0);

//            }

//            //Assign FullSize here
//            FullSize = new Point(FullSize.X, labelData.Count);
            
//        }


//        /// <summary>
//        /// This function takes a start string, and distributes it along the lines, based on the start index.
//        /// Returns the ending index. This function is intended to distribute lines of text that may overflow
//        /// and go to another line.
//        /// </summary>
//        int DistributeLine(string startString, int startIndex)
//        {


//            int index = startIndex;

//            string remainder = DistributeLineHelper(index, startString);
//            while (remainder != "")
//            {
//                index++;
//                remainder = DistributeLineHelper(index, remainder);
//            }

//            //FullSize = new Point(FullSize.X, index);
//            return index;
//        }

//        /// <summary>
//        /// Distributes the text in the label with specified index. Returns the string that was left over.
//        /// </summary>
//        string DistributeLineHelper(int index, string sz)
//        {
//            string before = sz;
//            string after = "";
//            int i = sz.Length;

//            while (!text[0].DoesStringFit(before))
//            {
//                before = sz.Substring(0, i);
//                after = sz.Substring(i, sz.Length - i);
//                i--;
//            }

//            //if(index >= 0 && index < labelData.Count)
//            //labelData[index] = before;
//            labelData.Add(before);

//            return after;
//        }

//        public override void Update(GameTime gameTime)
//        {
//            double time = gameTime.TotalGameTime.TotalSeconds;
//            if (time > nextBlink)
//            {
//                Invalidate();
//                showCursor = !showCursor;
//                nextBlink = time + cursorBlinkDelay;
//            }


//            base.Update(gameTime);
//        }

//        public override void Paint(SpriteBatch sb, Point positionOffset, Vector2 scale)
//        {

//            base.Paint(sb, positionOffset, scale);

//            if (text == null)
//                return;

//            int normalizedCursorPosition = cursorPosition.Y - DisplayCoordinates.Y;

//            if (normalizedCursorPosition >= 0 && normalizedCursorPosition < text.Count && showCursor && HasFocus)
//            {
//                string cursorString = text[normalizedCursorPosition].Caption.Substring(0, (int)MathHelper.Min(text[normalizedCursorPosition].Caption.Length,
//                    cursorPosition.X));


//                Vector2 position = new Vector2(
//                    positionOffset.X + this.Location.X + text[normalizedCursorPosition].MeasureStringWidth(cursorString) - 3,
//                    positionOffset.Y + this.Location.Y + (normalizedCursorPosition) * text.FontHeight);
//                position *= scale;

//                if(font != null)
//                sb.DrawString(font, "|",position , Color.Black);
//            }
//        }
//    }
//}
