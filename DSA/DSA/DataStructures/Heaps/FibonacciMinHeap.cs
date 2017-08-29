using DSA.DataStructures.Interfaces;
using System;
using System.Collections.Generic;

namespace DSA.DataStructures.Heaps
{
    /// <summary>
    /// Represents a fibonacci min heap.
    /// </summary>
    /// <typeparam name="T">The stored value type. T implements <see cref="IComparable{T}"/>.</typeparam>
    public class FibonacciMinHeap<T> : IMinHeap<T>
        where T : IComparable<T>
    {
        /// <summary>
        /// The min node of the <see cref="FibonacciMinHeap{T}"/>.
        /// </summary>
        internal FibonacciNode minNode;

        /// <summary>
        /// Gets the number of elements in the <see cref="FibonacciMinHeap{T}"/>.
        /// </summary>
        public int Count { get; internal set; }

        /// <summary>
        /// Determines whether the <see cref="FibonacciMinHeap{T}"/> is empty.
        /// </summary>
        public bool IsEmpty { get { return Count == 0; } }

        /// <summary>
        /// Creates a new instance of the <see cref="FibonacciMinHeap{T}"/> class.
        /// </summary>
        public FibonacciMinHeap() { }

        /// <summary>
        /// Merging two <see cref="FibonacciNode"/> lists into one returning the node with the smaller value.
        /// </summary>
        /// <param name="first">The first <see cref="FibonacciNode"/>.</param>
        /// <param name="second">The second <see cref="FibonacciNode"/>.</param>
        /// <returns>Returns the node with the smaller value.</returns>
        internal FibonacciNode MergeNodeLists(FibonacciNode first, FibonacciNode second)
        {
            // if one of the nodes is null
            if (first == null || second == null)
                return first ?? second;// we return the other or null if both are null

            // link the first node to the node after the second
            var temp = first.Next;
            first.Next = second.Next;
            first.Next.Previous = first;
            // and link second node to the node after the first
            second.Next = temp;
            second.Next.Previous = second;
            // Note: the nodes are linked like a circularly linked list
            // so a single node has its Previous and Next pointers pointing to itself

            return first.Value.CompareTo(second.Value) < 0 ? first : second;// return the node with the smaller value
        }

        /// <summary>
        /// Removes a <see cref="FibonacciNode"/> from the list of nodes, in which it belongs.
        /// </summary>
        /// <param name="node">The <see cref="FibonacciNode"/> to remove.</param>
        internal void RemoveNodeFromList(FibonacciNode node)
        {
            var prev = node.Previous;
            var next = node.Next;

            prev.Next = next;
            next.Previous = prev;

            node.Next = node;
            node.Previous = node;
        }

        /// <summary>
        /// Merge all same order trees until no more same order trees are present
        /// </summary>
        internal void MergeSameOrderTrees()
        {
            if (minNode == null) return;

            // Get root nodes
            var rootNodes = new List<FibonacciNode>();
            var rNode = minNode;
            do
            {
                rootNodes.Add(rNode);
                rNode = rNode.Next;
            } while (rNode != minNode);

            // List of the nodes saving them to be used for merging. Indexed by their degree
            // and using them for merging when another node of the same degree is present
            var degreeList = new List<FibonacciNode>();

            // Iterate over root nodes and merge trees with of same order
            foreach (var node in rootNodes)
            {
                var current = node;

                // Ensure degreeList have enough items
                while (degreeList.Count <= current.Degree + 1)
                    degreeList.Add(null);

                // while having a node with same degree as the current we merge them
                while (degreeList[current.Degree] != null)
                {
                    FibonacciNode bigger;
                    FibonacciNode smaller;

                    // check if the current or the node in the list has a bigger value
                    if (current.Value.CompareTo(degreeList[current.Degree].Value) > 0)
                    {
                        bigger = current;
                        smaller = degreeList[current.Degree];
                    }
                    else
                    {
                        bigger = degreeList[current.Degree];
                        smaller = current;
                    }

                    // Merging the trees of the same order
                    RemoveNodeFromList(bigger);
                    smaller.Child = MergeNodeLists(bigger, smaller.Child);
                    bigger.Parent = smaller;

                    // Removing the node from the list because it is now merged
                    degreeList[current.Degree] = null;

                    // The current node is now the smaller one and its degree is updated
                    current = smaller;
                    current.Degree++;
                }

                // Ensure degreeList have enough items
                while (degreeList.Count <= current.Degree + 1)
                    degreeList.Add(null);

                // add the current node to the list. Saving it if we have another node
                // of the same order or when reconstuctiong the root list
                degreeList[current.Degree] = current;
            }

            // Reconstructing the root list
            minNode = null;
            for (int i = 0; i < degreeList.Count; i++)
            {
                if (degreeList[i] != null)
                {
                    // Remove previous and next node pointers before merging
                    degreeList[i].Previous = degreeList[i];
                    degreeList[i].Next = degreeList[i];
                    minNode = MergeNodeLists(minNode, degreeList[i]);
                }
            }
        }

        /// <summary>
        /// Heapifies the given <see cref="IEnumerable{T}"/> collection. Overrides the current <see cref="FibonacciMinHeap{T}"/>. If the given collection is empty the heap is not cleared.
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
        /// Merges the elements of the <see cref="FibonacciMinHeap{T}"/> with the other given <see cref="FibonacciMinHeap{T}"/>. The other <see cref="FibonacciMinHeap{T}"/> is cleared after the merging.
        /// </summary>
        /// <param name="otherMinHeap">The other <see cref="FibonacciMinHeap{T}"/> used for merging.</param>
        public void Merge(FibonacciMinHeap<T> otherMinHeap)
        {
            minNode = MergeNodeLists(minNode, otherMinHeap.minNode);
            Count += otherMinHeap.Count;

            otherMinHeap.Clear();
        }

        /// <summary>
        /// Adds an element to the <see cref="FibonacciMinHeap{T}"/>.
        /// </summary>
        /// <param name="value">The value to add.</param>
        public void Add(T value)
        {
            var newNode = new FibonacciNode(value);
            minNode = MergeNodeLists(minNode, newNode);
            Count++;
        }

        /// <summary>
        /// Gets the min element of the <see cref="FibonacciMinHeap{T}"/>.
        /// </summary>
        /// <returns>Returns the min element of the <see cref="FibonacciMinHeap{T}"/>.</returns>
        public T PeekMin()
        {
            if (IsEmpty) throw new InvalidOperationException("Heap is empty!");

            return minNode.Value;
        }

        /// <summary>
        /// Removes and returns the min element of the <see cref="FibonacciMinHeap{T}"/>.
        /// </summary>
        /// <returns>Returns the min element which was removed from the <see cref="FibonacciMinHeap{T}"/>.</returns>
        public T PopMin()
        {
            if (IsEmpty) throw new InvalidOperationException("Heap is empty!");

            T minValue = minNode.Value;
            var minCopy = minNode;

            // Remove minNode's children parent pointers
            if (minCopy.Child != null)
            {
                var child = minCopy.Child;
                do
                {
                    child.Parent = null;
                    child = child.Next;
                } while (child != minCopy.Child);
            }

            // Get next tree root
            FibonacciNode nextNode = minCopy.Next == minCopy ? null : minCopy.Next;

            // Remove node from the list of tree roots
            RemoveNodeFromList(minCopy);
            Count--;

            // Merging minNode's children with the root list
            minNode = MergeNodeLists(nextNode, minCopy.Child);

            if (nextNode != null)
            {
                minNode = nextNode;
                MergeSameOrderTrees();
            }

            return minValue;
        }

        /// <summary>
        /// Replaces the min element with the given value and rebalancing the <see cref="FibonacciMinHeap{T}"/>.
        /// </summary>
        /// <param name="value">The value to replace the min element of the <see cref="FibonacciMinHeap{T}"/>.</param>
        public void ReplaceMin(T value)
        {
            if (IsEmpty) throw new InvalidOperationException("Heap is empty!");

            PopMin();
            Add(value);
        }

        /// <summary>
        /// Removes the min element from the <see cref="FibonacciMinHeap{T}"/>.
        /// </summary>
        public void RemoveMin()
        {
            if (IsEmpty) throw new InvalidOperationException("Heap is empty!");

            PopMin();
        }

        /// <summary>
        /// Removes all elements from the <see cref="FibonacciMinHeap{T}"/>.
        /// </summary>
        public void Clear()
        {
            minNode = null;
            Count = 0;
        }

        /// <summary>
        /// Copies the elements of the <see cref="FibonacciMinHeap{T}"/> to a new array.
        /// </summary>
        /// <returns>An array containing copies of the elements of the <see cref="FibonacciMinHeap{T}"/>.</returns>
        public T[] ToArray()
        {
            // BFS iteration over heap
            var values = new List<T>(Count);

            // if no nodes are present return empty array
            if (minNode == null) return values.ToArray();

            var current = minNode;

            var queue = new Queue<FibonacciNode>(Count);

            // Adding roots of trees to the queue
            var node = current;
            do
            {
                queue.Enqueue(node);
                node = node.Next;
            }
            while (node != current);

            // while there are nodes in the queue
            while (queue.Count != 0)
            {
                // Add value to list
                current = queue.Dequeue();
                values.Add(current.Value);

                // Add children to the queue
                if (current.Child != null)
                {
                    var child = current.Child;
                    do
                    {
                        queue.Enqueue(child);
                        child = child.Next;
                    }
                    while (child != current.Child);
                }

            }

            return values.ToArray();
        }

        /// <summary>
        /// Returns a new <see cref="FibonacciMaxHeap{T}"/> containing the elements of the <see cref="FibonacciMinHeap{T}"/>.
        /// </summary>
        /// <returns>Returns a new <see cref="FibonacciMaxHeap{T}"/> containing the elements of the <see cref="FibonacciMinHeap{T}"/>.</returns>
        public FibonacciMaxHeap<T> ToMaxHeap()
        {
            var maxHeap = new FibonacciMaxHeap<T>();
            maxHeap.Heapify(ToArray());
            return maxHeap;
        }

        IMaxHeap<T> IMinHeap<T>.ToMaxHeap()
        {
            return ToMaxHeap();
        }

        /// <summary>
        /// Represents a node in the <see cref="FibonacciMinHeap{T}"/>.
        /// </summary>
        internal class FibonacciNode
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
            internal FibonacciNode Parent { get; set; }

            /// <summary>
            /// Gets the child node.
            /// </summary>
            internal FibonacciNode Child { get; set; }

            /// <summary>
            /// Gets the previous node.
            /// </summary>
            internal FibonacciNode Previous { get; set; }

            /// <summary>
            /// Gets the next node.
            /// </summary>
            internal FibonacciNode Next { get; set; }

            /// <summary>
            /// Creates a new instance of the <see cref="FibonacciNode"/> class, containing the specified value.
            /// </summary>
            /// <param name="value">The value to contain in the <see cref="FibonacciNode"/>.</param>
            public FibonacciNode(T value)
            {
                Value = value;
                Previous = this;
                Next = this;
            }
        }
    }
}
