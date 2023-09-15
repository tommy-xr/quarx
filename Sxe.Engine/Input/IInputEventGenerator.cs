using System;
using System.Collections.Generic;
using System.Text;

using Sxe.Engine.UI;

namespace Sxe.Engine.Input
{
    public interface IInputEventGenerator
    {
        void AddEventHandler(IInputEventReceiver handler);

    }
}
