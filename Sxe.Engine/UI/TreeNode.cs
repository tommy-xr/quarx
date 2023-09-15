using System;
using System.Collections.Generic;
using System.Text;

namespace Sxe.Engine.UI
{
    /// <summary>
    /// Tree node class for Sxe UI elements
    /// </summary>
    public class TreeNode
    {
        List<TreeNode> children;
        bool expanded;
        string text = "";
        UIImage image;

        public List<TreeNode> Nodes
        {
            get { return children; }
        }

        public TreeNode()
        {
            children = new List<TreeNode>();
        }

        public bool Expanded
        {
            get { return expanded; }
            set { expanded = value; }
        }

        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        public UIImage Image
        {
            get { return image; }
            set { image = value; }
        }
    }
}
