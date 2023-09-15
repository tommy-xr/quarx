using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework.Content;

namespace Sxe.Library.Editor
{
    public class MenuStripEntry
    {
        string iconPath;
        [ContentSerializer(Optional=true)]
        public string IconPath
        {
            get { return iconPath; }
            set { iconPath = value; }
        }

        string caption;
        public string Caption
        {
            get { return caption; }
            set { caption = value; }
        }

        string commandName;
        public string CommandName
        {
            get { return commandName; }
            set { commandName = value; }
        }

        bool showCheck = false;
        [ContentSerializer(Optional=true)]
        public bool ShowCheck
        {
            get { return showCheck; }
            set { showCheck = value; }
        }



        List<MenuStripEntry> subEntries = new List<MenuStripEntry>();
        public List<MenuStripEntry> SubEntries
        {
            get { return subEntries; }
            set { subEntries = value; }
        }

        bool isChecked = false;
        [ContentSerializerIgnore]
        public bool IsCheckecd
        {
            get { return isChecked; }
            set { isChecked = value; }
        }

        public class MenuStripEntryReader : ContentTypeReader<MenuStripEntry>
        {
            protected override MenuStripEntry Read(ContentReader input, MenuStripEntry existingInstance)
            {
                MenuStripEntry menuStrip = existingInstance;
                if (menuStrip == null)
                    menuStrip = new MenuStripEntry();

                menuStrip.iconPath = input.ReadString();
                menuStrip.caption = input.ReadString();
                menuStrip.commandName = input.ReadString();
                menuStrip.showCheck = input.ReadBoolean();
                menuStrip.subEntries.AddRange(input.ReadObject<List<MenuStripEntry>>());
                return menuStrip;
            }
        }
    }
}
