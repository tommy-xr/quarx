using System;
using System.Collections.Generic;
using System.Text;

using Sxe.Engine.UI;

namespace Sxe.Engine.Input
{
    /// <summary>
    /// Base class for most input devices. Generates input events
    /// </summary>
    public class InputEventGenerator : IInputEventGenerator
    {
        List<IInputEventReceiver> eventHandlers = new List<IInputEventReceiver>();

        public void AddEventHandler(IInputEventReceiver handler)
        {
            eventHandlers.Add(handler);
        }

        protected void FireEvent(InputEventArgs args)
        {
            for (int i = 0; i < eventHandlers.Count; i++)
            {
                eventHandlers[i].HandleEvent(args);
            }
        }
    }
}
