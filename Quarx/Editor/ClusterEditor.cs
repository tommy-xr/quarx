using System;
using System.Collections.Generic;
using System.Text;
using Sxe.Engine.UI;
using Sxe.Engine.UI.Buttons;

namespace Quarx.Editor
{
    public class ClusterEditor : CompositePanel
    {
        private UIImage blueImage;
        private UIImage redImage;
        private UIImage yellowImage;

        private Sxe.Engine.UI.Buttons.ColorButton colorButton2;
        private Sxe.Engine.UI.Buttons.ColorButton colorButton3;
        private Label label1;
        private Sxe.Engine.UI.Buttons.ColorButton colorButton1;

        public string Number
        {
            get { return label1.Caption; }
            set { label1.Caption = value; }
        }

        public AtomClusterDescription Cluster
        {
            get 
            {
                if (colorButton2.Image != null && colorButton3.Image != null)
                {
                    AtomClusterDescription acd = new AtomClusterDescription();
                    acd.Color1 = ColorFromImage(colorButton2.Image);
                    acd.Color2 = ColorFromImage(colorButton3.Image);
                    return acd;
                }

                return null; 
            }
            set
            {
                if (value == null)
                {
                    this.colorButton1.Text = "Add";
                    this.colorButton2.Image = null;
                    this.colorButton3.Image = null;
                }
                else
                {
                    colorButton2.Image = ImageFromColor(value.Color1);
                    colorButton3.Image = ImageFromColor(value.Color2);
                    this.colorButton1.Text = "Clear";
                }
            }
        }

        public ClusterEditor()
        {
            InitializeComponent();
        }

        public override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            base.LoadContent(content);

            blueImage = this.LoadImage(content, "Editor/blueAtom");
            redImage = this.LoadImage(content, "Editor/redAtom");
            yellowImage = this.LoadImage(content, "Editor/yellowAtom");

            this.colorButton2.Image = null;
            this.colorButton3.Image = null;
            this.colorButton1.Text = "Add";


            
        }

