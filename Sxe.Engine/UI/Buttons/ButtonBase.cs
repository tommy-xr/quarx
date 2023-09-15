using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Sxe.Engine.Input;

namespace Sxe.Engine.UI
{

    public enum ButtonState
    {
        Normal = 0,
        Over = 1,
        Pressed = 2,
        Disabled = 3
    }

    public class ButtonPressEventArgs : EventArgs
    {
        int index;
        public int PlayerIndex
        {
            get { return index; }
            set { index = value; }
        }
    }

    /// <summary>
    /// The base class for button and button-liek objects
    /// </summary>
#if !XBOX
    [ToolboxItemFilter("AnarchyUI", ToolboxItemFilterType.Prevent)]
    [DefaultEvent("ButtonPressed")]
#endif
    public class ButtonBase : PanelContainer, IMenuEntry
    {

        Label label = new Label();
        string fontPath = null;
        string overCue;
        string pressCue;

        ButtonState state = ButtonState.Normal;
        public ButtonState ButtonState
        {
            get { return state; }
        }

        public string Text
        {
            get { return label.Caption; }
            set { label.Caption = value; }
        }

        public string OverCue
        {
            get { return overCue; }
            set { overCue = value; }
        }

        public string PressCue
        {
            get { return pressCue; }
            set { pressCue = value; }
        }

        public Color FontColor
        {
            get { return label.FontColor; }
            set { label.FontColor = value; }
        }

        protected SpriteFont Font
        {
            get { return label.Font; }
            set { label.Font = value; }
        }

#if !XBOX
        [TypeConverter(typeof(Sxe.Design.FontContentNameConverter))]
#endif
        [ReloadContent]
        public string FontPath
        {
            get { return fontPath; }
            set { fontPath = value; }
        }

        bool IMenuEntry.AllowPlayerIndex(int playerIndex)
        {
            return true;
        }

        //public virtual bool CanLeave
        //{
        //    get { return true; }
        //}

        //public virtual bool CanMove(MenuDirection direction)
        //{
        //    return true;
        //}

        public virtual void PerformClick(int index)
        {
            this.state = ButtonState.Pressed;

            if (pressCue != null && Audio != null)
                Audio.PlayCue(pressCue);

            ButtonPressEventArgs args = new ButtonPressEventArgs();
            args.PlayerIndex = index;

            if (ButtonPressed != null)
                ButtonPressed(this, args);

          
        }

        public virtual void Leave(int index)
        {
            this.state = ButtonState.Normal;

            if (ButtonLeave != null)
                ButtonLeave(this, EventArgs.Empty);
        }

        public virtual void Over(int index)
        {
            this.state = ButtonState.Over;

            if (overCue != null && Audio != null)
                Audio.PlayCue(overCue);

            if (ButtonOver != null)
                ButtonOver(this, EventArgs.Empty);
        }



        public event EventHandler<ButtonPressEventArgs> ButtonPressed;
        public event EventHandler ButtonOver;
        public event EventHandler ButtonLeave;

        public ButtonBase()
        {
            this.MouseEnter += this.OnMouseOver;
            this.MouseLeave += this.OnMouseLeave;
            this.MouseClick += this.OnMouseClick;

            this.SizeChanged += OnSizeChanged;

            label.Location = Point.Zero;
            label.HorizontalAlignment = HorizontalAlignment.Center;
            label.VerticalAlignment = VerticalAlignment.Middle;
            this.Panels.Add(label);
        }

        void OnSizeChanged(object sender, EventArgs args)
        {
            ResetLabel();
        }

        void ResetLabel()
        {
            this.label.Size = this.Size;
        }

        public override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            base.LoadContent(content);

            if (fontPath != null)
                Font = content.Load<SpriteFont>(fontPath);
        }

        public virtual void PaintAll(SpriteBatch sb, Rectangle rectangle)
        {
        }

        public override sealed void Paint(SpriteBatch sb, Point positionOffset, Vector2 scale)
        {
            if (!Visible)
                return;

            Rectangle rectangle = GetDestinationRectangle(positionOffset, scale);

            this.PaintAll(sb, rectangle);
            
            switch (state)
            {
                case ButtonState.Normal:
                    this.PaintNormal(sb, rectangle);
                    break;
                case ButtonState.Over:
                    this.PaintOver(sb, rectangle);
                    break;
                case ButtonState.Pressed:
                    this.PaintPressed(sb, rectangle);
                    break;
                case ButtonState.Disabled:
                    this.PaintDisabled(sb, rectangle);
                    break;
            }

            base.Paint(sb, positionOffset, scale);
        }

        public virtual void PaintNormal(SpriteBatch sb, Rectangle destinationRect) { }
        public virtual void PaintOver(SpriteBatch sb, Rectangle destinationRect) { }
        public virtual void PaintPressed(SpriteBatch sb, Rectangle destinationRect) { }
        public virtual void PaintDisabled(SpriteBatch sb, Rectangle destinationRect) { }

        public override bool HandleEvent(Sxe.Engine.Input.InputEventArgs inputEvent)
        {
            return base.HandleEvent(inputEvent);
        }


        void OnMouseOver(object sender, MouseEventArgs args)
        {
            Over(args.PlayerIndex);
        }

        void OnMouseLeave(object sender, MouseEventArgs args)
        {
            Leave(args.PlayerIndex);
        }

        void OnMouseClick(object sender, MouseEventArgs args)
        {
            PerformClick(args.PlayerIndex);
        }


    }
}
