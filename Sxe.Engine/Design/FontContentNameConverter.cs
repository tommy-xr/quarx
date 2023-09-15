using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace Sxe.Design
{
    public class FontContentNameConverter : BaseContentNameConverter
    {
        protected override System.Collections.Specialized.StringCollection AllowedProcessors
        {
            get
            {
                //return base.AllowedProcessors;
                StringCollection strings = new StringCollection();
                strings.Add("FontDescriptionProcessor");
                return strings;
            }
        }
    }
}
