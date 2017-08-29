using System;
using System.Collections.Generic;

namespace DSA.Algorithms.Searching
{
    /// <summary>
    /// A static class providing methods for linear search.
    /// </summary>
    public static partial class LinearSearcher
    {
        /// <summary>
        /// Searches the entire <see cref="IList{T}"/> for an item using the default comparer and returns the zero-based index of the first occurrence.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="IList{T}"/>.</typeparam>
        /// <param name="list">The <see cref="IList{T}"/> containing the items for searching.</param>
        /// <param name="item">The item to search for.</param>
        /// <returns>The zero-based index of the first occurrence of the item in the <see cref="IList{T}"/>, if the item is found; otherwise -1.</returns>
        public static int LinearSearchFirstIndexOf<T>(this IList<T> list, T item)
        {
            if (list.Count == 0) return ~0;

            return LinearSearchFirstIndexOf(list, item, Comparer<T>.Default);
        }

        /// <summary>
        /// Searches the entire <see cref="IList{T}"/> for an item using the specified comparison and returns the zero-based index of the first occurrence.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="IList{T}"/>.</typeparam>
        /// <param name="list">The <see cref="IList{T}"/> containing the items for searching.</param>
        /// <param name="item">The item to search for.</param>
        /// <param name="comparison">The <see cref="Comparison{T}"/> used for comparing the elements.</param>
        /// <returns>The zero-based index of the first occurrence of the item in the <see cref="IList{T}"/>, if the item is found; otherwise -1.</returns>
        public static int LinearSearchFirstIndexOf<T>(this IList<T> list, T item, Comparison<T> comparison)
        {
            if (list.Count == 0) return ~0;

            return LinearSearchFirstIndexOf(list, item, Comparer<T>.Create(comparison));
        }

        /// <summary>
        /// Searches the entire <see cref="IList{T}"/> for an item using the specified comparer and returns the zero-based index of the first occurrence.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="IList{T}"/>.</typeparam>
        /// <param name="list">The <see cref="IList{T}"/> containing the items for searching.</param>
        /// <param name="item">The item to search for.</param>
        /// <param name="comparer">The <see cref="IComparable{T}"/> implementation used for comparing the elements,
        /// or null to use the default comparer <see cref="Comparer{T}.Default"/>.</param>
        /// <returns>The zero-based index of the first occurrence of the item in the <see cref="IList{T}"/>, if the item is found; otherwise -1.</returns>
        public static int LinearSearchFirstIndexOf<T>(this IList<T> list, T item, IComparer<T> comparer)
        {
            if (list.Count == 0) return ~0;

            return LinearSearchFirstIndexOf(list, item, 0, list.Count, comparer);
        }

        /// <summary>
        /// Searches a range of items in the <see cref="IList{T}"/> for an item using the specified comparer and returns the zero-based index of the first occurrence.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="IList{T}"/>.</typeparam>
        /// <param name="item">The item to search for.</param>
        /// <param name="list">The <see cref="IList{T}"/> containing the items for searching.</param>
        /// <param name="index">The zero-based starting index of the range for searching.</param>
        /// <param name="count">The length of the range for searching.</param>
        /// <param name="comparer">The <see cref="IComparable{T}"/> implementation used for comparing the elements,
        /// or null to use the default comparer <see cref="Comparer{T}.Default"/>.</param>
        /// <returns>The zero-based index of the first occurrence of the item in the <see cref="IList{T}"/>, if the item is found; otherwise -1.</returns>
        public static int LinearSearchFirstIndexOf<T>(this IList<T> list, T item, int index, int count, IComparer<T> comparer)
        {
            if (index < 0 || index >= list.Count) throw new ArgumentOutOfRangeException(nameof(index));
            if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));
            if (index + count > list.Count) throw new ArgumentException("Invalid length specified.");

            if (comparer == null) comparer = Comparer<T>.Default;

