using DSA.DataStructures.Lists;
using System;
using System.Collections.Generic;

namespace DSA.DataStructures.Queues
{
    /// <summary>
    /// Represents a min-priority queue structure.
    /// </summary>
    /// <typeparam name="TPriority">The data type of the priority.</typeparam>
    /// <typeparam name="TValue">The stored data type.</typeparam>
    public class MinPriorityQueue<TPriority, TValue>
        where TPriority : IComparable<TPriority>
    {
        /// <summary>
        /// The backing <see cref="ArrayList{T}"/> containing the elements of the <see cref="MinPriorityQueue{TPriority, TValue}"/>.
        /// </summary>
        internal ArrayList<KeyValuePair<TPriority, TValue>> array;

        /// <summary>
        /// The priority only comparer of the elements in the <see cref="MinPriorityQueue{TPriority, TValue}"/>.
        /// </summary>
        internal Comparer<TPriority> priorityComparer;

        /// <summary>
        /// The kvp comparer of the elements in the <see cref="MinPriorityQueue{TPriority, TValue}"/>.
        /// </summary>
        internal Comparer<KeyValuePair<TPriority, TValue>> kvpComparer;

        /// <summary>
        /// Determines wheter the elements are compared only by priority(with priorityComparer) or not(with kvpComparer).
        /// </summary>
        internal bool onlyPriorityComparison;

        /// <summary>
        /// Gets the number of elements in the <see cref="MinPriorityQueue{TPriority, TValue}"/>.
        /// </summary>
        public int Count { get; internal set; }

        /// <summary>
        /// Determines whether the <see cref="MinPriorityQueue{TPriority, TValue}"/> is empty.
        /// </summary>
        public bool IsEmpty { get { return Count == 0; } }

        /// <summary>
        /// Creates a new instance of the <see cref="MinPriorityQueue{TPriority, TValue}"/> class with default capacity and using the default comparer.
        /// </summary>
        public MinPriorityQueue() : this(4, priorityComparer: null) { }

        /// <summary>
        /// Creates a new instance of the <see cref="MinPriorityQueue{TPriority, TValue}"/> class with the given capacity for the backing <see cref="ArrayList{T}"/> and default comparer.
        /// </summary>
        /// <param name="capacity">The capacity for the underlying <see cref="ArrayList{T}"/>.</param>
        public MinPriorityQueue(int capacity) : this(capacity, priorityComparer: null) { }

        /// <summary>
        /// Creates a new instance of the <see cref="MinPriorityQueue{TPriority, TValue}"/> class with the specified comparer and default capacity.
        /// </summary>
        /// <param name="priorityComparer">The comparer of the elements in the <see cref="MinPriorityQueue{TPriority, TValue}"/>.</param>
        public MinPriorityQueue(Comparer<TPriority> priorityComparer) : this(4, priorityComparer) { }

        /// <summary>
        /// Creates a new instance of the <see cref="MinPriorityQueue{TPriority, TValue}"/> class with the specified comparer and default capacity.
        /// </summary>
        /// <param name="kvpComparer">The comparer of the elements in the <see cref="MinPriorityQueue{TPriority, TValue}"/>.</param>
        public MinPriorityQueue(Comparer<KeyValuePair<TPriority, TValue>> kvpComparer) : this(4, kvpComparer) { }

        /// <summary>
        /// Creates a new instance of the <see cref="MinPriorityQueue{TPriority, TValue}"/> class with the specified comparer and the given capacity for the backing <see cref="ArrayList{T}"/>.
        /// </summary>
        /// <param name="capacity">The capacity for the underlying <see cref="ArrayList{T}"/>.</param>
        /// <param name="priorityComparer">The comparer of the elements in the <see cref="MinPriorityQueue{TPriority, TValue}"/>.</param>
        public MinPriorityQueue(int capacity, Comparer<TPriority> priorityComparer)
        {
            array = new ArrayList<KeyValuePair<TPriority, TValue>>(capacity);
            this.priorityComparer = priorityComparer ?? Comparer<TPriority>.Default;
            onlyPriorityComparison = true;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="MinPriorityQueue{TPriority, TValue}"/> class with the specified comparer and the given capacity for the backing <see cref="ArrayList{T}"/>.
        /// </summary>
        /// <param name="capacity">The capacity for the underlying <see cref="ArrayList{T}"/>.</param>
        /// <param name="kvpComparer">The comparer of the elements in the <see cref="MinPriorityQueue{TPriority, TValue}"/>.</param>
        public MinPriorityQueue(int capacity, Comparer<KeyValuePair<TPriority, TValue>> kvpComparer)
        {
            array = new ArrayList<KeyValuePair<TPriority, TValue>>(capacity);
            if (kvpComparer == null)
            {
                this.priorityComparer = Comparer<TPriority>.Default;
                onlyPriorityComparison = true;
            }
            else
            {
                this.kvpComparer = kvpComparer;
                onlyPriorityComparison = false;
            }
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

                int cmp = onlyPriorityComparison ?
                    priorityComparer.Compare(array[parentIndex].Key, array[nodeIndex].Key)
                    : kvpComparer.Compare(array[parentIndex], array[nodeIndex]);
                if (cmp > 0)
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
                {
                    int rcmp = onlyPriorityComparison ?
                        priorityComparer.Compare(array[rigthChildIndex].Key, array[smallestNodeIndex].Key)
                        : kvpComparer.Compare(array[rigthChildIndex], array[smallestNodeIndex]);
                    if (rcmp < 0)
                        smallestNodeIndex = rigthChildIndex;
                }

                // compare left child with the smallest node(current node or right child)
                int lcmp = onlyPriorityComparison ?
                    priorityComparer.Compare(array[leftChildIndex].Key, array[smallestNodeIndex].Key)
                    : kvpComparer.Compare(array[leftChildIndex], array[smallestNodeIndex]);
                if (lcmp < 0)
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
        /// Heapifies the given <see cref="IEnumerable{T}"/> collection. Overrides the current <see cref="MinPriorityQueue{TPriority, TValue}"/>.
        /// </summary>
        /// <param name="collection">The collection of elements to heapify.</param>
        public void Heapify(IEnumerable<KeyValuePair<TPriority, TValue>> collection)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));

            var newArrayList = new ArrayList<KeyValuePair<TPriority, TValue>>(collection);

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
        /// Merges the elements of the <see cref="MinPriorityQueue{TPriority, TValue}"/> with the other given <see cref="MinPriorityQueue{TPriority, TValue}"/>.
        /// </summary>
        /// <param name="otherMinPriorityQueue">The other <see cref="MinPriorityQueue{TPriority, TValue}"/> used for merging.</param>
        public void Merge(MinPriorityQueue<TPriority, TValue> otherMinPriorityQueue)
        {
            for (int i = 0; i < otherMinPriorityQueue.Count; i++)
            {
                var kvp = otherMinPriorityQueue.array[i];
                Enqueue(kvp.Key, kvp.Value);
            }
        }

        /// <summary>
        /// Enqueues an element to the <see cref="MinPriorityQueue{TPriority, TValue}"/>.
        /// </summary>
        /// <param name="priority">The priority of the enqueued value.</param>
        /// <param name="value">The value to enqueue.</param>
        public void Enqueue(TPriority priority, TValue value)
        {
            if (IsEmpty)
                array.Add(new KeyValuePair<TPriority, TValue>(priority, value));
            else
            {
                array.Add(new KeyValuePair<TPriority, TValue>(priority, value));
                NodeHeapifyUp(array.Count - 1);
            }

            Count++;
        }

        /// <summary>
        /// Gets the first element of the <see cref="MinPriorityQueue{TPriority, TValue}"/>.
        /// </summary>
        /// <returns>Returns a <see cref="KeyValuePair{TKey, TValue}"/> (with the first element as value and its priority as key) from the <see cref="MinPriorityQueue{TPriority, TValue}"/>.</returns>
        public KeyValuePair<TPriority, TValue> Peek()
        {
            if (IsEmpty) throw new InvalidOperationException("PriorityQueue is empty!");

            return array[0];
        }

        /// <summary>
        /// Removes and returns the first element of the <see cref="MinPriorityQueue{TPriority, TValue}"/>.
        /// </summary>
        /// <returns>Returns a <see cref="KeyValuePair{TKey, TValue}"/> (with the first element as value and its priority as key) which was removed from the <see cref="MinPriorityQueue{TPriority, TValue}"/>.</returns>
        public KeyValuePair<TPriority, TValue> Dequeue()
        {
            var min = Peek();

            array[0] = array[Count - 1];
            array.RemoveAt(Count - 1);
            Count--;
            NodeHeapifyDown(0);

            return min;
        }

        /// <summary>
        /// Replaces the first element with the given value and priority.
        /// </summary>
        /// <param name="priority">The preority of the value. </param>
        /// <param name="value">The value to replace the first element of the <see cref="MinPriorityQueue{TPriority, TValue}"/>.</param>
        public void ReplaceFirst(TPriority priority, TValue value)
        {
            if (IsEmpty) throw new InvalidOperationException("PriorityQueue is empty!");

            array[0] = new KeyValuePair<TPriority, TValue>(priority, value);
            NodeHeapifyDown(0);
        }

        /// <summary>
        /// Determines whether a value is contained in the <see cref="MinPriorityQueue{TPriority, TValue}"/>.
        /// </summary>
        /// <param name="value">The value to search in the <see cref="MinPriorityQueue{TPriority, TValue}"/>.</param>
        /// <returns>returns true if the value is found; otherwise false.</returns>
        public bool ContainsValue(TValue value)
        {
            for (int i = 0; i < array.Count; i++)
            {
                if (object.Equals(array[i].Value, value))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Removes all elements from the <see cref="MinPriorityQueue{TPriority, TValue}"/>.
        /// </summary>
        public void Clear()
        {
            array = new ArrayList<KeyValuePair<TPriority, TValue>>();
            Count = 0;
        }

        /// <summary>
        /// Copies the elements of the <see cref="MinPriorityQueue{TPriority, TValue}"/> to a new array.
        /// </summary>
        /// <returns>An array containing copies of the elements of the <see cref="MinPriorityQueue{TPriority, TValue}"/>.</returns>
        public KeyValuePair<TPriority, TValue>[] ToArray()
        {
            var newArray = new KeyValuePair<TPriority, TValue>[Count];
            for (int i = 0; i < Count; i++)
            {
                newArray[i] = array[i];
            }
            return newArray;
        }
    }
}
