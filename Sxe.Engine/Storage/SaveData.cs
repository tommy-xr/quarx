using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using Sxe.Library.Utilities;

using Microsoft.Xna.Framework.Storage;

namespace Sxe.Engine.Storage
{
    public interface ISaveData : IIsComplete
    {
        void Save(StorageDevice device);
    }

    public delegate void SaveDelegate<T>(T saveObject, string path);

    public class SaveData<T> : ISaveData where T : ICloneable
    {
        T saveData;
        bool completed = false;

        public T Data
        {
            get { return saveData; }
        }

        string fileName;
        public string FileName
        {
            get { return fileName; }
        }

        public bool IsComplete
        {
            get { return completed; }
            set { completed = value; }
        }

        public SaveData(T inSaveData, string inFileName, SaveDelegate<T> saveFunc)
        {
            this.SaveFunc = saveFunc;
            saveData = (T)inSaveData.Clone();
            fileName = inFileName;
        }

        public SaveDelegate<T> SaveFunc
        {
            get;
            set;
        }

        public void Save(StorageDevice device)
        {
            StorageContainer container = device.OpenContainer(SxeGame.GetGameInstance.GetGameTitle());

            string path = Path.Combine(container.Path, fileName);

            if (SaveFunc == null)
                XmlIO.Save<T>(saveData,
                    Path.Combine(container.Path, fileName),
                    true);
            else
                SaveFunc(saveData, path);

            container.Dispose();
        }
    }
}