        private void InitializeComponent()
        {
            this.colorButton1 = new Sxe.Engine.UI.Buttons.ColorButton();
            this.colorButton2 = new Sxe.Engine.UI.Buttons.ColorButton();
            this.colorButton3 = new Sxe.Engine.UI.Buttons.ColorButton();
            this.label1 = new Sxe.Engine.UI.Label();
            // 
            // colorButton1
            // 
            this.colorButton1.BackColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(128)), ((byte)(128)), ((byte)(128)), ((byte)(128)));
            this.colorButton1.BackgroundPath = "Editor\\slot";
            this.colorButton1.CanDrag = false;
            this.colorButton1.ClickColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.colorButton1.ColorTransitionTime = System.TimeSpan.Parse("00:00:00.1000000");
            this.colorButton1.DefaultColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(128)), ((byte)(128)), ((byte)(128)), ((byte)(128)));
            this.colorButton1.Enabled = true;
            this.colorButton1.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowAll;
            this.colorButton1.FontColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.colorButton1.FontPath = "Calibri11";
            this.colorButton1.Location = new Microsoft.Xna.Framework.Point(3, 55);
            this.colorButton1.Name = "";
            this.colorButton1.OverColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.colorButton1.OverCue = null;
            this.colorButton1.Panels.Add(this.colorButton2);
            this.colorButton1.Parent = this;
            this.colorButton1.PressCue = null;
            this.colorButton1.Size = new Microsoft.Xna.Framework.Point(70, 20);
            this.colorButton1.Text = "Add";
            this.colorButton1.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.colorButton1.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.colorButton1.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.colorButton1.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.colorButton1.Visible = true;
            this.colorButton1.ButtonPressed += new System.EventHandler<Sxe.Engine.UI.ButtonPressEventArgs>(this.colorButton1_ButtonPressed);
            // 
            // colorButton2
            // 
            this.colorButton2.BackColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.colorButton2.BackgroundPath = "Editor\\blueatom";
            this.colorButton2.CanDrag = false;
            this.colorButton2.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowAll;
            this.colorButton2.ClickColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.colorButton2.ColorTransitionTime = System.TimeSpan.Parse("00:00:00.1000000");
            this.colorButton2.DefaultColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(150)), ((byte)(150)), ((byte)(150)), ((byte)(150))); this.colorButton2.Enabled = true;
            this.colorButton2.FontColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.colorButton2.FontPath = null;
            this.colorButton2.Location = new Microsoft.Xna.Framework.Point(8, -33);
            this.colorButton2.Name = "";
            this.colorButton2.OverColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.colorButton2.OverCue = null;
            this.colorButton2.Panels.Add(this.colorButton3);
            this.colorButton2.Parent = this.colorButton1;
            this.colorButton2.PressCue = null;
            this.colorButton2.Size = new Microsoft.Xna.Framework.Point(30, 30);
            this.colorButton2.Text = "";
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
            this.colorButton3.BackgroundPath = "Editor\\redatom";
            this.colorButton3.CanDrag = false;
            this.colorButton3.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowAll;
            this.colorButton3.ClickColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.colorButton3.ColorTransitionTime = System.TimeSpan.Parse("00:00:00.1000000");
            this.colorButton3.DefaultColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(150)), ((byte)(150)), ((byte)(150)), ((byte)(150)));
            this.colorButton3.Enabled = true;
            this.colorButton3.FontColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.colorButton3.FontPath = null;
            this.colorButton3.Location = new Microsoft.Xna.Framework.Point(26, -1);
            this.colorButton3.Name = "";
            this.colorButton3.OverColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.colorButton3.OverCue = null;
            this.colorButton3.Parent = this.colorButton2;
            this.colorButton3.PressCue = null;
            this.colorButton3.Size = new Microsoft.Xna.Framework.Point(30, 30);
            this.colorButton3.Text = "";
            this.colorButton3.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.colorButton3.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.colorButton3.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.colorButton3.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.colorButton3.Visible = true;
            this.colorButton3.ButtonPressed += new System.EventHandler<Sxe.Engine.UI.ButtonPressEventArgs>(this.colorButton3_ButtonPressed);
            // 
            // label1
            // 
            this.label1.BackColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.label1.BackgroundPath = "menugamertagbox";
            this.label1.CanDrag = false;
            this.label1.Caption = "1";
            this.label1.Enabled = true;
            this.label1.FontColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.label1.FontPath = "Neuropol";
            this.label1.HorizontalAlignment = Sxe.Engine.UI.HorizontalAlignment.Center;
            this.label1.Location = new Microsoft.Xna.Framework.Point(2, 2);
            this.label1.Name = "";
            this.label1.Parent = this;
            this.label1.Selection = new Microsoft.Xna.Framework.Point(-1, -1);
            this.label1.Size = new Microsoft.Xna.Framework.Point(73, 22);
            this.label1.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.label1.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.label1.VerticalAlignment = Sxe.Engine.UI.VerticalAlignment.Top;
            this.label1.Visible = true;
            // 
            // ClusterEditor
            // 
            this.BackgroundPath = "previewbox2";
            this.Panels.Add(this.colorButton1);
            this.Panels.Add(this.label1);
            this.Size = new Microsoft.Xna.Framework.Point(75, 75);

        }

        private void colorButton1_ButtonPressed(object sender, ButtonPressEventArgs e)
        {
            if (this.colorButton2.Image == null || this.colorButton3.Image == null)
            {
                colorButton1.Text = "Clear";
                this.colorButton2.Image = blueImage;
                this.colorButton3.Image = blueImage;
            }
            else
            {
                colorButton1.Text = "Add";
                this.colorButton2.Image = null;
                this.colorButton3.Image = null;
            }
        }

        private void colorButton2_ButtonPressed(object sender, ButtonPressEventArgs e)
        {
            CycleColor(colorButton2);
        }

        void CycleColor(ColorButton button)
        {
            if (button.Image == blueImage)
                button.Image = redImage;
            else if (button.Image == redImage)
                button.Image = yellowImage;
            else if (button.Image == yellowImage)
                button.Image = blueImage;
        }

        BlockColor ColorFromImage(UIImage image)
        {
            if (image == blueImage)
                return BlockColor.Blue;
            else if (image == redImage)
                return BlockColor.Red;
            else if (image == yellowImage)
                return BlockColor.Yellow;
            else
                return BlockColor.Null;
        }

        UIImage ImageFromColor(BlockColor color)
        {
            if (color == BlockColor.Red)
                return redImage;
            else if (color == BlockColor.Blue)
                return blueImage;
            else if (color == BlockColor.Yellow)
                return yellowImage;
            else
                return null;
        }

        private void colorButton3_ButtonPressed(object sender, ButtonPressEventArgs e)
        {
            CycleColor(colorButton3);
        }
    }
}
