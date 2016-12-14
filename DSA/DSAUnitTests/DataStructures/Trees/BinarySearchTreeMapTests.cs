using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DSA.DataStructures.Trees;
using System.Diagnostics;

namespace DSAUnitTests.DataStructures.Trees
{
    [TestClass]
    public class BinarySearchTreeMapTests
    {
        [TestMethod]
        public void SortedElementsAfterAdding()
        {
            BinarySearchTreeMap<int, int> tree = new BinarySearchTreeMap<int, int>();

            int elementsCount = 10000;

            //To make it more balanced
            tree.Add(50, 50);
            tree.Add(75, 75);
            tree.Add(25, 25);

            //Adding every seventh number, then every fifth number,
            //every third and at last all numbers
            for (int i = 7; i > 0; i -= 2)
            {
                int el = 0;
                while (el < elementsCount)
                {
                    if (!tree.ContainsKey(el)) tree.Add(el, el);
                    el += i;
                }

            }

            int last = -1;
            int count = 0;
            bool elementsAreSorted = true;
            foreach (var item in tree)
            {
                if (last > item.Key)
                {
                    elementsAreSorted = false;
                }
                last = item.Key;
                count++;
            }

            Trace.WriteLine(tree.Count);

            Assert.IsTrue(tree.Count == elementsCount
                            && elementsAreSorted
                            && count == elementsCount);
        }

        [TestMethod]
        public void SortedElementsAfterAddingAndRemoving()
        {
            BinarySearchTreeMap<int, int> tree = new BinarySearchTreeMap<int, int>();

            int elementsCount = 10000;

            //To make it more balanced
            tree.Add(50, 50);
            tree.Add(75, 75);
            tree.Add(25, 25);

            //Adding every seventh number, then every fifth number,
            //every third and at last all numbers
            for (int i = 7; i > 0; i -= 2)
            {
                int el = 0;
                while (el < elementsCount)
                {
                    if (!tree.ContainsKey(el)) tree.Add(el, el);
                    el += i;
                }

            }

            bool removedEverything = true;

            //Removing every second number
            for (int i = 0; i < elementsCount; i += 2)
            {
                if (!tree.Remove(i)) removedEverything = false;
            }


            int last = -1;
            int count = 0;
            bool elementsAreSorted = true;
            foreach (var item in tree)
            {
                if (last > item.Key)
                {
                    elementsAreSorted = false;
                }
                last = item.Key;
                count++;
            }

            Trace.WriteLine(tree.Count);

            Assert.IsTrue(tree.Count == count
                            && elementsAreSorted
                            && removedEverything);
        }

        [TestMethod]
        public void RemoveAllExceptOne()
        {
            BinarySearchTreeMap<int, int> tree = new BinarySearchTreeMap<int, int>();

            int elementsCount = 10000;

            //To make it more balanced
            tree.Add(50, 50);
            tree.Add(75, 75);
            tree.Add(25, 25);

            //Adding every seventh number, then every fifth number,
            //every third and at last all numbers
            for (int i = 7; i > 0; i -= 2)
            {
                int el = 0;
                while (el < elementsCount)
                {
                    if (!tree.ContainsKey(el)) tree.Add(el, el);
                    el += i;
                }

            }

            bool removedEverything = true;

            for (int i = 0; i < elementsCount - 1; i++)
            {
                if (!tree.Remove(i)) removedEverything = false;
            }

            Assert.IsTrue(tree.Count == 1
                            && removedEverything
                            && tree.Root.Value == elementsCount - 1);
        }

        [TestMethod]
        public void RemoveRootEveryTimeUntilTreeElementsAreHalfed()
        {
            var tree = new BinarySearchTreeMap<int, int>();

            int elementsCount = 10000;

            //To make it more balanced
            tree.Add(50, 50);
            tree.Add(75, 75);
            tree.Add(25, 25);

            //Adding every seventh number, then every fifth number,
            //every third and at last all numbers
            for (int i = 7; i > 0; i -= 2)
            {
                int el = 0;
                while (el < elementsCount)
                {
                    if (!tree.ContainsKey(el))
                    {
                        tree.Add(el, el);
                    }
                    el += i;
                }

            }

            bool removedEverything = true;

            while (tree.Count > elementsCount / 2)
            {
                if (!tree.Remove(tree.Root.Value)) removedEverything = false;
            }

            int last = -1;
            int count = 0;
            bool elementsAreSorted = true;
            foreach (var item in tree)
            {
                if (last > item.Key)
                {
                    elementsAreSorted = false;
                }
                last = item.Key;
                count++;
            }

            Trace.WriteLine(tree.Count);

            Assert.IsTrue(tree.Count == count
                            && elementsAreSorted
                            && count == elementsCount / 2
                            && removedEverything);
        }

