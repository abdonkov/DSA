using System;
using System.Collections.Generic;

namespace DSA.Algorithms.Sorting
{
    /// <summary>
    /// Static class containing extension methods for merge sort.
    /// </summary>
    public static partial class MergeSorter
    {
        /// <summary>
        /// Sorts the elements in the entire <see cref="IList{T}"/> in ascending order using the default comparer.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="IList{T}"/>.</typeparam>
        /// <param name="list">The <see cref="IList{T}"/> containing the elements for sorting.</param>
        /// <returns>Returns the given <see cref="IList{T}"/> when sorted.</returns>
        public static IList<T> MergeSort<T>(this IList<T> list)
        {
            if (list.Count == 0) return list;

            return MergeSort(list, Comparer<T>.Default);
        }

        /// <summary>
        /// Sorts the elements in the entire <see cref="IList{T}"/> in descending order using the default comparer.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="IList{T}"/>.</typeparam>
        /// <param name="list">The <see cref="IList{T}"/> containing the elements for sorting.</param>
        /// <returns>Returns the given <see cref="IList{T}"/> when sorted.</returns>
        public static IList<T> MergeSortDescending<T>(this IList<T> list)
        {
            if (list.Count == 0) return list;

            return MergeSortDescending(list, Comparer<T>.Default);
        }

        /// <summary>
        /// Sorts the elements in the entire <see cref="IList{T}"/> in ascending order using the specified comparison.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="IList{T}"/>.</typeparam>
        /// <param name="list">The <see cref="IList{T}"/> containing the elements for sorting.</param>
        /// <param name="comparison">The <see cref="Comparison{T}"/> used for comparing the elements.</param>
        /// <returns>Returns the given <see cref="IList{T}"/> when sorted.</returns>
        public static IList<T> MergeSort<T>(this IList<T> list, Comparison<T> comparison)
        {
            if (comparison == null) throw new ArgumentNullException(nameof(comparison));

            if (list.Count == 0) return list;

            return MergeSort(list, Comparer<T>.Create(comparison));
        }

        /// <summary>
        /// Sorts the elements in the entire <see cref="IList{T}"/> in descending order using the specified comparison.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="IList{T}"/>.</typeparam>
        /// <param name="list">The <see cref="IList{T}"/> containing the elements for sorting.</param>
        /// <param name="comparison">The <see cref="Comparison{T}"/> used for comparing the elements.</param>
        /// <returns>Returns the given <see cref="IList{T}"/> when sorted.</returns>
        public static IList<T> MergeSortDescending<T>(this IList<T> list, Comparison<T> comparison)
        {
            if (comparison == null) throw new ArgumentNullException(nameof(comparison));

            if (list.Count == 0) return list;

            return MergeSortDescending(list, Comparer<T>.Create(comparison));
        }

        /// <summary>
        /// Sorts the elements in the entire <see cref="IList{T}"/> in ascending order using the specified comparer.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="IList{T}"/>.</typeparam>
        /// <param name="list">The <see cref="IList{T}"/> containing the elements for sorting.</param>
        /// <param name="comparer">The <see cref="IComparable{T}"/> implementation used for comparing the elements,
        /// or null to use the default comparer <see cref="Comparer{T}.Default"/>.</param>
        /// <returns>Returns the given <see cref="IList{T}"/> when sorted.</returns>
        public static IList<T> MergeSort<T>(this IList<T> list, IComparer<T> comparer)
        {
            if (list.Count == 0) return list;

            return MergeSort(list, 0, list.Count, comparer);
        }

        /// <summary>
        /// Sorts the elements in the entire <see cref="IList{T}"/> in descending order using the specified comparer.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="IList{T}"/>.</typeparam>
        /// <param name="list">The <see cref="IList{T}"/> containing the elements for sorting.</param>
        /// <param name="comparer">The <see cref="IComparable{T}"/> implementation used for comparing the elements,
        /// or null to use the default comparer <see cref="Comparer{T}.Default"/>.</param>
        /// <returns>Returns the given <see cref="IList{T}"/> when sorted.</returns>
        public static IList<T> MergeSortDescending<T>(this IList<T> list, IComparer<T> comparer)
        {
            if (list.Count == 0) return list;

            return MergeSortDescending(list, 0, list.Count, comparer);
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
        public static IList<T> MergeSort<T>(this IList<T> list, int index, int count, IComparer<T> comparer)
        {
            if (index < 0 || index >= list.Count) throw new ArgumentOutOfRangeException(nameof(index));
            if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));
            if (index + count > list.Count) throw new ArgumentException("Invalid length specified.");

