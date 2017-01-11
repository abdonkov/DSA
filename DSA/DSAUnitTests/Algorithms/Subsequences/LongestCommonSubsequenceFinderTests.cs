using DSA.Algorithms.Subsequences;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace DSAUnitTests.Algorithms.Subsequences
{
    [TestClass]
    public class LongestCommonSubsequenceFinderTests
    {
        [TestMethod]
        public void CheckIfLCSLengthIsCorrect()
        {
            //Strings are axbycz and xaybzc
            string s1 = "axbycz";
            string s2 = "xaybzc";

            //Expected length is 3
            int length = LongestCommonSubsequenceFinder.LCSLength(s1, s2);
            Assert.IsTrue(length == 3);

            //Sequences are 231233132132313 and 2131212322221223
            var a = new int[] { 2, 3, 1, 2, 3, 3, 1, 3, 2, 1, 3, 2, 3, 1, 3 };
            var b = new int[] { 2, 1, 3, 1, 2, 1, 2, 3, 2, 2, 2, 2, 1, 2, 2, 3 };
            //Expected length is 10
            length = LongestCommonSubsequenceFinder.LCSLength(a, b);
            Assert.IsTrue(length == 10);
        }

        [TestMethod]
        public void CheckIfAllLCSAreOutputed()
        {
            //Strings are axbycz and xaybzc
            string s1 = "axbycz";
            string s2 = "xaybzc";

            //Expected 8 subsequences
            //abc, abz, ayc, ayz
            //xbc, xbz, xyc, xyz
            var subseq = new string[] { "abc", "abz", "ayc", "ayz", "xbc", "xbz", "xyc", "xyz"};
            var allLCS = LongestCommonSubsequenceFinder.GetAllLCS(s1, s2);
            for (int i = 0; i < subseq.Length; i++)
            {
                Assert.IsTrue(allLCS.Contains(subseq[i]));
            }

            //Sequences are 231233132132313 and 2131212322221223
            var a = new int[] { 2, 3, 1, 2, 3, 3, 1, 3, 2, 1, 3, 2, 3, 1, 3 };
            var b = new int[] { 2, 1, 3, 1, 2, 1, 2, 3, 2, 2, 2, 2, 1, 2, 2, 3 };

            //Expected 8 subsequences
            //2131212313
            //2131213213
            //2312123213
            //2312132123
            //2312132213
            var subseq2 = new List<List<int>>
            {
                new List<int>() { 2, 1, 3, 1, 2, 1, 2, 3, 1, 3 },
                new List<int>() { 2, 1, 3, 1, 2, 1, 3, 2, 1, 3 },
                new List<int>() { 2, 3, 1, 2, 1, 2, 3, 2, 1, 3 },
                new List<int>() { 2, 3, 1, 2, 1, 3, 2, 1, 2, 3 },
                new List<int>() { 2, 3, 1, 2, 1, 3, 2, 2, 1, 3 }
            };

            var allLCS2 = LongestCommonSubsequenceFinder.GetAllLCS(a, b);
            for (int i = 0; i < subseq2.Count; i++)
            {
                bool isContained = true;
                for (int j = 0; j < allLCS2.Count; j++)
                {
                    isContained = true;
                    for (int k = 0; k < allLCS2[j].Count; k++)
                    {
                        if (!object.Equals(subseq2[i][k], allLCS2[j][k]))
                        {
                            isContained = false;
                            break;
                        }
                    }
                    if (isContained) break;
                }

                Assert.IsTrue(isContained);
            }
        }

        [TestMethod]
        public void CheckIfOneOutputedLCSIsFound()
        {
            //Strings are axbycz and xaybzc
            string s1 = "axbycz";
            string s2 = "xaybzc";

            int length = LongestCommonSubsequenceFinder.LCSLength(s1, s2);
            var lcs1 = LongestCommonSubsequenceFinder.GetOneLCS(s1, s2);

            Assert.IsTrue(length == lcs1.Length);

            int lcsIndex = 0;
            for (int i = 0; i < s1.Length; i++)
            {
                if (object.Equals(s1[i], lcs1[lcsIndex])) lcsIndex++;
                if (lcsIndex == lcs1.Length) break;
            }
            Assert.IsTrue(lcsIndex == length);

            lcsIndex = 0;
            for (int i = 0; i < s2.Length; i++)
            {
                if (object.Equals(s2[i], lcs1[lcsIndex])) lcsIndex++;
                if (lcsIndex == lcs1.Length) break;
            }
            Assert.IsTrue(lcsIndex == length);

            //Sequences are 231233132132313 and 2131212322221223
            var a = new int[] { 2, 3, 1, 2, 3, 3, 1, 3, 2, 1, 3, 2, 3, 1, 3 };
            var b = new int[] { 2, 1, 3, 1, 2, 1, 2, 3, 2, 2, 2, 2, 1, 2, 2, 3 };

            length = LongestCommonSubsequenceFinder.LCSLength(a, b);
            var lcs2 = LongestCommonSubsequenceFinder.GetOneLCS(a, b);

            Assert.IsTrue(length == lcs2.Count);

            lcsIndex = 0;
            for (int i = 0; i < a.Length; i++)
            {
                if (object.Equals(a[i], lcs2[lcsIndex])) lcsIndex++;
                if (lcsIndex == lcs2.Count) break;
            }
            Assert.IsTrue(lcsIndex == length);

            lcsIndex = 0;
            for (int i = 0; i < b.Length; i++)
            {
                if (object.Equals(b[i], lcs2[lcsIndex])) lcsIndex++;
                if (lcsIndex == lcs2.Count) break;
            }
            Assert.IsTrue(lcsIndex == length);
        }
    }
}
