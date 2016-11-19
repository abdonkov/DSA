using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DSA.DataStructures.Graphs;
using DSA.Algorithms.Graphs;

namespace DSAUnitTests.Algorithms.Graphs
{
    [TestClass]
    public class PrimMSTFinderTests
    {
        [TestMethod]
        public void PositiveEdgesWeightsAsAbsoluteVertexDifference()
        {
            var vertices = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            var graph = new WeightedALGraph<int, int>();

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

            // Expected MST
            // 1 <-> 2, 4
            // 2 <-> 1, 3, 5
            // 3 <-> 2
            // 4 <-> 1, 6
            // 5 <-> 2, 7
            // 6 <-> 4
            // 7 <-> 5, 8
            // 8 <-> 7
            // 9 <->
            
            var mst = graph.PrimMST();

            int weight = 0;
            Assert.IsTrue(mst.TryGetEdgeWeight(1, 2, out weight));
            Assert.IsTrue(weight == 1);
            Assert.IsTrue(mst.TryGetEdgeWeight(2, 1, out weight));
            Assert.IsTrue(weight == 1);
            Assert.IsTrue(mst.TryGetEdgeWeight(1, 4, out weight));
            Assert.IsTrue(weight == 3);
            Assert.IsTrue(mst.TryGetEdgeWeight(4, 1, out weight));
            Assert.IsTrue(weight == 3);
            Assert.IsTrue(mst.TryGetEdgeWeight(2, 3, out weight));
            Assert.IsTrue(weight == 1);
            Assert.IsTrue(mst.TryGetEdgeWeight(3, 2, out weight));
            Assert.IsTrue(weight == 1);
            Assert.IsTrue(mst.TryGetEdgeWeight(2, 5, out weight));
            Assert.IsTrue(weight == 3);
            Assert.IsTrue(mst.TryGetEdgeWeight(5, 2, out weight));
            Assert.IsTrue(weight == 3);
            Assert.IsTrue(mst.TryGetEdgeWeight(4, 6, out weight));
            Assert.IsTrue(weight == 2);
            Assert.IsTrue(mst.TryGetEdgeWeight(6, 4, out weight));
            Assert.IsTrue(weight == 2);
            Assert.IsTrue(mst.TryGetEdgeWeight(5, 7, out weight));
            Assert.IsTrue(weight == 2);
            Assert.IsTrue(mst.TryGetEdgeWeight(7, 5, out weight));
            Assert.IsTrue(weight == 2);
            Assert.IsTrue(mst.TryGetEdgeWeight(7, 8, out weight));
            Assert.IsTrue(weight == 1);
            Assert.IsTrue(mst.TryGetEdgeWeight(8, 7, out weight));
            Assert.IsTrue(weight == 1);

            Assert.IsFalse(mst.ContainsEdge(1, 3));
            Assert.IsFalse(mst.ContainsEdge(3, 1));
            Assert.IsFalse(mst.ContainsEdge(2, 6));
            Assert.IsFalse(mst.ContainsEdge(6, 2));
            Assert.IsFalse(mst.ContainsEdge(3, 6));
            Assert.IsFalse(mst.ContainsEdge(6, 3));
            Assert.IsFalse(mst.ContainsEdge(4, 7));
            Assert.IsFalse(mst.ContainsEdge(7, 4));
            Assert.IsFalse(mst.ContainsEdge(5, 8));
            Assert.IsFalse(mst.ContainsEdge(8, 5));
        }