        [TestMethod]
        public void AddingAfterClearingTree()
        {
            var tree = new BinarySearchTreeMap<int, int>();

            int elementsCount = 10000;

            //Adding every seventh number, then every fifth number,
            //every third and at last all numbers
            for (int i = 7; i > 0; i -= 2)
            {
                int el = 0;
                while (el < elementsCount)
                {
                    if (!tree.ContainsKey(el))
                    {
                        tree.Add(el, el);
                    }
                    el += i;
                }

            }

            if (tree.Count != elementsCount) Assert.Fail();

            tree.Clear();

            if (tree.Count != 0) Assert.Fail();

            //Adding every seventh number, then every fifth number,
            //every third and at last all numbers
            for (int i = 7; i > 0; i -= 2)
            {
                int el = 0;
                while (el < elementsCount)
                {
                    if (!tree.ContainsKey(el))
                    {
                        tree.Add(el, el);
                    }
                    el += i;
                }

            }

            int last = -1;
            int count = 0;
            bool elementsAreSorted = true;
            foreach (var item in tree)
            {
                if (last > item.Key)
                {
                    elementsAreSorted = false;

                }
                last = item.Key;
                count++;
            }

            Assert.IsTrue(tree.Count == elementsCount
                            && elementsAreSorted
                            && count == elementsCount);
        }

        [TestMethod]
        public void AddingAfterRemovingAllElements()
        {
            var tree = new BinarySearchTreeMap<int, int>();

            int elementsCount = 10000;

            //Adding every seventh number, then every fifth number,
            //every third and at last all numbers
            for (int i = 7; i > 0; i -= 2)
            {
                int el = 0;
                while (el < elementsCount)
                {
                    if (!tree.ContainsKey(el))
                    {
                        tree.Add(el, el);
                    }
                    el += i;
                }

            }

            if (tree.Count != elementsCount) Assert.Fail();

            //Removing every seventh number, then every fifth number,
            //every third and at last all numbers
            for (int i = 7; i > 0; i -= 2)
            {
                int el = 0;
                while (el < elementsCount)
                {
                    tree.Remove(el);
                    el += i;
                }

            }

            if (tree.Count != 0) Assert.Fail();

            //Adding every seventh number, then every fifth number,
            //every third and at last all numbers
            for (int i = 7; i > 0; i -= 2)
            {
                int el = 0;
                while (el < elementsCount)
                {
                    if (!tree.ContainsKey(el))
                    {
                        tree.Add(el, el);
                    }
                    el += i;
                }

            }

            int last = -1;
            int count = 0;
            bool elementsAreSorted = true;
            foreach (var item in tree)
            {
                if (last > item.Key)
                {
                    elementsAreSorted = false;

                }
                last = item.Key;
                count++;
            }

            Assert.IsTrue(tree.Count == elementsCount
                            && elementsAreSorted
                            && count == elementsCount);
        }

        [TestMethod]
        public void CheckIfNodeIsInvalidatedAfterClearingAndAfterRemoval()
        {
            var tree = new BinarySearchTreeMap<int, int>();

            tree.Add(2, 2);
            tree.Add(1, 1);
            tree.Add(3, 3);

            // Tree looks like this:
            //   2
            //  / \
            // 1   3

            var node1 = tree.Root.Left;
            var node2 = tree.Root;
            var node3 = tree.Root.Right;

            tree.Remove(2);
            if (node2.Left != null || node2.Right != null) Assert.Fail("2");

            tree.Remove(3);
            if (node3.Left != null || node3.Right != null) Assert.Fail("3");

            tree.Remove(1);
            if (node1.Left != null || node1.Right != null) Assert.Fail("1");

            tree.Add(2, 2);
            tree.Add(1, 1);
            tree.Add(3, 3);

            node1 = tree.Root.Left;
            node2 = tree.Root;
            node3 = tree.Root.Right;

            tree.Clear();

            Assert.IsTrue(node1.Left == null && node1.Right == null
                            && node2.Left == null && node2.Right == null
                            && node3.Left == null && node3.Right == null
                            && tree.Root == null
                            && tree.Count == 0);
        }

