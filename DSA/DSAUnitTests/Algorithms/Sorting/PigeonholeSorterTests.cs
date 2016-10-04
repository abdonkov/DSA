using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using DSA.Algorithms.Sorting;

namespace DSAUnitTests.Algorithms.Sorting
{
    [TestClass]
    public class PigeonholeSorterTests
    {
        [TestMethod]
        public void SortingIntegersInAscendingOrderAndCheckingIfSorted()
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
            
            list.PigeonholeSort();

            var last = int.MinValue;
            foreach (var item in list)
            {
                if (last > item) Assert.Fail();

                last = item;
            }
        }

        [TestMethod]
        public void SortingIntegersInDescendingOrderAndCheckingIfSorted()
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

            list.PigeonholeSortDescending();

            var last = int.MaxValue;
            foreach (var item in list)
            {
                if (last < item) Assert.Fail();

                last = item;
            }
        }

        [TestMethod]
        public void SortingARangeOfIntegersInAscendingOrderAndCheckingIfSorted()
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

            list.PigeonholeSort(beginSortAt, sortedCount);

            var last = int.MinValue;
            for (int i = beginSortAt; i < beginSortAt + sortedCount; i++)
            {
                if (last > list[i]) Assert.Fail();

                last = list[i];
            }
        }

        [TestMethod]
        public void SortingARangeOfIntegersInDescendingOrderAndCheckingIfSorted()
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

            list.PigeonholeSortDescending(beginSortAt, sortedCount);

            var last = int.MaxValue;
            for (int i = beginSortAt; i < beginSortAt + sortedCount; i++)
            {
                if (last < list[i]) Assert.Fail();

                last = list[i];
            }
        }

        [TestMethod]
        public void SortingKeyValuePairKeysInAscendingOrderAndCheckingIfSorted()
        {
            var list = new List<KeyValuePair<int, int>>();

            int minElement = -50000;
            int maxElement = 50000;

            int addedElements = 0;
            // The current value to add as the the value of the key-value pair
            // All values will be different but there will be equal keys
            // That way we will check stability of the sorting
            int curValue = int.MinValue;

            //Adding every seventh number, then every fifth number,
            //every third and at last all numbers
            //NOTE: some items are added more than once
            for (int i = 7; i > 0; i -= 2)
            {
                int el = minElement;
                while (el <= maxElement)
                {
                    list.Add(new KeyValuePair<int, int>(el, curValue++));
                    addedElements++;
                    el += i;
                }
            }

            list.PigeonholeSortKeys();

            var lastKey = int.MinValue;
            int lastValue = int.MinValue;

            foreach (var kvp in list)
            {
                if (lastKey > kvp.Key) Assert.Fail();

                if (lastKey == kvp.Key)
                    if (lastValue > kvp.Value) Assert.Fail();

                lastKey = kvp.Key;
                lastValue = kvp.Value;
            }
        }

        [TestMethod]
        public void SortingKeyValuePairKeysInDescendingOrderAndCheckingIfSorted()
        {
            var list = new List<KeyValuePair<int, int>>();

            int minElement = -50000;
            int maxElement = 50000;

            int addedElements = 0;
            // The current value to add as the the value of the key-value pair
            // All values will be different but there will be equal keys
            // That way we will check stability of the sorting
            int curValue = int.MinValue;

            //Adding every seventh number, then every fifth number,
            //every third and at last all numbers
            //NOTE: some items are added more than once
            for (int i = 7; i > 0; i -= 2)
            {
                int el = minElement;
                while (el <= maxElement)
                {
                    list.Add(new KeyValuePair<int, int>(el, curValue++));
                    addedElements++;
                    el += i;
                }
            }

            list.PigeonholeSortDescendingKeys();

            var lastKey = int.MaxValue;
            // The values are in ascending order even though the key-value pairs are sorted in descending
            // order because the original order of the equal elements is preserved. The sort is stable.
            int lastValue = int.MinValue;

            foreach (var kvp in list)
            {
                if (lastKey < kvp.Key) Assert.Fail();

                if (lastKey == kvp.Key)
                    if (lastValue > kvp.Value) Assert.Fail();

                lastKey = kvp.Key;
                lastValue = kvp.Value;
            }
        }

        [TestMethod]
        public void SortingARangeOfKeyValuePairKeysInAscendingOrderAndCheckingIfSorted()
        {
            var list = new List<KeyValuePair<int, int>>();

            int minElement = -50000;
            int maxElement = 50000;

            int addedElements = 0;
            // The current value to add as the the value of the key-value pair
            // All values will be different but there will be equal keys
            // That way we will check stability of the sorting
            int curValue = int.MinValue;

            //Adding every seventh number, then every fifth number,
            //every third and at last all numbers
            //NOTE: some items are added more than once
            for (int i = 7; i > 0; i -= 2)
            {
                int el = minElement;
                while (el <= maxElement)
                {
                    list.Add(new KeyValuePair<int, int>(el, curValue++));
                    addedElements++;
                    el += i;
                }
            }

            int beginSortAt = addedElements / 3;
            int sortedCount = addedElements / 2;

            list.PigeonholeSortKeys(beginSortAt, sortedCount);

            var lastKey = int.MinValue;
            int lastValue = int.MinValue;

            for (int i = beginSortAt; i < beginSortAt + sortedCount; i++)
            {
                if (lastKey > list[i].Key) Assert.Fail();

                if (lastKey == list[i].Key)
                    if (lastValue > list[i].Value) Assert.Fail();

                lastKey = list[i].Key;
                lastValue = list[i].Value;
            }
        }

        [TestMethod]
        public void SortingARangeOfKeyValuePairKeysInDescendingOrderAndCheckingIfSorted()
        {
            var list = new List<KeyValuePair<int, int>>();

            int minElement = -50000;
            int maxElement = 50000;

            int addedElements = 0;
            // The current value to add as the the value of the key-value pair
            // All values will be different but there will be equal keys
            // That way we will check stability of the sorting
            int curValue = int.MinValue;

            //Adding every seventh number, then every fifth number,
            //every third and at last all numbers
            //NOTE: some items are added more than once
            for (int i = 7; i > 0; i -= 2)
            {
                int el = maxElement;
                while (el >= minElement)
                {
                    list.Add(new KeyValuePair<int, int>(el, curValue++));
                    addedElements++;
                    el -= i;
                }
            }

            int beginSortAt = addedElements / 3;
            int sortedCount = addedElements / 2;

            list.PigeonholeSortDescendingKeys(beginSortAt, sortedCount);

            var lastKey = int.MaxValue;
            // The values are in ascending order even though the key-value pairs are sorted in descending order
            // because the original order of the equal elements is preserved. The sort is stable.
            int lastValue = int.MinValue;

            for (int i = beginSortAt; i < beginSortAt + sortedCount; i++)
            {
                if (lastKey < list[i].Key) Assert.Fail();

                if (lastKey == list[i].Key)
                    if (lastValue > list[i].Value) Assert.Fail();

                lastKey = list[i].Key;
                lastValue = list[i].Value;
            }
        }

        [TestMethod]
        public void SortingKeyValuePairValuesInAscendingOrderAndCheckingIfSorted()
        {
            var list = new List<KeyValuePair<int, int>>();

            int minElement = -50000;
            int maxElement = 50000;

            int addedElements = 0;
            // The current key to add as the the key of the key-value pair
            // All keys will be different but there will be equal values
            // That way we will check stability of the sorting
            int curKey = int.MinValue;

            //Adding every seventh number, then every fifth number,
            //every third and at last all numbers
            //NOTE: some items are added more than once
            for (int i = 7; i > 0; i -= 2)
            {
                int el = minElement;
                while (el <= maxElement)
                {
                    list.Add(new KeyValuePair<int, int>(curKey++, el));
                    addedElements++;
                    el += i;
                }
            }

            list.PigeonholeSortValues();

            var lastKey = int.MinValue;
            int lastValue = int.MinValue;

            foreach (var kvp in list)
            {
                if (lastValue > kvp.Value) Assert.Fail();

                if (lastValue == kvp.Value)
                    if (lastKey > kvp.Key) Assert.Fail();

                lastKey = kvp.Key;
                lastValue = kvp.Value;
            }
        }

        [TestMethod]
        public void SortingKeyValuePairValuesInDescendingOrderAndCheckingIfSorted()
        {
            var list = new List<KeyValuePair<int, int>>();

            int minElement = -50000;
            int maxElement = 50000;

            int addedElements = 0;
            // The current key to add as the the key of the key-value pair
            // All keys will be different but there will be equal values
            // That way we will check stability of the sorting
            int curKey = int.MinValue;

            //Adding every seventh number, then every fifth number,
            //every third and at last all numbers
            //NOTE: some items are added more than once
            for (int i = 7; i > 0; i -= 2)
            {
                int el = minElement;
                while (el <= maxElement)
                {
                    list.Add(new KeyValuePair<int, int>(curKey++, el));
                    addedElements++;
                    el += i;
                }
            }

            list.PigeonholeSortDescendingValues();

            // The keys are in ascending order even though the key-value pairs are sorted in descending
            // order because the original order of the equal elements is preserved. The sort is stable.
            var lastKey = int.MinValue;
            int lastValue = int.MaxValue;

            foreach (var kvp in list)
            {
                if (lastValue < kvp.Value) Assert.Fail();

                if (lastValue == kvp.Value)
                    if (lastKey > kvp.Key) Assert.Fail();

                lastKey = kvp.Key;
                lastValue = kvp.Value;
            }
        }

        [TestMethod]
        public void SortingARangeOfKeyValuePairValuesInAscendingOrderAndCheckingIfSorted()
        {
            var list = new List<KeyValuePair<int, int>>();

            int minElement = -50000;
            int maxElement = 50000;

            int addedElements = 0;
            // The current key to add as the the key of the key-value pair
            // All keys will be different but there will be equal values
            // That way we will check stability of the sorting
            int curKey = int.MinValue;

            //Adding every seventh number, then every fifth number,
            //every third and at last all numbers
            //NOTE: some items are added more than once
            for (int i = 7; i > 0; i -= 2)
            {
                int el = minElement;
                while (el <= maxElement)
                {
                    list.Add(new KeyValuePair<int, int>(curKey++, el));
                    addedElements++;
                    el += i;
                }
            }

            int beginSortAt = addedElements / 3;
            int sortedCount = addedElements / 2;

            list.PigeonholeSortValues(beginSortAt, sortedCount);

            var lastKey = int.MinValue;
            int lastValue = int.MinValue;

            for (int i = beginSortAt; i < beginSortAt + sortedCount; i++)
            {
                if (lastValue > list[i].Value) Assert.Fail();

                if (lastValue == list[i].Value)
                    if (lastKey > list[i].Key) Assert.Fail();

                lastKey = list[i].Key;
                lastValue = list[i].Value;
            }
        }

        [TestMethod]
        public void SortingARangeOfKeyValuePairValuesInDescendingOrderAndCheckingIfSorted()
        {
            var list = new List<KeyValuePair<int, int>>();

            int minElement = -50000;
            int maxElement = 50000;

            int addedElements = 0;
            // The current key to add as the the key of the key-value pair
            // All keys will be different but there will be equal values
            // That way we will check stability of the sorting
            int curKey = int.MinValue;

            //Adding every seventh number, then every fifth number,
            //every third and at last all numbers
            //NOTE: some items are added more than once
            for (int i = 7; i > 0; i -= 2)
            {
                int el = maxElement;
                while (el >= minElement)
                {
                    list.Add(new KeyValuePair<int, int>(curKey++, el));
                    addedElements++;
                    el -= i;
                }
            }

            int beginSortAt = addedElements / 3;
            int sortedCount = addedElements / 2;

            list.PigeonholeSortDescendingValues(beginSortAt, sortedCount);

            // The keys are in ascending order even though the key-value pairs are sorted in descending order
            // because the original order of the equal elements is preserved. The sort is stable.
            var lastKey = int.MinValue;
            int lastValue = int.MaxValue;

            for (int i = beginSortAt; i < beginSortAt + sortedCount; i++)
            {
                if (lastValue < list[i].Value) Assert.Fail();

                if (lastValue == list[i].Value)
                    if (lastKey > list[i].Key) Assert.Fail();

                lastKey = list[i].Key;
                lastValue = list[i].Value;
            }
        }
    }
}
