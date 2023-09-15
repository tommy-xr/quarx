using System;
using System.Collections.Generic;
using System.Text;

using Sxe.Engine.UI;

namespace Sxe.Engine.Test.Framework
{

    public class NamespaceNode : TreeNode
    {
    }

    public class TestFixtureNode : TreeNode
    {
        UnitTestFixtureInfo fixture;

        public UnitTestFixtureInfo Fixture
        {
            get { return fixture; }
        }
        public TestFixtureNode(UnitTestFixtureInfo info)
        {
            fixture = info;
        }
    }

    public class TestCaseNode : TreeNode
    {
        UnitTestCaseInfo testCase;

        public UnitTestCaseInfo TestCase
        {
            get { return testCase; }
        }

        public TestCaseNode(UnitTestCaseInfo info)
        {
            testCase = info;
        }
    }
}
