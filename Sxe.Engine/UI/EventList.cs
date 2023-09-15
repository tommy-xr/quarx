using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Sxe.Engine.UI
{
    /// <summary>
    /// Just like a list, but has events for when data changes!
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EventList<T> : IList<T>, ICollection<T>, IEnumerable<T>
    {
        List<T> list;

        public event EventHandler ListModified;

        public EventList()
        {
            list = new List<T>();
        }

        public T this[int index] 
        {
            get { return list[index]; }
            set { list[index] = value; FireListModified(); }
        }

        public int IndexOf(T item)
        {
            return list.IndexOf(item);
        }
        public void Insert(int index, T item)
        {
            list.Insert(index, item);
            FireListModified();
        }

        public void RemoveAt(int index)
        {
            list.RemoveAt(index);
            FireListModified();
        }
        public int Count { get { return list.Count; } }
        public bool IsReadOnly { get { return false; } }

        public void Add(T item)
        {
            list.Add(item);
            FireListModified();
        }
        public void Clear()
        {
            list.Clear();
            FireListModified();
        }
        public bool Contains(T item)
        {
            return list.Contains(item);
        }
        public void CopyTo(T[] array, int arrayIndex)
        {
            list.CopyTo(array, arrayIndex);
        }
        public bool Remove(T item)
        {
            FireListModified();
            return list.Remove(item);
            
        }

        public IEnumerator<T> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();  // Calls IEnumerator<T> GetEnumerator()
        }


        void FireListModified()
        {
            if (ListModified != null)
                ListModified(this, null);
        }
    }
}
