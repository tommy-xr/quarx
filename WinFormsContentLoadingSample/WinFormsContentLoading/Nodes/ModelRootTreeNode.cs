using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace WinFormsContentLoading 
{
    public class ModelRootTreeNode : TreeNode
    {
        string contentPath;
        public string ContentPath
        {
            get { return contentPath; }
            set { contentPath = value; }
        }

        ModelInfo info;
        public ModelInfo ModelInfo
        {
            get { return info; }
            set { info = value; }
        }

        public ModelRootTreeNode(string inPath, ModelInfo mi)
            : base(inPath)
        {
            contentPath = inPath;
            info = mi;
        }
    }
}
