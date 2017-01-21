using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DSA.Algorithms.Strings;
using System.Collections.Generic;
using System.Text;

namespace DSAUnitTests.Algorithms.Strings
{
    [TestClass]
    public class LongestCommonSubstringFinderTests
    {
        [TestMethod]
        public void CheckIfLCSLengthIsCorrect()
        {
            string s1 = "Hi... What are you doing?";
            string s2 = "Nothing... What about you?";

            //Expected length is 10
            int length = LongestCommonSubstringFinder.LCSLength(s1, s2);
            Assert.IsTrue(length == 10);

            s1 = "123456789qazwsxedcrfvtgb";
            s2 = "qazwsxedcrfv123456789uiopkjl;";

            //Expected length is 12
            length = LongestCommonSubstringFinder.LCSLength(s1, s2);
            Assert.IsTrue(length == 12);

            s1 = "кирилица";
            s2 = "Още малко кирилица";

            //Expected length is 8
            length = LongestCommonSubstringFinder.LCSLength(s1, s2);
            Assert.IsTrue(length == 8);

            s1 = "12345678";
            s2 = "qwertyuiop";

            //Expected length is 0
            length = LongestCommonSubstringFinder.LCSLength(s1, s2);
            Assert.IsTrue(length == 0);
        }

        [TestMethod]
        public void CheckIfAllLCSAreOutputed()
        {
            string s1 = "Just a simple test, nothing more.";
            string s2 = "Something not so...simple, want more.";

            var expected = new string[] { "thing ", "simple", " more." };

            var LCSs = LongestCommonSubstringFinder.GetAllLCS(s1, s2);

            Assert.IsTrue(LCSs.Count == expected.Length);
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.IsTrue(LCSs.Contains(expected[i]));
            }


            s1 = "Кирилицата е много хубав език.";
            s2 = "Пиши на кирилица... много красив език.";

            expected = new string[] { "ирилица", " много ", "в език." };

            LCSs = LongestCommonSubstringFinder.GetAllLCS(s1, s2);

            Assert.IsTrue(LCSs.Count == expected.Length);
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.IsTrue(LCSs.Contains(expected[i]));
            }


            s1 = "a b c d e f g h i j k l m n";
            s2 = "n m l k j i h g f e d c b a";

            expected = new string[] { " b ", " c ", " d ", " e ", " f ", " g ", " h ", " i ", " j ", " k ", " l ", " m " };

            LCSs = LongestCommonSubstringFinder.GetAllLCS(s1, s2);

            Assert.IsTrue(LCSs.Count == expected.Length);
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.IsTrue(LCSs.Contains(expected[i]));
            }


            s1 = "52352345235";
            s2 = "assadgasdfasf";

            expected = new string[] { };

            LCSs = LongestCommonSubstringFinder.GetAllLCS(s1, s2);

            Assert.IsTrue(LCSs.Count == expected.Length);
        }

        [TestMethod]
        public void CheckIfOneOutputedLCSIsFound()
        {
            string s1 = "Just a simple test, nothing more.";
            string s2 = "Something not so...simple, want more.";

            var allLCS = new List<string> { "thing ", "simple", " more." };

            var oneLCS = LongestCommonSubstringFinder.GetOneLCS(s1, s2);

            Assert.IsTrue(oneLCS.Length == allLCS[0].Length);
            Assert.IsTrue(allLCS.Contains(oneLCS));


            s1 = "Кирилицата е много хубав език.";
            s2 = "Пиши на кирилица... много красив език.";

            allLCS = new List<string> { "ирилица", " много ", "в език." };

            oneLCS = LongestCommonSubstringFinder.GetOneLCS(s1, s2);

            Assert.IsTrue(oneLCS.Length == allLCS[0].Length);
            Assert.IsTrue(allLCS.Contains(oneLCS));


            s1 = "a b c d e f g h i j k l m n";
            s2 = "n m l k j i h g f e d c b a";

            allLCS = new List<string> { " b ", " c ", " d ", " e ", " f ", " g ", " h ", " i ", " j ", " k ", " l ", " m " };

            oneLCS = LongestCommonSubstringFinder.GetOneLCS(s1, s2);

            Assert.IsTrue(oneLCS.Length == allLCS[0].Length);
            Assert.IsTrue(allLCS.Contains(oneLCS));
        }

        [TestMethod]
        public void RandomStringCheckIfAllLCSAreFoundInStrings()
        {
            var rand = new Random(Environment.TickCount);

            int stringsLength = 100;
            int numOfDifferentSymbols = 10;

            int numberOfTests = 1000;

            for (int t = 0; t < numberOfTests; t++)
            {
                // Create first string
                var sb = new StringBuilder();
                for (int i = 0; i < stringsLength; i++)
                {
                    sb.Append((char)rand.Next(numOfDifferentSymbols));
                }
                string s1 = sb.ToString();

                // Create second string
                sb.Length = 0;
                for (int i = 0; i < stringsLength; i++)
                {
                    sb.Append((char)rand.Next(numOfDifferentSymbols));
                }
                string s2 = sb.ToString();

                // Check LCSs
                int lcsLength;
                var allLCSs = LongestCommonSubstringFinder.GetAllLCS(s1, s2, out lcsLength);

                foreach (var lcs in allLCSs)
                {
                    Assert.IsTrue(s1.Contains(lcs));
                    Assert.IsTrue(s2.Contains(lcs));
                    Assert.IsTrue(lcs.Length == lcsLength);
                }
            }
        }
    }
}
