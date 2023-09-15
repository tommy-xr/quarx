using System;
using System.Collections.Generic;
using System.Text;

using Sxe.Engine.UI;
using Sxe.Engine.Input;

using Microsoft.Xna.Framework.Graphics;

namespace Quarx.NewPanels
{
    public class PlayerOptions : CompositePanel, IMenuEntry
    {

        UIImage difficultyDimImage;
        UIImage difficultySelectedImage;

        UIImage speedDimImage;
        UIImage speedSelectedImage;

        UIImage sliderImageDefault;
        UIImage sliderImageSelected;

        UIImage playerSliderBackImage;
        UIImage computerSliderBackImage;

        private QuarxSliderPanel quarxSliderPanel1;
        private QuarxSliderPanel quarxSliderPanel2;
        private Sxe.Engine.UI.Menu.MenuGroup menuGroup1;
        private Button button1;
        private Button button2;
        private Label label1;

        bool isSpeedSelected = true;

        public int Speed
        {
            get { return quarxSliderPanel1.Value; }
        }

        public int Isotopes
        {
            get { return quarxSliderPanel2.Value; }
        }

        //bool IMenuEntry.CanMove(MenuDirection direction)
        //{
        //    if(direction == MenuDirection.Up && isSpeedSelected)
        //    return true;

        //    if (direction == MenuDirection.Down && !isSpeedSelected)
        //        return true;

        //    return false;
        //}

        bool IMenuEntry.AllowPlayerIndex(int playerIndex)
        {
            return true;
        }


        public override void Activate()
        {
            base.Activate();
            SetupLabel();

        }

        bool IMenuEntry.HandleEvent(InputEventArgs args)
        {
            MenuEventArgs menuEvent = args as MenuEventArgs;
            if (menuEvent != null)
            {

                this.menuGroup1.HandleEvent(args);

                if (menuEvent.MenuEventType == MenuEventType.Up && !isSpeedSelected)
                {
                    isSpeedSelected = true;
                    //panel1.Image = speedSelected;
                    quarxSliderPanel1.SliderImage = this.sliderImageSelected;
                    quarxSliderPanel2.SliderImage = this.sliderImageDefault;

                    Audio.PlayCue("option_change");

                    return true;
                }
                else if (menuEvent.MenuEventType == MenuEventType.Down && isSpeedSelected)
                {
                    isSpeedSelected = false;
                    quarxSliderPanel1.SliderImage = this.sliderImageDefault;
                    quarxSliderPanel2.SliderImage = this.sliderImageSelected;
                    Audio.PlayCue("option_change");

                    //panel1.Image = isotopesSelected;

                    return true;
                }
                else if (menuEvent.MenuEventType == MenuEventType.Left)
                {
                    if (isSpeedSelected)
                        quarxSliderPanel1.Decrement();
                    else
                        quarxSliderPanel2.Decrement();

                    SetupLabel();

                    return true;
                }
                else if (menuEvent.MenuEventType == MenuEventType.Right)
                {
                    if (isSpeedSelected)
                        quarxSliderPanel1.Increment();
                    else
                        quarxSliderPanel2.Increment();

                    SetupLabel();

                    return true;
                }


            }

            return false;
        }

        void SetupLabel()
        {
            this.label1.Caption = (quarxSliderPanel2.Value+1).ToString();
        }

        public void Leave(int index)
        {
            //this.BackColor = Color.Green;
            //this.BackColor = Color.White;
            this.quarxSliderPanel1.SliderImage = this.sliderImageDefault;
            this.quarxSliderPanel2.SliderImage = this.sliderImageDefault;

            
            this.menuGroup1.Leave(index);
        }

        public void Over(int index)
        {
            //this.BackColor = Color.White;
            //this.BackColor = Color.Green;
            this.menuGroup1.Over(index);

            if (isSpeedSelected)
                this.quarxSliderPanel1.SliderImage = this.sliderImageSelected;
            else
                this.quarxSliderPanel2.SliderImage = this.sliderImageSelected;

        }

