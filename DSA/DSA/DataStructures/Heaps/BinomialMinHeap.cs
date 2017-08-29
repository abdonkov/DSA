using DSA.DataStructures.Interfaces;
using DSA.DataStructures.Lists;
using System;
using System.Collections.Generic;

namespace DSA.DataStructures.Heaps
{
    /// <summary>
    /// Represents a binomial min heap.
    /// </summary>
    /// <typeparam name="T">The stored value type. T implements <see cref="IComparable{T}"/>.</typeparam>
    public class BinomialMinHeap<T> : IMinHeap<T>
        where T : IComparable<T>
    {
        /// <summary>
        /// The head node of the <see cref="BinomialMinHeap{T}"/>.
        /// </summary>
        internal BinomialNode head;

        /// <summary>
        /// Gets the number of elements in the <see cref="BinomialMinHeap{T}"/>.
        /// </summary>
        public int Count { get; internal set; }

        /// <summary>
        /// Determines whether the <see cref="BinomialMinHeap{T}"/> is empty.
        /// </summary>
        public bool IsEmpty { get { return Count == 0; } }

        /// <summary>
        /// Creates a new instance of the <see cref="BinomialMinHeap{T}"/> class.
        /// </summary>
        public BinomialMinHeap() { }

        /// <summary>
        /// Creates a new instance of the <see cref="BinomialMinHeap{T}"/> class, with the specified node as head.
        /// </summary>
        /// <param name="head">The head node.</param>
        internal BinomialMinHeap(BinomialNode head)
        {
            this.head = head;
            Count = 1;
        }

        /// <summary>
        /// Heapifies the given <see cref="IEnumerable{T}"/> collection. Overrides the current <see cref="BinomialMinHeap{T}"/>. If the given collection is empty the heap is not cleared.
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
            while(child != null)
            {
                var next = child.Sibling;
                child.Sibling = newHead;
                child.Parent = null;
                newHead = child;
                child = next;
            }

            var newHeap = new BinomialMinHeap<T>(newHead);

            Merge(newHeap);
        }

        /// <summary>
        /// Merge two binomial trees to a new tree of higher order, given the roots of the trees.
        /// </summary>
        /// <param name="smallerNode">The <see cref="BinomialNode"/> with the smaller value.</param>
        /// <param name="biggerNode">The <see cref="BinomialNode"/> with the bigger value.</param>
        internal void MergeBinomialTrees(BinomialNode smallerNode, BinomialNode biggerNode)
        {
            biggerNode.Parent = smallerNode;
            biggerNode.Sibling = smallerNode.Child;
            smallerNode.Child = biggerNode;
            smallerNode.Degree++;
        }

        /// <summary>
        /// Merges the elements of the <see cref="BinomialMinHeap{T}"/> with the other given <see cref="BinomialMinHeap{T}"/>. The other <see cref="BinomialMinHeap{T}"/> is cleared after the merging.
        /// </summary>
        /// <param name="otherMinHeap">The other <see cref="BinomialMinHeap{T}"/> used for merging.</param>
        public void Merge(BinomialMinHeap<T> otherMinHeap)
        {
            // if the given heap has no elements
            if (otherMinHeap.head == null)
                return;

            // if this heap is empty
            if (head == null)
            {
                // copy the head of the other heap and clear it
                head = otherMinHeap.head;
                Count = otherMinHeap.Count;
                otherMinHeap.Clear();
                return;
            }

            // if both heaps have elements

            BinomialNode newHead;

            var firstHeapNextNode = head;
            var secondHeapNextNode = otherMinHeap.head;

            // Set the heap head of lower order as the new head
            if (head.Degree <= otherMinHeap.head.Degree)
            {
                newHead = head;
                firstHeapNextNode = firstHeapNextNode.Sibling;
            }
            else
            {
                newHead = otherMinHeap.head;
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

            while(next != null)
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
                    // if the current node is smaller than or equal to the next
                    if (current.Value.CompareTo(next.Value) <= 0)
                    {
                        current.Sibling = next.Sibling;
                        MergeBinomialTrees(smallerNode: current, biggerNode: next);
                    }
                    else// if the current is bigger that the next
                    {
                        // we set the previous node link to this one
                        if (previous == null)
                            newHead = next;
                        else
                            previous.Sibling = next;

                        MergeBinomialTrees(smallerNode: next, biggerNode: current);
                        current = next;
                    }
                }
                // Here current has became the next node,
                // so the new next node is the sibling of the current
                next = current.Sibling;
            }

