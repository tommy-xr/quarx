using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Sxe.Library.Editor
{
    /// <summary>
    /// Class for buttons in the ribbon
    /// </summary>
    public class RibbonStripEntry
    {
        string command;
        public string Command
        {
            get { return command; }
            set { command = value; }
        }

        string defaultImagePath;
        public string DefaultImagePath
        {
            get { return defaultImagePath; }
            set { defaultImagePath = value; }
        }

        string overImagePath;
        public string OverImagePath
        {
            get { return overImagePath; }
            set { overImagePath = value; }
        }

        Point location;
        public Point Location
        {
            get { return location; }
            set { location = value; }
        }

        Point size;
        public Point Size
        {
            get { return size; }
            set { size = value; }
        }

        string toolTip;
        [ContentSerializer(Optional=true)]
        public string ToolTip
        {
            get { return toolTip; }
            set { toolTip = value; }
        }

        public class RibbonStripEntryReader : ContentTypeReader<RibbonStripEntry>
        {
            protected override RibbonStripEntry Read(ContentReader input, RibbonStripEntry existingInstance)
            {
                RibbonStripEntry ribbonEntry = existingInstance;
                if (ribbonEntry == null)
                    ribbonEntry = new RibbonStripEntry();

                ribbonEntry.command = input.ReadString();
                ribbonEntry.defaultImagePath = input.ReadString();
                ribbonEntry.overImagePath = input.ReadString();
                ribbonEntry.location = input.ReadRawObject<Point>();
                ribbonEntry.size = input.ReadRawObject<Point>();
                ribbonEntry.toolTip = input.ReadString();
                return ribbonEntry;
            }
        }
    }

}
