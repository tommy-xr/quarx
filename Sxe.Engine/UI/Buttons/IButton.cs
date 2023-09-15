using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;

namespace Sxe.Engine.UI
{
    public interface IButton
    {
        void PerformClick(int index);
        void Over(int index);
        void Leave(int index);
    }
}
