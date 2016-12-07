using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using DSA.Algorithms.Searching;
using DSA.Algorithms.Sorting;

namespace DSAUnitTests.Algorithms.Searching
{
    [TestClass]
    public class LinearSearcherLinkedListTests
    {
        [TestMethod]
        public void SearchInListWithUniqueItems()
        {
            int itemCount = 2000;

            var list = new LinkedList<int>();

            for (int i = 0; i < itemCount; i++)
            {
                list.AddLast(i);
            }

            for (int i = 0; i < itemCount; i++)
            {
                var fi = list.LinearSearchFirstNode(i);
                var li = list.LinearSearchLastNode(i);

                if (fi != li) Assert.Fail();
                if (fi.Value != li.Value) Assert.Fail();
                if (fi.Value != i) Assert.Fail();
            }
        }

        [TestMethod]
        public void SearchInListWithNonUniqueItems()
        {
            int maxItem = 2000;

            var list = new LinkedList<int>();

            //Adding every seventh number, then every fifth number,
            //every third and at last all numbers
            //NOTE: some items are added more than once
            for (int i = 7; i > 0; i -= 2)
            {
                int el = 0;
                while (el <= maxItem)
                {
                    list.AddLast(el);
                    el += i;
                }
            }

            list.MergeSort();

            for (int i = 0; i <= maxItem; i++)
            {
                var fi = list.LinearSearchFirstNode(i);
                var li = list.LinearSearchLastNode(i);

                // Calculate number of occurrences of current number
                int occurNum = 1;
                if (i % 7 == 0) occurNum++;
                if (i % 5 == 0) occurNum++;
                if (i % 3 == 0) occurNum++;

                int cnt = 0;
                var curNode = fi;
                while (curNode != li.Next)
                {
                    if (curNode.Value != fi.Value) Assert.Fail();
                    if (curNode.Value != i) Assert.Fail();

                    curNode = curNode.Next;
                    cnt++;
                }

                if (cnt != occurNum) Assert.Fail();
            }
        }

        [TestMethod]
        public void SearchForMissingItems()
        {
            int maxItem = 2000;

            var list = new LinkedList<int>();

            // Add only even numbers
            for (int i = 0; i <= maxItem; i += 2)
            {
                list.AddLast(i);
            }

            for (int i = 1; i < maxItem; i += 2)
            {
                var fi = list.LinearSearchFirstNode(i);
                var li = list.LinearSearchLastNode(i);

                if (fi != null) Assert.Fail();
                if (li != null) Assert.Fail();
            }
        }

        [TestMethod]
        public void SearchInRangeOfItems()
        {
            int itemCount = 2000;

            var list = new LinkedList<int>();

            // Add only even numbers
            for (int i = 0; i < itemCount; i++)
            {
                list.AddLast(i);
            }

            int startIndex = list.Count / 3;
            int rangeCount = list.Count / 2;
            
            // Find corresponding nodes
            LinkedListNode<int> startNode = null;
            LinkedListNode<int> endNode = null;

            int curNodeIndex = 0;
            int nodesTraversedAfterStartNode = 0;
            var curNode = list.First;

            while (curNode != null)
            {
                // if we at the node on the given index save the start node
                if (curNodeIndex == startIndex)
                    startNode = curNode;

                // if we are at the node or after the given index, increment the node counter
                if (curNodeIndex >= startIndex)
                    nodesTraversedAfterStartNode++;

                // if we traversed enought nodes as the given count save the end node and break
                if (nodesTraversedAfterStartNode == rangeCount)
                {
                    endNode = curNode;
                    break;
                }

                // go onwards
                curNodeIndex++;
                curNode = curNode.Next;
            }

            // Check for items
            for (int i = startIndex; i < startIndex + rangeCount - 1; i++)
            {
                var fi = list.LinearSearchFirstNode(i, startNode, endNode, null);
                var li = list.LinearSearchLastNode(i, startNode, endNode, null);

                if (fi != li) Assert.Fail();
                if (fi.Value != li.Value) Assert.Fail();
                if (fi.Value != i) Assert.Fail();
            }

        }
    }
}
