using System;
using System.Collections.Generic;
using System.Text;

using Sxe.Engine.UI;

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Quarx.NewGameScreens
{
    public class QuarxSignInScreen : BaseSignInScreen
    {
        private Sxe.Engine.UI.Buttons.ColorButton colorButton1;
        private Sxe.Engine.UI.Menu.MenuGroup menuGroup1;
        private Sxe.Engine.UI.Buttons.ColorButton colorButton2;
        private Sxe.Engine.UI.Buttons.ColorButton colorButton3;
    
        public QuarxSignInScreen()
        {
            InitializeComponent();

            Color overColor = new Color(0, 200, 0, 200);
            Color defaultColor = new Color(255, 255, 255, 128);
            Color clickColor = new Color(200, 0, 0, 200);

            colorButton1.OverColor = overColor;
            colorButton1.DefaultColor = defaultColor;
            colorButton1.ClickColor = clickColor;
            colorButton2.OverColor = overColor;
            colorButton2.DefaultColor = defaultColor;
            colorButton2.ClickColor = clickColor;
            colorButton3.OverColor = overColor;
            colorButton3.DefaultColor = defaultColor;
            colorButton3.ClickColor = clickColor;


        }

        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);

            //colorButton1.Image = new Sxe.Engine.UI.OscillatingImage(content.Load<Texture2D>(colorButton1.BackgroundPath));

        }

        public override void Activate()
        {
            this.GameScreenService.Audio.PlayCue("dialog_show");
            base.Activate();
        }

        public override bool HandleEvent(Sxe.Engine.Input.InputEventArgs inputEvent)
        {
            return base.HandleEvent(inputEvent);
        }

        void InitializeComponent()
        {
            this.colorButton1 = new Sxe.Engine.UI.Buttons.ColorButton();
            this.menuGroup1 = new Sxe.Engine.UI.Menu.MenuGroup();
            this.colorButton2 = new Sxe.Engine.UI.Buttons.ColorButton();
            this.colorButton3 = new Sxe.Engine.UI.Buttons.ColorButton();
            // 
            // colorButton1
            // 
            this.colorButton1.BackColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.colorButton1.BackgroundPath = "menugamertagbox";
            this.colorButton1.CanDrag = false;
            this.colorButton1.ClickColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.colorButton1.ColorTransitionTime = System.TimeSpan.Parse("00:00:00.1000000");
            this.colorButton1.DefaultColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.colorButton1.Enabled = true;
            this.colorButton1.FontColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.colorButton1.FontPath = "Neuropol";
            this.colorButton1.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowSignedIn;
            this.colorButton1.Location = new Microsoft.Xna.Framework.Point(89, 56);
            this.colorButton1.Name = "";
            this.colorButton1.OverColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.colorButton1.OverCue = null;
            this.colorButton1.Parent = this.menuGroup1;
            this.colorButton1.PressCue = null;
            this.colorButton1.Size = new Microsoft.Xna.Framework.Point(460, 39);
            this.colorButton1.Text = "Sign in as temporary profile...";
            this.colorButton1.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.colorButton1.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.colorButton1.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.colorButton1.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.colorButton1.Visible = true;
            this.colorButton1.ButtonPressed += new System.EventHandler<Sxe.Engine.UI.ButtonPressEventArgs>(this.colorButton1_ButtonPressed);
            // 
            // menuGroup1
            // 
            this.menuGroup1.AllowMultiSelect = false;
            this.menuGroup1.BackColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.menuGroup1.BackgroundPath = "setupbutton2";
            this.menuGroup1.CanDrag = false;
            this.menuGroup1.ControlType = Sxe.Engine.UI.Menu.MenuControlType.Vertical;
            this.menuGroup1.Enabled = true;
            this.menuGroup1.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowAll;
            this.menuGroup1.Location = new Microsoft.Xna.Framework.Point(107, 110);
            this.menuGroup1.Name = "";
            this.menuGroup1.Panels.Add(this.colorButton1);
            this.menuGroup1.Panels.Add(this.colorButton2);
            this.menuGroup1.Panels.Add(this.colorButton3);
            this.menuGroup1.Parent = this;
            this.menuGroup1.SelectFirstByDefault = true;
            this.menuGroup1.Size = new Microsoft.Xna.Framework.Point(614, 213);
            this.menuGroup1.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.menuGroup1.TransitionOffTime = System.TimeSpan.Parse("00:00:00.2000000");
            this.menuGroup1.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.menuGroup1.TransitionOnTime = System.TimeSpan.Parse("00:00:00.2000000");
            this.menuGroup1.Visible = true;
            // 
            // colorButton2
            // 
            this.colorButton2.BackColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.colorButton2.BackgroundPath = "menugamertagbox";
            this.colorButton2.CanDrag = false;
            this.colorButton2.ClickColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.colorButton2.ColorTransitionTime = System.TimeSpan.Parse("00:00:00.1000000");
            this.colorButton2.DefaultColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.colorButton2.Enabled = true;
            this.colorButton2.FontColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.colorButton2.FontPath = "Neuropol";
            this.colorButton2.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowSignedIn;
            this.colorButton2.Location = new Microsoft.Xna.Framework.Point(89, 98);
            this.colorButton2.Name = "";
            this.colorButton2.OverColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.colorButton2.OverCue = null;
            this.colorButton2.Parent = this.menuGroup1;
            this.colorButton2.PressCue = null;
            this.colorButton2.Size = new Microsoft.Xna.Framework.Point(460, 39);
            this.colorButton2.Text = "Sign in with an xbox profile...";
            this.colorButton2.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.colorButton2.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.colorButton2.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.colorButton2.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.colorButton2.Visible = true;
            this.colorButton2.ButtonPressed += new System.EventHandler<Sxe.Engine.UI.ButtonPressEventArgs>(this.colorButton2_ButtonPressed);
            // 
            // colorButton3
            // 
            this.colorButton3.BackColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.colorButton3.BackgroundPath = "menugamertagbox";
            this.colorButton3.CanDrag = false;
            this.colorButton3.ClickColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.colorButton3.ColorTransitionTime = System.TimeSpan.Parse("00:00:00.1000000");
            this.colorButton3.DefaultColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.colorButton3.Enabled = true;
            this.colorButton3.FontColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.colorButton3.FontPath = "Neuropol";
            this.colorButton3.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowSignedIn;
            this.colorButton3.Location = new Microsoft.Xna.Framework.Point(89, 140);
            this.colorButton3.Name = "";
            this.colorButton3.OverColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.colorButton3.OverCue = null;
            this.colorButton3.Parent = this.menuGroup1;
            this.colorButton3.PressCue = null;
            this.colorButton3.Size = new Microsoft.Xna.Framework.Point(460, 39);
            this.colorButton3.Text = "Cancel";
            this.colorButton3.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.colorButton3.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.colorButton3.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.colorButton3.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.colorButton3.Visible = true;
            this.colorButton3.ButtonPressed += new System.EventHandler<Sxe.Engine.UI.ButtonPressEventArgs>(this.colorButton3_ButtonPressed);
            // 
            // QuarxSignInScreen
            // 
            this.Panels.Add(this.menuGroup1);
            this.TransitionOffOffset = new Microsoft.Xna.Framework.Point(-500, 0);
            this.TransitionOffTime = System.TimeSpan.Parse("00:00:00.2000000");
            this.TransitionOnOffset = new Microsoft.Xna.Framework.Point(500, 0);
            this.TransitionOnTime = System.TimeSpan.Parse("00:00:00.2000000");

        }

        private void colorButton1_ButtonPressed(object sender, ButtonPressEventArgs e)
        {
            this.DoTemporaryProfile();
           
        }

        private void colorButton2_ButtonPressed(object sender, ButtonPressEventArgs e)
        {
            this.DoSignIn();
        }

        public override void Paint(SpriteBatch sb, Microsoft.Xna.Framework.Point positionOffset, Microsoft.Xna.Framework.Vector2 scale)
        {
            base.Paint(sb, positionOffset, scale);
        }

        private void colorButton3_ButtonPressed(object sender, ButtonPressEventArgs e)
        {
            this.ExitScreen();
        }

    }

}