        void IButton.PerformClick(int playerIndex)
        {
        }
    
        public PlayerOptions()
        {
            InitializeComponent();

            quarxSliderPanel1.Minimum = 1;
            quarxSliderPanel1.Maximum = 5;

            quarxSliderPanel2.Minimum = 0;
            quarxSliderPanel2.Maximum = 20;
        }

        public void Setup(bool isPlayer)
        {
            if (isPlayer)
            {
                this.button1.DefaultImage = speedDimImage;
                this.button1.OverImage = speedSelectedImage;
                this.button1.ClickImage = speedSelectedImage;

                this.Image = playerSliderBackImage;
            }
            else
            {
                this.button1.DefaultImage = difficultyDimImage;
                this.button1.OverImage = difficultySelectedImage;
                this.button1.ClickImage = difficultySelectedImage;
                this.Image = computerSliderBackImage;
            }
        }

        public override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            base.LoadContent(content);

            speedDimImage = LoadImage(content, "Options\\Speed Dim");
            speedSelectedImage = LoadImage(content, "Options\\speedselected");
            difficultyDimImage = LoadImage(content, "Options\\Difficulty Dim");
            difficultySelectedImage = LoadImage(content, "Options\\Difficulty Selected");

            playerSliderBackImage = LoadImage(content, "Options\\slidersback");
            computerSliderBackImage = LoadImage(content, "Options\\slidersbackcomputer");

            sliderImageDefault = LoadImage(content, "Options\\slidergray");
            sliderImageSelected = LoadImage(content, "Options\\slider");

            quarxSliderPanel1.SliderImage = sliderImageDefault;
            quarxSliderPanel2.SliderImage = sliderImageDefault;


        }

