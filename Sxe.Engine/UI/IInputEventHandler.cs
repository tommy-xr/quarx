using System;
using System.Collections.Generic;
using System.Text;

using Sxe.Engine.Input;

namespace Sxe.Engine.UI
{
    /// <summary>
    /// Interface for objects that can handle ui events
    /// </summary>
    public interface IInputEventReceiver
    {
        /// <summary>
        /// Returns true if the event was handled, false otherwise
        /// </summary>
        bool HandleEvent(InputEventArgs inputEvent);
    }

}
