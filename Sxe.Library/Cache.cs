using System;
using System.Collections.Generic;
using System.Text;

namespace Sxe.Library
{
    public class CacheException : Exception
    {
        public CacheException(string exception)
            : base(exception)
        {
        }
    }


    /// <summary>
    /// A generic cache implementation
    /// </summary>
    public class Cache<K, T>
    {

        public delegate T LoadCallbackDelegate(K key);
        public delegate void UnloadCallbackDelegate(K key, T tag);
        private delegate void UnloadAsyncDelegate(K key, T tag, UnloadCallbackDelegate unloadDelegate, IAsyncResult loadResult);

        /// <summary>
        /// A class that encapsulates a cache entry
        /// </summary>
        class CacheEntry<K, T>
        {
            public K key;
            public bool referenced;
            public T tag;
            public IAsyncResult loadResult;
        }

        List<CacheEntry<K,T>> cacheEntries;
        List<IAsyncResult> unloadResults;
        List<IAsyncResult> removeResults;
        Queue<int> freeIndices;
        Queue<int> usedIndices;
        Dictionary<K, int> keyToIndex;
        LoadCallbackDelegate loadCallback;
        UnloadCallbackDelegate unloadCallback;
        bool asyncLoad;
        bool asyncUnload;
        private UnloadAsyncDelegate unloadAsync;

        public LoadCallbackDelegate LoadCallback
        {
            get { return loadCallback; }
        }

        public UnloadCallbackDelegate UnloadCallback
        {
            get { return unloadCallback; }
        }


        public Cache(int numberOfEntries, LoadCallbackDelegate loadDelegate, UnloadCallbackDelegate unloadDelegate, bool isAsyncLoad, bool isAsyncUnload)
        {
            this.cacheEntries = new List<CacheEntry<K, T>>(numberOfEntries);
            this.freeIndices = new Queue<int>(numberOfEntries);
            this.usedIndices = new Queue<int>(numberOfEntries);
            this.keyToIndex = new Dictionary<K, int>(numberOfEntries);
            this.asyncLoad = isAsyncLoad;
            this.asyncUnload = isAsyncUnload;
            this.loadCallback = loadDelegate;
            this.unloadCallback = unloadDelegate;
            this.unloadResults = new List<IAsyncResult>();
            this.removeResults = new List<IAsyncResult>();
            this.unloadAsync = this.UnloadAsync;

            //Create all the cache entries
            for (int i = 0; i < numberOfEntries; i++)
                this.cacheEntries.Add(new CacheEntry<K, T>());

            //Create all the free indicies
            for (int i = 0; i < numberOfEntries; i++)
                this.freeIndices.Enqueue(i);
           
        }

        public T RequestItem(K key)
        {
            if (loadCallback == null)
                throw new CacheException("Cache Error: No load callback defined.");

            //Does this key already exist in the cache?
            if (keyToIndex.ContainsKey(key))
            {
                //It does! Let's look up the index, and update the associated cache data
                int index = keyToIndex[key];
                CacheEntry<K, T> foundEntry = cacheEntries[index];
                foundEntry.referenced = true;

                if(foundEntry.tag != null)
                    return foundEntry.tag;

                if (foundEntry.loadResult != null && foundEntry.loadResult.IsCompleted)
                {
                    foundEntry.tag = loadCallback.EndInvoke(foundEntry.loadResult);
                    foundEntry.loadResult = null;
                    return foundEntry.tag;
                }

                return default(T);

            }
            //It doesn't, so lets try and get a free index

            int newIndex = GetFreeIndex();
            CacheEntry<K, T> entry = cacheEntries[newIndex];
            entry.tag = default(T);
            entry.loadResult = null;
            entry.key = key;
            entry.referenced = true;

            keyToIndex.Add(key, newIndex);
            

            //We'll asynchronously load the value, but return the default for now

            if (!asyncLoad)
            {
                entry.tag = loadCallback(key);
                return entry.tag;
            }
            else
            {
                //Load hasn't started, so lets started it
                entry.loadResult = this.loadCallback.BeginInvoke(key, null, null);
                return default(T);
            }
            //entry.loadResult = cacheEntries[newIndex].loadResult = loadCallback.BeginInvoke(key, null, null);
            //return default(T);

            ////Check the IAsyncResult to see if the tag has been loaded
            //if (cacheEntries[newIndex].tag == null && cacheEntries[newIndex].loadResult == null)
            //{
            //    //Start the load
                
            //    return default(T);
            //}
            //else if (entry.tag == null && entry.loadResult != null)
            //{
            //    //Is it finished yet?
            //    IAsyncResult result = entry.loadResult;
            //    if (result.IsCompleted)
            //    {
            //        entry.tag = 
            //        return entry.tag;
            //    }
            //    //Not finished, so we'll return null
            //    else
            //        return default(T);
            //}
            //    //we do have  a tag... so return it
            //else
            //    return entry.tag;

            ////T tag = loadCallback(key);
            //cacheEntries[newIndex].tag = tag;
            
            ////keyToIndex[key] = newIndex;

            //return tag;

        }

