using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DSA.DataStructures.Graphs;
using System.Collections.Generic;
using DSA.Algorithms.Graphs;

namespace DSAUnitTests.Algorithms.Graphs
{
    [TestClass]
    public class BipartiteColorerTests
    {
        [TestMethod]
        public void BipartiteColoringOnUndirectedGraphCheck()
        {
            var vertices = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            var graph = new ALGraph<int>();

            graph.AddVertices(vertices);

            // Graph is:
            // 1 <-> 2, 3, 4
            // 2 <-> 1, 5, 6
            // 3 <-> 1, 6
            // 4 <-> 1, 5, 6, 7
            // 5 <-> 2, 4, 8
            // 6 <-> 2, 3, 4
            // 7 <-> 4, 8
            // 8 <-> 7, 5
            // 9 <->
            graph.AddEdge(1, 2);
            graph.AddEdge(1, 3);
            graph.AddEdge(1, 4);
            graph.AddEdge(2, 5);
            graph.AddEdge(2, 6);
            graph.AddEdge(3, 6);
            graph.AddEdge(4, 5);
            graph.AddEdge(4, 6);
            graph.AddEdge(4, 7);
            graph.AddEdge(5, 8);
            graph.AddEdge(7, 8);

            Dictionary<int, BipartiteColor> verticesColors;
            Assert.IsTrue(graph.TryGetBipartiteColoring(out verticesColors));

            foreach (var edge in graph.Edges)
            {
                Assert.IsTrue(verticesColors[edge.Source] != verticesColors[edge.Destination]);
            }

            // Check with not bipartite graph

            graph = new ALGraph<int>();

            graph.AddVertices(vertices);

            // Graph is:
            // 1 <-> 2, 3, 4
            // 2 <-> 1, 3, 5, 6
            // 3 <-> 1, 2, 6
            // 4 <-> 1, 6, 7
            // 5 <-> 2, 7, 8
            // 6 <-> 2, 3, 4
            // 7 <-> 4, 8
            // 8 <-> 7, 5
            // 9 <->
            graph.AddEdge(1, 2);
            graph.AddEdge(1, 3);
            graph.AddEdge(1, 4);
            graph.AddEdge(2, 3);
            graph.AddEdge(2, 5);
            graph.AddEdge(2, 6);
            graph.AddEdge(3, 6);
            graph.AddEdge(4, 6);
            graph.AddEdge(4, 7);
            graph.AddEdge(5, 7);
            graph.AddEdge(5, 8);
            graph.AddEdge(7, 8);

            Assert.IsFalse(graph.TryGetBipartiteColoring(out verticesColors));
        }

        [TestMethod]
        public void BipartiteColoringOnDirectedGraphCheck()
        {
            var vertices = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            var graph = new DirectedALGraph<int>();

            graph.AddVertices(vertices);

            // Graph is:
            // 1 -> 3, 4
            // 1 <- 2
            // 2 -> 1, 5
            // 2 <- 5, 6
            // 3 -> 6
            // 3 <- 1
            // 4 -> 6
            // 4 <- 1, 5
            // 5 -> 2, 4, 8
            // 5 <- 2, 8
            // 6 -> 2
            // 6 <- 3, 4
            // 7 -> 4
            // 7 <- 5, 8
            // 8 -> 5, 7
            // 8 <- 5
            // 9 ->
            // 9 <-
            graph.AddEdge(1, 3);
            graph.AddEdge(1, 4);
            graph.AddEdge(2, 1);
            graph.AddEdge(2, 5);
            graph.AddEdge(3, 6);
            graph.AddEdge(4, 6);
            graph.AddEdge(5, 2);
            graph.AddEdge(5, 4);
            graph.AddEdge(5, 8);
            graph.AddEdge(6, 2);
            graph.AddEdge(7, 4);
            graph.AddEdge(8, 5);
            graph.AddEdge(8, 7);

            Dictionary<int, BipartiteColor> verticesColors;
            Assert.IsTrue(graph.TryGetBipartiteColoring(out verticesColors));

            foreach (var edge in graph.Edges)
            {
                Assert.IsTrue(verticesColors[edge.Source] != verticesColors[edge.Destination]);
            }

            // Check with not bipartite graph

            graph = new DirectedALGraph<int>();

            graph.AddVertices(vertices);

            // Graph is:
            // 1 -> 3, 4
            // 1 <- 2
            // 2 -> 1, 5
            // 2 <- 3, 5, 6
            // 3 -> 2, 6
            // 3 <- 1, 4
            // 4 -> 3, 6
            // 4 <- 1, 7
            // 5 -> 2, 7, 8
            // 5 <- 2, 8
            // 6 -> 2
            // 6 <- 3, 4
            // 7 -> 4
            // 7 <- 5, 8
            // 8 -> 5, 7
            // 8 <- 5
            // 9 ->
            // 9 <-
            graph.AddEdge(1, 3);
            graph.AddEdge(1, 4);
            graph.AddEdge(2, 1);
            graph.AddEdge(2, 5);
            graph.AddEdge(3, 2);
            graph.AddEdge(3, 6);
            graph.AddEdge(4, 3);
            graph.AddEdge(4, 6);
            graph.AddEdge(5, 2);
            graph.AddEdge(5, 7);
            graph.AddEdge(5, 8);
            graph.AddEdge(6, 2);
            graph.AddEdge(7, 4);
            graph.AddEdge(8, 5);
            graph.AddEdge(8, 7);

            Assert.IsFalse(graph.TryGetBipartiteColoring(out verticesColors));
        }

