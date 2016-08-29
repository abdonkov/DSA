using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DSA.DataStructures.Trees;
using System.Diagnostics;
using System.Linq;

namespace DSAUnitTests.DataStructures.Trees
{
    [TestClass]
    public class BinarySearchTreeTests
    {
        [TestMethod]
        public void SortedElementsAfterAdding()
        {
            BinarySearchTree<int> tree = new BinarySearchTree<int>();

            int elementsCount = 10000;

            //To make it more balanced
            tree.Add(50);
            tree.Add(75);
            tree.Add(25);

            //Adding every seventh number, then every fifth number,
            //every third and at last all numbers
            for (int i = 7; i > 0; i -= 2)
            {
                int el = 0;
                while (el < elementsCount)
                {
                    if (!tree.Contains(el)) tree.Add(el);
                    el += i;
                }

            }

            int last = -1;
            int count = 0;
            bool elementsAreSorted = true;
            foreach (var item in tree)
            {
                if (last > item)
                {
                    elementsAreSorted = false;
                }
                last = item;
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
            BinarySearchTree<int> tree = new BinarySearchTree<int>();

            int elementsCount = 10000;

            //To make it more balanced
            tree.Add(50);
            tree.Add(75);
            tree.Add(25);

            //Adding every seventh number, then every fifth number,
            //every third and at last all numbers
            for (int i = 7; i > 0; i -= 2)
            {
                int el = 0;
                while (el < elementsCount)
                {
                    if (!tree.Contains(el)) tree.Add(el);
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
                if (last > item)
                {
                    elementsAreSorted = false;
                }
                last = item;
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
            BinarySearchTree<int> tree = new BinarySearchTree<int>();

            int elementsCount = 10000;

            //To make it more balanced
            tree.Add(50);
            tree.Add(75);
            tree.Add(25);

            //Adding every seventh number, then every fifth number,
            //every third and at last all numbers
            for (int i = 7; i > 0; i -= 2)
            {
                int el = 0;
                while (el < elementsCount)
                {
                    if (!tree.Contains(el)) tree.Add(el);
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
            var tree = new BinarySearchTree<int>();

            int elementsCount = 10000;

            //To make it more balanced
            tree.Add(50);
            tree.Add(75);
            tree.Add(25);

            //Adding every seventh number, then every fifth number,
            //every third and at last all numbers
            for (int i = 7; i > 0; i -= 2)
            {
                int el = 0;
                while (el < elementsCount)
                {
                    if (!tree.Contains(el))
                    {
                        tree.Add(el);
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
                if (last > item)
                {
                    elementsAreSorted = false;
                }
                last = item;
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
            var tree = new BinarySearchTree<int>();

            int elementsCount = 10000;

            //Adding every seventh number, then every fifth number,
            //every third and at last all numbers
            for (int i = 7; i > 0; i -= 2)
            {
                int el = 0;
                while (el < elementsCount)
                {
                    if (!tree.Contains(el))
                    {
                        tree.Add(el);
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
                    if (!tree.Contains(el))
                    {
                        tree.Add(el);
                    }
                    el += i;
                }

            }

            int last = -1;
            int count = 0;
            bool elementsAreSorted = true;
            foreach (var item in tree)
            {
                if (last > item)
                {
                    elementsAreSorted = false;

                }
                last = item;
                count++;
            }

            Assert.IsTrue(tree.Count == elementsCount
                            && elementsAreSorted
                            && count == elementsCount);
        }

        [TestMethod]
        public void AddingAfterRemovingAllElements()
        {
            var tree = new BinarySearchTree<int>();

            int elementsCount = 10000;

            //Adding every seventh number, then every fifth number,
            //every third and at last all numbers
            for (int i = 7; i > 0; i -= 2)
            {
                int el = 0;
                while (el < elementsCount)
                {
                    if (!tree.Contains(el))
                    {
                        tree.Add(el);
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
                    if (!tree.Contains(el))
                    {
                        tree.Add(el);
                    }
                    el += i;
                }

            }

            int last = -1;
            int count = 0;
            bool elementsAreSorted = true;
            foreach (var item in tree)
            {
                if (last > item)
                {
                    elementsAreSorted = false;

                }
                last = item;
                count++;
            }

            Assert.IsTrue(tree.Count == elementsCount
                            && elementsAreSorted
                            && count == elementsCount);
        }

        [TestMethod]
        public void CheckIfNodeIsInvalidatedAfterClearingAndAfterRemoval()
        {
            var tree = new BinarySearchTree<int>();

            tree.Add(2);
            tree.Add(1);
            tree.Add(3);

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

            tree.Add(2);
            tree.Add(1);
            tree.Add(3);

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
    }
}