        int GetFreeIndex()
        {
            int index = -1;
            //Do we have any free indices around?
            if (freeIndices.Count <= 0)
            {
                //We don't... so let's evict
                Evict();
                
            }

            index = freeIndices.Dequeue();
            usedIndices.Enqueue(index);


            return index;
        }

        /// <summary>
        /// Use the clock algorithm to evict a node
        /// </summary>
        void Evict()
        {
            //make two passes
            for (int j = 0; j < 2; j++)
            {
                //Loop through the queue, annihilating stuff if they haven't been referenced
                for (int i = 0; i < usedIndices.Count; i++)
                {
                    int index = usedIndices.Dequeue();
                    if (cacheEntries[index].referenced == false)
                    {
                        RemoveIndex(index);
                        return;
                    }
                    else
                    {
                        cacheEntries[index].referenced = false;
                        usedIndices.Enqueue(index);
                    }
                }
            }

            throw new Exception("Error: Could not evict!");

        }

        /// <summary>
        /// Handles the cleanup/logic of removing an index
        /// </summary>
        /// <param name="index"></param>
        void RemoveIndex(int index)
        {
            //Go through and prune the async results that are completed
            removeResults.Clear();
            foreach (IAsyncResult checkResult in unloadResults)
            {
                if (checkResult.IsCompleted)
                {
                    this.unloadAsync.EndInvoke(checkResult);
                    removeResults.Add(checkResult);
                }
            }
            foreach (IAsyncResult deleteResult in removeResults)
                unloadResults.Remove(deleteResult);


            K key = cacheEntries[index].key;
            T tag = cacheEntries[index].tag;
            IAsyncResult result = cacheEntries[index].loadResult;
            cacheEntries[index].loadResult = null;
            freeIndices.Enqueue(index);
            keyToIndex.Remove(cacheEntries[index].key);

            if (unloadCallback != null)
            {
                if (!asyncUnload)
                {
                    this.unloadAsync(key, tag, this.unloadCallback, result);
                    //unloadCallback(key, tag);
                }
                else
                {
                    IAsyncResult unloadResult = this.unloadAsync.BeginInvoke(key, tag, this.unloadCallback, result, null, null);
                    this.unloadResults.Add(unloadResult);
                }
            }



        }

        private void UnloadAsync(K key, T tag, UnloadCallbackDelegate unloadDelegate, IAsyncResult loadResult)
        {
            //First, was this thing loaded completely?
            //If not, we have to spin while we wait for it to finish
            if (loadResult != null)
            {
                while (!loadResult.IsCompleted)
                    System.Threading.Thread.Sleep(1);

                tag = this.loadCallback.EndInvoke(loadResult);
            }

            this.unloadCallback(key, tag);


        }


    }
}
