//using System;
//using System.Collections.Generic;
//using System.Text;

//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;
//using Microsoft.Xna.Framework.Content;

//namespace Sxe.Engine.UI
//{
//    /// <summary>
//    /// A scheme is a set of predefined images, colors, and fonts to use with the UI
//    /// </summary>
//    public interface IScheme
//    {
//        //Gamescreen defintions
//        UIImage ScreenBackground { get; }

//        //Form definitions
//        UIImage FormTitleBackground { get; }
//        UIImage FormBackground { get; }
//        Color FormColor { get; }
//        int FormTitleBarHeight { get; }
//        int FormTitlePadding { get; }
//        Point FormIconSize { get; }

//        //Button definitions
//        //Close button
//        UIImage CloseButtonImage { get; }
//        UIImage CloseButtonOverImage { get; }
//        UIImage CloseButtonClickImage { get; }
//        Color CloseButtonColor { get; }
//        Point CloseButtonSize { get; }
//        Point CloseButtonOffset { get; }
//        //Command button
//        UIImage CommandButtonImage { get; }
//        UIImage CommandButtonOverImage { get; }
//        UIImage CommandButtonClickImage { get; }

//        //Textbox definitions
//        UIImage TextBoxImage { get; }
//        UIImage TextBoxActiveImage { get; }

//        //Checkbox definitions
//        UIImage CheckBoxImage { get; }
//        UIImage CheckBoxOverImage { get; }
//        UIImage CheckImage { get; }
//        Point CheckSize { get; }

//        //Combobox definitions
//        UIImage ComboArrowImage { get; }
//        UIImage ComboArrowOverImage { get; }
//        UIImage ComboArrowDownImage { get; }

//        //Treeview definitions
//        UIImage TreeViewBottomBracket { get; }
//        UIImage TreeViewMiddleBracket { get; }
//        UIImage TreeViewTopBracket { get; }
//        UIImage TreeViewPlus { get; }
//        UIImage TreeViewMinus { get; }
//        Point TreeViewBracketSize { get; }
//        Point TreeViewExpandIconSize { get; }
//        Point TreeViewExpandIconOffset { get; }

//        //Scroll bar definitions
//        int ScrollBarWidth { get; }
//        UIImage ScrollBarUpArrow { get; }
//        UIImage ScrollBarUpArrowOver { get; }
//        UIImage ScrollBarUpArrowPressed { get; }
//        UIImage ScrollBarDownArrow { get; }
//        UIImage ScrollBarDownArrowOver { get; }
//        UIImage ScrollBarDownArrowPressed { get; }

//        //Tab control definitions
//        int TabSelectedHeightIncrease { get; }
//        UIImage TabButtonUnselected { get; }
//        UIImage TabButtonUnselectedOver { get; }
//        UIImage TabButtonSelected { get; }
//        UIImage TabButtonSelectedOver { get; }


//        UIImage White { get; }

//        //Font definitions
//        SpriteFont TitleFont { get; }
//        SpriteFont DefaultFont { get; }
//        SpriteFont TextBoxFont { get; }
//        Color DefaultFontColor { get; }
//        int FontHeight { get; }

//        //General definitions
//        Color SelectionColor { get; }
//        Point ArrowSize { get; }
//        Color DisabledColor { get; }

//        //Cursor
//        double CursorBlinkDelay { get; }

//        ContentManager Content { get; }

//    }
//}
