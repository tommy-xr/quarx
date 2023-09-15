using System;
using System.Collections.Generic;
using System.Text;

namespace Sxe.Engine.Test.Framework
{
    /// <summary>
    /// Thrown when an assertion fails
    /// </summary>
    public class SxeTestException : Exception
    {
        public SxeTestException()
            : base()
        {
        }

        public SxeTestException(string message)
            : base(message)
        {
        }
    }
}
