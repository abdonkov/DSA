using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DSA.DataStructures.Queues
{
    /// <summary>
    /// Represents an array-based queue structure.
    /// </summary>
    /// <typeparam name="T">The stored data type.</typeparam>
    public class ArrayQueue<T> : IEnumerable<T>
    {
        /// <summary>
        /// The backing array of the queue. Used as a circular buffer.
        /// </summary>
        internal T[] array;

        /// <summary>
        /// The array index of the first element in the queue.
        /// </summary>
        internal int startIndex;

        /// <summary>
        /// The array index of the last element in the queue.
        /// </summary>
        internal int endIndex;

        /// <summary>
        /// Gets the current capacity of the backing array.
        /// </summary>
        internal int Capacity { get; set; }

        /// <summary>
        /// Gets the number of elements in the <see cref="ArrayQueue{T}"/>.
        /// </summary>
        public int Count { get; internal set; }

        /// <summary>
        /// Creates a new instance of the <see cref="ArrayQueue{T}"/> class that is empty and has default capacity.
        /// </summary>
        public ArrayQueue() : this(capacity: 4) { }

        /// <summary>
        /// Creates a new instance of the <see cref="ArrayQueue{T}"/> class that is empty and has the given capacity.
        /// </summary>
        /// <param name="capacity">The capacity of the backing array.</param>
        public ArrayQueue(int capacity)
        {
            if (capacity < 0) throw new ArgumentOutOfRangeException(nameof(capacity));

            Capacity = capacity;
            array = new T[capacity];
            Count = 0;
            startIndex = -1;
            endIndex = -1;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="ArrayQueue{T}"/> class that contains the elements from the specified collection.
        /// </summary>
        /// <param name="collection">The collection to copy elements from.</param>
        public ArrayQueue(IEnumerable<T> collection)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));

            int colSize = collection.Count();

            Capacity = 0;
            Count = 0;

            Resize(increase: true, minCapacity: colSize);

            int i = 0;
            foreach (var item in collection)
            {
                array[i++] = item;
            }

            startIndex = 0;
            endIndex = i - 1;
            Count = i;            
        }

        /// <summary>
        /// Resize the backing array to a corresponding size.
        /// </summary>
        /// <param name="increase">if true increses the size. if false decreases it.</param>
        /// <param name="minCapacity">if set determines the minimum needed capacity after resize, else auto change of capacity is performed.</param>
        private void Resize(bool increase, int minCapacity = 0)
        {
            // save the old capacity
            int oldCapacity = Capacity;

            if (minCapacity == 0)// auto increase/decrease
            {
                if (increase)
                {
                    if (Capacity == 0) Capacity = 1;
                    else Capacity = Capacity << 1;
                }
                else Capacity = Capacity >> 1;
            }
            else// if minCapacity is set
            {
                if (increase)
                {
                    while (Capacity < minCapacity)
                    {
                        if (Capacity == 0) Capacity = 1;
                        else Capacity = Capacity << 1;
                    }
                }
                else
                {
                    while (minCapacity < Capacity) Capacity = Capacity >> 1;
                    Capacity = Capacity << 1;
                }
            }

            // Items are copied in the new array with the first item of the queue
            // being on the first array index (array[0])
            int bufferIndex = startIndex;
            T[] newArray = new T[Capacity];
            for (int i = 0; i < Count; i++)
            {
                newArray[i] = array[bufferIndex++];
                if (bufferIndex == oldCapacity) bufferIndex = 0;
            }
            array = newArray;
            if (Count == 0) startIndex = -1;
            else startIndex = 0;

            endIndex = Count - 1;
        }

        /// <summary>
        /// Returns the item at the beginning of the <see cref="ArrayQueue{T}"/> without removing it.
        /// </summary>
        /// <returns>The item at the beginning of the <see cref="ArrayQueue{T}"/>.</returns>
        public T Peek()
        {
            if (Count == 0)
                throw new InvalidOperationException("Stack is empty!");

            return array[startIndex];
        }

        /// <summary>
        /// Removes and returns the item at the beginning of the <see cref="ArrayQueue{T}"/>.
        /// </summary>
        /// <returns>The item removed from the beginning of the <see cref="ArrayQueue{T}"/>.</returns>
        public T Dequeue()
        {
            if (Count == 0)
                throw new InvalidOperationException("Stack is empty!");

            Count--;
            T item = array[startIndex++];
            if (startIndex == Capacity) startIndex = 0;

            if (Count * 3 <= Capacity)
                Resize(increase: false);

            if (Count == 0)
            {
                startIndex = -1;
                endIndex = -1;
            }

            return item;
        }

        /// <summary>
        /// Adds an item at the end of the <see cref="ArrayQueue{T}"/>.
        /// </summary>
        /// <param name="item">The item to add at the end of the <see cref="ArrayQueue{T}"/>. The value can be null for reference types.</param>
        public void Enqueue(T item)
        {
            if (Count == Capacity)
                Resize(increase: true);

            Count++;
            endIndex++;
            if (endIndex == Capacity) endIndex = 0;

            if (endIndex == startIndex)// shoud never happen
                throw new Exception("Circular buffer overflow! Resizing was not performed correctly!");

            array[endIndex] = item;
            if (startIndex == -1) startIndex = 0;
        }

        /// <summary>
        /// Determines whether an item is in the <see cref="ArrayQueue{T}"/>.
        /// </summary>
        /// <param name="item">The item to search in the <see cref="ArrayQueue{T}"/>.</param>
        /// <returns>returns true if the item is found; otherwise false.</returns>
        public bool Contains(T item)
        {
            int bufferIndex = startIndex;
            for (int i = 0; i < Count; i++)
            {
                if (object.Equals(array[bufferIndex++], item)) return true;
                if (bufferIndex == Capacity) bufferIndex = 0;
            }
            return false;
        }

        /// <summary>
        /// Removes all elements from the <see cref="ArrayQueue{T}"/>.
        /// </summary>
        public void Clear()
        {
            Capacity = 4;
            array = new T[Capacity];
            Count = 0;
            startIndex = -1;
            endIndex = -1;
        }

        /// <summary>
        /// Copies the elements of the <see cref="ArrayQueue{T}"/> to a new array in first-in-first-out (FIFO) order.
        /// </summary>
        /// <returns>An array containing copies of the elements of the <see cref="ArrayQueue{T}"/>.</returns>
        public T[] ToArray()
        {
            int bufferIndex = startIndex;
            T[] newArray = new T[Count];
            for (int i = 0; i < Count; i++)
            {
                newArray[i] = array[bufferIndex++];
                if (bufferIndex == Capacity) bufferIndex = 0;
            }
            return newArray;
        }

        /// <summary>
        /// Returns an enumerator that iterates throught the <see cref="ArrayQueue{T}"/>.
        /// </summary>
        /// <returns>Enumerator for the <see cref="ArrayQueue{T}"/>.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            int bufferIndex = startIndex;
            for (int i = 0; i < Count; i++)
            {
                yield return array[bufferIndex++];
                if (bufferIndex == Capacity) bufferIndex = 0;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