            // At last we update the head and the count
            head = newHead;
            Count += otherMinHeap.Count;
            // We clear the other heap
            otherMinHeap.Clear();
        }

        /// <summary>
        /// Adds an element to the <see cref="BinomialMinHeap{T}"/>.
        /// </summary>
        /// <param name="value">The value to add.</param>
        public void Add(T value)
        {
            var newNode = new BinomialNode(value);
            var tempHeap = new BinomialMinHeap<T>(newNode);
            Merge(tempHeap);
        }

        /// <summary>
        /// Gets the min element of the <see cref="BinomialMinHeap{T}"/>.
        /// </summary>
        /// <returns>Returns the min element of the <see cref="BinomialMinHeap{T}"/>.</returns>
        public T PeekMin()
        {
            if (IsEmpty) throw new InvalidOperationException("Heap is empty!");

            var min = head;
            var next = min.Sibling;
            while (next != null)
            {
                if (next.Value.CompareTo(min.Value) < 0)
                {
                    min = next;
                }

                next = next.Sibling;
            }

            return min.Value;
        }

        /// <summary>
        /// Removes and returns the min element of the <see cref="BinomialMinHeap{T}"/>.
        /// </summary>
        /// <returns>Returns the min element which was removed from the <see cref="BinomialMinHeap{T}"/>.</returns>
        public T PopMin()
        {
            if (IsEmpty) throw new InvalidOperationException("Heap is empty!");

            BinomialNode min = head;// min node
            BinomialNode minPrev = null;// node before the min node
            BinomialNode current = min.Sibling;// current node in the traversal
            BinomialNode last = min;// the last traversed node

            while(current != null)
            {
                if (current.Value.CompareTo(min.Value) <= 0)
                {
                    min = current;
                    minPrev = last;
                }

                last = current;
                current = current.Sibling;
            }

            // Saving count because when removing the binomial tree root
            // the current heap is merged with a new heap with unknown
            // number of elements
            var oldCount = Count;

            RemoveBinomialTreeRoot(min, minPrev);

            Count = oldCount - 1;

            return min.Value;
        }

        /// <summary>
        /// Replaces the min element with the given value and rebalancing the <see cref="BinomialMinHeap{T}"/>.
        /// </summary>
        /// <param name="value">The value to replace the min element of the <see cref="BinomialMinHeap{T}"/>.</param>
        public void ReplaceMin(T value)
        {
            if (IsEmpty) throw new InvalidOperationException("Heap is empty!");

            RemoveMin();
            Add(value);
        }

        /// <summary>
        /// Removes the min element from the <see cref="BinomialMinHeap{T}"/>.
        /// </summary>
        public void RemoveMin()
        {
            if (IsEmpty) throw new InvalidOperationException("Heap is empty!");

            PopMin();
        }

        /// <summary>
        /// Removes all elements from the <see cref="BinomialMinHeap{T}"/>.
        /// </summary>
        public void Clear()
        {
            head = null;
            Count = 0;
        }

        /// <summary>
        /// Copies the elements of the <see cref="BinomialMinHeap{T}"/> to a new array.
        /// </summary>
        /// <returns>An array containing copies of the elements of the <see cref="BinomialMinHeap{T}"/>.</returns>
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

            while(queue.Count != 0)
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
        /// Returns a new <see cref="BinomialMaxHeap{T}"/> containing the elements of the <see cref="BinomialMinHeap{T}"/>.
        /// </summary>
        /// <returns>Returns a new <see cref="BinomialMaxHeap{T}"/> containing the elements of the <see cref="BinomialMinHeap{T}"/>.</returns>
        public BinomialMaxHeap<T> ToMaxHeap()
        {
            var maxHeap = new BinomialMaxHeap<T>();
            maxHeap.Heapify(ToArray());
            return maxHeap;
        }

        IMaxHeap<T> IMinHeap<T>.ToMaxHeap()
        {
            return ToMaxHeap();
        }

        /// <summary>
        /// Represents a node in the <see cref="BinomialMinHeap{T}"/>.
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
