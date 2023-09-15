using System;
using System.Collections.Generic;
using System.Text;

namespace Sxe.Library.Graphics.Materials
{
    public class MaterialEffectAttribute : Attribute
    {
        string defaultEffectName;
        public string DefaultEffectName
        {
            get { return defaultEffectName; }
            set { defaultEffectName = value; }
        }

        public MaterialEffectAttribute(string effectName)
        {
            defaultEffectName = effectName;
        }
    }
}
