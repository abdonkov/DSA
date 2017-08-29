using DSA.DataStructures.Interfaces;
using DSA.DataStructures.Lists;
using System;
using System.Collections.Generic;

namespace DSA.DataStructures.Heaps
{
    /// <summary>
    /// Represents a binary min heap backed by an <see cref="ArrayList{T}"/>.
    /// </summary>
    /// <typeparam name="T">The stored value type. T implements <see cref="IComparable{T}"/>.</typeparam>
    public class BinaryMinHeap<T> : IMinHeap<T>
        where T : IComparable<T>
    {
        /// <summary>
        /// The backing <see cref="ArrayList{T}"/> containing the elements of the <see cref="BinaryMinHeap{T}"/>.
        /// </summary>
        internal ArrayList<T> array;

        /// <summary>
        /// The comparer of the elements in the <see cref="BinaryMinHeap{T}"/>.
        /// </summary>
        internal Comparer<T> comparer;

        /// <summary>
        /// Gets the number of elements in the <see cref="BinaryMinHeap{T}"/>.
        /// </summary>
        public int Count { get; internal set; }

        /// <summary>
        /// Determines whether the <see cref="BinaryMinHeap{T}"/> is empty.
        /// </summary>
        public bool IsEmpty { get { return Count == 0; } }

        /// <summary>
        /// Creates a new instance of the <see cref="BinaryMinHeap{T}"/> class.
        /// </summary>
        public BinaryMinHeap() : this(0, null) { }

        /// <summary>
        /// Creates a new instance of the <see cref="BinaryMinHeap{T}"/> class with the given capacity for the backing <see cref="ArrayList{T}"/>.
        /// </summary>
        /// <param name="capacity">The capacity for the underlying <see cref="ArrayList{T}"/>.</param>
        public BinaryMinHeap(int capacity) : this(capacity, null) { }

        /// <summary>
        /// Creates a new instance of the <see cref="BinaryMinHeap{T}"/> class with the specified comparer.
        /// </summary>
        /// <param name="comparer">The comparer of the elements in the <see cref="BinaryMinHeap{T}"/>.</param>
        public BinaryMinHeap(Comparer<T> comparer) : this(0, comparer) { }

        /// <summary>
        /// Creates a new instance of the <see cref="BinaryMinHeap{T}"/> class with the specified comparer and the given capacity for the backing <see cref="ArrayList{T}"/>.
        /// </summary>
        /// <param name="capacity">The capacity for the underlying <see cref="ArrayList{T}"/>.</param>
        /// <param name="comparer">The comparer of the elements in the <see cref="BinaryMinHeap{T}"/>.</param>
        public BinaryMinHeap(int capacity, Comparer<T> comparer)
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
            while(nodeIndex > 0)
            {
                // Getting the parent node
                int parentIndex = (nodeIndex - 1) / 2;

                if (comparer.Compare(array[parentIndex], array[nodeIndex]) > 0)
                {// if the parent is bigger that the current node
                    // we have to swap them
                    var temp = array[parentIndex];
                    array[parentIndex] = array[nodeIndex];
                    array[nodeIndex] = temp;
                    // then we continue comparing the node with it's new parent
                    nodeIndex = parentIndex;
                }
                else// if the parent is smaller than or equal to the current node
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
                // saving the index of the smallest node of the three(current node and its children)
                var smallestNodeIndex = nodeIndex;

                // compare right child with the smallest node(current node for now)
                if (rigthChildIndex < array.Count)// needed checking if there is a right node also
                    if (comparer.Compare(array[rigthChildIndex], array[smallestNodeIndex]) < 0)
                        smallestNodeIndex = rigthChildIndex;

                // compare left child with the smallest node(current node or right child)
                if (comparer.Compare(array[leftChildIndex], array[smallestNodeIndex]) < 0)
                    smallestNodeIndex = leftChildIndex;

                // if the current node is the smallest
                if (smallestNodeIndex == nodeIndex)
                    break;// no more adjustments are needed

                // else if one of the children is smaller that the current node we swap them
                var temp = array[nodeIndex];
                array[nodeIndex] = array[smallestNodeIndex];
                array[smallestNodeIndex] = temp;

                // continue downwards with the comparison
                nodeIndex = smallestNodeIndex;
                leftChildIndex = 2 * nodeIndex + 1;
                rigthChildIndex = 2 * nodeIndex + 2;
            }
        }

