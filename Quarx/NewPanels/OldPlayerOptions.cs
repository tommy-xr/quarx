//using System;
//using System.Collections.Generic;
//using System.Text;

//using Sxe.Engine.UI;
//using Sxe.Engine.Input;

//using Microsoft.Xna.Framework.Graphics;

//namespace Quarx.NewPanels
//{
//    public class OldPlayerOptions : CompositePanel, IMenuEntry
//    {
//        private Panel panel1;

//        UIImage speedSelected;
//        UIImage isotopesSelected;

//        UIImage sliderImageDefault;
//        UIImage sliderImageSelected;

//        private QuarxSliderPanel quarxSliderPanel1;
//        private QuarxSliderPanel quarxSliderPanel2;

//        bool isSpeedSelected = true;

//        public int Speed
//        {
//            get { return quarxSliderPanel1.Value; }
//        }

//        public int Isotopes
//        {
//            get { return quarxSliderPanel2.Value; }
//        }

//        //bool IMenuEntry.CanMove(MenuDirection direction)
//        //{
//        //    if(direction == MenuDirection.Up && isSpeedSelected)
//        //    return true;

//        //    if (direction == MenuDirection.Down && !isSpeedSelected)
//        //        return true;

//        //    return false;
//        //}

//        bool IMenuEntry.AllowPlayerIndex(int playerIndex)
//        {
//            return true;
//        }

//        bool IMenuEntry.HandleEvent(InputEventArgs args)
//        {
//            MenuEventArgs menuEvent = args as MenuEventArgs;
//            if (menuEvent != null)
//            {
//                if (menuEvent.MenuEventType == MenuEventType.Up && !isSpeedSelected)
//                {
//                    isSpeedSelected = true;
//                    panel1.Image = speedSelected;
//                    quarxSliderPanel1.SliderImage = sliderImageSelected;
//                    quarxSliderPanel2.SliderImage = sliderImageDefault;
//                    return true;
//                }
//                else if (menuEvent.MenuEventType == MenuEventType.Down && isSpeedSelected)
//                {
//                    isSpeedSelected = false;
//                    panel1.Image = isotopesSelected;
//                    quarxSliderPanel1.SliderImage = sliderImageDefault;
//                    quarxSliderPanel2.SliderImage = sliderImageSelected;
//                    return true;
//                }
//                else if (menuEvent.MenuEventType == MenuEventType.Left)
//                {
//                    if (isSpeedSelected)
//                        quarxSliderPanel1.Decrement();
//                    else
//                        quarxSliderPanel2.Decrement();

//                    return true;
//                }
//                else if (menuEvent.MenuEventType == MenuEventType.Right)
//                {
//                    if (isSpeedSelected)
//                        quarxSliderPanel1.Increment();
//                    else
//                        quarxSliderPanel2.Increment();
//                    return true;
//                }
                

//            }

//            return false;
//        }

//        void IButton.Leave(int index)
//        {
//            //this.BackColor = Color.Green;
//            this.BackColor = Color.White;
//        }

//        void IButton.Over(int index)
//        {
//            //this.BackColor = Color.White;
//            this.BackColor = Color.Green;
//        }

//        void IButton.PerformClick(int playerIndex)
//        {
//        }
    
//        public OldPlayerOptions()
//        {
//            InitializeComponent();

//            quarxSliderPanel1.Minimum = 0;
//            quarxSliderPanel1.Maximum = 5;

//            quarxSliderPanel2.Minimum = 0;
//            quarxSliderPanel2.Maximum = 8;
//        }

//        public override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content)
//        {
//            base.LoadContent(content);

//            speedSelected = LoadImage(content, "Options\\speedSelected");
//            isotopesSelected = LoadImage(content, "Options\\isotopesSelected");

//            sliderImageDefault = LoadImage(content, "Options\\slidergray");
//            sliderImageSelected = LoadImage(content, "Options\\slider");
//        }

//        void InitializeComponent()
//        {
//            this.panel1 = new Sxe.Engine.UI.Panel();
//            this.quarxSliderPanel1 = new Quarx.QuarxSliderPanel();
//            this.quarxSliderPanel2 = new Quarx.QuarxSliderPanel();
//            // 
//            // panel1
//            // 
//            this.panel1.BackColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
//            this.panel1.BackgroundPath = "Options\\speedselected";
//            this.panel1.CanDrag = false;
//            this.panel1.Enabled = true;
//            this.panel1.Location = new Microsoft.Xna.Framework.Point(184, 6);
//            this.panel1.Name = "";
//            this.panel1.Parent = this;
//            this.panel1.Size = new Microsoft.Xna.Framework.Point(56, 56);
//            this.panel1.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
//            this.panel1.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
//            this.panel1.Visible = true;
//            // 
//            // quarxSliderPanel1
//            // 
//            this.quarxSliderPanel1.BackColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
//            this.quarxSliderPanel1.BackgroundPath = null;
//            this.quarxSliderPanel1.CanDrag = false;
//            this.quarxSliderPanel1.Enabled = true;
//            this.quarxSliderPanel1.Location = new Microsoft.Xna.Framework.Point(278, 8);
//            this.quarxSliderPanel1.Maximum = 0;
//            this.quarxSliderPanel1.Minimum = 0;
//            this.quarxSliderPanel1.Name = "";
//            this.quarxSliderPanel1.Parent = this;
//            this.quarxSliderPanel1.Size = new Microsoft.Xna.Framework.Point(185, 25);
//            this.quarxSliderPanel1.SliderImage = null;
//            this.quarxSliderPanel1.SliderWidth = 20;
//            this.quarxSliderPanel1.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
//            this.quarxSliderPanel1.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
//            this.quarxSliderPanel1.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
//            this.quarxSliderPanel1.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
//            this.quarxSliderPanel1.ValueSize = 1;
//            this.quarxSliderPanel1.Visible = true;
//            // 
//            // quarxSliderPanel2
//            // 
//            this.quarxSliderPanel2.BackColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
//            this.quarxSliderPanel2.BackgroundPath = null;
//            this.quarxSliderPanel2.CanDrag = false;
//            this.quarxSliderPanel2.Enabled = true;
//            this.quarxSliderPanel2.Location = new Microsoft.Xna.Framework.Point(278, 34);
//            this.quarxSliderPanel2.Maximum = 0;
//            this.quarxSliderPanel2.Minimum = 0;
//            this.quarxSliderPanel2.Name = "";
//            this.quarxSliderPanel2.Parent = this;
//            this.quarxSliderPanel2.Size = new Microsoft.Xna.Framework.Point(185, 25);
//            this.quarxSliderPanel2.SliderImage = null;
//            this.quarxSliderPanel2.SliderWidth = 20;
//            this.quarxSliderPanel2.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
//            this.quarxSliderPanel2.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
//            this.quarxSliderPanel2.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
//            this.quarxSliderPanel2.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
//            this.quarxSliderPanel2.ValueSize = 1;
//            this.quarxSliderPanel2.Visible = true;
//            // 
//            // PlayerOptions
//            // 
//            this.BackgroundPath = "Options\\slidersback";
//            this.Panels.Add(this.panel1);
//            this.Panels.Add(this.quarxSliderPanel1);
//            this.Panels.Add(this.quarxSliderPanel2);
//            this.Size = new Microsoft.Xna.Framework.Point(500, 75);

//        }
//    }
//}
