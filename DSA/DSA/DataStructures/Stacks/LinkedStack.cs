using System;
using System.Collections;
using System.Collections.Generic;

namespace DSA.DataStructures.Stacks
{
    /// <summary>
    /// Represents a linked list based stack structure.
    /// </summary>
    /// <typeparam name="T">The stored data type.</typeparam>
    public class LinkedStack<T> : IEnumerable<T>
    {
        /// <summary>
        /// The head node of the linked stack.
        /// </summary>
        internal Node Head { get; set; }

        /// <summary>
        /// Gets the number of elements in the <see cref="LinkedStack{T}"/>.
        /// </summary>
        public int Count { get; internal set; }

        /// <summary>
        /// Creates a new instance of the <see cref="LinkedStack{T}"/> class that is empty.
        /// </summary>
        public LinkedStack()
        {
            Count = 0;
            Head = null;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="LinkedStack{T}"/> class that contains the elements from the specified collection.
        /// </summary>
        /// <param name="collection">The collection to copy elements from.</param>
        public LinkedStack(IEnumerable<T> collection)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));

            Count = 0;
            Node head = null;

            foreach (var item in collection)
            {
                head = new Node(item, head);
                Count++;
            }

            Head = head;
        }

        /// <summary>
        /// Returns the item at the top of the <see cref="LinkedStack{T}"/> without removing it.
        /// </summary>
        /// <returns>The item at the top of the <see cref="LinkedStack{T}"/>.</returns>
        public T Peek()
        {
            if (Count == 0)
                throw new InvalidOperationException("Stack is empty!");

            return Head.Data;
        }

        /// <summary>
        /// Removes and returns the item at the top of the <see cref="LinkedStack{T}"/>.
        /// </summary>
        /// <returns>The item removed from the top of the <see cref="LinkedStack{T}"/>.</returns>
        public T Pop()
        {
            if (Count == 0)
                throw new InvalidOperationException("Stack is empty!");

            T item = Head.Data;

            Head = Head.Next;
            Count--;

            return item;
        }

        /// <summary>
        /// Inserts an item at the top of the <see cref="LinkedStack{T}"/>.
        /// </summary>
        /// <param name="item">The item to push onto the <see cref="LinkedStack{T}"/>. The value can be null for reference types.</param>
        public void Push(T item)
        {
            Head = new Node(item, Head);
            Count++;
        }

        /// <summary>
        /// Determines whether an item is in the <see cref="LinkedStack{T}"/>.
        /// </summary>
        /// <param name="item">The item to search in the <see cref="LinkedStack{T}"/>.</param>
        /// <returns>returns true if the item is found; otherwise false.</returns>
        public bool Contains(T item)
        {
            Node current = Head;
            while(current != null)
            {
                if (object.Equals(current.Data, item)) return true;
                current = current.Next;
            }
            return false;
        }

        /// <summary>
        /// Removes all elements from the <see cref="LinkedStack{T}"/>.
        /// </summary>
        public void Clear()
        {
            Head = null;
            Count = 0;
        }

        /// <summary>
        /// Copies the elements of the <see cref="LinkedStack{T}"/> to a new array in last-in-first-out (LIFO) order.
        /// </summary>
        /// <returns>An array containing copies of the elements of the <see cref="LinkedStack{T}"/>.</returns>
        public T[] ToArray()
        {
            T[] array = new T[Count];
            int index = 0;
            Node current = Head;
            while (current != null)
            {
                array[index++] = current.Data;
                current = current.Next;
            }
            return array;
        }

        /// <summary>
        /// Returns an enumerator that iterates throught the <see cref="LinkedStack{T}"/>.
        /// </summary>
        /// <returns>Enumerator for the <see cref="LinkedStack{T}"/>.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            Node current = Head;
            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>
        /// Repesents a node in the <see cref="LinkedStack{T}"/>.
        /// </summary>
        internal class Node
        {
            /// <summary>
            /// The data stored in the <see cref="Node"/>.
            /// </summary>
            public T Data { get; set; }

            /// <summary>
            /// The next <see cref="Node"/>.
            /// </summary>
            public Node Next { get; set; }

            /// <summary>
            /// Creates a new node with the given data and pointing to the given next node.
            /// </summary>
            /// <param name="data">The data stored in the node.</param>
            /// <param name="next">The next node.</param>
            public Node(T data, Node next)
            {
                Data = data;
                Next = next;
            }
        }
    }
}
