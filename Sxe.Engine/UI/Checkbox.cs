using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Sxe.Engine.Input;

namespace Sxe.Engine.UI
{
    public class Checkbox : Panel
    {
        //Checkbox images
        UIImage defaultImage;
        UIImage overImage;

        //Check image
        UIImage checkImage;

        //Panel for the check box & check
        Panel checkbox;
        Panel check;
        //Label for the description
        Label label;

        bool checkedValue;

        public bool Checked
        {
            get { return checkedValue; }
            set 
            {
                checkedValue = value;
                ResetCheck();
            }
        }

        //public string Text
        //{
        //    get { return label.Caption; }
        //    set { label.Caption = value; }
        //}

        public Label Caption
        {
            get { return label; }
        }
        


        public Checkbox()
        {
            //defaultImage = defaultBox;
            //overImage = overBox;
            //checkImage = checkImg;



            //checkbox = new Panel(this, new Point(0, offset), size, defaultImage);
            checkbox = new Panel();
            checkbox.Parent = this;


            //check = new Panel(this, new Point(0, offset), size, checkImage);
            //check.Visible = false;
            check = new Panel();
            check.Parent = this;

            //label = new Label(this, new Point(size.X + labelOffset.X, labelOffset.Y),
            //    labelSize, textFont, textColor, scheme);
            //label.AutofitText();
            label = new Label();
            label.Parent = this;

            //Point adjSize = new Point(size.X + labelOffset.X + label.Size.X, size.Y + labelOffset.Y + label.Size.Y);
            //this.Size = adjSize;

            MouseClick += OnClick;
        }

        //public Checkbox(IPanelContainer parent, Point position, Point size,
        //    SpriteFont textFont, string text, Color textColor, Point labelOffset, Point labelSize,
        //    UIImage defaultBox, UIImage overBox, UIImage checkImg, IScheme scheme)
        //    : base(parent, position, new Point(size.X + labelOffset.X + labelSize.X, size.Y + labelOffset.Y + labelSize.Y)
        //    , defaultBox)
        //{
        //    defaultImage = defaultBox;
        //    overImage = overBox;
        //    checkImage = checkImg;

        //    int offset = 0;
        //    if (labelSize.Y > size.Y)
        //    {
        //        offset = (labelSize.Y - size.Y) / 2;
        //    }


        //    checkbox = new Panel(this, new Point(0, offset), size, defaultImage);
        //    check = new Panel(this, new Point(0, offset), size, checkImage);
        //    check.Visible = false;

        //    label = new Label(this, new Point(size.X + labelOffset.X, labelOffset.Y),
        //        labelSize, textFont, textColor, scheme);
        //    label.AutofitText();

        //    Point adjSize = new Point(size.X + labelOffset.X + label.Size.X, size.Y + labelOffset.Y + label.Size.Y);
        //    this.Size = adjSize;

        //    MouseClick += OnClick;
        //}

        //public override void ApplyScheme(IScheme scheme)
        //{
        //    UIImage schemeDefault = scheme.GetImage("checkbox_default");
        //    UIImage schemeOver = scheme.GetImage("checkbox_default");
        //    UIImage schemeCheck = scheme.GetImage("checkbox_check");

        //    if(schemeDefault != null)
        //    defaultImage = schemeDefault;

        //    if(schemeOver != null)
        //    overImage = schemeOver;

        //    if (schemeCheck != null)
        //    checkImage = schemeCheck;
        //}

        void OnClick(object value, MouseEventArgs args)
        {
            Checked = !Checked;

        }

        void ResetCheck()
        {
            check.Visible = checkedValue;
            Invalidate();
        }

    }
}
