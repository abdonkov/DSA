using System;
using System.Collections.Generic;

namespace DSA.Algorithms.Sorting
{
    /// <summary>
    /// Static class containing extension methods for quick3 sort.
    /// </summary>
    public static class Quick3Sorter
    {
        /// <summary>
        /// Sorts the elements in the entire <see cref="IList{T}"/> in ascending order using the default comparer.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="IList{T}"/>.</typeparam>
        /// <param name="list">The <see cref="IList{T}"/> containing the elements for sorting.</param>
        /// <returns>Returns the given <see cref="IList{T}"/> when sorted.</returns>
        public static IList<T> Quick3Sort<T>(this IList<T> list)
        {
            if (list.Count == 0) return list;

            return Quick3Sort(list, Comparer<T>.Default);
        }

        /// <summary>
        /// Sorts the elements in the entire <see cref="IList{T}"/> in descending order using the default comparer.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="IList{T}"/>.</typeparam>
        /// <param name="list">The <see cref="IList{T}"/> containing the elements for sorting.</param>
        /// <returns>Returns the given <see cref="IList{T}"/> when sorted.</returns>
        public static IList<T> Quick3SortDescending<T>(this IList<T> list)
        {
            if (list.Count == 0) return list;

            return Quick3SortDescending(list, Comparer<T>.Default);
        }

        /// <summary>
        /// Sorts the elements in the entire <see cref="IList{T}"/> in ascending order using the specified comparison.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="IList{T}"/>.</typeparam>
        /// <param name="list">The <see cref="IList{T}"/> containing the elements for sorting.</param>
        /// <param name="comparison">The <see cref="Comparison{T}"/> used for comparing the elements.</param>
        /// <returns>Returns the given <see cref="IList{T}"/> when sorted.</returns>
        public static IList<T> Quick3Sort<T>(this IList<T> list, Comparison<T> comparison)
        {
            if (comparison == null) throw new ArgumentNullException(nameof(comparison));

            if (list.Count == 0) return list;

            return Quick3Sort(list, Comparer<T>.Create(comparison));
        }

        /// <summary>
        /// Sorts the elements in the entire <see cref="IList{T}"/> in descending order using the specified comparison.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="IList{T}"/>.</typeparam>
        /// <param name="list">The <see cref="IList{T}"/> containing the elements for sorting.</param>
        /// <param name="comparison">The <see cref="Comparison{T}"/> used for comparing the elements.</param>
        /// <returns>Returns the given <see cref="IList{T}"/> when sorted.</returns>
        public static IList<T> Quick3SortDescending<T>(this IList<T> list, Comparison<T> comparison)
        {
            if (comparison == null) throw new ArgumentNullException(nameof(comparison));

            if (list.Count == 0) return list;

            return Quick3SortDescending(list, Comparer<T>.Create(comparison));
        }

        /// <summary>
        /// Sorts the elements in the entire <see cref="IList{T}"/> in ascending order using the specified comparer.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="IList{T}"/>.</typeparam>
        /// <param name="list">The <see cref="IList{T}"/> containing the elements for sorting.</param>
        /// <param name="comparer">The <see cref="IComparable{T}"/> implementation used for comparing the elements,
        /// or null to use the default comparer <see cref="Comparer{T}.Default"/>.</param>
        /// <returns>Returns the given <see cref="IList{T}"/> when sorted.</returns>
        public static IList<T> Quick3Sort<T>(this IList<T> list, IComparer<T> comparer)
        {
            if (list.Count == 0) return list;

            return Quick3Sort(list, 0, list.Count, comparer);
        }

        /// <summary>
        /// Sorts the elements in the entire <see cref="IList{T}"/> in descending order using the specified comparer.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="IList{T}"/>.</typeparam>
        /// <param name="list">The <see cref="IList{T}"/> containing the elements for sorting.</param>
        /// <param name="comparer">The <see cref="IComparable{T}"/> implementation used for comparing the elements,
        /// or null to use the default comparer <see cref="Comparer{T}.Default"/>.</param>
        /// <returns>Returns the given <see cref="IList{T}"/> when sorted.</returns>
        public static IList<T> Quick3SortDescending<T>(this IList<T> list, IComparer<T> comparer)
        {
            if (list.Count == 0) return list;

            return Quick3SortDescending(list, 0, list.Count, comparer);
        }

        /// <summary>
        /// Sorts a range of elements in the <see cref="IList{T}"/> in ascending order using the specified comparer.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="IList{T}"/>.</typeparam>
        /// <param name="list">The <see cref="IList{T}"/> containing the elements for sorting.</param>
        /// <param name="index">The zero-based starting index of the range for sorting.</param>
        /// <param name="count">The length of the range for sorting.</param>
        /// <param name="comparer">The <see cref="IComparable{T}"/> implementation used for comparing the elements,
        /// or null to use the default comparer <see cref="Comparer{T}.Default"/>.</param>
        /// <returns>Returns the given <see cref="IList{T}"/> when sorted.</returns>
        public static IList<T> Quick3Sort<T>(this IList<T> list, int index, int count, IComparer<T> comparer)
        {
            if (index < 0 || index >= list.Count) throw new ArgumentOutOfRangeException(nameof(index));
            if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));
            if (index + count > list.Count) throw new ArgumentException("Invalid length specified.");

