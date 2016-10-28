using DSA.Algorithms.Sorting;
using DSA.DataStructures.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DSA.DataStructures.Graphs
{
    /// <summary>
    /// Represents an undirected and unweighted adjacency matrix graph.
    /// </summary>
    /// <typeparam name="TVertex">The data type of the vertices. TVertex implements <see cref="IComparable{T}"/>.</typeparam>
    public class AMGraph<TVertex> : IGraph<TVertex>
        where TVertex : IComparable<TVertex>
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
        /// Determines whether the <see cref="AMGraph{TVertex}"/> is directed.
        /// </summary>
        public bool IsDirected { get { return false; } }

        /// <summary>
        /// Deteremines whether the <see cref="AMGraph{TVertex}"/> is weighted.
        /// </summary>
        public bool IsWeighted { get { return false; } }

        /// <summary>
        /// Gets the number of edges in the <see cref="AMGraph{TVertex}"/>.
        /// </summary>
        public int EdgesCount { get; internal set; }

        /// <summary>
        /// Gets the number of vertices in the <see cref="AMGraph{TVertex}"/>.
        /// </summary>
        public int VerticesCount { get; internal set; }

        /// <summary>
        /// Gets the vertices in the <see cref="AMGraph{TVertex}"/>.
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
        /// Gets the vertices in the <see cref="AMGraph{TVertex}"/> in sorted ascending order.
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
        /// Gets the edges in the <see cref="AMGraph{TVertex}"/>. For each edge in the graph returns two <see cref="UnweightedEdge{TVertex}"/> objects with swapped source and destination vertices.
        /// </summary>
        public IEnumerable<UnweightedEdge<TVertex>> Edges
        {
            get
            {
                int mLength = adjacencyMatrix.GetLength(0);
                for (int i = 0; i < mLength; i++)
                {
                    for (int j = 0; j < mLength; j++)
                    {
                        if (adjacencyMatrix[i, j])
                            yield return new UnweightedEdge<TVertex>(vertices[i], vertices[j]);
                    }
                }
            }
        }

        /// <summary>
        /// Creates a new instance of the <see cref="AMGraph{TVertex}"/>.
        /// </summary>
        public AMGraph()
        {
            verticesIDs = new Dictionary<TVertex, int>();
            vertices = new Dictionary<int, TVertex>();
            adjacencyMatrix = new bool[0, 0];
            EdgesCount = 0;
            VerticesCount = 0;
        }

        /// <summary>
        /// Adds an edge defined by the given vertices to the <see cref="AMGraph{TVertex}"/>. If the vertices are not present in the graph they will be added.
        /// </summary>
        /// <param name="firstVertex">The first vertex.</param>
        /// <param name="secondVertex">The second vertex.</param>
        /// <returns>Returns true if the edge was added successfully; otherwise false. Also returns false if edge already exists.</returns>
        public bool AddEdge(TVertex firstVertex, TVertex secondVertex)
        {
            if (object.Equals(firstVertex, secondVertex)) return false;

            // Add first vertex if it is not in the graph
            if (!AddVertex(firstVertex))
                if (verticesIDs.ContainsKey(secondVertex))
                    if (adjacencyMatrix[verticesIDs[firstVertex], verticesIDs[secondVertex]])// if the vertices are connected
                        return false;// we return false

            // Add second vertex if not in the graph
            AddVertex(secondVertex);

            int firstVertexID = verticesIDs[firstVertex];
            int secondVertexID = verticesIDs[secondVertex];

            // Here the vertices are in the graph, so we connect them
            adjacencyMatrix[firstVertexID, secondVertexID] = true;

            // Add the other way around. Graph is not directed.
            adjacencyMatrix[secondVertexID, firstVertexID] = true;

            // Counted as one edge because graph is undirected
            EdgesCount++;
            return true;
        }

        /// <summary>
        /// Adds a vertex to the <see cref="AMGraph{TVertex}"/>.
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

            // Resize matrix
            var newAdjacencyMatrix = new bool[vertexID + 1, vertexID + 1];

            for (int i = 0; i < vertexID; i++)
            {
                for (int j = 0; j < vertexID; j++)
                {
                    newAdjacencyMatrix[i, j] = adjacencyMatrix[i, j];
                }
            }

            adjacencyMatrix = newAdjacencyMatrix;

            VerticesCount++;

            return true;
        }

        /// <summary>
        /// Adds the specified collection of vertices to the <see cref="AMGraph{TVertex}"/>. Only one matrix resizing is performed. If some of the vertices are already in the graph exception is not thrown.
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

            // Resize matrix
            var newAdjacencyMatrix = new bool[curVertexID, curVertexID];
            for (int i = 0; i < matrixCount; i++)
            {
                for (int j = 0; j < matrixCount; j++)
                {
                    newAdjacencyMatrix[i, j] = adjacencyMatrix[i, j];
                }
            }

            adjacencyMatrix = newAdjacencyMatrix;

            VerticesCount = adjacencyMatrix.GetLength(0);
        }

        /// <summary>
        /// Returns the incoming edges of the given vertex.
        /// </summary>
        /// <param name="vertex">The vertex whose incoming edges are returned.</param>
        /// <returns>Returns a <see cref="IEnumerable{T}"/> of <see cref="UnweightedEdge{TVertex}"/> of all incoming edges of the given vertex.</returns>
        public IEnumerable<UnweightedEdge<TVertex>> IncomingEdges(TVertex vertex)
        {
            if (!verticesIDs.ContainsKey(vertex)) throw new KeyNotFoundException("Vertex does not belong to the graph!");

            // Get vertexID and matrix length
            int vertexID = verticesIDs[vertex];
            int mLength = adjacencyMatrix.GetLength(0);

            // Add the adjacent vertices to a list
            var adjacent = new List<TVertex>(mLength);
            for (int i = 0; i < mLength; i++)
            {
                if (adjacencyMatrix[i, vertexID])
                    adjacent.Add(vertices[i]);
            }

            if (adjacent.Count > 0)
            {
                for (int i = 0; i < adjacent.Count; i++)
                {
                    yield return new UnweightedEdge<TVertex>(adjacent[i], vertex);
                }          
            }
        }

        /// <summary>
        /// Returns the outgoing edges of the given vertex.
        /// </summary>
        /// <param name="vertex">The vertex whose outgoing edges are returned.</param>
        /// <returns>Returns a <see cref="IEnumerable{T}"/> of <see cref="UnweightedEdge{TVertex}"/> of all outgoing edges of the given vertex.</returns>
        public IEnumerable<UnweightedEdge<TVertex>> OutgoingEdges(TVertex vertex)
        {
            if (!verticesIDs.ContainsKey(vertex)) throw new KeyNotFoundException("Vertex does not belong to the graph!");

            // Get vertexID and matrix length
            int vertexID = verticesIDs[vertex];
            int mLength = adjacencyMatrix.GetLength(0);

            // Add the adjacent vertices to a list
            var adjacent = new List<TVertex>(mLength);
            for (int i = 0; i < mLength; i++)
            {
                if (adjacencyMatrix[vertexID, i])
                    adjacent.Add(vertices[i]);
            }

            if (adjacent.Count > 0)
            {
                for (int i = 0; i < adjacent.Count; i++)
                {
                    yield return new UnweightedEdge<TVertex>(vertex, adjacent[i]);
                }
            }
        }

        /// <summary>
        /// Returns the incoming edges of the given vertex sorted by their source vertex.
        /// </summary>
        /// <param name="vertex">The vertex whose incoming edges are returned.</param>
        /// <returns>Returns a <see cref="IEnumerable{T}"/> of <see cref="UnweightedEdge{TVertex}"/> of all incoming edges of the given vertex.</returns>
        public IEnumerable<UnweightedEdge<TVertex>> IncomingEdgesSorted(TVertex vertex)
        {
            if (!verticesIDs.ContainsKey(vertex)) throw new KeyNotFoundException("Vertex does not belong to the graph!");

            // Get vertexID and matrix length
            int vertexID = verticesIDs[vertex];
            int mLength = adjacencyMatrix.GetLength(0);

            // Add the adjacent vertices to a list
            var adjacent = new List<TVertex>(mLength);
            for (int i = 0; i < mLength; i++)
            {
                if (adjacencyMatrix[i, vertexID])
                    adjacent.Add(vertices[i]);
            }
            
            if (adjacent.Count > 0)
            {
                adjacent.QuickSort();
                for (int i = 0; i < adjacent.Count; i++)
                {
                    yield return new UnweightedEdge<TVertex>(adjacent[i], vertex);
                }
            }
        }

        /// <summary>
        /// Returns the outgoing edges of the given vertex sorted by their destination vertex.
        /// </summary>
        /// <param name="vertex">The vertex whose outgoing edges are returned.</param>
        /// <returns>Returns a <see cref="IEnumerable{T}"/> of <see cref="UnweightedEdge{TVertex}"/> of all outgoing edges of the given vertex.</returns>
        public IEnumerable<UnweightedEdge<TVertex>> OutgoingEdgesSorted(TVertex vertex)
        {
            if (!verticesIDs.ContainsKey(vertex)) throw new KeyNotFoundException("Vertex does not belong to the graph!");

            // Get vertexID and matrix length
            int vertexID = verticesIDs[vertex];
            int mLength = adjacencyMatrix.GetLength(0);

            // Add the adjacent vertices to a list
            var adjacent = new List<TVertex>(mLength);
            for (int i = 0; i < mLength; i++)
            {
                if (adjacencyMatrix[vertexID, i])
                    adjacent.Add(vertices[i]);
            }

            if (adjacent.Count > 0)
            {
                adjacent.QuickSort();
                for (int i = 0; i < adjacent.Count; i++)
                {
                    yield return new UnweightedEdge<TVertex>(vertex, adjacent[i]);
                }
            }
        }

        /// <summary>
        /// Determines whether the edge is presented in the <see cref="AMGraph{TVertex}"/>.
        /// </summary>
        /// <param name="firstVertex">The first vertex of the edge.</param>
        /// <param name="secondVertex">The second vertex of the edge.</param>
        /// <returns>Returns true if the edge is presented in the <see cref="AMGraph{TVertex}"/>; false otherwise.</returns>
        public bool ContainsEdge(TVertex firstVertex, TVertex secondVertex)
        {
            if (!verticesIDs.ContainsKey(firstVertex)) return false;
            if (!verticesIDs.ContainsKey(secondVertex)) return false;

            return adjacencyMatrix[verticesIDs[firstVertex], verticesIDs[secondVertex]];
        }

        /// <summary>
        /// Determines whether the vertex is presented in the <see cref="AMGraph{TVertex}"/>.
        /// </summary>
        /// <param name="vertex">The vertex to see if presented in the <see cref="AMGraph{TVertex}"/>.</param>
        /// <returns>Returns true if the vertex is presented in the <see cref="AMGraph{TVertex}"/>; false otherwise.</returns>
        public bool ContainsVertex(TVertex vertex)
        {
            return verticesIDs.ContainsKey(vertex);
        }

        /// <summary>
        /// Removes the edge defined by the given vertices from the <see cref="AMGraph{TVertex}"/>.
        /// </summary>
        /// <param name="firstVertex">The first vertex.</param>
        /// <param name="secondVertex">The second vertex.</param>
        /// <returns>Returns true if the edge was removed successfully; otherwise false. Also returns false if the vertices are not present in this graph or the edge does not exist.</returns>
        public bool RemoveEdge(TVertex firstVertex, TVertex secondVertex)
        {
            if (!verticesIDs.ContainsKey(firstVertex)) return false;
            if (!verticesIDs.ContainsKey(secondVertex)) return false;

            int firstVertexID = verticesIDs[firstVertex];
            int secondVertexID = verticesIDs[secondVertex];

            adjacencyMatrix[firstVertexID, secondVertexID] = false;
            adjacencyMatrix[secondVertexID, firstVertexID] = false;

            // Counted as one edge because graph is undirected
            EdgesCount--;
            return true;
        }

        /// <summary>
        /// Removes the given vertex from the <see cref="AMGraph{TVertex}"/>.
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
            }

            // Create new adjacency matrix
            var newAdjacencyMatrix = new bool[newMatrixLength, newMatrixLength];

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
                }
            }

            adjacencyMatrix = newAdjacencyMatrix;

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
        /// Returns the degree of the given vertex presented in the <see cref="AMGraph{TVertex}"/>.
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
                if (adjacencyMatrix[i, vertexID])
                    degree++;
            }

            return degree;
        }

        /// <summary>
        /// Removes all edges and vertices from the <see cref="AMGraph{TVertex}"/>.
        /// </summary>
        public void Clear()
        {
            verticesIDs.Clear();
            vertices.Clear();
            adjacencyMatrix = new bool[0, 0];
            EdgesCount = 0;
            VerticesCount = 0;
        }

        /// <summary>
        /// Breadth-first search of the <see cref="AMGraph{TVertex}"/> with sorted levels. Returns <see cref="IEnumerable{T}"/> of the vertices.
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
        /// Breadth-first search of the <see cref="AMGraph{TVertex}"/> with sorted levels. Returns <see cref="IEnumerable{T}"/> of <see cref="UnweightedEdge{TVertex}"/> representing the edges of the graph.
        /// </summary>
        /// <param name="vertex">The vertex from which the breadth-first search starts.</param>
        /// <returns>.Returns <see cref="IEnumerable{T}"/> of <see cref="UnweightedEdge{TVertex}"/> representing the edges of the graph.</returns>
        public IEnumerable<UnweightedEdge<TVertex>> BreadthFirstSearchEdges(TVertex vertex)
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

                        yield return new UnweightedEdge<TVertex>(curVertex, sortedLevel[i]);
                    }
                }
            }
        }

        /// <summary>
        /// Depth-first search of the <see cref="AMGraph{TVertex}"/> with sorted levels. Returns <see cref="IEnumerable{T}"/> of the vertices.
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
        /// Depth-first search of the <see cref="AMGraph{TVertex}"/> with sorted levels. Returns <see cref="IEnumerable{T}"/> of <see cref="UnweightedEdge{TVertex}"/> representing the edges of the graph.
        /// </summary>
        /// <param name="vertex">The vertex from which the depth-first search starts.</param>
        /// <returns>.Returns <see cref="IEnumerable{T}"/> of <see cref="UnweightedEdge{TVertex}"/> representing the edges of the graph.</returns>
        public IEnumerable<UnweightedEdge<TVertex>> DepthFirstSearchEdges(TVertex vertex)
        {
            if (!verticesIDs.ContainsKey(vertex)) throw new KeyNotFoundException("Vertex does not belong to the graph!");

            var stackSource = new Stack<TVertex>(VerticesCount);
            var stackDestination = new Stack<TVertex>(VerticesCount);
            var visited = new HashSet<TVertex>();
            TVertex[] sortedLevel = new TVertex[VerticesCount];
            int mLength = adjacencyMatrix.GetLength(0);
            int sCount = 0;
            int curVertexID = verticesIDs[vertex];

            // Add vertex neighbours to stack
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
                sortedLevel.QuickSortDescending(0, sCount, null);// descending sort beacause we add them in stack
                for (int i = 0; i < sCount; i++)
                {
                    stackSource.Push(vertex);
                    stackDestination.Push(sortedLevel[i]);
                }
            }

            visited.Add(vertex);

            while (stackDestination.Count > 0)
            {
                TVertex curSourceVertex = stackSource.Pop();
                TVertex curDestinationVertex = stackDestination.Pop();

                curVertexID = verticesIDs[curDestinationVertex];

                if (!visited.Contains(curDestinationVertex))
                {
                    yield return new UnweightedEdge<TVertex>(curSourceVertex, curDestinationVertex);

                    visited.Add(curDestinationVertex);

                    sCount = 0;

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
                        sortedLevel.QuickSortDescending(0, sCount, null);// descending sort beacause we add them in stack
                        for (int i = 0; i < sCount; i++)
                        {
                            stackSource.Push(curDestinationVertex);
                            stackDestination.Push(sortedLevel[i]);
                        }
                    }
                }
            }
        }

        IEnumerable<IEdge<TVertex>> IGraph<TVertex>.Edges { get { return Edges; } }

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

        IEnumerable<IEdge<TVertex>> IGraph<TVertex>.BreadthFirstSearchEdges(TVertex vertex)
        {
            return BreadthFirstSearchEdges(vertex);
        }

        IEnumerable<IEdge<TVertex>> IGraph<TVertex>.DepthFirstSearchEdges(TVertex vertex)
        {
            return DepthFirstSearchEdges(vertex);
        }
    }
}
