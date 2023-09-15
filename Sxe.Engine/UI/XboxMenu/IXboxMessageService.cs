using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace Sxe.Engine.UI
{
    public interface IXboxMessageService
    {
        void Add(XboxMessageInfo message);
        void ShowMarketPlace(PlayerIndex playerIndex);
        void ShowSignIn(int paneCount, bool liveOnly);
    }
}
