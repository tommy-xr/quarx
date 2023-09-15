using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Storage;

using Sxe.Engine.Input;
using Sxe.Engine.Storage;


namespace Sxe.Engine
{
    public class BaseAIGamer : IAnarchyGamer
    {
        Dictionary<string, object> tags = new Dictionary<string, object>();
        private AnarchyGamerDefaults gamerDefaults = new AnarchyGamerDefaults();


        bool IAnarchyGamer.IsPlayer { get { return false; } }
        int IAnarchyGamer.Index { get { return -1; } }

        public bool IsSignedInToLive { get { return false; } }
        public GamerPrivileges Privileges { get { return null; } }

        IGameController IAnarchyGamer.Controller { get { return null; } }


        public virtual UI.UIImage GamerIcon { get { return null; } }
        public virtual string GamerTag { get { return "DefaultAIGamer" ; } }

        public virtual void Update(GameTime gameTime) { }

        public bool IsTemporary
        {
            get { return false; }
        }

        public bool IsSignedIn
        {
            get { return true; }
        }

        public AnarchyGamerDefaults GamerDefaults
        {
            get { return gamerDefaults; }
        }

        public Dictionary<string, object> Tags
        {
            get { return tags; }
        }

        public void Save<T>(string fileName, T saveObject, SaveDelegate<T> saveFunc) where T : ICloneable
        {
            throw new NotSupportedException("AI gamers may not save data.");
        }

        public LoadResult<T> Load<T>(string fileName, LoadDelegate<T> loadFunc)
        {
            throw new NotSupportedException("AI gamers may not load data.");
        }

        public virtual void OnSignIn()
        {
        }

        public virtual void OnSignOut()
        {
        }

        public GamerPresenceMode PresenceMode
        {
            get;
            set;
        }

        public IStorageDeviceService StorageDevice
        {
            get;
            set;
        }
    } 
}
