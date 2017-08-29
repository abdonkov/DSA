using System;
using System.Collections.Generic;

namespace DSA.Algorithms.Searching
{
    /// <summary>
    /// A static class providing methods for quick select.
    /// </summary>
    public static class QuickSelector
    {
        #region Not in-place quick select

        /// <summary>
        /// Returns N-th smallest item of the entire <see cref="IList{T}"/> using the default comparer without modifying list.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="IList{T}"/>.</typeparam>
        /// <param name="list">The <see cref="IList{T}"/> containing the items for searching.</param>
        /// <param name="n">The N-th smallest item to search for.</param>
        /// <returns>The N-th smallest item in the entire <see cref="IList{T}"/>.</returns>
        public static T QuickSelectSmallest<T>(this IList<T> list, int n)
        {
            return QuickSelectSmallest(list, n, Comparer<T>.Default);
        }

        /// <summary>
        /// Returns N-th biggest item of the entire <see cref="IList{T}"/> using the default comparer without modifying list.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="IList{T}"/>.</typeparam>
        /// <param name="list">The <see cref="IList{T}"/> containing the items for searching.</param>
        /// <param name="n">The N-th biggest item to search for.</param>
        /// <returns>The N-th biggest item in the entire <see cref="IList{T}"/>.</returns>
        public static T QuickSelectBiggest<T>(this IList<T> list, int n)
        {
            return QuickSelectBiggest(list, n, Comparer<T>.Default);
        }

        /// <summary>
        /// Returns N-th smallest item of the entire <see cref="IList{T}"/> using the default comparer without modifying list.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="IList{T}"/>.</typeparam>
        /// <param name="list">The <see cref="IList{T}"/> containing the items for searching.</param>
        /// <param name="n">The N-th smallest item to search for.</param>
        /// <param name="comparison">The <see cref="Comparison{T}"/> used for comparing the elements.</param>
        /// <returns>The N-th smallest item in the entire <see cref="IList{T}"/>.</returns>
        public static T QuickSelectSmallest<T>(this IList<T> list, int n, Comparison<T> comparison)
        {
            return QuickSelectSmallest(list, n, Comparer<T>.Create(comparison));
        }

        /// <summary>
        /// Returns N-th biggest item of the entire <see cref="IList{T}"/> using the default comparer without modifying list.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="IList{T}"/>.</typeparam>
        /// <param name="list">The <see cref="IList{T}"/> containing the items for searching.</param>
        /// <param name="n">The N-th biggest item to search for.</param>
        /// <param name="comparison">The <see cref="Comparison{T}"/> used for comparing the elements.</param>
        /// <returns>The N-th biggest item in the entire <see cref="IList{T}"/>.</returns>
        public static T QuickSelectBiggest<T>(this IList<T> list, int n, Comparison<T> comparison)
        {
            return QuickSelectBiggest(list, n, Comparer<T>.Create(comparison));
        }

        /// <summary>
        /// Returns N-th smallest item of the entire <see cref="IList{T}"/> using the default comparer without modifying list.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="IList{T}"/>.</typeparam>
        /// <param name="list">The <see cref="IList{T}"/> containing the items for searching.</param>
        /// <param name="n">The N-th smallest item to search for.</param>
        /// <param name="comparer">The <see cref="IComparable{T}"/> implementation used for comparing the elements,
        /// or null to use the default comparer <see cref="Comparer{T}.Default"/>.</param>
        /// <returns>The N-th smallest item in the entire <see cref="IList{T}"/>.</returns>
        public static T QuickSelectSmallest<T>(this IList<T> list, int n, IComparer<T> comparer)
        {
            return QuickSelectSmallest(list, n, 0, list.Count, comparer);
        }

