using System;
using System.Collections.Generic;
using System.Text;

using Sxe.Engine.UI;

namespace Sxe.Engine.Gamers
{
    public class DefaultSignInScreen : BaseSignInScreen
    {
        private Label label1;
        private Sxe.Engine.UI.Menu.MenuGroup menuGroup1;
        private Sxe.Engine.UI.Buttons.ColorButton colorButton1;
        private Sxe.Engine.UI.Buttons.ColorButton colorButton2;
        private Sxe.Engine.UI.Buttons.ColorButton colorButton3;
        private Panel panel1;
    
        public DefaultSignInScreen()
        {
            InitializeComponent();
        }

        void InitializeComponent()
        {
            this.panel1 = new Sxe.Engine.UI.Panel();
            this.label1 = new Sxe.Engine.UI.Label();
            this.menuGroup1 = new Sxe.Engine.UI.Menu.MenuGroup();
            this.colorButton1 = new Sxe.Engine.UI.Buttons.ColorButton();
            this.colorButton2 = new Sxe.Engine.UI.Buttons.ColorButton();
            this.colorButton3 = new Sxe.Engine.UI.Buttons.ColorButton();
            // 
            // panel1
            // 
            this.panel1.BackColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.panel1.BackgroundPath = "gradient";
            this.panel1.CanDrag = false;
            this.panel1.Enabled = true;
            this.panel1.Location = new Microsoft.Xna.Framework.Point(1, 182);
            this.panel1.Name = "";
            this.panel1.Parent = this;
            this.panel1.Size = new Microsoft.Xna.Framework.Point(799, 176);
            this.panel1.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.panel1.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.panel1.Visible = true;
            // 
            // label1
            // 
            this.label1.BackColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.label1.BackgroundPath = null;
            this.label1.CanDrag = false;
            this.label1.Caption = "Temporary Profile";
            this.label1.Enabled = true;
            this.label1.FontColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.label1.FontPath = "Calibri11";
            this.label1.HorizontalAlignment = Sxe.Engine.UI.HorizontalAlignment.Left;
            this.label1.Location = new Microsoft.Xna.Framework.Point(36, 202);
            this.label1.Name = "";
            this.label1.Parent = this;
            this.label1.Selection = new Microsoft.Xna.Framework.Point(-1, -1);
            this.label1.Size = new Microsoft.Xna.Framework.Point(178, 22);
            this.label1.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.label1.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.label1.VerticalAlignment = Sxe.Engine.UI.VerticalAlignment.Top;
            this.label1.Visible = true;
            // 
            // menuGroup1
            // 
            this.menuGroup1.BackColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.menuGroup1.BackgroundPath = null;
            this.menuGroup1.CanDrag = false;
            this.menuGroup1.ControlType = Sxe.Engine.UI.Menu.MenuControlType.Vertical;
            this.menuGroup1.Enabled = true;
            this.menuGroup1.Location = new Microsoft.Xna.Framework.Point(29, 229);
            this.menuGroup1.Name = "";
            this.menuGroup1.Panels.Add(this.colorButton1);
            this.menuGroup1.Panels.Add(this.colorButton2);
            this.menuGroup1.Panels.Add(this.colorButton3);
            this.menuGroup1.Parent = this;
            this.menuGroup1.Size = new Microsoft.Xna.Framework.Point(757, 94);
            this.menuGroup1.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.menuGroup1.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.menuGroup1.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.menuGroup1.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.menuGroup1.Visible = true;
            // 
            // colorButton1
            // 
            this.colorButton1.BackColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(150)), ((byte)(150)), ((byte)(150)), ((byte)(150)));
            this.colorButton1.BackgroundPath = "blank";
            this.colorButton1.CanDrag = false;
            this.colorButton1.ClickColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(0)), ((byte)(0)), ((byte)(200)));
            this.colorButton1.ColorTransitionTime = System.TimeSpan.Parse("00:00:00.1000000");
            this.colorButton1.DefaultColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(150)), ((byte)(150)), ((byte)(150)), ((byte)(150)));
            this.colorButton1.Enabled = true;
            this.colorButton1.FontColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.colorButton1.FontPath = "Calibri11";
            this.colorButton1.Location = new Microsoft.Xna.Framework.Point(9, 4);
            this.colorButton1.Name = "";
            this.colorButton1.OverColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(0)), ((byte)(255)), ((byte)(0)), ((byte)(200)));
            this.colorButton1.OverCue = null;
            this.colorButton1.Parent = this.menuGroup1;
            this.colorButton1.PressCue = null;
            this.colorButton1.Size = new Microsoft.Xna.Framework.Point(559, 28);
            this.colorButton1.Text = "Start Temporary Profile";
            this.colorButton1.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.colorButton1.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.colorButton1.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.colorButton1.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.colorButton1.Visible = true;
            this.colorButton1.ButtonPressed += new System.EventHandler<Sxe.Engine.UI.ButtonPressEventArgs>(this.colorButton1_ButtonPressed);
            // 
            // colorButton2
            // 
            this.colorButton2.BackColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(150)), ((byte)(150)), ((byte)(150)), ((byte)(150)));
            this.colorButton2.BackgroundPath = "blank";
            this.colorButton2.CanDrag = false;
            this.colorButton2.ClickColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(0)), ((byte)(0)), ((byte)(200)));
            this.colorButton2.ColorTransitionTime = System.TimeSpan.Parse("00:00:00.1000000");
            this.colorButton2.DefaultColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(150)), ((byte)(150)), ((byte)(150)), ((byte)(150)));
            this.colorButton2.Enabled = true;
            this.colorButton2.FontColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.colorButton2.FontPath = "Calibri11";
            this.colorButton2.Location = new Microsoft.Xna.Framework.Point(9, 40);
            this.colorButton2.Name = "";
            this.colorButton2.OverColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(0)), ((byte)(255)), ((byte)(0)), ((byte)(200)));
            this.colorButton2.OverCue = null;
            this.colorButton2.Parent = this.menuGroup1;
            this.colorButton2.PressCue = null;
            this.colorButton2.Size = new Microsoft.Xna.Framework.Point(559, 28);
            this.colorButton2.Text = "Sign In";
            this.colorButton2.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.colorButton2.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.colorButton2.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.colorButton2.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.colorButton2.Visible = true;
            this.colorButton2.ButtonPressed += new System.EventHandler<Sxe.Engine.UI.ButtonPressEventArgs>(this.colorButton2_ButtonPressed);
            // 
            // colorButton3
            // 
            this.colorButton3.BackColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(150)), ((byte)(150)), ((byte)(150)), ((byte)(150)));
            this.colorButton3.BackgroundPath = "blank";
            this.colorButton3.CanDrag = false;
            this.colorButton3.ClickColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(0)), ((byte)(0)), ((byte)(200)));
            this.colorButton3.ColorTransitionTime = System.TimeSpan.Parse("00:00:00.1000000");
            this.colorButton3.DefaultColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(150)), ((byte)(150)), ((byte)(150)), ((byte)(150)));
            this.colorButton3.Enabled = true;
            this.colorButton3.FontColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.colorButton3.FontPath = "Calibri11";
            this.colorButton3.Location = new Microsoft.Xna.Framework.Point(9, 77);
            this.colorButton3.Name = "";
            this.colorButton3.OverColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(0)), ((byte)(255)), ((byte)(0)), ((byte)(200)));
            this.colorButton3.OverCue = null;
            this.colorButton3.Parent = this.menuGroup1;
            this.colorButton3.PressCue = null;
            this.colorButton3.Size = new Microsoft.Xna.Framework.Point(559, 28);
            this.colorButton3.Text = "Cancel";
            this.colorButton3.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.colorButton3.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.colorButton3.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.colorButton3.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.colorButton3.Visible = true;
            this.colorButton3.ButtonPressed += new System.EventHandler<Sxe.Engine.UI.ButtonPressEventArgs>(this.colorButton3_ButtonPressed);
            // 
            // DefaultSignInScreen
            // 
            this.Panels.Add(this.panel1);
            this.Panels.Add(this.label1);
            this.Panels.Add(this.menuGroup1);

        }

        private void colorButton1_ButtonPressed(object sender, ButtonPressEventArgs e)
        {
            this.DoTemporaryProfile();
        }

        private void colorButton2_ButtonPressed(object sender, ButtonPressEventArgs e)
        {
            this.DoSignIn();

            
        }

        private void colorButton3_ButtonPressed(object sender, ButtonPressEventArgs e)
        {
            this.ExitScreen();
        }

        public override bool HandleEvent(Sxe.Engine.Input.InputEventArgs inputEvent)
        {
            return base.HandleEvent(inputEvent);
        }


    }
}
