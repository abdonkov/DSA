using System;
using System.Collections.Generic;

namespace DSA.Algorithms.Sorting
{
    /// <summary>
    /// Static class containing extension methods for LSD Radix sort.
    /// </summary>
    public static class LSDRadixSorter
    {
        /// <summary>
        /// Sorts the elements in the entire <see cref="IList{T}"/> in ascending order.
        /// </summary>
        /// <param name="list">The <see cref="IList{T}"/> containing the elements for sorting.</param>
        /// <returns>Returns the given <see cref="IList{T}"/> when sorted.</returns>
        public static IList<int> LSDRadixSort(this IList<int> list)
        {
            if (list.Count == 0) return list;

            return LSDRadixSort(list, 0, list.Count);
        }

        /// <summary>
        /// Sorts the elements in the entire <see cref="IList{T}"/> in descending order.
        /// </summary>
        /// <param name="list">The <see cref="IList{T}"/> containing the elements for sorting.</param>
        /// <returns>Returns the given <see cref="IList{T}"/> when sorted.</returns>
        public static IList<int> LSDRadixSortDescending(this IList<int> list)
        {
            if (list.Count == 0) return list;

            return LSDRadixSortDescending(list, 0, list.Count);
        }

        /// <summary>
        /// Sorts a range of elements in the <see cref="IList{T}"/> in ascending order.
        /// </summary>
        /// <param name="list">The <see cref="IList{T}"/> containing the elements for sorting.</param>
        /// <param name="index">The zero-based starting index of the range for sorting.</param>
        /// <param name="count">The length of the range for sorting.</param>
        /// <returns>Returns the given <see cref="IList{T}"/> when sorted.</returns>
        public static IList<int> LSDRadixSort(this IList<int> list, int index, int count)
        {
            if (index < 0 || index >= list.Count) throw new ArgumentOutOfRangeException(nameof(index));
            if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));
            if (index + count > list.Count) throw new ArgumentException("Invalid length specified.");

            if (count == 1) return list;

            // Compute last index(exclusive)
            int lastIndex = index + count;

            // Find min element
            int min = list[index];
            for (int i = index + 1; i < lastIndex; i++)
            {
                if (list[i] < min) min = list[i];
            }

            // Number of bits for each group
            int bitsPerGroup = 8;// 8 bits per group making the counts array having a 256 elements

            // Number of groups
            int groupsNumber = 32 / bitsPerGroup;

            // The mask to identify the groups
            int mask = (1 << bitsPerGroup) - 1;

            // Array for saving the number of occurrences for the items in a given group
            int[] counts = new int[1 << bitsPerGroup];

            // Array to copy the sorted items from a given group
            int[] copyArray = new int[count];

            // Iterating over the groups of bits and sorting them
            for (int group = 0, shift = 0; group < groupsNumber; group++, shift += bitsPerGroup)
            {
                // Reset count array
                for (int i = 0; i < counts.Length; i++)
                {
                    counts[i] = 0;
                }

                // Counting the elements of the current group
                for (int i = index; i < lastIndex; i++)
                {
                    counts[((list[i] - min) >> shift) & mask]++;
                }

                // Calculating the starting index for each item
                for (int i = 1; i < counts.Length; i++)
                {
                    counts[i] += counts[i - 1];
                }

                // Copy the now sorted by group elements to the copyArray
                for (int i = lastIndex - 1; i >= index; i--)
                {
                    copyArray[--counts[((list[i] - min) >> shift) & mask]] = list[i];
                }

                // copyArray index
                int j = 0;
                // Copy elements from the copyArray to the list
                for (int i = index; i < lastIndex; i++)
                {
                    list[i] = copyArray[j++];
                }
            }

