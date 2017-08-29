using DSA.DataStructures.Lists;
using System;
using System.Collections.Generic;

namespace DSA.Algorithms.Sorting
{
    /// <summary>
    /// Static class containing extension methods for pigeonhole sort.
    /// </summary>
    public static class PigeonholeSorter
    {
        /// <summary>
        /// Sorts the elements in the entire <see cref="IList{T}"/> in ascending order. The difference between the minimun and maximum element must be lower than (<see cref="int.MaxValue"/> / 16).
        /// </summary>
        /// <param name="list">The <see cref="IList{T}"/> containing the elements for sorting.</param>
        /// <returns>Returns the given <see cref="IList{T}"/> when sorted.</returns>
        public static IList<int> PigeonholeSort(this IList<int> list)
        {
            if (list.Count == 0) return list;

            return PigeonholeSort(list, 0, list.Count);
        }

        /// <summary>
        /// Sorts the elements in the entire <see cref="IList{T}"/> in descending order. The difference between the minimun and maximum element must be lower than (<see cref="int.MaxValue"/> / 16).
        /// </summary>
        /// <param name="list">The <see cref="IList{T}"/> containing the elements for sorting.</param>
        /// <returns>Returns the given <see cref="IList{T}"/> when sorted.</returns>
        public static IList<int> PigeonholeSortDescending(this IList<int> list)
        {
            if (list.Count == 0) return list;

            return PigeonholeSortDescending(list, 0, list.Count);
        }

        /// <summary>
        /// Sorts a range of elements in the <see cref="IList{T}"/> in ascending order. The difference between the minimun and maximum element must be lower than (<see cref="int.MaxValue"/> / 16).
        /// </summary>
        /// <param name="list">The <see cref="IList{T}"/> containing the elements for sorting.</param>
        /// <param name="index">The zero-based starting index of the range for sorting.</param>
        /// <param name="count">The length of the range for sorting.</param>
        /// <returns>Returns the given <see cref="IList{T}"/> when sorted.</returns>
        public static IList<int> PigeonholeSort(this IList<int> list, int index, int count)
        {
            if (index < 0 || index >= list.Count) throw new ArgumentOutOfRangeException(nameof(index));
            if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));
            if (index + count > list.Count) throw new ArgumentException("Invalid length specified.");

            if (count == 1) return list;

            // Compute last index(exclusive)
            int lastIndex = index + count;
            // Get min and max elements in the array
            int min = list[index];
            int max = list[index];
            for (int i = index + 1; i < lastIndex; i++)
            {
                if (list[i] < min) min = list[i];
                else if (list[i] > max) max = list[i];
            }

            // Check for overflow
            int rangeOfItems = 0;
            try
            {
                rangeOfItems = checked(max - min + 1);
            }
            catch (OverflowException oe)
            {
                throw new InvalidOperationException("Range of elements is too big for this sorting algorithm!", oe);
            }

            // Check if range of items is too big
            if (rangeOfItems >= int.MaxValue / 16) throw new InvalidOperationException("Range of elements is too big for this sorting algorithm!");

            // Array of arraylists(representing each pigeonhole) for saving the items
            var pigeonholes = new ArrayList<int>[rangeOfItems];// throws OutOfMemoryException if range is too big and memory is not enought

            // Place the items in their corresponding pigeonholes
            for (int i = index; i < lastIndex; i++)
            {
                int hole = list[i] - min;
                if (pigeonholes[hole] == null)// check if initialization is needed
                    pigeonholes[hole] = new ArrayList<int>(1);
                pigeonholes[hole].Add(list[i]);// Adding the element in the pigeonhole
            }
            // index in the sorted array
            int sortedIndex = index;
            // Copy elements from the pigeonholes to the list
            for (int i = 0; i < rangeOfItems; i++)
            {
                if (pigeonholes[i] != null)
                {
                    for (int j = 0; j < pigeonholes[i].Count; j++)
                    {
                        list[sortedIndex++] = pigeonholes[i][j];
                    }
                }
            }

