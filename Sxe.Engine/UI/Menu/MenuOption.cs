using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using Sxe.Engine.Input;

using System.ComponentModel;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

#if !XBOX
using System.ComponentModel.Design;
using Sxe.Design;
#endif

namespace Sxe.Engine.UI
{
    /// <summary>
    /// This is a menu object that has a list of items to choose from
    /// </summary>
    //[Designer(typeof(Sxe.Design.PanelDesigner))]
    public class MenuOption : CompositePanel, IMenuEntry
    {
        ArrayList values = new ArrayList();
        int currentValue = 0;
        string caption;
        string fontPath;
        SpriteFont font;
        MenuSelectionCriteria allowedSelection = new MenuSelectionCriteria();

        public event EventHandler<EventArgs> ButtonOver;
       

        public MenuSelectionCriteria AllowedSelection
        {
            get { return allowedSelection; }
        }

        public string Selected
        {
            get
            {
                if (currentValue < 0 || currentValue >= values.Count)
                    return null;

                return values[currentValue].ToString();
            }
           
        }

        public string Caption
        {
            get { return caption; }
            set { caption = value; OnCaptionChanged(); }
        }

        public void SetSelected(int index)
        {
            //TODO: Add error checking
            currentValue = index;
        }

        public IList Values
        {
            get { return values; }
        }

        //bool IMenuEntry.CanMove(MenuDirection direction)
        //{
        //    return true;
        //}

        bool IMenuEntry.AllowPlayerIndex(int playerIndex)
        {
            if (AllowedSelection.AllowAll)
                return true;
            else if (AllowedSelection.AllowedIndices.Contains(playerIndex))
                return true;
            else
                return false;
        }

        public virtual void PerformClick(int index)
        {
        }

        public virtual void Over(int index)
        {
            if (ButtonOver != null)
                this.ButtonOver(this, EventArgs.Empty);
        }

        public virtual void Leave(int index)
        {
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

        protected SpriteFont Font
        {
            get { return font; }
            set { font = value; OnFontChanged(); }
        }

        public override bool HandleEvent(Sxe.Engine.Input.InputEventArgs inputEvent)
        {
            MenuEventArgs menuEvent = inputEvent as MenuEventArgs;
            if (menuEvent != null)
            {
                if (menuEvent.MenuEventType == MenuEventType.Left)
                {
                    DecrementValue(menuEvent.PlayerIndex);
                    return true;
                }
                else if (menuEvent.MenuEventType == MenuEventType.Right)
                {
                    IncrementValue(menuEvent.PlayerIndex);
                    return true;
                }
            }

            return base.HandleEvent(inputEvent);
        }

        public virtual void DecrementValue(int index)
        {
            currentValue--;
            if (currentValue < 0)
                currentValue = 0;
        }

        public virtual void IncrementValue(int index)
        {
            currentValue++;
            if (currentValue >= Values.Count)
                currentValue = Values.Count - 1;
        }

        public override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            base.LoadContent(content);

            if (fontPath != null)
                font = content.Load<SpriteFont>(fontPath);
        }

        public virtual void OnFontChanged()
        {
        }

        public virtual void OnCaptionChanged()
        {
        }

    }
}
