using DSA.DataStructures.Interfaces;
using DSA.DataStructures.Lists;
using System;
using System.Collections.Generic;

namespace DSA.DataStructures.Heaps
{
    /// <summary>
    /// Represents a binary max heap backed by an <see cref="ArrayList{T}"/>.
    /// </summary>
    /// <typeparam name="T">The stored value type. T implements <see cref="IComparable{T}"/>.</typeparam>
    public class BinaryMaxHeap<T> : IMaxHeap<T>
        where T : IComparable<T>
    {
        /// <summary>
        /// The backing <see cref="ArrayList{T}"/> containing the elements of the <see cref="BinaryMaxHeap{T}"/>.
        /// </summary>
        internal ArrayList<T> array;

        /// <summary>
        /// The comparer of the elements in the <see cref="BinaryMaxHeap{T}"/>.
        /// </summary>
        internal Comparer<T> comparer;

        /// <summary>
        /// Gets the number of elements in the <see cref="BinaryMaxHeap{T}"/>.
        /// </summary>
        public int Count { get; internal set; }

        /// <summary>
        /// Determines whether the <see cref="BinaryMaxHeap{T}"/> is empty.
        /// </summary>
        public bool IsEmpty { get { return Count == 0; } }

        /// <summary>
        /// Creates a new instance of the <see cref="BinaryMaxHeap{T}"/> class.
        /// </summary>
        public BinaryMaxHeap() : this(0, null) { }

        /// <summary>
        /// Creates a new instance of the <see cref="BinaryMaxHeap{T}"/> class with the given capacity for the backing <see cref="ArrayList{T}"/>.
        /// </summary>
        /// <param name="capacity">The capacity for the underlying <see cref="ArrayList{T}"/>.</param>
        public BinaryMaxHeap(int capacity) : this(capacity, null) { }

        /// <summary>
        /// Creates a new instance of the <see cref="BinaryMaxHeap{T}"/> class with the specified comparer.
        /// </summary>
        /// <param name="comparer">The comparer of the elements in the <see cref="BinaryMaxHeap{T}"/>.</param>
        public BinaryMaxHeap(Comparer<T> comparer) : this(0, comparer) { }

        /// <summary>
        /// Creates a new instance of the <see cref="BinaryMaxHeap{T}"/> class with the specified comparer and the given capacity for the backing <see cref="ArrayList{T}"/>.
        /// </summary>
        /// <param name="capacity">The capacity for the underlying <see cref="ArrayList{T}"/>.</param>
        /// <param name="comparer">The comparer of the elements in the <see cref="BinaryMaxHeap{T}"/>.</param>
        public BinaryMaxHeap(int capacity, Comparer<T> comparer)
        {
            array = new ArrayList<T>(capacity);
            this.comparer = comparer ?? Comparer<T>.Default;
        }

        /// <summary>
        /// Performing heapify-up operation on the given node. Used to rebalance the heap after adding a new item.
        /// </summary>
        /// <param name="nodeIndex">The index of the node to heapify-up.</param>
        private void NodeHeapifyUp(int nodeIndex)
        {
            while (nodeIndex > 0)
            {
                // Getting the parent node
                int parentIndex = (nodeIndex - 1) / 2;

                if (comparer.Compare(array[parentIndex], array[nodeIndex]) < 0)
                {// if the parent is smaller that the current node
                    // we have to swap them
                    var temp = array[parentIndex];
                    array[parentIndex] = array[nodeIndex];
                    array[nodeIndex] = temp;
                    // then we continue comparing the node with it's new parent
                    nodeIndex = parentIndex;
                }
                else// if the parent is bigger than or equal to the current node
                    break;// no more adjustments are needed
            }
        }

        /// <summary>
        /// Performing heapify-down operation on the given node. Used to rebalance the heap after removing or replacing the root.
        /// </summary>
        /// <param name="nodeIndex"></param>
        private void NodeHeapifyDown(int nodeIndex)
        {
            var leftChildIndex = 2 * nodeIndex + 1;
            var rigthChildIndex = 2 * nodeIndex + 2;

            // while the current node has children
            while (leftChildIndex < array.Count)
            {
                // saving the index of the biggest node of the three(current node and its children)
                var biggestNodeIndex = nodeIndex;

                // compare right child with the biggest node(current node for now)
                if (rigthChildIndex < array.Count)// needed checking if there is a right node also
                    if (comparer.Compare(array[rigthChildIndex], array[biggestNodeIndex]) > 0)
                        biggestNodeIndex = rigthChildIndex;

                // compare left child with the biggest node(current node or right child)
                if (comparer.Compare(array[leftChildIndex], array[biggestNodeIndex]) > 0)
                    biggestNodeIndex = leftChildIndex;

                // if the current node is the biggest
                if (biggestNodeIndex == nodeIndex)
                    break;// no more adjustments are needed

                // else if one of the children is bigger that the current node we swap them
                var temp = array[nodeIndex];
                array[nodeIndex] = array[biggestNodeIndex];
                array[biggestNodeIndex] = temp;

                // continue downwards with the comparison
                nodeIndex = biggestNodeIndex;
                leftChildIndex = 2 * nodeIndex + 1;
                rigthChildIndex = 2 * nodeIndex + 2;
            }
        }

        /// <summary>
        /// Heapifies the given <see cref="IEnumerable{T}"/> collection. Overrides the current <see cref="BinaryMaxHeap{T}"/>.
        /// </summary>
        /// <param name="collection">The collection of elements to heapify.</param>
        public void Heapify(IEnumerable<T> collection)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));

            var newArrayList = new ArrayList<T>(collection);

            if (newArrayList.Count > 0)
            {
                array = newArrayList;

                // Building the binary heap
                int lastNodeWithChildrenIndex = (array.Count - 2) / 2;
                for (int i = lastNodeWithChildrenIndex; i >= 0; i--)
                {
                    NodeHeapifyDown(i);
                }

                Count = array.Count;
            }
        }

        /// <summary>
        /// Merges the elements of the <see cref="BinaryMaxHeap{T}"/> with the other given <see cref="BinaryMaxHeap{T}"/>.
        /// </summary>
        /// <param name="otherMaxHeap">The other <see cref="BinaryMaxHeap{T}"/> used for merging.</param>
        public void Merge(BinaryMaxHeap<T> otherMaxHeap)
        {
            for (int i = 0; i < otherMaxHeap.Count; i++)
            {
                Add(otherMaxHeap.array[i]);
            }
        }

        /// <summary>
        /// Adds an element to the <see cref="BinaryMaxHeap{T}"/>.
        /// </summary>
        /// <param name="value">The value to add.</param>
        public void Add(T value)
        {
            if (IsEmpty)
                array.Add(value);
            else
            {
                array.Add(value);
                NodeHeapifyUp(array.Count - 1);
            }

            Count++;
        }

        /// <summary>
        /// Gets the max element of the <see cref="BinaryMaxHeap{T}"/>.
        /// </summary>
        /// <returns>Returns the max element of the <see cref="BinaryMaxHeap{T}"/>.</returns>
        public T PeekMax()
        {
            if (IsEmpty) throw new InvalidOperationException("Heap is empty!");

            return array[0];
        }

        /// <summary>
        /// Removes and returns the max element of the <see cref="BinaryMaxHeap{T}"/>.
        /// </summary>
        /// <returns>Returns the max element which was removed from the <see cref="BinaryMaxHeap{T}"/>.</returns>
        public T PopMax()
        {
            var max = PeekMax();
            RemoveMax();
            return max;
        }

        /// <summary>
        /// Replaces the max element with the given value and rebalancing the <see cref="BinaryMaxHeap{T}"/>.
        /// </summary>
        /// <param name="value">The value to replace the max element of the <see cref="BinaryMaxHeap{T}"/>.</param>
        public void ReplaceMax(T value)
        {
            if (IsEmpty) if (IsEmpty) throw new InvalidOperationException("Heap is empty!");

            array[0] = value;
            NodeHeapifyDown(0);
        }

        /// <summary>
        /// Removes the max element from the <see cref="BinaryMaxHeap{T}"/>.
        /// </summary>
        public void RemoveMax()
        {
            if (IsEmpty) throw new InvalidOperationException("Heap is empty!");

            array[0] = array[Count - 1];
            array.RemoveAt(Count - 1);
            Count--;
            NodeHeapifyDown(0);
        }

        /// <summary>
        /// Removes all elements from the <see cref="BinaryMaxHeap{T}"/>.
        /// </summary>
        public void Clear()
        {
            array = new ArrayList<T>();
            Count = 0;
        }

        /// <summary>
        /// Copies the elements of the <see cref="BinaryMaxHeap{T}"/> to a new array.
        /// </summary>
        /// <returns>An array containing copies of the elements of the <see cref="BinaryMaxHeap{T}"/>.</returns>
        public T[] ToArray()
        {
            var newArray = new T[Count];
            for (int i = 0; i < Count; i++)
            {
                newArray[i] = array[i];
            }
            return newArray;
        }

        /// <summary>
        /// Returns a new <see cref="BinaryMinHeap{T}"/> containing the elements of the <see cref="BinaryMaxHeap{T}"/>.
        /// </summary>
        /// <returns>Returns a new <see cref="BinaryMinHeap{T}"/> containing the elements of the <see cref="BinaryMaxHeap{T}"/>.</returns>
        public BinaryMinHeap<T> ToMinHeap()
        {
            var minHeap = new BinaryMinHeap<T>(Count, comparer);
            minHeap.Heapify(ToArray());
            return minHeap;
        }

        IMinHeap<T> IMaxHeap<T>.ToMinHeap()
        {
            return ToMinHeap();
        }
    }
}
