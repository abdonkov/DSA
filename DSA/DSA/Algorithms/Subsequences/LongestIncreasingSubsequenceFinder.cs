using System.Collections.Generic;

namespace DSA.Algorithms.Subsequences
{
    /// <summary>
    /// A static class containing methods for finding the longest increasing subsequence.
    /// </summary>
    public static class LongestIncreasingSubsequenceFinder
    {
        /// <summary>
        /// Finds the longest increasing subsequence in the given <see cref="IList{T}"/> object using the default comparer.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="IList{T}"/>.</typeparam>
        /// <param name="seq">The <see cref="IList{T}"/> object representing the sequence.</param>
        /// <returns>A <see cref="IList{T}"/> object representing the longest increasing subsequence.</returns>
        public static IList<T> LongestIncreasingSubsequence<T>(this IList<T> seq)
        {
            return LongestIncreasingSubsequence(seq, Comparer<T>.Default);
        }

        /// <summary>
        /// Finds the longest increasing subsequence in the given <see cref="IList{T}"/> object using the given comparer.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="IList{T}"/>.</typeparam>
        /// <param name="seq">The <see cref="IList{T}"/> object representing the sequence.</param>
        /// <param name="comparer">The comparer for the elements in the sequence. If null using the default comparer.</param>
        /// <returns>A <see cref="IList{T}"/> object representing the longest increasing subsequence.</returns>
        public static IList<T> LongestIncreasingSubsequence<T>(this IList<T> seq, IComparer<T> comparer)
        {
            if (comparer == null) comparer = Comparer<T>.Default;

            int count = seq.Count;

            // Stores the index of the smallest value that denotes the ending of a increasing subseqeunce
            // with a given length (the length being the index of the array)
            var endValues = new int[seq.Count + 1];
            // Stores the index of the predecessor of a value in the sequence
            var predecessors = new int[seq.Count];
            //The length of the current longest subsequence
            int longestSubseqLength = 0;

            // Go through all values in the sequence
            for (int i = 0; i < count; i++)
            {
                int low = 1;
                int high = longestSubseqLength;

                // Binary search for the largest subsequence that ends with an element smaller that the current one
                while (low <= high)
                {
                    int mid = low + (high - low) / 2;

                    if (comparer.Compare(seq[endValues[mid]], seq[i]) < 0)
                        low = mid + 1;
                    else
                        high = mid - 1;
                }

                // After the search, low is the length of the new subsequence
                // created with the current element
                var newSeqLength = low;

                // The predecessor of the current element is the last element
                // in the subsequence of length newSeqLength - 1
                predecessors[i] = endValues[newSeqLength - 1];

                // Set the last element of the newly created subsequence
                endValues[newSeqLength] = i;

                //Update longest found subsequence if needed
                if (newSeqLength > longestSubseqLength)
                    longestSubseqLength = newSeqLength;
            }

            //Construct the longest increasing subsequence
            var lis = new T[longestSubseqLength];
            
            int mainSeqIndex = endValues[longestSubseqLength];
            for (int i = longestSubseqLength - 1; i >= 0; i--)
            {
                lis[i] = seq[mainSeqIndex];
                mainSeqIndex = predecessors[mainSeqIndex];
            }
            
            return lis;
        }
    }
}
