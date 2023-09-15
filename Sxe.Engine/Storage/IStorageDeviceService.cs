using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Storage;

namespace Sxe.Engine.Storage
{
    public interface IStorageDeviceService
    {
        StorageDeviceMode StorageDeviceMode { get; set; }
        StorageDevice StorageDevice { get; }
        void Save<T>(string fileName, T saveObject, SaveDelegate<T> saveFunc) where T : ICloneable;
        LoadResult<T> Load<T>(string fileName, LoadDelegate<T> loadFunc);
    }
}
