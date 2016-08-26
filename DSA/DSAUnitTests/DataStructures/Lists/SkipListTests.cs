using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DSA.DataStructures.Lists;
using System.Diagnostics;
using System.Linq;

namespace DSAUnitTests.DataStructures.Lists
{
    [TestClass]
    public class SkipListTests
    {
        [TestMethod]
        public void AddingOneMillionInts()
        {
            var list = new SkipList<int>();

            for (int i = 0; i < 1000000; i++)
            {
                list.Add(i);
            }

            Trace.WriteLine("List height: " + list.Height);

            Assert.IsTrue(list.Count == 1000000);
        }

        [TestMethod]
        public void SortedElementsAfterAdding()
        {
            var list = new SkipList<int>();

            int elementsCount = 100000;

            //Adding every seventh number, then every fifth number,
            //every third and at last all numbers
            for (int i = 7; i > 0; i -= 2)
            {
                int el = 0;
                while (el < elementsCount)
                {
                    if (!list.Contains(el))
                    {
                        list.Add(el);
                    }
                    el += i;
                }

            }

            int last = -1;
            int count = 0;
            bool elementsAreSorted = true;
            foreach (var item in list)
            {
                if (last > item)
                {
                    elementsAreSorted = false;

                }
                last = item;
                count++;
            }

            Assert.IsTrue(list.Count == elementsCount
                            && elementsAreSorted
                            && count == elementsCount);
        }

        [TestMethod]
        public void SortedElementsAfterAddingAndRemoving()
        {
            var list = new SkipList<int>();

            int elementsCount = 100000;

            //Adding every seventh number, then every fifth number,
            //every third and at last all numbers
            for (int i = 7; i > 0; i -= 2)
            {
                int el = 0;
                while (el < elementsCount)
                {
                    if (!list.Contains(el)) list.Add(el);
                    el += i;
                }

            }

            bool removedEverything = true;

            //Removing every second number
            for (int i = 0; i < elementsCount; i += 2)
            {
                if (!list.Remove(i)) removedEverything = false;
            }


            int last = -1;
            int count = 0;
            bool elementsAreSorted = true;
            foreach (var item in list)
            {
                if (last > item)
                {
                    elementsAreSorted = false;
                }
                last = item;
                count++;
            }

            Trace.WriteLine(list.Count);

            Assert.IsTrue(list.Count == count
                            && elementsAreSorted
                            && removedEverything);
        }

        [TestMethod]
        public void RemoveAllExceptOne()
        {
            var list = new SkipList<int>();

            int elementsCount = 100000;

            //Adding every seventh number, then every fifth number,
            //every third and at last all numbers
            for (int i = 7; i > 0; i -= 2)
            {
                int el = 0;
                while (el < elementsCount)
                {
                    if (!list.Contains(el)) list.Add(el);
                    el += i;
                }

            }

            bool removedEverything = true;

            for (int i = 0; i < elementsCount - 1; i++)
            {
                if (!list.Remove(i)) removedEverything = false;
            }

            Assert.IsTrue(list.Count == 1
                            && removedEverything
                            && list.Head[0].Value == elementsCount - 1);
        }

        [TestMethod]
        public void AddingAfterRemovingAllElements()
        {
            var list = new SkipList<int>();

            int elementsCount = 100000;

            //Adding every seventh number, then every fifth number,
            //every third and at last all numbers
            for (int i = 7; i > 0; i -= 2)
            {
                int el = 0;
                while (el < elementsCount)
                {
                    if (!list.Contains(el))
                    {
                        list.Add(el);
                    }
                    el += i;
                }

            }

            if (list.Count != elementsCount) Assert.Fail();

            //Removing every seventh number, then every fifth number,
            //every third and at last all numbers
            for (int i = 7; i > 0; i -= 2)
            {
                int el = 0;
                while (el < elementsCount)
                {
                    list.Remove(el);
                    el += i;
                }

            }

            if (list.Count != 0) Assert.Fail();

            //Adding every seventh number, then every fifth number,
            //every third and at last all numbers
            for (int i = 7; i > 0; i -= 2)
            {
                int el = 0;
                while (el < elementsCount)
                {
                    if (!list.Contains(el))
                    {
                        list.Add(el);
                    }
                    el += i;
                }

            }

            int last = -1;
            int count = 0;
            bool elementsAreSorted = true;
            foreach (var item in list)
            {
                if (last > item)
                {
                    elementsAreSorted = false;

                }
                last = item;
                count++;
            }

            Assert.IsTrue(list.Count == elementsCount
                            && elementsAreSorted
                            && count == elementsCount);
        }

        [TestMethod]
        public void AddingAfterClearingList()
        {
            var list = new SkipList<int>();

            int elementsCount = 100000;

            //Adding every seventh number, then every fifth number,
            //every third and at last all numbers
            for (int i = 7; i > 0; i -= 2)
            {
                int el = 0;
                while (el < elementsCount)
                {
                    if (!list.Contains(el))
                    {
                        list.Add(el);
                    }
                    el += i;
                }

            }

            if (list.Count != elementsCount) Assert.Fail();

            list.Clear();

            if (list.Count != 0) Assert.Fail();

            //Adding every seventh number, then every fifth number,
            //every third and at last all numbers
            for (int i = 7; i > 0; i -= 2)
            {
                int el = 0;
                while (el < elementsCount)
                {
                    if (!list.Contains(el))
                    {
                        list.Add(el);
                    }
                    el += i;
                }

            }

            int last = -1;
            int count = 0;
            bool elementsAreSorted = true;
            foreach (var item in list)
            {
                if (last > item)
                {
                    elementsAreSorted = false;

                }
                last = item;
                count++;
            }

            Assert.IsTrue(list.Count == elementsCount
                            && elementsAreSorted
                            && count == elementsCount);
        }

        [TestMethod]
        public void CheckIfNodeIsInvalidatedAfterRemoval()
        {
            var list = new SkipList<int>();

            int elementsCount = 100000;

            //Adding every seventh number, then every fifth number,
            //every third and at last all numbers
            for (int i = 7; i > 0; i -= 2)
            {
                int el = 0;
                while (el < elementsCount)
                {
                    if (!list.Contains(el))
                    {
                        list.Add(el);
                    }
                    el += i;
                }

            }

            

            var firstNode = list.Head[0];

            var lastNode = list.Head;

            while (lastNode[0] != null)
            {
                lastNode = lastNode[0];
            }

            list.Remove(0);
            list.Remove(elementsCount - 1);

            int last = -1;
            int count = 0;
            bool elementsAreSorted = true;
            foreach (var item in list)
            {
                if (last > item)
                {
                    elementsAreSorted = false;

                }
                last = item;
                count++;
            }

            Assert.IsTrue(list.Count == elementsCount - 2
                            && elementsAreSorted
                            && count == list.Count
                            && firstNode.Height == 0
                            && lastNode.Height == 0);
        }
    }
}