        [TestMethod]
        public void PositiveEdgesWeightsAsVertexSum()
        {
            var vertices = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            var graph = new WeightedALGraph<int, int>();

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
            // With each edge having a weight of the absolute value of the difference of the vertices
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

            // Expected MST
            // 1 <-> 2, 3, 4
            // 2 <-> 1, 5, 6
            // 3 <-> 1
            // 4 <-> 1
            // 5 <-> 2, 7, 8
            // 6 <-> 2
            // 7 <-> 5
            // 8 <-> 5
            // 9 <->

            var mst = graph.PrimMST();

            int weight = 0;
            Assert.IsTrue(mst.TryGetEdgeWeight(1, 2, out weight));
            Assert.IsTrue(weight == 3);
            Assert.IsTrue(mst.TryGetEdgeWeight(2, 1, out weight));
            Assert.IsTrue(weight == 3);
            Assert.IsTrue(mst.TryGetEdgeWeight(1, 3, out weight));
            Assert.IsTrue(weight == 4);
            Assert.IsTrue(mst.TryGetEdgeWeight(3, 1, out weight));
            Assert.IsTrue(weight == 4);
            Assert.IsTrue(mst.TryGetEdgeWeight(1, 4, out weight));
            Assert.IsTrue(weight == 5);
            Assert.IsTrue(mst.TryGetEdgeWeight(4, 1, out weight));
            Assert.IsTrue(weight == 5);
            Assert.IsTrue(mst.TryGetEdgeWeight(2, 5, out weight));
            Assert.IsTrue(weight == 7);
            Assert.IsTrue(mst.TryGetEdgeWeight(5, 2, out weight));
            Assert.IsTrue(weight == 7);
            Assert.IsTrue(mst.TryGetEdgeWeight(2, 6, out weight));
            Assert.IsTrue(weight == 8);
            Assert.IsTrue(mst.TryGetEdgeWeight(6, 2, out weight));
            Assert.IsTrue(weight == 8);
            Assert.IsTrue(mst.TryGetEdgeWeight(4, 7, out weight));
            Assert.IsTrue(weight == 11);
            Assert.IsTrue(mst.TryGetEdgeWeight(7, 4, out weight));
            Assert.IsTrue(weight == 11);
            Assert.IsTrue(mst.TryGetEdgeWeight(5, 8, out weight));
            Assert.IsTrue(weight == 13);
            Assert.IsTrue(mst.TryGetEdgeWeight(8, 5, out weight));
            Assert.IsTrue(weight == 13);

            Assert.IsFalse(mst.ContainsEdge(2, 3));
            Assert.IsFalse(mst.ContainsEdge(3, 2));
            Assert.IsFalse(mst.ContainsEdge(3, 6));
            Assert.IsFalse(mst.ContainsEdge(6, 3));
            Assert.IsFalse(mst.ContainsEdge(4, 6));
            Assert.IsFalse(mst.ContainsEdge(6, 4));
            Assert.IsFalse(mst.ContainsEdge(5, 7));
            Assert.IsFalse(mst.ContainsEdge(7, 5));
            Assert.IsFalse(mst.ContainsEdge(7, 8));
            Assert.IsFalse(mst.ContainsEdge(8, 7));
        }

