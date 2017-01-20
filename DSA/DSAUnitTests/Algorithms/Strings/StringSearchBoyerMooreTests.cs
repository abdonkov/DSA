using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DSA.Algorithms.Strings;

namespace DSAUnitTests.Algorithms.Strings
{
    [TestClass]
    public class StringSearchBoyerMooreTests
    {
        [TestMethod]
        public void SinglePatternFirstOccurrence()
        {
            string t = "searching for a pattern in this string with multiple patterns";
            string p = "pattern";

            Assert.IsTrue(StringSearch.BoyerMooreSearchFirst(t, p) == 16);

            t = "test1 test2 test3 test4";
            p = "test2";

            Assert.IsTrue(StringSearch.BoyerMooreSearchFirst(t, p) == 6);

            t = "кирилица";
            p = "ли";

            Assert.IsTrue(StringSearch.BoyerMooreSearchFirst(t, p) == 4);

            t = "no such pattern";
            p = "smth";

            Assert.IsTrue(StringSearch.BoyerMooreSearchFirst(t, p) == -1);
        }

        [TestMethod]
        public void SinglePatternAllOccurrences()
        {
            string t = "searching for a pattern in this string with multiple patterns";
            string p = "pattern";
            var expectedOccur = new int[] { 16, 53 };

            var positions = StringSearch.BoyerMooreSearchAll(t, p);
            Assert.IsTrue(positions.Count == expectedOccur.Length);
            for (int i = 0; i < expectedOccur.Length; i++)
                Assert.IsTrue(positions[i] == expectedOccur[i]);


            t = "test1 test2 test3 test4";
            p = "test";
            expectedOccur = new int[] { 0, 6, 12, 18 };

            positions = StringSearch.BoyerMooreSearchAll(t, p);
            Assert.IsTrue(positions.Count == expectedOccur.Length);
            for (int i = 0; i < expectedOccur.Length; i++)
                Assert.IsTrue(positions[i] == expectedOccur[i]);

            p = "t";
            expectedOccur = new int[] { 0, 3, 6, 9, 12, 15, 18, 21 };

            positions = StringSearch.BoyerMooreSearchAll(t, p);
            Assert.IsTrue(positions.Count == expectedOccur.Length);
            for (int i = 0; i < expectedOccur.Length; i++)
                Assert.IsTrue(positions[i] == expectedOccur[i]);

            t = "кирилица";
            p = "и";
            expectedOccur = new int[] { 1, 3, 5 };

            positions = StringSearch.BoyerMooreSearchAll(t, p);
            Assert.IsTrue(positions.Count == expectedOccur.Length);
            for (int i = 0; i < expectedOccur.Length; i++)
                Assert.IsTrue(positions[i] == expectedOccur[i]);

            t = "no such pattern";
            p = "smth";
            expectedOccur = new int[] { };

            positions = StringSearch.BoyerMooreSearchAll(t, p);
            Assert.IsTrue(positions.Count == expectedOccur.Length);
        }

        [TestMethod]
        public void MultiplePatternsFirstOccurence()
        {
            string t = "searching for a pattern in this string with multiple patterns";
            string[] p = new string[] { "pattern", "for", "multiple", "s", "with" };
            var expectedOccur = new int[] { 16, 10, 44, 0, 39 };

            var positions = StringSearch.BoyerMooreMultipleSearchFirst(t, p);
            Assert.IsTrue(positions.Count == expectedOccur.Length);
            for (int i = 0; i < expectedOccur.Length; i++)
                Assert.IsTrue(positions[p[i]] == expectedOccur[i]);


            t = "test1 test2 test3 test4";
            p = new string[] { "test", "2", "test3", " " };
            expectedOccur = new int[] { 0, 10, 12, 5 };

            positions = StringSearch.BoyerMooreMultipleSearchFirst(t, p);
            Assert.IsTrue(positions.Count == expectedOccur.Length);
            for (int i = 0; i < expectedOccur.Length; i++)
                Assert.IsTrue(positions[p[i]] == expectedOccur[i]);


            t = "кирлица... още малко";
            p = new string[] { "и", "ца.", "още", "малко" };
            expectedOccur = new int[] { 1, 5, 11, 15 };

            positions = StringSearch.BoyerMooreMultipleSearchFirst(t, p);
            Assert.IsTrue(positions.Count == expectedOccur.Length);
            for (int i = 0; i < expectedOccur.Length; i++)
                Assert.IsTrue(positions[p[i]] == expectedOccur[i]);


            t = "no such pattern";
            p = new string[] { "ha", "asdf", "wtf", "test" };
            expectedOccur = new int[] { };

            positions = StringSearch.BoyerMooreMultipleSearchFirst(t, p);
            Assert.IsTrue(positions.Count == expectedOccur.Length);
        }

        [TestMethod]
        public void MultiplePatternsAllOccurrences()
        {
            string t = "searching for a pattern in this string with multiple patterns";
            string[] p = new string[] { "pattern", " ", "ing", "a", "with" };
            var expectedOccur = new int[5][]
            {
                new int[] { 16, 53 }, // pattern
                new int[] { 9, 13, 15, 23, 26, 31, 38, 43, 52 }, // " "
                new int[] { 6, 35 }, // ing
                new int[] { 2, 14, 17, 54 }, // a
                new int[] { 39 } // with
            };

            var positions = StringSearch.BoyerMooreMultipleSearchAll(t, p);
            for (int i = 0; i < p.Length; i++)
            {
                Assert.IsTrue(positions[p[i]].Count == expectedOccur[i].Length);
                for (int j = 0; j < expectedOccur[i].Length; j++)
                    Assert.IsTrue(positions[p[i]][j] == expectedOccur[i][j]);
            }


            t = "test1 test2 test3 test4";
            p = new string[] { "test", " ", "t", "3" };
            expectedOccur = new int[4][]
            {
                new int[] { 0, 6, 12, 18 }, // test
                new int[] { 5, 11, 17 }, // " "
                new int[] { 0, 3, 6, 9, 12, 15, 18, 21 }, // t
                new int[] { 16 } // 3
            };

            positions = StringSearch.BoyerMooreMultipleSearchAll(t, p);
            for (int i = 0; i < p.Length; i++)
            {
                Assert.IsTrue(positions[p[i]].Count == expectedOccur[i].Length);
                for (int j = 0; j < expectedOccur[i].Length; j++)
                    Assert.IsTrue(positions[p[i]][j] == expectedOccur[i][j]);
            }


            t = "кирилица... още малко";
            p = new string[] { "и", " ", "о", "." };
            expectedOccur = new int[4][]
            {
                new int[] { 1, 3, 5 }, // и
                new int[] { 11, 15 }, // " "
                new int[] { 12, 20 }, // о
                new int[] { 8, 9, 10 } // .
            };

            positions = StringSearch.BoyerMooreMultipleSearchAll(t, p);
            for (int i = 0; i < p.Length; i++)
            {
                Assert.IsTrue(positions[p[i]].Count == expectedOccur[i].Length);
                for (int j = 0; j < expectedOccur[i].Length; j++)
                    Assert.IsTrue(positions[p[i]][j] == expectedOccur[i][j]);
            }


            t = "no such pattern";
            p = new string[] { "ha", "test", "smth", "anything", "ok" };
            expectedOccur = new int[5][]
            {
                new int[] { },
                new int[] { },
                new int[] { },
                new int[] { },
                new int[] { }
            };

            positions = StringSearch.BoyerMooreMultipleSearchAll(t, p);
            for (int i = 0; i < p.Length; i++)
                Assert.IsFalse(positions.ContainsKey(p[i]));
        }
    }
}
