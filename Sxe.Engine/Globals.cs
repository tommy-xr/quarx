using System;
using System.Collections.Generic;
using System.Text;

namespace Sxe.Engine
{
    public static class Globals
    {
        static bool debugMode = false;
        public static bool DebugMode
        {
            get { return debugMode; }
            set { debugMode = value; }
        }
    }
}