        /// <summary>
        /// Returns N-th biggest item of the entire <see cref="IList{T}"/> using the default comparer without modifying list.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="IList{T}"/>.</typeparam>
        /// <param name="list">The <see cref="IList{T}"/> containing the items for searching.</param>
        /// <param name="n">The N-th biggest item to search for.</param>
        /// <param name="comparer">The <see cref="IComparable{T}"/> implementation used for comparing the elements,
        /// or null to use the default comparer <see cref="Comparer{T}.Default"/>.</param>
        /// <returns>The N-th biggest item in the entire <see cref="IList{T}"/>.</returns>
        public static T QuickSelectBiggest<T>(this IList<T> list, int n, IComparer<T> comparer)
        {
            return QuickSelectBiggest(list, n, 0, list.Count, comparer);
        }

        /// <summary>
        /// Returns N-th smallest item in a range of items in the <see cref="IList{T}"/> using the default comparer without modifying list.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="IList{T}"/>.</typeparam>
        /// <param name="n">The N-th smallest item to search for.</param>
        /// <param name="list">The <see cref="IList{T}"/> containing the items for searching.</param>
        /// <param name="index">The zero-based starting index of the range for searching.</param>
        /// <param name="count">The length of the range for searching.</param>
        /// <param name="comparer">The <see cref="IComparable{T}"/> implementation used for comparing the elements,
        /// or null to use the default comparer <see cref="Comparer{T}.Default"/>.</param>
        /// <returns>The N-th smallest item in the range of items in the <see cref="IList{T}"/>.</returns>
        public static T QuickSelectSmallest<T>(this IList<T> list, int n, int index, int count, IComparer<T> comparer)
        {
            if (index < 0 || index >= list.Count) throw new ArgumentOutOfRangeException(nameof(index));
            if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));
            if (n < 1 || n > count) throw new ArgumentOutOfRangeException(nameof(n));
            if (index + count > list.Count) throw new ArgumentException("Invalid length specified.");

            if (comparer == null) comparer = Comparer<T>.Default;

            var copyArray = new T[count];
            for (int i = 0; i < count; i++)
            {
                copyArray[i] = list[index + i];
            }

