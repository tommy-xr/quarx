using System;
using System.Collections.Generic;
using System.Text;

namespace Sxe.Engine
{
    public class AnarchySignedInEventArgs : EventArgs
    {
        IAnarchyGamer gamer;

        public IAnarchyGamer Gamer
        {
            get { return gamer; }
            set { gamer = value; }
        }

        public AnarchySignedInEventArgs(IAnarchyGamer inGamer)
        {
            gamer = inGamer;
        }
    }

    public class AnarchySignedOutEventArgs : EventArgs
    {
        IAnarchyGamer gamer;

        public IAnarchyGamer Gamer
        {
            get { return gamer; }
            set { gamer = value; }
        }

        public AnarchySignedOutEventArgs(IAnarchyGamer inGamer)
        {
            gamer = inGamer;
        }
    }
}
