using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DSA.DataStructures.Trees;

namespace DSAUnitTests.DataStructures.Trees
{
    [TestClass]
    public class SuffixTreeMapTests
    {
        [TestMethod]
        public void AddingAndCheckingIfContained()
        {
            var suffixTree = new SuffixTreeMap<string>();

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
                if (suffixTree.ContainsWord(words[i])) Assert.Fail();

                suffixTree.Add(words[i], words[i]);

                if (!suffixTree.ContainsWord(words[i])) Assert.Fail();

            }

            Assert.IsTrue(suffixTree.Count == words.Length);
        }

        [TestMethod]
        public void AddingAndRemovingSomeWordsAndCheckingIfContained()
        {
            var suffixTree = new SuffixTreeMap<string>();

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
                if (suffixTree.ContainsWord(words[i])) Assert.Fail();

                suffixTree.Add(words[i], words[i]);

                if (!suffixTree.ContainsWord(words[i])) Assert.Fail();

            }

            if (suffixTree.Count != words.Length) Assert.Fail();

            int removedWords = 0;

            for (int i = 0; i < words.Length; i += 2)
            {
                if (suffixTree.Remove(words[i])) removedWords++;
                else Assert.Fail();

                if (suffixTree.ContainsWord(words[i])) Assert.Fail();
            }

