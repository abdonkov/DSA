using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DSA.DataStructures.Lists;
using System.Diagnostics;

namespace DSAUnitTests.DataStructures.Lists
{
    [TestClass]
    public class SinglyLinkedListTests
    {
        [TestMethod]
        public void AddFirstForAllElementsAndCheckForOrder()
        {
            var list = new SinglyLinkedList<int>();

            int itemCount = 1000;

            for (int i = 0; i < itemCount; i++)
            {
                list.AddFirst(i);
            }

            int previous = itemCount;
            int numberOfElementsInList = 0;
            foreach (var item in list)
            {
                if (previous < item) Assert.Fail();
                previous = item;
                numberOfElementsInList++;
            }

            Assert.IsTrue(list.Count == itemCount
                            && numberOfElementsInList == list.Count);
        }

        [TestMethod]
        public void AddLastForAllElementsAndCheckForOrder()
        {
            var list = new SinglyLinkedList<int>();

            int itemCount = 1000;

            for (int i = 0; i < itemCount; i++)
            {
                list.AddLast(i);
            }

            int previous = -1;
            int numberOfElementsInList = 0;
            foreach (var item in list)
            {
                if (previous > item) Assert.Fail();
                previous = item;
                numberOfElementsInList++;
            }

            Assert.IsTrue(list.Count == itemCount
                            && numberOfElementsInList == list.Count);
        }

        [TestMethod]
        public void AddAfterTheMiddleNodeAndCheckForOrder()
        {
            var list = new SinglyLinkedList<int>();

            int itemCount = 1000;
            var middleNode = new SinglyLinkedListNode<int>(0);

            for (int i = 0; i < itemCount; i++)
            {
                if (i == itemCount / 2) middleNode = list.AddLast(i);
                else list.AddLast(i);
            }

            for (int i = 0; i < itemCount; i++)
            {
                list.AddAfter(middleNode, i);
            }

            int j = 0;
            int k = 0;
            int numberOfElementsInList = 0;

            foreach (var item in list)
            {
                if (j <= itemCount / 2)
                {
                    if (j != item) Assert.Fail("1");
                }
                else if (j > itemCount / 2 && j <= itemCount + itemCount / 2)
                {
                    if (itemCount - 1 - k++ != item) Assert.Fail("2");
                }
                else
                {
                    if (j - itemCount != item) Assert.Fail("3");
                }

                j++;
                numberOfElementsInList++;
            }

            Assert.IsTrue(list.Count == 2 * itemCount
                            && numberOfElementsInList == list.Count);
        }

        [TestMethod]
        public void AddBeforeTheMiddleNodeAndCheckForOrder()
        {
            var list = new SinglyLinkedList<int>();

            int itemCount = 1000;
            var middleNode = new SinglyLinkedListNode<int>(0);

            for (int i = 0; i < itemCount; i++)
            {
                if (i == itemCount / 2) middleNode = list.AddLast(i);
                else list.AddLast(i);
            }

            for (int i = 0; i < itemCount; i++)
            {
                list.AddBefore(middleNode, i);
            }

            int j = 0;
            int k = 0;
            int numberOfElementsInList = 0;

            foreach (var item in list)
            {
                if (j < itemCount / 2)
                {
                    if (j != item) Assert.Fail("1");
                }
                else if (j >= itemCount / 2 && j < itemCount + itemCount / 2)
                {
                    if (k++ != item) Assert.Fail("2");
                }
                else
                {
                    if (j - itemCount != item) Assert.Fail("3");
                }

                j++;
                numberOfElementsInList++;
            }

            Assert.IsTrue(list.Count == 2 * itemCount
                            && numberOfElementsInList == list.Count);
        }

        [TestMethod]
        public void RemoveFirstForHalfTheItems()
        {
            var list = new SinglyLinkedList<int>();

            int itemCount = 1000;

            for (int i = 0; i < itemCount; i++)
            {
                list.AddLast(i);
            }

            for (int i = 0; i < itemCount / 2; i++)
            {
                list.RemoveFirst();
            }

            int current = itemCount / 2;
            int numberOfElementsInList = 0;
            foreach (var item in list)
            {
                if (current++ != item) Assert.Fail();
                numberOfElementsInList++;
            }

            Assert.IsTrue(list.Count == itemCount / 2
                            && numberOfElementsInList == list.Count);
        }

        [TestMethod]
        public void RemoveLastForHalfTheItems()
        {
            var list = new SinglyLinkedList<int>();

            int itemCount = 1000;

            for (int i = 0; i < itemCount; i++)
            {
                list.AddLast(i);
            }

            for (int i = 0; i < itemCount / 2; i++)
            {
                list.RemoveLast();
            }

            int current = 0;
            int numberOfElementsInList = 0;
            foreach (var item in list)
            {
                if (current++ != item) Assert.Fail();
                numberOfElementsInList++;
            }

            Assert.IsTrue(list.Count == itemCount / 2
                            && numberOfElementsInList == list.Count);
        }

