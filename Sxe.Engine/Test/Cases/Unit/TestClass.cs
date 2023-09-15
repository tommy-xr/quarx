using System;
using System.Collections.Generic;
using System.Text;

using Sxe.Engine.Test.Framework;

namespace Sxe.Engine.Test.Cases.Unit
{
    [SxeTestFixture]
    public class TestClass
    {
        [SxeTestSetup]
        public void TestSetup()
        {
        }

        [SxeTestTearDown]
        public void TestTearDown()
        {
        }

        [SxeTest]
        public void Test1()
        {
            int i = 0;
        }

        [SxeTest]
        public void Test2()
        {
            int i = 1;
            Console.WriteLine("Hey hey");
            Console.WriteLine("Hey hey hey hey");
            Assert.AreEqual(true, false);
        }

        [SxeTest]
        public void Test3()
        {
            int i = 2;
        }
    }
}
