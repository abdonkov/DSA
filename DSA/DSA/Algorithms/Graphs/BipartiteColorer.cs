using DSA.DataStructures.Interfaces;
using System;
using System.Collections.Generic;

namespace DSA.Algorithms.Graphs
{
    /// <summary>
    /// Bipartite colors enum. Representing Red and Blue color.
    /// </summary>
    public enum BipartiteColor
    {
        /// <summary>
        /// Red color for the bipartite coloring.
        /// </summary>
        Red = 0,
        /// <summary>
        /// Blue color for the bipartite coloring.
        /// </summary>
        Blue = 1
    }

    /// <summary>
    /// A static class providing methods to check for bipartite coloring in a graph.
    /// </summary>
    public static class BipartiteColorer
    {
        /// <summary>
        /// Performs a bipartite coloring on the specified graph. Returns a <see cref="Dictionary{TKey, TValue}"/> of vertices as keys and their colors as values.
        /// </summary>
        /// <typeparam name="TVertex">The data type of the vertices. TVertex implements <see cref="IComparable{T}"/>.</typeparam>
        /// <param name="graph">The graph structure for bipartite coloring.</param>
        /// <returns>Returns a <see cref="Dictionary{TKey, TValue}"/> of vertices as keys and their colors as values.</returns>
        public static Dictionary<TVertex, BipartiteColor> BipartiteColoring<TVertex>(this IGraph<TVertex> graph)
            where TVertex : IComparable<TVertex>
        {
            Dictionary<TVertex, BipartiteColor> coloredVertices;
            if (TryGetBipartiteColoring(graph, out coloredVertices))
                return coloredVertices;
            else
                throw new InvalidOperationException("Graph is not bipartite!");
        }

        /// <summary>
        /// Tries to perform bipartite coloring on the specified graph. Returns true if successful; otherwise false.
        /// </summary>
        /// <typeparam name="TVertex">The data type of the vertices. TVertex implements <see cref="IComparable{T}"/>.</typeparam>
        /// <param name="graph">The graph structure for bipartite coloring.</param>
        /// <param name="coloredVertices">A <see cref="Dictionary{TKey, TValue}"/> of vertices as keys and their colors as values where the result of the coloring is saved.</param>
        /// <returns>Returns true if the bipartite coloring was successful; otherwise false.</returns>
        public static bool TryGetBipartiteColoring<TVertex>(this IGraph<TVertex> graph, out Dictionary<TVertex, BipartiteColor> coloredVertices)
            where TVertex : IComparable<TVertex>
        {
            // Saving the color for each vertex
            coloredVertices = new Dictionary<TVertex, BipartiteColor>();

            foreach (var vertex in graph.Vertices)
            {
                BipartiteColor vertexColor;

                // if the vertex is not colored, we color it
                if (!coloredVertices.ContainsKey(vertex))
                {
                    // Check if there is a colored vertex on a level of the BFS
                    BipartiteColor BFSVertexColor;
                    int level = BFSFindFirstLevelWithColoredVertex(graph, vertex, coloredVertices, out BFSVertexColor);

                    // Get this vertex color
                    if (level != -1)// if we have a colored vertex in the BFS
                    {
                        vertexColor = level % 2 == 0 ? // if the level is even
                                        BFSVertexColor// vertex color is the same as the found color
                                        : BFSVertexColor == BipartiteColor.Red ? // else its the opposite
                                                            BipartiteColor.Blue
                                                            : BipartiteColor.Red;
                    }// if we don't have colored vertex in the BFS
                    else vertexColor = BipartiteColor.Red;// we set the color to red

                    // Color current vertex
                    coloredVertices.Add(vertex, vertexColor);
                }
                else vertexColor = coloredVertices[vertex];

                // Here we know the current vertex is colored for sure.
                // Color the vertices in the BFS if not colored and check for appropriate coloring
                var queue = new Queue<KeyValuePair<TVertex, BipartiteColor>>(graph.VerticesCount);
                var visited = new HashSet<TVertex>();

                queue.Enqueue(new KeyValuePair<TVertex, BipartiteColor>(vertex, vertexColor));
                visited.Add(vertex);

                while (queue.Count > 0)
                {
                    var curVertex = queue.Dequeue();
                    // Neighbors color is the opposite of this vertex color
                    BipartiteColor neighborsColor = curVertex.Value == BipartiteColor.Red ?
                                                                        BipartiteColor.Blue
                                                                        : BipartiteColor.Red;

                    // Check adjacent vertices colors and color them if not colored
                    foreach (var edge in graph.OutgoingEdges(curVertex.Key))
                    {
                        var neighbor = edge.Destination;
                        // Check if neighbor vertex has a color and if it is correct
                        if (coloredVertices.ContainsKey(neighbor))
                        {
                            if (coloredVertices[neighbor] != neighborsColor)
                                return false;
                        }
                        else// if the vertex does not have a color, we color it
                        {
                            coloredVertices.Add(neighbor, neighborsColor);
                        }
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Returns on which level of the BFS a colored vertex is found. -1 if no such vertex is found. Saves the color in the out parameter.
        /// </summary>
        /// <typeparam name="TVertex">The data type of the vertices. TVertex implements <see cref="IComparable{T}"/>.</typeparam>
        /// <param name="graph">The graph structure for the BFS color check.</param>
        /// <param name="source">The source vertex at which the BFS starts.</param>
        /// <param name="coloredVertices">A dictionary containing the colored vertices and their color./></param>
        /// <param name="foundColor">The color of the found colored vertex.</param>
        /// <returns>Retruns the level on which the colored vertex is found. Returns -1 if the was no colored vertex found.</returns>
        private static int BFSFindFirstLevelWithColoredVertex<TVertex>(IGraph<TVertex> graph, TVertex source, Dictionary<TVertex, BipartiteColor> coloredVertices, out BipartiteColor foundColor)
            where TVertex : IComparable<TVertex>
        {
            foundColor = BipartiteColor.Red;

            // Queue used for BFS. Saves the vertex as key and its level as value.
            var queue = new Queue<KeyValuePair<TVertex, int>>(graph.VerticesCount);
            var visited = new HashSet<TVertex>();

            queue.Enqueue(new KeyValuePair<TVertex, int>(source, 0));
            visited.Add(source);

            while (queue.Count > 0)
            {
                var curVertex = queue.Dequeue();

                // Check if current vertex has a color and end the BFS
                if (coloredVertices.ContainsKey(curVertex.Key))
                {
                    foundColor = coloredVertices[curVertex.Key];// save color
                    return curVertex.Value;// return the vertex level
                }

                foreach (var edge in graph.OutgoingEdges(curVertex.Key))
                {
                    if (!visited.Contains(edge.Destination))
                    {
                        queue.Enqueue(new KeyValuePair<TVertex, int>(edge.Destination, curVertex.Value + 1));
                        visited.Add(edge.Destination);
                    }
                }
            }

            // If a colored vertex is not found we return -1
            return -1;
        }
    }
}
