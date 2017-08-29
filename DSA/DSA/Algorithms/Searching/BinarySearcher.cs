using System;
using System.Collections.Generic;

namespace DSA.Algorithms.Searching
{
    /// <summary>
    /// A static class providing methods for binary search.
    /// </summary>
    public static class BinarySearcher
    {
        /// <summary>
        /// Searches the entire sorted <see cref="IList{T}"/> for an item using the default comparer and returns the zero-based index of the first equal item.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="IList{T}"/>.</typeparam>
        /// <param name="list">The sorted <see cref="IList{T}"/> containing the items for searching.</param>
        /// <param name="item">The item to search for.</param>
        /// <returns>The zero-based index of the first equal item in the sorted <see cref="IList{T}"/>, if the item is found; otherwise, a negative number
        /// that is the bitwise complement of the index of the next element that is larger than item or, if there is no larger element,
        /// the bitwise complement of the items count.</returns>
        public static int BinarySearchFirstIndexOf<T>(this IList<T> list, T item)
        {
            if (list.Count == 0) return ~0;

            return BinarySearchFirstIndexOf(list, item, Comparer<T>.Default);
        }

        /// <summary>
        /// Searches the entire sorted <see cref="IList{T}"/> for an item using the specified comparison and returns the zero-based index of the first equal item.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="IList{T}"/>.</typeparam>
        /// <param name="list">The sorted <see cref="IList{T}"/> containing the items for searching.</param>
        /// <param name="item">The item to search for.</param>
        /// <param name="comparison">The <see cref="Comparison{T}"/> used for comparing the elements.</param>
        /// <returns>The zero-based index of the first equal item in the sorted <see cref="IList{T}"/>, if the item is found; otherwise, a negative number
        /// that is the bitwise complement of the index of the next element that is larger than item or, if there is no larger element,
        /// the bitwise complement of the items count.</returns>
        public static int BinarySearchFirstIndexOf<T>(this IList<T> list, T item, Comparison<T> comparison)
        {
            if (list.Count == 0) return ~0;

            return BinarySearchFirstIndexOf(list, item, Comparer<T>.Create(comparison));
        }

        /// <summary>
        /// Searches the entire sorted <see cref="IList{T}"/> for an item using the specified comparer and returns the zero-based index of the first equal item.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="IList{T}"/>.</typeparam>
        /// <param name="list">The sorted <see cref="IList{T}"/> containing the items for searching.</param>
        /// <param name="item">The item to search for.</param>
        /// <param name="comparer">The <see cref="IComparable{T}"/> implementation used for comparing the elements,
        /// or null to use the default comparer <see cref="Comparer{T}.Default"/>.</param>
        /// <returns>The zero-based index of the first equal item in the sorted <see cref="IList{T}"/>, if the item is found; otherwise, a negative number
        /// that is the bitwise complement of the index of the next element that is larger than item or, if there is no larger element,
        /// the bitwise complement of the items count.</returns>
        public static int BinarySearchFirstIndexOf<T>(this IList<T> list, T item, IComparer<T> comparer)
        {
            if (list.Count == 0) return ~0;

            return BinarySearchFirstIndexOf(list, item, 0, list.Count, comparer);
        }

        /// <summary>
        /// Searches a range of items in the sorted <see cref="IList{T}"/> for an item using the specified comparer and returns the zero-based index of the first equal item.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="IList{T}"/>.</typeparam>
        /// <param name="item">The item to search for.</param>
        /// <param name="list">The sorted <see cref="IList{T}"/> containing the items for searching.</param>
        /// <param name="index">The zero-based starting index of the range for searching.</param>
        /// <param name="count">The length of the range for searching.</param>
        /// <param name="comparer">The <see cref="IComparable{T}"/> implementation used for comparing the elements,
        /// or null to use the default comparer <see cref="Comparer{T}.Default"/>.</param>
        /// <returns>The zero-based index of the first equal item in the sorted <see cref="IList{T}"/>, if the item is found; otherwise, a negative number
        /// that is the bitwise complement of the index of the next element that is larger than item or, if there is no larger element,
        /// the bitwise complement of the items count.</returns>
        public static int BinarySearchFirstIndexOf<T>(this IList<T> list, T item, int index, int count, IComparer<T> comparer)
        {
            if (index < 0 || index >= list.Count) throw new ArgumentOutOfRangeException(nameof(index));
            if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));
            if (index + count > list.Count) throw new ArgumentException("Invalid length specified.");

            if (comparer == null) comparer = Comparer<T>.Default;

            // Define lower and upper bounds of the range
            int lower = index;
            int upper = index + count - 1;

            while (lower <= upper)
            {
                // Calculate middle point. No overflow.
                int middle = lower + ((upper - lower) >> 1);
                // Compare list item with the searched item
                int cmp = comparer.Compare(item, list[middle]);
                
                // if searched item is equal to the current
                if (cmp == 0)
                {
                    // If first equal item in range for searching, return it.
                    if (middle == index) return middle;

                    // Compare it with previous item
                    if (comparer.Compare(list[middle], list[middle - 1]) == 0) // if previous item is equal to current
                        upper = middle - 1; // go to the lower part of the list
                    else return middle;// if previous is not equal to current. The current is the first item equal to the searched one
                }
                else if (cmp < 0) // if searched item is smaller
                    upper = middle - 1; // go to the lower part of the list
                else // if searched item is bigger
                    lower = middle + 1; // go to the upper part of the list
            }

