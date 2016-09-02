using Microsoft.VisualStudio.TestTools.UnitTesting;
using DSA.DataStructures.Trees;

namespace DSAUnitTests.DataStructures.Trees
{
    [TestClass]
    public class TrieTests
    {
        [TestMethod]
        public void AddingAndCheckingIfContained()
        {
            var trie = new Trie();

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
                if (trie.Contains(words[i])) Assert.Fail();

                trie.Add(words[i]);

                if (!trie.Contains(words[i])) Assert.Fail();

            }

            Assert.IsTrue(trie.Count == words.Length);
        }

        [TestMethod]
        public void AddingAndRemovingSomeWordsAndCheckingIfContained()
        {
            var trie = new Trie();

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
                if (trie.Contains(words[i])) Assert.Fail();

                trie.Add(words[i]);

                if (!trie.Contains(words[i])) Assert.Fail();

            }

            if (trie.Count != words.Length) Assert.Fail();

            int removedWords = 0;

            for (int i = 0; i < words.Length; i += 2)
            {
                if (trie.Remove(words[i])) removedWords++;
                else Assert.Fail();

                if (trie.Contains(words[i])) Assert.Fail();
            }

            Assert.IsTrue(trie.Count == words.Length - removedWords);
        }

        [TestMethod]
        public void AddingAfterRemovingEverything()
        {
            var trie = new Trie();

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
                if (trie.Contains(words[i])) Assert.Fail();

                trie.Add(words[i]);

                if (!trie.Contains(words[i])) Assert.Fail();

            }

            if (trie.Count != words.Length) Assert.Fail();

            for (int i = 0; i < words.Length; i++)
            {
                if (!trie.Remove(words[i])) Assert.Fail();

                if (trie.Contains(words[i])) Assert.Fail();
            }

            if (trie.Count != 0) Assert.Fail();

            for (int i = 0; i < words.Length; i++)
            {
                if (trie.Contains(words[i])) Assert.Fail();

                trie.Add(words[i]);

                if (!trie.Contains(words[i])) Assert.Fail();

            }

            Assert.IsTrue(trie.Count == words.Length);
        }

        [TestMethod]
        public void AddingAfterClearingTrie()
        {
            var trie = new Trie();

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
                if (trie.Contains(words[i])) Assert.Fail();

                trie.Add(words[i]);

                if (!trie.Contains(words[i])) Assert.Fail();

            }

            if (trie.Count != words.Length) Assert.Fail();

            trie.Clear();

            if (trie.Count != 0) Assert.Fail();

            for (int i = 0; i < words.Length; i++)
            {
                if (trie.Contains(words[i])) Assert.Fail();

                trie.Add(words[i]);

                if (!trie.Contains(words[i])) Assert.Fail();

            }

            Assert.IsTrue(trie.Count == words.Length);
        }

        [TestMethod]
        public void CheckIfSortedAfterAdding()
        {
            var trie = new Trie();

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
                if (trie.Contains(words[i])) Assert.Fail();

                trie.Add(words[i]);

                if (!trie.Contains(words[i])) Assert.Fail();

            }

            var previousWord = string.Empty;

            foreach (var word in trie)
            {
                if (string.CompareOrdinal(previousWord, word) > 0) Assert.Fail();

                //System.Diagnostics.Trace.WriteLine(word);

                previousWord = word;
            }

            Assert.IsTrue(trie.Count == words.Length);
        }

        [TestMethod]
        public void CheckIfSortedAfterAddingAndRemovingSomeWords()
        {
            var trie = new Trie();

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
                if (trie.Contains(words[i])) Assert.Fail();

                trie.Add(words[i]);

                if (!trie.Contains(words[i])) Assert.Fail();

            }

            if (trie.Count != words.Length) Assert.Fail();

            int removedWords = 0;

            for (int i = 0; i < words.Length; i += 2)
            {
                if (trie.Remove(words[i])) removedWords++;
                else Assert.Fail();

                if (trie.Contains(words[i])) Assert.Fail();
            }

            var previousWord = string.Empty;

            foreach (var word in trie)
            {
                if (string.CompareOrdinal(previousWord, word) > 0) Assert.Fail();

                //System.Diagnostics.Trace.WriteLine(word);

                previousWord = word;
            }

            Assert.IsTrue(trie.Count == words.Length - removedWords);
        }
    }
}
