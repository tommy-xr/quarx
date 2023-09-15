#if !XBOX

using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Text;

using System.Windows.Forms;

namespace Sxe.Design.ImagePicker
{
    class PathNode : TreeNode
    {
        
        private string fullPath;
        private StringCollection contentNames = new StringCollection();

        public string FullPath
        {
            get { return fullPath; }
            set { fullPath = value; }
        }

        public StringCollection ContentNames
        {
            get { return contentNames; }
        }
    }
}

#endif
