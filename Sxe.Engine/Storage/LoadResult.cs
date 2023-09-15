using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using Microsoft.Xna.Framework.Storage;

using Sxe.Library.Utilities;

namespace Sxe.Engine.Storage
{


    public interface ILoadResult : IIsComplete
    {
        void Load(StorageDevice device);
    }

    public delegate T LoadDelegate<T>(string path);


    /// <summary>
    /// Ghetto version of load result, for use with 
    /// loading data from a gamer
    /// </summary>
    public class LoadResult<T> : ILoadResult, IIsComplete
    {
        bool isComplete = false;
        public bool IsComplete
        {
            get { return isComplete; }
            set { isComplete = value; }
        }

        T loadedData = default(T);
        public T LoadedData
        {
            get { return loadedData; }
            set { loadedData = value; }
        }

        public LoadDelegate<T> LoadFunc
        {
            get;
            set;
        }

        string fileName;

        //dont think this is needed
        //public string FileName
        //{
        //    get { return fileName; }
        //}

        public LoadResult(string inFileName, LoadDelegate<T> loadFunc)
        {
            fileName = inFileName;
            this.LoadFunc = loadFunc;
        }

        public void Load(StorageDevice device)
        {

            StorageContainer container = device.OpenContainer(SxeGame.GetGameInstance.GetGameTitle());
            //First see if the path exists
            string path = Path.Combine(container.Path, fileName);

            if (File.Exists(path))
            {

                if (LoadFunc == null)
                    loadedData = XmlIO.Load<T>(
                        path, true);
                else
                    loadedData = this.LoadFunc(path);
            }
            else
                loadedData = default(T);

            //isComplete = true;
            container.Dispose();
        }
    }
}
