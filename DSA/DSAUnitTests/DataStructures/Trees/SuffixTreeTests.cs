using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DSA.DataStructures.Trees;

namespace DSAUnitTests.DataStructures.Trees
{
    [TestClass]
    public class SuffixTreeTests
    {
        [TestMethod]
        public void AddingAndCheckingIfContained()
        {
            var suffixTree = new SuffixTree();


            var words = new string[]
            {
                "One", "one", "oNe", "two", "hello",
                "test", "here", "there", "?!??!?!?!?",
                "VeryVeryVeryLoooooooooooooooooong",
                ".k[2c3-9024g-u,9weg,ouimwt", "3q2tgwadh",
                "`+rv`+*1v+vt23*1`vt*1v", "!@#)(*^$!%@_",
                "  bum  ", "  bam  ", "  bum  bam  ",
                "1", "12", "123", "1234", "12345", "123456"
            };


            for (int i = 0; i < words.Length; i++)
            {
                if (suffixTree.Contains(words[i])) Assert.Fail();

                suffixTree.Add(words[i]);

                if (!suffixTree.Contains(words[i])) Assert.Fail();

            }

            Assert.IsTrue(suffixTree.Count == words.Length);
        }

        [TestMethod]
        public void AddingAndRemovingSomeWordsAndCheckingIfContained()
        {
            var suffixTree = new SuffixTree();

            var words = new string[]
            {
                "One", "one", "oNe", "two", "hello",
                "test", "here", "there", "?!??!?!?!?",
                "VeryVeryVeryLoooooooooooooooooong",
                ".k[2c3-9024g-u,9weg,ouimwt", "3q2tgwadh",
                "`+rv`+*1v+vt23*1`vt*1v", "!@#)(*^$!%@_",
                "  bum  ", "  bam  ", "  bum  bam  ",
                "1", "12", "123", "1234", "12345", "123456"
            };


            for (int i = 0; i < words.Length; i++)
            {
                if (suffixTree.Contains(words[i])) Assert.Fail();

                suffixTree.Add(words[i]);

                if (!suffixTree.Contains(words[i])) Assert.Fail();

            }

            if (suffixTree.Count != words.Length) Assert.Fail();

            int removedWords = 0;

            for (int i = 0; i < words.Length; i += 2)
            {
                if (suffixTree.Remove(words[i])) removedWords++;
                else Assert.Fail();

                if (suffixTree.Contains(words[i])) Assert.Fail();
            }

            Assert.IsTrue(suffixTree.Count == words.Length - removedWords);
        }

        [TestMethod]
        public void AddingAfterRemovingEverything()
        {
            var suffixTree = new SuffixTree();

            var words = new string[]
            {
                "One", "one", "oNe", "two", "hello",
                "test", "here", "there", "?!??!?!?!?",
                "VeryVeryVeryLoooooooooooooooooong",
                ".k[2c3-9024g-u,9weg,ouimwt", "3q2tgwadh",
                "`+rv`+*1v+vt23*1`vt*1v", "!@#)(*^$!%@_",
                "  bum  ", "  bam  ", "  bum  bam  ",
                "1", "12", "123", "1234", "12345", "123456"
            };


            for (int i = 0; i < words.Length; i++)
            {
                if (suffixTree.Contains(words[i])) Assert.Fail();

                suffixTree.Add(words[i]);

                if (!suffixTree.Contains(words[i])) Assert.Fail();

            }

            if (suffixTree.Count != words.Length) Assert.Fail();

            for (int i = 0; i < words.Length; i++)
            {
                if (!suffixTree.Remove(words[i])) Assert.Fail();

                if (suffixTree.Contains(words[i])) Assert.Fail();
            }

            if (suffixTree.Count != 0) Assert.Fail();

            for (int i = 0; i < words.Length; i++)
            {
                if (suffixTree.Contains(words[i])) Assert.Fail();

                suffixTree.Add(words[i]);

                if (!suffixTree.Contains(words[i])) Assert.Fail();

            }

            Assert.IsTrue(suffixTree.Count == words.Length);
        }

        [TestMethod]
        public void AddingAfterClearingSuffixTree()
        {
            var suffixTree = new SuffixTree();

            var words = new string[]
            {
                "One", "one", "oNe", "two", "hello",
                "test", "here", "there", "?!??!?!?!?",
                "VeryVeryVeryLoooooooooooooooooong",
                ".k[2c3-9024g-u,9weg,ouimwt", "3q2tgwadh",
                "`+rv`+*1v+vt23*1`vt*1v", "!@#)(*^$!%@_",
                "  bum  ", "  bam  ", "  bum  bam  ",
                "1", "12", "123", "1234", "12345", "123456"
            };


            for (int i = 0; i < words.Length; i++)
            {
                if (suffixTree.Contains(words[i])) Assert.Fail();

                suffixTree.Add(words[i]);

                if (!suffixTree.Contains(words[i])) Assert.Fail();

            }

            if (suffixTree.Count != words.Length) Assert.Fail();

            suffixTree.Clear();

            if (suffixTree.Count != 0) Assert.Fail();

            for (int i = 0; i < words.Length; i++)
            {
                if (suffixTree.Contains(words[i])) Assert.Fail();

                suffixTree.Add(words[i]);

                if (!suffixTree.Contains(words[i])) Assert.Fail();

            }

            Assert.IsTrue(suffixTree.Count == words.Length);
        }

