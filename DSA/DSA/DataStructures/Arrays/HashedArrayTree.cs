using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DSA.DataStructures.Arrays
{
    /// <summary>
    /// Represents a hashed array tree.
    /// </summary>
    /// <typeparam name="T">The stored data type.</typeparam>
    public class HashedArrayTree<T> : IEnumerable<T>
    {
        /// <summary>
        /// The base 2 logarithm of the top array size. Used for bitwise indexing calculations.
        /// </summary>
        private int topArrayLog2Size;

        /// <summary>
        /// The backing arrays of the <see cref="HashedArrayTree{T}"/>.
        /// </summary>
        internal T[][] arrays; //using jagged array instead of multidimensional array for better performance

        /// <summary>
        /// Gets the current capacity of the backing arrays.
        /// </summary>
        public int Capacity { get; internal set; }

        /// <summary>
        /// Gets the number of elements in the <see cref="HashedArrayTree{T}"/>.
        /// </summary>
        public int Count { get; internal set; }

        /// <summary>
        /// The element at the given index in the <see cref="HashedArrayTree{T}"/>.
        /// </summary>
        /// <param name="index">The index of the element.</param>
        /// <returns>The element on the given index.</returns>
        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= Count) throw new IndexOutOfRangeException(nameof(index));
                var topArrayIndex = GetTopArrayIndex(index);
                var subarrayIndex = GetSubarrayIndex(index);
                return arrays[topArrayIndex][subarrayIndex];
            }
            set
            {
                if (index < 0 || index >= Count) throw new IndexOutOfRangeException(nameof(index));
                var topArrayIndex = GetTopArrayIndex(index);
                var subarrayIndex = GetSubarrayIndex(index);
                arrays[topArrayIndex][subarrayIndex] = value;
            }
        }

        /// <summary>
        /// Creates a new instance of the <see cref="HashedArrayTree{T}"/> class that is empty.
        /// </summary>
        public HashedArrayTree()
        {
            arrays = new T[2][];
            Count = 0;
            Capacity = 4;
            topArrayLog2Size = 1;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="HashedArrayTree{T}"/> class that contains the elements from the specified collection.
        /// </summary>
        /// <param name="collection">The collection whose elements are copied to the new <see cref="HashedArrayTree{T}"/>.</param>
        public HashedArrayTree(IEnumerable<T> collection)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));

            arrays = new T[2][];
            Count = 0;
            Capacity = 4;
            topArrayLog2Size = 1;

            var colSize = collection.Count();

            Resize(increase: true, minCapacity: colSize);

            foreach (var item in collection)
            {
                Add(item);
            }
        }

        /// <summary>
        /// Resize the backing arrays to a corresponding size.
        /// </summary>
        /// <param name="increase">if true increses the size. if false decreases it.</param>
        /// <param name="minCapacity">if set determines the minimum needed capacity after resize, else auto change of capacity is performed.</param>
        private void Resize(bool increase, int minCapacity = 0)
        {
            if (increase)
            {
                int newTopArraySize = arrays.Length;
                int newTopArrayLog2Size = topArrayLog2Size;

                do// Calculate new top array size
                {
                    // Multiply by 2
                    newTopArraySize = newTopArraySize << 1;
                    newTopArrayLog2Size++;
                } while ((newTopArraySize << newTopArrayLog2Size) < minCapacity);// We continue until the minCapacity is reached

                // Copy the old arrays items to a new arrays
                CopyToNewArrays(newTopArraySize: newTopArraySize, newTopArrayLog2Size: newTopArrayLog2Size);
            }
            else// if decreasing the size
            {
                int newTopArraySize = arrays.Length;
                int newTopArrayLog2Size = topArrayLog2Size;

                if (minCapacity != 0)
                {
                    while (minCapacity < (newTopArraySize << newTopArrayLog2Size))
                    {
                        // Divide by 2
                        newTopArraySize = newTopArraySize >> 1;
                        newTopArrayLog2Size--;
                    }
                    // When when we get out of the loop min capacity will be higher that
                    // the new capacity so we need to multiply the new capacity by 2
                    newTopArraySize = newTopArraySize << 1;
                    newTopArrayLog2Size++;
                }
                else
                {
                    newTopArraySize = newTopArraySize >> 1;
                    newTopArrayLog2Size--;
                }

                // Copy the old arrays items to a new arrays
                CopyToNewArrays(newTopArraySize: newTopArraySize, newTopArrayLog2Size: newTopArrayLog2Size);
            }
        }

        /// <summary>
        /// Creating new arrays with the given size and copies the items from the old arrays to the new ones.
        /// </summary>
        /// <param name="newTopArraySize">The size of the new top array.</param>
        /// <param name="newTopArrayLog2Size">The base 2 logarithm of the size of the new top array.</param>
        private void CopyToNewArrays(int newTopArraySize, int newTopArrayLog2Size)
        {
            T[][] newArrays = new T[newTopArraySize][];

            int oldArraysIndex = 0;

            for (int i = 0; i < newArrays.Length; i++)
            {
                if (oldArraysIndex == Count) break;

                newArrays[i] = new T[newTopArraySize];
                for (int j = 0; j < newArrays.Length; j++)
                {
                    newArrays[i][j] = this[oldArraysIndex++];
                    if (oldArraysIndex == Count) break;
                }
            }

            Capacity = newTopArraySize << newTopArrayLog2Size;
            topArrayLog2Size = newTopArrayLog2Size;
            arrays = newArrays;
        }

        /// <summary>
        /// Gets the index in the top array at which the item with the given index can be found.
        /// </summary>
        /// <param name="index">The index of the item.</param>
        /// <returns>The index in the top array where the item with the given index can be found.</returns>
        internal int GetTopArrayIndex(int index)
        {
            // The top array index is the result of the division of the searched item index
            // by the size of the top array. However, because the size of the top array
            // is always a power of two we can make that division by bit shifting N times
            // where 2^N is the size of the array. So we shift log2(arraySize) times.
            return index >> topArrayLog2Size;
        }

        /// <summary>
        /// Gets the index in the subarray at which the item with the given index can be found.
        /// </summary>
        /// <param name="index">The index of the item.</param>
        /// <returns>The index in the subarray where the item with the given index can be found.</returns>
        internal int GetSubarrayIndex(int index)
        {
            // The subarray index is the remainder of the division of the searched item
            // index by the size of the top array. However, because the size of the top array
            // is always a power of two we can calculate the remainder with a neat bitwise
            // trick. Size of top array is power of two so: 0001 0000. If we substract 1
            // we get 0000 1111. If we make bitwise AND with the index we only get flipped
            // bits in the part with power of 2s lower that the size of the top array
            // which is the remainder when dividing by a power of two.
            // Example: Searched item index is 21: 0001 0101
            //        Top array size is 8 - 1 = 7: 0000 0111
            //           Result after bitwise and: 0000 0101 which is 5 and 21 % 8 = 5
            return index & (arrays.Length - 1);
        }

        /// <summary>
        /// Adds an item to the end of the <see cref="HashedArrayTree{T}"/>.
        /// </summary>
        /// <param name="item">The item to add at the end of the <see cref="HashedArrayTree{T}"/>.</param>
        public void Add(T item)
        {
            if (Count == Capacity)
                Resize(increase: true);
            
            var topArrayIndex = GetTopArrayIndex(Count);
            var subarrayIndex = GetSubarrayIndex(Count);

            // If array is not created on this index create one
            if (arrays[topArrayIndex] == null)
            {
                arrays[topArrayIndex] = new T[arrays.Length];
            }

            arrays[topArrayIndex][subarrayIndex] = item;
            Count++;
        }

        /// <summary>
        /// Adds the elements of the specific collection to the end of the <see cref="HashedArrayTree{T}"/>.
        /// </summary>
        /// <param name="collection">The collection whose elements should be added to end of the <see cref="HashedArrayTree{T}"/>.</param>
        public void AddRange(IEnumerable<T> collection)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));

            int colSize = collection.Count();

            if (Count + colSize > Capacity)
                Resize(increase: true, minCapacity: Count + colSize);

            foreach (var item in collection)
            {
                Add(item);
            }
        }

        /// <summary>
        /// Inserts an item into the <see cref="HashedArrayTree{T}"/> at the specific index.
        /// </summary>
        /// <param name="index">The index at which the item is inserted in the <see cref="HashedArrayTree{T}"/>.</param>
        /// <param name="item">The item to insert in the <see cref="HashedArrayTree{T}"/>.</param>
        public void Insert(int index, T item)
        {
            if (index < 0 || index > Count) throw new IndexOutOfRangeException(nameof(index));
            if (index == Count)
            {
                Add(item);
                return;
            }

            // Adding item at the end makes easier handling the cases where resize is needed.
            Add(item);
            // After that we just need to shift all elements after the given index by 1
            // and the last element will override the item we added at the end

            // Storing the item on the previous index. It is the given item
            // the first time bevause we need to insert it on the given position
            var previousItem = item;

            for (int i = index; i < Count; i++)
            {
                var temp = this[i];
                this[i] = previousItem;
                previousItem = temp;
            }            
        }

        /// <summary>
        /// Inserts the elements of the collection into the <see cref="HashedArrayTree{T}"/> at the specific index.
        /// </summary>
        /// <param name="index">The index at which the item is inserted in the <see cref="HashedArrayTree{T}"/>.</param>
        /// <param name="collection">The collection whose elements should be inserted in the <see cref="HashedArrayTree{T}"/>at the specified index.</param>
        public void InsertRange(int index, IEnumerable<T> collection)
        {
            if (index < 0 || index >= Count) throw new IndexOutOfRangeException(nameof(index));
            if (collection == null) throw new ArgumentNullException(nameof(collection));

            int colSize = collection.Count();

            if (Count + colSize > Capacity)
                Resize(increase: true, minCapacity: Count + colSize);

            // Adding dummy elements(easier handling of the case where an array has to be created)
            for (int i = 0; i < colSize; i++)
            {
                Add(default(T));
            }

            // Shifting items to make place for the elements of the collection
            for (int i = Count - colSize - 1; i >= index; i--)
            {
                this[i + colSize] = this[i];
            }

            // Inserting the items from the collection
            foreach (var item in collection)
            {
                this[index++] = item;
            }
        }

        /// <summary>
        /// Removes the first occurrence of the item from the <see cref="HashedArrayTree{T}"/>.
        /// </summary>
        /// <param name="item">The item to remove from the <see cref="HashedArrayTree{T}"/>.</param>
        /// <returns>true if the item is removed successfully; otherwise false. Also returns false if item was not found.</returns>
        public bool Remove(T item)
        {
            int index = IndexOf(item);
            if (index != -1)
                return RemoveAt(index);
            else
                return false;
        }

        /// <summary>
        /// Removes the item at the specific index of the <see cref="HashedArrayTree{T}"/>.
        /// </summary>
        /// <param name="index">The index of the item to remove from the <see cref="HashedArrayTree{T}"/>.</param>
        /// <returns>true if the item is removed successfully; otherwise false.</returns>
        public bool RemoveAt(int index)
        {
            if (index < 0 || index >= Count) throw new IndexOutOfRangeException(nameof(index));

            // We just need to shift all items until the given index by 1 to the left

            // Storing the last item. The first time it is null
            // because we need to remove the item
            T previousItem = default(T);

            // Shifting the items
            for (int i = Count - 1; i >= index; i--)
            {
                var temp = this[i];
                this[i] = previousItem;
                previousItem = temp;
            }

            Count--;

            // If the elements are 1/8 of the capacity we need to resize the backing arrays
            if (Count << 3 == Capacity) // By shifting left 3 times we multiply by 8
                Resize(increase: false);

            // If the number of items is now a multiple of the top array size
            // we can remove the last allocated array
            if (Count % arrays.Length == 0)
                arrays[GetTopArrayIndex(Count)] = null;

            return true;
        }

        /// <summary>
        /// Removes a range of elements from the <see cref="HashedArrayTree{T}"/>.
        /// </summary>
        /// <param name="index">The starting index of the range of elements to remove from the <see cref="HashedArrayTree{T}"/>.</param>
        /// <param name="count">The number of elements to remove.</param>
        public void RemoveRange(int index, int count)
        {
            if (index < 0 || index >= Count) throw new IndexOutOfRangeException(nameof(index));
            if (index + count > Count || count < 1) throw new ArgumentException("Invalid length specified.");

            // Shifting all items until the end
            while (index + count < Count)
            {
                this[index] = this[index++ + count];
            }

            Count -= count;

            // If Count is 1/8 or lower of the capacity decreasing of the arrays is needed
            if (Count << 3 <= Capacity)
            {
                if (Count > 1)
                    Resize(increase: false, minCapacity: Count << 2);
                else
                    Resize(increase: false, minCapacity: 4);
            }

            var lastItemTopArrayIndex = GetTopArrayIndex(Count - 1);

            // All array allocations after the array containing the last item can be deleted
            for (int i = lastItemTopArrayIndex + 1; i < arrays.Length; i++)
            {
                arrays[i] = null;
            }
        }

        /// <summary>
        /// Searches for the item and returns the index of the first occurrence within the entire <see cref="HashedArrayTree{T}"/>.
        /// </summary>
        /// <param name="item">The item to search for the first occurrence in the <see cref="HashedArrayTree{T}"/>.</param>
        /// <returns>The index of the first occurrence of the item if found; otherwise -1.</returns>
        public int IndexOf(T item)
        {
            for (int i = 0; i < Count; i++)
            {
                if (object.Equals(this[i], item))
                {
                    // Returns the index of the item
                    return i;
                }
            }
            // If nothing is found returns -1
            return -1;
        }

        /// <summary>
        /// Searches for the item and returns the index of the last occurrence within the entire <see cref="HashedArrayTree{T}"/>.
        /// </summary>
        /// <param name="item">The item to search for the last occurrence in the <see cref="HashedArrayTree{T}"/>.</param>
        /// <returns>The index of the last occurrence of the item if found; otherwise -1.</returns>
        public int LastIndexOf(T item)
        {
            for (int i = Count - 1; i >= 0; i--)
            {

                if (object.Equals(this[i], item))
                {
                    // Returns the index of the item
                    return i;
                }
            }
            // If nothing is found returns -1
            return -1;
        }

        /// <summary>
        /// Determines whether an item is in the <see cref="HashedArrayTree{T}"/>.
        /// </summary>
        /// <param name="item">The item to search in the <see cref="HashedArrayTree{T}"/>.</param>
        /// <returns>returns true if the item is found; otherwise false.</returns>
        public bool Contains(T item)
        {
            return IndexOf(item) != -1;
        }

        /// <summary>
        /// Removes all elements from the <see cref="HashedArrayTree{T}"/>.
        /// </summary>
        public void Clear()
        {
            arrays = new T[2][];
            Count = 0;
            Capacity = 4;
            topArrayLog2Size = 1;
        }

        /// <summary>
        /// Copies the elements of the <see cref="HashedArrayTree{T}"/> to a new array.
        /// </summary>
        /// <returns>An array containing copies of the elements of the <see cref="HashedArrayTree{T}"/>.</returns>
        public T[] ToArray()
        {
            T[] newArray = new T[Count];

            for (int i = 0; i < Count; i++)
            {
                newArray[i] = this[i];
            }

            return newArray;
        }

        /// <summary>
        /// Returns an enumerator that iterates throught the <see cref="HashedArrayTree{T}"/>.
        /// </summary>
        /// <returns>Enumerator for the <see cref="HashedArrayTree{T}"/>.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < Count; i++)
            {
                yield return this[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
