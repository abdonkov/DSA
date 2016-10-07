using DSA.DataStructures.Interfaces;
using System;

namespace DSA.DataStructures.Graphs
{
    /// <summary>
    /// Represents an unweighted edge in a graph.
    /// </summary>
    /// <typeparam name="TVertex">The data type of the vertices. TVertex implements <see cref="IComparable{T}"/>.</typeparam>
    public class UnweightedEdge<TVertex> : IEdge<TVertex>
        where TVertex : IComparable<TVertex>
    {
        /// <summary>
        /// Deteremines whether the edge is weighted.
        /// </summary>
        public bool IsWeighted { get { return false; } }

        /// <summary>
        /// Gets the source vertex of the edge.
        /// </summary>
        public TVertex Source { get; internal set; }

        /// <summary>
        /// Gets the destination vertex of the edge.
        /// </summary>
        public TVertex Destination { get; internal set; }
    }
}