            Assert.IsTrue(suffixTree.Count == words.Length - removedWords);
        }

        [TestMethod]
        public void AddingAfterRemovingEverything()
        {
            var suffixTree = new SuffixTreeMap<string>();

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
                if (suffixTree.ContainsWord(words[i])) Assert.Fail();

                suffixTree.Add(words[i], words[i]);

                if (!suffixTree.ContainsWord(words[i])) Assert.Fail();

            }

            if (suffixTree.Count != words.Length) Assert.Fail();

            for (int i = 0; i < words.Length; i++)
            {
                if (!suffixTree.Remove(words[i])) Assert.Fail();

                if (suffixTree.ContainsWord(words[i])) Assert.Fail();
            }

            if (suffixTree.Count != 0) Assert.Fail();

            for (int i = 0; i < words.Length; i++)
            {
                if (suffixTree.ContainsWord(words[i])) Assert.Fail();

                suffixTree.Add(words[i], words[i]);

                if (!suffixTree.ContainsWord(words[i])) Assert.Fail();

            }

            Assert.IsTrue(suffixTree.Count == words.Length);
        }

        [TestMethod]
        public void AddingAfterClearingSuffixTreeMap()
        {
            var suffixTree = new SuffixTreeMap<string>();

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
                if (suffixTree.ContainsWord(words[i])) Assert.Fail();

                suffixTree.Add(words[i], words[i]);

                if (!suffixTree.ContainsWord(words[i])) Assert.Fail();

            }

            if (suffixTree.Count != words.Length) Assert.Fail();

            suffixTree.Clear();

            if (suffixTree.Count != 0) Assert.Fail();

            for (int i = 0; i < words.Length; i++)
            {
                if (suffixTree.ContainsWord(words[i])) Assert.Fail();

                suffixTree.Add(words[i], words[i]);

                if (!suffixTree.ContainsWord(words[i])) Assert.Fail();

            }

            Assert.IsTrue(suffixTree.Count == words.Length);
        }

        [TestMethod]
        public void CheckIfSortedAfterAdding()
        {
            var suffixTree = new SuffixTreeMap<string>();

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
                if (suffixTree.ContainsWord(words[i])) Assert.Fail();

                suffixTree.Add(words[i], words[i]);

                if (!suffixTree.ContainsWord(words[i])) Assert.Fail();

            }

            var previousWord = string.Empty;

            foreach (var word in suffixTree)
            {
                if (string.CompareOrdinal(previousWord, word.Key) > 0) Assert.Fail();

                //System.Diagnostics.Trace.WriteLine(word);

                previousWord = word.Key;
            }

            Assert.IsTrue(suffixTree.Count == words.Length);
        }

        [TestMethod]
        public void CheckIfSortedAfterAddingAndRemovingSomeWords()
        {
            var suffixTree = new SuffixTreeMap<string>();

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
                if (suffixTree.ContainsWord(words[i])) Assert.Fail();

                suffixTree.Add(words[i], words[i]);

                if (!suffixTree.ContainsWord(words[i])) Assert.Fail();

            }

            if (suffixTree.Count != words.Length) Assert.Fail();

            int removedWords = 0;

            for (int i = 0; i < words.Length; i += 2)
            {
                if (suffixTree.Remove(words[i])) removedWords++;
                else Assert.Fail();

                if (suffixTree.ContainsWord(words[i])) Assert.Fail();
            }

            var previousWord = string.Empty;

            foreach (var word in suffixTree)
            {
                if (string.CompareOrdinal(previousWord, word.Key) > 0) Assert.Fail();

                //System.Diagnostics.Trace.WriteLine(word);

                previousWord = word.Key;
            }

            Assert.IsTrue(suffixTree.Count == words.Length - removedWords);
        }

        [TestMethod]
        public void CheckIfSuffixIsContained()
        {
            var suffixTree = new SuffixTreeMap<string>();

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
                suffixTree.Add(words[i], words[i]);

                for (int j = 0; j < words[i].Length; j++)
                {
                    if (!suffixTree.ContainsSuffix(words[i].Substring(j, words[i].Length - j))) Assert.Fail();
                }
                if (!suffixTree.ContainsWord(words[i])) Assert.Fail();

            }

            Assert.IsTrue(suffixTree.Count == words.Length);
        }

        [TestMethod]
        public void GetWordsBySuffixAndCheckIfSorted()
        {
            var suffixTree = new SuffixTreeMap<string>();

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
                if (suffixTree.ContainsWord(words[i])) Assert.Fail();

                suffixTree.Add(words[i], words[i]);

                if (!suffixTree.ContainsWord(words[i])) Assert.Fail();

            }

            var previousWord = string.Empty;
            int suffixWords = 0;

            foreach (var word in suffixTree.GetWordsBySuffix("suffix"))
            {
                if (string.CompareOrdinal(previousWord, word.Key) > 0) Assert.Fail();

                System.Diagnostics.Trace.WriteLine(word);

                previousWord = word.Key;
                suffixWords++;
            }

            Assert.IsTrue(suffixTree.Count == words.Length
                            && suffixWords < words.Length);
        }

        [TestMethod]
        public void AddingElementsWithIndexer()
        {
            var suffixTree = new SuffixTreeMap<string>();

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
                if (suffixTree.ContainsWord(words[i])) Assert.Fail();

                suffixTree[words[i]] = words[i];

                if (!suffixTree.ContainsWord(words[i])) Assert.Fail();

            }

            Assert.IsTrue(suffixTree.Count == words.Length);
        }

        [TestMethod]
        public void UpdatingElementsWithIndexerUsingTryGetValueMethodToGetValue()
        {
            var suffixTree = new SuffixTreeMap<string>();

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
                if (suffixTree.ContainsWord(words[i])) Assert.Fail("1");

                suffixTree.Add(words[i], words[i]);

                if (!suffixTree.ContainsWord(words[i])) Assert.Fail("2");

            }

            Assert.IsTrue(suffixTree.Count == words.Length);

            // Update all values
            for (int i = 0; i < words.Length; i++)
            {
                string value;
                if (suffixTree.TryGetValue(words[i], out value))
                {
                    suffixTree[value] = value + "updated";
                }
            }

            int count = 0;
            foreach (var kvp in suffixTree)
            {
                if (!object.Equals(kvp.Key + "updated", kvp.Value)) Assert.Fail("3");
                count++;
            }

            Assert.IsTrue(suffixTree.Count == count
                            && suffixTree.Count == words.Length);
        }

        [TestMethod]
        public void ContatinsValueBeforeAndAfterUpdatingValue()
        {
            var suffixTree = new SuffixTreeMap<string>();

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
                if (suffixTree.ContainsWord(words[i])) Assert.Fail();

                suffixTree.Add(words[i], words[i]);

                if (!suffixTree.ContainsWord(words[i])) Assert.Fail();

            }

            Assert.IsTrue(suffixTree.Count == words.Length);

            // Update all values
            for (int i = 0; i < words.Length; i++)
            {
                string value;
                if (suffixTree.TryGetValue(words[i], out value))
                {
                    if (!suffixTree.ContainsValue(value)) Assert.Fail();
                    if (suffixTree.ContainsValue(value + "updated")) Assert.Fail();
                    suffixTree[value] = value + "updated";
                    if (suffixTree.ContainsValue(value)) Assert.Fail();
                    if (!suffixTree.ContainsValue(value + "updated")) Assert.Fail();
                }
            }

            int count = 0;
            foreach (var kvp in suffixTree)
            {
                if (!object.Equals(kvp.Key + "updated", kvp.Value)) Assert.Fail();
                count++;
            }

            Assert.IsTrue(suffixTree.Count == count
                            && suffixTree.Count == words.Length);
        }
    }
}
