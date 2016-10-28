using System;
using System.Collections.Generic;

namespace DSA.DataStructures.Interfaces
{
    /// <summary>
    /// The weighted graph interface.
    /// </summary>
    /// <typeparam name="TVertex">The data type of the vertices. TVertex implements <see cref="IComparable{T}"/>.</typeparam>
    /// <typeparam name="TWeight">The data type of the weight of the edges. TWeight implements <see cref="IComparable{T}"/>.</typeparam>
    public interface IWeightedGraph<TVertex, TWeight> : IGraph<TVertex>
        where TVertex : IComparable<TVertex>
        where TWeight : IComparable<TWeight>
    {
        /// <summary>
        /// Gets the edges in the graph.
        /// </summary>
        new IEnumerable<IWeightedEdge<TVertex, TWeight>> Edges { get; }

        /// <summary>
        /// Adds an edge defined by the given vertices, having the given weight. If the vertices are not present in the graph they will be added.
        /// </summary>
        /// <param name="firstVertex">The first vertex. Source of the edge if the graph is directed.</param>
        /// <param name="secondVertex">The second vertex. Destination of the edge if the graph is directed.</param>
        /// <param name="weight">The weight of the edge.</param>
        /// <returns>Returns true if the edge was added successfully; otherwise false. Also returns false if the edge already exists.</returns>
        bool AddEdge(TVertex firstVertex, TVertex secondVertex, TWeight weight);

        /// <summary>
        /// Returns the incoming edges of the given vertex.
        /// </summary>
        /// <param name="vertex">The vertex whose incoming edges are returned.</param>
        /// <returns>Returns a <see cref="IEnumerable{T}"/> of <see cref="IWeightedEdge{TVertex, TWeight}"/> of all incoming edges of the given vertex.</returns>
        new IEnumerable<IWeightedEdge<TVertex, TWeight>> IncomingEdges(TVertex vertex);

        /// <summary>
        /// Returns the outgoing edges of the given vertex.
        /// </summary>
        /// <param name="vertex">The vertex whose outgoing edges are returned.</param>
        /// <returns>Returns a <see cref="IEnumerable{T}"/> of <see cref="IWeightedEdge{TVertex, TWeight}"/> of all outgoing edges of the given vertex.</returns>
        new IEnumerable<IWeightedEdge<TVertex, TWeight>> OutgoingEdges(TVertex vertex);

        /// <summary>
        /// Returns the incoming edges of the given vertex sorted by their source vertex.
        /// </summary>
        /// <param name="vertex">The vertex whose incoming edges are returned.</param>
        /// <returns>Returns a <see cref="IEnumerable{T}"/> of <see cref="IWeightedEdge{TVertex, TWeight}"/> of all incoming edges of the given vertex.</returns>
        new IEnumerable<IWeightedEdge<TVertex, TWeight>> IncomingEdgesSorted(TVertex vertex);

        /// <summary>
        /// Returns the outgoing edges of the given vertex sorted by their destination vertex.
        /// </summary>
        /// <param name="vertex">The vertex whose outgoing edges are returned.</param>
        /// <returns>Returns a <see cref="IEnumerable{T}"/> of <see cref="IWeightedEdge{TVertex, TWeight}"/> of all outgoing edges of the given vertex.</returns>
        new IEnumerable<IWeightedEdge<TVertex, TWeight>> OutgoingEdgesSorted(TVertex vertex);

        /// <summary>
        /// Updates the weight of the edge defined by the given vertices.
        /// </summary>
        /// <param name="firstVertex">The first vertex. Source of the edge if the graph is directed.</param>
        /// <param name="secondVertex">The second vertex. Destination of the edge if the graph is directed.</param>
        /// <param name="weight">The new weight of the edge.</param>
        /// <returns>Returns true if the edge weight was updated successfully; otherwise, false. Also returns false if the vertices are not present in this graph.</returns>
        bool UpdateEdgeWeight(TVertex firstVertex, TVertex secondVertex, TWeight weight);

        /// <summary>
        /// Gets the weight of the edge defined by the given vertices.
        /// </summary>
        /// <param name="firstVertex">The first vertex. Source of the edge if the graph is directed.</param>
        /// <param name="secondVertex">The second vertex. Destination of the edge if the graph is directed.</param>
        /// <param name="weight">Contains the weight of the edge, if the edge is presented in the graph; otherwise, contains the default value for the type of the weight parameter.</param>
        /// <returns>Returns true if the graph contains the specified edge; otherwise, false.</returns>
        bool TryGetEdgeWeight(TVertex firstVertex, TVertex secondVertex, out TWeight weight);

        /// <summary>
        /// Breadth-first search of the graph with sorted levels. Returns <see cref="IEnumerable{T}"/> of <see cref="IWeightedEdge{TVertex, TWeight}"/> representing the edges of the graph.
        /// </summary>
        /// <param name="vertex">The vertex from which the breadth-first search starts.</param>
        /// <returns>.Returns <see cref="IEnumerable{T}"/> of <see cref="IWeightedEdge{TVertex, TWeight}"/> representing the edges of the graph.</returns>
        new IEnumerable<IWeightedEdge<TVertex, TWeight>> BreadthFirstSearchEdges(TVertex vertex);

        /// <summary>
        /// Depth-first search of the graph with sorted levels. Returns <see cref="IEnumerable{T}"/> of <see cref="IWeightedEdge{TVertex, TWeight}"/> representing the edges of the graph.
        /// </summary>
        /// <param name="vertex">The vertex from which the depth-first search starts.</param>
        /// <returns>.Returns <see cref="IEnumerable{T}"/> of <see cref="IWeightedEdge{TVertex, TWeight}"/> representing the edges of the graph.</returns>
        new IEnumerable<IWeightedEdge<TVertex, TWeight>> DepthFirstSearchEdges(TVertex vertex);
    }
}
