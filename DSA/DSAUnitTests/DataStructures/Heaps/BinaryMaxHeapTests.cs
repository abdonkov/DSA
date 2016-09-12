using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DSA.DataStructures.Heaps;
using System.Collections.Generic;

namespace DSAUnitTests.DataStructures.Heaps
{
    [TestClass]
    public class BinaryMaxHeapTests
    {
        [TestMethod]
        public void AddingElementsAndCheckingIfExtractedInSortedOrder()
        {
            var heap = new BinaryMaxHeap<int>();

            int maxHeapElement = 50000;
            int minHeapElement = -50000;

            int addedElements = 0;

            //Adding every seventh number, then every fifth number,
            //every third and at last all numbers
            //NOTE: some items are added more than once
            for (int i = 7; i > 0; i -= 2)
            {
                int el = minHeapElement;
                while (el <= maxHeapElement)
                {
                    heap.Add(el);
                    addedElements++;
                    el += i;
                }
            }

            if (heap.Count != addedElements) Assert.Fail();

            int removedElements = 0;
            var max = heap.PeekMax();
            while (!heap.IsEmpty)
            {
                if (max < heap.PeekMax()) Assert.Fail();

                max = heap.PopMax();
                removedElements++;
            }

            Assert.IsTrue(heap.IsEmpty
                            && heap.Count == 0
                            && addedElements == removedElements);
        }

        [TestMethod]
        public void AddingElementsWithCustomComparerAndCheckingIfExtractedInSortedOrder()
        {
            //Creating heap with reversed comparer
            var heap = new BinaryMaxHeap<int>(Comparer<int>.Create(
                                                            (x, y) => y.CompareTo(x)));

            int maxHeapElement = 50000;
            int minHeapElement = -50000;

            int addedElements = 0;

            //Adding every seventh number, then every fifth number,
            //every third and at last all numbers
            //NOTE: some items are added more than once
            for (int i = 7; i > 0; i -= 2)
            {
                int el = minHeapElement;
                while (el <= maxHeapElement)
                {
                    heap.Add(el);
                    addedElements++;
                    el += i;
                }
            }

            if (heap.Count != addedElements) Assert.Fail();

            int removedElements = 0;
            // because of the reversed comparer
            var min = heap.PeekMax();
            while (!heap.IsEmpty)
            {
                if (min > heap.PeekMax()) Assert.Fail();

                min = heap.PopMax();
                removedElements++;
            }

            Assert.IsTrue(heap.IsEmpty
                            && heap.Count == 0
                            && addedElements == removedElements);
        }

        [TestMethod]
        public void HeapifyUnsortedCollectionAndCheckIfExtractedInSortedOrder()
        {
            var heap = new BinaryMaxHeap<int>();

            var unsortedList = new List<int>();

            int maxHeapElement = 50000;
            int minHeapElement = -50000;

            int addedElements = 0;

            //Adding every seventh number, then every fifth number,
            //every third and at last all numbers
            //NOTE: some items are added more than once
            for (int i = 7; i > 0; i -= 2)
            {
                int el = minHeapElement;
                while (el <= maxHeapElement)
                {
                    unsortedList.Add(el);
                    addedElements++;
                    el += i;
                }
            }

            heap.Heapify(unsortedList);

            if (heap.Count != addedElements) Assert.Fail("1");

            int removedElements = 0;
            var max = heap.PeekMax();
            while (!heap.IsEmpty)
            {
                if (max < heap.PeekMax()) Assert.Fail("2");

                max = heap.PopMax();
                removedElements++;
            }

            Assert.IsTrue(heap.IsEmpty
                            && heap.Count == 0
                            && addedElements == removedElements);
        }

        [TestMethod]
        public void AddingAfterClearingHeap()
        {
            var heap = new BinaryMaxHeap<int>();

            int maxHeapElement = 50000;
            int minHeapElement = -50000;

            int addedElements = 0;

            //Adding every seventh number, then every fifth number,
            //every third and at last all numbers
            //NOTE: some items are added more than once
            for (int i = 7; i > 0; i -= 2)
            {
                int el = minHeapElement;
                while (el <= maxHeapElement)
                {
                    heap.Add(el);
                    addedElements++;
                    el += i;
                }
            }

            if (heap.Count != addedElements) Assert.Fail();

            heap.Clear();

            if (heap.Count != 0) Assert.Fail();

            addedElements = 0;

            //Adding every seventh number, then every fifth number,
            //every third and at last all numbers
            //NOTE: some items are added more than once
            for (int i = 7; i > 0; i -= 2)
            {
                int el = minHeapElement;
                while (el < maxHeapElement)
                {
                    heap.Add(el);
                    addedElements++;
                    el += i;
                }
            }

            if (heap.Count != addedElements) Assert.Fail();

            int removedElements = 0;
            var max = heap.PeekMax();
            while (!heap.IsEmpty)
            {
                if (max < heap.PeekMax()) Assert.Fail();

                max = heap.PopMax();
                removedElements++;
            }

            Assert.IsTrue(heap.IsEmpty
                            && heap.Count == 0
                            && addedElements == removedElements);
        }

