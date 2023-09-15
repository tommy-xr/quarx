using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sxe.Engine.UI
{
    public interface IGameScreen : IInputEventReceiver
    {
        void Activate();
        void Deactivate();
        void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen) ;
        void Draw(SpriteBatch spriteBatch, GameTime gameTime);

        ScreenState ScreenState { get; }
        byte TransitionAlpha { get; }
        float TransitionPosition { get; }

        //FormCollection Forms { get; }
    }
}
