using System;
using System.Collections.Generic;
using System.Text;

using Sxe.Engine.UI;

using Microsoft.Xna.Framework;

namespace Sxe.Editor.UI
{
    public class ImageBrowser : ScrollablePanel
    {
        EventList<UIImage> imageList = new EventList<UIImage>();


        Point iconSize = new Point(64, 64);
        int selected = -1;

        Button[] buttons;
        
        //Dictionary<UIImage, ImageBrowserEntry> imageDictionary = new Dictionary<UIImage, ImageBrowserEntry>();

        public EventList<UIImage> Images
        {
            get { return imageList; }
        }

        public ImageBrowser()
        {
            this.SizeChanged += OnSizeChanged;
            imageList.ListModified += OnListChanged;
        }

        public override void ApplyScheme(IScheme scheme)
        {
            base.ApplyScheme(scheme);

            ResetImages();
        }

        void OnSizeChanged(object sender, EventArgs args)
        {
            int numButtons = this.Size.Y / iconSize.Y;

            if (buttons != null)
            {
                for (int i = 0; i < buttons.Length; i++)
                {
                    buttons[i].Parent = null;
                }
            }

            buttons = new Button[numButtons];

            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i] = new Button();
                buttons[i].Parent = this;
                buttons[i].Location = new Point(0, iconSize.Y * i);
                buttons[i].Size = iconSize;
            }

            DisplaySize = new Point(1, numButtons);

        }

        void OnListChanged(object sender, EventArgs args)
        {
            FullSize = new Point(1, imageList.Count);
            ResetImages();



        }

        void ResetImages()
        {
            //Update images
            for (int i = 0;
                i < Math.Min(DisplaySize.Y, imageList.Count - DisplayCoordinates.Y); i++)
            {
                buttons[i].Image = imageList[i];
                buttons[i].OverImage = imageList[i];
                buttons[i].DefaultImage = imageList[i];
                buttons[i].ClickImage = imageList[i];
            }
        }
    }
}
