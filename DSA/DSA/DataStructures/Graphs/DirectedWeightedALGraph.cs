using DSA.Algorithms.Sorting;
using DSA.DataStructures.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DSA.DataStructures.Graphs
{
    /// <summary>
    /// Represents a directed and weighted adjacency list graph with optimized amortized O(1) edge lookup and removal.
    /// </summary>
    /// <typeparam name="TVertex">The data type of the vertices. TVertex implements <see cref="IComparable{T}"/>.</typeparam>
    /// <typeparam name="TWeight">The data type of the weight of the edges. TWeight implements <see cref="IComparable{T}"/>.</typeparam>
    public class DirectedWeightedALGraph<TVertex, TWeight> : IWeightedGraph<TVertex, TWeight>
        where TVertex : IComparable<TVertex>
        where TWeight : IComparable<TWeight>
    {
        /// <summary>
        /// Represents the connections between each vertex and its neighbours.
        /// </summary>
        internal Dictionary<TVertex, Dictionary<TVertex, TWeight>> adjacencyList;

        /// <summary>
        /// Determines whether the <see cref="DirectedWeightedALGraph{TVertex, TWeight}"/> is directed.
        /// </summary>
        public bool IsDirected { get { return true; } }

        /// <summary>
        /// Deteremines whether the <see cref="DirectedWeightedALGraph{TVertex, TWeight}"/> is weighted.
        /// </summary>
        public bool IsWeighted { get { return true; } }

        /// <summary>
        /// Gets the number of edges in the <see cref="DirectedWeightedALGraph{TVertex, TWeight}"/>.
        /// </summary>
        public int EdgesCount { get; internal set; }

        /// <summary>
        /// Gets the number of vertices in the <see cref="DirectedWeightedALGraph{TVertex, TWeight}"/>.
        /// </summary>
        public int VerticesCount { get; internal set; }

        /// <summary>
        /// Gets the vertices in the <see cref="DirectedWeightedALGraph{TVertex, TWeight}"/>.
        /// </summary>
        public IEnumerable<TVertex> Vertices
        {
            get
            {
                List<TVertex> vertices = new List<TVertex>(VerticesCount);
                foreach (var vertex in adjacencyList.Keys)
                {
                    vertices.Add(vertex);
                }
                return vertices;
            }
        }

        /// <summary>
        /// Gets the vertices in the <see cref="DirectedWeightedALGraph{TVertex, TWeight}"/> in sorted ascending order.
        /// </summary>
        public IEnumerable<TVertex> VerticesSorted
        {
            get
            {
                List<TVertex> vertices = new List<TVertex>(VerticesCount);
                foreach (var vertex in adjacencyList.Keys)
                {
                    vertices.Add(vertex);
                }

                if (vertices.Count > 0)
                    vertices.QuickSort();

                return vertices;
            }
        }

        /// <summary>
        /// Gets the edges in the <see cref="DirectedWeightedALGraph{TVertex, TWeight}"/>.
        /// </summary>
        public IEnumerable<WeightedEdge<TVertex, TWeight>> Edges
        {
            get
            {
                foreach (var kvp in adjacencyList)
                {
                    foreach (var adjacent in kvp.Value)
                    {
                        yield return new WeightedEdge<TVertex, TWeight>(kvp.Key, adjacent.Key, adjacent.Value);
                    }
                }
            }
        }

        /// <summary>
        /// Creates a new instance of the <see cref="DirectedWeightedALGraph{TVertex, TWeight}"/>.
        /// </summary>
        public DirectedWeightedALGraph()
        {
            adjacencyList = new Dictionary<TVertex, Dictionary<TVertex, TWeight>>();
            EdgesCount = 0;
            VerticesCount = 0;
        }

        /// <summary>
        /// Adds an edge defined by the given vertices to the <see cref="DirectedWeightedALGraph{TVertex, TWeight}"/> with weight being the TWeight default value of the <see cref="DirectedWeightedALGraph{TVertex, TWeight}"/>. If the vertices are not present in the graph they will be added.
        /// </summary>
        /// <param name="source">The source vertex of the edge.</param>
        /// <param name="destination">The destination vertex of the edge.</param>
        /// <returns>Returns true if the edge was added successfully; otherwise false. Also returns false if edge already exists.</returns>
        public bool AddEdge(TVertex source, TVertex destination)
        {
            return AddEdge(source, destination, default(TWeight));
        }

        /// <summary>
        /// Adds an edge defined by the given vertices to the <see cref="DirectedWeightedALGraph{TVertex, TWeight}"/> with the given weight. If the vertices are not present in the graph they will be added.
        /// </summary>
        /// <param name="source">The source vertex of the edge.</param>
        /// <param name="destination">The destination vertex of the edge.</param>
        /// <param name="weight">The weight of the edge.</param>
        /// <returns>Returns true if the edge was added successfully; otherwise false. Also returns false if the edge already exists.</returns>
        public bool AddEdge(TVertex source, TVertex destination, TWeight weight)
        {
            if (object.Equals(source, destination)) return false;

            // Add first vertex if it is not in the graph
            if (!AddVertex(source))
                if (adjacencyList[source].ContainsKey(destination))// if the vertices are connected
                    return false;// we return false

            // Here the firstVertex is in the graph, so we connect it with the second
            adjacencyList[source].Add(destination, weight);

            EdgesCount++;
            return true;
        }

        /// <summary>
        /// Adds a vertex to the <see cref="DirectedWeightedALGraph{TVertex, TWeight}"/>.
        /// </summary>
        /// <param name="vertex">The vertex to add.</param>
        /// <returns>Returns true if the edge was added successfully; otherwise false. Also returns false if the vertex already exists.</returns>
        public bool AddVertex(TVertex vertex)
        {
            if (adjacencyList.ContainsKey(vertex)) return false;

            adjacencyList.Add(vertex, new Dictionary<TVertex, TWeight>());
            VerticesCount++;

            return true;
        }

        /// <summary>
        /// Adds the specified collection of vertices to the <see cref="DirectedWeightedALGraph{TVertex, TWeight}"/>. If some of the vertices are already in the graph exception is not thrown.
        /// </summary>
        /// <param name="vertices">Adds the <see cref="IEnumerable{T}"/> of vertices to the graph.</param>
        public void AddVertices(IEnumerable<TVertex> vertices)
        {
            foreach (var vertex in vertices)
            {
                AddVertex(vertex);
            }
        }

        /// <summary>
        /// Returns the incoming edges of the given vertex.
        /// </summary>
        /// <param name="vertex">The vertex whose incoming edges are returned.</param>
        /// <returns>Returns a <see cref="IEnumerable{T}"/> of <see cref="WeightedEdge{TVertex, TWeight}"/> of all incoming edges of the given vertex.</returns>
        public IEnumerable<WeightedEdge<TVertex, TWeight>> IncomingEdges(TVertex vertex)
        {
            if (!adjacencyList.ContainsKey(vertex)) throw new KeyNotFoundException("Vertex does not belong to the graph!");

            var adjacent = adjacencyList
                                .Where(x => x.Value.ContainsKey(vertex))
                                .Select(x => new KeyValuePair<TVertex, TWeight>(x.Key, x.Value[vertex]))
                                .ToList();

            if (adjacent.Count > 0)
            {
                for (int i = 0; i < adjacent.Count; i++)
                {
                    yield return new WeightedEdge<TVertex, TWeight>(adjacent[i].Key, vertex, adjacent[i].Value);
                }
            }
        }

        /// <summary>
        /// Returns the outgoing edges of the given vertex.
        /// </summary>
        /// <param name="vertex">The vertex whose outgoing edges are returned.</param>
        /// <returns>Returns a <see cref="IEnumerable{T}"/> of <see cref="WeightedEdge{TVertex, TWeight}"/> of all outgoing edges of the given vertex.</returns>
        public IEnumerable<WeightedEdge<TVertex, TWeight>> OutgoingEdges(TVertex vertex)
        {
            if (!adjacencyList.ContainsKey(vertex)) throw new KeyNotFoundException("Vertex does not belong to the graph!");

            var adjacent = adjacencyList[vertex].ToList();

            if (adjacent.Count > 0)
            {
                for (int i = 0; i < adjacent.Count; i++)
                {
                    yield return new WeightedEdge<TVertex, TWeight>(vertex, adjacent[i].Key, adjacent[i].Value);
                }
            }
        }

        /// <summary>
        /// Returns the incoming edges of the given vertex sorted by their source vertex.
        /// </summary>
        /// <param name="vertex">The vertex whose incoming edges are returned.</param>
        /// <returns>Returns a <see cref="IEnumerable{T}"/> of <see cref="WeightedEdge{TVertex, TWeight}"/> of all incoming edges of the given vertex.</returns>
        public IEnumerable<WeightedEdge<TVertex, TWeight>> IncomingEdgesSorted(TVertex vertex)
        {
            if (!adjacencyList.ContainsKey(vertex)) throw new KeyNotFoundException("Vertex does not belong to the graph!");

            var kvpComparer = Comparer<KeyValuePair<TVertex, TWeight>>.Create((x, y) => x.Key.CompareTo(y.Key));

            var adjacent = adjacencyList
                                .Where(x => x.Value.ContainsKey(vertex))
                                .Select(x => new KeyValuePair<TVertex, TWeight>(x.Key, x.Value[vertex]))
                                .ToList();

            if (adjacent.Count > 0)
            {
                adjacent.QuickSort(kvpComparer);
                for (int i = 0; i < adjacent.Count; i++)
                {
                    yield return new WeightedEdge<TVertex, TWeight>(adjacent[i].Key, vertex, adjacent[i].Value);
                }
            }
        }

        /// <summary>
        /// Returns the outgoing edges of the given vertex sorted by their destination vertex.
        /// </summary>
        /// <param name="vertex">The vertex whose outgoing edges are returned.</param>
        /// <returns>Returns a <see cref="IEnumerable{T}"/> of <see cref="WeightedEdge{TVertex, TWeight}"/> of all outgoing edges of the given vertex.</returns>
        public IEnumerable<WeightedEdge<TVertex, TWeight>> OutgoingEdgesSorted(TVertex vertex)
        {
            if (!adjacencyList.ContainsKey(vertex)) throw new KeyNotFoundException("Vertex does not belong to the graph!");

            var kvpComparer = Comparer<KeyValuePair<TVertex, TWeight>>.Create((x, y) => x.Key.CompareTo(y.Key));

            var adjacent = adjacencyList[vertex].ToList();

            if (adjacent.Count > 0)
            {
                adjacent.QuickSort(kvpComparer);
                for (int i = 0; i < adjacent.Count; i++)
                {
                    yield return new WeightedEdge<TVertex, TWeight>(vertex, adjacent[i].Key, adjacent[i].Value);
                }
            }
        }

        /// <summary>
        /// Determines whether the edge is presented in the <see cref="DirectedWeightedALGraph{TVertex, TWeight}"/>.
        /// </summary>
        /// <param name="source">The source vertex of the edge.</param>
        /// <param name="destination">The destination vertex of the edge.</param>
        /// <returns>Returns true if the edge is presented in the <see cref="DirectedWeightedALGraph{TVertex, TWeight}"/>; false otherwise.</returns>
        public bool ContainsEdge(TVertex source, TVertex destination)
        {
            if (!adjacencyList.ContainsKey(source)) return false;

            return adjacencyList[source].ContainsKey(destination);
        }

        /// <summary>
        /// Determines whether the vertex is presented in the <see cref="DirectedWeightedALGraph{TVertex, TWeight}"/>.
        /// </summary>
        /// <param name="vertex">The vertex to see if presented in the <see cref="DirectedWeightedALGraph{TVertex, TWeight}"/>.</param>
        /// <returns>Returns true if the vertex is presented in the <see cref="DirectedWeightedALGraph{TVertex, TWeight}"/>; false otherwise.</returns>
        public bool ContainsVertex(TVertex vertex)
        {
            return adjacencyList.ContainsKey(vertex);
        }

        /// <summary>
        /// Updates the weight of the edge defined by the given vertices in the <see cref="DirectedWeightedALGraph{TVertex, TWeight}"/>.
        /// </summary>
        /// <param name="source">The source vertex of the edge.</param>
        /// <param name="destination">The destination vertex of the edge.</param>
        /// <param name="weight">The new weight of the edge.</param>
        /// <returns>Returns true if the edge weight was updated successfully; otherwise, false. Also returns false if the vertices are not present in this <see cref="WeightedALGraph{TVertex, TWeight}"/>.</returns>
        public bool UpdateEdgeWeight(TVertex source, TVertex destination, TWeight weight)
        {
            if (!adjacencyList[source].ContainsKey(destination)) return false;

            adjacencyList[source][destination] = weight;
            return true;
        }

        /// <summary>
        /// Gets the weight of the edge defined by the given vertices in the <see cref="DirectedWeightedALGraph{TVertex, TWeight}"/>.
        /// </summary>
        /// <param name="source">The source vertex of the edge.</param>
        /// <param name="destination">The destination vertex of the edge.</param>
        /// <param name="weight">Contains the weight of the edge, if the edge is presented in the graph; otherwise, contains the default value for the type of the weight parameter.</param>
        /// <returns>Returns true if the graph contains the specified edge; otherwise, false.</returns>
        public bool TryGetEdgeWeight(TVertex source, TVertex destination, out TWeight weight)
        {
            weight = default(TWeight);

            if (!adjacencyList[source].ContainsKey(destination)) return false;

            weight = adjacencyList[source][destination];
            return true;
        }

        /// <summary>
        /// Removes the edge defined by the given vertices from the <see cref="DirectedWeightedALGraph{TVertex, TWeight}"/>.
        /// </summary>
        /// <param name="source">The source vertex of the edge.</param>
        /// <param name="destination">The destination vertex of the edge.</param>
        /// <returns>Returns true if the edge was removed successfully; otherwise false. Also returns false if the vertices are not present in this graph or the edge does not exist.</returns>
        public bool RemoveEdge(TVertex source, TVertex destination)
        {
            if (!adjacencyList.ContainsKey(source)) return false;
            if (!adjacencyList[source].ContainsKey(destination)) return false;

            adjacencyList[source].Remove(destination);

            EdgesCount--;
            return true;
        }

        /// <summary>
        /// Removes the given vertex from the <see cref="DirectedWeightedALGraph{TVertex, TWeight}"/>.
        /// </summary>
        /// <param name="vertex">The vertex to remove.</param>
        /// <returns>Returns true if the vertex was removed successfully; otherwise false. Also returns false if the vertex does not exist.</returns>
        public bool RemoveVertex(TVertex vertex)
        {
            if (!adjacencyList.ContainsKey(vertex)) return false;

            // Remove incoming edges
            foreach (var kvp in adjacencyList)
            {
                if (kvp.Value.Remove(vertex))
                    EdgesCount--;
            }

            // Remove vertex
            EdgesCount -= adjacencyList[vertex].Count;
            adjacencyList.Remove(vertex);

            VerticesCount--;
            return true;
        }

        /// <summary>
        /// Returns the degree of the given vertex presented in the <see cref="DirectedWeightedALGraph{TVertex, TWeight}"/>.
        /// </summary>
        /// <param name="vertex">The vertex to calculate its degeree.</param>
        /// <returns>Returns the degree of the given vertex.</returns>
        public int Degree(TVertex vertex)
        {
            if (!ContainsVertex(vertex)) throw new KeyNotFoundException("Vertex does not belong to the graph!");

            // outgoing edges
            int degree = adjacencyList[vertex].Count;
            // incoming edges
            foreach (var kvp in adjacencyList)
            {
                if (kvp.Value.ContainsKey(vertex))
                    degree++;
            }

            return degree;
        }

        /// <summary>
        /// Removes all edges and vertices from the <see cref="DirectedWeightedALGraph{TVertex, TWeight}"/>.
        /// </summary>
        public void Clear()
        {
            adjacencyList.Clear();
            EdgesCount = 0;
            VerticesCount = 0;
        }

        /// <summary>
        /// Breadth-first search of the <see cref="DirectedWeightedALGraph{TVertex, TWeight}"/> with sorted levels. Returns <see cref="IEnumerable{T}"/> of the vertices.
        /// </summary>
        /// <param name="vertex">The vertex from which the breadth-first search starts.</param>
        /// <returns>Returns <see cref="IEnumerable{T}"/> of the vertices.</returns>
        public IEnumerable<TVertex> BreadthFirstSearch(TVertex vertex)
        {
            if (!adjacencyList.ContainsKey(vertex)) throw new KeyNotFoundException("Vertex does not belong to the graph!");

            var queue = new Queue<TVertex>(VerticesCount);
            var visited = new HashSet<TVertex>();
            TVertex[] sortedLevel = new TVertex[adjacencyList.Values.Max(x => x?.Count ?? 0)];

            queue.Enqueue(vertex);
            visited.Add(vertex);

            while (queue.Count > 0)
            {
                TVertex curVertex = queue.Dequeue();

                yield return curVertex;

                int sCount = 0;

                foreach (var kvp in adjacencyList[curVertex])
                {
                    if (!visited.Contains(kvp.Key))
                    {
                        sortedLevel[sCount++] = kvp.Key;
                    }
                }

                if (sCount > 0)
                {
                    sortedLevel.QuickSort(0, sCount, null);
                    for (int i = 0; i < sCount; i++)
                    {
                        queue.Enqueue(sortedLevel[i]);
                        visited.Add(sortedLevel[i]);
                    }
                }
            }
        }

        /// <summary>
        /// Breadth-first search of the <see cref="DirectedWeightedALGraph{TVertex, TWeight}"/> with sorted levels. Returns <see cref="IEnumerable{T}"/> of <see cref="WeightedEdge{TVertex, TWeight}"/> representing the edges of the graph.
        /// </summary>
        /// <param name="vertex">The vertex from which the breadth-first search starts.</param>
        /// <returns>.Returns <see cref="IEnumerable{T}"/> of <see cref="WeightedEdge{TVertex, TWeight}"/> representing the edges of the graph.</returns>
        public IEnumerable<WeightedEdge<TVertex, TWeight>> BreadthFirstSearchEdges(TVertex vertex)
        {
            if (!adjacencyList.ContainsKey(vertex)) throw new KeyNotFoundException("Vertex does not belong to the graph!");

            var queue = new Queue<TVertex>(VerticesCount);
            var visited = new HashSet<TVertex>();
            var sortedLevel = new KeyValuePair<TVertex, TWeight>[adjacencyList.Values.Max(x => x?.Count ?? 0)];
            var kvpComparer = Comparer<KeyValuePair<TVertex, TWeight>>.Create((x, y) => x.Key.CompareTo(y.Key));

            queue.Enqueue(vertex);
            visited.Add(vertex);

            while (queue.Count > 0)
            {
                TVertex curVertex = queue.Dequeue();

                int sCount = 0;

                foreach (var kvp in adjacencyList[curVertex])
                {
                    if (!visited.Contains(kvp.Key))
                    {
                        sortedLevel[sCount++] = kvp;
                    }
                }

                if (sCount > 0)
                {
                    sortedLevel.QuickSort(0, sCount, kvpComparer);
                    for (int i = 0; i < sCount; i++)
                    {
                        queue.Enqueue(sortedLevel[i].Key);
                        visited.Add(sortedLevel[i].Key);

                        yield return new WeightedEdge<TVertex, TWeight>(curVertex, sortedLevel[i].Key, sortedLevel[i].Value);
                    }
                }
            }
        }

        /// <summary>
        /// Depth-first search of the <see cref="DirectedWeightedALGraph{TVertex, TWeight}"/> with sorted levels. Returns <see cref="IEnumerable{T}"/> of the vertices.
        /// </summary>
        /// <param name="vertex">The vertex from which the depth-first search starts.</param>
        /// <returns>Returns <see cref="IEnumerable{T}"/> of the vertices.</returns>
        public IEnumerable<TVertex> DepthFirstSearch(TVertex vertex)
        {
            if (!adjacencyList.ContainsKey(vertex)) throw new KeyNotFoundException("Vertex does not belong to the graph!");

            var stack = new Stack<TVertex>(VerticesCount);
            var visited = new HashSet<TVertex>();
            TVertex[] sortedLevel = new TVertex[adjacencyList.Values.Max(x => x?.Count ?? 0)];

            stack.Push(vertex);

            while (stack.Count > 0)
            {
                TVertex curVertex = stack.Pop();

                if (!visited.Contains(curVertex))
                {
                    yield return curVertex;

                    visited.Add(curVertex);

                    int sCount = 0;

                    foreach (var kvp in adjacencyList[curVertex])
                    {
                        if (!visited.Contains(kvp.Key))
                        {
                            sortedLevel[sCount++] = kvp.Key;
                        }
                    }

                    if (sCount > 0)
                    {
                        sortedLevel.QuickSortDescending(0, sCount, null);// descending sort because we add them in stack
                        for (int i = 0; i < sCount; i++)
                        {
                            stack.Push(sortedLevel[i]);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Depth-first search of the <see cref="DirectedWeightedALGraph{TVertex, TWeight}"/> with sorted levels. Returns <see cref="IEnumerable{T}"/> of <see cref="WeightedEdge{TVertex, TWeight}"/> representing the edges of the graph.
        /// </summary>
        /// <param name="vertex">The vertex from which the depth-first search starts.</param>
        /// <returns>.Returns <see cref="IEnumerable{T}"/> of <see cref="WeightedEdge{TVertex, TWeight}"/> representing the edges of the graph.</returns>
        public IEnumerable<WeightedEdge<TVertex, TWeight>> DepthFirstSearchEdges(TVertex vertex)
        {
            if (!adjacencyList.ContainsKey(vertex)) throw new KeyNotFoundException("Vertex does not belong to the graph!");

            var stackSource = new Stack<TVertex>(VerticesCount);
            var stackDestination = new Stack<TVertex>(VerticesCount);
            var stackWeight = new Stack<TWeight>(VerticesCount);
            var visited = new HashSet<TVertex>();
            var sortedLevel = new KeyValuePair<TVertex, TWeight>[adjacencyList.Values.Max(x => x?.Count ?? 0)];
            var kvpComparer = Comparer<KeyValuePair<TVertex, TWeight>>.Create((x, y) => x.Key.CompareTo(y.Key));
            int sCount = 0;

            // Add vertex neighbours to stack
            foreach (var kvp in adjacencyList[vertex])
            {
                sortedLevel[sCount++] = kvp;
            }

            if (sCount > 0)
            {
                sortedLevel.QuickSortDescending(0, sCount, kvpComparer);// descending sort beacause we add them in stack
                for (int i = 0; i < sCount; i++)
                {
                    stackSource.Push(vertex);
                    stackDestination.Push(sortedLevel[i].Key);
                    stackWeight.Push(sortedLevel[i].Value);
                }
            }

            visited.Add(vertex);

            while (stackDestination.Count > 0)
            {
                TVertex curSourceVertex = stackSource.Pop();
                TVertex curDestinationVertex = stackDestination.Pop();
                TWeight curWeight = stackWeight.Pop();

                //visited.Add(curVertex);
                if (!visited.Contains(curDestinationVertex))
                {
                    yield return new WeightedEdge<TVertex, TWeight>(curSourceVertex, curDestinationVertex, curWeight);

                    visited.Add(curDestinationVertex);

                    sCount = 0;

                    foreach (var kvp in adjacencyList[curDestinationVertex])
                    {
                        if (!visited.Contains(kvp.Key))
                        {
                            sortedLevel[sCount++] = kvp;
                        }
                    }

                    if (sCount > 0)
                    {
                        sortedLevel.QuickSortDescending(0, sCount, kvpComparer);// descending sort beacause we add them in stack
                        for (int i = 0; i < sCount; i++)
                        {
                            stackSource.Push(curDestinationVertex);
                            stackDestination.Push(sortedLevel[i].Key);
                            stackWeight.Push(sortedLevel[i].Value);
                        }
                    }
                }
            }
        }

        IEnumerable<IEdge<TVertex>> IGraph<TVertex>.Edges { get { return Edges; } }

        IEnumerable<IWeightedEdge<TVertex, TWeight>> IWeightedGraph<TVertex, TWeight>.Edges { get { return Edges; } }

        IEnumerable<IEdge<TVertex>> IGraph<TVertex>.IncomingEdges(TVertex vertex)
        {
            return IncomingEdges(vertex);
        }

        IEnumerable<IEdge<TVertex>> IGraph<TVertex>.OutgoingEdges(TVertex vertex)
        {
            return OutgoingEdges(vertex);
        }

        IEnumerable<IEdge<TVertex>> IGraph<TVertex>.IncomingEdgesSorted(TVertex vertex)
        {
            return IncomingEdgesSorted(vertex);
        }

        IEnumerable<IEdge<TVertex>> IGraph<TVertex>.OutgoingEdgesSorted(TVertex vertex)
        {
            return OutgoingEdgesSorted(vertex);
        }

        IEnumerable<IWeightedEdge<TVertex, TWeight>> IWeightedGraph<TVertex, TWeight>.IncomingEdges(TVertex vertex)
        {
            return IncomingEdges(vertex);
        }

        IEnumerable<IWeightedEdge<TVertex, TWeight>> IWeightedGraph<TVertex, TWeight>.OutgoingEdges(TVertex vertex)
        {
            return OutgoingEdges(vertex);
        }

        IEnumerable<IWeightedEdge<TVertex, TWeight>> IWeightedGraph<TVertex, TWeight>.IncomingEdgesSorted(TVertex vertex)
        {
            return IncomingEdgesSorted(vertex);
        }

        IEnumerable<IWeightedEdge<TVertex, TWeight>> IWeightedGraph<TVertex, TWeight>.OutgoingEdgesSorted(TVertex vertex)
        {
            return OutgoingEdgesSorted(vertex);
        }

        IEnumerable<IEdge<TVertex>> IGraph<TVertex>.BreadthFirstSearchEdges(TVertex vertex)
        {
            return BreadthFirstSearchEdges(vertex);
        }

        IEnumerable<IEdge<TVertex>> IGraph<TVertex>.DepthFirstSearchEdges(TVertex vertex)
        {
            return DepthFirstSearchEdges(vertex);
        }

        IEnumerable<IWeightedEdge<TVertex, TWeight>> IWeightedGraph<TVertex, TWeight>.BreadthFirstSearchEdges(TVertex vertex)
        {
            return BreadthFirstSearchEdges(vertex);
        }

        IEnumerable<IWeightedEdge<TVertex, TWeight>> IWeightedGraph<TVertex, TWeight>.DepthFirstSearchEdges(TVertex vertex)
        {
            return DepthFirstSearchEdges(vertex);
        }

        bool IWeightedGraph<TVertex, TWeight>.UpdateEdgeWeight(TVertex firstVertex, TVertex secondVertex, TWeight weight)
        {
            return UpdateEdgeWeight(firstVertex, secondVertex, weight);
        }

        bool IWeightedGraph<TVertex, TWeight>.TryGetEdgeWeight(TVertex firstVertex, TVertex secondVertex, out TWeight weight)
        {
            return TryGetEdgeWeight(firstVertex, secondVertex, out weight);
        }
    }
}
