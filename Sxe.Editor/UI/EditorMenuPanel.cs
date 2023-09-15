using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Sxe.Library.Editor;
using Sxe.Engine.UI;

namespace Sxe.Editor.UI
{
    public class EditorMenuPanel : Panel
    {
        JewelButton jewelButton;
        JewelSubPanel jewelSubMenu;

        public EditorMenuPanel(BaseScreen screen, EditorMenu menuDescription)
        {
            this.Parent = screen.Panel;
            this.Location = new Point(0, 0);
            //TODO: Make height adjustable
            this.Size = new Point((int)screen.DesignResolution.X, 50);


            jewelButton = new JewelButton();
            jewelButton.Parent = this;
            jewelButton.Location = new Point(0, 0);
            jewelButton.Size = new Point(50, 50);


            jewelSubMenu = new JewelSubPanel(menuDescription.JewelMenuEntries);
            jewelSubMenu.Parent = this;
            jewelSubMenu.Location = new Point(0, 20);
            jewelSubMenu.Size = new Point(50, 50);
            

            //Create the tab control
            TabControl tabs = new TabControl();
            tabs.Parent = this;
            tabs.Location = new Point(50, 0);
            tabs.Size = new Point(this.Size.X - 50, 50);

            TabPage page1 = new TabPage();
            page1.Caption = "Page1";
            tabs.Pages.Add(page1);

            TabPage page2 = new TabPage();
            page2.Caption = "Page2";
            tabs.Pages.Add(page2);

        }

        public override void ApplyScheme(IScheme scheme)
        {
            Texture2D blank = scheme.GetTexture("blank");
            this.Image = new UIImage(blank);
            this.BackColor = Color.Gray;

            base.ApplyScheme(scheme);
        }
    }
}
