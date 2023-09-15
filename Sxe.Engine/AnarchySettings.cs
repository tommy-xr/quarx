using System;
using System.Collections.Generic;
using System.Text;

namespace Sxe.Engine
{
    /// <summary>
    /// Static manager for all kinds of things anarchy related
    /// </summary>
    public static class AnarchySettings
    {
        static bool showTitleSafeBars;
        public static bool ShowTitleSafeBars
        {
            get { return showTitleSafeBars; }
            set { showTitleSafeBars = value; }
        }

    }
}
