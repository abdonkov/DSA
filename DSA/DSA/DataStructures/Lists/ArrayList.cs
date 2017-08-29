using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DSA.DataStructures.Lists
{
    /// <summary>
    /// Represents an array-based list structure.
    /// </summary>
    /// <typeparam name="T">The stored data type.</typeparam>
    public class ArrayList<T> : IList<T>
    {
        /// <summary>
        /// The backing array of the list.
        /// </summary>
        internal T[] array;

        /// <summary>
        /// Gets the current capacity of the backing array.
        /// </summary>
        public int Capacity { get; internal set; }

        /// <summary>
        /// Gets the number of elements in the <see cref="ArrayList{T}"/>.
        /// </summary>
        public int Count { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether the <see cref="ArrayList{T}"/> is read-only.
        /// </summary>
        public bool IsReadOnly { get { return false; } }

        /// <summary>
        /// The element at the given index in the <see cref="ArrayList{T}"/>.
        /// </summary>
        /// <param name="index">The index of the element.</param>
        /// <returns>The element on the given index.</returns>
        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= Count) throw new IndexOutOfRangeException(nameof(index));
                return array[index];
            }
            set
            {
                if (index < 0 || index >= Count) throw new IndexOutOfRangeException(nameof(index));
                array[index] = value;
            }
        }

        /// <summary>
        /// Creates a new instance of the <see cref="ArrayList{T}"/> class that is empty and has default capacity.
        /// </summary>
        public ArrayList() : this(capacity: 4) { }

        /// <summary>
        /// Creates a new instance of the <see cref="ArrayList{T}"/> class that is empty and has the given capacity.
        /// </summary>
        /// <param name="capacity">The capacity of the backing array.</param>
        public ArrayList(int capacity)
        {
            if (capacity < 0) throw new ArgumentOutOfRangeException(nameof(capacity));

            Capacity = capacity;
            array = new T[Capacity];
            Count = 0;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="ArrayList{T}"/> class that contains the elements from the specified collection.
        /// </summary>
        /// <param name="collection">The collection whose elements are copied to the new <see cref="ArrayList{T}"/>.</param>
        public ArrayList(IEnumerable<T> collection)
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
        /// Adds an item to the end of the <see cref="ArrayList{T}"/>.
        /// </summary>
        /// <param name="item">The item to add at the end of the <see cref="ArrayList{T}"/>.</param>
        public void Add(T item)
        {
            if (Count == Capacity) Resize(increase: true);
            array[Count++] = item;
        }

        /// <summary>
        /// Adds the elements of the specific collection to the end of the <see cref="ArrayList{T}"/>.
        /// </summary>
        /// <param name="collection">The collection whose elements should be added to end of the <see cref="ArrayList{T}"/>.</param>
        public void AddRange(IEnumerable<T> collection)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));

            int colSize = collection.Count();

            if (Count + colSize > Capacity)
                Resize(increase: true, minCapacity: Count + colSize);

            foreach (var item in collection)
            {
                array[Count++] = item;
            }
        }

        /// <summary>
        /// Inserts an item into the <see cref="ArrayList{T}"/> at the specific index.
        /// </summary>
        /// <param name="index">The index at which the item is inserted in the <see cref="ArrayList{T}"/>.</param>
        /// <param name="item">The item to insert in the <see cref="ArrayList{T}"/>.</param>
        public void Insert(int index, T item)
        {
            if (index < 0 || index > Count) throw new IndexOutOfRangeException(nameof(index));
            if (index == Count)
            {
                Add(item);
                return;
            }
            if (Count == Capacity) Resize(increase: true);

            for (int i = Count - 1; i >= 0; i--)
            {
                array[i + 1] = array[i];
                if (index == i)
                {
                    array[i] = item;
                    break;
                }
            }
            Count++;
        }

        /// <summary>
        /// Inserts the elements of the collection into the <see cref="ArrayList{T}"/> at the specific index.
        /// </summary>
        /// <param name="index">The index at which the item is inserted in the <see cref="ArrayList{T}"/>.</param>
        /// <param name="collection">The collection whose elements should be inserted in the <see cref="ArrayList{T}"/>at the specified index.</param>
        public void InsertRange(int index, IEnumerable<T> collection)
        {
            if (index < 0 || index >= Count) throw new IndexOutOfRangeException(nameof(index));
            if (collection == null) throw new ArgumentNullException(nameof(collection));

            int colSize = collection.Count();

            if (Count + colSize > Capacity)
                Resize(increase: true, minCapacity: Count + colSize);

            for (int i = Count - 1; i >= index; i--)
            {
                array[i + colSize] = array[i];
            }

            foreach (var item in collection)
            {
                array[index++] = item;
            }

            Count += colSize;
        }

        /// <summary>
        /// Removes the first occurrence of the item from the <see cref="ArrayList{T}"/>.
        /// </summary>
        /// <param name="item">The item to remove from the <see cref="ArrayList{T}"/>.</param>
        /// <returns>true if the item is removed successfully; otherwise false.</returns>
        public bool Remove(T item)
        {
            int index = IndexOf(item);
            if (index == -1) return false;
            RemoveAt(index);
            return true;
        }

        /// <summary>
        /// Removes a range of elements from the <see cref="ArrayList{T}"/>.
        /// </summary>
        /// <param name="index">The starting index of the range of elements to remove from the <see cref="ArrayList{T}"/>.</param>
        /// <param name="count">The number of elements to remove.</param>
        public void RemoveRange(int index, int count)
        {
            if (index < 0 || index >= Count) throw new IndexOutOfRangeException(nameof(index));
            if (index + count > Count || count < 1) throw new ArgumentOutOfRangeException("Invalid length specified.");

            while (index + count < Count)
            {
                array[index] = array[index++ + count];
            }

            Count -= count;

            if (Count <= Capacity / 3)
                Resize(increase: false, minCapacity: Count * 3 / 2);
        }

        /// <summary>
        /// Removes the item at the specific index of the <see cref="ArrayList{T}"/>.
        /// </summary>
        /// <param name="index">The index of the item to remove from the <see cref="ArrayList{T}"/>.</param>
        public void RemoveAt(int index)
        {
            if (index < 0 || index >= Count) throw new IndexOutOfRangeException(nameof(index));

            for (int i = index; i < Count - 1; i++)
            {
                array[i] = array[i + 1];
            }

            Count--;
            if (Count == Capacity / 3) Resize(increase: false);
        }

        /// <summary>
        /// Searches for the item and returns the index of the first occurrence within the entire <see cref="ArrayList{T}"/>.
        /// </summary>
        /// <param name="item">The item to search for the first occurrence in the <see cref="ArrayList{T}"/>.</param>
        /// <returns>The index of the first occurrence of the item if found; otherwise -1.</returns>
        public int IndexOf(T item)
        {
            for (int i = 0; i < Count; i++)
            {
                if (object.Equals(array[i], item)) return i;
            }
            return -1;
        }

        /// <summary>
        /// Searches for the item and returns the index of the last occurrence within the entire <see cref="ArrayList{T}"/>.
        /// </summary>
        /// <param name="item">The item to search for the last occurrence in the <see cref="ArrayList{T}"/>.</param>
        /// <returns>The index of the last occurrence of the item if found; otherwise -1.</returns>
        public int LastIndexOf(T item)
        {
            for (int i = Count - 1; i >= 0; i--)
            {
                if (object.Equals(array[i], item)) return i;
            }
            return -1;
        }

        /// <summary>
        /// Determines whether an item is in the <see cref="ArrayList{T}"/>.
        /// </summary>
        /// <param name="item">The item to search in the <see cref="ArrayList{T}"/>.</param>
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
        /// Removes all elements from the <see cref="ArrayList{T}"/>.
        /// </summary>
        public void Clear()
        {
            Capacity = 4;
            array = new T[Capacity];
            Count = 0;
        }

        /// <summary>
        /// Copies the elements of the <see cref="ArrayList{T}"/> to a new array.
        /// </summary>
        /// <returns>An array containing copies of the elements of the <see cref="ArrayList{T}"/>.</returns>
        public T[] ToArray()
        {
            T[] newArray = new T[Count];
            for (int i = 0; i < Count; i++)
            {
                newArray[i] = array[i];
            }
            return newArray;
        }

        /// <summary>
        /// Copies the entire <see cref="ArrayList{T}"/> to compatible one-dimensional array, starting at the given index of the target array.
        /// </summary>
        /// <param name="array">The one-dimensional <see cref="Array"/> that is the destination of the elements copied from the <see cref="ArrayList{T}"/>. The <see cref="Array"/> must have zero-based indexing.</param>
        /// <param name="arrayIndex">The zero-based index in the array at which the copying begin.</param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            Array.Copy(this.array, 0, array, arrayIndex, Count);
        }

        /// <summary>
        /// Returns an enumerator that iterates throught the <see cref="ArrayList{T}"/>.
        /// </summary>
        /// <returns>Enumerator for the <see cref="ArrayList{T}"/>.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < Count; i++)
            {
                yield return array[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