            return list;
        }

        /// <summary>
        /// Sorts a range of elements in the <see cref="IList{T}"/> in descending order. The difference between the minimun and maximum element must be lower than (<see cref="int.MaxValue"/> / 16).
        /// </summary>
        /// <param name="list">The <see cref="IList{T}"/> containing the elements for sorting.</param>
        /// <param name="index">The zero-based starting index of the range for sorting.</param>
        /// <param name="count">The length of the range for sorting.</param>
        /// <returns>Returns the given <see cref="IList{T}"/> when sorted.</returns>
        public static IList<int> PigeonholeSortDescending(this IList<int> list, int index, int count)
        {
            if (index < 0 || index >= list.Count) throw new ArgumentOutOfRangeException(nameof(index));
            if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));
            if (index + count > list.Count) throw new ArgumentException("Invalid length specified.");

            if (count == 1) return list;

            // Compute last index(exclusive)
            int lastIndex = index + count;
            // Get min and max elements in the array
            int min = list[index];
            int max = list[index];
            for (int i = index + 1; i < lastIndex; i++)
            {
                if (list[i] < min) min = list[i];
                else if (list[i] > max) max = list[i];
            }

            // Check for overflow
            int rangeOfItems = 0;
            try
            {
                rangeOfItems = checked(max - min + 1);
            }
            catch (OverflowException oe)
            {
                throw new InvalidOperationException("Range of elements is too big for this sorting algorithm!", oe);
            }

            // Check if range of items is too big
            if (rangeOfItems >= int.MaxValue / 16) throw new InvalidOperationException("Range of elements is too big for this sorting algorithm!");

            // Array of arraylists(representing each pigeonhole) for saving the items
            var pigeonholes = new ArrayList<int>[rangeOfItems];// throws OutOfMemoryException if range is too big and memory is not enought

            // Place the items in their corresponding pigeonholes
            for (int i = index; i < lastIndex; i++)
            {
                int hole = list[i] - min;
                if (pigeonholes[hole] == null)// check if initialization is needed
                    pigeonholes[hole] = new ArrayList<int>(1);
                pigeonholes[hole].Add(list[i]);// Adding the element in the pigeonhole
            }
            // index in the sorted array
            int sortedIndex = index;
            // Copy elements from the pigeonholes to the list
            for (int i = rangeOfItems - 1; i >= 0; i--)
            {
                if (pigeonholes[i] != null)
                {
                    for (int j = 0; j < pigeonholes[i].Count; j++)
                    {
                        list[sortedIndex++] = pigeonholes[i][j];
                    }
                }
            }

            return list;
        }

        /// <summary>
        /// Sorts the key-value pairs in the entire <see cref="IList{T}"/> by their key in ascending order. The difference between the minimun and maximum key must be lower than (<see cref="int.MaxValue"/> / 16).
        /// </summary>
        /// <typeparam name="TValue">The data type of the values in the list of <see cref="KeyValuePair{TKey, TValue}"/> items.</typeparam>
        /// <param name="list">The <see cref="IList{T}"/> containing the elements for sorting.</param>
        /// <returns>Returns the given <see cref="IList{T}"/> when sorted.</returns>
        public static IList<KeyValuePair<int, TValue>> PigeonholeSortKeys<TValue>(this IList<KeyValuePair<int, TValue>> list)
        {
            if (list.Count == 0) return list;

            return PigeonholeSortKeys(list, 0, list.Count);
        }

        /// <summary>
        /// Sorts the key-value pairs in the entire <see cref="IList{T}"/> by their key in descending order. The difference between the minimun and maximum key must be lower than (<see cref="int.MaxValue"/> / 16).
        /// </summary>
        /// <typeparam name="TValue">The data type of the values in the list of <see cref="KeyValuePair{TKey, TValue}"/> items.</typeparam>
        /// <param name="list">The <see cref="IList{T}"/> containing the elements for sorting.</param>
        /// <returns>Returns the given <see cref="IList{T}"/> when sorted.</returns>
        public static IList<KeyValuePair<int, TValue>> PigeonholeSortDescendingKeys<TValue>(this IList<KeyValuePair<int, TValue>> list)
        {
            if (list.Count == 0) return list;

            return PigeonholeSortDescendingKeys(list, 0, list.Count);
        }

        /// <summary>
        /// Sorts a range of key-value pairs in the <see cref="IList{T}"/> by their key in ascending order. The difference between the minimun and maximum key must be lower than (<see cref="int.MaxValue"/> / 16).
        /// </summary>
        /// <typeparam name="TValue">The data type of the values in the list of <see cref="KeyValuePair{TKey, TValue}"/> items.</typeparam>
        /// <param name="list">The <see cref="IList{T}"/> containing the elements for sorting.</param>
        /// <param name="index">The zero-based starting index of the range for sorting.</param>
        /// <param name="count">The length of the range for sorting.</param>
        /// <returns>Returns the given <see cref="IList{T}"/> when sorted.</returns>
        public static IList<KeyValuePair<int, TValue>> PigeonholeSortKeys<TValue>(this IList<KeyValuePair<int, TValue>> list, int index, int count)
        {
            if (index < 0 || index >= list.Count) throw new ArgumentOutOfRangeException(nameof(index));
            if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));
            if (index + count > list.Count) throw new ArgumentException("Invalid length specified.");

            if (count == 1) return list;

            // Compute last index(exclusive)
            int lastIndex = index + count;
            // Get min and max elements in the array
            int min = list[index].Key;
            int max = list[index].Key;
            for (int i = index + 1; i < lastIndex; i++)
            {
                if (list[i].Key < min) min = list[i].Key;
                else if (list[i].Key > max) max = list[i].Key;
            }

            // Check for overflow
            int rangeOfItems = 0;
            try
            {
                rangeOfItems = checked(max - min + 1);
            }
            catch (OverflowException oe)
            {
                throw new InvalidOperationException("Range of elements is too big for this sorting algorithm!", oe);
            }

            // Check if range of items is too big
            if (rangeOfItems >= int.MaxValue / 16) throw new InvalidOperationException("Range of elements is too big for this sorting algorithm!");

            // Array of arraylists(representing each pigeonhole) for saving the items
            var pigeonholes = new ArrayList<KeyValuePair<int, TValue>>[rangeOfItems];// throws OutOfMemoryException if range is too big and memory is not enought

            // Place the items in their corresponding pigeonholes
            for (int i = index; i < lastIndex; i++)
            {
                int hole = list[i].Key - min;
                if (pigeonholes[hole] == null)// check if initialization is needed
                    pigeonholes[hole] = new ArrayList<KeyValuePair<int, TValue>>(1);
                pigeonholes[hole].Add(list[i]);// Adding the element in the pigeonhole
            }
            // index in the sorted array
            int sortedIndex = index;
            // Copy elements from the pigeonholes to the list
            for (int i = 0; i < rangeOfItems; i++)
            {
                if (pigeonholes[i] != null)
                {
                    for (int j = 0; j < pigeonholes[i].Count; j++)
                    {
                        list[sortedIndex++] = pigeonholes[i][j];
                    }
                }
            }

            return list;
        }

        /// <summary>
        /// Sorts a range of key-value pairs in the <see cref="IList{T}"/> by their key in descending order. The difference between the minimun and maximum key must be lower than (<see cref="int.MaxValue"/> / 16).
        /// </summary>
        /// <typeparam name="TValue">The data type of the values in the list of <see cref="KeyValuePair{TKey, TValue}"/> items.</typeparam>
        /// <param name="list">The <see cref="IList{T}"/> containing the elements for sorting.</param>
        /// <param name="index">The zero-based starting index of the range for sorting.</param>
        /// <param name="count">The length of the range for sorting.</param>
        /// <returns>Returns the given <see cref="IList{T}"/> when sorted.</returns>
        public static IList<KeyValuePair<int, TValue>> PigeonholeSortDescendingKeys<TValue>(this IList<KeyValuePair<int, TValue>> list, int index, int count)
        {
            if (index < 0 || index >= list.Count) throw new ArgumentOutOfRangeException(nameof(index));
            if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));
            if (index + count > list.Count) throw new ArgumentException("Invalid length specified.");

            if (count == 1) return list;

            // Compute last index(exclusive)
            int lastIndex = index + count;
            // Get min and max elements in the array
            int min = list[index].Key;
            int max = list[index].Key;
            for (int i = index + 1; i < lastIndex; i++)
            {
                if (list[i].Key < min) min = list[i].Key;
                else if (list[i].Key > max) max = list[i].Key;
            }

            // Check for overflow
            int rangeOfItems = 0;
            try
            {
                rangeOfItems = checked(max - min + 1);
            }
            catch (OverflowException oe)
            {
                throw new InvalidOperationException("Range of elements is too big for this sorting algorithm!", oe);
            }

            // Check if range of items is too big
            if (rangeOfItems >= int.MaxValue / 16) throw new InvalidOperationException("Range of elements is too big for this sorting algorithm!");

            // Array of arraylists(representing each pigeonhole) for saving the items
            var pigeonholes = new ArrayList<KeyValuePair<int, TValue>>[rangeOfItems];// throws OutOfMemoryException if range is too big and memory is not enought

            // Place the items in their corresponding pigeonholes
            for (int i = index; i < lastIndex; i++)
            {
                int hole = list[i].Key - min;
                if (pigeonholes[hole] == null)// check if initialization is needed
                    pigeonholes[hole] = new ArrayList<KeyValuePair<int, TValue>>(1);
                pigeonholes[hole].Add(list[i]);// Adding the element in the pigeonhole
            }
            // index in the sorted array
            int sortedIndex = index;
            // Copy elements from the pigeonholes to the list
            for (int i = rangeOfItems - 1; i >= 0; i--)
            {
                if (pigeonholes[i] != null)
                {
                    for (int j = 0; j < pigeonholes[i].Count; j++)
                    {
                        list[sortedIndex++] = pigeonholes[i][j];
                    }
                }
            }

            return list;
        }

        /// <summary>
        /// Sorts the key-value pairs in the entire <see cref="IList{T}"/> by their value in ascending order. The difference between the minimun and maximum key must be lower than (<see cref="int.MaxValue"/> / 16).
        /// </summary>
        /// <typeparam name="TKey">The data type of the keys in the list of <see cref="KeyValuePair{TKey, TValue}"/> items.</typeparam>
        /// <param name="list">The <see cref="IList{T}"/> containing the elements for sorting.</param>
        /// <returns>Returns the given <see cref="IList{T}"/> when sorted.</returns>
        public static IList<KeyValuePair<TKey, int>> PigeonholeSortValues<TKey>(this IList<KeyValuePair<TKey, int>> list)
        {
            if (list.Count == 0) return list;

            return PigeonholeSortValues(list, 0, list.Count);
        }

        /// <summary>
        /// Sorts the key-value pairs in the entire <see cref="IList{T}"/> by their value in descending order. The difference between the minimun and maximum key must be lower than (<see cref="int.MaxValue"/> / 16).
        /// </summary>
        /// <typeparam name="TKey">The data type of the keys in the list of <see cref="KeyValuePair{TKey, TValue}"/> items.</typeparam>
        /// <param name="list">The <see cref="IList{T}"/> containing the elements for sorting.</param>
        /// <returns>Returns the given <see cref="IList{T}"/> when sorted.</returns>
        public static IList<KeyValuePair<TKey, int>> PigeonholeSortDescendingValues<TKey>(this IList<KeyValuePair<TKey, int>> list)
        {
            if (list.Count == 0) return list;

            return PigeonholeSortDescendingValues(list, 0, list.Count);
        }

        /// <summary>
        /// Sorts a range of key-value pairs in the <see cref="IList{T}"/> by their value in ascending order. The difference between the minimun and maximum key must be lower than (<see cref="int.MaxValue"/> / 16).
        /// </summary>
        /// <typeparam name="TKey">The data type of the keys in the list of <see cref="KeyValuePair{TKey, TValue}"/> items.</typeparam>
        /// <param name="list">The <see cref="IList{T}"/> containing the elements for sorting.</param>
        /// <param name="index">The zero-based starting index of the range for sorting.</param>
        /// <param name="count">The length of the range for sorting.</param>
        /// <returns>Returns the given <see cref="IList{T}"/> when sorted.</returns>
        public static IList<KeyValuePair<TKey, int>> PigeonholeSortValues<TKey>(this IList<KeyValuePair<TKey, int>> list, int index, int count)
        {
            if (index < 0 || index >= list.Count) throw new ArgumentOutOfRangeException(nameof(index));
            if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));
            if (index + count > list.Count) throw new ArgumentException("Invalid length specified.");

            if (count == 1) return list;

            // Compute last index(exclusive)
            int lastIndex = index + count;
            // Get min and max elements in the array
            int min = list[index].Value;
            int max = list[index].Value;
            for (int i = index + 1; i < lastIndex; i++)
            {
                if (list[i].Value < min) min = list[i].Value;
                else if (list[i].Value > max) max = list[i].Value;
            }

            // Check for overflow
            int rangeOfItems = 0;
            try
            {
                rangeOfItems = checked(max - min + 1);
            }
            catch (OverflowException oe)
            {
                throw new InvalidOperationException("Range of elements is too big for this sorting algorithm!", oe);
            }

            // Check if range of items is too big
            if (rangeOfItems >= int.MaxValue / 16) throw new InvalidOperationException("Range of elements is too big for this sorting algorithm!");

            // Array of arraylists(representing each pigeonhole) for saving the items
            var pigeonholes = new ArrayList<KeyValuePair<TKey, int>>[rangeOfItems];// throws OutOfMemoryException if range is too big and memory is not enought

            // Place the items in their corresponding pigeonholes
            for (int i = index; i < lastIndex; i++)
            {
                int hole = list[i].Value - min;
                if (pigeonholes[hole] == null)// check if initialization is needed
                    pigeonholes[hole] = new ArrayList<KeyValuePair<TKey, int>>(1);
                pigeonholes[hole].Add(list[i]);// Adding the element in the pigeonhole
            }
            // index in the sorted array
            int sortedIndex = index;
            // Copy elements from the pigeonholes to the list
            for (int i = 0; i < rangeOfItems; i++)
            {
                if (pigeonholes[i] != null)
                {
                    for (int j = 0; j < pigeonholes[i].Count; j++)
                    {
                        list[sortedIndex++] = pigeonholes[i][j];
                    }
                }
            }

            return list;
        }

        /// <summary>
        /// Sorts a range of key-value pairs in the <see cref="IList{T}"/> by their value in descending order. The difference between the minimun and maximum key must be lower than (<see cref="int.MaxValue"/> / 16).
        /// </summary>
        /// <typeparam name="TKey">The data type of the keys in the list of <see cref="KeyValuePair{TKey, TValue}"/> items.</typeparam>
        /// <param name="list">The <see cref="IList{T}"/> containing the elements for sorting.</param>
        /// <param name="index">The zero-based starting index of the range for sorting.</param>
        /// <param name="count">The length of the range for sorting.</param>
        /// <returns>Returns the given <see cref="IList{T}"/> when sorted.</returns>
        public static IList<KeyValuePair<TKey, int>> PigeonholeSortDescendingValues<TKey>(this IList<KeyValuePair<TKey, int>> list, int index, int count)
        {
            if (index < 0 || index >= list.Count) throw new ArgumentOutOfRangeException(nameof(index));
            if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));
            if (index + count > list.Count) throw new ArgumentException("Invalid length specified.");

            if (count == 1) return list;

            // Compute last index(exclusive)
            int lastIndex = index + count;
            // Get min and max elements in the array
            int min = list[index].Value;
            int max = list[index].Value;
            for (int i = index + 1; i < lastIndex; i++)
            {
                if (list[i].Value < min) min = list[i].Value;
                else if (list[i].Value > max) max = list[i].Value;
            }

            // Check for overflow
            int rangeOfItems = 0;
            try
            {
                rangeOfItems = checked(max - min + 1);
            }
            catch (OverflowException oe)
            {
                throw new InvalidOperationException("Range of elements is too big for this sorting algorithm!", oe);
            }

            // Check if range of items is too big
            if (rangeOfItems >= int.MaxValue / 16) throw new InvalidOperationException("Range of elements is too big for this sorting algorithm!");

            // Array of arraylists(representing each pigeonhole) for saving the items
            var pigeonholes = new ArrayList<KeyValuePair<TKey, int>>[rangeOfItems];// throws OutOfMemoryException if range is too big and memory is not enought

            // Place the items in their corresponding pigeonholes
            for (int i = index; i < lastIndex; i++)
            {
                int hole = list[i].Value - min;
                if (pigeonholes[hole] == null)// check if initialization is needed
                    pigeonholes[hole] = new ArrayList<KeyValuePair<TKey, int>>(1);
                pigeonholes[hole].Add(list[i]);// Adding the element in the pigeonhole
            }
            // index in the sorted array
            int sortedIndex = index;
            // Copy elements from the pigeonholes to the list
            for (int i = rangeOfItems - 1; i >= 0; i--)
            {
                if (pigeonholes[i] != null)
                {
                    for (int j = 0; j < pigeonholes[i].Count; j++)
                    {
                        list[sortedIndex++] = pigeonholes[i][j];
                    }
                }
            }

            return list;
        }
    }
}
