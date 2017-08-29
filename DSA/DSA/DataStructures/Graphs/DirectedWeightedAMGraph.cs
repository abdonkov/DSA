using DSA.Algorithms.Sorting;
using DSA.DataStructures.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DSA.DataStructures.Graphs
{
    /// <summary>
    /// Represents a directed and weighted adjacency matrix graph.
    /// </summary>
    /// <typeparam name="TVertex">The data type of the vertices. TVertex implements <see cref="IComparable{T}"/>.</typeparam>
    /// <typeparam name="TWeight">The data type of the weight of the edges. TWeight implements <see cref="IComparable{T}"/>.</typeparam>
    public class DirectedWeightedAMGraph<TVertex, TWeight> : IWeightedGraph<TVertex, TWeight>
        where TVertex : IComparable<TVertex>
        where TWeight : IComparable<TWeight>
    {
        /// <summary>
        /// Dictionary saving the vertices IDs as values and the vertices as keys.
        /// </summary>
        internal Dictionary<TVertex, int> verticesIDs;

        /// <summary>
        /// Dictionary saving the vertices as values and the vertices IDs as keys.
        /// </summary>
        internal Dictionary<int, TVertex> vertices;

        /// <summary>
        /// Represents the adjacency matrix between the vertices using their IDs.
        /// </summary>
        internal bool[,] adjacencyMatrix;

        /// <summary>
        /// Represents a adjacency matrix that holds the weight between the vertices using their IDs.
        /// </summary>
        internal TWeight[,] edgeWeights;

        /// <summary>
        /// Determines whether the <see cref="DirectedWeightedAMGraph{TVertex, TWeight}"/> is directed.
        /// </summary>
        public bool IsDirected { get { return true; } }

        /// <summary>
        /// Deteremines whether the <see cref="DirectedWeightedAMGraph{TVertex, TWeight}"/> is weighted.
        /// </summary>
        public bool IsWeighted { get { return true; } }

        /// <summary>
        /// Gets the number of edges in the <see cref="DirectedWeightedAMGraph{TVertex, TWeight}"/>.
        /// </summary>
        public int EdgesCount { get; internal set; }

        /// <summary>
        /// Gets the number of vertices in the <see cref="DirectedWeightedAMGraph{TVertex, TWeight}"/>.
        /// </summary>
        public int VerticesCount { get; internal set; }

        /// <summary>
        /// Gets the vertices in the <see cref="DirectedWeightedAMGraph{TVertex, TWeight}"/>.
        /// </summary>
        public IEnumerable<TVertex> Vertices
        {
            get
            {
                List<TVertex> vertices = new List<TVertex>(VerticesCount);
                foreach (var vertex in verticesIDs.Keys)
                {
                    vertices.Add(vertex);
                }
                return vertices;
            }
        }

        /// <summary>
        /// Gets the vertices in the <see cref="DirectedWeightedAMGraph{TVertex, TWeight}"/> in sorted ascending order.
        /// </summary>
        public IEnumerable<TVertex> VerticesSorted
        {
            get
            {
                List<TVertex> vertices = new List<TVertex>(VerticesCount);
                foreach (var vertex in verticesIDs.Keys)
                {
                    vertices.Add(vertex);
                }

                if (vertices.Count > 0)
                    vertices.QuickSort();

                return vertices;
            }
        }

        /// <summary>
        /// Gets the edges in the <see cref="DirectedWeightedAMGraph{TVertex, TWeight}"/>.
        /// </summary>
        public IEnumerable<WeightedEdge<TVertex, TWeight>> Edges
        {
            get
            {
                int mLength = adjacencyMatrix.GetLength(0);
                for (int i = 0; i < mLength; i++)
                {
                    for (int j = 0; j < mLength; j++)
                    {
                        if (adjacencyMatrix[i, j])
                            yield return new WeightedEdge<TVertex, TWeight>(vertices[i], vertices[j], edgeWeights[i, j]);
                    }
                }
            }
        }

        /// <summary>
        /// Creates a new instance of the <see cref="DirectedWeightedAMGraph{TVertex, TWeight}"/>.
        /// </summary>
        public DirectedWeightedAMGraph()
        {
            verticesIDs = new Dictionary<TVertex, int>();
            vertices = new Dictionary<int, TVertex>();
            adjacencyMatrix = new bool[0, 0];
            edgeWeights = new TWeight[0, 0];
            EdgesCount = 0;
            VerticesCount = 0;
        }

        /// <summary>
        /// Adds an edge defined by the given vertices to the <see cref="DirectedWeightedAMGraph{TVertex, TWeight}"/> with weight being the TWeight default value of the <see cref="WeightedALGraph{TVertex, TWeight}"/>. If the vertices are not present in the graph they will be added.
        /// </summary>
        /// <param name="source">The source vertex of the edge.</param>
        /// <param name="destination">The destination vertex of the edge.</param>
        /// <returns>Returns true if the edge was added successfully; otherwise false. Also returns false if edge already exists.</returns>
        public bool AddEdge(TVertex source, TVertex destination)
        {
            return AddEdge(source, destination, default(TWeight));
        }

        /// <summary>
        /// Adds an edge defined by the given vertices to the <see cref="DirectedWeightedAMGraph{TVertex, TWeight}"/> with the given weight. If the vertices are not present in the graph they will be added.
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
                if (verticesIDs.ContainsKey(destination))
                    if (adjacencyMatrix[verticesIDs[source], verticesIDs[destination]])// if the vertices are connected
                        return false;// we return false

            // Add second vertex if not in the graph
            AddVertex(destination);

            int firstVertexID = verticesIDs[source];
            int secondVertexID = verticesIDs[destination];

            // Here the vertices are in the graph, so we connect them
            adjacencyMatrix[firstVertexID, secondVertexID] = true;
            edgeWeights[firstVertexID, secondVertexID] = weight;

            EdgesCount++;
            return true;
        }

        /// <summary>
        /// Adds a vertex to the <see cref="DirectedWeightedAMGraph{TVertex, TWeight}"/>.
        /// </summary>
        /// <param name="vertex">The vertex to add.</param>
        /// <returns>Returns true if the edge was added successfully; otherwise false. Also returns false if the vertex already exists.</returns>
        public bool AddVertex(TVertex vertex)
        {
            if (verticesIDs.ContainsKey(vertex)) return false;

            int vertexID = adjacencyMatrix.GetLength(0);// Get new vertex ID

            // Add vertex to dictionaries
            verticesIDs.Add(vertex, vertexID);
            vertices.Add(vertexID, vertex);

            // Resize matrices
            var newAdjacencyMatrix = new bool[vertexID + 1, vertexID + 1];
            var newEdgeWeights = new TWeight[vertexID + 1, vertexID + 1];

            for (int i = 0; i < vertexID; i++)
            {
                for (int j = 0; j < vertexID; j++)
                {
                    newAdjacencyMatrix[i, j] = adjacencyMatrix[i, j];
                    newEdgeWeights[i, j] = edgeWeights[i, j];
                }
            }

            adjacencyMatrix = newAdjacencyMatrix;
            edgeWeights = newEdgeWeights;

            VerticesCount++;

            return true;
        }

        /// <summary>
        /// Adds the specified collection of vertices to the <see cref="DirectedWeightedAMGraph{TVertex, TWeight}"/>. Only one matrix resizing is performed. If some of the vertices are already in the graph exception is not thrown.
        /// </summary>
        /// <param name="vertices">Adds the <see cref="IEnumerable{T}"/> of vertices to the graph.</param>
        public void AddVertices(IEnumerable<TVertex> vertices)
        {
            // Get matrix lenght and the current vertex ID
            int matrixCount = adjacencyMatrix.GetLength(0);
            int curVertexID = matrixCount;

            // Add only the new vertices to the dictionaries
            foreach (var vertex in vertices)
            {
                if (!verticesIDs.ContainsKey(vertex))
                {
                    verticesIDs.Add(vertex, curVertexID);
                    this.vertices.Add(curVertexID++, vertex);
                }
            }

            if (curVertexID == matrixCount) return;// if there aren't new vertices return

            // Resize matrices
            var newAdjacencyMatrix = new bool[curVertexID, curVertexID];
            var newEdgeWeights = new TWeight[curVertexID, curVertexID];
            for (int i = 0; i < matrixCount; i++)
            {
                for (int j = 0; j < matrixCount; j++)
                {
                    newAdjacencyMatrix[i, j] = adjacencyMatrix[i, j];
                    newEdgeWeights[i, j] = edgeWeights[i, j];
                }
            }

            adjacencyMatrix = newAdjacencyMatrix;
            edgeWeights = newEdgeWeights;

            VerticesCount = adjacencyMatrix.GetLength(0);
        }

        /// <summary>
        /// Returns the incoming edges of the given vertex.
        /// </summary>
        /// <param name="vertex">The vertex whose incoming edges are returned.</param>
        /// <returns>Returns a <see cref="IEnumerable{T}"/> of <see cref="WeightedEdge{TVertex, TWeight}"/> of all incoming edges of the given vertex.</returns>
        public IEnumerable<WeightedEdge<TVertex, TWeight>> IncomingEdges(TVertex vertex)
        {
            if (!verticesIDs.ContainsKey(vertex)) throw new KeyNotFoundException("Vertex does not belong to the graph!");

            // Get vertexID and matrix length
            int vertexID = verticesIDs[vertex];
            int mLength = adjacencyMatrix.GetLength(0);

            // Add the adjacent vertices and the weights of the edges to a list
            var adjacent = new List<KeyValuePair<TVertex, TWeight>>(mLength);
            for (int i = 0; i < mLength; i++)
            {
                if (adjacencyMatrix[i, vertexID])
                    adjacent.Add(new KeyValuePair<TVertex, TWeight>(vertices[i], edgeWeights[i, vertexID]));
            }

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
            if (!verticesIDs.ContainsKey(vertex)) throw new KeyNotFoundException("Vertex does not belong to the graph!");

            // Get vertexID and matrix length
            int vertexID = verticesIDs[vertex];
            int mLength = adjacencyMatrix.GetLength(0);

            // Add the adjacent vertices and the weights of the edges to a list
            var adjacent = new List<KeyValuePair<TVertex, TWeight>>(mLength);
            for (int i = 0; i < mLength; i++)
            {
                if (adjacencyMatrix[vertexID, i])
                    adjacent.Add(new KeyValuePair<TVertex, TWeight>(vertices[i], edgeWeights[vertexID, i]));
            }

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
            if (!verticesIDs.ContainsKey(vertex)) throw new KeyNotFoundException("Vertex does not belong to the graph!");

            var kvpComparer = Comparer<KeyValuePair<TVertex, TWeight>>.Create((x, y) => x.Key.CompareTo(y.Key));

            // Get vertexID and matrix length
            int vertexID = verticesIDs[vertex];
            int mLength = adjacencyMatrix.GetLength(0);

            // Add the adjacent vertices and the weights of the edges to a list
            var adjacent = new List<KeyValuePair<TVertex, TWeight>>(mLength);
            for (int i = 0; i < mLength; i++)
            {
                if (adjacencyMatrix[i, vertexID])
                    adjacent.Add(new KeyValuePair<TVertex, TWeight>(vertices[i], edgeWeights[i, vertexID]));
            }

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
            if (!verticesIDs.ContainsKey(vertex)) throw new KeyNotFoundException("Vertex does not belong to the graph!");

            var kvpComparer = Comparer<KeyValuePair<TVertex, TWeight>>.Create((x, y) => x.Key.CompareTo(y.Key));

            // Get vertexID and matrix length
            int vertexID = verticesIDs[vertex];
            int mLength = adjacencyMatrix.GetLength(0);

            // Add the adjacent vertices and the weights of the edges to a list
            var adjacent = new List<KeyValuePair<TVertex, TWeight>>(mLength);
            for (int i = 0; i < mLength; i++)
            {
                if (adjacencyMatrix[vertexID, i])
                    adjacent.Add(new KeyValuePair<TVertex, TWeight>(vertices[i], edgeWeights[vertexID, i]));
            }

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
        /// Determines whether the edge is presented in the <see cref="DirectedWeightedAMGraph{TVertex, TWeight}"/>.
        /// </summary>
        /// <param name="source">The source vertex of the edge.</param>
        /// <param name="destination">The destination vertex of the edge.</param>
        /// <returns>Returns true if the edge is presented in the <see cref="DirectedWeightedAMGraph{TVertex, TWeight}"/>; false otherwise.</returns>
        public bool ContainsEdge(TVertex source, TVertex destination)
        {
            if (!verticesIDs.ContainsKey(source)) return false;
            if (!verticesIDs.ContainsKey(destination)) return false;

            return adjacencyMatrix[verticesIDs[source], verticesIDs[destination]];
        }

        /// <summary>
        /// Determines whether the vertex is presented in the <see cref="DirectedWeightedAMGraph{TVertex, TWeight}"/>.
        /// </summary>
        /// <param name="vertex">The vertex to see if presented in the <see cref="DirectedWeightedAMGraph{TVertex, TWeight}"/>.</param>
        /// <returns>Returns true if the vertex is presented in the <see cref="DirectedWeightedAMGraph{TVertex, TWeight}"/>; false otherwise.</returns>
        public bool ContainsVertex(TVertex vertex)
        {
            return verticesIDs.ContainsKey(vertex);
        }

        /// <summary>
        /// Updates the weight of the edge defined by the given vertices in the <see cref="DirectedWeightedAMGraph{TVertex, TWeight}"/>.
        /// </summary>
        /// <param name="source">The source vertex of the edge.</param>
        /// <param name="destination">The destination vertex of the edge.</param>
        /// <param name="weight">The new weight of the edge.</param>
        /// <returns>Returns true if the edge weight was updated successfully; otherwise, false. Also returns false if the vertices are not present in this <see cref="WeightedALGraph{TVertex, TWeight}"/>.</returns>
        public bool UpdateEdgeWeight(TVertex source, TVertex destination, TWeight weight)
        {
            if (!verticesIDs.ContainsKey(source)) return false;
            if (!verticesIDs.ContainsKey(destination)) return false;

            edgeWeights[verticesIDs[source], verticesIDs[destination]] = weight;

            return true;
        }

        /// <summary>
        /// Gets the weight of the edge defined by the given vertices in the <see cref="DirectedWeightedAMGraph{TVertex, TWeight}"/>.
        /// </summary>
        /// <param name="source">The source vertex of the edge.</param>
        /// <param name="destination">The destination vertex of the edge.</param>
        /// <param name="weight">Contains the weight of the edge, if the edge is presented in the graph; otherwise, contains the default value for the type of the weight parameter.</param>
        /// <returns>Returns true if the graph contains the specified edge; otherwise, false.</returns>
        public bool TryGetEdgeWeight(TVertex source, TVertex destination, out TWeight weight)
        {
            weight = default(TWeight);

            if (!verticesIDs.ContainsKey(source)) return false;
            if (!verticesIDs.ContainsKey(destination)) return false;

            weight = edgeWeights[verticesIDs[source], verticesIDs[destination]];
            return true;
        }

        /// <summary>
        /// Removes the edge defined by the given vertices from the <see cref="DirectedWeightedAMGraph{TVertex, TWeight}"/>.
        /// </summary>
        /// <param name="source">The source vertex of the edge.</param>
        /// <param name="destination">The destination vertex of the edge.</param>
        /// <returns>Returns true if the edge was removed successfully; otherwise false. Also returns false if the vertices are not present in this graph or the edge does not exist.</returns>
        public bool RemoveEdge(TVertex source, TVertex destination)
        {
            if (!verticesIDs.ContainsKey(source)) return false;
            if (!verticesIDs.ContainsKey(destination)) return false;

            int sourceID = verticesIDs[source];
            int destinationID = verticesIDs[destination];

            adjacencyMatrix[sourceID, destinationID] = false;
            edgeWeights[sourceID, destinationID] = default(TWeight);

            EdgesCount--;
            return true;
        }

        /// <summary>
        /// Removes the given vertex from the <see cref="DirectedWeightedAMGraph{TVertex, TWeight}"/>.
        /// </summary>
        /// <param name="vertex">The vertex to remove.</param>
        /// <returns>Returns true if the vertex was removed successfully; otherwise false. Also returns false if the vertex does not exist.</returns>
        public bool RemoveVertex(TVertex vertex)
        {
            if (!verticesIDs.ContainsKey(vertex)) return false;

            int vertexID = verticesIDs[vertex];
            int newMatrixLength = adjacencyMatrix.GetLength(0) - 1;

            // Count removed edges
            int removedEdges = 0;
            for (int i = 0; i <= newMatrixLength; i++)
            {
                if (adjacencyMatrix[vertexID, i]) removedEdges++;
                if (adjacencyMatrix[i, vertexID]) removedEdges++;
            }

            // Create new matrices
            var newAdjacencyMatrix = new bool[newMatrixLength, newMatrixLength];
            var newEdgeWeights = new TWeight[newMatrixLength, newMatrixLength];

            // Copy adjancency matrix without the vertex for removal
            for (int i = 0; i < newMatrixLength; i++)
            {
                for (int j = 0; j < newMatrixLength; j++)
                {
                    // Calculate the corresponding matrix indexes from the old adjacency matrix.
                    // Indexes before the vertex for removal are the same and the indexes after it are 
                    // smaller by 1 in the new adjacency matrix
                    int oldI = i < vertexID ? i : i + 1;
                    int oldJ = j < vertexID ? j : j + 1;
                    newAdjacencyMatrix[i, j] = adjacencyMatrix[oldI, oldJ];
                    newEdgeWeights[i, j] = edgeWeights[oldI, oldJ];
                }
            }

            adjacencyMatrix = newAdjacencyMatrix;
            edgeWeights = newEdgeWeights;

            // Remove vertex from dictionaries
            verticesIDs.Remove(vertex);
            vertices.Remove(vertexID);

            // Now we decrease all vertexIDs bigger than the vertex for removal ID by 1
            // to correspond to the new adjacency matrix
            verticesIDs = verticesIDs.ToDictionary(// create a new dictionary from this one
                kvp => kvp.Key, // new key is the same
                kvp => kvp.Value < vertexID ? kvp.Value : kvp.Value - 1); // new value(vertexID) is the same if lower that the vertex for removal ID else smaller by 1

            vertices = vertices.ToDictionary(// create a new dictionary from this one
                kvp => kvp.Key < vertexID ? kvp.Key : kvp.Key - 1, // new key(vertexID) is the same if lower that the vertex for removal ID else smaller by 1
                kvp => kvp.Value);// new value is the same

            // Decrease vertices count and edges count
            VerticesCount--;
            EdgesCount -= removedEdges;
            return true;
        }

        /// <summary>
        /// Returns the degree of the given vertex presented in the <see cref="DirectedWeightedAMGraph{TVertex, TWeight}"/>.
        /// </summary>
        /// <param name="vertex">The vertex to calculate its degeree.</param>
        /// <returns>Returns the degree of the given vertex.</returns>
        public int Degree(TVertex vertex)
        {
            if (!verticesIDs.ContainsKey(vertex)) throw new KeyNotFoundException("Vertex does not belong to the graph!");

            int vertexID = verticesIDs[vertex];
            int mLength = adjacencyMatrix.GetLength(0);

            int degree = 0;
            for (int i = 0; i < mLength; i++)
            {
                if (adjacencyMatrix[i, vertexID]) degree++;
                if (adjacencyMatrix[vertexID, i]) degree++;
            }

            return degree;
        }

        /// <summary>
        /// Removes all edges and vertices from the <see cref="DirectedWeightedAMGraph{TVertex, TWeight}"/>.
        /// </summary>
        public void Clear()
        {
            verticesIDs.Clear();
            vertices.Clear();
            adjacencyMatrix = new bool[0, 0];
            edgeWeights = new TWeight[0, 0];
            EdgesCount = 0;
            VerticesCount = 0;
        }

        /// <summary>
        /// Breadth-first search of the <see cref="DirectedWeightedAMGraph{TVertex, TWeight}"/> with sorted levels. Returns <see cref="IEnumerable{T}"/> of the vertices.
        /// </summary>
        /// <param name="vertex">The vertex from which the breadth-first search starts.</param>
        /// <returns>Returns <see cref="IEnumerable{T}"/> of the vertices.</returns>
        public IEnumerable<TVertex> BreadthFirstSearch(TVertex vertex)
        {
            if (!verticesIDs.ContainsKey(vertex)) throw new KeyNotFoundException("Vertex does not belong to the graph!");

            var queue = new Queue<TVertex>(VerticesCount);
            var visited = new HashSet<TVertex>();
            TVertex[] sortedLevel = new TVertex[VerticesCount];
            int mLength = adjacencyMatrix.GetLength(0);

            queue.Enqueue(vertex);
            visited.Add(vertex);

            while (queue.Count > 0)
            {
                TVertex curVertex = queue.Dequeue();
                int curVertexID = verticesIDs[curVertex];

                yield return curVertex;

                int sCount = 0;

                for (int i = 0; i < mLength; i++)
                {
                    if (adjacencyMatrix[curVertexID, i])
                    {
                        var adjVertex = vertices[i];
                        if (!visited.Contains(adjVertex))
                            sortedLevel[sCount++] = adjVertex;
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
        /// Breadth-first search of the <see cref="DirectedWeightedAMGraph{TVertex, TWeight}"/> with sorted levels. Returns <see cref="IEnumerable{T}"/> of <see cref="WeightedEdge{TVertex, TWeight}"/> representing the edges of the graph.
        /// </summary>
        /// <param name="vertex">The vertex from which the breadth-first search starts.</param>
        /// <returns>.Returns <see cref="IEnumerable{T}"/> of <see cref="WeightedEdge{TVertex, TWeight}"/> representing the edges of the graph.</returns>
        public IEnumerable<WeightedEdge<TVertex, TWeight>> BreadthFirstSearchEdges(TVertex vertex)
        {
            if (!verticesIDs.ContainsKey(vertex)) throw new KeyNotFoundException("Vertex does not belong to the graph!");

            var queue = new Queue<TVertex>(VerticesCount);
            var visited = new HashSet<TVertex>();
            var sortedLevel = new KeyValuePair<TVertex, TWeight>[VerticesCount];
            var kvpComparer = Comparer<KeyValuePair<TVertex, TWeight>>.Create((x, y) => x.Key.CompareTo(y.Key));
            int mLength = adjacencyMatrix.GetLength(0);

            queue.Enqueue(vertex);
            visited.Add(vertex);

            while (queue.Count > 0)
            {
                TVertex curVertex = queue.Dequeue();
                int curVertexID = verticesIDs[curVertex];

                int sCount = 0;

                for (int i = 0; i < mLength; i++)
                {
                    if (adjacencyMatrix[curVertexID, i])
                    {
                        var adjVertex = vertices[i];
                        if (!visited.Contains(adjVertex))
                            sortedLevel[sCount++] = new KeyValuePair<TVertex, TWeight>(adjVertex, edgeWeights[curVertexID, i]);
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
        /// Depth-first search of the <see cref="DirectedWeightedAMGraph{TVertex, TWeight}"/> with sorted levels. Returns <see cref="IEnumerable{T}"/> of the vertices.
        /// </summary>
        /// <param name="vertex">The vertex from which the depth-first search starts.</param>
        /// <returns>Returns <see cref="IEnumerable{T}"/> of the vertices.</returns>
        public IEnumerable<TVertex> DepthFirstSearch(TVertex vertex)
        {
            if (!verticesIDs.ContainsKey(vertex)) throw new KeyNotFoundException("Vertex does not belong to the graph!");

            var stack = new Stack<TVertex>(VerticesCount);
            var visited = new HashSet<TVertex>();
            TVertex[] sortedLevel = new TVertex[VerticesCount];
            int mLength = adjacencyMatrix.GetLength(0);

            stack.Push(vertex);

            while (stack.Count > 0)
            {
                TVertex curVertex = stack.Pop();
                int curVertexID = verticesIDs[curVertex];

                if (!visited.Contains(curVertex))
                {
                    yield return curVertex;

                    visited.Add(curVertex);

                    int sCount = 0;

                    for (int i = 0; i < mLength; i++)
                    {
                        if (adjacencyMatrix[curVertexID, i])
                        {
                            var adjVertex = vertices[i];
                            if (!visited.Contains(adjVertex))
                                sortedLevel[sCount++] = adjVertex;
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
        /// Depth-first search of the <see cref="DirectedWeightedAMGraph{TVertex, TWeight}"/> with sorted levels. Returns <see cref="IEnumerable{T}"/> of <see cref="WeightedEdge{TVertex, TWeight}"/> representing the edges of the graph.
        /// </summary>
        /// <param name="vertex">The vertex from which the depth-first search starts.</param>
        /// <returns>.Returns <see cref="IEnumerable{T}"/> of <see cref="WeightedEdge{TVertex, TWeight}"/> representing the edges of the graph.</returns>
        public IEnumerable<WeightedEdge<TVertex, TWeight>> DepthFirstSearchEdges(TVertex vertex)
        {
            if (!verticesIDs.ContainsKey(vertex)) throw new KeyNotFoundException("Vertex does not belong to the graph!");

            var stackSource = new Stack<TVertex>(VerticesCount);
            var stackDestination = new Stack<TVertex>(VerticesCount);
            var stackWeight = new Stack<TWeight>(VerticesCount);
            var visited = new HashSet<TVertex>();
            var sortedLevel = new KeyValuePair<TVertex, TWeight>[VerticesCount];
            var kvpComparer = Comparer<KeyValuePair<TVertex, TWeight>>.Create((x, y) => x.Key.CompareTo(y.Key));
            int sCount = 0;
            int mLength = adjacencyMatrix.GetLength(0);
            int curVertexID = verticesIDs[vertex];

            // Add vertex neighbours to stack
            for (int i = 0; i < mLength; i++)
            {
                if (adjacencyMatrix[curVertexID, i])
                {
                    var adjVertex = vertices[i];
                    if (!visited.Contains(adjVertex))
                        sortedLevel[sCount++] = new KeyValuePair<TVertex, TWeight>(adjVertex, edgeWeights[curVertexID, i]);
                }
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

                curVertexID = verticesIDs[curDestinationVertex];

                if (!visited.Contains(curDestinationVertex))
                {
                    yield return new WeightedEdge<TVertex, TWeight>(curSourceVertex, curDestinationVertex, curWeight);

                    visited.Add(curDestinationVertex);

                    sCount = 0;

                    for (int i = 0; i < mLength; i++)
                    {
                        if (adjacencyMatrix[curVertexID, i])
                        {
                            var adjVertex = vertices[i];
                            if (!visited.Contains(adjVertex))
                                sortedLevel[sCount++] = new KeyValuePair<TVertex, TWeight>(adjVertex, edgeWeights[curVertexID, i]);
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
