using System;
using System.Collections.Generic;
using System.Text;

namespace Sxe.Engine.Input
{

    /// <summary>
    /// Interface for the engine's input component
    /// </summary>
    public interface IInputService
    {
        IMouseInput Mouse { get; set; }
        IKeyboardInput Keyboard { get; set; }

        IGameController Controller { get; }
        ControllerCollection Controllers { get; }

        bool DrawCursor { get; set; }

    }
}
