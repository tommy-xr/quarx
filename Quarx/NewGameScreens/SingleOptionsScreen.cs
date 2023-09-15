using System;
using System.Collections.Generic;
using System.Text;

using Sxe.Engine.UI;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Quarx
{
    public class SingleOptionsScreen : BaseScreen
    {
        private QuarxOption quarxOption2;
        private Sxe.Engine.UI.Menu.MenuGroup menuGroup1;
        private QuarxOption quarxOption3;
        private QuarxOption quarxOption4;
        private Sxe.Engine.UI.Buttons.ColorButton colorButton1;
        private Sxe.Engine.UI.Buttons.ColorButton colorButton2;
        private QuarxOption quarxOption1;
        BaseQuarxGameScreen customScreen;

        public BaseQuarxGameScreen GameScreen
        {
            get { return customScreen; }
            set { customScreen = value; }
        }
    
        public SingleOptionsScreen()
        {
            InitializeComponent();
            SetupValues();
        }

        /// <summary>
        /// Sets up values for the different options
        /// </summary>
        void SetupValues()
        {
            //Add values to quarx option 1: Speed
            for (int i = 0; i <= 5; i++)
                quarxOption2.Values.Add(i.ToString());

            //Add values to quarx option 2: Isotopes
            for (int i = 1; i <= 20; i++)
                quarxOption3.Values.Add(i.ToString());

            //Add valeus to quarx option 3: Music
            for (int i = 0; i <= 2; i++)
                quarxOption4.Values.Add(i.ToString());

            
        }

        void InitializeComponent()
        {
            this.quarxOption1 = new Quarx.QuarxOption();
            this.quarxOption2 = new Quarx.QuarxOption();
            this.menuGroup1 = new Sxe.Engine.UI.Menu.MenuGroup();
            this.quarxOption3 = new Quarx.QuarxOption();
            this.quarxOption4 = new Quarx.QuarxOption();
            this.colorButton1 = new Sxe.Engine.UI.Buttons.ColorButton();
            this.colorButton2 = new Sxe.Engine.UI.Buttons.ColorButton();
            // 
            // quarxOption1
            // 
            this.quarxOption1.BackColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.quarxOption1.BackgroundPath = null;
            this.quarxOption1.CanDrag = false;
            this.quarxOption1.Caption = null;
            this.quarxOption1.Enabled = true;
            this.quarxOption1.FontPath = null;
            this.quarxOption1.Location = new Microsoft.Xna.Framework.Point(207, 69);
            this.quarxOption1.Name = "";
            this.quarxOption1.Parent = null;
            this.quarxOption1.Size = new Microsoft.Xna.Framework.Point(278, 50);
            this.quarxOption1.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.quarxOption1.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.quarxOption1.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.quarxOption1.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.quarxOption1.Visible = true;
            // 
            // quarxOption2
            // 
            this.quarxOption2.BackColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(0)), ((byte)(128)), ((byte)(0)), ((byte)(255)));
            this.quarxOption2.BackgroundPath = null;
            this.quarxOption2.CanDrag = false;
            this.quarxOption2.Caption = "Speed";
            this.quarxOption2.Enabled = true;
            this.quarxOption2.FontPath = "Neuropol";
            this.quarxOption2.Location = new Microsoft.Xna.Framework.Point(160, 85);
            this.quarxOption2.Name = "";
            this.quarxOption2.Parent = this.menuGroup1;
            this.quarxOption2.Size = new Microsoft.Xna.Framework.Point(263, 50);
            this.quarxOption2.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.quarxOption2.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.quarxOption2.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.quarxOption2.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.quarxOption2.Visible = true;
            // 
            // menuGroup1
            // 
            this.menuGroup1.BackColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.menuGroup1.BackgroundPath = "customgamemenu";
            this.menuGroup1.CanDrag = false;
            this.menuGroup1.ControlType = Sxe.Engine.UI.Menu.MenuControlType.Vertical;
            this.menuGroup1.Enabled = true;
            this.menuGroup1.Location = new Microsoft.Xna.Framework.Point(110, 110);
            this.menuGroup1.Name = "";
            this.menuGroup1.Panels.Add(this.quarxOption2);
            this.menuGroup1.Panels.Add(this.quarxOption3);
            this.menuGroup1.Panels.Add(this.quarxOption4);
            this.menuGroup1.Panels.Add(this.colorButton1);
            this.menuGroup1.Panels.Add(this.colorButton2);
            this.menuGroup1.Parent = this;
            this.menuGroup1.Size = new Microsoft.Xna.Framework.Point(531, 436);
            this.menuGroup1.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, -200);
            this.menuGroup1.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.menuGroup1.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, -200);
            this.menuGroup1.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.menuGroup1.Visible = true;
            this.menuGroup1.MenuCancel += new System.EventHandler<ButtonPressEventArgs>(this.menuGroup1_MenuCancel);
            // 
            // quarxOption3
            // 
            this.quarxOption3.BackColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(0)), ((byte)(255)));
            this.quarxOption3.BackgroundPath = null;
            this.quarxOption3.CanDrag = false;
            this.quarxOption3.Caption = "Isotopes";
            this.quarxOption3.Enabled = true;
            this.quarxOption3.FontPath = "Neuropol";
            this.quarxOption3.Location = new Microsoft.Xna.Framework.Point(160, 138);
            this.quarxOption3.Name = "";
            this.quarxOption3.Parent = this.menuGroup1;
            this.quarxOption3.Size = new Microsoft.Xna.Framework.Point(263, 50);
            this.quarxOption3.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.quarxOption3.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.quarxOption3.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.quarxOption3.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.quarxOption3.Visible = true;
            // 
            // quarxOption4
            // 
            this.quarxOption4.BackColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(0)), ((byte)(128)), ((byte)(0)), ((byte)(255)));
            this.quarxOption4.BackgroundPath = null;
            this.quarxOption4.CanDrag = false;
            this.quarxOption4.Caption = "Music";
            this.quarxOption4.Enabled = true;
            this.quarxOption4.FontPath = "Neuropol";
            this.quarxOption4.Location = new Microsoft.Xna.Framework.Point(167, 191);
            this.quarxOption4.Name = "";
            this.quarxOption4.Parent = this.menuGroup1;
            this.quarxOption4.Size = new Microsoft.Xna.Framework.Point(249, 50);
            this.quarxOption4.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.quarxOption4.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.quarxOption4.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.quarxOption4.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.quarxOption4.Visible = true;
            // 
            // colorButton1
            // 
            this.colorButton1.BackColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.colorButton1.BackgroundPath = "setupbutton";
            this.colorButton1.CanDrag = false;
            this.colorButton1.ClickColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.colorButton1.ColorTransitionTime = System.TimeSpan.Parse("00:00:00.1500000");
            this.colorButton1.DefaultColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.colorButton1.Enabled = true;
            this.colorButton1.FontColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.colorButton1.FontPath = "Neuropol";
            this.colorButton1.Location = new Microsoft.Xna.Framework.Point(214, 275);
            this.colorButton1.Name = "";
            this.colorButton1.OverColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(0)), ((byte)(255)), ((byte)(0)), ((byte)(255)));
            this.colorButton1.OverCue = null;
            this.colorButton1.Parent = this.menuGroup1;
            this.colorButton1.PressCue = null;
            this.colorButton1.Size = new Microsoft.Xna.Framework.Point(176, 37);
            this.colorButton1.Text = "Back";
            this.colorButton1.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.colorButton1.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.colorButton1.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.colorButton1.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.colorButton1.Visible = true;
            this.colorButton1.ButtonPressed += new System.EventHandler<ButtonPressEventArgs>(this.colorButton1_ButtonPressed);
            // 
            // colorButton2
            // 
            this.colorButton2.BackColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.colorButton2.BackgroundPath = "setupbutton";
            this.colorButton2.CanDrag = false;
            this.colorButton2.ClickColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.colorButton2.ColorTransitionTime = System.TimeSpan.Parse("00:00:00.1500000");
            this.colorButton2.DefaultColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.colorButton2.Enabled = true;
            this.colorButton2.FontColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.colorButton2.FontPath = "Neuropol";
            this.colorButton2.Location = new Microsoft.Xna.Framework.Point(215, 326);
            this.colorButton2.Name = "";
            this.colorButton2.OverColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(0)), ((byte)(255)), ((byte)(0)), ((byte)(255)));
            this.colorButton2.OverCue = null;
            this.colorButton2.Parent = this.menuGroup1;
            this.colorButton2.PressCue = null;
            this.colorButton2.Size = new Microsoft.Xna.Framework.Point(176, 37);
            this.colorButton2.Text = "Start";
            this.colorButton2.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.colorButton2.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.colorButton2.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.colorButton2.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.colorButton2.Visible = true;
            this.colorButton2.ButtonPressed += new System.EventHandler<ButtonPressEventArgs>(this.colorButton2_ButtonPressed);
            // 
            // SingleOptionsScreen
            // 
            this.BackgroundPath = "background";
            this.Panels.Add(this.menuGroup1);

        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch, Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Draw(spriteBatch, gameTime);
        }

        private void menuGroup1_MenuCancel(object sender, ButtonPressEventArgs e)
        {
            this.ExitScreen();
        }

        public override void Paint(SpriteBatch sb, Point positionOffset, Vector2 scale)
        {
            this.BackColor = new Color(255, 255, 255, TransitionAlpha);
            base.Paint(sb, positionOffset, scale);
        }

        private void button5_ButtonPressed(object sender, EventArgs e)
        {

        }

        private void colorButton1_ButtonPressed(object sender, ButtonPressEventArgs e)
        {
            this.ExitScreen();
        }

        private void colorButton2_ButtonPressed(object sender, ButtonPressEventArgs e)
        {
            this.ExitScreen();

            int isotopes = Int32.Parse(this.quarxOption3.Selected);
            float speed = float.Parse(this.quarxOption2.Selected);

            speed = 1.05f - (0.2f * speed);

            //Create settings

            BaseQuarxGameScreen customGame = customScreen;

            QuarxGameSettings customSettings = new QuarxGameSettings();
            customSettings.Isotopes = 4 * isotopes;
            customSettings.MaxHeight = (customSettings.Isotopes / 8) + 2;

            if (customSettings.MaxHeight < 5)
                customSettings.MaxHeight += 4;

            customSettings.FallSpeed = (double)speed;

            //customGame.Settings = customSettings;

            GameScreenService.AddScreen(customGame);
        }
    }
}