        [TestMethod]
        public void NegativeEdgesWeightsAsVertexDifference()
        {
            var vertices = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            var graph = new WeightedALGraph<int, int>();

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
            // With each edge having a weight of the absolute value of the difference of the vertices
            graph.AddEdge(1, 2, -1);
            graph.AddEdge(1, 3, -2);
            graph.AddEdge(1, 4, -3);
            graph.AddEdge(2, 3, -1);
            graph.AddEdge(2, 5, -3);
            graph.AddEdge(2, 6, -4);
            graph.AddEdge(3, 6, -3);
            graph.AddEdge(4, 6, -2);
            graph.AddEdge(4, 7, -3);
            graph.AddEdge(5, 7, -2);
            graph.AddEdge(5, 8, -3);
            graph.AddEdge(7, 8, -1);

            // Expected MST
            // 1 <-> 3, 4
            // 2 <-> 5, 6
            // 3 <-> 1, 6
            // 4 <-> 1, 7
            // 5 <-> 2, 8
            // 6 <-> 2, 3
            // 7 <-> 4
            // 8 <-> 5
            // 9 <->

            var mst = graph.PrimMST();

            int weight = 0;
            Assert.IsTrue(mst.TryGetEdgeWeight(1, 3, out weight));
            Assert.IsTrue(weight == -2);
            Assert.IsTrue(mst.TryGetEdgeWeight(3, 1, out weight));
            Assert.IsTrue(weight == -2);
            Assert.IsTrue(mst.TryGetEdgeWeight(1, 4, out weight));
            Assert.IsTrue(weight == -3);
            Assert.IsTrue(mst.TryGetEdgeWeight(4, 1, out weight));
            Assert.IsTrue(weight == -3);
            Assert.IsTrue(mst.TryGetEdgeWeight(2, 5, out weight));
            Assert.IsTrue(weight == -3);
            Assert.IsTrue(mst.TryGetEdgeWeight(5, 2, out weight));
            Assert.IsTrue(weight == -3);
            Assert.IsTrue(mst.TryGetEdgeWeight(2, 6, out weight));
            Assert.IsTrue(weight == -4);
            Assert.IsTrue(mst.TryGetEdgeWeight(6, 2, out weight));
            Assert.IsTrue(weight == -4);
            Assert.IsTrue(mst.TryGetEdgeWeight(3, 6, out weight));
            Assert.IsTrue(weight == -3);
            Assert.IsTrue(mst.TryGetEdgeWeight(6, 3, out weight));
            Assert.IsTrue(weight == -3);
            Assert.IsTrue(mst.TryGetEdgeWeight(4, 7, out weight));
            Assert.IsTrue(weight == -3);
            Assert.IsTrue(mst.TryGetEdgeWeight(7, 4, out weight));
            Assert.IsTrue(weight == -3);
            Assert.IsTrue(mst.TryGetEdgeWeight(5, 8, out weight));
            Assert.IsTrue(weight == -3);
            Assert.IsTrue(mst.TryGetEdgeWeight(8, 5, out weight));
            Assert.IsTrue(weight == -3);

            Assert.IsFalse(mst.ContainsEdge(1, 2));
            Assert.IsFalse(mst.ContainsEdge(2, 1));
            Assert.IsFalse(mst.ContainsEdge(2, 3));
            Assert.IsFalse(mst.ContainsEdge(3, 2));
            Assert.IsFalse(mst.ContainsEdge(4, 6));
            Assert.IsFalse(mst.ContainsEdge(6, 4));
            Assert.IsFalse(mst.ContainsEdge(5, 7));
            Assert.IsFalse(mst.ContainsEdge(7, 5));
            Assert.IsFalse(mst.ContainsEdge(7, 8));
            Assert.IsFalse(mst.ContainsEdge(8, 7));
        }

