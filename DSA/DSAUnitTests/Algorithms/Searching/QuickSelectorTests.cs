using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using DSA.Algorithms.Searching;
using System.Collections.Generic;

namespace DSAUnitTests.Algorithms.Searching
{
    [TestClass]
    public class QuickSelectorTests
    {
        [TestMethod]
        public void QuickSelectSmallestNotInPlace()
        {
            int itemCount = 10000;
            int numberOfSmallestChecks = 100;
            // randomize elements in list
            var rand = new Random();
            var list = Enumerable.Range(1, itemCount).OrderBy(x => rand.Next()).ToList();

            // first Nth smallest check
            for (int nthSmallest = 1; nthSmallest <= numberOfSmallestChecks; nthSmallest++)
            {
                if (list.QuickSelectSmallest(nthSmallest) != nthSmallest) Assert.Fail();
            }

            // random smallest check
            for (int i = 0; i < numberOfSmallestChecks; i++)
            {
                int random = rand.Next(1, itemCount + 1);

                if (list.QuickSelectSmallest(random) != random) Assert.Fail();
            }
        }

        [TestMethod]
        public void QuickSelectBiggestNotInPlace()
        {
            int itemCount = 10000;
            int numberOfBiggestChecks = 100;
            // randomize elements in list
            var rand = new Random();
            var list = Enumerable.Range(1, itemCount).OrderBy(x => rand.Next()).ToList();

            // first Nth biggest check
            for (int nthBiggest = 1; nthBiggest <= numberOfBiggestChecks; nthBiggest++)
            {
                if (list.QuickSelectBiggest(nthBiggest) != itemCount + 1 - nthBiggest) Assert.Fail();
            }

            // random smallest check
            for (int i = 0; i < numberOfBiggestChecks; i++)
            {
                int random = rand.Next(1, itemCount + 1);

                if (list.QuickSelectBiggest(random) != itemCount + 1 - random) Assert.Fail();
            }
        }

        [TestMethod]
        public void QuickSelectSmallestInPlace()
        {
            int itemCount = 10000;
            int numberOfSmallestChecks = 100;
            // randomize elements in list
            var rand = new Random();
            var list = Enumerable.Range(1, itemCount).OrderBy(x => rand.Next()).ToList();

            // first Nth smallest check
            for (int nthSmallest = 1; nthSmallest <= numberOfSmallestChecks; nthSmallest++)
            {
                if (list.QuickSelectInPlaceSmallest(nthSmallest) != nthSmallest) Assert.Fail();
            }

            // random smallest check
            for (int i = 0; i < numberOfSmallestChecks; i++)
            {
                int random = rand.Next(1, itemCount + 1);

                if (list.QuickSelectInPlaceSmallest(random) != random) Assert.Fail();
            }
        }

        [TestMethod]
        public void QuickSelectBiggestInPlace()
        {
            int itemCount = 10000;
            int numberOfBiggestChecks = 100;
            // randomize elements in list
            var rand = new Random();
            var list = Enumerable.Range(1, itemCount).OrderBy(x => rand.Next()).ToList();

            // first Nth biggest check
            for (int nthBiggest = 1; nthBiggest <= numberOfBiggestChecks; nthBiggest++)
            {
                if (list.QuickSelectInPlaceBiggest(nthBiggest) != itemCount + 1 - nthBiggest) Assert.Fail();
            }

            // random smallest check
            for (int i = 0; i < numberOfBiggestChecks; i++)
            {
                int random = rand.Next(1, itemCount + 1);

                if (list.QuickSelectInPlaceBiggest(random) != itemCount + 1 - random) Assert.Fail();
            }
        }

        [TestMethod]
        public void QuickSelectSmallestSameAsBiggestWithOppositeComparer()
        {
            int itemCount = 10000;
            int numberOfChecks = 100;
            // randomize elements in list
            var rand = new Random();
            var list = Enumerable.Range(1, itemCount).OrderBy(x => rand.Next()).ToList();
            // Opposite comparer for biggest check
            var comp = Comparer<int>.Create((x, y) => y.CompareTo(x));

            // first Nth checks
            for (int nth = 1; nth <= numberOfChecks; nth++)
            {
                int smallestEl = list.QuickSelectInPlaceSmallest(nth);
                int biggestEl = list.QuickSelectInPlaceBiggest(nth, comp);

                if (smallestEl != biggestEl) Assert.Fail();
            }

            // random checks
            for (int i = 0; i < numberOfChecks; i++)
            {
                int random = rand.Next(1, itemCount + 1);

                int smallestEl = list.QuickSelectInPlaceSmallest(random);
                int biggestEl = list.QuickSelectInPlaceBiggest(random, comp);

                if (smallestEl != biggestEl) Assert.Fail();
            }
        }

        [TestMethod]
        public void QuickSelectSmallestRangeOfItems()
        {
            int itemCount = 10000;
            int numberOfSmallestChecks = 100;
            // randomize elements in list
            var rand = new Random();
            var list = Enumerable.Range(1, itemCount).OrderBy(x => rand.Next()).ToList();

            // Define range
            int start = itemCount / 4;
            int count = itemCount / 2;
            int end = start + count - 1;

            // copy of range, used for saving smallest elements with iterative search
            var smEl = new int[count];
            for (int i = 0; i < count; i++)
            {
                smEl[i] = list[start + i];
            }

            // first Nth smallest check
            for (int nthSmallest = 1; nthSmallest <= numberOfSmallestChecks; nthSmallest++)
            {
                // find nth smallest iterative
                for (int i = nthSmallest - 1; i < count; i++)
                {
                    if (smEl[nthSmallest - 1] > smEl[i])
                    {
                        var temp = smEl[nthSmallest - 1];
                        smEl[nthSmallest - 1] = smEl[i];
                        smEl[i] = temp;
                    }
                }

                if (list.QuickSelectSmallest(nthSmallest, start, count, null) != smEl[nthSmallest - 1]) Assert.Fail();
            }
        }

        [TestMethod]
        public void QuickSelectBiggestRangeOfItems()
        {
            int itemCount = 10000;
            int numberOfSmallestChecks = 100;
            // randomize elements in list
            var rand = new Random();
            var list = Enumerable.Range(1, itemCount).OrderBy(x => rand.Next()).ToList();

            // Define range
            int start = itemCount / 4;
            int count = itemCount / 2;
            int end = start + count - 1;

            // copy of range, used for saving biggest elements with iterative search
            var biEl = new int[count];
            for (int i = 0; i < count; i++)
            {
                biEl[i] = list[start + i];
            }

            // first Nth biggest check
            for (int nthSmallest = 1; nthSmallest <= numberOfSmallestChecks; nthSmallest++)
            {
                // find nth biggest iterative
                for (int i = nthSmallest - 1; i < count; i++)
                {
                    if (biEl[nthSmallest - 1] > biEl[i])
                    {
                        var temp = biEl[nthSmallest - 1];
                        biEl[nthSmallest - 1] = biEl[i];
                        biEl[i] = temp;
                    }
                }

                if (list.QuickSelectSmallest(nthSmallest, start, count, null) != biEl[nthSmallest - 1]) Assert.Fail();
            }
        }
    }
}