            return list;
        }

        /// <summary>
        /// Sorts a range of elements in the <see cref="IList{T}"/> in descending order.
        /// </summary>
        /// <param name="list">The <see cref="IList{T}"/> containing the elements for sorting.</param>
        /// <param name="index">The zero-based starting index of the range for sorting.</param>
        /// <param name="count">The length of the range for sorting.</param>
        /// <returns>Returns the given <see cref="IList{T}"/> when sorted.</returns>
        public static IList<int> LSDRadixSortDescending(this IList<int> list, int index, int count)
        {
            if (index < 0 || index >= list.Count) throw new ArgumentOutOfRangeException(nameof(index));
            if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));
            if (index + count > list.Count) throw new ArgumentException("Invalid length specified.");

            if (count == 1) return list;

            // Compute last index(exclusive)
            int lastIndex = index + count;

            // Find min element
            int min = list[index];
            for (int i = index + 1; i < lastIndex; i++)
            {
                if (list[i] < min) min = list[i];
            }

            // Number of bits for each group
            int bitsPerGroup = 8;// 8 bits per group making the counts array having a 256 elements

            // Number of groups
            int groupsNumber = 32 / bitsPerGroup;

            // The mask to identify the groups
            int mask = (1 << bitsPerGroup) - 1;

            // Array for saving the number of occurrences for the items in a given group
            int[] counts = new int[1 << bitsPerGroup];

            // Array to copy the sorted items from a given group
            int[] copyArray = new int[count];

            // Iterating over the groups of bits and sorting them
            for (int group = 0, shift = 0; group < groupsNumber; group++, shift += bitsPerGroup)
            {
                // Reset count array
                for (int i = 0; i < counts.Length; i++)
                    counts[i] = 0;

                // Counting the elements of the current group
                for (int i = index; i < lastIndex; i++)
                    counts[((list[i] - min) >> shift) & mask]++;

                // Calculating the starting index for each item
                for (int i = 1; i < counts.Length; i++)
                    counts[i] += counts[i - 1];

                // Copy the now sorted by group elements to the copyArray
                for (int i = index; i < lastIndex; i++)
                {
                    copyArray[--counts[((list[i] - min) >> shift) & mask]] = list[i];
                }

                // copyArray index
                int j = count - 1;
                // Copy elements from the copyArray to the list
                for (int i = index; i < lastIndex; i++)
                {
                    list[i] = copyArray[j--];
                }
            }

            return list;
        }

        /// <summary>
        /// Sorts the key-value pairs in the entire <see cref="IList{T}"/> by their key in ascending order.
        /// </summary>
        /// <typeparam name="TValue">The data type of the values in the list of <see cref="KeyValuePair{TKey, TValue}"/> items.</typeparam>
        /// <param name="list">The <see cref="IList{T}"/> containing the elements for sorting.</param>
        /// <returns>Returns the given <see cref="IList{T}"/> when sorted.</returns>
        public static IList<KeyValuePair<int, TValue>> LSDRadixSortKeys<TValue>(this IList<KeyValuePair<int, TValue>> list)
        {
            if (list.Count == 0) return list;

            return LSDRadixSortKeys(list, 0, list.Count);
        }

        /// <summary>
        /// Sorts the key-value pairs in the entire <see cref="IList{T}"/> by their key in descending order.
        /// </summary>
        /// <typeparam name="TValue">The data type of the values in the list of <see cref="KeyValuePair{TKey, TValue}"/> items.</typeparam>
        /// <param name="list">The <see cref="IList{T}"/> containing the elements for sorting.</param>
        /// <returns>Returns the given <see cref="IList{T}"/> when sorted.</returns>
        public static IList<KeyValuePair<int, TValue>> LSDRadixSortDescendingKeys<TValue>(this IList<KeyValuePair<int, TValue>> list)
        {
            if (list.Count == 0) return list;

            return LSDRadixSortDescendingKeys(list, 0, list.Count);
        }

        /// <summary>
        /// Sorts a range of key-value pairs in the <see cref="IList{T}"/> by their key in ascending order.
        /// </summary>
        /// <typeparam name="TValue">The data type of the values in the list of <see cref="KeyValuePair{TKey, TValue}"/> items.</typeparam>
        /// <param name="list">The <see cref="IList{T}"/> containing the elements for sorting.</param>
        /// <param name="index">The zero-based starting index of the range for sorting.</param>
        /// <param name="count">The length of the range for sorting.</param>
        /// <returns>Returns the given <see cref="IList{T}"/> when sorted.</returns>
        public static IList<KeyValuePair<int, TValue>> LSDRadixSortKeys<TValue>(this IList<KeyValuePair<int, TValue>> list, int index, int count)
        {
            if (index < 0 || index >= list.Count) throw new ArgumentOutOfRangeException(nameof(index));
            if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));
            if (index + count > list.Count) throw new ArgumentException("Invalid length specified.");

            if (count == 1) return list;

            // Compute last index(exclusive)
            int lastIndex = index + count;

            // Find min element
            int min = list[index].Key;
            for (int i = index + 1; i < lastIndex; i++)
            {
                if (list[i].Key < min) min = list[i].Key;
            }

            // Number of bits for each group
            int bitsPerGroup = 8;// 8 bits per group making the counts array having a 256 elements

            // Number of groups
            int groupsNumber = 32 / bitsPerGroup;

            // The mask to identify the groups
            int mask = (1 << bitsPerGroup) - 1;

            // Array for saving the number of occurrences for the items in a given group
            int[] counts = new int[1 << bitsPerGroup];

            // Array to copy the sorted items from a given group
            var copyArray = new KeyValuePair<int, TValue>[count];

            // Iterating over the groups of bits and sorting them
            for (int group = 0, shift = 0; group < groupsNumber; group++, shift += bitsPerGroup)
            {
                // Reset count array
                for (int i = 0; i < counts.Length; i++)
                    counts[i] = 0;

                // Counting the elements of the current group
                for (int i = index; i < lastIndex; i++)
                    counts[((list[i].Key - min) >> shift) & mask]++;

                // Calculating the starting index for each item
                for (int i = 1; i < counts.Length; i++)
                    counts[i] += counts[i - 1];

                // Copy the now sorted by group elements to the copyArray
                for (int i = lastIndex - 1; i >= index; i--)
                {
                    copyArray[--counts[((list[i].Key - min) >> shift) & mask]] = list[i];
                }

                // copyArray index
                int j = 0;
                // Copy elements from the copyArray to the list
                for (int i = index; i < lastIndex; i++)
                {
                    list[i] = copyArray[j++];
                }
            }

            return list;
        }

        /// <summary>
        /// Sorts a range of key-value pairs in the <see cref="IList{T}"/> by their key in descending order.
        /// </summary>
        /// <typeparam name="TValue">The data type of the values in the list of <see cref="KeyValuePair{TKey, TValue}"/> items.</typeparam>
        /// <param name="list">The <see cref="IList{T}"/> containing the elements for sorting.</param>
        /// <param name="index">The zero-based starting index of the range for sorting.</param>
        /// <param name="count">The length of the range for sorting.</param>
        /// <returns>Returns the given <see cref="IList{T}"/> when sorted.</returns>
        public static IList<KeyValuePair<int, TValue>> LSDRadixSortDescendingKeys<TValue>(this IList<KeyValuePair<int, TValue>> list, int index, int count)
        {
            if (index < 0 || index >= list.Count) throw new ArgumentOutOfRangeException(nameof(index));
            if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));
            if (index + count > list.Count) throw new ArgumentException("Invalid length specified.");

            if (count == 1) return list;

            // Compute last index(exclusive)
            int lastIndex = index + count;

            // Find min element
            int min = list[index].Key;
            for (int i = index + 1; i < lastIndex; i++)
            {
                if (list[i].Key < min) min = list[i].Key;
            }

            // Number of bits for each group
            int bitsPerGroup = 8;// 8 bits per group making the counts array having a 256 elements

            // Number of groups
            int groupsNumber = 32 / bitsPerGroup;

            // The mask to identify the groups
            int mask = (1 << bitsPerGroup) - 1;

            // Array for saving the number of occurrences for the items in a given group
            int[] counts = new int[1 << bitsPerGroup];

            // Array to copy the sorted items from a given group
            var copyArray = new KeyValuePair<int, TValue>[count];

            // Iterating over the groups of bits and sorting them
            for (int group = 0, shift = 0; group < groupsNumber; group++, shift += bitsPerGroup)
            {
                // Reset count array
                for (int i = 0; i < counts.Length; i++)
                    counts[i] = 0;

                // Counting the elements of the current group
                for (int i = index; i < lastIndex; i++)
                    counts[((list[i].Key - min) >> shift) & mask]++;

                // Calculating the starting index for each item
                for (int i = 1; i < counts.Length; i++)
                    counts[i] += counts[i - 1];

                // Copy the now sorted by group elements to the copyArray
                for (int i = index; i < lastIndex; i++)
                {
                    copyArray[--counts[((list[i].Key - min) >> shift) & mask]] = list[i];
                }

                // copyArray index
                int j = count - 1;
                // Copy elements from the copyArray to the list
                for (int i = index; i < lastIndex; i++)
                {
                    list[i] = copyArray[j--];
                }
            }

            return list;
        }

        /// <summary>
        /// Sorts the key-value pairs in the entire <see cref="IList{T}"/> by their value in ascending order.
        /// </summary>
        /// <typeparam name="TKey">The data type of the keys in the list of <see cref="KeyValuePair{TKey, TValue}"/> items.</typeparam>
        /// <param name="list">The <see cref="IList{T}"/> containing the elements for sorting.</param>
        /// <returns>Returns the given <see cref="IList{T}"/> when sorted.</returns>
        public static IList<KeyValuePair<TKey, int>> LSDRadixSortValues<TKey>(this IList<KeyValuePair<TKey, int>> list)
        {
            if (list.Count == 0) return list;

            return LSDRadixSortValues(list, 0, list.Count);
        }

        /// <summary>
        /// Sorts the key-value pairs in the entire <see cref="IList{T}"/> by their value in descending order.
        /// </summary>
        /// <typeparam name="TKey">The data type of the keys in the list of <see cref="KeyValuePair{TKey, TValue}"/> items.</typeparam>
        /// <param name="list">The <see cref="IList{T}"/> containing the elements for sorting.</param>
        /// <returns>Returns the given <see cref="IList{T}"/> when sorted.</returns>
        public static IList<KeyValuePair<TKey, int>> LSDRadixSortDescendingValues<TKey>(this IList<KeyValuePair<TKey, int>> list)
        {
            if (list.Count == 0) return list;

            return LSDRadixSortDescendingValues(list, 0, list.Count);
        }

        /// <summary>
        /// Sorts a range of key-value pairs in the <see cref="IList{T}"/> by their value in ascending order.
        /// </summary>
        /// <typeparam name="TKey">The data type of the keys in the list of <see cref="KeyValuePair{TKey, TValue}"/> items.</typeparam>
        /// <param name="list">The <see cref="IList{T}"/> containing the elements for sorting.</param>
        /// <param name="index">The zero-based starting index of the range for sorting.</param>
        /// <param name="count">The length of the range for sorting.</param>
        /// <returns>Returns the given <see cref="IList{T}"/> when sorted.</returns>
        public static IList<KeyValuePair<TKey, int>> LSDRadixSortValues<TKey>(this IList<KeyValuePair<TKey, int>> list, int index, int count)
        {
            if (index < 0 || index >= list.Count) throw new ArgumentOutOfRangeException(nameof(index));
            if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));
            if (index + count > list.Count) throw new ArgumentException("Invalid length specified.");

            if (count == 1) return list;

            // Compute last index(exclusive)
            int lastIndex = index + count;

            // Find min element
            int min = list[index].Value;
            for (int i = index + 1; i < lastIndex; i++)
            {
                if (list[i].Value < min) min = list[i].Value;
            }

            // Number of bits for each group
            int bitsPerGroup = 8;// 8 bits per group making the counts array having a 256 elements

            // Number of groups
            int groupsNumber = 32 / bitsPerGroup;

            // The mask to identify the groups
            int mask = (1 << bitsPerGroup) - 1;

            // Array for saving the number of occurrences for the items in a given group
            int[] counts = new int[1 << bitsPerGroup];

            // Array to copy the sorted items from a given group
            var copyArray = new KeyValuePair<TKey, int>[count];

            // Iterating over the groups of bits and sorting them
            for (int group = 0, shift = 0; group < groupsNumber; group++, shift += bitsPerGroup)
            {
                // Reset count array
                for (int i = 0; i < counts.Length; i++)
                    counts[i] = 0;

                // Counting the elements of the current group
                for (int i = index; i < lastIndex; i++)
                    counts[((list[i].Value - min) >> shift) & mask]++;

                // Calculating the starting index for each item
                for (int i = 1; i < counts.Length; i++)
                    counts[i] += counts[i - 1];

                // Copy the now sorted by group elements to the copyArray
                for (int i = lastIndex - 1; i >= index; i--)
                {
                    copyArray[--counts[((list[i].Value - min) >> shift) & mask]] = list[i];
                }

                // copyArray index
                int j = 0;
                // Copy elements from the copyArray to the list
                for (int i = index; i < lastIndex; i++)
                {
                    list[i] = copyArray[j++];
                }
            }

            return list;
        }

        /// <summary>
        /// Sorts a range of key-value pairs in the <see cref="IList{T}"/> by their value in descending order.
        /// </summary>
        /// <typeparam name="TKey">The data type of the keys in the list of <see cref="KeyValuePair{TKey, TValue}"/> items.</typeparam>
        /// <param name="list">The <see cref="IList{T}"/> containing the elements for sorting.</param>
        /// <param name="index">The zero-based starting index of the range for sorting.</param>
        /// <param name="count">The length of the range for sorting.</param>
        /// <returns>Returns the given <see cref="IList{T}"/> when sorted.</returns>
        public static IList<KeyValuePair<TKey, int>> LSDRadixSortDescendingValues<TKey>(this IList<KeyValuePair<TKey, int>> list, int index, int count)
        {
            if (index < 0 || index >= list.Count) throw new ArgumentOutOfRangeException(nameof(index));
            if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));
            if (index + count > list.Count) throw new ArgumentException("Invalid length specified.");

            if (count == 1) return list;

            // Compute last index(exclusive)
            int lastIndex = index + count;

            // Find min element
            int min = list[index].Value;
            for (int i = index + 1; i < lastIndex; i++)
            {
                if (list[i].Value < min) min = list[i].Value;
            }

            // Number of bits for each group
            int bitsPerGroup = 8;// 8 bits per group making the counts array having a 256 elements

            // Number of groups
            int groupsNumber = 32 / bitsPerGroup;

            // The mask to identify the groups
            int mask = (1 << bitsPerGroup) - 1;

            // Array for saving the number of occurrences for the items in a given group
            int[] counts = new int[1 << bitsPerGroup];

            // Array to copy the sorted items from a given group
            var copyArray = new KeyValuePair<TKey, int>[count];

            // Iterating over the groups of bits and sorting them
            for (int group = 0, shift = 0; group < groupsNumber; group++, shift += bitsPerGroup)
            {
                // Reset count array
                for (int i = 0; i < counts.Length; i++)
                    counts[i] = 0;

                // Counting the elements of the current group
                for (int i = index; i < lastIndex; i++)
                    counts[((list[i].Value - min) >> shift) & mask]++;

                // Calculating the starting index for each item
                for (int i = 1; i < counts.Length; i++)
                    counts[i] += counts[i - 1];

                // Copy the now sorted by group elements to the copyArray
                for (int i = index; i < lastIndex; i++)
                {
                    copyArray[--counts[((list[i].Value - min) >> shift) & mask]] = list[i];
                }

                // copyArray index
                int j = count - 1;
                // Copy elements from the copyArray to the list
                for (int i = index; i < lastIndex; i++)
                {
                    list[i] = copyArray[j--];
                }
            }

            return list;
        }
    }
}
