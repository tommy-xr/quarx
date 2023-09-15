using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace SXEMaterialManager
{
    public class BindableTest
    {
        private int attr1;
        private string attr2;

        [CategoryAttribute("Atte1")]
        public int Attr1
        {
            get { return attr1; }
            set { attr1 = value; }
        }

        [CategoryAttribute("Attr2")]
        [Description("Hey hey")]
        public string Attr2
        {
            get { return attr2; }
            set { attr2 = value; }
        }

    }
}
