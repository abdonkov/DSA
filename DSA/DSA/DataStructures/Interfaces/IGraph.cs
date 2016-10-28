using System;
using System.Collections.Generic;

namespace DSA.DataStructures.Interfaces
{
    /// <summary>
    /// The graph interface.
    /// </summary>
    /// <typeparam name="TVertex">The data type of the vertices. TVertex implements <see cref="IComparable{T}"/>.</typeparam>
    public interface IGraph<TVertex>
        where TVertex : IComparable<TVertex>
    {
        /// <summary>
        /// Determines whether the graph is directed.
        /// </summary>
        bool IsDirected { get; }

        /// <summary>
        /// Deteremines whether the graph is weighted.
        /// </summary>
        bool IsWeighted { get; }

        /// <summary>
        /// Gets the number of edges in the graph.
        /// </summary>
        int EdgesCount { get; }

        /// <summary>
        /// Gets the number of vertices in the graph.
        /// </summary>
        int VerticesCount { get; }

        /// <summary>
        /// Gets the vertices in the graph.
        /// </summary>
        IEnumerable<TVertex> Vertices { get; }

        /// <summary>
        /// Gets the vertices in the graph in sorted ascending order.
        /// </summary>
        IEnumerable<TVertex> VerticesSorted { get; }

        /// <summary>
        /// Gets the edges in the graph.
        /// </summary>
        IEnumerable<IEdge<TVertex>> Edges { get; }

        /// <summary>
        /// Adds an edge defined by the given vertices. If the vertices are not present in the graph they will be added.
        /// </summary>
        /// <param name="firstVertex">The first vertex. Source of the edge if the graph is directed.</param>
        /// <param name="secondVertex">The second vertex. Destination of the edge if the graph is directed.</param>
        /// <returns>Returns true if the edge was added successfully; otherwise false. Also returns false if the edge already exists.</returns>
        bool AddEdge(TVertex firstVertex, TVertex secondVertex);

        /// <summary>
        /// Adds a vertex to the graph.
        /// </summary>
        /// <param name="vertex">The vertex to add.</param>
        /// <returns>Returns true if the edge was added successfully; otherwise false. Also returns false if the vertex already exists.</returns>
        bool AddVertex(TVertex vertex);

        /// <summary>
        /// Adds the specified collection of vertices to the graph. If some of the vertices are already in the graph exception is not thrown.
        /// </summary>
        /// <param name="vertices">Adds the <see cref="IEnumerable{T}"/> of vertices to the graph.</param>
        void AddVertices(IEnumerable<TVertex> vertices);

        /// <summary>
        /// Returns the incoming edges of the given vertex.
        /// </summary>
        /// <param name="vertex">The vertex whose incoming edges are returned.</param>
        /// <returns>Returns a <see cref="IEnumerable{T}"/> of <see cref="IEdge{TVertex}"/> of all incoming edges of the given vertex.</returns>
        IEnumerable<IEdge<TVertex>> IncomingEdges(TVertex vertex);

        /// <summary>
        /// Returns the outgoing edges of the given vertex.
        /// </summary>
        /// <param name="vertex">The vertex whose outgoing edges are returned.</param>
        /// <returns>Returns a <see cref="IEnumerable{T}"/> of <see cref="IEdge{TVertex}"/> of all outgoing edges of the given vertex.</returns>
        IEnumerable<IEdge<TVertex>> OutgoingEdges(TVertex vertex);

        /// <summary>
        /// Returns the incoming edges of the given vertex sorted by their source vertex.
        /// </summary>
        /// <param name="vertex">The vertex whose incoming edges are returned.</param>
        /// <returns>Returns a <see cref="IEnumerable{T}"/> of <see cref="IEdge{TVertex}"/> of all incoming edges of the given vertex.</returns>
        IEnumerable<IEdge<TVertex>> IncomingEdgesSorted(TVertex vertex);

        /// <summary>
        /// Returns the outgoing edges of the given vertex sorted by their destination vertex.
        /// </summary>
        /// <param name="vertex">The vertex whose outgoing edges are returned.</param>
        /// <returns>Returns a <see cref="IEnumerable{T}"/> of <see cref="IEdge{TVertex}"/> of all outgoing edges of the given vertex.</returns>
        IEnumerable<IEdge<TVertex>> OutgoingEdgesSorted(TVertex vertex);

        /// <summary>
        /// Determines whether the edge is presented in the graph.
        /// </summary>
        /// <param name="firstVertex">The first vertex of the edge. Source of the edge if the graph is directed.</param>
        /// <param name="secondVertex">The second vertex of the edge. Destination of the edge if graph is directed.</param>
        /// <returns>Returns true if the edge is presented in the graph; false otherwise.</returns>
        bool ContainsEdge(TVertex firstVertex, TVertex secondVertex);

        /// <summary>
        /// Determines whether the vertex is presented in the graph.
        /// </summary>
        /// <param name="vertex">The vertex to see if presented in the graph.</param>
        /// <returns>Returns true if the vertex is presented in the graph; false otherwise.</returns>
        bool ContainsVertex(TVertex vertex);

        /// <summary>
        /// Removes the edge defined by the given vertices.
        /// </summary>
        /// <param name="firstVertex">The first vertex. Source of the edge if the graph is directed.</param>
        /// <param name="secondVertex">The second vertex. Destination of the edge if the graph is directed.</param>
        /// <returns>Returns true if the edge was removed successfully; otherwise false. Also returns false if the vertices are not present in this graph or the edge does not exist.</returns>
        bool RemoveEdge(TVertex firstVertex, TVertex secondVertex);

        /// <summary>
        /// Removes the given vertex.
        /// </summary>
        /// <param name="vertex">The vertex to remove.</param>
        /// <returns>Returns true if the vertex was removed successfully; otherwise false. Also returns false if the vertex does not exist.</returns>
        bool RemoveVertex(TVertex vertex);

        /// <summary>
        /// Returns the degree of the given vertex.
        /// </summary>
        /// <param name="vertex">The vertex to calculate its degeree.</param>
        /// <returns>Returns the degree of the given vertex.</returns>
        int Degree(TVertex vertex);
        
        /// <summary>
        /// Removes all edges and vertices from the graph.
        /// </summary>
        void Clear();

        /// <summary>
        /// Breadth-first search of the graph with sorted levels. Returns <see cref="IEnumerable{T}"/> of the vertices.
        /// </summary>
        /// <param name="vertex">The vertex from which the breadth-first search starts.</param>
        /// <returns>Returns <see cref="IEnumerable{T}"/> of the vertices.</returns>
        IEnumerable<TVertex> BreadthFirstSearch(TVertex vertex);

        /// <summary>
        /// Breadth-first search of the graph with sorted levels. Returns <see cref="IEnumerable{T}"/> of <see cref="IEdge{TVertex}"/> representing the edges of the graph.
        /// </summary>
        /// <param name="vertex">The vertex from which the breadth-first search starts.</param>
        /// <returns>.Returns <see cref="IEnumerable{T}"/> of <see cref="IEdge{TVertex}"/> representing the edges of the graph.</returns>
        IEnumerable<IEdge<TVertex>> BreadthFirstSearchEdges(TVertex vertex);

        /// <summary>
        /// Depth-first search of the graph with sorted levels. Returns <see cref="IEnumerable{T}"/> of the vertices.
        /// </summary>
        /// <param name="vertex">The vertex from which the depth-first search starts.</param>
        /// <returns>Returns <see cref="IEnumerable{T}"/> of the vertices.</returns>
        IEnumerable<TVertex> DepthFirstSearch(TVertex vertex);

        /// <summary>
        /// Depth-first search of the graph with sorted levels. Returns <see cref="IEnumerable{T}"/> of <see cref="IEdge{TVertex}"/> representing the edges of the graph.
        /// </summary>
        /// <param name="vertex">The vertex from which the depth-first search starts.</param>
        /// <returns>.Returns <see cref="IEnumerable{T}"/> of <see cref="IEdge{TVertex}"/> representing the edges of the graph.</returns>
        IEnumerable<IEdge<TVertex>> DepthFirstSearchEdges(TVertex vertex);
    }
}
