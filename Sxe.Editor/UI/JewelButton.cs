using System;
using System.Collections.Generic;
using System.Text;

using Sxe.Engine.UI;

namespace Sxe.Editor.UI
{
    public class JewelButton : Button
    {
        public override void ApplyScheme(IScheme scheme)
        {
            base.ApplyScheme(scheme);
            this.OverImage = scheme.GetImage("jewel_over");
            this.DefaultImage = scheme.GetImage("jewel_default");
        }
    }
}
