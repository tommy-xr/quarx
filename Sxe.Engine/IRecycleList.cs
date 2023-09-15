using System;
using System.Collections.Generic;
using System.Text;

namespace Sxe.Engine
{
    public interface IRecycleList<T>
    {
        int Count { get; }
        int Capacity { get; }

        T GetFreeItem();
        T GetObjectByIndex(int index);
        void Recycle(int index);
    }
}
