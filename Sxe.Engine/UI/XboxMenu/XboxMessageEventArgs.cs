using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace Sxe.Engine.UI
{
    public class XboxMessageEventArgs : EventArgs
    {
        public int? Result
        {
            get;
            set;
        }

        public PlayerIndex PlayerIndex
        {
            get;
            set;
        }
    }
}
