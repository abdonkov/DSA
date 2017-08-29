using System;
using System.Collections.Generic;

namespace DSA.Algorithms.Sorting
{
    /// <summary>
    /// Static class containing extension methods for counting sort.
    /// </summary>
    public static class CountingSorter
    {
        /// <summary>
        /// Sorts the elements in the entire <see cref="IList{T}"/> in ascending order. The difference between the minimun and maximum element must be lower than (<see cref="int.MaxValue"/> / 4).
        /// </summary>
        /// <param name="list">The <see cref="IList{T}"/> containing the elements for sorting.</param>
        /// <returns>Returns the given <see cref="IList{T}"/> when sorted.</returns>
        public static IList<int> CountingSort(this IList<int> list)
        {
            if (list.Count == 0) return list;

            return CountingSort(list, 0, list.Count);
        }

        /// <summary>
        /// Sorts the elements in the entire <see cref="IList{T}"/> in descending order. The difference between the minimun and maximum element must be lower than (<see cref="int.MaxValue"/> / 4).
        /// </summary>
        /// <param name="list">The <see cref="IList{T}"/> containing the elements for sorting.</param>
        /// <returns>Returns the given <see cref="IList{T}"/> when sorted.</returns>
        public static IList<int> CountingSortDescending(this IList<int> list)
        {
            if (list.Count == 0) return list;

            return CountingSortDescending(list, 0, list.Count);
        }

        /// <summary>
        /// Sorts a range of elements in the <see cref="IList{T}"/> in ascending order. The difference between the minimun and maximum element must be lower than (<see cref="int.MaxValue"/> / 4).
        /// </summary>
        /// <param name="list">The <see cref="IList{T}"/> containing the elements for sorting.</param>
        /// <param name="index">The zero-based starting index of the range for sorting.</param>
        /// <param name="count">The length of the range for sorting.</param>
        /// <returns>Returns the given <see cref="IList{T}"/> when sorted.</returns>
        public static IList<int> CountingSort(this IList<int> list, int index, int count)
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
            catch(OverflowException oe)
            {
                throw new InvalidOperationException("Range of elements is too big for this sorting algorithm!", oe);
            }

            // Check if range of items is too big
            if (rangeOfItems >= int.MaxValue / 4) throw new InvalidOperationException("Range of elements is too big for this sorting algorithm!");

            // Array for saving the number of items
            int[] counts = new int[rangeOfItems];// throws OutOfMemoryException if range is too big and memory is not enought
            // Array for copying sorted elements
            int[] copyArray = new int[count];

            // Count the numbers
            for (int i = index; i < lastIndex; i++)
            {
                counts[list[i] - min] += 1;
            }
            // Calculating the starting index of each key by updating counts array
            // so each index is storing the sum till the previous step
            for (int i = 1; i < rangeOfItems; i++)
            {
                counts[i] += counts[i - 1];
            }
            // Copying sorted elements from the list in the copyArray preserving order of equal elements
            for (int i = lastIndex - 1; i >= index; i--)
            {
                copyArray[--counts[list[i] - min]] = list[i];
            }
            // copyArray index
            int j = 0;
            // Copy elements from the copyArray to the list
            for (int i = index; i < lastIndex; i++)
            {
                list[i] = copyArray[j++];
            }

            return list;
        }

        /// <summary>
        /// Sorts a range of elements in the <see cref="IList{T}"/> in descending order. The difference between the minimun and maximum element must be lower than (<see cref="int.MaxValue"/> / 4).
        /// </summary>
        /// <param name="list">The <see cref="IList{T}"/> containing the elements for sorting.</param>
        /// <param name="index">The zero-based starting index of the range for sorting.</param>
        /// <param name="count">The length of the range for sorting.</param>
        /// <returns>Returns the given <see cref="IList{T}"/> when sorted.</returns>
        public static IList<int> CountingSortDescending(this IList<int> list, int index, int count)
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
            if (rangeOfItems >= int.MaxValue / 4) throw new InvalidOperationException("Range of elements is too big for this sorting algorithm!");

