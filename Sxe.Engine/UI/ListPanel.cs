//using System;
//using System.Collections.Generic;
//using System.Text;

//using Microsoft.Xna.Framework;

//using Sxe.Engine.Input;

//namespace Sxe.Engine.UI
//{
//    public class ListPanel<T> : ScrollablePanel where T : class
//    {

//        Panel background;
//        MultiLineLabel labels;
//        bool allowMultiSelect;
//        EventList<T> dataSource;
//        T selected;
//        string[] displayData;

//        public event EventHandler SelectionChanged;

//        public bool AllowMultiSelect
//        {
//            get { return allowMultiSelect; }
//            set { allowMultiSelect = value; }
//        }

//        public EventList<T> DataSource
//        {
//            get { return dataSource; }
//            set {
//                if (dataSource != null)
//                    dataSource.ListModified -= OnModified; 
//                dataSource = value;
//                dataSource.ListModified += OnModified;
//                RefreshList(); 
//            }
//        }

//        public T Selected
//        {
//            get { return selected; }
//        }

//        //public ListPanel(IPanelContainer parent, Point location, Point size, IScheme scheme)
//        //    : base(parent, location, size, null, scheme)
//        //{
//        //    background = new BorderPanel(this, Point.Zero, size, scheme.TextBoxImage, BorderPanelMode.Resize);
//        //    labels = new MultiLineLabel(this, Point.Zero, size, scheme);

//        //    DisplayCoordinates = new Point(0, 0);
//        //    DisplaySize = new Point(1, labels.Count);

//        //    this.MouseClick += OnClick;
//        //    this.Scroll += OnScroll;
//        //}

//        public ListPanel()
//        {
//            //background = new BorderPanel(this, Point.Zero, size, scheme.TextBoxImage, BorderPanelMode.Resize);
//            background = new Panel();
//            background.Parent = this;
//            background.Location = this.Location;
            
//            labels = new MultiLineLabel(this, Point.Zero, Size);

//            DisplayCoordinates = new Point(0, 0);
//            DisplaySize = new Point(1, labels.Count);

//            this.MouseClick += OnClick;
//            this.Scroll += OnScroll;
//            SizeChanged += OnResize;
//        }

//        void OnResize(object sender, EventArgs args)
//        {
//            background.Size = this.Size;
//        }

//        void OnModified(object list, EventArgs args)
//        {
//            RefreshList();
//        }

//        void OnClick(object value, MouseEventArgs args)
//        {
//            //Find which label we clicked
//            Point label = labels.GetStringCoordinatesFromMouse(args.Position);

//            selected = dataSource[DisplayCoordinates.Y + label.Y];
//            if (SelectionChanged != null)
//                SelectionChanged(this, null);

//            RefreshList();

//            Invalidate();

//        }

//        void OnScroll(object value, EventArgs args)
//        {
//            RefreshList();
//        }

//        void RefreshList()
//        {
//            labels.Clear();
//            if (DataSource == null)
//                return;

//            //Populate our data "model"
//            displayData = new string[DataSource.Count];
//            for (int i = 0; i < displayData.Length; i++)
//            {
//                if (i < DataSource.Count)
//                    displayData[i] = DataSource[i].ToString();
//                else
//                    displayData[i] = "";
//            }

//            //Update our "view" of the data
//            for (int i = DisplayCoordinates.Y; i < DisplayCoordinates.Y + DisplaySize.Y; i++)
//            {
//                int normalized = i - DisplayCoordinates.Y;
//                if (i < displayData.Length && normalized < labels.Count)
//                    labels[normalized].Caption = displayData[i];
//                else
//                    labels[normalized].Caption = "";

//                if (i < dataSource.Count && dataSource[i] == Selected)
//                    labels[normalized].SelectAll();
//                else
//                    labels[normalized].Unselect();
//            }

//            this.FullSize = new Point(this.FullSize.X, displayData.Length);
//            Invalidate();
//        }
//    }
//}
