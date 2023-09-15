using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework.Content;

namespace Sxe.Library.Editor
{
    public class RibbonCategory
    {
        string captionName;
        public string CaptionName
        {
            get { return captionName; }
            set { captionName = value; }
        }

        List<RibbonStripEntry> entries = new List<RibbonStripEntry>();
        public List<RibbonStripEntry> Entries
        {
            get { return entries; }
            set { entries = value; }
        }

        public class RibbonCategoryReader : ContentTypeReader<RibbonCategory>
        {
            protected override RibbonCategory Read(ContentReader input, RibbonCategory existingInstance)
            {
                RibbonCategory category = existingInstance;
                if (category == null)
                    category = new RibbonCategory();

                category.captionName = input.ReadString();
                category.entries.AddRange(input.ReadObject<List<RibbonStripEntry>>());
                return category;
            }
        }
    }
}
