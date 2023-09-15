using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Sxe.Engine.UI
{
    public class TabButton : Button
    {
        TabPage page;
        /// <summary>
        /// The TabPage that this button refers to
        /// </summary>
        public TabPage Page
        {
            get { return page; }
            set { page = value; }
        }

        //public override void ApplyScheme(IScheme scheme)
        //{
        //    base.ApplyScheme(scheme);
        //    this.DefaultImage = scheme.GetImage("tab_default");
        //    this.OverImage = scheme.GetImage("tab_over");
        //    this.ClickImage = scheme.GetImage("tab_click");

        //    this.Label.FontColor = Color.Black;
        //} 

    }

    public class TabControl : Panel
    {
        EventList<TabPage> tabList = new EventList<TabPage>();
        TabPage selectedTab;

        public EventList<TabPage> Pages
        {
            get { return tabList; }
        }

        Point tabDimensions = new Point(50, 40);
        public Point TabDimensions
        {
            get { return tabDimensions; }
            set { tabDimensions = value; }
        }

        public TabControl()
        {
            tabList.ListModified += OnListChanged;
        }

        //public override void ApplyScheme(IScheme scheme)
        //{
        //    base.ApplyScheme(scheme);
        //    this.tabDimensions = scheme.GetPoint("tab_size");

        //    RefreshTabs();
        //}

        void OnListChanged(object sender, EventArgs args)
        {

            //If we don't have a selected tab yet, and there is a page now, make that selected
            if (tabList.Count > 0 && selectedTab == null)
                selectedTab = tabList[0];


            foreach (TabPage page in tabList)
            {
                //If this page does not have a button, lets make a button for it
                if (page.Button == null)
                {
                    TabButton tabButton = new TabButton();
                    tabButton.Parent = this;
                    tabButton.Page = page;

                    tabButton.Size = new Point(10, tabDimensions.Y);
                    page.Button = tabButton;
                    page.Button.MouseClick += OnButtonPressed;
                }   

                page.Parent = this;
            }

            RefreshTabs();


        }

        void OnButtonPressed(object sender, EventArgs args)
        {
            TabButton button = sender as TabButton;
            if (button != null)
            {
                selectedTab = button.Page;
                RefreshTabs();
            }
        }

        void RefreshTabs()
        {
            int startX = 0;

            int defaultWidth = tabDimensions.X;

            for (int i = 0; i < tabList.Count; i++)
            {
                TabPage page = tabList[i];

                int inactiveHeight = tabDimensions.Y - 2;
                int activeHeight = tabDimensions.Y;


                int yCoord = 0;
                if (selectedTab != page)
                    yCoord = 2;

                page.Location = new Point(0, tabDimensions.Y);
                page.Size = new Point(Size.X, Size.Y - tabDimensions.Y);

                page.Button.Location = new Point(startX, yCoord);
                //page.Button.Text = page.Text;
                page.Button.Size = new Point(defaultWidth, activeHeight - yCoord);
                page.Button.Text = page.Caption;

                page.Location = new Point(0, activeHeight);
                page.Size = new Point(Size.X, Size.Y - activeHeight);

                if (selectedTab == page)
                {
                    page.Visible = true;
                }
                else
                {
                    page.Visible = false;
                }

                startX += defaultWidth + 1;
            }

            Invalidate();
        }


    }

    public class TabPage : Panel
    {
        TabButton button;
        /// <summary>
        /// The button that controls this tabPage
        /// </summary>
        public TabButton Button
        {
            get { return button; }
            set { button = value; }
        }

        string caption;
        public string Caption
        {
            get { return caption; }
            set { caption = value; }
        }

        //public override void ApplyScheme(IScheme scheme)
        //{
        //    base.ApplyScheme(scheme);

        //    Texture2D blank = scheme.GetTexture("blank");
        //    this.Image = new UIImage(blank);
        //    this.BackColor = Color.Green;
        //}

    }


}


//{
//    public class TabControl : Panel
//    {
//        EventList<TabPage> tabList;
//        TabPage selectedTab;

