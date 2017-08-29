using System;
using System.Collections.Generic;

namespace DSA.Algorithms.Searching
{
    public static partial class LinearSearcher
    {
        /// <summary>
        /// Searches the entire <see cref="LinkedList{T}"/> for an item using the default comparer and returns the <see cref="LinkedListNode{T}"/> of the first occurrence.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="LinkedList{T}"/>.</typeparam>
        /// <param name="list">The <see cref="LinkedList{T}"/> containing the items for searching.</param>
        /// <param name="item">The item to search for.</param>
        /// <returns>The <see cref="LinkedListNode{T}"/> of the first occurrence of the item in the <see cref="LinkedList{T}"/>, if the item is found; otherwise null.</returns>
        public static LinkedListNode<T> LinearSearchFirstNode<T>(this LinkedList<T> list, T item)
        {
            if (list.Count == 0) return null;

            return LinearSearchFirstNode(list, item, Comparer<T>.Default);
        }

        /// <summary>
        /// Searches the entire <see cref="LinkedList{T}"/> for an item using the specified comparison and returns the <see cref="LinkedListNode{T}"/> of the first occurrence.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="LinkedList{T}"/>.</typeparam>
        /// <param name="list">The <see cref="LinkedList{T}"/> containing the items for searching.</param>
        /// <param name="item">The item to search for.</param>
        /// <param name="comparison">The <see cref="Comparison{T}"/> used for comparing the elements.</param>
        /// <returns>The <see cref="LinkedListNode{T}"/> of the first occurrence of the item in the <see cref="LinkedList{T}"/>, if the item is found; otherwise null.</returns>
        public static LinkedListNode<T> LinearSearchFirstNode<T>(this LinkedList<T> list, T item, Comparison<T> comparison)
        {
            if (list.Count == 0) return null;

            return LinearSearchFirstNode(list, item, Comparer<T>.Create(comparison));
        }

        /// <summary>
        /// Searches the entire <see cref="LinkedList{T}"/> for an item using the specified comparer and returns the <see cref="LinkedListNode{T}"/> of the first occurrence.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="LinkedList{T}"/>.</typeparam>
        /// <param name="list">The <see cref="LinkedList{T}"/> containing the items for searching.</param>
        /// <param name="item">The item to search for.</param>
        /// <param name="comparer">The <see cref="IComparable{T}"/> implementation used for comparing the elements,
        /// or null to use the default comparer <see cref="Comparer{T}.Default"/>.</param>
        /// <returns>The <see cref="LinkedListNode{T}"/> of the first occurrence of the item in the <see cref="LinkedList{T}"/>, if the item is found; otherwise null.</returns>
        public static LinkedListNode<T> LinearSearchFirstNode<T>(this LinkedList<T> list, T item, IComparer<T> comparer)
        {
            if (list.Count == 0) return null;

            return LinearSearchFirstNode(list, item, list.First, list.Last, comparer);
        }

        /// <summary>
        /// Searches a range of items in the <see cref="LinkedList{T}"/> for an item using the specified comparer and returns the <see cref="LinkedListNode{T}"/> of the first occurrence.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="LinkedList{T}"/>.</typeparam>
        /// <param name="list">The <see cref="LinkedList{T}"/> containing the elements for searching.</param>
        /// <param name="item">The item to search for.</param>
        /// <param name="startNode">The start <see cref="LinkedListNode{T}"/> of the range for searching.</param>
        /// <param name="endNode">The end <see cref="LinkedListNode{T}"/> of the range for searching.</param>
        /// <param name="comparer">The <see cref="IComparable{T}"/> implementation used for comparing the elements,
        /// or null to use the default comparer <see cref="Comparer{T}.Default"/>.</param>
        /// <returns>The <see cref="LinkedListNode{T}"/> of the first occurrence of the item in the <see cref="LinkedList{T}"/>, if the item is found; otherwise null.</returns>
        public static LinkedListNode<T> LinearSearchFirstNode<T>(this LinkedList<T> list, T item, LinkedListNode<T> startNode, LinkedListNode<T> endNode, IComparer<T> comparer)
        {
            if (startNode == null) throw new ArgumentNullException(nameof(startNode));
            if (endNode == null) throw new ArgumentNullException(nameof(endNode));
            if (startNode.List != list) throw new ArgumentException(nameof(startNode) + "doesn't belong to the list!");
            if (endNode.List != list) throw new ArgumentException(nameof(endNode) + "doesnt't belong to the list!");

            if (comparer == null) comparer = Comparer<T>.Default;

            var nodeAfterEndNode = endNode.Next;

            var curNode = startNode;
            do
            {
                if (comparer.Compare(curNode.Value, item) == 0)
                    return curNode;

                curNode = curNode.Next;
            }
            while (curNode != nodeAfterEndNode && curNode != startNode && curNode != null);
            
            return null;
        }