        [TestMethod]
        public void StringWeightsMST()
        {
            var vertices = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            var graph = new WeightedALGraph<int, string>();

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
            graph.AddEdge(1, 2, "a");
            graph.AddEdge(1, 3, "b");
            graph.AddEdge(1, 4, "b");
            graph.AddEdge(2, 3, "a");
            graph.AddEdge(2, 5, "c");
            graph.AddEdge(2, 6, "c");
            graph.AddEdge(3, 6, "c");
            graph.AddEdge(4, 6, "b");
            graph.AddEdge(4, 7, "b");
            graph.AddEdge(5, 7, "b");
            graph.AddEdge(5, 8, "c");
            graph.AddEdge(7, 8, "a");

            // Expected MST
            // 1 <-> 2, 4
            // 2 <-> 3
            // 3 <-> 2
            // 4 <-> 1, 6, 7
            // 5 <-> 7
            // 6 <-> 4
            // 7 <-> 4, 5, 8
            // 8 <-> 7
            // 9 <->
            var mst = graph.PrimMST();

            string weight = string.Empty;
            Assert.IsTrue(mst.TryGetEdgeWeight(1, 2, out weight));
            Assert.IsTrue(object.Equals(weight, "a"));
            Assert.IsTrue(mst.TryGetEdgeWeight(2, 1, out weight));
            Assert.IsTrue(object.Equals(weight, "a"));
            Assert.IsTrue(mst.TryGetEdgeWeight(1, 4, out weight));
            Assert.IsTrue(object.Equals(weight, "b"));
            Assert.IsTrue(mst.TryGetEdgeWeight(4, 1, out weight));
            Assert.IsTrue(object.Equals(weight, "b"));
            Assert.IsTrue(mst.TryGetEdgeWeight(2, 3, out weight));
            Assert.IsTrue(object.Equals(weight, "a"));
            Assert.IsTrue(mst.TryGetEdgeWeight(3, 2, out weight));
            Assert.IsTrue(object.Equals(weight, "a"));
            Assert.IsTrue(mst.TryGetEdgeWeight(4, 6, out weight));
            Assert.IsTrue(object.Equals(weight, "b"));
            Assert.IsTrue(mst.TryGetEdgeWeight(6, 4, out weight));
            Assert.IsTrue(object.Equals(weight, "b"));
            Assert.IsTrue(mst.TryGetEdgeWeight(4, 7, out weight));
            Assert.IsTrue(object.Equals(weight, "b"));
            Assert.IsTrue(mst.TryGetEdgeWeight(7, 4, out weight));
            Assert.IsTrue(object.Equals(weight, "b"));
            Assert.IsTrue(mst.TryGetEdgeWeight(5, 7, out weight));
            Assert.IsTrue(object.Equals(weight, "b"));
            Assert.IsTrue(mst.TryGetEdgeWeight(7, 5, out weight));
            Assert.IsTrue(object.Equals(weight, "b"));
            Assert.IsTrue(mst.TryGetEdgeWeight(7, 8, out weight));
            Assert.IsTrue(object.Equals(weight, "a"));
            Assert.IsTrue(mst.TryGetEdgeWeight(8, 7, out weight));
            Assert.IsTrue(object.Equals(weight, "a"));

            Assert.IsFalse(mst.ContainsEdge(1, 3));
            Assert.IsFalse(mst.ContainsEdge(3, 1));
            Assert.IsFalse(mst.ContainsEdge(2, 5));
            Assert.IsFalse(mst.ContainsEdge(5, 2));
            Assert.IsFalse(mst.ContainsEdge(2, 6));
            Assert.IsFalse(mst.ContainsEdge(6, 2));
            Assert.IsFalse(mst.ContainsEdge(3, 6));
            Assert.IsFalse(mst.ContainsEdge(6, 3));
            Assert.IsFalse(mst.ContainsEdge(5, 8));
            Assert.IsFalse(mst.ContainsEdge(8, 5));
        }

        [TestMethod]
        public void MinimumSpanningForest()
        {
            var vertices = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 , 10, 11, 12, 13, 14, 15, 16};

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

            // Expected MST
            // first component
            // 1 <-> 2, 4
            // 2 <-> 1, 3, 5
            // 3 <-> 2
            // 4 <-> 1, 6
            // 5 <-> 2, 7
            // 6 <-> 4
            // 7 <-> 5, 8
            // 8 <-> 7
            // second component
            // 9 <-> 10
            // 10 <-> 9, 11
            // 11 <-> 10, 12, 13
            // 12 <-> 11
            // 13 <-> 11
            // third component
            // 14 <-> 15
            // 15 <-> 14, 16
            // 16 <-> 15

            var mst = graph.PrimMST();

