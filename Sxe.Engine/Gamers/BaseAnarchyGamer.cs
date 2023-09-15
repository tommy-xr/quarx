using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text;
using System.IO;

using Sxe.Engine.Input;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Storage;
using Sxe.Engine.Gamers;
using Sxe.Engine.UI;
using Sxe.Engine.Storage;

namespace Sxe.Engine
{
    public enum AnarchyGamerState
    {
        NotSignedIn = 0,
        SignedInTemporary = 1,
        SignedInLocal = 2,
        SignedInLive = 3
    }

    /// <summary>
    /// Class that abstracts gamer information into an anarchy gamer class
    /// This allows for things like temporary profiles and such
    /// </summary>
    public class BaseAnarchyGamer : IAnarchyGamer
    {





        AnarchyGamerState state;
        IGameController controller;
        SignedInGamer gamer;
        UIImage gamerIcon = new UIImage();
        string gamerName;
        int index;
        Dictionary<string, object> tags = new Dictionary<string, object>();
        bool showDisconnectScreen = true;
        bool isSignedIn = false;
        IStorageDeviceService storageDevice;
        AnarchyGamerDefaults defaults = new AnarchyGamerDefaults();
        ContentManager content;


        public Dictionary<string, object> Tags
        {
            get { return tags; }
        }
        public IGameController Controller
        {
            get { return controller; }
        }
        public SignedInGamer Gamer
        {
            get { return gamer; }
            protected set { gamer = value; }
        }
        public AnarchyGamerState State
        {
            get { return state; }
            protected set { state = value; }
        }
        public string GamerTag
        {
            get { return gamerName; }
            protected set { gamerName = value; }
        }
        public UIImage GamerIcon
        {
            get { return gamerIcon; }
            //protected set { gamerIcon = value; }
        }
        public int Index
        {
            get { return index; }
        }

        public bool IsPlayer
        {
            get { return true; }
        }

        public bool IsSignedIn
        {
            get { return isSignedIn; }
        }

        public virtual bool IsTemporary
        {
            get { return true; }
        }

        public virtual bool IsSignedInToLive
        {
            get { return false; }
        }

        public virtual GamerPrivileges Privileges
        {
            get { return null; }
        }


        public AnarchyGamerDefaults GamerDefaults
        {
            get { return defaults; }
            protected set { this.defaults = value; }
        }

        public IStorageDeviceService StorageDevice
        {
            get { return this.storageDevice; }
            set { this.storageDevice = value; }
        }

        public GamerPresenceMode PresenceMode
        {
            get 
            {
                if (gamer != null)
                    return gamer.Presence.PresenceMode;
                else
                    return GamerPresenceMode.CornflowerBlue;
            }
            set 
            {
                if(gamer != null)
                gamer.Presence.PresenceMode = value;
            }
        }

        public virtual void OnSignIn()
        {
            this.isSignedIn = true;
        }

        public virtual void OnSignOut()
        {  
            this.isSignedIn = false;
        }



        public BaseAnarchyGamer(ContentManager inContent, int inIndex, IGameController inController)
        {
            content = inContent;
            index = inIndex;
            controller = inController;
            controller.Gamer = this;
            

            

        }

        public virtual void Save<T>(string fileName, T saveObject, SaveDelegate<T> saveFunc) where T : ICloneable
        {
            if (IsTemporary)
                return;

            if (this.storageDevice != null)
                storageDevice.Save<T>(Path.Combine(this.GamerTag, fileName), saveObject, saveFunc); 
        }

        public virtual LoadResult<T> Load<T>(string fileName, LoadDelegate<T> loadFunc)
        {
            if (IsTemporary)
                return null;

            if (this.storageDevice != null)
               return storageDevice.Load<T>(Path.Combine(this.GamerTag, fileName), loadFunc);

            return null;
        }


        public virtual void Update(GameTime gameTime)
        {
            //Update sensitivity based on our settings
            controller.Sensitivity = this.GamerDefaults.ControllerSensitivity;

            //This is commented out because otherwise you are fooked if you 
            //dont have a xbox controller hooked up


#if XBOX
            if (this.Controller.Connected == false && showDisconnectScreen)
            {
                //HACK to get the game screen service
                IGameScreenService gameScreenService = (IGameScreenService)AnarchyGamerComponent.GamerComponent.Game.Services.GetService(typeof(IGameScreenService));
                gameScreenService.AddScreen(AnarchyGamer.DisconnectScreen);
                showDisconnectScreen = false;
            }

            if (this.Controller.Connected == true && showDisconnectScreen == false)
                showDisconnectScreen = true;
#endif
        }


       
    }



}
