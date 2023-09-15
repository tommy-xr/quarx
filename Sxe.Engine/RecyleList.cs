using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Sxe.Engine
{
    /// <summary>
    /// Class used to implement lists of objects that need to be reused
    /// </summary>
    /// <typeparam name="T">Type of class to create list for - must have parameterless constructor</typeparam>
    public class RecycleList<T> : IRecycleList<T> where T : IRecycleable
    {
        T[] objectList;
        int numObjects;
        int maxObjects;


        /// <summary>
        /// The number of objects that are currently allocated
        /// </summary>
        public int Count { get { return numObjects; } }

        /// <summary>
        /// The total number of objects that can be allocated at once
        /// </summary>
        public int Capacity { get { return maxObjects; } }

        public RecycleList(int num, params object [] constructorArguments)
        {
            maxObjects = num;
            objectList = new T[maxObjects];
            numObjects = 0;

            //Here we get the types for all the arguments
            Type[] types = new Type[constructorArguments.Length];
            for (int i = 0; i < types.Length; i++)
                types[i] = constructorArguments[i].GetType();

            //Get the constructor for this object using reflection
            //This is a possible performance bottle neck, so this should all be done in a pre-process
            ConstructorInfo ci = (ConstructorInfo)typeof(T).GetConstructor(types);

            //Allocate space for each object
            for (int i = 0; i < maxObjects; i++)
            {
                objectList[i] = (T)ci.Invoke(constructorArguments);
                IRecycleable rec = (IRecycleable)objectList[i];
                rec.RecycleIndex = i;
            }
        }

        public T GetObjectByIndex(int index)
        {
            if (index < 0)
                throw new ArgumentOutOfRangeException("index", "Index must be non-negative.");

            if (index >= numObjects)
                throw new ArgumentOutOfRangeException("index", "Index exceeds the number of objects allocated.");

            return objectList[index];
        }

        public T GetFreeItem()
        {
            if (numObjects >= maxObjects)
            {
                return default(T);
            }

            T retObject = objectList[numObjects];

            numObjects++;
            return retObject;
        }


        public void Recycle(int index)
        {
            if (index < 0)
                throw new ArgumentOutOfRangeException("index", "Index must be non-negative");

            if (index >= numObjects)
                throw new ArgumentOutOfRangeException("index", "Index must not exceed or equal the current allocation.");

            //To destroy an object, we swap the end object with whatever object is being destroyed
            int endObjectIndex = numObjects - 1;

            //Make sure the index and end object aren't the same!
            if (endObjectIndex != index)
            {
                swap(index, endObjectIndex);
            }

            numObjects--;
        }

        void swap(int index1, int index2)
        {
            T object1 = objectList[index1];
            T object2 = objectList[index2];

            objectList[index1] = object2;
            ((IRecycleable)object2).RecycleIndex = index1;

            objectList[index2] = object1;
            ((IRecycleable)object1).RecycleIndex = index2;
        }
    }
}
