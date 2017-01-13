using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DSA.Algorithms.Strings;

namespace DSAUnitTests.Algorithms.Strings
{
    [TestClass]
    public class EditDistanceLevenshteinTests
    {
        [TestMethod]
        public void NullOrEmptyStringsDistanceCheck()
        {
            string s1 = null;
            string s2 = "test";

            Assert.IsTrue(EditDistance.LevenshteinDistance(s1, s2) == s2.Length);
            var temp = s1;
            s1 = s2;
            s2 = temp;
            Assert.IsTrue(EditDistance.LevenshteinDistance(s1, s2) == s1.Length);

            s2 = string.Empty;
            Assert.IsTrue(EditDistance.LevenshteinDistance(s1, s2) == s1.Length);
            temp = s1;
            s1 = s2;
            s2 = temp;
            Assert.IsTrue(EditDistance.LevenshteinDistance(s1, s2) == s2.Length);

            s1 = null;
            s2 = string.Empty;
            Assert.IsTrue(EditDistance.LevenshteinDistance(s1, s2) == 0);
            temp = s1;
            s1 = s2;
            s2 = temp;
            Assert.IsTrue(EditDistance.LevenshteinDistance(s1, s2) == 0);
        }

        [TestMethod]
        public void ReversedStringsDistanceCheck()
        {
            string s1 = "asdfghjkl";
            string s2 = "lkjhgfdsa";

            // Expected length - 1, because one char is on the same place (odd number of chars)
            Assert.IsTrue(EditDistance.LevenshteinDistance(s1, s2) == s1.Length - 1);
            s1 = "qazwsxedcrfv";
            s2 = "vfrcdexswzaq";

            Assert.IsTrue(EditDistance.LevenshteinDistance(s1, s2) == s1.Length);
        }

        [TestMethod]
        public void DifferentLengthStringsDistanceCheck()
        {
            string s1 = "short string";
            string s2 = "long string, but not so much";

            System.Diagnostics.Trace.WriteLine(EditDistance.LevenshteinDistance(s1, s2));

            // Expected length - 7, because we have 7 consecutive chars that are the same in both strings
            Assert.IsTrue(EditDistance.LevenshteinDistance(s1, s2) == s2.Length - 7);

            s1 = "another one";
            s2 = "and another one";

            // Expected 4, because 4 chars from the second are not present in the first
            Assert.IsTrue(EditDistance.LevenshteinDistance(s1, s2) == 4);
        }
    }
}