        [TestMethod]
        public void BipartiteColoringWeightedGraphCheck()
        {
            var vertices = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            var graph = new WeightedALGraph<int, int>();

            graph.AddVertices(vertices);

            // Graph is:
            // 1 <-> 2, 3, 4
            // 2 <-> 1, 5, 6
            // 3 <-> 1, 6
            // 4 <-> 1, 5, 6, 7
            // 5 <-> 2, 4, 8
            // 6 <-> 2, 3, 4
            // 7 <-> 4, 8
            // 8 <-> 7, 5
            // 9 <->
            // With each edge having a weight of the sum of its vertices
            graph.AddEdge(1, 2, 3);
            graph.AddEdge(1, 3, 4);
            graph.AddEdge(1, 4, 5);
            graph.AddEdge(2, 5, 7);
            graph.AddEdge(2, 6, 8);
            graph.AddEdge(3, 6, 9);
            graph.AddEdge(4, 5, 9);
            graph.AddEdge(4, 6, 10);
            graph.AddEdge(4, 7, 11);
            graph.AddEdge(5, 8, 13);
            graph.AddEdge(7, 8, 15);

            Dictionary<int, BipartiteColor> verticesColors;
            Assert.IsTrue(graph.TryGetBipartiteColoring(out verticesColors));

            foreach (var edge in graph.Edges)
            {
                Assert.IsTrue(verticesColors[edge.Source] != verticesColors[edge.Destination]);
            }

            // Check with not bipartite graph

            graph = new WeightedALGraph<int, int>();

            graph.AddVertices(vertices);

            // Graph is:
            // 1 <-> 2, 3, 4
            // 2 <-> 1, 3, 5, 6
            // 3 <-> 1, 2, 6
            // 4 <-> 1, 6, 7
            // 5 <-> 2, 7, 8
            // 6 <-> 2, 3, 4
            // 7 <-> 4, 8
            // 8 <-> 7, 5
            // 9 <->
            // With each edge having a weight of the sum of its vertices
            graph.AddEdge(1, 2, 3);
            graph.AddEdge(1, 3, 4);
            graph.AddEdge(1, 4, 5);
            graph.AddEdge(2, 3, 5);
            graph.AddEdge(2, 5, 7);
            graph.AddEdge(2, 6, 8);
            graph.AddEdge(3, 6, 9);
            graph.AddEdge(4, 6, 10);
            graph.AddEdge(4, 7, 11);
            graph.AddEdge(5, 7, 12);
            graph.AddEdge(5, 8, 13);
            graph.AddEdge(7, 8, 15);

            Assert.IsFalse(graph.TryGetBipartiteColoring(out verticesColors));
        }
    }
}
