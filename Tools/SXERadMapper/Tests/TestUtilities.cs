using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;

using NUnit.Framework;

namespace SXERadMapper.Tests
{
    [TestFixture]
    public class TestUtilities
    {
        [Test]
        public void TestMinMax()
        {
            Assert.AreEqual(0, Utilities.Min<int>(0, 1, 2));
            Assert.AreEqual(2, Utilities.Max<int>(0, 1, 2));
        }

        [Test]
        public void TestBarycentricSimple()
        {
            Point p1 = new Point(0, 0);
            Point p2 = new Point(0, 1);
            Point p3 = new Point(1, 1);

            float alpha;
            float beta;
            float gamma;
            Utilities.CalcBarycentric(p1, p1, p2, p3, out alpha, out beta, out gamma);
            Assert.AreEqual(1.0f, alpha);

            Utilities.CalcBarycentric(p2, p1, p2, p3, out alpha, out beta, out gamma);
            Assert.AreEqual(1.0f, beta);

            Utilities.CalcBarycentric(p3, p1, p2, p3, out alpha, out beta, out gamma);
            Assert.AreEqual(1.0f, gamma);
        }

        [Test]
        public void TestBarycentricLines()
        {
            Point p1 = new Point(0, 0);
            Point p2 = new Point(0, 4);
            Point p3 = new Point(1, 4);

            float alpha, beta, gamma;
            Utilities.CalcBarycentric(new Point(0, 1), p1, p2, p3, out alpha, out beta, out gamma);
            Assert.AreEqual(0.75, alpha);
            Assert.AreEqual(0.25, beta);

        }
    }
}
