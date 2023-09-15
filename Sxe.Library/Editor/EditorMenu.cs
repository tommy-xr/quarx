using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework.Content;

namespace Sxe.Library.Editor
{
    public class EditorMenu
    {
        List<MenuStripEntry> jewelMenuEntries = new List<MenuStripEntry>();
        public List<MenuStripEntry> JewelMenuEntries
        {
            get { return jewelMenuEntries; }
            set { jewelMenuEntries = value; }
        }

        List<RibbonCategory> ribbonCategories = new List<RibbonCategory>();
        public List<RibbonCategory> RibbonCategories
        {
            get { return ribbonCategories; }
            set { ribbonCategories = value; }
        }

        public class EditorMenuReader : ContentTypeReader<EditorMenu>
        {
            protected override EditorMenu Read(ContentReader input, EditorMenu existingInstance)
            {
                EditorMenu menu = existingInstance;
                if (menu == null)
                    menu = new EditorMenu();

                menu.jewelMenuEntries.AddRange(input.ReadObject<List<MenuStripEntry>>());
                menu.ribbonCategories.AddRange(input.ReadObject<List<RibbonCategory>>());

                return menu;
            }
        }
    }
}