        [TestMethod]
        public void RemoveMiddeNodeAndOnward()
        {
            var list = new SinglyLinkedList<int>();

            int itemCount = 1000;
            var middleNode = new SinglyLinkedListNode<int>(0);

            for (int i = 0; i < itemCount; i++)
            {
                if (i == itemCount / 2) middleNode = list.AddLast(i);
                else list.AddLast(i);
            }

            SinglyLinkedListNode<int> currentNode = middleNode;
            SinglyLinkedListNode<int> nextNode = middleNode;

            do
            {
                nextNode = currentNode.Next;
                list.Remove(currentNode);
                currentNode = nextNode;

            } while (currentNode != null);

            int current = 0;
            int numberOfElementsInList = 0;
            foreach (var item in list)
            {
                if (current++ != item) Assert.Fail();
                numberOfElementsInList++;
            }


            Assert.IsTrue(list.Count == itemCount / 2
                            && list.Last.Value == itemCount / 2 - 1
                            && numberOfElementsInList == list.Count);
        }

        [TestMethod]
        public void AddingAfterRemovingAllElements()
        {
            var list = new SinglyLinkedList<int>();

            int itemCount = 1000;

            for (int i = 0; i < itemCount; i++) list.AddLast(i);

            for (int i = 0; i < itemCount; i++) list.RemoveFirst();

            if (list.Count != 0) Assert.Fail();

            for (int i = 0; i < itemCount; i++) list.AddFirst(i);

            for (int i = 0; i < itemCount; i++) list.RemoveLast();

            if (list.Count != 0) Assert.Fail();

            for (int i = 0; i < itemCount; i++) list.AddLast(i);

            for (int i = 0; i < itemCount; i++) list.RemoveFirst();

            if (list.Count != 0) Assert.Fail();

            for (int i = 0; i < itemCount; i++) list.AddLast(i);

            for (int i = 0; i < itemCount; i++) list.RemoveLast();

            if (list.Count != 0) Assert.Fail();

            list.AddFirst(0);

            Assert.IsTrue(list.Count == 1
                            && list.First == list.Last
                            && list.First.Value == 0);
        }

        [TestMethod]
        public void AddingAfterClearingList()
        {
            var list = new SinglyLinkedList<int>();

            int itemCount = 1000;

            for (int i = 0; i < itemCount; i++)
            {
                list.AddLast(i);
            }

            list.Clear();

            if (list.Count != 0) Assert.Fail();

            list.AddFirst(0);

            Assert.IsTrue(list.Count == 1
                            && list.First == list.Last
                            && list.First.Value == 0);
        }

        [TestMethod]
        public void CheckIfContainedAfterAddingAndAfterRemoving()
        {
            var list = new SinglyLinkedList<int>();

            int itemCount = 1000;

            for (int i = 0; i < itemCount; i++)
            {
                if (!list.Contains(i)) list.AddLast(i);
            }

            if (list.Count != itemCount) Assert.Fail();

            for (int i = 0; i < itemCount; i++)
            {
                if (list.Contains(i)) list.RemoveFirst();
            }
            
            if (list.Count != 0) Assert.Fail();

            for (int i = 0; i < itemCount; i++)
            {
                if (!list.Contains(i)) list.AddFirst(i);
            }

            if (list.Count != itemCount) Assert.Fail();

            for (int i = 0; i < itemCount; i++)
            {
                if (list.Contains(i)) list.RemoveLast();
            }

            Assert.IsTrue(list.Count == 0);
        }

        [TestMethod]
        public void CheckIfNodeIsInvalidatedAfterRemoval()
        {
            var list = new SinglyLinkedList<int>();

            int itemCount = 1000;

            var firstNode = new SinglyLinkedListNode<int>(0);
            var lastNode = new SinglyLinkedListNode<int>(0);

            for (int i = 0; i < itemCount; i++)
            {
                lastNode = list.AddLast(i);
            }

            firstNode = list.First;

            list.RemoveFirst();
            list.RemoveLast();

            int current = list.First.Value;
            int numberOfElementsInList = 0;
            foreach (var item in list)
            {
                if (current++ != item) Assert.Fail();
                numberOfElementsInList++;
            }

            Assert.IsTrue(list.First != firstNode
                            && firstNode.Next == null
                            && firstNode.List == null
                            && lastNode != list.Last
                            && lastNode.Next == null
                            && lastNode.List == null
                            && numberOfElementsInList == list.Count);
        }

        [TestMethod]
        public void InitializeListFromIEnumeraable()
        {
            var list = new SinglyLinkedList<int>();

            int itemCount = 1000;

            for (int i = 0; i < itemCount; i++)
            {
                list.AddFirst(i);
            }

            var list2 = new SinglyLinkedList<int>(list);

            if (list.Count != list2.Count) Assert.Fail();

            foreach (var item in list)
            {
                if (item != list2.First.Value) Assert.Fail();
                list2.RemoveFirst();
            }

            Assert.IsTrue(list.Count == itemCount
                            && list2.Count == 0);
        }
    }
}
