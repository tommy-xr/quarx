using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sxe.Engine.UI
{
    public enum HorizontalAlignment
    {
        Left = 0,
        Center,
        Right
    }

    public enum VerticalAlignment
    {
        Top = 0,
        Middle,
        Bottom
    }


    /// <summary>
    /// A label is a simple panel that only displays text
    /// </summary>
    public class Label : Panel
    {
        #region Fields
        SpriteFont font;
        string fontPath;
        string text;
        Color fontColor = Color.White;
        Color highlightColor = Color.Red;
        float scale; //the scale factor is the amount to scale to get the text to fit vertically
        int maxCharacters; //the max characters this label can display
        HorizontalAlignment horizontalAlign;
        VerticalAlignment verticalAlign;
        Vector2 textPos; //the actual position where the text should be drawn
        int stringSize; //the width of the current string, in pixels
        Point selection = new Point(-1, -1); //determines the start & end of selection
        Texture2D selectionTexture;

        #endregion

        #region Properties

#if !XBOX
        [TypeConverter(typeof(Sxe.Design.FontContentNameConverter))]
#endif
        [ReloadContent]
        public string FontPath
        {
            get { return fontPath; }
            set { fontPath = value; }
        }

        public SpriteFont Font
        {
            get { return font; }
            set { font = value; ResetScale(false); ResetPosition(); }
        }
        public string Caption
        {
            get { return text; }
            set 
            { 
                text = value;
                if (text == null) text = "";
                ResetScale(false);  
                ResetPosition(); 
            }
        }
        public Color FontColor
        {
            get { return fontColor; }
            set { fontColor = value; }
        }
        public HorizontalAlignment HorizontalAlignment
        {
            get { return horizontalAlign; }
            set { horizontalAlign = value; ResetPosition(); }
        }
        public VerticalAlignment VerticalAlignment
        {
            get { return verticalAlign; }
            set { verticalAlign = value; ResetPosition();  }
        }
        public int MaxCharacters
        {
            get { return maxCharacters; }
        }
        public int CaptionSize
        {
            get { return stringSize; }
        }
        public int CharacterSize
        {
            get { return stringSize / text.Length; }
        }
        public Point Selection
        {
            get { return selection; }
            set { selection = value; }
        }
        #endregion

        public void SelectAll()
        {
            SelectAll(0);
        }

        public void SelectAll(int start)
        {
            selection = new Point(start, text.Length + 1);
        }

        public void Unselect()
        {
            selection = new Point(-1, -1);
        }

        public Label()
        {
            //this.fontColor = color;
            //this.font = font;
            this.text = "";
            //ResetScale(false);
            //ResetPosition();

            this.SizeChanged += OnSizeChanged;
        }

        //public Label(IPanelContainer parent, Point position, Point size, SpriteFont font, Color color, IScheme inScheme)
        //    : base(parent, position, size, null, inScheme)
        //{
        //    //this.fontColor = color;
        //    //this.font = font;
        //    this.text = "";
        //    //ResetScale(false);
        //    //ResetPosition();

        //    this.SizeChanged += OnSizeChanged;
        //}

        //public override void ApplyScheme(IScheme scheme)
        //{
        //    base.ApplyScheme(scheme);
        //    this.font = scheme.GetFont("default_font");
        //    this.fontColor = scheme.GetColor("default_font_color");
        //    this.selectionTexture = scheme.GetTexture("blank");
        //    this.highlightColor = scheme.GetColor("highlight_color");
        //    ResetScale(false);
        //    ResetPosition();
        //}

        public void OnSizeChanged(object sender, EventArgs args)
        {
            ResetPosition();
        }

        public override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            if (fontPath != null)
                font = content.Load<SpriteFont>(fontPath);
            base.LoadContent(content);
        }

        /// <summary>
        /// This functions resizes the label to fit text
        /// </summary>
        public void AutofitText()
        {
            ResetScale(true);

        }

        void ResetScale(bool autofit)
        {
            //Measure an arbitrary string to get an idea of how big of a string we can have
            if (font == null)
                return;

            Vector2 currentSize = font.MeasureString(text);
            Vector2 result = font.MeasureString("W"); //W is just chosen for fun

            scale = (float)Size.Y / result.Y;
            if (scale > 1.0f) //clamp yscale, for now
                scale = 1.0f;

            maxCharacters = text.Length + (int)((  (Size.X - 10.0f - (currentSize.X*scale)) / (result.X * scale)));

            Point p = Size;
            p.X = (int)(currentSize.X * scale) + 5;

            if (autofit)
            {

                Size = p;
            }

            stringSize = p.X;
            
        }

        public int MeasureStringWidth(string sz)
        {
            if (font == null)
                return -1;

            return (int)font.MeasureString(sz).X;
        }
        public int MeasureStringHeight(string sz)
        {
            if (font == null)
                return -1;

            return (int)font.MeasureString(sz).Y;
        }

        public bool DoesStringFit(string sz)
        {
            if (font == null)
                return false;

            Vector2 result = font.MeasureString(sz);
            if ((int)result.X > this.Size.X)
                return false;

            return true;
        }

        /// <summary>
        /// ResetPosition handles the aligning of text
        /// </summary>
        void ResetPosition()
        {
            if (font == null)
                return;

            ResetScale(false);

            Vector2 fontSize = Vector2.Zero;
            if(Caption != null)
                fontSize = font.MeasureString(Caption);
            fontSize *= scale;

            float xPos, yPos;
            switch (HorizontalAlignment)
            {
                default:
                case HorizontalAlignment.Left:
                    xPos = 0;
                    break;
                case HorizontalAlignment.Center:
                    xPos = this.Size.X / 2 - (int)(fontSize.X / 2.0f);
                    break;
                case HorizontalAlignment.Right:
                    xPos = this.Size.X - (int)fontSize.X;
                    break;
            }

            switch (VerticalAlignment)
            {
                default:
                case VerticalAlignment.Top:
                    yPos = 0;
                    break;
                case VerticalAlignment.Middle:
                    yPos = this.Size.Y / 2 - (int)(fontSize.Y / 2.0f);
                    break;
                case VerticalAlignment.Bottom:
                    yPos = this.Size.Y - (int)fontSize.Y;
                    break;
            }

            this.textPos = new Vector2(xPos, yPos);

            Invalidate();
        }

        public override void Paint(SpriteBatch sb, Point positionOffset, Vector2 scale)
        {
            if (!Visible)
                return;

            base.Paint(sb, positionOffset, scale);

            if (font != null)
            {

                //Only draw a selection if we have a string
                if (selection.X != -1 && selection.Y != -1 && this.Caption.Length > 0)
                {
                    int beginningLength = 0;
                    int selectLength = 0;

                    string begin = text.Substring(0, selection.X);
                    beginningLength = (int)font.MeasureString(begin).X;

                    if (selection.X < text.Length)
                    {
                        string sz = text.Substring(selection.X, (int)MathHelper.Min(text.Length - selection.X, selection.Y - selection.X));
                        selectLength = (int)font.MeasureString(sz).X;
                    }


                    Rectangle selectionRectangle = new Rectangle(
                        (int)(this.Location.X + positionOffset.X + beginningLength), 
                        (int)(this.Location.Y + positionOffset.Y),
                        selectLength + CharacterSize / 4, (int)this.Size.Y);
                    selectionRectangle.X = (int)(selectionRectangle.X * scale.X);
                    selectionRectangle.Y = (int)(selectionRectangle.Y * scale.Y);
                    selectionRectangle.Width = (int)(selectionRectangle.Width * scale.X);
                    selectionRectangle.Height = (int)(selectionRectangle.Height * scale.Y);

                    //TODO: Get selection color
                    if (selectionTexture != null)
                        sb.Draw(selectionTexture, selectionRectangle, highlightColor);
                }

                //Don't show newlines in the text
                string printText = text.Replace("\n", "");
                Vector2 position = new Vector2(this.Location.X + positionOffset.X + textPos.X, this.Location.Y + positionOffset.Y + textPos.Y);
                position *= scale;

                sb.DrawString(font, printText, position,
                    this.fontColor, 0.0f, Vector2.Zero, scale, SpriteEffects.None, 0.0f);


            }

           
        }

    }
}
