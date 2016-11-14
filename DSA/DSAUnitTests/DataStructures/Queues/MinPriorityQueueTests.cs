using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DSA.DataStructures.Queues;
using System.Collections.Generic;

namespace DSAUnitTests.DataStructures.Queues
{
    [TestClass]
    public class MinPriorityQueueTests
    {
        [TestMethod]
        public void AddingElementsAndCheckingIfExtractedInSortedOrder()
        {
            var pq = new MinPriorityQueue<int, int>();

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
                    pq.Enqueue(el, -el);
                    addedElements++;
                    el += i;
                }
            }

            if (pq.Count != addedElements) Assert.Fail();

            int removedElements = 0;
            var min = pq.Peek();
            while (!pq.IsEmpty)
            {
                var kvp = pq.Peek();
                if (min.Key > kvp.Key) Assert.Fail();

                min = pq.Dequeue();
                removedElements++;
            }

            Assert.IsTrue(pq.IsEmpty
                            && pq.Count == 0
                            && addedElements == removedElements);
        }

        [TestMethod]
        public void AddingElementsWithCustomComparerAndCheckingIfExtractedInSortedOrder()
        {
            //Creating heap with reversed comparer
            var pq = new MinPriorityQueue<int, int>(Comparer<int>.Create(
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
                    pq.Enqueue(el, -el);
                    addedElements++;
                    el += i;
                }
            }

            if (pq.Count != addedElements) Assert.Fail();

            int removedElements = 0;
            // because of the reversed comparer
            var max = pq.Peek();
            while (!pq.IsEmpty)
            {
                var kvp = pq.Peek();
                if (max.Key < kvp.Key) Assert.Fail();

                max = pq.Dequeue();
                removedElements++;
            }

            Assert.IsTrue(pq.IsEmpty
                            && pq.Count == 0
                            && addedElements == removedElements);
        }

        [TestMethod]
        public void HeapifyUnsortedCollectionAndCheckIfExtractedInSortedOrder()
        {
            var pq = new MinPriorityQueue<int, int>();

            var unsortedList = new List<KeyValuePair<int, int>>();

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
                    unsortedList.Add(new KeyValuePair<int, int>(el, -el));
                    addedElements++;
                    el += i;
                }
            }

            pq.Heapify(unsortedList);

            if (pq.Count != addedElements) Assert.Fail("1");

            int removedElements = 0;
            var min = pq.Peek();
            while (!pq.IsEmpty)
            {
                var kvp = pq.Peek();
                if (min.Key > kvp.Key) Assert.Fail("2");

                min = pq.Dequeue();
                removedElements++;
            }

            Assert.IsTrue(pq.IsEmpty
                            && pq.Count == 0
                            && addedElements == removedElements);
        }

        [TestMethod]
        public void AddingAfterClearingHeap()
        {
            var pq = new MinPriorityQueue<int, int>();

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
                    pq.Enqueue(el, -el);
                    addedElements++;
                    el += i;
                }
            }

            if (pq.Count != addedElements) Assert.Fail();

            pq.Clear();

            if (pq.Count != 0) Assert.Fail();

            addedElements = 0;

            //Adding every seventh number, then every fifth number,
            //every third and at last all numbers
            //NOTE: some items are added more than once
            for (int i = 7; i > 0; i -= 2)
            {
                int el = minHeapElement;
                while (el < maxHeapElement)
                {
                    pq.Enqueue(el, -el);
                    addedElements++;
                    el += i;
                }
            }

            if (pq.Count != addedElements) Assert.Fail();

            int removedElements = 0;
            var min = pq.Peek();
            while (!pq.IsEmpty)
            {
                var kvp = pq.Peek();
                if (min.Key > kvp.Key) Assert.Fail();

                min = pq.Dequeue();
                removedElements++;
            }

            Assert.IsTrue(pq.IsEmpty
                            && pq.Count == 0
                            && addedElements == removedElements);
        }

        [TestMethod]
        public void ReplacingMinElementAndCheckingIfExtractedInSortedOrder()
        {
            var pq = new MinPriorityQueue<int, int>();

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
                    pq.Enqueue(el, -el);
                    addedElements++;
                    el += i;
                }
            }

            if (pq.Count != addedElements) Assert.Fail();

            pq.ReplaceFirst(int.MaxValue, -int.MaxValue);
            pq.ReplaceFirst(int.MaxValue, -int.MaxValue);
            pq.ReplaceFirst(int.MaxValue, -int.MaxValue);

            int removedElements = 0;
            var min = pq.Peek();
            while (!pq.IsEmpty)
            {
                var kvp = pq.Peek();
                if (min.Key > kvp.Key) Assert.Fail();

                min = pq.Dequeue();
                removedElements++;
            }

            Assert.IsTrue(pq.IsEmpty
                            && pq.Count == 0
                            && addedElements == removedElements);
        }

        [TestMethod]
        public void MergingTwoPriorityQueuesAndCheckingIfExtractedInSortedOrder()
        {
            var pq1 = new MinPriorityQueue<int, int>();
            var pq2 = new MinPriorityQueue<int, int>();

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
                    pq1.Enqueue(el, -el);
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
                    pq2.Enqueue(el, -el);
                    addedElementsInSecondHeap++;
                    el += i;
                }
            }

            if (pq1.Count != addedElementsInFirstHeap) Assert.Fail("first priority queue incorrect count");
            if (pq2.Count != addedElementsInSecondHeap) Assert.Fail("second priority queue incorrect count");

            var oldHeap = new MinPriorityQueue<int, int>();
            oldHeap.Heapify(pq1.ToArray());

            pq1.Merge(pq2);
            pq2.Merge(oldHeap);

            int mergedHeapElements = addedElementsInFirstHeap + addedElementsInSecondHeap;

            if (pq1.Count != mergedHeapElements) Assert.Fail("merged first with second incorect count");
            if (pq2.Count != mergedHeapElements) Assert.Fail("merged second with first incorrect count");

            var min1 = pq1.Peek();
            var min2 = pq2.Peek();
            if (min1.Key != min2.Key) Assert.Fail("merged priority queues min element is different");

            int removedElements = 0;
            while (!pq1.IsEmpty && !pq2.IsEmpty)
            {
                var kvp1 = pq1.Peek();
                var kvp2 = pq2.Peek();
                if (min1.Key > kvp1.Key) Assert.Fail();
                if (min2.Key > kvp2.Key) Assert.Fail();

                min1 = pq1.Dequeue();
                min2 = pq2.Dequeue();
                removedElements++;

                if (min1.Key != min2.Key) Assert.Fail("merged priority queues min element is different");
            }

            Assert.IsTrue(pq1.IsEmpty
                            && pq2.IsEmpty
                            && pq1.Count == 0
                            && pq2.Count == 0
                            && mergedHeapElements == removedElements);
        }
    }
}