        /// <summary>
        /// Searches the entire <see cref="LinkedList{T}"/> for an item using the default comparer and returns the <see cref="LinkedListNode{T}"/> of the last occurrence.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="LinkedList{T}"/>.</typeparam>
        /// <param name="list">The <see cref="LinkedList{T}"/> containing the items for searching.</param>
        /// <param name="item">The item to search for.</param>
        /// <returns>The <see cref="LinkedListNode{T}"/> of the last occurrence of the item in the <see cref="LinkedList{T}"/>, if the item is found; otherwise null.</returns>
        public static LinkedListNode<T> LinearSearchLastNode<T>(this LinkedList<T> list, T item)
        {
            if (list.Count == 0) return null;

            return LinearSearchLastNode(list, item, Comparer<T>.Default);
        }

        /// <summary>
        /// Searches the entire <see cref="LinkedList{T}"/> for an item using the specified comparison and returns the <see cref="LinkedListNode{T}"/> of the last occurrence.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="LinkedList{T}"/>.</typeparam>
        /// <param name="list">The <see cref="LinkedList{T}"/> containing the items for searching.</param>
        /// <param name="item">The item to search for.</param>
        /// <param name="comparison">The <see cref="Comparison{T}"/> used for comparing the elements.</param>
        /// <returns>The <see cref="LinkedListNode{T}"/> of the last occurrence of the item in the <see cref="LinkedList{T}"/>, if the item is found; otherwise null.</returns>
        public static LinkedListNode<T> LinearSearchLastNode<T>(this LinkedList<T> list, T item, Comparison<T> comparison)
        {
            if (list.Count == 0) return null;

            return LinearSearchLastNode(list, item, Comparer<T>.Create(comparison));
        }

        /// <summary>
        /// Searches the entire <see cref="LinkedList{T}"/> for an item using the specified comparer and returns the <see cref="LinkedListNode{T}"/> of the last occurrence.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="LinkedList{T}"/>.</typeparam>
        /// <param name="list">The <see cref="LinkedList{T}"/> containing the items for searching.</param>
        /// <param name="item">The item to search for.</param>
        /// <param name="comparer">The <see cref="IComparable{T}"/> implementation used for comparing the elements,
        /// or null to use the default comparer <see cref="Comparer{T}.Default"/>.</param>
        /// <returns>The <see cref="LinkedListNode{T}"/> of the last occurrence of the item in the <see cref="LinkedList{T}"/>, if the item is found; otherwise null.</returns>
        public static LinkedListNode<T> LinearSearchLastNode<T>(this LinkedList<T> list, T item, IComparer<T> comparer)
        {
            if (list.Count == 0) return null;

            return LinearSearchLastNode(list, item, list.First, list.Last, comparer);
        }

        /// <summary>
        /// Searches a range of items in the <see cref="LinkedList{T}"/> for an item using the specified comparer and returns the <see cref="LinkedListNode{T}"/> of the last occurrence.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="LinkedList{T}"/>.</typeparam>
        /// <param name="list">The <see cref="LinkedList{T}"/> containing the elements for searching.</param>
        /// <param name="item">The item to search for.</param>
        /// <param name="startNode">The start <see cref="LinkedListNode{T}"/> of the range for searching.</param>
        /// <param name="endNode">The end <see cref="LinkedListNode{T}"/> of the range for searching.</param>
        /// <param name="comparer">The <see cref="IComparable{T}"/> implementation used for comparing the elements,
        /// or null to use the default comparer <see cref="Comparer{T}.Default"/>.</param>
        /// <returns>The <see cref="LinkedListNode{T}"/> of the last occurrence of the item in the <see cref="LinkedList{T}"/>, if the item is found; otherwise null.</returns>
        public static LinkedListNode<T> LinearSearchLastNode<T>(this LinkedList<T> list, T item, LinkedListNode<T> startNode, LinkedListNode<T> endNode, IComparer<T> comparer)
        {
            if (startNode == null) throw new ArgumentNullException(nameof(startNode));
            if (endNode == null) throw new ArgumentNullException(nameof(endNode));
            if (startNode.List != list) throw new ArgumentException(nameof(startNode) + "doesn't belong to the list!");
            if (endNode.List != list) throw new ArgumentException(nameof(endNode) + "doesnt't belong to the list!");

            if (comparer == null) comparer = Comparer<T>.Default;

            var nodeBeforeStartNode = startNode.Previous;

            var curNode = endNode;
            do
            {
                if (comparer.Compare(curNode.Value, item) == 0)
                    return curNode;

                curNode = curNode.Previous;
            }
            while (curNode != nodeBeforeStartNode && curNode != endNode && curNode != null);

            return null;
        }
    }
}
