using System;
using System.Collections.Generic;
using System.Text;

using Sxe.Engine.UI;

using Microsoft.Xna.Framework;


using System.ComponentModel;

#if !XBOX
using Sxe.Design;
#endif

namespace Quarx
{
#if !XBOX
    [ToolboxItemFilter("AnarchyUI", ToolboxItemFilterType.Require)]
#endif
    public class QuarxOption : MenuOption
    {
        private Sxe.Engine.UI.Buttons.ColorButton colorButton1;
        private Sxe.Engine.UI.Buttons.ColorButton colorButton2;
        private Sxe.Engine.UI.Buttons.ColorButton colorButton3;



        public QuarxOption()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.colorButton1 = new Sxe.Engine.UI.Buttons.ColorButton();
            this.colorButton2 = new Sxe.Engine.UI.Buttons.ColorButton();
            this.colorButton3 = new Sxe.Engine.UI.Buttons.ColorButton();
            // 
            // colorButton1
            // 
            this.colorButton1.BackgroundPath = "setupbutton";
            this.colorButton1.ClickColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.colorButton1.ColorTransitionTime = System.TimeSpan.Parse("00:00:00.1500000");
            this.colorButton1.DefaultColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.colorButton1.FontColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.colorButton1.FontPath = "Neuropol";
            this.colorButton1.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowSignedIn;
            this.colorButton1.Location = new Microsoft.Xna.Framework.Point(39, 8);
            this.colorButton1.OverColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(0)), ((byte)(255)), ((byte)(0)), ((byte)(255)));
            this.colorButton1.OverCue = null;
            this.colorButton1.Panels.Add(this.colorButton2);
            this.colorButton1.Panels.Add(this.colorButton3);
            this.colorButton1.Parent = this;
            this.colorButton1.PressCue = null;
            this.colorButton1.Size = new Microsoft.Xna.Framework.Point(176, 37);
            this.colorButton1.Text = "";
            this.colorButton1.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.colorButton1.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.colorButton1.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.colorButton1.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            // 
            // colorButton2
            // 
            this.colorButton2.BackgroundPath = "leftarrow";
            this.colorButton2.ClickColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(0)), ((byte)(255)), ((byte)(0)), ((byte)(255)));
            this.colorButton2.ColorTransitionTime = System.TimeSpan.Parse("00:00:00.2500000");
            this.colorButton2.DefaultColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.colorButton2.FontColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(0)), ((byte)(0)), ((byte)(0)), ((byte)(0)));
            this.colorButton2.FontPath = null;
            this.colorButton2.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowSignedIn;
            this.colorButton2.Location = new Microsoft.Xna.Framework.Point(-12, 2);
            this.colorButton2.OverColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.colorButton2.OverCue = null;
            this.colorButton2.Parent = this.colorButton1;
            this.colorButton2.PressCue = null;
            this.colorButton2.Size = new Microsoft.Xna.Framework.Point(15, 30);
            this.colorButton2.Text = "";
            this.colorButton2.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.colorButton2.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.colorButton2.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.colorButton2.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            // 
            // colorButton3
            // 
            this.colorButton3.BackgroundPath = "rightarrow";
            this.colorButton3.ClickColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(0)), ((byte)(255)), ((byte)(0)), ((byte)(255)));
            this.colorButton3.ColorTransitionTime = System.TimeSpan.Parse("00:00:00.2500000");
            this.colorButton3.DefaultColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.colorButton3.FontColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(0)), ((byte)(0)), ((byte)(0)), ((byte)(0)));
            this.colorButton3.FontPath = null;
            this.colorButton3.InputFilterMode = Sxe.Engine.Input.InputFilterMode.AllowSignedIn;
            this.colorButton3.Location = new Microsoft.Xna.Framework.Point(174, 1);
            this.colorButton3.OverColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(255)), ((byte)(255)));
            this.colorButton3.OverCue = null;
            this.colorButton3.Parent = this.colorButton1;
            this.colorButton3.PressCue = null;
            this.colorButton3.Size = new Microsoft.Xna.Framework.Point(15, 30);
            this.colorButton3.Text = "";
            this.colorButton3.TransitionOffOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.colorButton3.TransitionOffTime = System.TimeSpan.Parse("00:00:00.5000000");
            this.colorButton3.TransitionOnOffset = new Microsoft.Xna.Framework.Point(0, 0);
            this.colorButton3.TransitionOnTime = System.TimeSpan.Parse("00:00:00.5000000");
            // 
            // QuarxOption
            // 
            this.BackColor = new Microsoft.Xna.Framework.Graphics.Color(((byte)(255)), ((byte)(255)), ((byte)(0)), ((byte)(255)));
            this.Panels.Add(this.colorButton1);
            this.Size = new Microsoft.Xna.Framework.Point(275, 50);

        }

        public override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            base.LoadContent(content);
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch, Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Draw(spriteBatch, gameTime);
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            //if (this.label1.Caption != Selected)
            //    this.label1.Caption = Selected;

            if (colorButton1.Text != Selected)
                this.colorButton1.Text = Selected;

            base.Update(gameTime);
        }

        public override void Over(int index)
        {
            base.Over(index);
            colorButton1.Over(index);
            //Audio.PlayCue("option_change");
        }

        public override void Leave(int index)
        {
          
            base.Leave(index);
            colorButton1.Leave(index);
            //Audio.PlayCue("option_change");
        }

        public override void DecrementValue(int index)
        {
            base.DecrementValue(index);

            this.colorButton1.Text = this.Selected;
            //this.label1.Caption = this.Selected;
            colorButton2.PerformClick(index);

            Audio.PlayCue("slider");
        }

        public override void IncrementValue(int index)
        {
            base.IncrementValue(index);
            //this.label1.Caption = this.Selected;
            this.colorButton1.Text = this.Selected;
            colorButton3.PerformClick(index);

            Audio.PlayCue("slider");
        }

        public override void OnCaptionChanged()
        {
            base.OnCaptionChanged();
            //colorButton1.Text = this.Caption;
        }

        public override void OnFontChanged()
        {
            base.OnFontChanged();
            colorButton1.FontPath = this.FontPath;
            //label1.FontPath = this.FontPath;
        }

    }
}
