using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Sxe.Engine.Input;

namespace Sxe.Engine.UI
{
    /// <summary>
    /// This is the title bar of forms
    /// </summary>
    public class FormTitleBarPanel : Panel 
    {
        Panel iconPanel; //the panel to display the little icon
        Label title;
        Button closeButton;

        public string Text
        {
            get { return title.Caption; }
            set { title.Caption = value; }
        }

        public UIImage Icon
        {
            get { return iconPanel.Image; }
            set { iconPanel.Image = value; }
        }

        public event EventHandler<MouseEventArgs> CloseClick;

        //Scheme stuff
        Point closeButtonOffset = new Point(8, 8);
        Point closeButtonSize = new Point(32, 32);
        int formTitlePadding = 8;
        Point formIconSize = new Point(32, 32);

        public FormTitleBarPanel()
        {
 
            //title = new Label(this, new Point(scheme.FormTitlePadding, 0),
            //    new Point(size.X - scheme.FormTitlePadding, size.Y), scheme.TitleFont, Color.White, titleScheme);
            title = new Label();
            title.Parent = this;

            title.HorizontalAlignment = HorizontalAlignment.Left;
            title.VerticalAlignment = VerticalAlignment.Middle;

            //iconPanel = new Panel(this, new Point(4, (Size.Y - titleScheme.FormIconSize.Y) / 2), titleScheme.FormIconSize, icon);

            iconPanel = new Panel();
            iconPanel.Parent = this;

            //closeButton = new Button(this, new Point(this.Size.X - scheme.CloseButtonOffset.X - scheme.CloseButtonSize.X, scheme.CloseButtonOffset.Y),
            //    scheme.CloseButtonSize, titleScheme.TitleFont,
            //    scheme.CloseButtonImage, scheme.CloseButtonOverImage, scheme.CloseButtonClickImage,
            //    Color.White, BorderPanelMode.Stretch, scheme);
            closeButton = new Button();
            closeButton.Parent = this;

            closeButton.MouseClick += OnClose;

            this.SizeChanged += OnSizeChanged;


        }

        public override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            this.Image = LoadImage(content, "blank");

            this.closeButton.Image = LoadImage(content, "blank");
            this.closeButton.BackColor = Color.Red;

            base.LoadContent(content);
        }

        void OnSizeChanged(object sender, EventArgs args)
        {
            closeButton.Location = new Point(this.Size.X - closeButtonOffset.X - closeButtonSize.X, closeButtonOffset.Y);
            closeButton.Size = closeButtonSize;

            title.Location = new Point(formTitlePadding, 0);
            title.Size = new Point(Size.X - formTitlePadding, Size.Y);

            iconPanel.Location = new Point(4, (Size.Y - formIconSize.Y) / 2);
            iconPanel.Size = new Point(formIconSize.X, formIconSize.Y);
        }

        //public override void ApplyScheme(IScheme scheme)
        //{
        //    base.ApplyScheme(scheme);

        //    //Get close button stuff
        //    UIImage closeButtonImage = scheme.GetImage("form_close_button_default");
        //    UIImage closeButtonOver = scheme.GetImage("form_close_button_over");
        //    UIImage closeButtonClick = scheme.GetImage("form_close_button_click");
        //    closeButtonOffset = scheme.GetPoint("form_close_button_offset");
        //    closeButtonSize = scheme.GetPoint("form_close_button_size");

            
        //    closeButton.DefaultImage = closeButtonImage;
        //    closeButton.OverImage = closeButtonOver;
        //    closeButton.ClickImage = closeButtonClick;




        //    //Apply scheme to title
        //    formTitlePadding = scheme.GetInt("form_title_padding");
        //    UIImage titleImage = scheme.GetImage("form_title_background");
        //    this.Image = titleImage;



        //    title.Font = scheme.GetFont("form_title_font");

        //    formIconSize = scheme.GetPoint("form_icon_size");

        //}

        //public FormTitleBarPanel(IPanelContainer parent, Point position, Point size, IScheme titleScheme, UIImage icon)
        //    : base(parent, position, size, titleScheme.FormTitleBackground, BorderPanelMode.Resize)
        //{
        //    scheme = titleScheme;
        //    title = new Label(this, new Point(scheme.FormTitlePadding, 0), 
        //        new Point(size.X - scheme.FormTitlePadding, size.Y), scheme.TitleFont, Color.White, titleScheme);
        //    title.HorizontalAlignment = HorizontalAlignment.Left;
        //    title.VerticalAlignment = VerticalAlignment.Middle;

        //    iconPanel = new Panel(this, new Point(4, (Size.Y - titleScheme.FormIconSize.Y) / 2), titleScheme.FormIconSize, icon);

        //    closeButton = new Button(this, new Point(this.Size.X - scheme.CloseButtonOffset.X - scheme.CloseButtonSize.X, scheme.CloseButtonOffset.Y), 
        //        scheme.CloseButtonSize,titleScheme.TitleFont, 
        //        scheme.CloseButtonImage, scheme.CloseButtonOverImage, scheme.CloseButtonClickImage,
        //        Color.White, BorderPanelMode.Stretch, scheme);

        //    closeButton.MouseClick += OnClose;


        //}

        void OnClose(object sender, MouseEventArgs args)
        {
            if(CloseClick != null)
                CloseClick(this, args);
        }

    }
}