            return QuickSelectInPlaceSmallest(copyArray, n, 0, count, comparer);
        }

        /// <summary>
        /// Returns N-th biggest item in a range of items in the <see cref="IList{T}"/> using the default comparer without modifying list.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="IList{T}"/>.</typeparam>
        /// <param name="n">The N-th biggest item to search for.</param>
        /// <param name="list">The <see cref="IList{T}"/> containing the items for searching.</param>
        /// <param name="index">The zero-based starting index of the range for searching.</param>
        /// <param name="count">The length of the range for searching.</param>
        /// <param name="comparer">The <see cref="IComparable{T}"/> implementation used for comparing the elements,
        /// or null to use the default comparer <see cref="Comparer{T}.Default"/>.</param>
        /// <returns>The N-th biggest item in the range of items in the <see cref="IList{T}"/>.</returns>
        public static T QuickSelectBiggest<T>(this IList<T> list, int n, int index, int count, IComparer<T> comparer)
        {
            if (index < 0 || index >= list.Count) throw new ArgumentOutOfRangeException(nameof(index));
            if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));
            if (n < 1 || n > count) throw new ArgumentOutOfRangeException(nameof(n));
            if (index + count > list.Count) throw new ArgumentException("Invalid length specified.");

            if (comparer == null) comparer = Comparer<T>.Default;

            var copyArray = new T[count];
            for (int i = 0; i < count; i++)
            {
                copyArray[i] = list[index + i];
            }

            return QuickSelectInPlaceBiggest(copyArray, n, 0, count, comparer);
        }

        #endregion

        #region In-place quick select

        /// <summary>
        /// Returns N-th smallest item of the entire <see cref="IList{T}"/> using the default comparer. The list is modified.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="IList{T}"/>.</typeparam>
        /// <param name="list">The <see cref="IList{T}"/> containing the items for searching.</param>
        /// <param name="n">The N-th smallest item to search for.</param>
        /// <returns>The N-th smallest item in the entire <see cref="IList{T}"/>.</returns>
        public static T QuickSelectInPlaceSmallest<T>(this IList<T> list, int n)
        {
            return QuickSelectInPlaceSmallest(list, n, Comparer<T>.Default);
        }

        /// <summary>
        /// Returns N-th biggest item of the entire <see cref="IList{T}"/> using the default comparer. The list is modified.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="IList{T}"/>.</typeparam>
        /// <param name="list">The <see cref="IList{T}"/> containing the items for searching.</param>
        /// <param name="n">The N-th biggest item to search for.</param>
        /// <returns>The N-th biggest item in the entire <see cref="IList{T}"/>.</returns>
        public static T QuickSelectInPlaceBiggest<T>(this IList<T> list, int n)
        {
            return QuickSelectInPlaceBiggest(list, n, Comparer<T>.Default);
        }

        /// <summary>
        /// Returns N-th smallest item of the entire <see cref="IList{T}"/> using the default comparer. The list is modified.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="IList{T}"/>.</typeparam>
        /// <param name="list">The <see cref="IList{T}"/> containing the items for searching.</param>
        /// <param name="n">The N-th smallest item to search for.</param>
        /// <param name="comparison">The <see cref="Comparison{T}"/> used for comparing the elements.</param>
        /// <returns>The N-th smallest item in the entire <see cref="IList{T}"/>.</returns>
        public static T QuickSelectInPlaceSmallest<T>(this IList<T> list, int n, Comparison<T> comparison)
        {
            return QuickSelectInPlaceSmallest(list, n, Comparer<T>.Create(comparison));
        }

        /// <summary>
        /// Returns N-th biggest item of the entire <see cref="IList{T}"/> using the default comparer. The list is modified.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="IList{T}"/>.</typeparam>
        /// <param name="list">The <see cref="IList{T}"/> containing the items for searching.</param>
        /// <param name="n">The N-th biggest item to search for.</param>
        /// <param name="comparison">The <see cref="Comparison{T}"/> used for comparing the elements.</param>
        /// <returns>The N-th biggest item in the entire <see cref="IList{T}"/>.</returns>
        public static T QuickSelectInPlaceBiggest<T>(this IList<T> list, int n, Comparison<T> comparison)
        {
            return QuickSelectInPlaceBiggest(list, n, Comparer<T>.Create(comparison));
        }

        /// <summary>
        /// Returns N-th smallest item of the entire <see cref="IList{T}"/> using the default comparer. The list is modified.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="IList{T}"/>.</typeparam>
        /// <param name="list">The <see cref="IList{T}"/> containing the items for searching.</param>
        /// <param name="n">The N-th smallest item to search for.</param>
        /// <param name="comparer">The <see cref="IComparable{T}"/> implementation used for comparing the elements,
        /// or null to use the default comparer <see cref="Comparer{T}.Default"/>.</param>
        /// <returns>The N-th smallest item in the entire <see cref="IList{T}"/>.</returns>
        public static T QuickSelectInPlaceSmallest<T>(this IList<T> list, int n, IComparer<T> comparer)
        {
            return QuickSelectInPlaceSmallest(list, n, 0, list.Count, comparer);
        }

        /// <summary>
        /// Returns N-th biggest item of the entire <see cref="IList{T}"/> using the default comparer. The list is modified.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="IList{T}"/>.</typeparam>
        /// <param name="list">The <see cref="IList{T}"/> containing the items for searching.</param>
        /// <param name="n">The N-th biggest item to search for.</param>
        /// <param name="comparer">The <see cref="IComparable{T}"/> implementation used for comparing the elements,
        /// or null to use the default comparer <see cref="Comparer{T}.Default"/>.</param>
        /// <returns>The N-th biggest item in the entire <see cref="IList{T}"/>.</returns>
        public static T QuickSelectInPlaceBiggest<T>(this IList<T> list, int n, IComparer<T> comparer)
        {
            return QuickSelectInPlaceBiggest(list, n, 0, list.Count, comparer);
        }

        /// <summary>
        /// Returns N-th smallest item in a range of items in the <see cref="IList{T}"/> using the default comparer. The list is modified.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="IList{T}"/>.</typeparam>
        /// <param name="n">The N-th smallest item to search for.</param>
        /// <param name="list">The <see cref="IList{T}"/> containing the items for searching.</param>
        /// <param name="index">The zero-based starting index of the range for searching.</param>
        /// <param name="count">The length of the range for searching.</param>
        /// <param name="comparer">The <see cref="IComparable{T}"/> implementation used for comparing the elements,
        /// or null to use the default comparer <see cref="Comparer{T}.Default"/>.</param>
        /// <returns>The N-th smallest item in the range of items in the <see cref="IList{T}"/>.</returns>
        public static T QuickSelectInPlaceSmallest<T>(this IList<T> list, int n, int index, int count, IComparer<T> comparer)
        {
            if (index < 0 || index >= list.Count) throw new ArgumentOutOfRangeException(nameof(index));
            if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));
            if (n < 1 || n > count) throw new ArgumentOutOfRangeException(nameof(n));
            if (index + count > list.Count) throw new ArgumentException("Invalid length specified.");

            if (comparer == null) comparer = Comparer<T>.Default;

            int left = index;
            int right = index + count - 1;
            int nthIndex = index + n - 1;

            while (left < right)
            {
                int pivotIndex = QuickSelectSmallestPartition(list, left, right, comparer);

                if (pivotIndex == nthIndex)
                    return list[pivotIndex];
                else if (pivotIndex < nthIndex)
                    left = pivotIndex + 1;
                else
                    right = pivotIndex - 1;
            }

            return list[nthIndex];
        }

        /// <summary>
        /// Partitions the given range of items in the given <see cref="IList{T}"/> and returns the index of the pivot. Used for N-th smallest quick select.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="IList{T}"/>.</typeparam>
        /// <param name="list">The <see cref="IList{T}"/> containing the elements for searching.</param>
        /// <param name="leftIndex">The zero-based inclusive starting index of the range for partitioning.</param>
        /// <param name="rightIndex">The zero-based inclusive ending index of the range for partitioning.</param>
        /// <param name="comparer">The <see cref="IComparable{T}"/> implementation used for comparing the elements.</param>
        /// <returns>The index of the pivot.</returns>
        private static int QuickSelectSmallestPartition<T>(IList<T> list, int leftIndex, int rightIndex, IComparer<T> comparer)
        {
            // Get the middle element as pivot. Better performance on nearly sorted collections.
            int pivotIndex = leftIndex + (rightIndex - leftIndex) / 2;// no overflow
            //Swap pivot position with first element position. Easier partitioning.
            T pivot = list[pivotIndex];
            list[pivotIndex] = list[leftIndex];
            list[leftIndex] = pivot;

            // indexes for comparing left and right sides of the array
            int i = leftIndex - 1;
            int j = rightIndex + 1;

            while (true)
            {
                // find the first element on the left side bigger than the pivot
                do
                {
                    i = i + 1;
                    if (i > rightIndex)// if we iterate past the last element
                    {
                        // change the first element(pivot) with the last element
                        T temp = list[leftIndex];
                        list[leftIndex] = list[rightIndex];
                        list[rightIndex] = temp;

                        return rightIndex;// return pivot index
                    }
                }
                while (comparer.Compare(list[i], pivot) <= 0);

                // find first element smaller or equal to the pivot from the right side(going right to left)
                while (comparer.Compare(list[--j], pivot) > 0) ;

                if (i >= j)// if i surpassed j we end the partition
                {
                    // swap the element on j position(last element smaller or equal to the pivot)
                    // with the first element(pivot)
                    T temp = list[leftIndex];
                    list[leftIndex] = list[j];
                    list[j] = temp;

                    return j;// return pivot index
                }

                // swap i and j
                T swap = list[i];
                list[i] = list[j];
                list[j] = swap;
            }
        }

        /// <summary>
        /// Returns N-th biggest item in a range of items in the <see cref="IList{T}"/> using the default comparer. The list is modified.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="IList{T}"/>.</typeparam>
        /// <param name="n">The N-th biggest item to search for.</param>
        /// <param name="list">The <see cref="IList{T}"/> containing the items for searching.</param>
        /// <param name="index">The zero-based starting index of the range for searching.</param>
        /// <param name="count">The length of the range for searching.</param>
        /// <param name="comparer">The <see cref="IComparable{T}"/> implementation used for comparing the elements,
        /// or null to use the default comparer <see cref="Comparer{T}.Default"/>.</param>
        /// <returns>The N-th biggest item in the range of items in the <see cref="IList{T}"/>.</returns>
        public static T QuickSelectInPlaceBiggest<T>(this IList<T> list, int n, int index, int count, IComparer<T> comparer)
        {
            if (index < 0 || index >= list.Count) throw new ArgumentOutOfRangeException(nameof(index));
            if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));
            if (n < 1 || n > count) throw new ArgumentOutOfRangeException(nameof(n));
            if (index + count > list.Count) throw new ArgumentException("Invalid length specified.");

            if (comparer == null) comparer = Comparer<T>.Default;

            int left = index;
            int right = index + count - 1;
            int nthIndex = index + n - 1;

            while (left < right)
            {
                int pivotIndex = QuickSelectBiggestPartition(list, left, right, comparer);

                if (pivotIndex == nthIndex)
                    return list[pivotIndex];
                else if (pivotIndex < nthIndex)
                    left = pivotIndex + 1;
                else
                    right = pivotIndex - 1;
            }

            return list[nthIndex];
        }

        /// <summary>
        /// Partitions the given range of items in the given <see cref="IList{T}"/> and returns the index of the pivot. Used for N-th biggest quick select.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="IList{T}"/>.</typeparam>
        /// <param name="list">The <see cref="IList{T}"/> containing the elements for searching.</param>
        /// <param name="leftIndex">The zero-based inclusive starting index of the range for partitioning.</param>
        /// <param name="rightIndex">The zero-based inclusive ending index of the range for partitioning.</param>
        /// <param name="comparer">The <see cref="IComparable{T}"/> implementation used for comparing the elements.</param>
        /// <returns>The index of the pivot.</returns>
        private static int QuickSelectBiggestPartition<T>(IList<T> list, int leftIndex, int rightIndex, IComparer<T> comparer)
        {
            // Get the middle element as pivot. Better performance on nearly sorted collections.
            int pivotIndex = leftIndex + (rightIndex - leftIndex) / 2;// no overflow
            //Swap pivot position with first element position. Easier partitioning.
            T pivot = list[pivotIndex];
            list[pivotIndex] = list[leftIndex];
            list[leftIndex] = pivot;

            // indexes for comparing left and right sides of the array
            int i = leftIndex - 1;
            int j = rightIndex + 1;

            while (true)
            {
                // find the first element on the left side smaller than the pivot
                do
                {
                    i = i + 1;
                    if (i > rightIndex)// if we iterate past the last element
                    {
                        // change the first element(pivot) with the last element
                        T temp = list[leftIndex];
                        list[leftIndex] = list[rightIndex];
                        list[rightIndex] = temp;

                        return rightIndex;// return pivot index
                    }
                }
                while (comparer.Compare(list[i], pivot) >= 0);

                // find first element bigger or equal to the pivot from the right side(going right to left)
                while (comparer.Compare(list[--j], pivot) < 0) ;

                if (i >= j)// if i surpassed j we end the partition
                {
                    // swap the element on j position(last element bigger or equal to the pivot)
                    // with the first element(pivot)
                    T temp = list[leftIndex];
                    list[leftIndex] = list[j];
                    list[j] = temp;

                    return j;// return pivot index
                }

                // swap i and j
                T swap = list[i];
                list[i] = list[j];
                list[j] = swap;
            }
        }
        
        #endregion
    }
}
