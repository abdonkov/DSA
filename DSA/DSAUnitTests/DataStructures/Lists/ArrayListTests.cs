using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DSA.DataStructures.Lists;
using System.Linq;

namespace DSAUnitTests.DataStructures.Lists
{
    [TestClass]
    public class ArrayListTests
    {
        [TestMethod]
        public void AddingItemsOneByOne()
        {
            var list = new ArrayList<int>();

            int itemCount = 1000;

            for (int i = 0; i < itemCount; i++)
            {
                list.Add(i);
            }

            int trueCount = 0;
            int previousItem = int.MinValue;

            foreach (var item in list)
            {
                if (previousItem > item) Assert.Fail();
                previousItem = item;
                trueCount++;
            }

            Assert.IsTrue(list.Count == itemCount
                            && list.Count == trueCount);
        }

        [TestMethod]
        public void AddingRangeOfItems()
        {
            var list = new ArrayList<int>();

            int itemCount = 1000;

            for (int i = 0; i < itemCount; i++)
            {
                list.Add(i);
            }

            if (list.Count != itemCount) Assert.Fail();

            var list2 = new ArrayList<int>();

            list2.AddRange(list);

            int trueCount = 0;
            int previousItem = int.MinValue;

            foreach (var item in list2)
            {
                if (previousItem > item) Assert.Fail();
                previousItem = item;
                trueCount++;
            }

            Assert.IsTrue(list.Count == itemCount
                            && list2.Count == itemCount
                            && list2.Count == trueCount);
        }

        [TestMethod]
        public void InitializingArrayListWithCollection()
        {
            var list = new ArrayList<int>();

            int itemCount = 1000;

            for (int i = 0; i < itemCount; i++)
            {
                list.Add(i);
            }

            var list2 = new ArrayList<int>(list);

            int trueCount = 0;
            int previousItem = int.MinValue;

            foreach (var item in list2)
            {
                if (previousItem > item) Assert.Fail();
                previousItem = item;
                trueCount++;
            }

            Assert.IsTrue(list.Count == itemCount
                            && list2.Count == itemCount
                            && list2.Count == trueCount);
        }

        [TestMethod]
        public void RemovingAllExceptOne()
        {
            var list = new ArrayList<int>();

            int itemCount = 1000;

            for (int i = 0; i < itemCount; i++)
            {
                list.Add(i);
            }

            for (int i = 0; i < itemCount - 1; i++)
            {
                if (!list.Remove(i)) Assert.Fail(i.ToString());
            }

            int trueCount = 0;


            foreach (var item in list)
            {
                trueCount++;
            }

            Assert.IsTrue(list.Count == 1
                            && trueCount == 1
                            && list[0] == itemCount - 1);
        }

        [TestMethod]
        public void InitializationWithZeroCapacityAndAddingItemsAfterwards()
        {
            var list = new ArrayList<int>(0);

            int itemCount = 100;

            for (int i = 0; i < itemCount; i++)
            {
                list.Add(i);
            }

            int trueCount = 0;
            int previousItem = int.MinValue;

            foreach (var item in list)
            {
                if (previousItem > item) Assert.Fail();
                previousItem = item;
                trueCount++;
            }

            Assert.IsTrue(list.Count == itemCount
                            && list.Count == trueCount);
        }

        [TestMethod]
        public void RemovingAllItemsAndAddingAgain()
        {
            var list = new ArrayList<int>();

            int itemCount = 1000;

            for (int i = 0; i < itemCount; i++)
            {
                list.Add(i);
            }

            for (int i = 0; i < itemCount; i++)
            {
                if (!list.Remove(i)) Assert.Fail();
            }

            bool countWasZero = list.Count == 0;

            for (int i = 0; i < itemCount; i++)
            {
                list.Add(i);
            }

            int trueCount = 0;
            int previousItem = int.MinValue;

            foreach (var item in list)
            {
                if (previousItem > item) Assert.Fail();
                previousItem = item;
                trueCount++;
            }

            Assert.IsTrue(list.Count == itemCount
                            && list.Count == trueCount
                            && countWasZero);
        }