        [TestMethod]
        public void AddingElementsWithIndexer()
        {
            BinarySearchTreeMap<int, int> tree = new BinarySearchTreeMap<int, int>();

            int elementsCount = 10000;

            //To make it more balanced
            tree[50] = 50;
            tree[75] = 75;
            tree[25] = 25;

            //Adding every seventh number, then every fifth number,
            //every third and at last all numbers
            for (int i = 7; i > 0; i -= 2)
            {
                int el = 0;
                while (el < elementsCount)
                {
                    if (!tree.ContainsKey(el)) tree[el] = el;
                    el += i;
                }
            }

            int last = -1;
            int count = 0;
            bool elementsAreSorted = true;
            foreach (var item in tree)
            {
                if (last > item.Key)
                {
                    elementsAreSorted = false;
                }
                last = item.Key;
                count++;
            }

            Trace.WriteLine(tree.Count);

            Assert.IsTrue(tree.Count == elementsCount
                            && elementsAreSorted
                            && count == elementsCount);
        }

        [TestMethod]
        public void UpdatingElementsWithIndexerUsingTryGetValueMethodToGetValue()
        {
            BinarySearchTreeMap<int, int> tree = new BinarySearchTreeMap<int, int>();

            int elementsCount = 10000;

            //To make it more balanced
            tree[50] = 50;
            tree[75] = 75;
            tree[25] = 25;

            //Adding every seventh number, then every fifth number,
            //every third and at last all numbers
            for (int i = 7; i > 0; i -= 2)
            {
                int el = 0;
                while (el < elementsCount)
                {
                    if (!tree.ContainsKey(el)) tree.Add(el, el);
                    el += i;
                }

            }

            bool removedEverything = true;

            //Removing every second number
            for (int i = 0; i < elementsCount; i += 2)
            {
                if (!tree.Remove(i)) removedEverything = false;
            }

            // Make all values negative
            for (int i = 0; i < elementsCount; i++)
            {
                int value;
                if (tree.TryGetValue(i, out value))
                {
                    tree[value] = -value;
                }
            }


            int last = -1;
            int count = 0;
            bool elementsAreSorted = true;
            foreach (var item in tree)
            {
                if (item.Value > 0) Assert.Fail();
                if (last > item.Key)
                {
                    elementsAreSorted = false;
                }
                last = item.Key;
                count++;
            }

            Trace.WriteLine(tree.Count);

            Assert.IsTrue(tree.Count == count
                            && elementsAreSorted
                            && removedEverything);
        }

        [TestMethod]
        public void ContatinsValueBeforeAndAfterUpdatingValue()
        {
            BinarySearchTreeMap<int, int> tree = new BinarySearchTreeMap<int, int>();

            int elementsCount = 1000;

            //To make it more balanced
            tree.Add(50, 50);
            tree.Add(75, 75);
            tree.Add(25, 25);

            //Adding every seventh number, then every fifth number,
            //every third and at last all numbers
            for (int i = 7; i > 0; i -= 2)
            {
                int el = 0;
                while (el < elementsCount)
                {
                    if (!tree.ContainsKey(el)) tree.Add(el, el);
                    el += i;
                }

            }

            // Check if values are contained, make them negative and check again. Skip zero...
            for (int i = 1; i < elementsCount; i++)
            {
                if (!tree.ContainsValue(i)) Assert.Fail();
                tree[i] = -i;
                if (tree.ContainsValue(i)) Assert.Fail();
                if (!tree.ContainsValue(-i)) Assert.Fail();
            }

            int last = -1;
            int count = 0;
            bool elementsAreSorted = true;
            foreach (var item in tree)
            {
                if (last > item.Key)
                {
                    elementsAreSorted = false;
                }
                last = item.Key;
                count++;
            }

            Trace.WriteLine(tree.Count);

            Assert.IsTrue(tree.Count == elementsCount
                            && elementsAreSorted
                            && count == elementsCount);
        }
    }
}