        [TestMethod]
        public void ReplacingMinElementAndCheckingIfExtractedInSortedOrder()
        {
            var heap = new BinaryMaxHeap<int>();

            int maxHeapElement = 50000;
            int minHeapElement = -50000;

            int addedElements = 0;

            //Adding every seventh number, then every fifth number,
            //every third and at last all numbers
            //NOTE: some items are added more than once
            for (int i = 7; i > 0; i -= 2)
            {
                int el = minHeapElement;
                while (el <= maxHeapElement)
                {
                    heap.Add(el);
                    addedElements++;
                    el += i;
                }
            }

            if (heap.Count != addedElements) Assert.Fail();

            heap.ReplaceMax(int.MinValue);
            heap.ReplaceMax(int.MinValue);
            heap.ReplaceMax(int.MinValue);

            int removedElements = 0;
            var max = heap.PeekMax();
            while (!heap.IsEmpty)
            {
                if (max < heap.PeekMax()) Assert.Fail();

                max = heap.PopMax();
                removedElements++;
            }

            Assert.IsTrue(heap.IsEmpty
                            && heap.Count == 0
                            && addedElements == removedElements);
        }

        [TestMethod]
        public void MergingTwoHeapsAndCheckingIfExtractedInSortedOrder()
        {
            var heap1 = new BinaryMaxHeap<int>();
            var heap2 = new BinaryMaxHeap<int>();

            int maxElementInFirstHeap = 100000;
            int minElementInFirstHeap = 0;
            int addedElementsInFirstHeap = 0;

            //Adding every seventh number, then every fifth number,
            //every third and at last all numbers
            //NOTE: some items are added more than once
            for (int i = 7; i > 0; i -= 2)
            {
                int el = minElementInFirstHeap;
                while (el <= maxElementInFirstHeap)
                {
                    heap1.Add(el);
                    addedElementsInFirstHeap++;
                    el += i;
                }
            }

            int maxElementInSecondHeap = 50000;
            int minElementInSecondHeap = -50000;
            int addedElementsInSecondHeap = 0;

            //Adding every seventh number, then every fifth number,
            //every third and at last all numbers
            //NOTE: some items are added more than once
            for (int i = 7; i > 0; i -= 2)
            {
                int el = minElementInSecondHeap;
                while (el <= maxElementInSecondHeap)
                {
                    heap2.Add(el);
                    addedElementsInSecondHeap++;
                    el += i;
                }
            }

            if (heap1.Count != addedElementsInFirstHeap) Assert.Fail("first heap incorrect count");
            if (heap2.Count != addedElementsInSecondHeap) Assert.Fail("second heap incorrect count");

            var oldHeap = new BinaryMaxHeap<int>();
            oldHeap.Heapify(heap1.ToArray());

            heap1.Merge(heap2);
            heap2.Merge(oldHeap);

            int mergedHeapElements = addedElementsInFirstHeap + addedElementsInSecondHeap;

            if (heap1.Count != mergedHeapElements) Assert.Fail("merged first with second incorect count");
            if (heap2.Count != mergedHeapElements) Assert.Fail("merged second with first incorrect count");

            var max1 = heap1.PeekMax();
            var max2 = heap2.PeekMax();
            if (max1 != max2) Assert.Fail("merged heaps min element is different");

            int removedElements = 0;
            while (!heap1.IsEmpty && !heap2.IsEmpty)
            {
                if (max1 < heap1.PeekMax()) Assert.Fail();
                if (max2 < heap2.PeekMax()) Assert.Fail();

                max1 = heap1.PopMax();
                max2 = heap2.PopMax();
                removedElements++;

                if (max1 != max2) Assert.Fail("merged heaps min element is different");
            }

            Assert.IsTrue(heap1.IsEmpty
                            && heap2.IsEmpty
                            && heap1.Count == 0
                            && heap2.Count == 0
                            && mergedHeapElements == removedElements);
        }

        [TestMethod]
        public void ConvertingToBinaryMinHeap()
        {
            var maxHeap = new BinaryMaxHeap<int>();

            int maxHeapElement = 50000;
            int minHeapElement = -50000;

            int addedElements = 0;

            //Adding every seventh number, then every fifth number,
            //every third and at last all numbers
            //NOTE: some items are added more than once
            for (int i = 7; i > 0; i -= 2)
            {
                int el = minHeapElement;
                while (el <= maxHeapElement)
                {
                    maxHeap.Add(el);
                    addedElements++;
                    el += i;
                }
            }

            if (maxHeap.Count != addedElements) Assert.Fail();

            // Binary max heap with reversed comparer. Have to be the same as the min heap
            var reversedMaxHeap = new BinaryMaxHeap<int>(Comparer<int>.Create(
                                                                (x, y) => y.CompareTo(x)));

            reversedMaxHeap.Heapify(maxHeap.ToArray());

            var minHeap = maxHeap.ToMinHeap();

            if (minHeap.Count != reversedMaxHeap.Count) Assert.Fail();

            var min1 = reversedMaxHeap.PeekMax();
            var min2 = minHeap.PeekMin();

            int removedElements = 0;
            while (!reversedMaxHeap.IsEmpty && !minHeap.IsEmpty)
            {
                if (min1 > reversedMaxHeap.PeekMax()) Assert.Fail();
                if (min2 > minHeap.PeekMin()) Assert.Fail();

                min1 = reversedMaxHeap.PopMax();
                min2 = minHeap.PopMin();
                removedElements++;

                if (min1 != min2) Assert.Fail();
            }

            Assert.IsTrue(reversedMaxHeap.IsEmpty
                            && minHeap.IsEmpty
                            && reversedMaxHeap.Count == 0
                            && minHeap.Count == 0
                            && addedElements == removedElements);
        }
    }
}