            if (comparer == null) comparer = Comparer<T>.Default;

            // Quick sort recursion require inclusive index of last elements
            // and the last element is index + count - 1
            Quick3SortRecursion(list, index, index + count - 1, comparer);

            return list;
        }

        /// <summary>
        /// Recursively sorting the range of items in the given <see cref="IList{T}"/>. Used for ascending sort.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="IList{T}"/>.</typeparam>
        /// <param name="list">The <see cref="IList{T}"/> containing the elements for sorting.</param>
        /// <param name="leftIndex">The zero-based inclusive starting index of the range for partitioning.</param>
        /// <param name="rightIndex">The zero-based inclusive ending index of the range for partitioning.</param>
        /// <param name="comparer">The <see cref="IComparable{T}"/> implementation used for comparing the elements.</param>
        private static void Quick3SortRecursion<T>(IList<T> list, int leftIndex, int rightIndex, IComparer<T> comparer)
        {
            while (leftIndex < rightIndex)// if we have 1 element or less it is considered sorted
            {
                // Partition the elements and get the pivot position

                // Get the middle element as pivot. Better performance on nearly sorted collections.
                int pivotIndex = leftIndex + (rightIndex - leftIndex) / 2;// no overflow
                // Swap pivot position with last element position. Easier partitioning.
                T pivot = list[pivotIndex];
                list[pivotIndex] = list[rightIndex];
                list[rightIndex] = pivot;
                // Indexes of the elements to swap with. One bigger because
                int leftSwapIndex = leftIndex - 1;
                int rightSwapIndex = rightIndex;
                // Indexes for traversel
                int i = leftIndex - 1;
                int j = rightIndex;
                for (;;)
                {
                    // go to the first element on the left side which is bigger or equal to the pivot
                    int leftCompare = comparer.Compare(list[++i], pivot);
                    while (leftCompare < 0) leftCompare = comparer.Compare(list[++i], pivot);
                    // go to the first element on the right side which is smaller or equal to the pivot
                    int rightCompare = comparer.Compare(list[--j], pivot);
                    while (rightCompare > 0)
                    {
                        if (j == leftIndex) break;// break if at the end
                        rightCompare = comparer.Compare(list[--j], pivot);
                    }
                    // if the left position surpassed the right position we break the loop
                    if (i >= j) break;
                    // swap the >= element on the left with the <= element on the right
                    SwapListElements(list, i, j);
                    // if an element is equal to the pivot swap it with the element on the swap position
                    // Note: rightCompare saved the comparision for the now left element because of the swap and vice versa
                    if (rightCompare == 0) SwapListElements(list, ++leftSwapIndex, i);
                    if (leftCompare == 0) SwapListElements(list, --rightSwapIndex, j);
                }
                // After the elements are rearranged we have a structure like this one:
                // | equal to pivot | less than pivot | greater than pivot | equal to pivot | pivot |
                // so we put the pivot on the spot of the first greater element to the pivot on the right side
                SwapListElements(list, i, rightIndex);
                // set j to last less element(before pivot) and i to first greater element(after pivot) on right side
                j = i - 1;
                i = i + 1;
                // move equal elements on the left side to the middle
                for (int k = leftIndex; k < leftSwapIndex; k++, j--) SwapListElements(list, k, j);
                // move equal elements on the right side to the middle
                for (int l = rightIndex - 1; l > rightSwapIndex; l--, i++) SwapListElements(list, l, i);

                // Recursive sort for the smaller partionion. Ensures maximum recursion level is log(n).
                // Note: i is the index of the first greater than pivot element and j is the index of last less than pivot element
                if (j - leftIndex < rightIndex - i)
                {
                    Quick3SortRecursion(list, leftIndex, j, comparer);
                    leftIndex = i;
                }
                else
                {
                    Quick3SortRecursion(list, i, rightIndex, comparer);
                    rightIndex = j;
                }
            }
        }

        /// <summary>
        /// Sorts a range of elements in the <see cref="IList{T}"/> in descending order using the specified comparer.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="IList{T}"/>.</typeparam>
        /// <param name="list">The <see cref="IList{T}"/> containing the elements for sorting.</param>
        /// <param name="index">The zero-based starting index of the range for sorting.</param>
        /// <param name="count">The length of the range for sorting.</param>
        /// <param name="comparer">The <see cref="IComparable{T}"/> implementation used for comparing the elements,
        /// or null to use the default comparer <see cref="Comparer{T}.Default"/>.</param>
        /// <returns>Returns the given <see cref="IList{T}"/> when sorted.</returns>
        public static IList<T> Quick3SortDescending<T>(this IList<T> list, int index, int count, IComparer<T> comparer)
        {
            if (index < 0 || index >= list.Count) throw new ArgumentOutOfRangeException(nameof(index));
            if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));
            if (index + count > list.Count) throw new ArgumentException("Invalid length specified.");

            if (comparer == null) comparer = Comparer<T>.Default;

            // Quick sort recursion require inclusive index of last elements
            // and the last element is index + count - 1
            Quick3SortRecursionDescending(list, index, index + count - 1, comparer);

            return list;
        }

        /// <summary>
        /// Recursively sorting the range of items in the given <see cref="IList{T}"/>. Used for descending sort.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="IList{T}"/>.</typeparam>
        /// <param name="list">The <see cref="IList{T}"/> containing the elements for sorting.</param>
        /// <param name="leftIndex">The zero-based inclusive starting index of the range for partitioning.</param>
        /// <param name="rightIndex">The zero-based inclusive ending index of the range for partitioning.</param>
        /// <param name="comparer">The <see cref="IComparable{T}"/> implementation used for comparing the elements.</param>
        private static void Quick3SortRecursionDescending<T>(IList<T> list, int leftIndex, int rightIndex, IComparer<T> comparer)
        {
            while (leftIndex < rightIndex)// if we have 1 element or less it is considered sorted
            {
                // Partition the elements and get the pivot position

                // Get the middle element as pivot. Better performance on nearly sorted collections.
                int pivotIndex = leftIndex + (rightIndex - leftIndex) / 2;// no overflow
                // Swap pivot position with last element position. Easier partitioning.
                T pivot = list[pivotIndex];
                list[pivotIndex] = list[rightIndex];
                list[rightIndex] = pivot;
                // Indexes of the elements to swap with. One bigger because
                int leftSwapIndex = leftIndex - 1;
                int rightSwapIndex = rightIndex;
                // Indexes for traversel
                int i = leftIndex - 1;
                int j = rightIndex;
                for (;;)
                {
                    // go to the first element on the left side which is smaller or equal to the pivot
                    int leftCompare = comparer.Compare(list[++i], pivot);
                    while (leftCompare > 0) leftCompare = comparer.Compare(list[++i], pivot);
                    // go to the first element on the right side which is bigger or equal to the pivot
                    int rightCompare = comparer.Compare(list[--j], pivot);
                    while (rightCompare < 0)
                    {
                        if (j == leftIndex) break;// break if at the end
                        rightCompare = comparer.Compare(list[--j], pivot);
                    }
                    // if the left position surpassed the right position we break the loop
                    if (i >= j) break;
                    // swap the <= element on the left with the >= element on the right
                    SwapListElements(list, i, j);
                    // if an element is equal to the pivot swap it with the element on the swap position
                    // Note: rightCompare saved the comparision for the now left element because of the swap and vice versa
                    if (rightCompare == 0) SwapListElements(list, ++leftSwapIndex, i);
                    if (leftCompare == 0) SwapListElements(list, --rightSwapIndex, j);
                }
                // After the elements are rearranged we have a structure like this one:
                // | equal to pivot | greater than pivot | less than pivot | equal to pivot | pivot |
                // so we put the pivot on the spot of the first less element to the pivot on the right side
                SwapListElements(list, i, rightIndex);
                // set j to last greater element(before pivot) and i to first less element(after pivot) on right side
                j = i - 1;
                i = i + 1;
                // move equal elements on the left side to the middle
                for (int k = leftIndex; k < leftSwapIndex; k++, j--) SwapListElements(list, k, j);
                // move equal elements on the right side to the middle
                for (int l = rightIndex - 1; l > rightSwapIndex; l--, i++) SwapListElements(list, l, i);

                // Recursive sort for the smaller partionion. Ensures maximum recursion level is log(n).
                // Note: i is the index of the first less than pivot element and j is the index of last greater than pivot element
                if (j - leftIndex < rightIndex - i)
                {
                    Quick3SortRecursionDescending(list, leftIndex, j, comparer);
                    leftIndex = i;
                }
                else
                {
                    Quick3SortRecursionDescending(list, i, rightIndex, comparer);
                    rightIndex = j;
                }
            }
        }

        /// <summary>
        /// Swaps the elements on the given indexes in the given <see cref="IList{T}"/>.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="IList{T}"/>.</typeparam>
        /// <param name="list">The <see cref="IList{T}"/> containing the elements for swapping.</param>
        /// <param name="firstElementIndex">The zero-based index of the first element for swapping.</param>
        /// <param name="secondElementIndex">The zero-based index of the second element for swapping.</param>
        private static void SwapListElements<T>(IList<T> list, int firstElementIndex, int secondElementIndex)
        {
            T temp = list[firstElementIndex];
            list[firstElementIndex] = list[secondElementIndex];
            list[secondElementIndex] = temp;
        }
    }
}
