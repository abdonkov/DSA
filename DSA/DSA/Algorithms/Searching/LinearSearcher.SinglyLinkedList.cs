using DSA.DataStructures.Lists;
using System;
using System.Collections.Generic;

namespace DSA.Algorithms.Searching
{
    public static partial class LinearSearcher
    {
        /// <summary>
        /// Searches the entire <see cref="SinglyLinkedList{T}"/> for an item using the default comparer and returns the <see cref="SinglyLinkedListNode{T}"/> of the first occurrence.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="SinglyLinkedList{T}"/>.</typeparam>
        /// <param name="list">The <see cref="SinglyLinkedList{T}"/> containing the items for searching.</param>
        /// <param name="item">The item to search for.</param>
        /// <returns>The <see cref="SinglyLinkedListNode{T}"/> of the first occurrence of the item in the <see cref="SinglyLinkedList{T}"/>, if the item is found; otherwise null.</returns>
        public static SinglyLinkedListNode<T> LinearSearchFirstNode<T>(this SinglyLinkedList<T> list, T item)
        {
            if (list.Count == 0) return null;

            return LinearSearchFirstNode(list, item, Comparer<T>.Default);
        }

        /// <summary>
        /// Searches the entire <see cref="SinglyLinkedList{T}"/> for an item using the specified comparison and returns the <see cref="SinglyLinkedListNode{T}"/> of the first occurrence.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="SinglyLinkedList{T}"/>.</typeparam>
        /// <param name="list">The <see cref="SinglyLinkedList{T}"/> containing the items for searching.</param>
        /// <param name="item">The item to search for.</param>
        /// <param name="comparison">The <see cref="Comparison{T}"/> used for comparing the elements.</param>
        /// <returns>The <see cref="SinglyLinkedListNode{T}"/> of the first occurrence of the item in the <see cref="SinglyLinkedList{T}"/>, if the item is found; otherwise null.</returns>
        public static SinglyLinkedListNode<T> LinearSearchFirstNode<T>(this SinglyLinkedList<T> list, T item, Comparison<T> comparison)
        {
            if (list.Count == 0) return null;

            return LinearSearchFirstNode(list, item, Comparer<T>.Create(comparison));
        }

        /// <summary>
        /// Searches the entire <see cref="SinglyLinkedList{T}"/> for an item using the specified comparer and returns the <see cref="SinglyLinkedListNode{T}"/> of the first occurrence.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="SinglyLinkedList{T}"/>.</typeparam>
        /// <param name="list">The <see cref="SinglyLinkedList{T}"/> containing the items for searching.</param>
        /// <param name="item">The item to search for.</param>
        /// <param name="comparer">The <see cref="IComparable{T}"/> implementation used for comparing the elements,
        /// or null to use the default comparer <see cref="Comparer{T}.Default"/>.</param>
        /// <returns>The <see cref="SinglyLinkedListNode{T}"/> of the first occurrence of the item in the <see cref="SinglyLinkedList{T}"/>, if the item is found; otherwise null.</returns>
        public static SinglyLinkedListNode<T> LinearSearchFirstNode<T>(this SinglyLinkedList<T> list, T item, IComparer<T> comparer)
        {
            if (list.Count == 0) return null;

            return LinearSearchFirstNode(list, item, list.First, list.Last, comparer);
        }

        /// <summary>
        /// Searches a range of items in the <see cref="SinglyLinkedList{T}"/> for an item using the specified comparer and returns the <see cref="SinglyLinkedListNode{T}"/> of the first occurrence.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="SinglyLinkedList{T}"/>.</typeparam>
        /// <param name="list">The <see cref="SinglyLinkedList{T}"/> containing the elements for searching.</param>
        /// <param name="item">The item to search for.</param>
        /// <param name="startNode">The start <see cref="SinglyLinkedListNode{T}"/> of the range for searching.</param>
        /// <param name="endNode">The end <see cref="SinglyLinkedListNode{T}"/> of the range for searching.</param>
        /// <param name="comparer">The <see cref="IComparable{T}"/> implementation used for comparing the elements,
        /// or null to use the default comparer <see cref="Comparer{T}.Default"/>.</param>
        /// <returns>The <see cref="SinglyLinkedListNode{T}"/> of the first occurrence of the item in the <see cref="SinglyLinkedList{T}"/>, if the item is found; otherwise null.</returns>
        public static SinglyLinkedListNode<T> LinearSearchFirstNode<T>(this SinglyLinkedList<T> list, T item, SinglyLinkedListNode<T> startNode, SinglyLinkedListNode<T> endNode, IComparer<T> comparer)
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
            while (curNode != nodeAfterEndNode && curNode != null && curNode != startNode);

            return null;
        }