            if (comparer == null) comparer = Comparer<T>.Default;

            // creating an array to save the sorted items in
            // NOTE: In-place implementation of merge sort for arrays
            // is either not stable or have n^2 worst case complexity
            var workArray = new T[list.Count];

            // copy the elements from the given list to the work list
            for (int i = 0; i < list.Count; i++)
            {
                workArray[i] = list[i];
            }

            // Recursive merge sort using the workList as a source and our list as a destination for the sorted items
            SplitMerge(workArray, index, index + count, list, comparer);

            return list;
        }

        /// <summary>
        /// Sorting the elements from the source <see cref="IList{T}"/> and saving them in the destionation <see cref="IList{T}"/>. Used for ascending sort.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="IList{T}"/>.</typeparam>
        /// <param name="src">The source <see cref="IList{T}"/> containing the elements for sorting.</param>
        /// <param name="dst">The destination <see cref="IList{T}"/> for saving the sorted elements.</param>
        /// <param name="begin">The zero-based starting index of the current split.</param>
        /// <param name="end">The exclusive end of the current split.</param>
        /// <param name="comparer">The <see cref="IComparable{T}"/> implementation used for comparing the elements.</param>
        private static void SplitMerge<T>(IList<T> src, int begin, int end, IList<T> dst, IComparer<T> comparer)
        {
            // End of recursion. If we have 1 item or less we consider it sorted
            if (end - begin < 2) return;

            int middle = begin + ((end - begin) / 2);// compute middle point safe. No overflow posssible.

            // recursively sort the resulting splits from the destionation to the source.
            // NOTE: swapping of the source and destionatnion is done for better performance
            // If we don't swap them on each split we have to copy the items from one to the other after each merge
            SplitMerge(dst, begin, middle, src, comparer);
            SplitMerge(dst, middle, end, src, comparer);
            // merge the current split from the current source into the current destionation
            Merge(src, begin, middle, end, dst, comparer);

            // NOTE: when swapping the source and the destination on each split. The bottom of the 
            // recursion have them in one order which is sorted in the destionation, then the
            // next split is swapped so the now sorted destination is used as a source, then the next
            // split is swapped and the sorted new destionation is used as a source and so on...
            // The last merging operation always have the given list for sorting as a destination
            // and the working array as the source so after the last merging the sorted elements
            // are in the given list for sorting
        }

