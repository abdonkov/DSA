using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DSA.DataStructures.Heaps;
using System.Collections.Generic;

namespace DSAUnitTests.DataStructures.Heaps
{
    [TestClass]
    public class BinaryMinHeapTests
    {
        [TestMethod]
        public void AddingElementsAndCheckingIfExtractedInSortedOrder()
        {
            var heap = new BinaryMinHeap<int>();

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
            var min = heap.PeekMin();
            while(!heap.IsEmpty)
            {
                if (min > heap.PeekMin()) Assert.Fail();

                min = heap.PopMin();
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
            var heap = new BinaryMinHeap<int>(Comparer<int>.Create(
                                                            (x,y)=>y.CompareTo(x)));

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
            var max = heap.PeekMin();
            while (!heap.IsEmpty)
            {
                if (max < heap.PeekMin()) Assert.Fail();

                max = heap.PopMin();
                removedElements++;
            }

            Assert.IsTrue(heap.IsEmpty
                            && heap.Count == 0
                            && addedElements == removedElements);
        }

        [TestMethod]
        public void HeapifyUnsortedCollectionAndCheckIfExtractedInSortedOrder()
        {
            var heap = new BinaryMinHeap<int>();

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
            var min = heap.PeekMin();
            while (!heap.IsEmpty)
            {
                if (min > heap.PeekMin()) Assert.Fail("2");

                min = heap.PopMin();
                removedElements++;
            }

            Assert.IsTrue(heap.IsEmpty
                            && heap.Count == 0
                            && addedElements == removedElements);
        }

        [TestMethod]
        public void AddingAfterClearingHeap()
        {
            var heap = new BinaryMinHeap<int>();

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
            var min = heap.PeekMin();
            while (!heap.IsEmpty)
            {
                if (min > heap.PeekMin()) Assert.Fail();

                min = heap.PopMin();
                removedElements++;
            }

            Assert.IsTrue(heap.IsEmpty
                            && heap.Count == 0
                            && addedElements == removedElements);
        }

        [TestMethod]
        public void ReplacingMinElementAndCheckingIfExtractedInSortedOrder()
        {
            var heap = new BinaryMinHeap<int>();

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

            heap.ReplaceMin(int.MaxValue);
            heap.ReplaceMin(int.MaxValue);
            heap.ReplaceMin(int.MaxValue);

            int removedElements = 0;
            var min = heap.PeekMin();
            while (!heap.IsEmpty)
            {
                if (min > heap.PeekMin()) Assert.Fail();

                min = heap.PopMin();
                removedElements++;
            }

            Assert.IsTrue(heap.IsEmpty
                            && heap.Count == 0
                            && addedElements == removedElements);
        }

        [TestMethod]
        public void MergingTwoHeapsAndCheckingIfExtractedInSortedOrder()
        {
            var heap1 = new BinaryMinHeap<int>();
            var heap2 = new BinaryMinHeap<int>();

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

            var oldHeap = new BinaryMinHeap<int>();
            oldHeap.Heapify(heap1.ToArray());

            heap1.Merge(heap2);
            heap2.Merge(oldHeap);

            int mergedHeapElements = addedElementsInFirstHeap + addedElementsInSecondHeap;

            if (heap1.Count != mergedHeapElements) Assert.Fail("merged first with second incorect count");
            if (heap2.Count != mergedHeapElements) Assert.Fail("merged second with first incorrect count");

            var min1 = heap1.PeekMin();
            var min2 = heap2.PeekMin();
            if (min1 != min2) Assert.Fail("merged heaps min element is different");

            int removedElements = 0;
            while (!heap1.IsEmpty && !heap2.IsEmpty)
            {
                if (min1 > heap1.PeekMin()) Assert.Fail();
                if (min2 > heap2.PeekMin()) Assert.Fail();

                min1 = heap1.PopMin();
                min2 = heap2.PopMin();
                removedElements++;

                if (min1 != min2) Assert.Fail("merged heaps min element is different");
            }

            Assert.IsTrue(heap1.IsEmpty
                            && heap2.IsEmpty
                            && heap1.Count == 0
                            && heap2.Count == 0
                            && mergedHeapElements == removedElements);
        }

        [TestMethod]
        public void ConvertingToBinaryMaxHeap()
        {
            var minHeap = new BinaryMinHeap<int>();

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
                    minHeap.Add(el);
                    addedElements++;
                    el += i;
                }
            }

            if (minHeap.Count != addedElements) Assert.Fail();

            // Binary min heap with reversed comparer. Have to be the same as the max heap
            var reversedMinHeap = new BinaryMinHeap<int>(Comparer<int>.Create(
                                                                (x,y)=>y.CompareTo(x)));

            reversedMinHeap.Heapify(minHeap.ToArray());

            var maxHeap = minHeap.ToMaxHeap();

            if (maxHeap.Count != reversedMinHeap.Count) Assert.Fail();

            var max1 = reversedMinHeap.PeekMin();
            var max2 = maxHeap.PeekMax();

            int removedElements = 0;
            while (!reversedMinHeap.IsEmpty && !maxHeap.IsEmpty)
            {
                if (max1 < reversedMinHeap.PeekMin()) Assert.Fail();
                if (max2 < maxHeap.PeekMax()) Assert.Fail();

                max1 = reversedMinHeap.PopMin();
                max2 = maxHeap.PopMax();
                removedElements++;

                if (max1 != max2) Assert.Fail();
            }

            Assert.IsTrue(reversedMinHeap.IsEmpty
                            && maxHeap.IsEmpty
                            && reversedMinHeap.Count == 0
                            && maxHeap.Count == 0
                            && addedElements == removedElements);
        }
    }
}
