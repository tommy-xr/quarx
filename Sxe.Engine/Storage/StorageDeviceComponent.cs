using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;

using Sxe.Engine.UI;

namespace Sxe.Engine.Storage
{
    public class StorageDeviceEventArgs : EventArgs
    {
        public StorageDevice StorageDevice { get; set; }
    }

    public class StorageDeviceComponent : GameComponent, IStorageDeviceService
    {
        public delegate void LoadSaveFunc(StorageDevice device);
        public event EventHandler<StorageDeviceEventArgs> StorageDeviceConnect;

        private IXboxMessageService xboxMessages;

        private StorageDevice device;
        private Queue<ISaveData> queueSaveData = new Queue<ISaveData>();
        private Queue<ILoadResult> queueLoadData = new Queue<ILoadResult>();
        private IAsyncResult deviceResult;
        private IAsyncResult loadSaveResult;
        private LoadSaveFunc loadSaveFunc;
        private IIsComplete loadSaveItem;
        bool getDevice = false;
        private StorageDeviceEventArgs eventArgs = new StorageDeviceEventArgs();

        bool refusedDevice = false; //this is to hold if the player refused to connect a device


        public StorageDeviceMode StorageDeviceMode
        {
            get;
            set;
        }

        public StorageDevice StorageDevice
        {
            get { return device; }
        }

        public StorageDeviceComponent(Game game)
            : base(game)
        {
            game.Services.AddService(typeof(IStorageDeviceService), this);
        }

        public override void Initialize()
        {
            base.Initialize();
            this.xboxMessages = Game.Services.GetService(typeof(IXboxMessageService)) as IXboxMessageService;
        }

        public override void Update(GameTime gameTime)
        {
            //First, see if we need to get started loading or saving the device
            if (getDevice && !refusedDevice)
            {

                if (device == null && deviceResult == null)
                {
                    if (!Guide.IsVisible)
                    {
                        deviceResult = Guide.BeginShowStorageDeviceSelector(null, null);
                    }
                }
                else if (device == null && deviceResult != null)
                {
                    if (deviceResult.IsCompleted)
                    {
                        device = Guide.EndShowStorageDeviceSelector(deviceResult);

                        if (device == null)
                        {
                            //Show a message box saying hey, you didn't select a storage device
                            XboxMessageInfo xmi = new XboxMessageInfo(new List<string>() { "Re-Select Device", "No thanks" });
                            xmi.Title = "No Storage Device";
                            xmi.Message = "Are you sure you don't want a storage device?\n";
                            xmi.Message += "-High scores will not be saved.\n";
                            xmi.Message += "-Puzzle progress will be lost.\n";
                            xmi.MessageBoxIcon = MessageBoxIcon.Error;
                            //Need some player index
                            xmi.PlayerIndex = (PlayerIndex)AnarchyGamer.Gamers[0].Index;
                            xmi.Completed += this.DeviceNotSelected;
                            this.xboxMessages.Add(xmi);
                        }
                        else
                        {

                            //Fire event, if we have it
                            if (StorageDeviceConnect != null)
                            {
                                this.eventArgs.StorageDevice = this.device;
                                this.StorageDeviceConnect(this, this.eventArgs);
                            }
                        }

                        deviceResult = null;
                        getDevice = false;
                    }

                    //OndeviceConnect(device);
                }
            }

            //Next, if our device is not null, check if it was disconnected
            if (device != null)
            {
                if (!device.IsConnected)
                    device = null;
            }

            if (device == null && refusedDevice)
            {
                //If we refused, just loop through the queue, and get rid of everything
                while (queueSaveData.Count > 0)
                    queueSaveData.Dequeue();

                while (queueLoadData.Count > 0)
                {
                    //Mark it as complete without loading
                    ILoadResult result = queueLoadData.Dequeue();
                    result.IsComplete = true;
                }
            }

            //Otherwise, f the device isn't null, check if we have any load or save requests pending
            if (device != null)
            {
                //If we aren't currently loading or saving anything, lets try and find something off the load queue
                if (loadSaveResult == null)
                {
                    ILoadResult loadResult = null;
                    if (queueLoadData.Count > 0)
                    {
                        loadResult = queueLoadData.Dequeue();
                        this.loadSaveItem = loadResult as IIsComplete ;
                        this.loadSaveFunc = loadResult.Load;
                        loadSaveFunc(this.device);

                        //added due to no async
                        this.loadSaveItem.IsComplete = true;
                        //this.loadSaveResult = loadSaveFunc.BeginInvoke(this.device, null, null);
                    }
                }

                //If the load result is still null, try and get something off the save queue
                if (loadSaveResult == null)
                {
                    ISaveData saveData = null;
                    if (queueSaveData.Count > 0)
                    {
                        saveData = queueSaveData.Dequeue();
                        this.loadSaveItem = saveData as IIsComplete;
                        this.loadSaveFunc = saveData.Save;
                        this.loadSaveFunc.Invoke(this.device);

                        //added due to no async
                        this.loadSaveItem.IsComplete = true;
                    }
                }

                //Ok, now check if we do have a loadresult, if its been completed
                //if (loadSaveResult != null)
                //{
                //    if (loadSaveResult.IsCompleted)
                //    {
                //        this.loadSaveFunc.EndInvoke(loadSaveResult);
                //        this.loadSaveItem.IsComplete = true;

                //        this.loadSaveItem = null;
                //        this.loadSaveFunc = null;
                //        this.loadSaveResult = null;
                //    }
                //}

 
            }

            base.Update(gameTime);
        }

        //public bool FileExists(string fileName)
        //{
        //    StorageContainer container = this.GetContainer();

        //    //See if this file exists...
        //    bool exists = System.IO.File.Exists(Path.Combine(container.Path, fileName));
        //    container.Dispose();
        //    return exists;
        //}

        private void DeviceNotSelected(object sender, XboxMessageEventArgs args)
        {

            this.refusedDevice = true;
            if (args.Result.HasValue)
            {
                if (args.Result == 0)
                {
                    this.getDevice = true;
                    this.refusedDevice = false;
                }
            }
        }

        public void Save<T>(string fileName, T saveObject) where T : ICloneable
        {
            this.Save(fileName, saveObject, null);
        }

        public void Save<T>(string fileName, T saveObject, SaveDelegate<T> saveFunc) where T : ICloneable
        {
            this.getDevice = true;


            queueSaveData.Enqueue(new SaveData<T>(saveObject, fileName, saveFunc));
        }

        public LoadResult<T> Load<T>(string fileName, LoadDelegate<T> loadFunc)
        {
            this.getDevice = true;
            LoadResult<T> result = new LoadResult<T>(fileName, loadFunc);


             queueLoadData.Enqueue(result);
            
            return result;
        }

        private StorageContainer GetContainer()
        {
            return device.OpenContainer(SxeGame.GetGameInstance.GetGameTitle());
        }

        
    }
}
