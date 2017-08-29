using DSA.DataStructures.Lists;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DSA.Algorithms.Sorting
{
    public static partial class ParallelMergeSorter
    {
        /// <summary>
        /// Sorts the elements in the entire <see cref="SinglyLinkedList{T}"/> in ascending order using the default comparer.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="SinglyLinkedList{T}"/>.</typeparam>
        /// <param name="list">The <see cref="SinglyLinkedList{T}"/> containing the elements for sorting.</param>
        /// <returns>Returns the given <see cref="SinglyLinkedList{T}"/> when sorted.</returns>
        public static SinglyLinkedList<T> ParallelMergeSort<T>(this SinglyLinkedList<T> list)
        {
            return ParallelMergeSort(list, Comparer<T>.Default);
        }

        /// <summary>
        /// Sorts the elements in the entire <see cref="SinglyLinkedList{T}"/> in descending order using the default comparer.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="SinglyLinkedList{T}"/>.</typeparam>
        /// <param name="list">The <see cref="SinglyLinkedList{T}"/> containing the elements for sorting.</param>
        /// <returns>Returns the given <see cref="SinglyLinkedList{T}"/> when sorted.</returns>
        public static SinglyLinkedList<T> ParallelMergeSortDescending<T>(this SinglyLinkedList<T> list)
        {
            return ParallelMergeSortDescending(list, Comparer<T>.Default);
        }

        /// <summary>
        /// Sorts the elements in the entire <see cref="SinglyLinkedList{T}"/> in ascending order using the specified comparison.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="SinglyLinkedList{T}"/>.</typeparam>
        /// <param name="list">The <see cref="SinglyLinkedList{T}"/> containing the elements for sorting.</param>
        /// <param name="comparison">The <see cref="Comparison{T}"/> used for comparing the elements.</param>
        /// <returns>Returns the given <see cref="SinglyLinkedList{T}"/> when sorted.</returns>
        public static SinglyLinkedList<T> ParallelMergeSort<T>(this SinglyLinkedList<T> list, Comparison<T> comparison)
        {
            if (comparison == null) throw new ArgumentNullException(nameof(comparison));

            return ParallelMergeSort(list, Comparer<T>.Create(comparison));
        }

        /// <summary>
        /// Sorts the elements in the entire <see cref="SinglyLinkedList{T}"/> in descending order using the specified comparison.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="SinglyLinkedList{T}"/>.</typeparam>
        /// <param name="list">The <see cref="SinglyLinkedList{T}"/> containing the elements for sorting.</param>
        /// <param name="comparison">The <see cref="Comparison{T}"/> used for comparing the elements.</param>
        /// <returns>Returns the given <see cref="SinglyLinkedList{T}"/> when sorted.</returns>
        public static SinglyLinkedList<T> ParallelMergeSortDescending<T>(this SinglyLinkedList<T> list, Comparison<T> comparison)
        {
            if (comparison == null) throw new ArgumentNullException(nameof(comparison));

            return ParallelMergeSortDescending(list, Comparer<T>.Create(comparison));
        }

        /// <summary>
        /// Sorts the elements in the entire <see cref="SinglyLinkedList{T}"/> in ascending order using the specified comparer.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="SinglyLinkedList{T}"/>.</typeparam>
        /// <param name="list">The <see cref="SinglyLinkedList{T}"/> containing the elements for sorting.</param>
        /// <param name="comparer">The <see cref="IComparable{T}"/> implementation used for comparing the elements,
        /// or null to use the default comparer <see cref="Comparer{T}.Default"/>.</param>
        /// <returns>Returns the given <see cref="SinglyLinkedList{T}"/> when sorted.</returns>
        public static SinglyLinkedList<T> ParallelMergeSort<T>(this SinglyLinkedList<T> list, IComparer<T> comparer)
        {
            if (list.Count > 0)
                return ParallelMergeSort(list, list.First, list.Last, comparer);
            else
                return list;
        }

        /// <summary>
        /// Sorts the elements in the entire <see cref="SinglyLinkedList{T}"/> in descending order using the specified comparer.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="SinglyLinkedList{T}"/>.</typeparam>
        /// <param name="list">The <see cref="SinglyLinkedList{T}"/> containing the elements for sorting.</param>
        /// <param name="comparer">The <see cref="IComparable{T}"/> implementation used for comparing the elements,
        /// or null to use the default comparer <see cref="Comparer{T}.Default"/>.</param>
        /// <returns>Returns the given <see cref="SinglyLinkedList{T}"/> when sorted.</returns>
        public static SinglyLinkedList<T> ParallelMergeSortDescending<T>(this SinglyLinkedList<T> list, IComparer<T> comparer)
        {
            if (list.Count > 0)
                return ParallelMergeSortDescending(list, list.First, list.Last, comparer);
            else
                return list;
        }

        /// <summary>
        /// Sorts a range of elements in the <see cref="SinglyLinkedList{T}"/> in ascending order using the specified comparer.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="SinglyLinkedList{T}"/>.</typeparam>
        /// <param name="list">The <see cref="SinglyLinkedList{T}"/> containing the elements for sorting.</param>
        /// <param name="index">The zero-based starting index of the range for sorting.</param>
        /// <param name="count">The length of the range for sorting.</param>
        /// <param name="comparer">The <see cref="IComparable{T}"/> implementation used for comparing the elements,
        /// or null to use the default comparer <see cref="Comparer{T}.Default"/>.</param>
        /// <returns>Returns the given <see cref="SinglyLinkedList{T}"/> when sorted.</returns>
        public static SinglyLinkedList<T> ParallelMergeSort<T>(this SinglyLinkedList<T> list, int index, int count, IComparer<T> comparer)
        {
            if (index < 0 || index >= list.Count) throw new ArgumentOutOfRangeException(nameof(index));
            if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));
            if (index + count > list.Count) throw new ArgumentException("Invalid length specified.");

            if (comparer == null) comparer = Comparer<T>.Default;

            SinglyLinkedListNode<T> startNode = null;
            SinglyLinkedListNode<T> endNode = null;

            int curNodeIndex = 0;
            int nodesTraversedAfterStartNode = 0;
            var curNode = list.First;

            while (curNode != null)
            {
                // if we at the node on the given index save the start node
                if (curNodeIndex == index)
                    startNode = curNode;

                // if we are at the node or after the given index, increment the node counter
                if (curNodeIndex >= index)
                    nodesTraversedAfterStartNode++;

                // if we traversed enought nodes as the given count save the end node and break
                if (nodesTraversedAfterStartNode == count)
                {
                    endNode = curNode;
                    break;
                }

                // go onwards
                curNodeIndex++;
                curNode = curNode.Next;
            }

            return ParallelMergeSort(list, startNode, endNode, comparer);
        }

        /// <summary>
        /// Sorts a range of elements in the <see cref="SinglyLinkedList{T}"/> in descending order using the specified comparer.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="SinglyLinkedList{T}"/>.</typeparam>
        /// <param name="list">The <see cref="SinglyLinkedList{T}"/> containing the elements for sorting.</param>
        /// <param name="index">The zero-based starting index of the range for sorting.</param>
        /// <param name="count">The length of the range for sorting.</param>
        /// <param name="comparer">The <see cref="IComparable{T}"/> implementation used for comparing the elements,
        /// or null to use the default comparer <see cref="Comparer{T}.Default"/>.</param>
        /// <returns>Returns the given <see cref="SinglyLinkedList{T}"/> when sorted.</returns>
        public static SinglyLinkedList<T> ParallelMergeSortDescending<T>(this SinglyLinkedList<T> list, int index, int count, IComparer<T> comparer)
        {
            if (index < 0 || index >= list.Count) throw new ArgumentOutOfRangeException(nameof(index));
            if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));
            if (index + count > list.Count) throw new ArgumentException("Invalid length specified.");

            if (comparer == null) comparer = Comparer<T>.Default;

            SinglyLinkedListNode<T> startNode = null;
            SinglyLinkedListNode<T> endNode = null;

            int curNodeIndex = 0;
            int nodesTraversedAfterStartNode = 0;
            var curNode = list.First;

            while (curNode != null)
            {
                // if we at the node on the given index save the start node
                if (curNodeIndex == index)
                    startNode = curNode;

                // if we are at the node or after the given index, increment the node counter
                if (curNodeIndex >= index)
                    nodesTraversedAfterStartNode++;

                // if we traversed enought nodes as the given count save the end node and break
                if (nodesTraversedAfterStartNode == count)
                {
                    endNode = curNode;
                    break;
                }

                // go onwards
                curNodeIndex++;
                curNode = curNode.Next;
            }

            return ParallelMergeSortDescending(list, startNode, endNode, comparer);
        }

        /// <summary>
        /// Sorts a range of elements in the <see cref="SinglyLinkedList{T}"/> in ascending order using the specified comparer beginning from the start node and ending at the end node.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="SinglyLinkedList{T}"/>.</typeparam>
        /// <param name="list">The <see cref="SinglyLinkedList{T}"/> containing the elements for sorting.</param>
        /// <param name="startNode">The start <see cref="SinglyLinkedListNode{T}"/> of the range for sorting.</param>
        /// <param name="endNode">The end <see cref="SinglyLinkedListNode{T}"/> of the range for sorting.</param>
        /// <param name="comparer">The <see cref="IComparable{T}"/> implementation used for comparing the elements,
        /// or null to use the default comparer <see cref="Comparer{T}.Default"/>.</param>
        /// <returns>Returns the given <see cref="SinglyLinkedList{T}"/> when sorted.</returns>
        public static SinglyLinkedList<T> ParallelMergeSort<T>(this SinglyLinkedList<T> list, SinglyLinkedListNode<T> startNode, SinglyLinkedListNode<T> endNode, IComparer<T> comparer)
        {
            if (startNode == null) throw new ArgumentNullException(nameof(startNode));
            if (endNode == null) throw new ArgumentNullException(nameof(endNode));
            if (startNode.List != list) throw new ArgumentException(nameof(startNode) + "doesn't belong to the list!");
            if (endNode.List != list) throw new ArgumentException(nameof(endNode) + "doesnt't belong to the list!");

            if (comparer == null) comparer = Comparer<T>.Default;

            // Calculate max recursion depth for parallelism
            int depth = Environment.ProcessorCount;

            if (list.First == startNode)// the start node is the first in the list
                ParallelSplitMerge(list, null, startNode, endNode, depth, comparer);// the node before it is null
            else
            {
                var curNode = list.First;
                while (curNode.Next != startNode)
                {
                    curNode = curNode.Next;
                }
                ParallelSplitMerge(list, curNode, startNode, endNode, depth, comparer);
            }

            return list;
        }

        /// <summary>
        /// Parallel algorithm. In-place sorting of the given <see cref="SinglyLinkedList{T}"/> elements. Used for ascending sort.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="SinglyLinkedList{T}"/>.</typeparam>
        /// <param name="list">The <see cref="SinglyLinkedList{T}"/> containing the elements for sorting.</param>
        /// <param name="nodeBeforeStartNode">The <see cref="SinglyLinkedListNode{T}"/> which is the node before the sorted sequence.</param>
        /// <param name="startNode">The start <see cref="SinglyLinkedListNode{T}"/> of the current split.</param>
        /// <param name="endNode">The end <see cref="SinglyLinkedListNode{T}"/> of the current split.</param>
        /// <param name="depthRemaining">The remaining depth of the recursion for which parallel tasks are invoked.</param>
        /// <param name="comparer">The <see cref="IComparable{T}"/> implementation used for comparing the elements.</param>
        private static void ParallelSplitMerge<T>(SinglyLinkedList<T> list, SinglyLinkedListNode<T> nodeBeforeStartNode, SinglyLinkedListNode<T> startNode, SinglyLinkedListNode<T> endNode, int depthRemaining, IComparer<T> comparer)
        {
            // End of recursion. If we have 1 item or less we consider it sorted
            if (list.Count < 2) return;

            // spliting the list into two lists
            var left = new SinglyLinkedList<T>();
            var right = new SinglyLinkedList<T>();

            var curNode = startNode;
            var nodeAfterEndNode = endNode.Next;
            bool nodeBeforeStartNodeIsNull = nodeBeforeStartNode == null;
            SinglyLinkedListNode<T> lastLeftNode = null;
            SinglyLinkedListNode<T> lastRightNode = null;
            int i = 0;
            int leftItemsNumber = list.Count / 2;
            while (curNode != nodeAfterEndNode)
            {
                if (i++ < leftItemsNumber)
                {
                    var node = curNode;// save the current node
                    curNode = curNode.Next;// go to the next node
                    if (nodeBeforeStartNodeIsNull)// if the start node is first in the list
                        list.Remove(node);// remove the current node(which is the first node)
                    else// if the start node is not the first in the list
                        list.RemoveAfter(nodeBeforeStartNode);// remove the node after the node before the start node(i.e. the current node)
                    if (lastLeftNode == null)// if first time(no last node)
                        left.AddFirst(node);// add at the beginning
                    else// if we have last node
                        left.AddAfter(lastLeftNode, node);// add after it(faster than AddLast method)

                    lastLeftNode = node; // update last node
                }
                else
                {
                    var node = curNode;// save the current node
                    curNode = curNode.Next;// go to the next node
                    if (nodeBeforeStartNodeIsNull)// if the start node is first in the list
                        list.Remove(node);// remove the current node(which is the first node)
                    else// if the start node is not the first in the list
                        list.RemoveAfter(nodeBeforeStartNode);// remove the node after the node before the start node(i.e. the current node)
                    if (lastRightNode == null)// if first time(no last node)
                        right.AddFirst(node);// add at the beginning
                    else// if we have last node
                        right.AddAfter(lastRightNode, node);// add after it(faster than AddLast method)

                    lastRightNode = node; // update last node
                }
            }

            // Recursively sort both sublists
            // NOTE: node before startNode is null because in the left and right splits
            // we work with all nodes from the list.

            // if depth is low enough, we create new tasks for the spliting
            if (depthRemaining > 0)
            {
                Parallel.Invoke
                    (
                        () => ParallelSplitMerge(left, null, left.First, left.Last, depthRemaining - 1, comparer),
                        () => ParallelSplitMerge(right, null, right.First, right.Last, depthRemaining - 1, comparer)
                    );
            }
            else // if max depth level is exceeded we continue with sequential spliting
            {
                SequentialSplitMerge(left, null, left.First, left.Last, comparer);
                SequentialSplitMerge(right, null, right.First, right.Last, comparer);
            }

            // Merge the two splits into the given list
            Merge(list, left, right, nodeBeforeStartNode, comparer);

            // NOTE: only the last merging of two lists is done on the given list for sorting
        }

        /// <summary>
        /// Sequential algorithm. In-place sorting of the given <see cref="SinglyLinkedList{T}"/> elements. Used for ascending sort.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="SinglyLinkedList{T}"/>.</typeparam>
        /// <param name="list">The <see cref="SinglyLinkedList{T}"/> containing the elements for sorting.</param>
        /// <param name="nodeBeforeStartNode">The <see cref="SinglyLinkedListNode{T}"/> which is the node before the sorted sequence.</param>
        /// <param name="startNode">The start <see cref="SinglyLinkedListNode{T}"/> of the current split.</param>
        /// <param name="endNode">The end <see cref="SinglyLinkedListNode{T}"/> of the current split.</param>
        /// <param name="comparer">The <see cref="IComparable{T}"/> implementation used for comparing the elements.</param>
        private static void SequentialSplitMerge<T>(SinglyLinkedList<T> list, SinglyLinkedListNode<T> nodeBeforeStartNode, SinglyLinkedListNode<T> startNode, SinglyLinkedListNode<T> endNode, IComparer<T> comparer)
        {
            // End of recursion. If we have 1 item or less we consider it sorted
            if (list.Count < 2) return;

            // spliting the list into two lists
            var left = new SinglyLinkedList<T>();
            var right = new SinglyLinkedList<T>();

            var curNode = startNode;
            var nodeAfterEndNode = endNode.Next;
            bool nodeBeforeStartNodeIsNull = nodeBeforeStartNode == null;
            SinglyLinkedListNode<T> lastLeftNode = null;
            SinglyLinkedListNode<T> lastRightNode = null;
            int i = 0;
            int leftItemsNumber = list.Count / 2;
            while (curNode != nodeAfterEndNode)
            {
                if (i++ < leftItemsNumber)
                {
                    var node = curNode;// save the current node
                    curNode = curNode.Next;// go to the next node
                    if (nodeBeforeStartNodeIsNull)// if the start node is first in the list
                        list.Remove(node);// remove the current node(which is the first node)
                    else// if the start node is not the first in the list
                        list.RemoveAfter(nodeBeforeStartNode);// remove the node after the node before the start node(i.e. the current node)
                    if (lastLeftNode == null)// if first time(no last node)
                        left.AddFirst(node);// add at the beginning
                    else// if we have last node
                        left.AddAfter(lastLeftNode, node);// add after it(faster than AddLast method)

                    lastLeftNode = node; // update last node
                }
                else
                {
                    var node = curNode;// save the current node
                    curNode = curNode.Next;// go to the next node
                    if (nodeBeforeStartNodeIsNull)// if the start node is first in the list
                        list.Remove(node);// remove the current node(which is the first node)
                    else// if the start node is not the first in the list
                        list.RemoveAfter(nodeBeforeStartNode);// remove the node after the node before the start node(i.e. the current node)
                    if (lastRightNode == null)// if first time(no last node)
                        right.AddFirst(node);// add at the beginning
                    else// if we have last node
                        right.AddAfter(lastRightNode, node);// add after it(faster than AddLast method)

                    lastRightNode = node; // update last node
                }
            }

            // Recursively sort both sublists
            // NOTE: node before startNode is null because in the left and right splits
            // we work with all nodes from the list.
            SequentialSplitMerge(left, null, left.First, left.Last, comparer);
            SequentialSplitMerge(right, null, right.First, right.Last, comparer);

            // Merge the two splits into the given list
            Merge(list, left, right, nodeBeforeStartNode, comparer);

            // NOTE: only the last merging of two lists is done on the given list for sorting
        }

        /// <summary>
        /// Merging two lists into a given <see cref="SinglyLinkedList{T}"/>. Used for ascending sort.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="SinglyLinkedList{T}"/>.</typeparam>
        /// <param name="mergeList">The <see cref="SinglyLinkedList{T}"/> for merging the left and right lists into.</param>
        /// <param name="left">The left <see cref="SinglyLinkedList{T}"/> containing some of the elements for sorting.</param>
        /// <param name="right">The right <see cref="SinglyLinkedList{T}"/> containing some of the elements for sorting.</param>
        /// <param name="nodeBeforeStartNode">The <see cref="SinglyLinkedListNode{T}"/> which is the node after the sorted sequence.</param>
        /// <param name="comparer">The <see cref="IComparable{T}"/> implementation used for comparing the elements.</param>
        /// <returns>Returns the given <see cref="SinglyLinkedList{T}"/> for the merged elements from the left <see cref="SinglyLinkedList{T}"/> and right <see cref="SinglyLinkedList{T}"/>.</returns>
        private static void Merge<T>(SinglyLinkedList<T> mergeList, SinglyLinkedList<T> left, SinglyLinkedList<T> right, SinglyLinkedListNode<T> nodeBeforeStartNode, IComparer<T> comparer)
        {
            bool nodeBeforeStartNodeIsNull = nodeBeforeStartNode == null;
            SinglyLinkedListNode<T> lastAddedNode = nodeBeforeStartNode;

            // merging until one of the lists becomes empty
            while (left.Count > 0 && right.Count > 0)
            {
                if (comparer.Compare(left.First.Value, right.First.Value) <= 0)
                {
                    var node = left.First;
                    left.RemoveFirst();
                    if (nodeBeforeStartNodeIsNull)// if the start node is the first in the list
                    {
                        // Add after the last added node(if null add first)
                        if (lastAddedNode == null) mergeList.AddFirst(node);
                        else mergeList.AddAfter(lastAddedNode, node);
                    }
                    else// if the start node is not the first
                        mergeList.AddAfter(lastAddedNode, node);// add after the last added node

                    lastAddedNode = node;// Update last added node
                }
                else
                {
                    var node = right.First;
                    right.RemoveFirst();
                    if (nodeBeforeStartNodeIsNull)// if the start node is the first in the list
                    {
                        // Add after the last added node(if null add first)
                        if (lastAddedNode == null) mergeList.AddFirst(node);
                        else mergeList.AddAfter(lastAddedNode, node);
                    }
                    else// if the start node is not the first
                        mergeList.AddAfter(lastAddedNode, node);// add after the last added node

                    lastAddedNode = node;// Update last added node
                }
            }

            // add the remaining elements from the left or the right list
            while (left.Count > 0)
            {
                var node = left.First;
                left.RemoveFirst();
                if (nodeBeforeStartNodeIsNull)// if the start node is the first in the list
                {
                    // Add after the last added node(if null add first)
                    if (lastAddedNode == null) mergeList.AddFirst(node);
                    else mergeList.AddAfter(lastAddedNode, node);
                }
                else// if the start node is not the first
                    mergeList.AddAfter(lastAddedNode, node);// add after the last added node

                lastAddedNode = node;// Update last added node
            }

            while (right.Count > 0)
            {
                var node = right.First;
                right.RemoveFirst();
                if (nodeBeforeStartNodeIsNull)// if the start node is the first in the list
                {
                    // Add after the last added node(if null add first)
                    if (lastAddedNode == null) mergeList.AddFirst(node);
                    else mergeList.AddAfter(lastAddedNode, node);
                }
                else// if the start node is not the first
                    mergeList.AddAfter(lastAddedNode, node);// add after the last added node

                lastAddedNode = node;// Update last added node
            }
        }

        /// <summary>
        /// Sorts a range of elements in the <see cref="SinglyLinkedList{T}"/> in descending order using the specified comparer beginning from the start node and ending at the end node.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="SinglyLinkedList{T}"/>.</typeparam>
        /// <param name="list">The <see cref="SinglyLinkedList{T}"/> containing the elements for sorting.</param>
        /// <param name="startNode">The start <see cref="SinglyLinkedListNode{T}"/> of the range for sorting.</param>
        /// <param name="endNode">The end <see cref="SinglyLinkedListNode{T}"/> of the range for sorting.</param>
        /// <param name="comparer">The <see cref="IComparable{T}"/> implementation used for comparing the elements,
        /// or null to use the default comparer <see cref="Comparer{T}.Default"/>.</param>
        /// <returns>Returns the given <see cref="SinglyLinkedList{T}"/> when sorted.</returns>
        public static SinglyLinkedList<T> ParallelMergeSortDescending<T>(this SinglyLinkedList<T> list, SinglyLinkedListNode<T> startNode, SinglyLinkedListNode<T> endNode, IComparer<T> comparer)
        {
            if (startNode == null) throw new ArgumentNullException(nameof(startNode));
            if (endNode == null) throw new ArgumentNullException(nameof(endNode));
            if (startNode.List != list) throw new ArgumentException(nameof(startNode) + "doesn't belong to the list!");
            if (endNode.List != list) throw new ArgumentException(nameof(endNode) + "doesnt't belong to the list!");

            if (comparer == null) comparer = Comparer<T>.Default;

            // Calculate max recursion depth for parallelism
            int depth = Environment.ProcessorCount;

            if (list.Last == endNode)// the start node is the first in the list
                ParallelSplitMergeDescending(list, null, startNode, endNode, depth, comparer);// the node before it is null
            else
            {
                var curNode = list.First;
                while (curNode.Next != startNode)
                {
                    curNode = curNode.Next;
                }
                ParallelSplitMergeDescending(list, curNode, startNode, endNode, depth, comparer);
            }

            return list;
        }

        /// <summary>
        /// Parallel algorithm. In-place sorting of the given <see cref="SinglyLinkedList{T}"/> elements. Used for descending sort.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="SinglyLinkedList{T}"/>.</typeparam>
        /// <param name="list">The <see cref="SinglyLinkedList{T}"/> containing the elements for sorting.</param>
        /// <param name="nodeBeforeStartNode">The <see cref="SinglyLinkedListNode{T}"/> which is the node before the sorted sequence.</param>
        /// <param name="startNode">The start <see cref="SinglyLinkedListNode{T}"/> of the current split.</param>
        /// <param name="endNode">The end <see cref="SinglyLinkedListNode{T}"/> of the current split.</param>
        /// <param name="depthRemaining">The remaining depth of the recursion for which parallel tasks are invoked.</param>
        /// <param name="comparer">The <see cref="IComparable{T}"/> implementation used for comparing the elements.</param>
        private static void ParallelSplitMergeDescending<T>(SinglyLinkedList<T> list, SinglyLinkedListNode<T> nodeBeforeStartNode, SinglyLinkedListNode<T> startNode, SinglyLinkedListNode<T> endNode, int depthRemaining, IComparer<T> comparer)
        {
            // End of recursion. If we have 1 item or less we consider it sorted
            if (list.Count < 2) return;

            // spliting the list into two lists
            var left = new SinglyLinkedList<T>();
            var right = new SinglyLinkedList<T>();

            var curNode = startNode;
            var nodeAfterEndNode = endNode.Next;
            bool nodeBeforeStartNodeIsNull = nodeBeforeStartNode == null;
            SinglyLinkedListNode<T> lastLeftNode = null;
            SinglyLinkedListNode<T> lastRightNode = null;
            int i = 0;
            int leftItemsNumber = list.Count / 2;
            while (curNode != nodeAfterEndNode)
            {
                if (i++ < leftItemsNumber)
                {
                    var node = curNode;// save the current node
                    curNode = curNode.Next;// go to the next node
                    if (nodeBeforeStartNodeIsNull)// if the start node is first in the list
                        list.Remove(node);// remove the current node(which is the first node)
                    else// if the start node is not the first in the list
                        list.RemoveAfter(nodeBeforeStartNode);// remove the node after the node before the start node(i.e. the current node)
                    if (lastLeftNode == null)// if first time(no last node)
                        left.AddFirst(node);// add at the beginning
                    else// if we have last node
                        left.AddAfter(lastLeftNode, node);// add after it(faster than AddLast method)

                    lastLeftNode = node; // update last node
                }
                else
                {
                    var node = curNode;// save the current node
                    curNode = curNode.Next;// go to the next node
                    if (nodeBeforeStartNodeIsNull)// if the start node is first in the list
                        list.Remove(node);// remove the current node(which is the first node)
                    else// if the start node is not the first in the list
                        list.RemoveAfter(nodeBeforeStartNode);// remove the node after the node before the start node(i.e. the current node)
                    if (lastRightNode == null)// if first time(no last node)
                        right.AddFirst(node);// add at the beginning
                    else// if we have last node
                        right.AddAfter(lastRightNode, node);// add after it(faster than AddLast method)

                    lastRightNode = node; // update last node
                }
            }

            // Recursively sort both sublists
            // NOTE: node before startNode is null because in the left and right splits
            // we work with all nodes from the list.

            // if depth is low enough, we create new tasks for the spliting
            if (depthRemaining > 0)
            {
                Parallel.Invoke
                    (
                        () => ParallelSplitMergeDescending(left, null, left.First, left.Last, depthRemaining - 1, comparer),
                        () => ParallelSplitMergeDescending(right, null, right.First, right.Last, depthRemaining - 1, comparer)
                    );
            }
            else // if max depth level is exceeded we continue with sequential spliting
            {
                SequentialSplitMergeDescending(left, null, left.First, left.Last, comparer);
                SequentialSplitMergeDescending(right, null, right.First, right.Last, comparer);
            }

            // Merge the two splits into the given list
            MergeDescending(list, left, right, nodeBeforeStartNode, comparer);

            // NOTE: only the last merging of two lists is done on the given list for sorting
        }

        /// <summary>
        /// Sequential algorithm. In-place sorting of the given <see cref="SinglyLinkedList{T}"/> elements. Used for descending sort.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="SinglyLinkedList{T}"/>.</typeparam>
        /// <param name="list">The <see cref="SinglyLinkedList{T}"/> containing the elements for sorting.</param>
        /// <param name="nodeBeforeStartNode">The <see cref="SinglyLinkedListNode{T}"/> which is the node before the sorted sequence.</param>
        /// <param name="startNode">The start <see cref="SinglyLinkedListNode{T}"/> of the current split.</param>
        /// <param name="endNode">The end <see cref="SinglyLinkedListNode{T}"/> of the current split.</param>
        /// <param name="comparer">The <see cref="IComparable{T}"/> implementation used for comparing the elements.</param>
        private static void SequentialSplitMergeDescending<T>(SinglyLinkedList<T> list, SinglyLinkedListNode<T> nodeBeforeStartNode, SinglyLinkedListNode<T> startNode, SinglyLinkedListNode<T> endNode, IComparer<T> comparer)
        {
            // End of recursion. If we have 1 item or less we consider it sorted
            if (list.Count < 2) return;

            // spliting the list into two lists
            var left = new SinglyLinkedList<T>();
            var right = new SinglyLinkedList<T>();

            var curNode = startNode;
            var nodeAfterEndNode = endNode.Next;
            bool nodeBeforeStartNodeIsNull = nodeBeforeStartNode == null;
            SinglyLinkedListNode<T> lastLeftNode = null;
            SinglyLinkedListNode<T> lastRightNode = null;
            int i = 0;
            int leftItemsNumber = list.Count / 2;
            while (curNode != nodeAfterEndNode)
            {
                if (i++ < leftItemsNumber)
                {
                    var node = curNode;// save the current node
                    curNode = curNode.Next;// go to the next node
                    if (nodeBeforeStartNodeIsNull)// if the start node is first in the list
                        list.Remove(node);// remove the current node(which is the first node)
                    else// if the start node is not the first in the list
                        list.RemoveAfter(nodeBeforeStartNode);// remove the node after the node before the start node(i.e. the current node)
                    if (lastLeftNode == null)// if first time(no last node)
                        left.AddFirst(node);// add at the beginning
                    else// if we have last node
                        left.AddAfter(lastLeftNode, node);// add after it(faster than AddLast method)

                    lastLeftNode = node; // update last node
                }
                else
                {
                    var node = curNode;// save the current node
                    curNode = curNode.Next;// go to the next node
                    if (nodeBeforeStartNodeIsNull)// if the start node is first in the list
                        list.Remove(node);// remove the current node(which is the first node)
                    else// if the start node is not the first in the list
                        list.RemoveAfter(nodeBeforeStartNode);// remove the node after the node before the start node(i.e. the current node)
                    if (lastRightNode == null)// if first time(no last node)
                        right.AddFirst(node);// add at the beginning
                    else// if we have last node
                        right.AddAfter(lastRightNode, node);// add after it(faster than AddLast method)

                    lastRightNode = node; // update last node
                }
            }

            // Recursively sort both sublists
            // NOTE: node before startNode is null because in the left and right splits
            // we work with all nodes from the list.
            SequentialSplitMergeDescending(left, null, left.First, left.Last, comparer);
            SequentialSplitMergeDescending(right, null, right.First, right.Last, comparer);

            // Merge the two splits into the given list
            MergeDescending(list, left, right, nodeBeforeStartNode, comparer);

            // NOTE: only the last merging of two lists is done on the given list for sorting
        }

        /// <summary>
        /// Merging two lists into a given <see cref="SinglyLinkedList{T}"/>. Used for descending sort.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="SinglyLinkedList{T}"/>.</typeparam>
        /// <param name="mergeList">The <see cref="SinglyLinkedList{T}"/> for merging the left and right lists into.</param>
        /// <param name="left">The left <see cref="SinglyLinkedList{T}"/> containing some of the elements for sorting.</param>
        /// <param name="right">The right <see cref="SinglyLinkedList{T}"/> containing some of the elements for sorting.</param>
        /// <param name="nodeBeforeStartNode">The <see cref="SinglyLinkedListNode{T}"/> which is the node after the sorted sequence.</param>
        /// <param name="comparer">The <see cref="IComparable{T}"/> implementation used for comparing the elements.</param>
        /// <returns>Returns the given <see cref="SinglyLinkedList{T}"/> for the merged elements from the left <see cref="SinglyLinkedList{T}"/> and right <see cref="SinglyLinkedList{T}"/>.</returns>
        private static void MergeDescending<T>(SinglyLinkedList<T> mergeList, SinglyLinkedList<T> left, SinglyLinkedList<T> right, SinglyLinkedListNode<T> nodeBeforeStartNode, IComparer<T> comparer)
        {
            bool nodeBeforeStartNodeIsNull = nodeBeforeStartNode == null;
            SinglyLinkedListNode<T> lastAddedNode = nodeBeforeStartNode;

            // merging until one of the lists becomes empty
            while (left.Count > 0 && right.Count > 0)
            {
                if (comparer.Compare(left.First.Value, right.First.Value) >= 0)
                {
                    var node = left.First;
                    left.RemoveFirst();
                    if (nodeBeforeStartNodeIsNull)// if the start node is the first in the list
                    {
                        // Add after the last added node(if null add first)
                        if (lastAddedNode == null) mergeList.AddFirst(node);
                        else mergeList.AddAfter(lastAddedNode, node);
                    }
                    else// if the start node is not the first
                        mergeList.AddAfter(lastAddedNode, node);// add after the last added node

                    lastAddedNode = node;// Update last added node
                }
                else
                {
                    var node = right.First;
                    right.RemoveFirst();
                    if (nodeBeforeStartNodeIsNull)// if the start node is the first in the list
                    {
                        // Add after the last added node(if null add first)
                        if (lastAddedNode == null) mergeList.AddFirst(node);
                        else mergeList.AddAfter(lastAddedNode, node);
                    }
                    else// if the start node is not the first
                        mergeList.AddAfter(lastAddedNode, node);// add after the last added node

                    lastAddedNode = node;// Update last added node
                }
            }

            // add the remaining elements from the left or the right list
            while (left.Count > 0)
            {
                var node = left.First;
                left.RemoveFirst();
                if (nodeBeforeStartNodeIsNull)// if the start node is the first in the list
                {
                    // Add after the last added node(if null add first)
                    if (lastAddedNode == null) mergeList.AddFirst(node);
                    else mergeList.AddAfter(lastAddedNode, node);
                }
                else// if the start node is not the first
                    mergeList.AddAfter(lastAddedNode, node);// add after the last added node

                lastAddedNode = node;// Update last added node
            }

            while (right.Count > 0)
            {
                var node = right.First;
                right.RemoveFirst();
                if (nodeBeforeStartNodeIsNull)// if the start node is the first in the list
                {
                    // Add after the last added node(if null add first)
                    if (lastAddedNode == null) mergeList.AddFirst(node);
                    else mergeList.AddAfter(lastAddedNode, node);
                }
                else// if the start node is not the first
                    mergeList.AddAfter(lastAddedNode, node);// add after the last added node

                lastAddedNode = node;// Update last added node
            }
        }
    }
}
