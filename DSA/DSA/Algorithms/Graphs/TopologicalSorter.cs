using DSA.DataStructures.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DSA.Algorithms.Graphs
{
    /// <summary>
    /// A static class containing extension methods for graph topological sorting.
    /// </summary>
    public static class TopologicalSorter
    {
        /// <summary>
        /// Topological sort for the vertices of a graph where the smaller vertex comes first.
        /// </summary>
        /// <typeparam name="TVertex">The data type of the vertices. TVertex implements <see cref="IComparable{T}"/>.</typeparam>
        /// <param name="graph">The graph structure for topological sorting.</param>
        /// <returns>Returns a <see cref="IList{T}"/> of the sorted vertices.</returns>
        public static IList<TVertex> TopologicalSortSmallestFirst<TVertex>(this IGraph<TVertex> graph)
            where TVertex : IComparable<TVertex>
        {
            if (!graph.IsDirected)
                throw new InvalidOperationException("Graph is not directed!");

            IList<TVertex> sortedVertices;
            if (TryTopologicalSortSmallestFirst(graph, out sortedVertices))
                return sortedVertices;
            else
                throw new InvalidOperationException("Graph is cyclic!");
        }

        /// <summary>
        /// Tries to sort topologically the vertices of a graph where the smaller vertex comes first. Returns true if successful; otherwise false.
        /// </summary>
        /// <typeparam name="TVertex">The data type of the vertices. TVertex implements <see cref="IComparable{T}"/>.</typeparam>
        /// <param name="graph">The graph structure for topological sorting.</param>
        /// <param name="sortedVertices">A <see cref="IList{T}"/> of the sorted vertices.</param>
        /// <returns>Returns true if the topological sort was successful; otherwise false.</returns>
        public static bool TryTopologicalSortSmallestFirst<TVertex>(this IGraph<TVertex> graph, out IList<TVertex> sortedVertices)
            where TVertex : IComparable<TVertex>
        {
            // Using Kahn's algorithm for topological sort

            sortedVertices = null;

            if (!graph.IsDirected)
                return false;

            // List containing the sorted items
            sortedVertices = new List<TVertex>(graph.VerticesCount);
            // Build a dictionary to save all edges in the graph. In the algorithm the edges are removed
            // so it is faster building a dictionary than making a deep copy of the graph.
            var edges = new Dictionary<TVertex, HashSet<TVertex>>();
            // Dictionary with vertices that have incoming edges and their number.
            var nodesWithIncomingEdges = new Dictionary<TVertex, int>();
            // Number of edges
            int numberOfEdges = graph.EdgesCount;

            // Save edges and vertices with incoming edges
            foreach (var edge in graph.Edges)
            {
                // Save edge
                if (edges.ContainsKey(edge.Source))
                    edges[edge.Source].Add(edge.Destination);
                else
                {
                    var hSet = new HashSet<TVertex>();
                    hSet.Add(edge.Destination);
                    edges[edge.Source] = hSet;
                }
                // Save vertex with incoming edge(i.e. the destination of the edge)
                if (nodesWithIncomingEdges.ContainsKey(edge.Destination))
                    nodesWithIncomingEdges[edge.Destination]++;
                else
                    nodesWithIncomingEdges.Add(edge.Destination, 1);
            }

            // Get all nodes with no incoming edges
            var noIncomingEdgesNodes = new SortedSet<TVertex>();
            foreach (var vertex in graph.Vertices)
            {
                if (!nodesWithIncomingEdges.ContainsKey(vertex))
                    noIncomingEdgesNodes.Add(vertex);
            }

            // Until we have nodes without incoming edges
            while (noIncomingEdgesNodes.Count > 0)
            {
                var curVertex = noIncomingEdgesNodes.Min;
                noIncomingEdgesNodes.Remove(curVertex);
                sortedVertices.Add(curVertex);

                if (!edges.ContainsKey(curVertex)) continue;// if vertex don't have outgoing edges we continue

                // For every vertex that the current vertex points to
                foreach (var vertex in edges[curVertex].ToList())
                {
                    // Remove the edge
                    edges[curVertex].Remove(vertex);
                    numberOfEdges--;
                    // If the vertex has no more incoming edges we add it to the queue
                    if (--nodesWithIncomingEdges[vertex] == 0)
                        noIncomingEdgesNodes.Add(vertex);
                }
            }

            if (numberOfEdges > 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// Topological sort for the vertices of a graph where the bigger vertex comes first.
        /// </summary>
        /// <typeparam name="TVertex">The data type of the vertices. TVertex implements <see cref="IComparable{T}"/>.</typeparam>
        /// <param name="graph">The graph structure for topological sorting.</param>
        /// <returns>Returns a <see cref="IList{T}"/> of the sorted vertices.</returns>
        public static IList<TVertex> TopologicalSortBiggestFirst<TVertex>(this IGraph<TVertex> graph)
            where TVertex : IComparable<TVertex>
        {
            if (!graph.IsDirected)
                throw new InvalidOperationException("Graph is not directed!");

            IList<TVertex> sortedVertices;
            if (TryTopologicalSortBiggestFirst(graph, out sortedVertices))
                return sortedVertices;
            else
                throw new InvalidOperationException("Graph is cyclic!");
        }

        /// <summary>
        /// Tries to sort topologically the vertices of a graph where the bigger vertex comes first. Returns true if successful; otherwise false.
        /// </summary>
        /// <typeparam name="TVertex">The data type of the vertices. TVertex implements <see cref="IComparable{T}"/>.</typeparam>
        /// <param name="graph">The graph structure for topological sorting.</param>
        /// <param name="sortedVertices">A <see cref="IList{T}"/> of the sorted vertices.</param>
        /// <returns>Returns true if the topological sort was successful; otherwise false.</returns>
        public static bool TryTopologicalSortBiggestFirst<TVertex>(this IGraph<TVertex> graph, out IList<TVertex> sortedVertices)
            where TVertex : IComparable<TVertex>
        {
            // Using Kahn's algorithm for topological sort

            sortedVertices = null;

            if (!graph.IsDirected)
                return false;

            // List containing the sorted items
            sortedVertices = new List<TVertex>(graph.VerticesCount);
            // Build a dictionary to save all edges in the graph. In the algorithm the edges are removed
            // so it is faster building a dictionary than making a deep copy of the graph.
            var edges = new Dictionary<TVertex, HashSet<TVertex>>();
            // Dictionary with vertices that have incoming edges and their number.
            var nodesWithIncomingEdges = new Dictionary<TVertex, int>();
            // Number of edges
            int numberOfEdges = graph.EdgesCount;

            // Save edges and vertices with incoming edges
            foreach (var edge in graph.Edges)
            {
                // Save edge
                if (edges.ContainsKey(edge.Source))
                    edges[edge.Source].Add(edge.Destination);
                else
                {
                    var hSet = new HashSet<TVertex>();
                    hSet.Add(edge.Destination);
                    edges[edge.Source] = hSet;
                }
                // Save vertex with incoming edge(i.e. the destination of the edge)
                if (nodesWithIncomingEdges.ContainsKey(edge.Destination))
                    nodesWithIncomingEdges[edge.Destination]++;
                else
                    nodesWithIncomingEdges.Add(edge.Destination, 1);
            }

            // Get all nodes with no incoming edges
            var noIncomingEdgesNodes = new SortedSet<TVertex>();
            foreach (var vertex in graph.Vertices)
            {
                if (!nodesWithIncomingEdges.ContainsKey(vertex))
                    noIncomingEdgesNodes.Add(vertex);
            }

            // Until we have nodes without incoming edges
            while (noIncomingEdgesNodes.Count > 0)
            {
                var curVertex = noIncomingEdgesNodes.Max;
                noIncomingEdgesNodes.Remove(curVertex);
                sortedVertices.Add(curVertex);

                if (!edges.ContainsKey(curVertex)) continue;// if vertex don't have outgoing edges we continue

                // For every vertex that the current vertex points to
                foreach (var vertex in edges[curVertex].ToList())
                {
                    // Remove the edge
                    edges[curVertex].Remove(vertex);
                    numberOfEdges--;
                    // If the vertex has no more incoming edges we add it to the queue
                    if (--nodesWithIncomingEdges[vertex] == 0)
                        noIncomingEdgesNodes.Add(vertex);
                }
            }

            if (numberOfEdges > 0)
                return false;
            else
                return true;
        }
    }
}
