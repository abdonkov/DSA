namespace DSA.DataStructures.Lists
{
    /// <summary>
    /// Represents a node in the <see cref="DoublyLinkedList{T}"/>. This class cannot be inherited.
    /// </summary>
    /// <typeparam name="T">The stored data type.</typeparam>
    public sealed class DoublyLinkedListNode<T>
    {
        /// <summary>
        /// Gets the value contained in the node.
        /// </summary>
        public T Value { get; set; }

        /// <summary>
        /// Gets the next node in the <see cref="DoublyLinkedList{T}"/>.
        /// </summary>
        public DoublyLinkedListNode<T> Next { get; internal set; }

        /// <summary>
        /// Gets the previous node in the <see cref="DoublyLinkedList{T}"/>.
        /// </summary>
        public DoublyLinkedListNode<T> Previous { get; internal set; }

        /// <summary>
        /// Gets the <see cref="DoublyLinkedList{T}"/> that the <see cref="DoublyLinkedListNode{T}"/> belongs to.
        /// </summary>
        public DoublyLinkedList<T> List { get; internal set; }

        /// <summary>
        /// Creates a new instance of the <see cref="DoublyLinkedListNode{T}"/> with the specified value.
        /// </summary>
        /// <param name="value">The value contained in the <see cref="DoublyLinkedListNode{T}"/>.</param>
        public DoublyLinkedListNode(T value)
        {
            Value = value;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="DoublyLinkedListNode{T}"/> with the specified value and the specified list.
        /// </summary>
        /// <param name="value">The value contained in the <see cref="DoublyLinkedListNode{T}"/>.</param>
        /// <param name="list">The <see cref="DoublyLinkedList{T}"/> that the <see cref="DoublyLinkedListNode{T}"/> belongs to.</param>
        internal DoublyLinkedListNode(T value, DoublyLinkedList<T> list)
        {
            Value = value;
            List = list;
        }

        /// <summary>
        /// Removes all references the <see cref="DoublyLinkedListNode{T}"/> has.
        /// </summary>
        internal void Invalidate()
        {
            Next = null;
            Previous = null;
            List = null;
        }
    }
}
