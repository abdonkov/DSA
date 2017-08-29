using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DSA.DataStructures.Stacks
{
    /// <summary>
    /// Represents an array-based stack structure.
    /// </summary>
    /// <typeparam name="T">The stored data type.</typeparam>
    public class ArrayStack<T> : IEnumerable<T>
    {
        /// <summary>
        /// The backing array of the stack.
        /// </summary>
        internal T[] array;

        /// <summary>
        /// Gets the current capacity of the backing array.
        /// </summary>
        internal int Capacity { get; set; }

        /// <summary>
        /// Gets the number of elements in the <see cref="ArrayStack{T}"/>.
        /// </summary>
        public int Count { get; internal set; }

        /// <summary>
        /// Creates a new instance of the <see cref="ArrayStack{T}"/> class that is empty and has default capacity.
        /// </summary>
        public ArrayStack() : this(capacity: 4) { }

        /// <summary>
        /// Creates a new instance of the <see cref="ArrayStack{T}"/> class that is empty and has the given capacity.
        /// </summary>
        /// <param name="capacity">The capacity of the backing array.</param>
        public ArrayStack(int capacity)
        {
            if (capacity < 0) throw new ArgumentOutOfRangeException(nameof(capacity));

            Capacity = capacity;
            array = new T[capacity];
        }

        /// <summary>
        /// Creates a new instance of the <see cref="ArrayStack{T}"/> class that contains the elements from the specified collection.
        /// </summary>
        /// <param name="collection">The collection to copy elements from.</param>
        public ArrayStack(IEnumerable<T> collection)
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
            Count = i;
        }

        /// <summary>
        /// Resize the backing array to a corresponding size.
        /// </summary>
        /// <param name="increase">if true increses the size. if false decreases it.</param>
        /// <param name="minCapacity">if set determines the minimum needed capacity after resize, else auto change of capacity is performed.</param>
        private void Resize(bool increase, int minCapacity = 0)
        {
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

            T[] newArray = new T[Capacity];
            for (int i = 0; i < Count; i++)
            {
                newArray[i] = array[i];
            }
            array = newArray;
        }

        /// <summary>
        /// Returns the item at the top of the <see cref="ArrayStack{T}"/> without removing it.
        /// </summary>
        /// <returns>The item at the top of the <see cref="ArrayStack{T}"/>.</returns>
        public T Peek()
        {
            if (Count == 0)
                throw new InvalidOperationException("Stack is empty!");

            return array[Count - 1];
        }

        /// <summary>
        /// Removes and returns the item at the top of the <see cref="ArrayStack{T}"/>.
        /// </summary>
        /// <returns>The item removed from the top of the <see cref="ArrayStack{T}"/>.</returns>
        public T Pop()
        {
            if (Count == 0)
                throw new InvalidOperationException("Stack is empty!");

            T item = array[--Count];

            if (Count * 3 <= Capacity)
                Resize(increase: false);

            return item;
        }

        /// <summary>
        /// Inserts an item at the top of the <see cref="ArrayStack{T}"/>.
        /// </summary>
        /// <param name="item">The item to push onto the <see cref="ArrayStack{T}"/>. The value can be null for reference types.</param>
        public void Push(T item)
        {
            if (Count == Capacity)
                Resize(increase: true);

            array[Count++] = item;
        }

        /// <summary>
        /// Determines whether an item is in the <see cref="ArrayStack{T}"/>.
        /// </summary>
        /// <param name="item">The item to search in the <see cref="ArrayStack{T}"/>.</param>
        /// <returns>returns true if the item is found; otherwise false.</returns>
        public bool Contains(T item)
        {
            for (int i = 0; i < Count; i++)
            {
                if (object.Equals(array[i], item)) return true;
            }
            return false;
        }

        /// <summary>
        /// Removes all elements from the <see cref="ArrayStack{T}"/>.
        /// </summary>
        public void Clear()
        {
            Capacity = 4;
            array = new T[Capacity];
            Count = 0;
        }

        /// <summary>
        /// Copies the elements of the <see cref="ArrayStack{T}"/> to a new array in last-in-first-out (LIFO) order.
        /// </summary>
        /// <returns>An array containing copies of the elements of the <see cref="ArrayStack{T}"/>.</returns>
        public T[] ToArray()
        {
            T[] newArray = new T[Count];
            for (int i = 0; i < Count; i++)
            {
                newArray[i] = array[Count - 1 - i];
            }
            return newArray;
        }

        /// <summary>
        /// Returns an enumerator that iterates throught the <see cref="ArrayStack{T}"/>.
        /// </summary>
        /// <returns>Enumerator for the <see cref="ArrayStack{T}"/>.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            for (int i = Count - 1; i >= 0; i--)
            {
                yield return array[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
