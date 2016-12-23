using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DSA.DataStructures.Trees;

namespace DSAUnitTests.DataStructures.Trees
{
    [TestClass]
    public class TrieMapTests
    {
        [TestMethod]
        public void AddingAndCheckingIfContained()
        {
            var trie = new TrieMap<string>();

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
                if (trie.ContainsWord(words[i])) Assert.Fail();

                trie.Add(words[i], words[i]);

                if (!trie.ContainsWord(words[i])) Assert.Fail();

            }

            Assert.IsTrue(trie.Count == words.Length);
        }

        [TestMethod]
        public void AddingAndRemovingSomeWordsAndCheckingIfContained()
        {
            var trie = new TrieMap<string>();

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
                if (trie.ContainsWord(words[i])) Assert.Fail();

                trie.Add(words[i], words[i]);

                if (!trie.ContainsWord(words[i])) Assert.Fail();

            }

            if (trie.Count != words.Length) Assert.Fail();

            int removedWords = 0;

            for (int i = 0; i < words.Length; i += 2)
            {
                if (trie.Remove(words[i])) removedWords++;
                else Assert.Fail();

                if (trie.ContainsWord(words[i])) Assert.Fail();
            }

            Assert.IsTrue(trie.Count == words.Length - removedWords);
        }

        [TestMethod]
        public void AddingAfterRemovingEverything()
        {
            var trie = new TrieMap<string>();

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
                if (trie.ContainsWord(words[i])) Assert.Fail();

                trie.Add(words[i], words[i]);

                if (!trie.ContainsWord(words[i])) Assert.Fail();

            }

            if (trie.Count != words.Length) Assert.Fail();

            for (int i = 0; i < words.Length; i++)
            {
                if (!trie.Remove(words[i])) Assert.Fail();

                if (trie.ContainsWord(words[i])) Assert.Fail();
            }

            if (trie.Count != 0) Assert.Fail();

            for (int i = 0; i < words.Length; i++)
            {
                if (trie.ContainsWord(words[i])) Assert.Fail();

                trie.Add(words[i], words[i]);

                if (!trie.ContainsWord(words[i])) Assert.Fail();

            }

            Assert.IsTrue(trie.Count == words.Length);
        }

        [TestMethod]
        public void AddingAfterClearingTrieMap()
        {
            var trie = new TrieMap<string>();

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
                if (trie.ContainsWord(words[i])) Assert.Fail();

                trie.Add(words[i], words[i]);

                if (!trie.ContainsWord(words[i])) Assert.Fail();

            }

            if (trie.Count != words.Length) Assert.Fail();

            trie.Clear();

            if (trie.Count != 0) Assert.Fail();

            for (int i = 0; i < words.Length; i++)
            {
                if (trie.ContainsWord(words[i])) Assert.Fail();

                trie.Add(words[i], words[i]);

                if (!trie.ContainsWord(words[i])) Assert.Fail();

            }

            Assert.IsTrue(trie.Count == words.Length);
        }

        [TestMethod]
        public void CheckIfSortedAfterAdding()
        {
            var trie = new TrieMap<string>();

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
                if (trie.ContainsWord(words[i])) Assert.Fail();

                trie.Add(words[i], words[i]);

                if (!trie.ContainsWord(words[i])) Assert.Fail();

            }

            var previousWord = string.Empty;

            foreach (var word in trie)
            {
                if (string.CompareOrdinal(previousWord, word.Key) > 0) Assert.Fail();

                //System.Diagnostics.Trace.WriteLine(word);

                previousWord = word.Key;
            }

            Assert.IsTrue(trie.Count == words.Length);
        }

        [TestMethod]
        public void CheckIfSortedAfterAddingAndRemovingSomeWords()
        {
            var trie = new TrieMap<string>();

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
                if (trie.ContainsWord(words[i])) Assert.Fail();

                trie.Add(words[i], words[i]);

                if (!trie.ContainsWord(words[i])) Assert.Fail();

            }

            if (trie.Count != words.Length) Assert.Fail();

            int removedWords = 0;

            for (int i = 0; i < words.Length; i += 2)
            {
                if (trie.Remove(words[i])) removedWords++;
                else Assert.Fail();

                if (trie.ContainsWord(words[i])) Assert.Fail();
            }

            var previousWord = string.Empty;

            foreach (var word in trie)
            {
                if (string.CompareOrdinal(previousWord, word.Key) > 0) Assert.Fail();

                //System.Diagnostics.Trace.WriteLine(word);

                previousWord = word.Key;
            }

            Assert.IsTrue(trie.Count == words.Length - removedWords);
        }

        [TestMethod]
        public void CheckIfPrefixIsContained()
        {
            var trie = new TrieMap<string>();

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
                trie.Add(words[i], words[i]);

                for (int j = 1; j <= words[i].Length; j++)
                {
                    if (!trie.ContainsPrefix(words[i].Substring(0, j))) Assert.Fail();
                }
                if (!trie.ContainsWord(words[i])) Assert.Fail();

            }

            Assert.IsTrue(trie.Count == words.Length);
        }

        [TestMethod]
        public void GetWordsByPrefixAndCheckIfSorted()
        {
            var trie = new TrieMap<string>();

            var words = new string[]
            {
                "prefix afa", "prefix abaa", "prefix asd",
                "prefix ase", "prefix rfda", "prefix rtfs",
                "prefix efad", "prefix Abaa", "prefix fasrg",
                "prefix test", "prefix t3st", "prefix tEst",
                "prefix TEST", "prefix Test", "prefix T3st",
                "prefix Test123", "prefix aheyj", "prefix gwecgs",
                "prefix .,gml;k", "prefix rbkp;r", "prefix wopger'lb",
                "prefix   zzz", "prefix ^&*(&", "prefix кирилица",
                "no prefix"
            };

            for (int i = 0; i < words.Length; i++)
            {
                if (trie.ContainsWord(words[i])) Assert.Fail();

                trie.Add(words[i], words[i]);

                if (!trie.ContainsWord(words[i])) Assert.Fail();

            }

            var previousWord = string.Empty;
            int prefixWords = 0;

            foreach (var word in trie.GetWordsByPrefix("prefix"))
            {
                if (string.CompareOrdinal(previousWord, word.Key) > 0) Assert.Fail();

                System.Diagnostics.Trace.WriteLine(word);

                previousWord = word.Key;
                prefixWords++;
            }

            Assert.IsTrue(trie.Count == words.Length
                            && prefixWords < words.Length);
        }

        [TestMethod]
        public void AddingElementsWithIndexer()
        {
            var trie = new TrieMap<string>();

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
                if (trie.ContainsWord(words[i])) Assert.Fail();

                trie[words[i]] = words[i];

                if (!trie.ContainsWord(words[i])) Assert.Fail();

            }

            Assert.IsTrue(trie.Count == words.Length);
        }

        [TestMethod]
        public void UpdatingElementsWithIndexerUsingTryGetValueMethodToGetValue()
        {
            var trie = new TrieMap<string>();

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
                if (trie.ContainsWord(words[i])) Assert.Fail("1");

                trie.Add(words[i], words[i]);

                if (!trie.ContainsWord(words[i])) Assert.Fail("2");

            }

            Assert.IsTrue(trie.Count == words.Length);

            // Update all values
            for (int i = 0; i < words.Length; i++)
            {
                string value;
                if (trie.TryGetValue(words[i], out value))
                {
                    trie[value] = value + "updated";
                }
            }

            int count = 0;
            foreach (var kvp in trie)
            {
                if (!object.Equals(kvp.Key + "updated", kvp.Value)) Assert.Fail("3");
                count++;
            }

            Assert.IsTrue(trie.Count == count
                            && trie.Count == words.Length);
        }

        [TestMethod]
        public void ContatinsValueBeforeAndAfterUpdatingValue()
        {
            var trie = new TrieMap<string>();

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
                if (trie.ContainsWord(words[i])) Assert.Fail();

                trie.Add(words[i], words[i]);

                if (!trie.ContainsWord(words[i])) Assert.Fail();

            }

            Assert.IsTrue(trie.Count == words.Length);

            // Update all values
            for (int i = 0; i < words.Length; i++)
            {
                string value;
                if (trie.TryGetValue(words[i], out value))
                {
                    if (!trie.ContainsValue(value)) Assert.Fail();
                    if (trie.ContainsValue(value + "updated")) Assert.Fail();
                    trie[value] = value + "updated";
                    if (trie.ContainsValue(value)) Assert.Fail();
                    if (!trie.ContainsValue(value + "updated")) Assert.Fail();
                }
            }

            int count = 0;
            foreach (var kvp in trie)
            {
                if (!object.Equals(kvp.Key + "updated", kvp.Value)) Assert.Fail();
                count++;
            }

            Assert.IsTrue(trie.Count == count
                            && trie.Count == words.Length);
        }
    }
}
