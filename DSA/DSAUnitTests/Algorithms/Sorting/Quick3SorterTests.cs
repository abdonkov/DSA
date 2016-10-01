using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using DSA.Algorithms.Sorting;

namespace DSAUnitTests.Algorithms.Sorting
{
    [TestClass]
    public class Quick3SorterTests
    {
        [TestMethod]
        public void SortingInAscendingOrderAndCheckingIfSorted()
        {
            var list = new List<int>();

            int minElement = -50000;
            int maxElement = 50000;

            int addedElements = 0;

            //Adding every seventh number, then every fifth number,
            //every third and at last all numbers
            //NOTE: some items are added more than once
            for (int i = 7; i > 0; i -= 2)
            {
                int el = minElement;
                while (el <= maxElement)
                {
                    list.Add(el);
                    addedElements++;
                    el += i;
                }
            }

            list.Quick3Sort();

            var last = int.MinValue;
            foreach (var item in list)
            {
                if (last > item) Assert.Fail();

                last = item;
            }
        }

        [TestMethod]
        public void SortingInDescendingOrderAndCheckingIfSorted()
        {
            var list = new List<int>();

            int minElement = -50000;
            int maxElement = 50000;

            int addedElements = 0;

            //Adding every seventh number, then every fifth number,
            //every third and at last all numbers
            //NOTE: some items are added more than once
            for (int i = 7; i > 0; i -= 2)
            {
                int el = maxElement;
                while (el >= minElement)
                {
                    list.Add(el);
                    addedElements++;
                    el -= i;
                }
            }

            list.Quick3SortDescending();

            var last = int.MaxValue;
            foreach (var item in list)
            {
                if (last < item) Assert.Fail();

                last = item;
            }
        }

        [TestMethod]
        public void SortingInAscendingOrderUsingAComparisonAndCheckingIfSorted()
        {
            var list = new List<int>();

            int minElement = -50000;
            int maxElement = 50000;

            int addedElements = 0;

            //Adding every seventh number, then every fifth number,
            //every third and at last all numbers
            //NOTE: some items are added more than once
            for (int i = 7; i > 0; i -= 2)
            {
                int el = minElement;
                while (el <= maxElement)
                {
                    list.Add(el);
                    addedElements++;
                    el += i;
                }
            }

            list.Quick3Sort((x, y) => x.CompareTo(y));

            var last = int.MinValue;
            foreach (var item in list)
            {
                if (last > item) Assert.Fail();

                last = item;
            }
        }

        [TestMethod]
        public void SortingInDescendingOrderUsingAComparisonAndCheckingIfSorted()
        {
            var list = new List<int>();

            int minElement = -50000;
            int maxElement = 50000;

            int addedElements = 0;

            //Adding every seventh number, then every fifth number,
            //every third and at last all numbers
            //NOTE: some items are added more than once
            for (int i = 7; i > 0; i -= 2)
            {
                int el = maxElement;
                while (el >= minElement)
                {
                    list.Add(el);
                    addedElements++;
                    el -= i;
                }
            }

            list.Quick3SortDescending((x, y) => x.CompareTo(y));

            var last = int.MaxValue;
            foreach (var item in list)
            {
                if (last < item) Assert.Fail();

                last = item;
            }
        }

        [TestMethod]
        public void SortingInAscendingOrderUsingACustomComparerAndCheckingIfSorted()
        {
            var list = new List<int>();

            int minElement = -50000;
            int maxElement = 50000;

            int addedElements = 0;

            //Adding every seventh number, then every fifth number,
            //every third and at last all numbers
            //NOTE: some items are added more than once
            for (int i = 7; i > 0; i -= 2)
            {
                int el = minElement;
                while (el <= maxElement)
                {
                    list.Add(el);
                    addedElements++;
                    el += i;
                }
            }

            list.Quick3Sort(Comparer<int>.Create((x, y) => x.CompareTo(y)));

            var last = int.MinValue;
            foreach (var item in list)
            {
                if (last > item) Assert.Fail();

                last = item;
            }
        }

        [TestMethod]
        public void SortingInDescendingOrderUsingACustomComparerAndCheckingIfSorted()
        {
            var list = new List<int>();

            int minElement = -50000;
            int maxElement = 50000;

            int addedElements = 0;

            //Adding every seventh number, then every fifth number,
            //every third and at last all numbers
            //NOTE: some items are added more than once
            for (int i = 7; i > 0; i -= 2)
            {
                int el = maxElement;
                while (el >= minElement)
                {
                    list.Add(el);
                    addedElements++;
                    el -= i;
                }
            }

            list.Quick3SortDescending(Comparer<int>.Create((x, y) => x.CompareTo(y)));

            var last = int.MaxValue;
            foreach (var item in list)
            {
                if (last < item) Assert.Fail();

                last = item;
            }
        }

        [TestMethod]
        public void SortingARangeOfItemsInAscendingOrderAndCheckingIfSorted()
        {
            var list = new List<int>();

            int minElement = -50000;
            int maxElement = 50000;

            int addedElements = 0;

            //Adding every seventh number, then every fifth number,
            //every third and at last all numbers
            //NOTE: some items are added more than once
            for (int i = 7; i > 0; i -= 2)
            {
                int el = minElement;
                while (el <= maxElement)
                {
                    list.Add(el);
                    addedElements++;
                    el += i;
                }
            }

            int beginSortAt = addedElements / 3;
            int sortedCount = addedElements / 2;

            list.Quick3Sort(beginSortAt, sortedCount, null);

            var last = int.MinValue;
            for (int i = beginSortAt; i < beginSortAt + sortedCount; i++)
            {
                if (last > list[i]) Assert.Fail();

                last = list[i];
            }
        }

        [TestMethod]
        public void SortingARangeOfItemsInDescendingOrderAndCheckingIfSorted()
        {
            var list = new List<int>();

            int minElement = -50000;
            int maxElement = 50000;

            int addedElements = 0;

            //Adding every seventh number, then every fifth number,
            //every third and at last all numbers
            //NOTE: some items are added more than once
            for (int i = 7; i > 0; i -= 2)
            {
                int el = maxElement;
                while (el >= minElement)
                {
                    list.Add(el);
                    addedElements++;
                    el -= i;
                }
            }

            int beginSortAt = addedElements / 3;
            int sortedCount = addedElements / 2;

            list.Quick3SortDescending(beginSortAt, sortedCount, null);

            var last = int.MaxValue;
            for (int i = beginSortAt; i < beginSortAt + sortedCount; i++)
            {
                if (last < list[i]) Assert.Fail();

                last = list[i];
            }
        }
    }
}
