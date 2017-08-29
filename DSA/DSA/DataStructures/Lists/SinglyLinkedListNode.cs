namespace DSA.DataStructures.Lists
{
    /// <summary>
    /// Represents a node in the <see cref="SinglyLinkedList{T}"/>. This class cannot be inherited.
    /// </summary>
    /// <typeparam name="T">The stored data type.</typeparam>
    public sealed class SinglyLinkedListNode<T>
    {
        /// <summary>
        /// Gets the value contained in the node.
        /// </summary>
        public T Value { get; set; }

        /// <summary>
        /// Gets the next node in the <see cref="SinglyLinkedList{T}"/>.
        /// </summary>
        public SinglyLinkedListNode<T> Next { get; internal set; }

        /// <summary>
        /// Gets the <see cref="SinglyLinkedList{T}"/> that the <see cref="SinglyLinkedListNode{T}"/> belongs to.
        /// </summary>
        public SinglyLinkedList<T> List { get; internal set; }

        /// <summary>
        /// Creates a new instance of the <see cref="SinglyLinkedListNode{T}"/> with the specified value.
        /// </summary>
        /// <param name="value">The value contained in the <see cref="SinglyLinkedListNode{T}"/>.</param>
        public SinglyLinkedListNode(T value)
        {
            Value = value;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="SinglyLinkedListNode{T}"/> with the specified value and the specified list.
        /// </summary>
        /// <param name="value">The value contained in the <see cref="SinglyLinkedListNode{T}"/>.</param>
        /// <param name="list">The <see cref="SinglyLinkedList{T}"/> that the <see cref="SinglyLinkedListNode{T}"/> belongs to.</param>
        internal SinglyLinkedListNode(T value, SinglyLinkedList<T> list)
        {
            Value = value;
            List = list;
        }

        /// <summary>
        /// Removes all references the <see cref="SinglyLinkedListNode{T}"/> has.
        /// </summary>
        internal void Invalidate()
        {
            Next = null;
            List = null;
        }
    }
}
