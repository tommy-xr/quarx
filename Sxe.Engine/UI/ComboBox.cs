//using System;
//using System.Collections.Generic;
//using System.Text;

//using System.Globalization;

//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;

//using Sxe.Engine.Input;

//namespace Sxe.Engine.UI
//{
//    public class ComboBox : Panel
//    {
//        EventList<object> items;
//        int previousCount;
//        Button comboButton;
//        TextBox textbox;

//        Point arrowSize = new Point(16, 16);

//        ListPanel<object> subpanel;

//        public IList<object> Items
//        {
//            get 
//            { 
//                return items;
//            }
//        }

//        public TextBox TextBox
//        {
//            get { return textbox; }
//        }

//        public object Selected
//        {
//            get { return subpanel.Selected; }
//        }

//        public event EventHandler DataSourceChanged;


//        //public ComboBox(IPanelContainer parent, Point position, Point size)
//        //    : base(parent, position, size)
//        //{

//        //    textbox = new TextBox(this, Point.Zero, new Point(size.X - Scheme.ArrowSize.X, Scheme.ArrowSize.Y),
//        //        Scheme, 1);
//        //    comboButton = new Button(this, new Point(size.X - Scheme.ArrowSize.X, 0), Scheme.ArrowSize,
//        //        Scheme.DefaultFont, Scheme.ComboArrowImage, Scheme.ComboArrowOverImage, Scheme.ComboArrowDownImage,
//        //        Color.Black, BorderPanelMode.Stretch, Scheme);
//        //    items = new EventList<object>();

//        //    subpanel = new ListPanel<object>(this, new Point(0, Scheme.ArrowSize.Y),
//        //        new Point(size.X - Scheme.ArrowSize.X * 2, size.Y - Scheme.ArrowSize.Y), Scheme);
//        //    subpanel.DataSource = items;

//        //    comboButton.MouseClick += OnDropDownPress;
//        //    items.ListModified += OnModified;
//        //    subpanel.SelectionChanged += OnSelectionChanged;
//        //    this.MouseClickOutside += OnMouseClickOutside;



//        //}

//        public ComboBox()
//        {

            

//            //textbox = new TextBox(this, Point.Zero, new Point(size.X - Scheme.ArrowSize.X, Scheme.ArrowSize.Y),
//            //    Scheme, 1);
//            textbox = new TextBox(1);
//            textbox.Parent = this;
//            textbox.Location = Point.Zero;



//            //comboButton = new Button(this, new Point(size.X - Scheme.ArrowSize.X, 0), Scheme.ArrowSize,
//            //    Scheme.DefaultFont, Scheme.ComboArrowImage, Scheme.ComboArrowOverImage, Scheme.ComboArrowDownImage,
//            //    Color.Black, BorderPanelMode.Stretch, Scheme);
//            comboButton = new Button();
//            comboButton.Parent = this;
//            comboButton.Location = new Point(Size.X - arrowSize.X, 0);


//            items = new EventList<object>();

//            //subpanel = new ListPanel<object>(this, new Point(0, Scheme.ArrowSize.Y),
//            //    new Point(size.X - Scheme.ArrowSize.X * 2, size.Y - Scheme.ArrowSize.Y), Scheme);
//            //subpanel = new ListPanel<object>(this, new Point(
//            subpanel = new ListPanel<object>();
//            subpanel.Parent = this;
//            subpanel.Size = new Point(0, arrowSize.Y);


//            subpanel.DataSource = items;


//            comboButton.MouseClick += OnDropDownPress;
//            items.ListModified += OnModified;
//            subpanel.SelectionChanged += OnSelectionChanged;
//            this.MouseClickOutside += OnMouseClickOutside;
//            this.SizeChanged += OnSizeChanged;




//        }

//        void OnSizeChanged(object sender, EventArgs args)
//        {
//            subpanel.Size = new Point(Size.X - arrowSize.X * 2, Size.Y - arrowSize.Y);
//        }

//        public override void  ApplyScheme(IScheme scheme)
//        {
//             base.ApplyScheme(scheme);

//             UIImage comboArrow = scheme.GetImage("combo_arrow");
//             UIImage comboArrowOver = scheme.GetImage("combo_arrowover");
//             UIImage comboArrowDown = scheme.GetImage("combo_arrowclick");

//             if(comboArrow != null)
//                 comboButton.Image = comboArrow;
//             if(comboArrowOver != null)
//                 comboButton.OverImage = comboArrowOver;
//            if(comboArrowDown != null)
//                comboButton.ClickImage = comboArrowDown;

            
//        }

//        void OnSelectionChanged(object value, EventArgs args)
//        {
//            this.TextBox.Text = subpanel.Selected.ToString();
//        }

//        void OnMouseClickOutside(object value, EventArgs args)
//        {
//            if (subpanel.Visible)
//                subpanel.Visible = false;
//            Invalidate();
//        }

//        void OnModified(object list, EventArgs args)
//        {
//            if (DataSourceChanged != null)
//                DataSourceChanged(list, args);
//        }

//        void OnDropDownPress(object value, EventArgs args)
//        {
//            subpanel.Visible = true;
//        }

//        public override void Update(GameTime gameTime)
//        {
//            if (previousCount != items.Count)
//            {
//                if (DataSourceChanged != null)
//                    DataSourceChanged(this, null);
//                previousCount = items.Count;
//            }

//            base.Update(gameTime);
//        }
//    }



//}
