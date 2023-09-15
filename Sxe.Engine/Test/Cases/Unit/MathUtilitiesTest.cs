using System;
using System.Collections.Generic;
using System.Text;

using Sxe.Engine.Test.Framework;
using Sxe.Library;

namespace Sxe.Engine.Test.Cases.Unit
{
    [SxeTestFixture]
    public class MathUtilitiesTest
    {
        [SxeTest]
        public void TestMax()
        {
            //Test with floats
            Assert.AreEqual(5.0f, MathUtilities.Max(1.0f, 3.0f, 5.0f));
            Assert.AreEqual(5.0f, MathUtilities.Max(-1.0f, 1.0f, 5.0f));
            Assert.AreEqual(-2.0f, MathUtilities.Max(-2.0f, -5.0f, -100.0f));
        }

        [SxeTest]
        public void TestMin()
        {
            //Test with floats
            Assert.AreEqual(1.0f, MathUtilities.Min(1.0f, 3.0f, 5.0f));
            Assert.AreEqual(-1.0f, MathUtilities.Min(-1.0f, 1.0f, 5.0f));
            Assert.AreEqual(-100.0f, MathUtilities.Min(-2.0f, -5.0f, -100.0f));
        }

    }
}