        [TestMethod]
        public void CheckIfSortedAfterAdding()
        {
            var suffixTree = new SuffixTree();

            var words = new string[]
            {
                "One", "one", "oNe", "two", "hello",
                "test", "here", "there", "?!??!?!?!?",
                "VeryVeryVeryLoooooooooooooooooong",
                ".k[2c3-9024g-u,9weg,ouimwt", "3q2tgwadh",
                "`+rv`+*1v+vt23*1`vt*1v", "!@#)(*^$!%@_",
                "  bum  ", "  bam  ", "  bum  bam  ",
                "1", "12", "123", "1234", "12345", "123456"
            };


            for (int i = 0; i < words.Length; i++)
            {
                if (suffixTree.Contains(words[i])) Assert.Fail();

                suffixTree.Add(words[i]);

                if (!suffixTree.Contains(words[i])) Assert.Fail();

            }

            var previousWord = string.Empty;

            foreach (var word in suffixTree)
            {
                if (string.CompareOrdinal(previousWord, word) > 0) Assert.Fail();

                //System.Diagnostics.Trace.WriteLine(word);

                previousWord = word;
            }

            Assert.IsTrue(suffixTree.Count == words.Length);
        }

        [TestMethod]
        public void CheckIfSortedAfterAddingAndRemovingSomeWords()
        {
            var suffixTree = new SuffixTree();

            var words = new string[]
            {
                "One", "one", "oNe", "two", "hello",
                "test", "here", "there", "?!??!?!?!?",
                "VeryVeryVeryLoooooooooooooooooong",
                ".k[2c3-9024g-u,9weg,ouimwt", "3q2tgwadh",
                "`+rv`+*1v+vt23*1`vt*1v", "!@#)(*^$!%@_",
                "  bum  ", "  bam  ", "  bum  bam  ",
                "1", "12", "123", "1234", "12345", "123456"
            };


            for (int i = 0; i < words.Length; i++)
            {
                if (suffixTree.Contains(words[i])) Assert.Fail();

                suffixTree.Add(words[i]);

                if (!suffixTree.Contains(words[i])) Assert.Fail();

            }

            if (suffixTree.Count != words.Length) Assert.Fail();

            int removedWords = 0;

            for (int i = 0; i < words.Length; i += 2)
            {
                if (suffixTree.Remove(words[i])) removedWords++;
                else Assert.Fail();

                if (suffixTree.Contains(words[i])) Assert.Fail();
            }

            var previousWord = string.Empty;

            foreach (var word in suffixTree)
            {
                if (string.CompareOrdinal(previousWord, word) > 0) Assert.Fail();

                //System.Diagnostics.Trace.WriteLine(word);

                previousWord = word;
            }

            Assert.IsTrue(suffixTree.Count == words.Length - removedWords);
        }

        [TestMethod]
        public void CheckIfSuffixIsContained()
        {
            var suffixTree = new SuffixTree();

            var words = new string[]
            {
                "One", "one", "oNe", "two", "hello",
                "test", "here", "there", "?!??!?!?!?",
                "VeryVeryVeryLoooooooooooooooooong",
                ".k[2c3-9024g-u,9weg,ouimwt", "3q2tgwadh",
                "`+rv`+*1v+vt23*1`vt*1v", "!@#)(*^$!%@_",
                "  bum  ", "  bam  ", "  bum  bam  ",
                "1", "12", "123", "1234", "12345", "123456"
            };


            for (int i = 0; i < words.Length; i++)
            {
                suffixTree.Add(words[i]);

                for (int j = 0; j < words[i].Length; j++)
                {
                    if (!suffixTree.ContainsSuffix(words[i].Substring(j, words[i].Length - j))) Assert.Fail();
                }
                if (!suffixTree.Contains(words[i])) Assert.Fail();

            }

            Assert.IsTrue(suffixTree.Count == words.Length);
        }

        [TestMethod]
        public void GetWordsBySuffixAndCheckIfSorted()
        {
            var suffixTree = new SuffixTree();

            var words = new string[]
            {
                "afa suffix", "abaa suffix", "asd suffix",
                "ase suffix", "rfda suffix", "rtfs suffix",
                "efad suffix", "Abaa suffix", "fasrg suffix",
                "test suffix", "t3st suffix", "tEst suffix",
                "TEST suffix", "Test suffix", "T3st suffix",
                "Test123 suffix", "aheyj suffix", "gwecgs suffix",
                ".,gml;k suffix", "rbkp;r suffix", "wopger'lb suffix",
                "  zzz suffix", "^&*(& suffix", "кирилица suffix",
                "suffix not presented"
            };

            for (int i = 0; i < words.Length; i++)
            {
                if (suffixTree.Contains(words[i])) Assert.Fail();

                suffixTree.Add(words[i]);

                if (!suffixTree.Contains(words[i])) Assert.Fail();

            }

            var previousWord = string.Empty;
            int suffixWords = 0;

            foreach (var word in suffixTree.GetWordsBySuffix("suffix"))
            {
                if (string.CompareOrdinal(previousWord, word) > 0) Assert.Fail();

                System.Diagnostics.Trace.WriteLine(word);

                previousWord = word;
                suffixWords++;
            }

            Assert.IsTrue(suffixTree.Count == words.Length
                            && suffixWords < words.Length);
        }
    }
}
