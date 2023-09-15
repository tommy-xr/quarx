using System;
using System.Collections.Generic;
using System.Text;

namespace Sxe.Engine.Test.Framework
{
    public class SxeTestFixture : Attribute
    {   
    }

    public class SxeTest : Attribute
    {
    }

    public class SxeTestSetup : Attribute
    {
    }

    public class SxeTestTearDown : Attribute
    {
    }

    public class GraphicalTestCategory : Attribute
    {
        string categoryName;
        public string Name
        {
            get { return categoryName; }
        }

        public GraphicalTestCategory(string category)
        {
            categoryName = category;
        }
    }
}
