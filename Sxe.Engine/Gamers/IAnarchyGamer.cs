using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Storage;

using Sxe.Engine.Input;
using Sxe.Engine.UI;
using Sxe.Engine.Storage;

namespace Sxe.Engine
{
    /// <summary>
    /// The interface for anything that can be an anarchy gamer
    /// This is very general to allow for both players and AI,
    /// and doesn't include any specific implementation details
    /// </summary>
    public interface IAnarchyGamer
    {
        UIImage GamerIcon { get; }
        string GamerTag { get; }
        bool IsPlayer { get; }
        bool IsTemporary { get; }
        bool IsSignedIn { get; }
        bool IsSignedInToLive { get; }

        GamerPresenceMode PresenceMode { get; set; }
        AnarchyGamerDefaults GamerDefaults { get; }
        GamerPrivileges Privileges { get; }
        


        IGameController Controller { get; }
        int Index { get; }
        Dictionary<string, object> Tags { get; }

        void Update(GameTime gameTime);

        void Save<T>(string fileName, T saveObject, SaveDelegate<T> saveFunc) where T : ICloneable;
        LoadResult<T> Load<T>(string fileName, LoadDelegate<T> loadFunc);
        IStorageDeviceService StorageDevice { get; set; }

        void OnSignIn();
        void OnSignOut();
    
    }

}
