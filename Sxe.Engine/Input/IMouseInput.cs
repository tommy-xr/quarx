using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;

using Sxe.Engine.UI;

namespace Sxe.Engine.Input
{
    /// <summary>
    /// A generic interface for mouse-type input
    /// </summary>
    public interface IMouseInput
    {
        Point AbsoluteMousePos { get; }
        Point AbsoluteMouseDelta { get; }

        bool IsLeftButtonJustReleased();
        bool IsLeftButtonJustPressed();
        bool IsRightButtonJustPressed();

        bool IsLeftButtonDown();
        bool IsMiddleButtonDown();
        bool IsRightButtonDown();

        bool FreezeMouse { get; set; }

        void Update(GameTime gameTime);

        void AddEventHandler(IInputEventReceiver handler);

    }
}
