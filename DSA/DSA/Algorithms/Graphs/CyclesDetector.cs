using DSA.DataStructures.Interfaces;
using System;
using System.Collections.Generic;

namespace DSA.Algorithms.Graphs
{
    /// <summary>
    /// A static class containing extension methods for graph cycle detection using DFS.
    /// </summary>
    public static class CyclesDetector
    {
        /// <summary>
        /// Determines whether the graph is cyclic or not.
        /// </summary>
        /// <typeparam name="TVertex">The data type of the vertices. TVertex implements <see cref="IComparable{T}"/>.</typeparam>
        /// <param name="graph">The graph structure that implements <see cref="IGraph{TVertex}"/> for checking if cyclic.</param>
        /// <returns>Returns true if a cycle is detected; otherwise false.</returns>
        public static bool IsCyclic<TVertex>(this IGraph<TVertex> graph)
            where TVertex : IComparable<TVertex>
        {
            // Hash set for all visited vertices
            var visited = new HashSet<TVertex>();
            // Hash set for visited vertices on the current DFS iteration
            var recursionStack = new HashSet<TVertex>();

            if (graph.IsDirected)
            {
                foreach (var vertex in graph.Vertices)
                {
                    if (IsDirectedCyclic(graph, vertex, visited, recursionStack))
                        return true;
                }
            }
            else
            {
                foreach (var vertex in graph.Vertices)
                {
                    if (IsUndirectedCyclic(graph, vertex, default(TVertex), false, visited))
                        return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Recursive DFS check for cycles in a directed graph.
        /// </summary>
        /// <typeparam name="TVertex">The data type of the vertices. TVertex implements <see cref="IComparable{T}"/>.</typeparam>
        /// <param name="graph">The graph structure that implements <see cref="IGraph{TVertex}"/> for checking if cyclic.</param>
        /// <param name="source">The source of the DFS or current vertex of the iteration.</param>
        /// <param name="visited">A <see cref="HashSet{T}"/> of the visited vertices. T being of type TVertex.</param>
        /// <param name="recursionStack">A <see cref="HashSet{T}"/> representing a recursion stack of the visited vertices on the current iteration. T being of type TVertex.</param>
        /// <returns>Returns true if a cycle is detected; otherwise false.</returns>
        private static bool IsDirectedCyclic<TVertex>(IGraph<TVertex> graph, TVertex source, HashSet<TVertex> visited, HashSet<TVertex> recursionStack)
            where TVertex : IComparable<TVertex>
        {
            // Check if the vertex was already visited
            if (!visited.Contains(source))
            {
                // Mark the current vertex as visited and add it to the recursion stack
                visited.Add(source);
                recursionStack.Add(source);

                // Recur for adjacent vertices
                foreach (var edge in graph.OutgoingEdges(source))
                {
                    // If vertex is not visited
                    if (!visited.Contains(edge.Destination))
                    {
                        // We check the adjacent for cycles
                        if (IsDirectedCyclic(graph, edge.Destination, visited, recursionStack))
                            return true;
                    }
                    else // If vertex is already visited
                    {
                        // If it is contained in the recursion stack we have a cycle
                        if (recursionStack.Contains(edge.Destination))
                            return true;
                    }
                }
            }

            // Remove the source vertex from the recursion stack
            recursionStack.Remove(source);
            return false;
        }

        /// <summary>
        /// Recursive DFS check for cycles in an undirected graph.
        /// </summary>
        /// <typeparam name="TVertex">The data type of the vertices. TVertex implements <see cref="IComparable{T}"/>.</typeparam>
        /// <param name="graph">The graph structure that implements <see cref="IGraph{TVertex}"/> for checking if cyclic.</param>
        /// <param name="source">The source of the DFS or current vertex of the iteration.</param>
        /// <param name="parent">The parent of the current vertex.</param>
        /// <param name="HasParent">Determines if the current vertex has a parent. True for all except source.</param>
        /// <param name="visited">A <see cref="HashSet{T}"/> of the visited vertices. T being of type TVertex.</param>
        /// <returns>Returns true if a cycle is detected; otherwise false.</returns>
        private static bool IsUndirectedCyclic<TVertex>(IGraph<TVertex> graph, TVertex source, TVertex parent, bool HasParent, HashSet<TVertex> visited)
            where TVertex : IComparable<TVertex>
        {
            // Check if the vertex was already visited
            if (!visited.Contains(source))
            {
                // Mark the current vertex as visited
                visited.Add(source);

                // Recur for adjacent vertices
                foreach (var edge in graph.OutgoingEdges(source))
                {
                    // If vertex is not visited
                    if (!visited.Contains(edge.Destination))
                    {
                        // We check the adjacent for cycles
                        if (IsUndirectedCyclic(graph, edge.Destination, source, true, visited))
                            return true;
                    }
                    else // If vertex is already visited
                    {
                        // If source vertex has a parent in the DFS tree
                        if (HasParent)
                        {
                            // If the adjacent vertex is not the parent of the source vertex
                            // we have a cycle
                            if (!object.Equals(parent, edge.Destination))
                                return true;
                        }
                    }
                }
            }

            return false;
        }
    }
}
