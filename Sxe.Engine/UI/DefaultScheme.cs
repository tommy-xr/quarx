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
//    public class DefaultScheme : IScheme
//    {
//        /// <summary>
//        /// The underlying data for the scheme
//        /// </summary>
//        class SchemeData
//        {
//            //Paths
//            static string basePath = "Default/UI/";

//            public string screenBackgroundPath = basePath + "img24";

//            public string formTitleBackgroundPath = "vista_title";
//            public string formBackgroundPath = "vista_window";



//            public string fontPath = basePath + "Calibri11";
//            public string textBoxFontPath = basePath + "Microsoft Sans Seriff";
//            public Color defaultFontColor = Color.Black;


//            public string closeButtonImagePath = basePath + "buttonx";
//            public string closeButtonOverImagePath = basePath + "buttonx_over";
//            public string closeButtonClickImagePath = basePath + "buttonx_over";
//            public string buttonImagePath = basePath + "button";
//            public string buttonOverImagePath = basePath + "button_over";
//            public string buttonClickImagePath = basePath + "button_click";

//            public string textboxImagePath = basePath + "textbox";
//            public string textboxActiveImagePath = basePath + "textbox_selected";

//            public string checkboxImagePath = basePath + "checkbox_default";
//            public string checkboxOverImagePath = basePath + "checkbox_over";
//            public string checkImagePath = basePath + "check";

//            public string comboArrowPath = basePath + "arrow_down";
//            public string comboArrowOverPath = basePath + "arrow_down";
//            public string comboArrowPressedPath = basePath + "arrow_down_pressed";

//            public string downArrowPath = basePath + "arrow_down";
//            public string downArrowOverPath = basePath + "arrow_down";
//            public string downArrowPressedPath = basePath + "arrow_down_pressed";
//            public string upArrowPath = basePath + "arrow_up";
//            public string upArrowOverPath = basePath + "arrow_up";
//            public string upArrowPressedPath = basePath + "arrow_up_pressed";

//            public string treeViewBottomBracket = basePath + "TreeViewBottomBracket";
//            public string treeViewMiddleBracket = basePath + "TreeViewMiddleBracket";
//            public string treeViewTopBracket = basePath + "TreeViewTopBracket";
//            public string treeViewMinus = basePath + "TreeViewMinus";
//            public string treeViewPlus = basePath + "TreeViewPlus";

//            public string whitePath = basePath + "white";
//        }

//        ContentManager content;
//        //The underlying data for the scheme
//        SchemeData data;


//        UIImage screenBackground;

//        UIImage formTitleBackground;
//        UIImage formBackground;
//        SpriteFont font;
//        SpriteFont textBoxFont;
//        UIImage closeButtonImage;
//        UIImage closeButtonOverImage;
//        UIImage closeButtonClickImage;
//        UIImage buttonImage;
//        UIImage buttonOverImage;
//        UIImage buttonClickImage;
//        UIImage textboxImage;
//        UIImage textboxActiveImage;

//        UIImage checkboxImage;
//        UIImage checkboxOverImage;
//        UIImage checkImage;

//        UIImage comboArrowImage;
//        UIImage comboArrowOverImage;
//        UIImage comboArrowDownImage;

//        UIImage downArrowImage;
//        UIImage downArrowOverImage;
//        UIImage downArrowPressedImage;
//        UIImage upArrowImage;
//        UIImage upArrowOverImage;
//        UIImage upArrowPressedImage;

//        UIImage treeViewTopBracket;
//        UIImage treeViewMiddleBracket;
//        UIImage treeViewBottomBracket;
//        UIImage treeViewMinus;
//        UIImage treeViewPlus;


//        UIImage whiteImage;

//        //Gamescreen definitions
//        public UIImage ScreenBackground { get { return screenBackground; } }
//        //Form definitions
//        public UIImage FormTitleBackground { get { return formTitleBackground; } }
//        public UIImage FormBackground { get { return formBackground; } }
//        public Color FormColor { get { return Color.White; } }
//        public int FormTitleBarHeight { get { return 30; } }
//        public int FormTitlePadding { get { return 10 + FormIconSize.X; } }
//        public Point FormIconSize { get { return new Point(22, 22); } }

//        //Button definitions
//        //Close button
//        public UIImage CloseButtonImage { get { return closeButtonImage; } }
//        public UIImage CloseButtonOverImage { get { return closeButtonOverImage; } }
//        public UIImage CloseButtonClickImage { get { return closeButtonClickImage; } }
//        public Color CloseButtonColor { get { return Color.White; } }
//        public Point CloseButtonSize { get { return new Point(44, 20); } }
//        public Point CloseButtonOffset { get { return new Point(5, 0); } }
//        //Command button
//        public UIImage CommandButtonImage { get { return buttonImage; } }
//        public UIImage CommandButtonOverImage { get { return buttonOverImage; } }
//        public UIImage CommandButtonClickImage { get { return buttonClickImage; } }

//        //Text box definitions
//        public UIImage TextBoxImage { get { return textboxImage; } }
//        public UIImage TextBoxActiveImage { get { return textboxActiveImage; } }

//        //Checkbox definitions
//        public UIImage CheckBoxImage { get { return checkboxImage; } }
//        public UIImage CheckBoxOverImage { get { return checkboxOverImage; } }
//        public UIImage CheckImage { get { return checkImage; } }
//        public Point CheckSize { get { return new Point(13, 13); } }

//        //Combobox definitions
//        public UIImage ComboArrowImage { get { return comboArrowImage; } }
//        public UIImage ComboArrowDownImage { get { return comboArrowDownImage; } }
//        public UIImage ComboArrowOverImage { get { return comboArrowOverImage; } }
//        public Point ArrowSize { get { return new Point(17, 22); } }