//        public EventList<TabPage> Tabs
//        {
//            get { return tabList; }
//        }

//        public TabControl(IPanelContainer parent, Point location, Point size, IScheme scheme)
//            : base(parent, location, size, null, scheme)
//        {
//            tabList = new EventList<TabPage>();
//            tabList.ListModified += OnListChanged;
        
//        }

//        void OnButtonPressed(object sender, EventArgs args)
//        {
//            TabButton button = sender as TabButton;
//            if (button != null)
//            {
//                selectedTab = button.Parent as TabPage;
//                RefreshTabs();
//            }
//        }

//        void OnListChanged(object sender, EventArgs args)
//        {

//                //If we don't have a selected tab yet, and there is a page now, make that selected
//                if (tabList.Count > 0 && selectedTab == null)
//                    selectedTab = tabList[0];


//                foreach(TabPage page in tabList)
//                {
//                    //TODO: This code assumes that .NET ignores multiple event handlers
//                    page.Button.MouseClick += OnButtonPressed;

//                    page.Parent = this;
//                }

//                RefreshTabs();

            
//        }

//        void RefreshTabs()
//        {
//            int startX = 0;
            
//            int defaultWidth = (int)((float)this.Size.X / (float)tabList.Count);
            
//            for (int i = 0; i < tabList.Count; i++)
//            {
//                TabPage page = tabList[i];
              
//                int inactiveHeight = (int)Scheme.DefaultFont.MeasureString("W").Y;
//                int activeHeight = inactiveHeight + Scheme.TabSelectedHeightIncrease;


//                int yCoord = 0;
//                if(selectedTab != page)
//                    yCoord = Scheme.TabSelectedHeightIncrease;

//                page.Location = new Point(0, 0);
//                page.Size = new Point(Size.X, Size.Y);

//                page.Button.Location = new Point(startX, yCoord);
//                //page.Button.Text = page.Text;
//                page.Button.Size = new Point(defaultWidth, activeHeight - yCoord);

//                page.Panel.Location = new Point(0, activeHeight);
//                page.Panel.Size = new Point(Size.X, Size.Y - activeHeight);

//                if (selectedTab == page)
//                {
//                    page.Panel.Visible = true;
//                }
//                else
//                {
//                    page.Panel.Visible = false;
//                }

//                startX += defaultWidth + 1;
//            }

//            Invalidate();
//        }
//    }

//    public class TabButton : Panel
//    {
//        UIImage unselected;
//        UIImage selected;

//        public TabButton()
//        {
//        }

//        //public TabButton(IPanelContainer parent, Point location, Point size, UIImage unselectedImage, UIImage selectedImage, IScheme scheme)
//        //    : base(parent, location, size, unselectedImage, scheme)
//        //{
//        //    unselected = unselectedImage;
//        //    selected = selectedImage;
//        //}
//    }

//    public class TabPage : Panel
//    {
//        TabButton tabButton;
//        Panel tabPage;

//        public TabButton Button
//        {
//            get { return tabButton; }
//        }
//        public Panel Panel
//        {
//            get { return tabPage; } 
//        }

//        //public string Text
//        //{
//        //    get { return tabButton.Text; }
//        //    set { tabButton.Text = value; }
//        //}

//        //public TabPage(IScheme scheme)
//        //    : base(null, new Point(0, 0), new Point(1, 1), null, scheme)
//        public TabPage()
//        {
//            //tabButton = new Button(this, new Point(0, 0), new Point(1, 1), scheme.DefaultFont,
//            //    scheme.TabButtonSelected, scheme.TabButtonSelectedOver, scheme.TabButtonUnselectedOver,
//            //    Color.Black, BorderPanelMode.Stretch, Scheme);

//            //tabButton = new TabButton(this, new Point(0, 0), new Point(1, 1),Scheme.TabButtonUnselected, Scheme.TabButtonSelected, Scheme);

//            //tabPage = new Panel(this, new Point(0, 0), new Point(1, 1), null, Scheme);
//        }



//    }
//}
