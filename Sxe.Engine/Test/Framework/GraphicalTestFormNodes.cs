using System;
using System.Collections.Generic;
using System.Text;

using Sxe.Engine.UI;

namespace Sxe.Engine.Test.Framework
{
    public class GraphicalCategoryNode : TreeNode
    {
    }

    public class GraphicalTestNode : TreeNode
    {
        GraphicalTestInfo info;

        public GraphicalTestInfo TestInfo
        {
            get { return info; }
        }

        public GraphicalTestNode(GraphicalTestInfo testInfo)
        {
            info = testInfo;
        }
    }
}
