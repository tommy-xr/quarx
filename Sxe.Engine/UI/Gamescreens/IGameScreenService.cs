using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using Sxe.Engine.Input; 
using Sxe.Engine.Storage;

namespace Sxe.Engine.UI
{
    /// <summary>
    /// The interface for the service that manages all the gamescreens
    /// </summary>
    public interface IGameScreenService : IInputEventReceiver
    {
        SchemeManager Schemes { get; }
        ContentManager Content { get; }
        IServiceProvider Services { get; }
        IInputService Input { get; }
        IAnarchyGamerService AnarchyGamers { get; }
        IStorageDeviceService Storage { get; }
        IXboxMessageService XboxMessage { get; }
        GraphicsDevice GraphicsDevice { get; }
        AudioManager Audio { get; }
        SxeGame Game { get; }

        //I2DRendererService Renderer { get; }
        //IAudioService Audio { get; }


        //IGameScreen CurrentScreen { get;}
        //void Pop();
        //void Push(IGameScreen gs);
        void AddScreen(BaseScreen screen);
        void RemoveScreen(BaseScreen screen);
        bool ContainsScreen(BaseScreen screen);

        void FadeBackBufferToBlack(int alpha);

    }
}