        /// <summary>
        /// Heapifies the given <see cref="IEnumerable{T}"/> collection. Overrides the current <see cref="BinaryMinHeap{T}"/>.
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
        /// Merges the elements of the <see cref="BinaryMinHeap{T}"/> with the other given <see cref="BinaryMinHeap{T}"/>.
        /// </summary>
        /// <param name="otherMinHeap">The other <see cref="BinaryMinHeap{T}"/> used for merging.</param>
        public void Merge(BinaryMinHeap<T> otherMinHeap)
        {
            for (int i = 0; i < otherMinHeap.Count; i++)
            {
                Add(otherMinHeap.array[i]);
            }
        }

        /// <summary>
        /// Adds an element to the <see cref="BinaryMinHeap{T}"/>.
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
        /// Gets the min element of the <see cref="BinaryMinHeap{T}"/>.
        /// </summary>
        /// <returns>Returns the min element of the <see cref="BinaryMinHeap{T}"/>.</returns>
        public T PeekMin()
        {
            if (IsEmpty) throw new InvalidOperationException("Heap is empty!");

            return array[0];
        }

        /// <summary>
        /// Removes and returns the min element of the <see cref="BinaryMinHeap{T}"/>.
        /// </summary>
        /// <returns>Returns the min element which was removed from the <see cref="BinaryMinHeap{T}"/>.</returns>
        public T PopMin()
        {
            var min = PeekMin();
            RemoveMin();
            return min;
        }

        /// <summary>
        /// Replaces the min element with the given value and rebalancing the <see cref="BinaryMinHeap{T}"/>.
        /// </summary>
        /// <param name="value">The value to replace the min element of the <see cref="BinaryMinHeap{T}"/>.</param>
        public void ReplaceMin(T value)
        {
            if (IsEmpty) if (IsEmpty) throw new InvalidOperationException("Heap is empty!");

            array[0] = value;
            NodeHeapifyDown(0);
        }

        /// <summary>
        /// Removes the min element from the <see cref="BinaryMinHeap{T}"/>.
        /// </summary>
        public void RemoveMin()
        {
            if (IsEmpty) throw new InvalidOperationException("Heap is empty!");

            array[0] = array[Count - 1];
            array.RemoveAt(Count - 1);
            Count--;
            NodeHeapifyDown(0);
        }

        /// <summary>
        /// Removes all elements from the <see cref="BinaryMinHeap{T}"/>.
        /// </summary>
        public void Clear()
        {
            array = new ArrayList<T>();
            Count = 0;
        }

        /// <summary>
        /// Copies the elements of the <see cref="BinaryMinHeap{T}"/> to a new array.
        /// </summary>
        /// <returns>An array containing copies of the elements of the <see cref="BinaryMinHeap{T}"/>.</returns>
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
        /// Returns a new <see cref="BinaryMaxHeap{T}"/> containing the elements of the <see cref="BinaryMinHeap{T}"/>.
        /// </summary>
        /// <returns>Returns a new <see cref="BinaryMaxHeap{T}"/> containing the elements of the <see cref="BinaryMinHeap{T}"/>.</returns>
        public BinaryMaxHeap<T> ToMaxHeap()
        {
            var maxHeap = new BinaryMaxHeap<T>(Count, comparer);
            maxHeap.Heapify(ToArray());
            return maxHeap;
        }

        IMaxHeap<T> IMinHeap<T>.ToMaxHeap()
        {
            return ToMaxHeap();
        }
    }
}
