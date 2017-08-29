using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DSA.Algorithms.Sorting
{
    /// <summary>
    /// Static class containing extension methods for a parallel quick sort.
    /// </summary>
    public static class ParallelQuickSorter
    {
        /// <summary>
        /// Sorts the elements in the entire <see cref="IList{T}"/> in ascending order using the default comparer.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="IList{T}"/>.</typeparam>
        /// <param name="list">The <see cref="IList{T}"/> containing the elements for sorting.</param>
        /// <returns>Returns the given <see cref="IList{T}"/> when sorted.</returns>
        public static IList<T> ParallelQuickSort<T>(this IList<T> list)
        {
            if (list.Count == 0) return list;

            return ParallelQuickSort(list, Comparer<T>.Default);
        }

        /// <summary>
        /// Sorts the elements in the entire <see cref="IList{T}"/> in descending order using the default comparer.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="IList{T}"/>.</typeparam>
        /// <param name="list">The <see cref="IList{T}"/> containing the elements for sorting.</param>
        /// <returns>Returns the given <see cref="IList{T}"/> when sorted.</returns>
        public static IList<T> ParallelQuickSortDescending<T>(this IList<T> list)
        {
            if (list.Count == 0) return list;

            return ParallelQuickSortDescending(list, Comparer<T>.Default);
        }

        /// <summary>
        /// Sorts the elements in the entire <see cref="IList{T}"/> in ascending order using the specified comparison.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="IList{T}"/>.</typeparam>
        /// <param name="list">The <see cref="IList{T}"/> containing the elements for sorting.</param>
        /// <param name="comparison">The <see cref="Comparison{T}"/> used for comparing the elements.</param>
        /// <returns>Returns the given <see cref="IList{T}"/> when sorted.</returns>
        public static IList<T> ParallelQuickSort<T>(this IList<T> list, Comparison<T> comparison)
        {
            if (comparison == null) throw new ArgumentNullException(nameof(comparison));

            if (list.Count == 0) return list;

            return ParallelQuickSort(list, Comparer<T>.Create(comparison));
        }

        /// <summary>
        /// Sorts the elements in the entire <see cref="IList{T}"/> in descending order using the specified comparison.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="IList{T}"/>.</typeparam>
        /// <param name="list">The <see cref="IList{T}"/> containing the elements for sorting.</param>
        /// <param name="comparison">The <see cref="Comparison{T}"/> used for comparing the elements.</param>
        /// <returns>Returns the given <see cref="IList{T}"/> when sorted.</returns>
        public static IList<T> ParallelQuickSortDescending<T>(this IList<T> list, Comparison<T> comparison)
        {
            if (comparison == null) throw new ArgumentNullException(nameof(comparison));

            if (list.Count == 0) return list;

            return ParallelQuickSortDescending(list, Comparer<T>.Create(comparison));
        }

        /// <summary>
        /// Sorts the elements in the entire <see cref="IList{T}"/> in ascending order using the specified comparer.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="IList{T}"/>.</typeparam>
        /// <param name="list">The <see cref="IList{T}"/> containing the elements for sorting.</param>
        /// <param name="comparer">The <see cref="IComparable{T}"/> implementation used for comparing the elements,
        /// or null to use the default comparer <see cref="Comparer{T}.Default"/>.</param>
        /// <returns>Returns the given <see cref="IList{T}"/> when sorted.</returns>
        public static IList<T> ParallelQuickSort<T>(this IList<T> list, IComparer<T> comparer)
        {
            if (list.Count == 0) return list;

            return ParallelQuickSort(list, 0, list.Count, comparer);
        }

        /// <summary>
        /// Sorts the elements in the entire <see cref="IList{T}"/> in descending order using the specified comparer.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="IList{T}"/>.</typeparam>
        /// <param name="list">The <see cref="IList{T}"/> containing the elements for sorting.</param>
        /// <param name="comparer">The <see cref="IComparable{T}"/> implementation used for comparing the elements,
        /// or null to use the default comparer <see cref="Comparer{T}.Default"/>.</param>
        /// <returns>Returns the given <see cref="IList{T}"/> when sorted.</returns>
        public static IList<T> ParallelQuickSortDescending<T>(this IList<T> list, IComparer<T> comparer)
        {
            if (list.Count == 0) return list;

            return ParallelQuickSortDescending(list, 0, list.Count, comparer);
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
        public static IList<T> ParallelQuickSort<T>(this IList<T> list, int index, int count, IComparer<T> comparer)
        {
            if (index < 0 || index >= list.Count) throw new ArgumentOutOfRangeException(nameof(index));
            if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));
            if (index + count > list.Count) throw new ArgumentException("Invalid length specified.");

            if (comparer == null) comparer = Comparer<T>.Default;

            int depth = Environment.ProcessorCount;

            // Quick sort recursion require inclusive index of last elements
            // and the last element is index + count - 1
            ParallelQuickSortRecursion(list, index, index + count - 1, depth, comparer);

            return list;
        }

        /// <summary>
        /// Parallel algoritm. Recursively sorting the range of items in the given <see cref="IList{T}"/>. Used for ascending sort.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="IList{T}"/>.</typeparam>
        /// <param name="list">The <see cref="IList{T}"/> containing the elements for sorting.</param>
        /// <param name="leftIndex">The zero-based inclusive starting index of the range for partitioning.</param>
        /// <param name="rightIndex">The zero-based inclusive ending index of the range for partitioning.</param>
        /// <param name="depthRemaining">The remaining depth of the recursion for which parallel tasks are invoked.</param>
        /// <param name="comparer">The <see cref="IComparable{T}"/> implementation used for comparing the elements.</param>
        private static void ParallelQuickSortRecursion<T>(IList<T> list, int leftIndex, int rightIndex, int depthRemaining, IComparer<T> comparer)
        {
            if (leftIndex < rightIndex)// if we have 1 element or less it is considered sorted
            {
                // Partition the elements and get the pivot position
                int pivotIndex = ParallelQuickSortPartition(list, leftIndex, rightIndex, comparer);

                // Recursive sort for both partitions
                if (depthRemaining > 0) // if recursion depth is low enough create new parallel tasks
                {
                    Parallel.Invoke
                        (
                           () => ParallelQuickSortRecursion(list, leftIndex, pivotIndex - 1, depthRemaining - 1, comparer),
                           () => ParallelQuickSortRecursion(list, pivotIndex + 1, rightIndex, depthRemaining - 1, comparer)
                        );
                }
                else // if recursion depth is too much, perform sequential quick sort
                {
                    SequentialQuickSortRecursion(list, leftIndex, pivotIndex - 1, comparer);
                    SequentialQuickSortRecursion(list, pivotIndex + 1, rightIndex, comparer);
                }
            }
        }

        /// <summary>
        /// Sequential algoritm. Recursively sorting the range of items in the given <see cref="IList{T}"/>. Used for ascending sort.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="IList{T}"/>.</typeparam>
        /// <param name="list">The <see cref="IList{T}"/> containing the elements for sorting.</param>
        /// <param name="leftIndex">The zero-based inclusive starting index of the range for partitioning.</param>
        /// <param name="rightIndex">The zero-based inclusive ending index of the range for partitioning.</param>
        /// <param name="comparer">The <see cref="IComparable{T}"/> implementation used for comparing the elements.</param>
        private static void SequentialQuickSortRecursion<T>(IList<T> list, int leftIndex, int rightIndex, IComparer<T> comparer)
        {
            while (leftIndex < rightIndex)// if we have 1 element or less it is considered sorted
            {
                // Partition the elements and get the pivot position
                int pivotIndex = ParallelQuickSortPartition(list, leftIndex, rightIndex, comparer);

                // Recursive sort for the smaller partition. Ensures maximum recursion level is log(n).
                if (pivotIndex - leftIndex < rightIndex - pivotIndex)
                {
                    SequentialQuickSortRecursion(list, leftIndex, pivotIndex - 1, comparer);
                    leftIndex = pivotIndex + 1;
                }
                else
                {
                    SequentialQuickSortRecursion(list, pivotIndex + 1, rightIndex, comparer);
                    rightIndex = pivotIndex - 1;
                }
            }
        }

        /// <summary>
        /// Partitions the given range of items in the given <see cref="IList{T}"/> and returns the index of the pivot. Used for ascending sort.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="IList{T}"/>.</typeparam>
        /// <param name="list">The <see cref="IList{T}"/> containing the elements for sorting.</param>
        /// <param name="leftIndex">The zero-based inclusive starting index of the range for partitioning.</param>
        /// <param name="rightIndex">The zero-based inclusive ending index of the range for partitioning.</param>
        /// <param name="comparer">The <see cref="IComparable{T}"/> implementation used for comparing the elements.</param>
        /// <returns></returns>
        private static int ParallelQuickSortPartition<T>(IList<T> list, int leftIndex, int rightIndex, IComparer<T> comparer)
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
        /// Sorts a range of elements in the <see cref="IList{T}"/> in descending order using the specified comparer.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="IList{T}"/>.</typeparam>
        /// <param name="list">The <see cref="IList{T}"/> containing the elements for sorting.</param>
        /// <param name="index">The zero-based starting index of the range for sorting.</param>
        /// <param name="count">The length of the range for sorting.</param>
        /// <param name="comparer">The <see cref="IComparable{T}"/> implementation used for comparing the elements,
        /// or null to use the default comparer <see cref="Comparer{T}.Default"/>.</param>
        /// <returns>Returns the given <see cref="IList{T}"/> when sorted.</returns>
        public static IList<T> ParallelQuickSortDescending<T>(this IList<T> list, int index, int count, IComparer<T> comparer)
        {
            if (index < 0 || index >= list.Count) throw new ArgumentOutOfRangeException(nameof(index));
            if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));
            if (index + count > list.Count) throw new ArgumentException("Invalid length specified.");

            if (comparer == null) comparer = Comparer<T>.Default;

            int depth = Environment.ProcessorCount;

            // Quick sort recursion require inclusive index of last elements
            // and the last element is index + count - 1
            ParallelQuickSortRecursionDescending(list, index, index + count - 1, depth, comparer);

            return list;
        }

        /// <summary>
        /// Parallel algorithm. Recursively sorting the range of items in the given <see cref="IList{T}"/>. Used for descending sort.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="IList{T}"/>.</typeparam>
        /// <param name="list">The <see cref="IList{T}"/> containing the elements for sorting.</param>
        /// <param name="leftIndex">The zero-based inclusive starting index of the range for partitioning.</param>
        /// <param name="rightIndex">The zero-based inclusive ending index of the range for partitioning.</param>
        /// <param name="depthRemaining">The remaining depth of the recursion for which parallel tasks are invoked.</param>
        /// <param name="comparer">The <see cref="IComparable{T}"/> implementation used for comparing the elements.</param>
        private static void ParallelQuickSortRecursionDescending<T>(IList<T> list, int leftIndex, int rightIndex, int depthRemaining, IComparer<T> comparer)
        {
            if (leftIndex < rightIndex)// if we have 1 element or less it is considered sorted
            {
                // Partition the elements and get the pivot position
                int pivotIndex = ParallelQuickSortPartitionDescending(list, leftIndex, rightIndex, comparer);

                // Recursive sort for both partitions
                if (depthRemaining > 0) // if recursion depth is low enough create new parallel tasks
                {
                    Parallel.Invoke
                        (
                           () => ParallelQuickSortRecursionDescending(list, leftIndex, pivotIndex - 1, depthRemaining - 1, comparer),
                           () => ParallelQuickSortRecursionDescending(list, pivotIndex + 1, rightIndex, depthRemaining - 1, comparer)
                        );
                }
                else // if recursion depth is too much, perform sequential quick sort
                {
                    SequentialQuickSortRecursionDescending(list, leftIndex, pivotIndex - 1, comparer);
                    SequentialQuickSortRecursionDescending(list, pivotIndex + 1, rightIndex, comparer);
                }
            }
        }

        /// <summary>
        /// Sequential algorithm. Recursively sorting the range of items in the given <see cref="IList{T}"/>. Used for descending sort.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="IList{T}"/>.</typeparam>
        /// <param name="list">The <see cref="IList{T}"/> containing the elements for sorting.</param>
        /// <param name="leftIndex">The zero-based inclusive starting index of the range for partitioning.</param>
        /// <param name="rightIndex">The zero-based inclusive ending index of the range for partitioning.</param>
        /// <param name="comparer">The <see cref="IComparable{T}"/> implementation used for comparing the elements.</param>
        private static void SequentialQuickSortRecursionDescending<T>(IList<T> list, int leftIndex, int rightIndex, IComparer<T> comparer)
        {
            while (leftIndex < rightIndex)// if we have 1 element or less it is considered sorted
            {
                // Partition the elements and get the pivot position
                int pivotIndex = ParallelQuickSortPartitionDescending(list, leftIndex, rightIndex, comparer);

                // Recursive sort for the smaller partition. Ensures maximum recursion level is log(n).
                if (pivotIndex - leftIndex < rightIndex - pivotIndex)
                {
                    SequentialQuickSortRecursionDescending(list, leftIndex, pivotIndex - 1, comparer);
                    leftIndex = pivotIndex + 1;
                }
                else
                {
                    SequentialQuickSortRecursionDescending(list, pivotIndex + 1, rightIndex, comparer);
                    rightIndex = pivotIndex - 1;
                }
            }
        }

        /// <summary>
        /// Partitions the given range of items in the given <see cref="IList{T}"/> and returns the index of the pivot. Used for descending sort.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="IList{T}"/>.</typeparam>
        /// <param name="list">The <see cref="IList{T}"/> containing the elements for sorting.</param>
        /// <param name="leftIndex">The zero-based inclusive starting index of the range for partitioning.</param>
        /// <param name="rightIndex">The zero-based inclusive ending index of the range for partitioning.</param>
        /// <param name="comparer">The <see cref="IComparable{T}"/> implementation used for comparing the elements.</param>
        /// <returns></returns>
        private static int ParallelQuickSortPartitionDescending<T>(IList<T> list, int leftIndex, int rightIndex, IComparer<T> comparer)
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
    }
}