            int weight = 0;
            // first component
            Assert.IsTrue(mst.TryGetEdgeWeight(1, 2, out weight));
            Assert.IsTrue(weight == 1);
            Assert.IsTrue(mst.TryGetEdgeWeight(2, 1, out weight));
            Assert.IsTrue(weight == 1);
            Assert.IsTrue(mst.TryGetEdgeWeight(1, 4, out weight));
            Assert.IsTrue(weight == 3);
            Assert.IsTrue(mst.TryGetEdgeWeight(4, 1, out weight));
            Assert.IsTrue(weight == 3);
            Assert.IsTrue(mst.TryGetEdgeWeight(2, 3, out weight));
            Assert.IsTrue(weight == 1);
            Assert.IsTrue(mst.TryGetEdgeWeight(3, 2, out weight));
            Assert.IsTrue(weight == 1);
            Assert.IsTrue(mst.TryGetEdgeWeight(2, 5, out weight));
            Assert.IsTrue(weight == 3);
            Assert.IsTrue(mst.TryGetEdgeWeight(5, 2, out weight));
            Assert.IsTrue(weight == 3);
            Assert.IsTrue(mst.TryGetEdgeWeight(4, 6, out weight));
            Assert.IsTrue(weight == 2);
            Assert.IsTrue(mst.TryGetEdgeWeight(6, 4, out weight));
            Assert.IsTrue(weight == 2);
            Assert.IsTrue(mst.TryGetEdgeWeight(5, 7, out weight));
            Assert.IsTrue(weight == 2);
            Assert.IsTrue(mst.TryGetEdgeWeight(7, 5, out weight));
            Assert.IsTrue(weight == 2);
            Assert.IsTrue(mst.TryGetEdgeWeight(7, 8, out weight));
            Assert.IsTrue(weight == 1);
            Assert.IsTrue(mst.TryGetEdgeWeight(8, 7, out weight));
            Assert.IsTrue(weight == 1);
            // second component
            Assert.IsTrue(mst.TryGetEdgeWeight(9, 10, out weight));
            Assert.IsTrue(weight == 1);
            Assert.IsTrue(mst.TryGetEdgeWeight(10, 9, out weight));
            Assert.IsTrue(weight == 1);
            Assert.IsTrue(mst.TryGetEdgeWeight(10, 11, out weight));
            Assert.IsTrue(weight == 1);
            Assert.IsTrue(mst.TryGetEdgeWeight(11, 10, out weight));
            Assert.IsTrue(weight == 1);
            Assert.IsTrue(mst.TryGetEdgeWeight(11, 12, out weight));
            Assert.IsTrue(weight == 1);
            Assert.IsTrue(mst.TryGetEdgeWeight(12, 11, out weight));
            Assert.IsTrue(weight == 1);
            Assert.IsTrue(mst.TryGetEdgeWeight(11, 13, out weight));
            Assert.IsTrue(weight == 2);
            Assert.IsTrue(mst.TryGetEdgeWeight(13, 11, out weight));
            Assert.IsTrue(weight == 2);
            // third component
            Assert.IsTrue(mst.TryGetEdgeWeight(14, 15, out weight));
            Assert.IsTrue(weight == 1);
            Assert.IsTrue(mst.TryGetEdgeWeight(15, 14, out weight));
            Assert.IsTrue(weight == 1);
            Assert.IsTrue(mst.TryGetEdgeWeight(15, 16, out weight));
            Assert.IsTrue(weight == 1);
            Assert.IsTrue(mst.TryGetEdgeWeight(16, 15, out weight));
            Assert.IsTrue(weight == 1);

            // first component
            Assert.IsFalse(mst.ContainsEdge(1, 3));
            Assert.IsFalse(mst.ContainsEdge(3, 1));
            Assert.IsFalse(mst.ContainsEdge(2, 6));
            Assert.IsFalse(mst.ContainsEdge(6, 2));
            Assert.IsFalse(mst.ContainsEdge(3, 6));
            Assert.IsFalse(mst.ContainsEdge(6, 3));
            Assert.IsFalse(mst.ContainsEdge(4, 7));
            Assert.IsFalse(mst.ContainsEdge(7, 4));
            Assert.IsFalse(mst.ContainsEdge(5, 8));
            Assert.IsFalse(mst.ContainsEdge(8, 5));
            // second component
            Assert.IsFalse(mst.ContainsEdge(9, 12));
            Assert.IsFalse(mst.ContainsEdge(12, 9));
            Assert.IsFalse(mst.ContainsEdge(10, 12));
            Assert.IsFalse(mst.ContainsEdge(12, 10));
            // third component
            Assert.IsFalse(mst.ContainsEdge(14, 16));
            Assert.IsFalse(mst.ContainsEdge(16, 14));
        }
    }
}
