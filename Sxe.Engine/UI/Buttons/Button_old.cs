//using System;
//using System.Collections.Generic;
//using System.Text;

//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;

//namespace Sxe.Engine.UI
//{
//    public class Button : Panel
//    {

//        Panel defaultPanel;
//        Panel overPanel;
//        Panel clickPanel;

//        Label label;

//        public string Text
//        {
//            get { return label.Caption; }
//            set { label.Caption = value; }
//        }

//        public UIImage DefaultImage
//        {
//            get { return defaultPanel.Image; }
//            set { defaultPanel.Image = value;}
//        }

//        public UIImage OverImage
//        {
//            get { return overPanel.Image; }
//            set { overPanel.Image = value; }
//        }

//        public UIImage ClickImage
//        {
//            get { return clickPanel.Image; }
//            set { clickPanel.Image = value; }
//        }

//        public Label Label
//        {
//            get { return label; }
//        }



//        //public Button(IPanelContainer parent, Point position, Point size)
//        //    : base(parent, position, size, null)
//        public Button()
//        {

//            //defaultPanel = new BorderPanel(this, Point.Zero, size, button, mode);
//            //defaultPanel.Visible = true;

//            //overPanel = new BorderPanel(this, Point.Zero, size, over, mode);
//            //overPanel.Visible = false;

//            //clickPanel = new BorderPanel(this, Point.Zero, size, click, mode);
//            //clickPanel.Visible = false;

//            defaultPanel = new Panel();
//            defaultPanel.Parent = this;
//            defaultPanel.Location = Point.Zero;
//            defaultPanel.Visible = true;


//            overPanel = new Panel();
//            overPanel.Parent = this;
//            overPanel.Location = Point.Zero;
//            overPanel.Visible = false;

//            clickPanel = new Panel();
//            clickPanel.Parent = this;
//            clickPanel.Location = Point.Zero;
//            clickPanel.Visible = false;

//            label = new Label();
//            label.Parent = this;
//            label.Location = Point.Zero;
//            label.HorizontalAlignment = HorizontalAlignment.Center;
//            label.VerticalAlignment = VerticalAlignment.Middle;

//            //label = new Label(this, Point.Zero, size, font, fontColor, scheme);
//            //label.HorizontalAlignment = HorizontalAlignment.Center;
//            //label.VerticalAlignment = VerticalAlignment.Middle;


//            MouseEnter += OnMouseOver;
//            MouseLeave += OnMouseLeave;
//            MouseClick += OnMouseClick;
//            MouseClickRelease += OnMouseUnclick;
//            SizeChanged += OnResize;
//        }

//        void OnResize(object value, EventArgs args)
//        {
//            defaultPanel.Size = this.Size;
//            overPanel.Size = this.Size;
//            clickPanel.Size = this.Size;
//            label.Size = this.Size;
//        }

//        //public override void ApplyScheme(IScheme scheme)
//        //{
//        //    base.ApplyScheme(scheme);

//        //    defaultPanel.Image = scheme.GetImage("button_default");
//        //    overPanel.Image = scheme.GetImage("button_over");
//        //    clickPanel.Image = scheme.GetImage("button_click");
//        //}


//        void OnMouseOver(object value, EventArgs eventArg)
//        {
//            defaultPanel.Visible = false;
//            clickPanel.Visible = false;
//            overPanel.Visible = true;
//            Invalidate();
//        }

//        void OnMouseLeave(object value, EventArgs eventArg)
//        {
//            clickPanel.Visible = false;
//            overPanel.Visible = false;
//            defaultPanel.Visible = true;
//            Invalidate();
//        }

//        void OnMouseClick(object value, EventArgs eventArg)
//        {
//            defaultPanel.Visible = false;
//            overPanel.Visible = false;
//            clickPanel.Visible = true;
//            Invalidate();
//        }

//        void OnMouseUnclick(object value, EventArgs eventArg)
//        {
//            OnMouseOver(value, eventArg);
//        }


//    }
//}