        [TestMethod]
        public void RemoveRangeOfItems()
        {
            var list = new ArrayList<int>();

            int itemCount = 1000;

            for (int i = 0; i < itemCount; i++)
            {
                list.Add(i);
            }

            list.RemoveRange(0, itemCount - 1);

            int trueCount = 0;

            foreach (var item in list)
            {
                trueCount++;
            }

            Assert.IsTrue(list.Count == 1
                            && list.Count == trueCount
                            && list[0] == itemCount - 1);
        }

        [TestMethod]
        public void InsertAllItemsAtTheBeginningAndCheckForReversedOrder()
        {
            var list = new ArrayList<int>();

            int itemCount = 1000;

            for (int i = 0; i < itemCount; i++)
            {
                list.Insert(0, i);
            }

            var list2 = new ArrayList<int>(list.Reverse());

            bool areReversed = true;

            for (int i = 0; i < itemCount; i++)
            {
                if (list[i] != list2[itemCount - 1 - i])
                {
                    areReversed = false;
                    break;
                }
            }

            Assert.IsTrue(list.Count == itemCount
                            && list2.Count == itemCount
                            && areReversed);
        }

        [TestMethod]
        public void RemoveAtZeroUntilOneItemIsLeft()
        {
            var list = new ArrayList<int>();

            int itemCount = 1000;

            for (int i = 0; i < itemCount; i++)
            {
                list.Add(i);
            }

            for (int i = 1; i < itemCount; i++)
            {
                list.RemoveAt(0);
            }

            int trueCount = 0;

            foreach (var item in list)
            {
                trueCount++;
            }

            Assert.IsTrue(list.Count == 1
                            && list.Count == trueCount
                            && list[0] == itemCount - 1);
        }

        [TestMethod]
        public void IndexOfAndLastIndexOfTest()
        {
            var list = new ArrayList<int>();

            int itemCount = 1000;

            for (int i = 0; i < itemCount; i++)
            {
                list.Add(i);
                list.Insert(0, i);
            }

            bool allInPlace = true;

            for (int i = 0; i < itemCount; i++)
            {
                int first = list.IndexOf(i);
                int last = list.LastIndexOf(i);

                if (last - first - 1 - 2 * i != 0)
                {
                    allInPlace = false;
                    break;
                }
            }

            Assert.IsTrue(list.Count == itemCount * 2
                            && allInPlace);
        }

        [TestMethod]
        public void CheckIfContainedBeforeAndAfterRemoval()
        {
            var list = new ArrayList<int>();

            int itemCount = 1000;

            for (int i = 0; i < itemCount; i++)
            {
                if (list.Contains(i)) Assert.Fail();
                list.Add(i);
                if (!list.Contains(i)) Assert.Fail();
            }

            for (int i = 0; i < itemCount; i++)
            {
                if (!list.Contains(i)) Assert.Fail();
                list.Remove(i);
                if (list.Contains(i)) Assert.Fail();
            }

            Assert.IsTrue(list.Count == 0);
        }

        [TestMethod]
        public void AddingAfterClearingCollection()
        {
            var list = new ArrayList<int>();

            int itemCount = 1000;

            for (int i = 0; i < itemCount; i++)
            {
                list.Add(i);
            }

            list.Clear();

            for (int i = 0; i < itemCount; i++)
            {
                list.Add(i);
            }

            bool everythingIntact = true;

            for (int i = 0; i < itemCount; i++)
            {
                if (list[i] != i)
                {
                    everythingIntact = false;
                    break;
                }
            }

            Assert.IsTrue(list.Count == itemCount
                            && everythingIntact);
        }

        [TestMethod]
        public void InsertingRangeOfItemsAtTheMiddle()
        {
            var list = new ArrayList<int>();

            int itemCount = 1000;

            for (int i = 0; i < itemCount; i++)
            {
                list.Add(i);
            }

            var list2 = new ArrayList<int>(list);

            list.InsertRange(itemCount / 2, list2);

            for (int i = 0; i < itemCount / 2; i++)
            {
                if (list[i] != i) Assert.Fail();
            }

            for (int i = itemCount / 2; i < itemCount + itemCount / 2; i++)
            {
                if (list[i] != i - itemCount / 2) Assert.Fail();
            }

            for (int i = itemCount + itemCount / 2; i < itemCount * 2; i++)
            {
                if (list[i] != i - itemCount) Assert.Fail();
            }

            Assert.IsTrue(list.Count == itemCount * 2);
        }
    }
}
