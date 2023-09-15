using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;

namespace Sxe.Engine
{
    public interface IAnarchyGamerService
    {
        void StartTemporaryProfile(PlayerIndex pi);
        void AddAI(IAnarchyGamer gamer);
    }
}
