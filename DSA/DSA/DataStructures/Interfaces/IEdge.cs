using System;

namespace DSA.DataStructures.Interfaces
{
    /// <summary>
    /// The edge interface.
    /// </summary>
    /// <typeparam name="TVertex">The data type of the vertices. TVertex implements <see cref="IComparable{T}"/>.</typeparam>
    public interface IEdge<TVertex>
        where TVertex : IComparable<TVertex>
    {
        /// <summary>
        /// Deteremines whether the edge is weighted.
        /// </summary>
        bool IsWeighted { get; }

        /// <summary>
        /// Gets the source vertex of the edge.
        /// </summary>
        TVertex Source { get; }

        /// <summary>
        /// Gets the destination vertex of the edge.
        /// </summary>
        TVertex Destination { get; }
    }
}