            // Array for saving the number of items
            int[] counts = new int[rangeOfItems];// throws OutOfMemoryException if range is too big and memory is not enought
            // Array for copying sorted elements
            int[] copyArray = new int[count];

            // Count the numbers
            for (int i = index; i < lastIndex; i++)
            {
                counts[list[i] - min] += 1;
            }
            // Calculating the starting index of each key by updating counts array
            // so each index is storing the sum till the previous step
            for (int i = 1; i < rangeOfItems; i++)
            {
                counts[i] += counts[i - 1];
            }
            // Copying sorted elements from the list in the copyArray preserving order of equal elements
            for (int i = index; i < lastIndex; i++)
            {
                copyArray[--counts[list[i] - min]] = list[i];
            }
            // copyArray index
            int j = count - 1;
            // Copy elements from the copyArray to the list
            for (int i = index; i < lastIndex; i++)
            {
                list[i] = copyArray[j--];
            }

            return list;
        }

        /// <summary>
        /// Sorts the key-value pairs in the entire <see cref="IList{T}"/> by their key in ascending order. The difference between the minimun and maximum key must be lower than (<see cref="int.MaxValue"/> / 4).
        /// </summary>
        /// <typeparam name="TValue">The data type of the values in the list of <see cref="KeyValuePair{TKey, TValue}"/> items.</typeparam>
        /// <param name="list">The <see cref="IList{T}"/> containing the elements for sorting.</param>
        /// <returns>Returns the given <see cref="IList{T}"/> when sorted.</returns>
        public static IList<KeyValuePair<int, TValue>> CountingSortKeys<TValue>(this IList<KeyValuePair<int, TValue>> list)
        {
            if (list.Count == 0) return list;

            return CountingSortKeys(list, 0, list.Count);
        }

        /// <summary>
        /// Sorts the key-value pairs in the entire <see cref="IList{T}"/> by their key in descending order. The difference between the minimun and maximum key must be lower than (<see cref="int.MaxValue"/> / 4).
        /// </summary>
        /// <typeparam name="TValue">The data type of the values in the list of <see cref="KeyValuePair{TKey, TValue}"/> items.</typeparam>
        /// <param name="list">The <see cref="IList{T}"/> containing the elements for sorting.</param>
        /// <returns>Returns the given <see cref="IList{T}"/> when sorted.</returns>
        public static IList<KeyValuePair<int, TValue>> CountingSortDescendingKeys<TValue>(this IList<KeyValuePair<int, TValue>> list)
        {
            if (list.Count == 0) return list;

            return CountingSortDescendingKeys(list, 0, list.Count);
        }

        /// <summary>
        /// Sorts a range of key-value pairs in the <see cref="IList{T}"/> by their key in ascending order. The difference between the minimun and maximum key must be lower than (<see cref="int.MaxValue"/> / 4).
        /// </summary>
        /// <typeparam name="TValue">The data type of the values in the list of <see cref="KeyValuePair{TKey, TValue}"/> items.</typeparam>
        /// <param name="list">The <see cref="IList{T}"/> containing the elements for sorting.</param>
        /// <param name="index">The zero-based starting index of the range for sorting.</param>
        /// <param name="count">The length of the range for sorting.</param>
        /// <returns>Returns the given <see cref="IList{T}"/> when sorted.</returns>
        public static IList<KeyValuePair<int, TValue>> CountingSortKeys<TValue>(this IList<KeyValuePair<int, TValue>> list, int index, int count)
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
            if (rangeOfItems >= int.MaxValue / 4) throw new InvalidOperationException("Range of elements is too big for this sorting algorithm!");

            // Array for saving the number of items
            int[] counts = new int[rangeOfItems];// throws OutOfMemoryException if range is too big and memory is not enought
            // Array for copying sorted elements
            var copyArray = new KeyValuePair<int, TValue>[count];

            // Count the numbers
            for (int i = index; i < lastIndex; i++)
            {
                counts[list[i].Key - min] += 1;
            }
            // Calculating the starting index of each key by updating counts array
            // so each index is storing the sum till the previous step
            for (int i = 1; i < rangeOfItems; i++)
            {
                counts[i] += counts[i - 1];
            }
            // Copying sorted elements from the list in the copyArray preserving order of equal elements
            for (int i = lastIndex - 1; i >= index; i--)
            {
                copyArray[--counts[list[i].Key - min]] = list[i];
            }
            // copyArray index
            int j = 0;
            // Copy elements from the copyArray to the list
            for (int i = index; i < lastIndex; i++)
            {
                list[i] = copyArray[j++];
            }

            return list;
        }

        /// <summary>
        /// Sorts a range of key-value pairs in the <see cref="IList{T}"/> by their key in descending order. The difference between the minimun and maximum key must be lower than (<see cref="int.MaxValue"/> / 4).
        /// </summary>
        /// <typeparam name="TValue">The data type of the values in the list of <see cref="KeyValuePair{TKey, TValue}"/> items.</typeparam>
        /// <param name="list">The <see cref="IList{T}"/> containing the elements for sorting.</param>
        /// <param name="index">The zero-based starting index of the range for sorting.</param>
        /// <param name="count">The length of the range for sorting.</param>
        /// <returns>Returns the given <see cref="IList{T}"/> when sorted.</returns>
        public static IList<KeyValuePair<int, TValue>> CountingSortDescendingKeys<TValue>(this IList<KeyValuePair<int, TValue>> list, int index, int count)
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
            if (rangeOfItems >= int.MaxValue / 4) throw new InvalidOperationException("Range of elements is too big for this sorting algorithm!");

            // Array for saving the number of items
            int[] counts = new int[rangeOfItems];// throws OutOfMemoryException if range is too big and memory is not enought
            // Array for copying sorted elements
            var copyArray = new KeyValuePair<int, TValue>[count];

            // Count the numbers
            for (int i = index; i < lastIndex; i++)
            {
                counts[list[i].Key - min] += 1;
            }
            // Calculating the starting index of each key by updating counts array
            // so each index is storing the sum till the previous step
            for (int i = 1; i < rangeOfItems; i++)
            {
                counts[i] += counts[i - 1];
            }
            // Copying sorted elements from the list in the copyArray preserving order of equal elements
            for (int i = index; i < lastIndex; i++)
            {
                copyArray[--counts[list[i].Key - min]] = list[i];
            }
            // copyArray index
            int j = count - 1;
            // Copy elements from the copyArray to the list
            for (int i = index; i < lastIndex; i++)
            {
                list[i] = copyArray[j--];
            }

            return list;
        }

        /// <summary>
        /// Sorts the key-value pairs in the entire <see cref="IList{T}"/> by their value in ascending order. The difference between the minimun and maximum key must be lower than (<see cref="int.MaxValue"/> / 4).
        /// </summary>
        /// <typeparam name="TKey">The data type of the keys in the list of <see cref="KeyValuePair{TKey, TValue}"/> items.</typeparam>
        /// <param name="list">The <see cref="IList{T}"/> containing the elements for sorting.</param>
        /// <returns>Returns the given <see cref="IList{T}"/> when sorted.</returns>
        public static IList<KeyValuePair<TKey, int>> CountingSortValues<TKey>(this IList<KeyValuePair<TKey, int>> list)
        {
            if (list.Count == 0) return list;

            return CountingSortValues(list, 0, list.Count);
        }

        /// <summary>
        /// Sorts the key-value pairs in the entire <see cref="IList{T}"/> by their value in descending order. The difference between the minimun and maximum key must be lower than (<see cref="int.MaxValue"/> / 4).
        /// </summary>
        /// <typeparam name="TKey">The data type of the keys in the list of <see cref="KeyValuePair{TKey, TValue}"/> items.</typeparam>
        /// <param name="list">The <see cref="IList{T}"/> containing the elements for sorting.</param>
        /// <returns>Returns the given <see cref="IList{T}"/> when sorted.</returns>
        public static IList<KeyValuePair<TKey, int>> CountingSortDescendingValues<TKey>(this IList<KeyValuePair<TKey, int>> list)
        {
            if (list.Count == 0) return list;

            return CountingSortDescendingValues(list, 0, list.Count);
        }

        /// <summary>
        /// Sorts a range of key-value pairs in the <see cref="IList{T}"/> by their value in ascending order. The difference between the minimun and maximum key must be lower than (<see cref="int.MaxValue"/> / 4).
        /// </summary>
        /// <typeparam name="TKey">The data type of the keys in the list of <see cref="KeyValuePair{TKey, TValue}"/> items.</typeparam>
        /// <param name="list">The <see cref="IList{T}"/> containing the elements for sorting.</param>
        /// <param name="index">The zero-based starting index of the range for sorting.</param>
        /// <param name="count">The length of the range for sorting.</param>
        /// <returns>Returns the given <see cref="IList{T}"/> when sorted.</returns>
        public static IList<KeyValuePair<TKey, int>> CountingSortValues<TKey>(this IList<KeyValuePair<TKey, int>> list, int index, int count)
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
            if (rangeOfItems >= int.MaxValue / 4) throw new InvalidOperationException("Range of elements is too big for this sorting algorithm!");

            // Array for saving the number of items
            int[] counts = new int[rangeOfItems];// throws OutOfMemoryException if range is too big and memory is not enought
            // Array for copying sorted elements
            var copyArray = new KeyValuePair<TKey, int>[count];

            // Count the numbers
            for (int i = index; i < lastIndex; i++)
            {
                counts[list[i].Value - min] += 1;
            }
            // Calculating the starting index of each key by updating counts array
            // so each index is storing the sum till the previous step
            for (int i = 1; i < rangeOfItems; i++)
            {
                counts[i] += counts[i - 1];
            }
            // Copying sorted elements from the list in the copyArray preserving order of equal elements
            for (int i = lastIndex - 1; i >= index; i--)
            {
                copyArray[--counts[list[i].Value - min]] = list[i];
            }
            // copyArray index
            int j = 0;
            // Copy elements from the copyArray to the list
            for (int i = index; i < lastIndex; i++)
            {
                list[i] = copyArray[j++];
            }

            return list;
        }

        /// <summary>
        /// Sorts a range of key-value pairs in the <see cref="IList{T}"/> by their value in descending order. The difference between the minimun and maximum key must be lower than (<see cref="int.MaxValue"/> / 4).
        /// </summary>
        /// <typeparam name="TKey">The data type of the keys in the list of <see cref="KeyValuePair{TKey, TValue}"/> items.</typeparam>
        /// <param name="list">The <see cref="IList{T}"/> containing the elements for sorting.</param>
        /// <param name="index">The zero-based starting index of the range for sorting.</param>
        /// <param name="count">The length of the range for sorting.</param>
        /// <returns>Returns the given <see cref="IList{T}"/> when sorted.</returns>
        public static IList<KeyValuePair<TKey, int>> CountingSortDescendingValues<TKey>(this IList<KeyValuePair<TKey, int>> list, int index, int count)
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
            if (rangeOfItems >= int.MaxValue / 4) throw new InvalidOperationException("Range of elements is too big for this sorting algorithm!");

            // Array for saving the number of items
            int[] counts = new int[rangeOfItems];// throws OutOfMemoryException if range is too big and memory is not enought
            // Array for copying sorted elements
            var copyArray = new KeyValuePair<TKey, int>[count];

            // Count the numbers
            for (int i = index; i < lastIndex; i++)
            {
                counts[list[i].Value - min] += 1;
            }
            // Calculating the starting index of each key by updating counts array
            // so each index is storing the sum till the previous step
            for (int i = 1; i < rangeOfItems; i++)
            {
                counts[i] += counts[i - 1];
            }
            // Copying sorted elements from the list in the copyArray preserving order of equal elements
            for (int i = index; i < lastIndex; i++)
            {
                copyArray[--counts[list[i].Value - min]] = list[i];
            }
            // copyArray index
            int j = count - 1;
            // Copy elements from the copyArray to the list
            for (int i = index; i < lastIndex; i++)
            {
                list[i] = copyArray[j--];
            }

            return list;
        }
    }
}
