using System;

namespace DSA.DataStructures.Lists
{
    /// <summary>
    /// Represents a node in the <see cref="SkipList{T}"/>. This class cannot be inherited.
    /// </summary>
    /// <typeparam name="T">The stored data type.</typeparam>
    public sealed class SkipListNode<T> where T : IComparable<T>
    {
        /// <summary>
        /// An array containing the references to the next <see cref="SkipListNode{T}"/> on every level.
        /// </summary>
        private SkipListNode<T>[] forwards;

        /// <summary>
        /// Gets the value contained in the node.
        /// </summary>
        public T Value { get; internal set; }

        /// <summary>
        /// Gets the node height.
        /// </summary>
        public int Height { get { return forwards?.Length ?? 0; } }

        /// <summary>
        /// Gets the next <see cref="SkipListNode{T}"/> on the given zero-based level.
        /// </summary>
        /// <param name="level">The zero-based level on which the next <see cref="SkipListNode{T}"/> will be returned.</param>
        /// <returns>The next <see cref="SkipListNode{T}"/> on the given zero-based level.</returns>
        public SkipListNode<T> this[int level]
        {
            get
            {
                if (level < 0 || level >= Height) throw new IndexOutOfRangeException(nameof(level));
                return forwards[level];
            }
            internal set
            {
                if (level < 0 || level >= Height) throw new IndexOutOfRangeException(nameof(level));
                forwards[level] = value;
            }
        }

        /// <summary>
        /// Creates a new instance of the <see cref="SkipListNode{T}"/> with the specified value and the given height.
        /// </summary>
        /// <param name="value">The value contained in the <see cref="SkipListNode{T}"/>.</param>
        /// <param name="height">The height of the <see cref="SkipListNode{T}"/>.</param>
        public SkipListNode(T value, int height)
        {
            if (height < 1) throw new ArgumentOutOfRangeException(nameof(height));
            Value = value;
            forwards = new SkipListNode<T>[height];
        }

        /// <summary>
        /// Increases the height of the <see cref="SkipListNode{T}"/> by 1. Used for the head node of the <see cref="SkipList{T}"/>.
        /// </summary>
        internal void IncrementHeight()
        {
            var newForwards = new SkipListNode<T>[Height + 1];
            for (int i = 0; i < Height; i++)
            {
                newForwards[i] = forwards[i];
            }

            forwards = newForwards;
        }

        /// <summary>
        /// Decreases the height of the <see cref="SkipListNode{T}"/> by 1. Used for the head node of the <see cref="SkipList{T}"/>.
        /// </summary>
        internal void DecrementHeight()
        {
            if (Height == 1) return;

            var newForwards = new SkipListNode<T>[Height - 1];
            for (int i = 0; i < Height - 1; i++)
            {
                newForwards[i] = forwards[i];
            }

            forwards = newForwards;
        }

        /// <summary>
        /// Removes all references the <see cref="SkipListNode{T}"/> has.
        /// </summary>
        internal void Invalidate()
        {
            forwards = null;
        }
    }
}
