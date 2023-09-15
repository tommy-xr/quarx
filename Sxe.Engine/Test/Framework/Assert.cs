using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;

namespace Sxe.Engine.Test.Framework
{
    public static class Assert
    {
        public static void AreEqual<T>(T expected, T actual) where T : IEquatable<T>
        {
            if (!expected.Equals(actual))
                Fail(expected.ToString(), actual.ToString());
        }

        public static void AreEqual(Vector2 expected, Vector2 actual)
        {
            AreEqual<Vector2>(expected, actual);
        }
        public static void AreEqual(Rectangle expected, Rectangle actual)
        {
            AreEqual<Rectangle>(expected, actual);
        }

        public static void AreEqual(int expected, int actual)
        {
            if (expected != actual)
                Fail(expected.ToString(), actual.ToString());
        }

        public static void AreObjectsEqual(object expected, object actual)
        {
            if (expected != actual)
                Fail(expected.ToString(), actual.ToString());
        }

        public static void AreEqual(bool expected, bool actual)
        {
            if (expected != actual)
                Fail(expected.ToString(), actual.ToString());
        }

        public static void AreEqual(string expected, string actual)
        {
            if (expected != actual)
                Fail(expected, actual);
        }

        public static void IsTrue(bool condition)
        {
            AreEqual(condition, true);
        }
        public static void IsFalse(bool condition)
        {
            AreEqual(condition, false);
        }

        public static void IsNull(object obj)
        {
            if (obj != null)
                Fail("[Null]", obj.ToString());
        }


        static void Fail(string expected, string actual)
        {
            Fail(expected, actual, "Assertion Failed: ");
        }

        static void Fail(string expected, string actual, string message)
        {
            throw new SxeTestException(message + String.Format("[Expected: {0}] [Actual: {1}]", expected, actual));
        }

    }
}