            // If the item was not found. The lower parameter is the index of the first item
            // higher that the searched one. If no such item - the count of the items.
            // So we return its bitwise compliment.
            return ~lower;
        }

        /// <summary>
        /// Searches the entire sorted <see cref="IList{T}"/> for an item using the default comparer and returns the zero-based index of the last equal item.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="IList{T}"/>.</typeparam>
        /// <param name="list">The sorted <see cref="IList{T}"/> containing the items for searching.</param>
        /// <param name="item">The item to search for.</param>
        /// <returns>The zero-based index of the last equal item in the sorted <see cref="IList{T}"/>, if the item is found; otherwise, a negative number
        /// that is the bitwise complement of the index of the next element that is larger than item or, if there is no larger element,
        /// the bitwise complement of the items count.</returns>
        public static int BinarySearchLastIndexOf<T>(this IList<T> list, T item)
        {
            if (list.Count == 0) return ~0;

            return BinarySearchLastIndexOf(list, item, Comparer<T>.Default);
        }

        /// <summary>
        /// Searches the entire sorted <see cref="IList{T}"/> for an item using the specified comparison and returns the zero-based index of the last equal item.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="IList{T}"/>.</typeparam>
        /// <param name="list">The sorted <see cref="IList{T}"/> containing the items for searching.</param>
        /// <param name="item">The item to search for.</param>
        /// <param name="comparison">The <see cref="Comparison{T}"/> used for comparing the elements.</param>
        /// <returns>The zero-based index of the last equal item in the sorted <see cref="IList{T}"/>, if the item is found; otherwise, a negative number
        /// that is the bitwise complement of the index of the next element that is larger than item or, if there is no larger element,
        /// the bitwise complement of the items count.</returns>
        public static int BinarySearchLastIndexOf<T>(this IList<T> list, T item, Comparison<T> comparison)
        {
            if (list.Count == 0) return ~0;

            return BinarySearchLastIndexOf(list, item, Comparer<T>.Create(comparison));
        }

        /// <summary>
        /// Searches the entire sorted <see cref="IList{T}"/> for an item using the specified comparer and returns the zero-based index of the last equal item.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="IList{T}"/>.</typeparam>
        /// <param name="list">The sorted <see cref="IList{T}"/> containing the items for searching.</param>
        /// <param name="item">The item to search for.</param>
        /// <param name="comparer">The <see cref="IComparable{T}"/> implementation used for comparing the elements,
        /// or null to use the default comparer <see cref="Comparer{T}.Default"/>.</param>
        /// <returns>The zero-based index of the last equal item in the sorted <see cref="IList{T}"/>, if the item is found; otherwise, a negative number
        /// that is the bitwise complement of the index of the next element that is larger than item or, if there is no larger element,
        /// the bitwise complement of the items count.</returns>
        public static int BinarySearchLastIndexOf<T>(this IList<T> list, T item, IComparer<T> comparer)
        {
            if (list.Count == 0) return ~0;

            return BinarySearchLastIndexOf(list, item, 0, list.Count, comparer);
        }

        /// <summary>
        /// Searches a range of items in the sorted <see cref="IList{T}"/> for an item using the specified comparer and returns the zero-based index of the last equal item.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="IList{T}"/>.</typeparam>
        /// <param name="item">The item to search for.</param>
        /// <param name="list">The sorted <see cref="IList{T}"/> containing the items for searching.</param>
        /// <param name="index">The zero-based starting index of the range for searching.</param>
        /// <param name="count">The length of the range for searching.</param>
        /// <param name="comparer">The <see cref="IComparable{T}"/> implementation used for comparing the elements,
        /// or null to use the default comparer <see cref="Comparer{T}.Default"/>.</param>
        /// <returns>The zero-based index of the last equal item in the sorted <see cref="IList{T}"/>, if the item is found; otherwise, a negative number
        /// that is the bitwise complement of the index of the next element that is larger than item or, if there is no larger element,
        /// the bitwise complement of the items count.</returns>
        public static int BinarySearchLastIndexOf<T>(this IList<T> list, T item, int index, int count, IComparer<T> comparer)
        {
            if (index < 0 || index >= list.Count) throw new ArgumentOutOfRangeException(nameof(index));
            if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));
            if (index + count > list.Count) throw new ArgumentException("Invalid length specified.");

            if (comparer == null) comparer = Comparer<T>.Default;

            // Define lower and upper bounds of the range
            int lower = index;
            int upper = index + count - 1;

            while (lower <= upper)
            {
                // Calculate middle point. No overflow.
                int middle = lower + ((upper - lower) >> 1);
                // Compare list item with the searched item
                int cmp = comparer.Compare(item, list[middle]);

                // if searched item is equal to the current
                if (cmp == 0)
                {
                    // If last equal item in range for searching, return it.
                    if (middle == index + count - 1) return middle;

                    // Compare it with next item
                    if (comparer.Compare(list[middle], list[middle + 1]) == 0) // if next item is equal to current
                        lower = middle + 1; // go to the upper part of the list
                    else return middle; // if next is not equal to current. The current is the last item equal to the searched one
                }
                else if (cmp < 0) // if searched item is smaller
                    upper = middle - 1; // go to the lower part of the list
                else // if searched item is bigger
                    lower = middle + 1; // go to the upper part of the list
            }

            // If the item was not found. The lower parameter is the index of the first item
            // higher that the searched one. If no such item - the count of the items.
            // So we return its bitwise compliment.
            return ~lower;
        }
    }
}
