using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DSA.Algorithms.Subsequences;
using System.Collections.Generic;

namespace DSAUnitTests.Algorithms.Subsequences
{
    [TestClass]
    public class LongestIncreasingSubsequenceFinderTests
    {
        [TestMethod]
        public void FindLISWithDefaultComparer()
        {
            var sequence = new List<int> { 0, 9, 7, 8, 3, 0, 9, 1, 0, 8, 9, 4, 2, 3, 7, 8, 9, 3, 8, 4, 0, 2, 6, 4, 10 };

            // Expected sequence: 0, 1, 2, 3, 7, 8, 9, 10
            var expectedSeq = new int[] { 0, 1, 2, 3, 7, 8, 9, 10 };

            var lis = sequence.LongestIncreasingSubsequence();

            for (int i = 0; i < expectedSeq.Length; i++)
            {
                Assert.IsTrue(lis[i] == expectedSeq[i]);
            }
            
            // reversing the sequence
            sequence.Reverse();
            // Expected sequence: 0, 1, 3, 7, 9
            expectedSeq = new int[] { 0, 1, 3, 7, 9 };

            lis = sequence.LongestIncreasingSubsequence();

            for (int i = 0; i < expectedSeq.Length; i++)
            {
                Assert.IsTrue(lis[i] == expectedSeq[i]);
            }
        }

        [TestMethod]
        public void FindLISWithAGivenComparer()
        {
            // Reversed comaprer
            var comparer = Comparer<int>.Create((x, y) => y.CompareTo(x));

            var sequence = new List<int> { 0, 9, 7, 8, 3, 0, 9, 1, 0, 8, 9, 4, 2, 3, 7, 8, 9, 3, 8, 4, 0, 2, 6, 4, 10 };

            // Expected sequence: 9, 8, 7, 6, 4
            var expectedSeq = new int[] { 9, 8, 7, 6, 4 };

            var lis = sequence.LongestIncreasingSubsequence(comparer);

            for (int i = 0; i < expectedSeq.Length; i++)
            {
                Assert.IsTrue(lis[i] == expectedSeq[i]);
            }

            // reversing the sequence
            sequence.Reverse();
            // Expected sequence: 10, 9, 8, 7, 3, 2, 1, 0
            expectedSeq = new int[] { 10, 9, 8, 7, 3, 2, 1, 0 };

            lis = sequence.LongestIncreasingSubsequence(comparer);

            for (int i = 0; i < expectedSeq.Length; i++)
            {
                Assert.IsTrue(lis[i] == expectedSeq[i]);
            }

        }

        [TestMethod]
        public void CheckIfLISForReversedListWithReversedComparerIsSameAsNormal()
        {
            // The sequence is special because only one LIS is possible. If there were
            // multiple LIS for this sequence the test would not work as intended
            var sequence = new List<int>() { 0, 8, 2, 5, 8, 4, 1, 5, 8, 9, 10, 2, 7, 13, 14, 4, 6, 7, 8, 12, 17 };

            // Get lis for sequence
            var normalSeqLIS = sequence.LongestIncreasingSubsequence();

            // Reversed comaprer
            var comparer = Comparer<int>.Create((x, y) => y.CompareTo(x));

            sequence.Reverse();

            // Reverse sequence and get new lis
            var revSeqLIS = sequence.LongestIncreasingSubsequence(comparer);

            // Reverse the LIS of the reversed seqeunce
            var reversedLIS = new List<int>(revSeqLIS);

            Assert.IsTrue(reversedLIS.Count == normalSeqLIS.Count);

            reversedLIS.Reverse();

            // Check if both LIS are the same
            for (int i = 0; i < normalSeqLIS.Count; i++)
            {
                Assert.IsTrue(reversedLIS[i] == normalSeqLIS[i]);
            }
        }
    }
}
