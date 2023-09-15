using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using Sxe.Engine.UI;

namespace Sxe.Engine.Input
{
    /// <summary>
    /// A generic interface for an item that handles key input
    /// </summary>
    public interface IKeyboardInput
    {
        bool IsKeyDown(Keys key);
        bool IsKeyJustPressed(Keys key);
        Keys[] GetPressedKeys();

        void Update(GameTime gameTime);

        void AddEventHandler(IInputEventReceiver handler);
    }
}