            int lastIndex = index + count - 1;
            for (int i = index; i <= lastIndex ; i++)
            {
                if (comparer.Compare(list[i], item) == 0)
                    return i;
            }

            return -1;
        }

        /// <summary>
        /// Searches the entire <see cref="IList{T}"/> for an item using the default comparer and returns the zero-based index of the last occurrence.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="IList{T}"/>.</typeparam>
        /// <param name="list">The <see cref="IList{T}"/> containing the items for searching.</param>
        /// <param name="item">The item to search for.</param>
        /// <returns>The zero-based index of the last occurrence of the item in the <see cref="IList{T}"/>, if the item is found; otherwise -1.</returns>
        public static int LinearSearchLastIndexOf<T>(this IList<T> list, T item)
        {
            if (list.Count == 0) return ~0;

            return LinearSearchLastIndexOf(list, item, Comparer<T>.Default);
        }

        /// <summary>
        /// Searches the entire <see cref="IList{T}"/> for an item using the specified comparison and returns the zero-based index of the last occurrence.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="IList{T}"/>.</typeparam>
        /// <param name="list">The <see cref="IList{T}"/> containing the items for searching.</param>
        /// <param name="item">The item to search for.</param>
        /// <param name="comparison">The <see cref="Comparison{T}"/> used for comparing the elements.</param>
        /// <returns>The zero-based index of the last occurrence of the item in the <see cref="IList{T}"/>, if the item is found; otherwise -1.</returns>
        public static int LinearSearchLastIndexOf<T>(this IList<T> list, T item, Comparison<T> comparison)
        {
            if (list.Count == 0) return ~0;

            return LinearSearchLastIndexOf(list, item, Comparer<T>.Create(comparison));
        }

        /// <summary>
        /// Searches the entire <see cref="IList{T}"/> for an item using the specified comparer and returns the zero-based index of the last occurrence.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="IList{T}"/>.</typeparam>
        /// <param name="list">The <see cref="IList{T}"/> containing the items for searching.</param>
        /// <param name="item">The item to search for.</param>
        /// <param name="comparer">The <see cref="IComparable{T}"/> implementation used for comparing the elements,
        /// or null to use the default comparer <see cref="Comparer{T}.Default"/>.</param>
        /// <returns>The zero-based index of the last occurrence of the item in the <see cref="IList{T}"/>, if the item is found; otherwise -1.</returns>
        public static int LinearSearchLastIndexOf<T>(this IList<T> list, T item, IComparer<T> comparer)
        {
            if (list.Count == 0) return ~0;

            return LinearSearchLastIndexOf(list, item, 0, list.Count, comparer);
        }

        /// <summary>
        /// Searches a range of items in the <see cref="IList{T}"/> for an item using the specified comparer and returns the zero-based index of the last occurrence.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="IList{T}"/>.</typeparam>
        /// <param name="item">The item to search for.</param>
        /// <param name="list">The <see cref="IList{T}"/> containing the items for searching.</param>
        /// <param name="index">The zero-based starting index of the range for searching.</param>
        /// <param name="count">The length of the range for searching.</param>
        /// <param name="comparer">The <see cref="IComparable{T}"/> implementation used for comparing the elements,
        /// or null to use the default comparer <see cref="Comparer{T}.Default"/>.</param>
        /// <returns>The zero-based index of the last occurrence of the item in the <see cref="IList{T}"/>, if the item is found; otherwise -1.</returns>
        public static int LinearSearchLastIndexOf<T>(this IList<T> list, T item, int index, int count, IComparer<T> comparer)
        {
            if (index < 0 || index >= list.Count) throw new ArgumentOutOfRangeException(nameof(index));
            if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));
            if (index + count > list.Count) throw new ArgumentException("Invalid length specified.");

            if (comparer == null) comparer = Comparer<T>.Default;

            int lastIndex = index + count - 1;
            for (int i = lastIndex; i >= index; i--)
            {
                if (comparer.Compare(list[i], item) == 0)
                    return i;
            }

            return -1;
        }
    }
}
