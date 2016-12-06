using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using DSA.Algorithms.Searching;
using DSA.Algorithms.Sorting;

namespace DSAUnitTests.Algorithms.Searching
{
    [TestClass]
    public class LinearSearcherIListTests
    {
        [TestMethod]
        public void SearchInListWithUniqueItems()
        {
            int itemCount = 2000;

            var list = new List<int>(itemCount);

            for (int i = 0; i < itemCount; i++)
            {
                list.Add(i);
            }

            for (int i = 0; i < itemCount; i++)
            {
                int fi = list.LinearSearchFirstIndexOf(i);
                int li = list.LinearSearchLastIndexOf(i);

                if (fi != li) Assert.Fail();
                if (fi != i) Assert.Fail();
                if (list[fi] != i) Assert.Fail();
            }
        }

        [TestMethod]
        public void SearchInListWithNonUniqueItems()
        {
            int maxItem = 2000;

            var list = new List<int>(maxItem);

            //Adding every seventh number, then every fifth number,
            //every third and at last all numbers
            //NOTE: some items are added more than once
            for (int i = 7; i > 0; i -= 2)
            {
                int el = 0;
                while (el <= maxItem)
                {
                    list.Add(el);
                    el += i;
                }
            }

            list.QuickSort();

            for (int i = 0; i <= maxItem; i++)
            {
                int fi = list.LinearSearchFirstIndexOf(i);
                int li = list.LinearSearchLastIndexOf(i);

                // Calculate number of occurrences of current number
                int occurNum = 1;
                if (i % 7 == 0) occurNum++;
                if (i % 5 == 0) occurNum++;
                if (i % 3 == 0) occurNum++;

                if (li - fi + 1 != occurNum) Assert.Fail();

                for (int j = fi + 1; j <= li; j++)
                {
                    if (list[j] != list[fi]) Assert.Fail();
                }
            }
        }

        [TestMethod]
        public void SearchForMissingItems()
        {
            int maxItem = 2000;

            var list = new List<int>(maxItem);

            // Add only even numbers
            for (int i = 0; i <= maxItem; i += 2)
            {
                list.Add(i);
            }

            for (int i = 1; i < maxItem; i += 2)
            {
                int fi = list.LinearSearchFirstIndexOf(i);
                int li = list.LinearSearchLastIndexOf(i);

                if (fi != li) Assert.Fail();
                if (fi != -1) Assert.Fail();
            }
        }

        [TestMethod]
        public void SearchInRangeOfItems()
        {
            int itemCount = 2000;

            var list = new List<int>(itemCount);

            // Add only even numbers
            for (int i = 0; i < itemCount; i++)
            {
                list.Add(i);
            }

            int startIndex = list.Count / 3;
            int rangeCount = list.Count / 2;

            for (int i = startIndex; i < startIndex + rangeCount - 1; i++)
            {
                int fi = list.LinearSearchFirstIndexOf(i, startIndex, rangeCount, null);
                int li = list.LinearSearchLastIndexOf(i, startIndex, rangeCount, null);

                if (fi != li) Assert.Fail();
                if (fi != i) Assert.Fail();
                if (list[fi] != i) Assert.Fail();
            }

        }
    }
}
