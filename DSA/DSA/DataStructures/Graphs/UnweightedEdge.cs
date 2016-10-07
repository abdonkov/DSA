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
        /// Gets the source vertex of the <see cref="UnweightedEdge{TVertex}"/>.
        /// </summary>
        public TVertex Source { get; internal set; }

        /// <summary>
        /// Gets the destination vertex of the <see cref="UnweightedEdge{TVertex}"/>.
        /// </summary>
        public TVertex Destination { get; internal set; }

        /// <summary>
        /// Creates a new instance of the <see cref="UnweightedEdge{TVertex}"/> defined by the given vertices.
        /// </summary>
        /// <param name="source">The source vertex of the <see cref="UnweightedEdge{TVertex}"/>.</param>
        /// <param name="destination">The destination vertex of the <see cref="UnweightedEdge{TVertex}"/>.</param>
        public UnweightedEdge(TVertex source, TVertex destination)
        {
            Source = source;
            Destination = destination;
        }
    }
}