        /// <summary>
        /// Searches the entire <see cref="SinglyLinkedList{T}"/> for an item using the default comparer and returns the <see cref="SinglyLinkedListNode{T}"/> of the last occurrence.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="SinglyLinkedList{T}"/>.</typeparam>
        /// <param name="list">The <see cref="SinglyLinkedList{T}"/> containing the items for searching.</param>
        /// <param name="item">The item to search for.</param>
        /// <returns>The <see cref="SinglyLinkedListNode{T}"/> of the last occurrence of the item in the <see cref="SinglyLinkedList{T}"/>, if the item is found; otherwise null.</returns>
        public static SinglyLinkedListNode<T> LinearSearchLastNode<T>(this SinglyLinkedList<T> list, T item)
        {
            if (list.Count == 0) return null;

            return LinearSearchLastNode(list, item, Comparer<T>.Default);
        }

        /// <summary>
        /// Searches the entire <see cref="SinglyLinkedList{T}"/> for an item using the specified comparison and returns the <see cref="SinglyLinkedListNode{T}"/> of the last occurrence.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="SinglyLinkedList{T}"/>.</typeparam>
        /// <param name="list">The <see cref="SinglyLinkedList{T}"/> containing the items for searching.</param>
        /// <param name="item">The item to search for.</param>
        /// <param name="comparison">The <see cref="Comparison{T}"/> used for comparing the elements.</param>
        /// <returns>The <see cref="SinglyLinkedListNode{T}"/> of the last occurrence of the item in the <see cref="SinglyLinkedList{T}"/>, if the item is found; otherwise null.</returns>
        public static SinglyLinkedListNode<T> LinearSearchLastNode<T>(this SinglyLinkedList<T> list, T item, Comparison<T> comparison)
        {
            if (list.Count == 0) return null;

            return LinearSearchLastNode(list, item, Comparer<T>.Create(comparison));
        }

        /// <summary>
        /// Searches the entire <see cref="SinglyLinkedList{T}"/> for an item using the specified comparer and returns the <see cref="SinglyLinkedListNode{T}"/> of the last occurrence.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="SinglyLinkedList{T}"/>.</typeparam>
        /// <param name="list">The <see cref="SinglyLinkedList{T}"/> containing the items for searching.</param>
        /// <param name="item">The item to search for.</param>
        /// <param name="comparer">The <see cref="IComparable{T}"/> implementation used for comparing the elements,
        /// or null to use the default comparer <see cref="Comparer{T}.Default"/>.</param>
        /// <returns>The <see cref="SinglyLinkedListNode{T}"/> of the last occurrence of the item in the <see cref="SinglyLinkedList{T}"/>, if the item is found; otherwise null.</returns>
        public static SinglyLinkedListNode<T> LinearSearchLastNode<T>(this SinglyLinkedList<T> list, T item, IComparer<T> comparer)
        {
            if (list.Count == 0) return null;

            return LinearSearchLastNode(list, item, list.First, list.Last, comparer);
        }

        /// <summary>
        /// Searches a range of items in the <see cref="SinglyLinkedList{T}"/> for an item using the specified comparer and returns the <see cref="SinglyLinkedListNode{T}"/> of the last occurrence.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="SinglyLinkedList{T}"/>.</typeparam>
        /// <param name="list">The <see cref="SinglyLinkedList{T}"/> containing the elements for searching.</param>
        /// <param name="item">The item to search for.</param>
        /// <param name="startNode">The start <see cref="SinglyLinkedListNode{T}"/> of the range for searching.</param>
        /// <param name="endNode">The end <see cref="SinglyLinkedListNode{T}"/> of the range for searching.</param>
        /// <param name="comparer">The <see cref="IComparable{T}"/> implementation used for comparing the elements,
        /// or null to use the default comparer <see cref="Comparer{T}.Default"/>.</param>
        /// <returns>The <see cref="SinglyLinkedListNode{T}"/> of the last occurrence of the item in the <see cref="SinglyLinkedList{T}"/>, if the item is found; otherwise null.</returns>
        public static SinglyLinkedListNode<T> LinearSearchLastNode<T>(this SinglyLinkedList<T> list, T item, SinglyLinkedListNode<T> startNode, SinglyLinkedListNode<T> endNode, IComparer<T> comparer)
        {
            if (startNode == null) throw new ArgumentNullException(nameof(startNode));
            if (endNode == null) throw new ArgumentNullException(nameof(endNode));
            if (startNode.List != list) throw new ArgumentException(nameof(startNode) + "doesn't belong to the list!");
            if (endNode.List != list) throw new ArgumentException(nameof(endNode) + "doesnt't belong to the list!");

            if (comparer == null) comparer = Comparer<T>.Default;

            var nodeAfterEndNode = endNode.Next;
            SinglyLinkedListNode<T> lastNode = null;
            var curNode = startNode;
            do
            {
                if (comparer.Compare(curNode.Value, item) == 0)
                {
                    lastNode = curNode;
                }

                curNode = curNode.Next;
            }
            while (curNode != nodeAfterEndNode && curNode != null && curNode != startNode);

            return lastNode;
        }
    }
}