        void InitializeComponent()
        {
            this.quarxSliderPanel1 = new Quarx.QuarxSliderPanel();
            this.quarxSliderPanel2 = new Quarx.QuarxSliderPanel();
            this.menuGroup1 = new Sxe.Engine.UI.Menu.MenuGroup();
            this.button1 = new Sxe.Engine.UI.Button();
            this.button2 = new Sxe.Engine.UI.Button();
            this.label1 = new Sxe.Engine.UI.Label();
            // 
            // quarxSliderPanel1
            // 
            this.quarxSliderPanel1.BackgroundPath = null;
            this.quarxSliderPanel1.CanDrag = false;
            this.quarxSliderPanel1.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowSignedIn;
            this.quarxSliderPanel1.Location = new Microsoft.Xna.Framework.Point(282, 8);
            this.quarxSliderPanel1.Maximum = 0;
            this.quarxSliderPanel1.Minimum = 0;
            this.quarxSliderPanel1.Parent = this;
            this.quarxSliderPanel1.Size = new Microsoft.Xna.Framework.Point(180, 25);
            this.quarxSliderPanel1.SliderImage = null;
            this.quarxSliderPanel1.SliderWidth = 20;
            this.quarxSliderPanel1.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.quarxSliderPanel1.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.quarxSliderPanel1.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.quarxSliderPanel1.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.quarxSliderPanel1.ValueSize = 1;
            // 
            // quarxSliderPanel2
            // 
            this.quarxSliderPanel2.BackgroundPath = null;
            this.quarxSliderPanel2.CanDrag = false;
            this.quarxSliderPanel2.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowSignedIn;
            this.quarxSliderPanel2.Location = new Microsoft.Xna.Framework.Point(284, 34);
            this.quarxSliderPanel2.Maximum = 0;
            this.quarxSliderPanel2.Minimum = 0;
            this.quarxSliderPanel2.Parent = this;
            this.quarxSliderPanel2.Size = new Microsoft.Xna.Framework.Point(178, 25);
            this.quarxSliderPanel2.SliderImage = null;
            this.quarxSliderPanel2.SliderWidth = 20;
            this.quarxSliderPanel2.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.quarxSliderPanel2.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.quarxSliderPanel2.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.quarxSliderPanel2.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.quarxSliderPanel2.ValueSize = 1;
            // 
            // menuGroup1
            // 
            this.menuGroup1.BackgroundPath = null;
            this.menuGroup1.CanDrag = false;
            this.menuGroup1.ControlType = Sxe.Engine.UI.Menu.MenuControlType.Vertical;
            this.menuGroup1.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowSignedIn;
            this.menuGroup1.Location = new Microsoft.Xna.Framework.Point(164, 6);
            this.menuGroup1.Panels.Add(this.button1);
            this.menuGroup1.Panels.Add(this.button2);
            this.menuGroup1.Parent = this;
            this.menuGroup1.SelectFirstByDefault = false;
            this.menuGroup1.Size = new Microsoft.Xna.Framework.Point(88, 68);
            this.menuGroup1.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.menuGroup1.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.menuGroup1.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.menuGroup1.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            // 
            // button1
            // 
            this.button1.BackgroundPath = null;
            this.button1.CanDrag = false;
            this.button1.ClickImagePath = "Options\\speedselected";
            this.button1.DefaultImagePath = "Options\\Speed Dim";
            this.button1.FontColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.button1.FontName = null;
            this.button1.FontPath = null;
            this.button1.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowSignedIn;
            this.button1.Location = new Microsoft.Xna.Framework.Point(-7, 3);
            this.button1.OverCue = null;
            this.button1.OverImagePath = "Options\\speedselected";
            this.button1.Parent = this.menuGroup1;
            this.button1.PressCue = null;
            this.button1.Size = new Microsoft.Xna.Framework.Point(92, 31);
            this.button1.Text = null;
            this.button1.TextImagePath = null;
            this.button1.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.button1.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.button1.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.button1.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            // 
            // button2
            // 
            this.button2.BackgroundPath = null;
            this.button2.CanDrag = false;
            this.button2.ClickImagePath = "Options\\Isotopes Selected";
            this.button2.DefaultImagePath = "Options\\Isotopes Dim";
            this.button2.FontColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.button2.FontName = null;
            this.button2.FontPath = null;
            this.button2.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowSignedIn;
            this.button2.Location = new Microsoft.Xna.Framework.Point(-7, 34);
            this.button2.OverCue = null;
            this.button2.OverImagePath = "Options\\Isotopes Selected";
            this.button2.Parent = this.menuGroup1;
            this.button2.PressCue = null;
            this.button2.Size = new Microsoft.Xna.Framework.Point(92, 31);
            this.button2.Text = null;
            this.button2.TextImagePath = null;
            this.button2.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.button2.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.button2.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.button2.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            // 
            // label1
            // 
            this.label1.BackgroundPath = null;
            this.label1.CanDrag = false;
            this.label1.Caption = "1";
            this.label1.FontColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.label1.FontPath = "Calibri11";
            this.label1.HorizontalAlignment = Sxe.Engine.UI.HorizontalAlignment.Center;
            this.label1.Location = new Microsoft.Xna.Framework.Point(363, 53);
            this.label1.Parent = this;
            this.label1.Selection = new Microsoft.Xna.Framework.Point(-1, -1);
            this.label1.Size = new Microsoft.Xna.Framework.Point(16, 19);
            this.label1.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.label1.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.label1.VerticalAlignment = Sxe.Engine.UI.VerticalAlignment.Top;
            // 
            // PlayerOptions
            // 
            this.BackgroundPath = "Options\\Empty Slot";
            this.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowSpecified;
            this.Panels.Add(this.quarxSliderPanel1);
            this.Panels.Add(this.quarxSliderPanel2);
            this.Panels.Add(this.menuGroup1);
            this.Panels.Add(this.label1);
            this.Size = new Microsoft.Xna.Framework.Point(500, 75);

        }
    }
}