//        //Treeview definitions
//        public UIImage TreeViewBottomBracket { get { return treeViewBottomBracket; } }
//        public UIImage TreeViewMiddleBracket { get { return treeViewMiddleBracket; } }
//        public UIImage TreeViewTopBracket { get { return treeViewTopBracket; } }
//        public Point TreeViewBracketSize { get { return new Point(16, 16); } }
//        public UIImage TreeViewMinus { get { return treeViewMinus; } }
//        public UIImage TreeViewPlus { get { return treeViewPlus; } }
//        public Point TreeViewExpandIconSize { get { return new Point(11, 11); } }
//        public Point TreeViewExpandIconOffset { get { return new Point(0, 4); } }

//        //Scrollbar definitions
//        public int ScrollBarWidth { get { return 19; } }
//        public UIImage ScrollBarUpArrow { get { return upArrowImage; } }
//        public UIImage ScrollBarUpArrowOver { get { return upArrowOverImage; } }
//        public UIImage ScrollBarUpArrowPressed { get { return upArrowPressedImage; } }
//        public UIImage ScrollBarDownArrow { get { return downArrowImage; } }
//        public UIImage ScrollBarDownArrowOver { get { return downArrowOverImage; } }
//        public UIImage ScrollBarDownArrowPressed { get { return downArrowPressedImage; } }

//        //Tab definitions
//        public int TabSelectedHeightIncrease { get { return 2; } }
//        public UIImage TabButtonUnselected { get { return buttonImage; } }
//        public UIImage TabButtonUnselectedOver { get { return buttonImage; } }
//        public UIImage TabButtonSelected { get { return buttonOverImage; } }
//        public UIImage TabButtonSelectedOver { get { return buttonOverImage; } }



//        public UIImage White { get  { return whiteImage; } }
        

//        //Font definitions
//        public SpriteFont TitleFont { get { return font; } }
//        public SpriteFont DefaultFont { get { return font; } }
//        public SpriteFont TextBoxFont { get { return textBoxFont; } }
//        public Color DefaultFontColor { get { return data.defaultFontColor; } }

//        public int FontHeight { get { return (int)font.MeasureString("W").Y; } }
//        public Color SelectionColor { get { return new Color(0, 50, 155, 100); } }
//        public Color DisabledColor { get { return Color.Gray; } }

//        public double CursorBlinkDelay { get { return 0.2; } }

//        public ContentManager Content { get { return content; } }



//        public DefaultScheme(ContentManager inContent)
//        {
//            content = inContent;
//            data = new SchemeData();

//            screenBackground = new UIImage(content.Load<Texture2D>(data.screenBackgroundPath));

//            formTitleBackground = new UIImage(content.Load<Texture2D>(data.formTitleBackgroundPath));
//            formBackground = new UIImage(content.Load<Texture2D>(data.formBackgroundPath));
//            font = content.Load<SpriteFont>(data.fontPath);
//            textBoxFont = content.Load<SpriteFont>(data.textBoxFontPath);
//            closeButtonClickImage = new UIImage(content.Load<Texture2D>(data.closeButtonClickImagePath));
//            closeButtonOverImage = new UIImage(content.Load<Texture2D>(data.closeButtonOverImagePath));
//            closeButtonImage = new UIImage(content.Load<Texture2D>(data.closeButtonImagePath));

//            buttonImage = new UIImage(content.Load<Texture2D>(data.buttonImagePath));
//            buttonOverImage = new UIImage(content.Load<Texture2D>(data.buttonOverImagePath));
//            buttonClickImage = new UIImage(content.Load<Texture2D>(data.buttonClickImagePath));

//            textboxImage = new UIImage(content.Load<Texture2D>(data.textboxImagePath));
//            textboxActiveImage = new UIImage(content.Load<Texture2D>(data.textboxActiveImagePath));

//            checkboxImage = new UIImage(content.Load<Texture2D>(data.checkboxImagePath));
//            checkboxOverImage = new UIImage(content.Load<Texture2D>(data.checkboxOverImagePath));
//            checkImage = new UIImage(content.Load<Texture2D>(data.checkImagePath));

//            comboArrowImage = new UIImage(content.Load<Texture2D>(data.comboArrowPath));
//            comboArrowOverImage = new UIImage(content.Load<Texture2D>(data.comboArrowOverPath));
//            comboArrowDownImage = new UIImage(content.Load<Texture2D>(data.comboArrowPressedPath));

//            downArrowImage = new UIImage(content.Load<Texture2D>(data.downArrowPath));
//            downArrowOverImage = new UIImage(content.Load<Texture2D>(data.downArrowOverPath));
//            downArrowPressedImage = new UIImage(content.Load<Texture2D>(data.downArrowPressedPath));
//            upArrowImage = new UIImage(content.Load<Texture2D>(data.upArrowPath));
//            upArrowOverImage = new UIImage(content.Load<Texture2D>(data.upArrowOverPath));
//            upArrowPressedImage = new UIImage(content.Load<Texture2D>(data.upArrowPressedPath));

//            treeViewBottomBracket = new UIImage(content.Load<Texture2D>(data.treeViewBottomBracket));
//            treeViewMiddleBracket = new UIImage(content.Load<Texture2D>(data.treeViewMiddleBracket));
//            treeViewTopBracket = new UIImage(content.Load<Texture2D>(data.treeViewTopBracket));
//            treeViewMinus = new UIImage(content.Load<Texture2D>(data.treeViewMinus));
//            treeViewPlus = new UIImage(content.Load<Texture2D>(data.treeViewPlus));


//            whiteImage = new UIImage(content.Load<Texture2D>(data.whitePath));


//        }




//    }
//}
