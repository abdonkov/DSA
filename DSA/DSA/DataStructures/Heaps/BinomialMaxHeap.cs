using DSA.DataStructures.Interfaces;
using DSA.DataStructures.Lists;
using System;
using System.Collections.Generic;

namespace DSA.DataStructures.Heaps
{
    /// <summary>
    /// Represents a binomial max heap.
    /// </summary>
    /// <typeparam name="T">The stored value type. T implements <see cref="IComparable{T}"/>.</typeparam>
    public class BinomialMaxHeap<T> : IMaxHeap<T>
        where T : IComparable<T>
    {
        /// <summary>
        /// The head node of the <see cref="BinomialMaxHeap{T}"/>.
        /// </summary>
        internal BinomialNode head;

        /// <summary>
        /// Gets the number of elements in the <see cref="BinomialMaxHeap{T}"/>.
        /// </summary>
        public int Count { get; internal set; }

        /// <summary>
        /// Determines whether the <see cref="BinomialMaxHeap{T}"/> is empty.
        /// </summary>
        public bool IsEmpty { get { return Count == 0; } }

        /// <summary>
        /// Creates a new instance of the <see cref="BinomialMaxHeap{T}"/> class.
        /// </summary>
        public BinomialMaxHeap() { }

        /// <summary>
        /// Creates a new instance of the <see cref="BinomialMaxHeap{T}"/> class, with the specified node as head.
        /// </summary>
        /// <param name="head">The head node.</param>
        internal BinomialMaxHeap(BinomialNode head)
        {
            this.head = head;
            Count = 1;
        }

        /// <summary>
        /// Heapifies the given <see cref="IEnumerable{T}"/> collection. Overrides the current <see cref="BinomialMaxHeap{T}"/>. If the given collection is empty the heap is not cleared.
        /// </summary>
        /// <param name="collection">The collection of elements to heapify.</param>
        public void Heapify(IEnumerable<T> collection)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));

            bool haveToClearHeap = true;

            foreach (var item in collection)
            {
                if (haveToClearHeap)
                {
                    Clear();
                    haveToClearHeap = false;
                }

                Add(item);
            }
        }

        /// <summary>
        /// Removes the tree root of a binomial tree given the tree root and the <see cref="BinomialNode"/> before it.
        /// </summary>
        /// <param name="treeRoot">The root node of the binomial tree.</param>
        /// <param name="previous">The <see cref="BinomialNode"/> before the tree root.</param>
        internal void RemoveBinomialTreeRoot(BinomialNode treeRoot, BinomialNode previous)
        {
            if (treeRoot == head)
            {
                head = head.Sibling;
            }
            else
            {
                if (previous != null) previous.Sibling = treeRoot.Sibling;
            }

            // Reversing order of root children and creating a new heap
            BinomialNode newHead = null;
            var child = treeRoot.Child;
            while (child != null)
            {
                var next = child.Sibling;
                child.Sibling = newHead;
                child.Parent = null;
                newHead = child;
                child = next;
            }

            var newHeap = new BinomialMaxHeap<T>(newHead);

            Merge(newHeap);
        }

        /// <summary>
        /// Merge two binomial trees to a new tree of higher order, given the roots of the trees.
        /// </summary>
        /// <param name="biggerNode">The <see cref="BinomialNode"/> with the bigger value.</param>
        /// <param name="smallerNode">The <see cref="BinomialNode"/> with the smaller value.</param>
        internal void MergeBinomialTrees(BinomialNode biggerNode, BinomialNode smallerNode)
        {
            smallerNode.Parent = biggerNode;
            smallerNode.Sibling = biggerNode.Child;
            biggerNode.Child = smallerNode;
            biggerNode.Degree++;
        }

        /// <summary>
        /// Merges the elements of the <see cref="BinomialMaxHeap{T}"/> with the other given <see cref="BinomialMaxHeap{T}"/>. The other <see cref="BinomialMaxHeap{T}"/> is cleared after the merging.
        /// </summary>
        /// <param name="otherMaxHeap">The other <see cref="BinomialMaxHeap{T}"/> used for merging.</param>
        public void Merge(BinomialMaxHeap<T> otherMaxHeap)
        {
            // if the given heap has no elements
            if (otherMaxHeap.head == null)
                return;

            // if this heap is empty
            if (head == null)
            {
                // copy the head of the other heap and clear it
                head = otherMaxHeap.head;
                Count = otherMaxHeap.Count;
                otherMaxHeap.Clear();
                return;
            }

            // if both heaps have elements

            BinomialNode newHead;

            var firstHeapNextNode = head;
            var secondHeapNextNode = otherMaxHeap.head;

            // Set the heap head of lower order as the new head
            if (head.Degree <= otherMaxHeap.head.Degree)
            {
                newHead = head;
                firstHeapNextNode = firstHeapNextNode.Sibling;
            }
            else
            {
                newHead = otherMaxHeap.head;
                secondHeapNextNode = secondHeapNextNode.Sibling;
            }

            var curNode = newHead;
            // Iterating over the roots of the binomial trees of the heaps and
            // sorting them by order(binomial tree order)
            while (firstHeapNextNode != null && secondHeapNextNode != null)
            {
                if (firstHeapNextNode.Degree <= secondHeapNextNode.Degree)
                {
                    curNode.Sibling = firstHeapNextNode;
                    firstHeapNextNode = firstHeapNextNode.Sibling;
                }
                else
                {
                    curNode.Sibling = secondHeapNextNode;
                    secondHeapNextNode = secondHeapNextNode.Sibling;
                }

                curNode = curNode.Sibling;
            }

            if (firstHeapNextNode != null)
            {
                curNode.Sibling = firstHeapNextNode;
            }
            else
            {
                curNode.Sibling = secondHeapNextNode;
            }

            head = newHead;

            // After the new head links to the binomial tree roots sorted by order
            // we have to leave at most one tree of each order. We can do this by merging every
            // two trees of order k to a new tree of order k + 1

            BinomialNode previous = null;
            BinomialNode current = newHead;
            BinomialNode next = newHead.Sibling;

            while (next != null)
            {
                // if the order of the trees is different
                // or we have 3 trees of the same order we continue onwards.
                // Having 3 trees of order k. We can leave one with order k and merge
                // the next 2 to create a tree of order k + 1
                // Note: It is not possible to have more than 3 trees of order k,
                // because a heap has at most one tree of each order. So merging two heaps
                // we get maximum of two trees with the same order and having 2 trees
                // of order k and 2 trees of order k + 1. We merge the 2 trees of order k
                // to a tree of order k + 1 and now we have 3 trees of order k + 1(leaving one
                // and merging the other 2). So having 4 trees of same order is not possible
                if (current.Degree != next.Degree
                    || ((next.Sibling?.Degree ?? -1) == current.Degree))
                {
                    previous = current;
                    current = next;
                }
                else// if we need to merge 2 trees of same order
                {
                    // if the current node is bigger than or equal to the next
                    if (current.Value.CompareTo(next.Value) >= 0)
                    {
                        current.Sibling = next.Sibling;
                        MergeBinomialTrees(biggerNode: current, smallerNode: next);
                    }
                    else// if the current is smaller that the next
                    {
                        // we set the previous node link to this one
                        if (previous == null)
                            newHead = next;
                        else
                            previous.Sibling = next;

                        MergeBinomialTrees(biggerNode: next, smallerNode: current);
                        current = next;
                    }
                }
                // Here current has became the next node,
                // so the new next node is the sibling of the current
                next = current.Sibling;
            }

            // At last we update the head and the count
            head = newHead;
            Count += otherMaxHeap.Count;
            // We clear the other heap
            otherMaxHeap.Clear();
        }

        /// <summary>
        /// Adds an element to the <see cref="BinomialMaxHeap{T}"/>.
        /// </summary>
        /// <param name="value">The value to add.</param>
        public void Add(T value)
        {
            var newNode = new BinomialNode(value);
            var tempHeap = new BinomialMaxHeap<T>(newNode);
            Merge(tempHeap);
        }

        /// <summary>
        /// Gets the max element of the <see cref="BinomialMaxHeap{T}"/>.
        /// </summary>
        /// <returns>Returns the max element of the <see cref="BinomialMaxHeap{T}"/>.</returns>
        public T PeekMax()
        {
            if (IsEmpty) throw new InvalidOperationException("Heap is empty!");

            var max = head;
            var next = max.Sibling;
            while (next != null)
            {
                if (next.Value.CompareTo(max.Value) > 0)
                {
                    max = next;
                }

                next = next.Sibling;
            }

            return max.Value;
        }

        /// <summary>
        /// Removes and returns the max element of the <see cref="BinomialMaxHeap{T}"/>.
        /// </summary>
        /// <returns>Returns the max element which was removed from the <see cref="BinomialMaxHeap{T}"/>.</returns>
        public T PopMax()
        {
            if (IsEmpty) throw new InvalidOperationException("Heap is empty!");

            BinomialNode max = head;// max node
            BinomialNode maxPrev = null;// node before the max node
            BinomialNode current = max.Sibling;// current node in the traversal
            BinomialNode last = max;// the last traversed node

            while (current != null)
            {
                if (current.Value.CompareTo(max.Value) >= 0)
                {
                    max = current;
                    maxPrev = last;
                }

                last = current;
                current = current.Sibling;
            }

            // Saving count because when removing the binomial tree root
            // the current heap is merged with a new heap with unknown
            // number of elements
            var oldCount = Count;

            RemoveBinomialTreeRoot(max, maxPrev);

            Count = oldCount - 1;

            return max.Value;
        }

        /// <summary>
        /// Replaces the max element with the given value and rebalancing the <see cref="BinomialMaxHeap{T}"/>.
        /// </summary>
        /// <param name="value">The value to replace the max element of the <see cref="BinomialMaxHeap{T}"/>.</param>
        public void ReplaceMax(T value)
        {
            if (IsEmpty) throw new InvalidOperationException("Heap is empty!");

            RemoveMax();
            Add(value);
        }

        /// <summary>
        /// Removes the max element from the <see cref="BinomialMaxHeap{T}"/>.
        /// </summary>
        public void RemoveMax()
        {
            if (IsEmpty) throw new InvalidOperationException("Heap is empty!");

            PopMax();
        }

        /// <summary>
        /// Removes all elements from the <see cref="BinomialMaxHeap{T}"/>.
        /// </summary>
        public void Clear()
        {
            head = null;
            Count = 0;
        }

        /// <summary>
        /// Copies the elements of the <see cref="BinomialMaxHeap{T}"/> to a new array.
        /// </summary>
        /// <returns>An array containing copies of the elements of the <see cref="BinomialMaxHeap{T}"/>.</returns>
        public T[] ToArray()
        {
            // BFS iteration over the binomial heap
            var list = new ArrayList<T>(Count);

            var current = head;

            var queue = new Queue<BinomialNode>(Count);

            while (current != null)
            {
                queue.Enqueue(current);
                current = current.Sibling;
            }

            while (queue.Count != 0)
            {
                current = queue.Dequeue();
                list.Add(current.Value);
                var child = current.Child;
                while (child != null)
                {
                    queue.Enqueue(child);
                    child = child.Sibling;
                }
            }

            return list.ToArray();
        }

        /// <summary>
        /// Returns a new <see cref="BinomialMinHeap{T}"/> containing the elements of the <see cref="BinomialMaxHeap{T}"/>.
        /// </summary>
        /// <returns>Returns a new <see cref="BinomialMinHeap{T}"/> containing the elements of the <see cref="BinomialMaxHeap{T}"/>.</returns>
        public BinomialMinHeap<T> ToMinHeap()
        {
            var minHeap = new BinomialMinHeap<T>();
            minHeap.Heapify(ToArray());
            return minHeap;
        }

        IMinHeap<T> IMaxHeap<T>.ToMinHeap()
        {
            return ToMinHeap();
        }

        /// <summary>
        /// Represents a node in the <see cref="BinomialMaxHeap{T}"/>.
        /// </summary>
        internal class BinomialNode
        {
            /// <summary>
            /// Gets the value of the node.
            /// </summary>
            internal T Value { get; set; }

            /// <summary>
            /// Gets the degree of the node.
            /// </summary>
            internal int Degree { get; set; }

            /// <summary>
            /// Gets the parent node.
            /// </summary>
            internal BinomialNode Parent { get; set; }

            /// <summary>
            /// Gets the child node.
            /// </summary>
            internal BinomialNode Child { get; set; }

            /// <summary>
            /// Gets the sibling node.
            /// </summary>
            internal BinomialNode Sibling { get; set; }

            /// <summary>
            /// Creates a new instance of the <see cref="BinomialNode"/> class, containing the specified value.
            /// </summary>
            /// <param name="value">The value to contain in the <see cref="BinomialNode"/>.</param>
            internal BinomialNode(T value)
            {
                Value = value;
            }
        }
    }
}
