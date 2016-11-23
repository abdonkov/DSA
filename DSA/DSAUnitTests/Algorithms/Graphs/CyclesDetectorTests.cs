using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DSA.DataStructures.Graphs;
using DSA.Algorithms.Graphs;

namespace DSAUnitTests.Algorithms.Graphs
{
    [TestClass]
    public class CyclesDetectorTests
    {
        [TestMethod]
        public void UndirectedCyclicGraphCheck()
        {
            var vertices = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            var graph = new ALGraph<int>();

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

            Assert.IsTrue(graph.IsCyclic());
        }

        [TestMethod]
        public void DirectedCyclicGraphCheck()
        {
            var vertices = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            var graph = new DirectedALGraph<int>();

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

            Assert.IsTrue(graph.IsCyclic());
        }

        [TestMethod]
        public void UndirectedAcyclicGraphCheck()
        {
            var vertices = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            var graph = new ALGraph<int>();

            graph.AddVertices(vertices);

            // Graph is:
            // 1 <-> 2, 4
            // 2 <-> 1, 3, 5
            // 3 <-> 2, 6
            // 4 <-> 1, 7
            // 5 <-> 2, 8
            // 6 <-> 3
            // 7 <-> 4, 9
            // 8 <-> 5
            // 9 <-> 7
            graph.AddEdge(1, 2);
            graph.AddEdge(1, 4);
            graph.AddEdge(2, 3);
            graph.AddEdge(2, 5);
            graph.AddEdge(3, 6);
            graph.AddEdge(4, 7);
            graph.AddEdge(5, 8);
            graph.AddEdge(7, 9);

            Assert.IsFalse(graph.IsCyclic());
        }

        [TestMethod]
        public void DirectedAcyclicGraphCheck()
        {
            var vertices = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            var graph = new DirectedALGraph<int>();

            graph.AddVertices(vertices);

            // Graph is:
            // 1 -> 3, 4
            // 2 -> 5
            // 2 <- 3, 6
            // 3 -> 2, 6
            // 3 <- 1, 4
            // 4 -> 3, 6, 7
            // 4 <- 1
            // 5 -> 7, 8
            // 5 <- 2, 8
            // 6 -> 2
            // 6 <- 3, 4
            // 7 -> 4
            // 7 <- 5, 8
            // 8 -> 7
            // 8 <- 5
            // 9 ->
            // 9 <-
            graph.AddEdge(1, 3);
            graph.AddEdge(1, 4);
            graph.AddEdge(2, 5);
            graph.AddEdge(3, 2);
            graph.AddEdge(3, 6);
            graph.AddEdge(4, 3);
            graph.AddEdge(4, 6);
            graph.AddEdge(4, 7);
            graph.AddEdge(5, 7);
            graph.AddEdge(5, 8);
            graph.AddEdge(6, 2);
            graph.AddEdge(8, 7);

            Assert.IsFalse(graph.IsCyclic());
        }

        [TestMethod]
        public void ForestCyclesCheck()
        {
            var vertices = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };

            var graph = new WeightedALGraph<int, int>();

            graph.AddVertices(vertices);

            // Graph is:
            // first component
            // 1 <-> 2, 3, 4
            // 2 <-> 1, 3, 5, 6
            // 3 <-> 1, 2, 6
            // 4 <-> 1, 6, 7
            // 5 <-> 2, 7, 8
            // 6 <-> 2, 3, 4
            // 7 <-> 4, 8
            // 8 <-> 7, 5
            // second component
            // 9 <-> 10, 12
            // 10 <-> 9, 11, 12
            // 11 <-> 10, 12, 13
            // 12 <-> 9, 10, 11
            // 13 <-> 11
            // third component
            // 14 <-> 15, 16
            // 15 <-> 14, 16
            // 16 <-> 14, 15
            // With each edge having a weight of the absolute value of the difference of the vertices
            graph.AddEdge(1, 2, 1);
            graph.AddEdge(1, 3, 2);
            graph.AddEdge(1, 4, 3);
            graph.AddEdge(2, 3, 1);
            graph.AddEdge(2, 5, 3);
            graph.AddEdge(2, 6, 4);
            graph.AddEdge(3, 6, 3);
            graph.AddEdge(4, 6, 2);
            graph.AddEdge(4, 7, 3);
            graph.AddEdge(5, 7, 2);
            graph.AddEdge(5, 8, 3);
            graph.AddEdge(7, 8, 1);
            graph.AddEdge(9, 10, 1);
            graph.AddEdge(9, 12, 3);
            graph.AddEdge(10, 11, 1);
            graph.AddEdge(10, 12, 2);
            graph.AddEdge(11, 12, 1);
            graph.AddEdge(11, 13, 2);
            graph.AddEdge(14, 15, 1);
            graph.AddEdge(14, 16, 2);
            graph.AddEdge(15, 16, 1);

            // Graph has to be cyclic
            Assert.IsTrue(graph.IsCyclic());

            // Compute MST of the graph
            var mst = graph.KruskalMST();
            // The MST is acyclic
            Assert.IsFalse(mst.IsCyclic());
        }
    }
}