        /// <summary>
        /// Merging the two lists (parts of the given source list) defined from the begin, middle and end indexes into the destionation. Used for ascending sort.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="IList{T}"/>.</typeparam>
        /// <param name="src">The source <see cref="IList{T}"/> containing the elements for sorting.</param>
        /// <param name="dst">The destination <see cref="IList{T}"/> for saving the sorted elements.</param>
        /// <param name="begin">The zero-based starting index of the left list.</param>
        /// <param name="middle">The zero-based starting index of the right list. Also the exclusive end of the left list.</param>
        /// <param name="end">The exclusive end of the right list.</param>
        /// <param name="comparer">The <see cref="IComparable{T}"/> implementation used for comparing the elements.</param>
        private static void Merge<T>(IList<T> src, int begin, int middle, int end, IList<T> dst, IComparer<T> comparer)
        {
            int i = begin;// left list beginnign index
            int j = middle;// right list beginning index

            // for all elemnts in the left and right list
            for (int k = begin; k < end; k++)
            {
                // if left list existing element is <= than the existing right list element OR right list is empty
                if (i < middle && (j >= end || comparer.Compare(src[i], src[j]) <= 0))
                {
                    dst[k] = src[i];
                    i++;
                }
                else// if left list is empty OR left list element is > than the right list element
                {
                    dst[k] = src[j];
                    j++;
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
        public static IList<T> MergeSortDescending<T>(this IList<T> list, int index, int count, IComparer<T> comparer)
        {
            if (index < 0 || index >= list.Count) throw new ArgumentOutOfRangeException(nameof(index));
            if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));
            if (index + count > list.Count) throw new ArgumentException("Invalid length specified.");

            if (comparer == null) comparer = Comparer<T>.Default;

            // creating an array to save the sorted items in
            // NOTE: In-place implementation of merge sort for arrays
            // is either not stable or have n^2 worst case complexity
            var workList = new T[list.Count];

            // copy the elements from the given list to the work list
            for (int i = 0; i < list.Count; i++)
            {
                workList[i] = list[i];
            }

            // Recursive merge sort using the workList as a source and our list as a destination for the sorted items
            SplitMergeDescending(workList, index, index + count, list, comparer);

            return list;
        }

        /// <summary>
        /// Sorting the elements from the source <see cref="IList{T}"/> and saving them in the destionation <see cref="IList{T}"/>. Used for descending sort.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="IList{T}"/>.</typeparam>
        /// <param name="src">The source <see cref="IList{T}"/> containing the elements for sorting.</param>
        /// <param name="dst">The destination <see cref="IList{T}"/> for saving the sorted elements.</param>
        /// <param name="begin">The zero-based starting index of the current split.</param>
        /// <param name="end">The exclusive end of the current split.</param>
        /// <param name="comparer">The <see cref="IComparable{T}"/> implementation used for comparing the elements.</param>
        private static void SplitMergeDescending<T>(IList<T> src, int begin, int end, IList<T> dst, IComparer<T> comparer)
        {
            // End of recursion. If we have 1 item or less we consider it sorted
            if (end - begin < 2) return;

            int middle = begin + ((end - begin) / 2);// compute middle point safe. No overflow posssible.

            // recursively sort the resulting splits from the destionation to the source.
            // NOTE: swapping of the source and destionatnion is done for better performance
            // If we don't swap them on each split we have to copy the items from one to the other after each merge
            SplitMergeDescending(dst, begin, middle, src, comparer);
            SplitMergeDescending(dst, middle, end, src, comparer);
            // merge the current split from the source into the destionation
            MergeDescending(src, begin, middle, end, dst, comparer);

            // NOTE: when swapping the source and the destination on each split. The bottom of the 
            // recursion have them in one order which is sorted in the destionation, then the
            // next split is swapped so the now sorted destination is used as a source, then the next
            // split is swapped and the sorted new destionation is used as a source and so on...
            // The last merging operation always have the given list for sorting as a destination
            // and the working array as the source so after the last merging the sorted elements
            // are in the given list for sorting
        }

        /// <summary>
        /// Merging the two lists (parts of the given source list) defined from the begin, middle and end indexes into the destionation. Used for descending sort.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="IList{T}"/>.</typeparam>
        /// <param name="src">The source <see cref="IList{T}"/> containing the elements for sorting.</param>
        /// <param name="dst">The destination <see cref="IList{T}"/> for saving the sorted elements.</param>
        /// <param name="begin">The zero-based starting index of the left list.</param>
        /// <param name="middle">The zero-based starting index of the right list. Also the exclusive end of the left list.</param>
        /// <param name="end">The exclusive end of the right list.</param>
        /// <param name="comparer">The <see cref="IComparable{T}"/> implementation used for comparing the elements.</param>
        private static void MergeDescending<T>(IList<T> src, int begin, int middle, int end, IList<T> dst, IComparer<T> comparer)
        {
            int i = begin;// left list beginnign index
            int j = middle;// right list beginning index

            // for all elemnts in the left and right list
            for (int k = begin; k < end; k++)
            {
                // if left list existing element is <= than the existing right list element OR right list is empty
                if (i < middle && (j >= end || comparer.Compare(src[i], src[j]) >= 0))
                {
                    dst[k] = src[i];
                    i++;
                }
                else// if left list is empty OR left list element is > than the right list element
                {
                    dst[k] = src[j];
                    j++;
                }
            }
        }
    }
}
