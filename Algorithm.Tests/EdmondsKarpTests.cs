using Algorithm.Common;
using EdmondsKarpAlgorithm;
using EdmondsKarpAlgorithm;

namespace Algorithm.Tests
{
    [TestClass]
    public sealed class EdmondsKarpTests
    {
        [TestMethod]
        public void FindMaxFlow_StandardGraph_ReturnsCorrectFlow()
        {
            // Arrange
            var graph = new FlowGraph("S", "T");
            graph.AddEdge("S", "A", 10);
            graph.AddEdge("S", "B", 10);
            graph.AddEdge("A", "B", 2);
            graph.AddEdge("A", "C", 4);
            graph.AddEdge("A", "D", 8);
            graph.AddEdge("B", "D", 9);
            graph.AddEdge("C", "T", 10);
            graph.AddEdge("D", "C", 6);
            graph.AddEdge("D", "T", 10);
            var edmondsKarp = new EdmondsKarp(graph);

            // Act
            var maxFlow = edmondsKarp.FindMaxFlow(graph.Source, graph.Sink);

            // Assert
            Assert.AreEqual(19, maxFlow);
        }

        [TestMethod]
        public void FindMaxFlow_NoPathToSink_ReturnsZero()
        {
            // Arrange: Disconnected graph
            var graph = new FlowGraph("S", "T");
            graph.AddEdge("S", "A", 10);
            graph.AddEdge("B", "T", 10);
            var edmondsKarp = new EdmondsKarp(graph);

            // Act
            var maxFlow = edmondsKarp.FindMaxFlow(graph.Source, graph.Sink);

            // Assert
            Assert.AreEqual(0, maxFlow);
        }

        [TestMethod]
        public void FindMaxFlow_DirectSourceToSink_ReturnsCapacity()
        {
            // Arrange: Single edge S -> T
            var graph = new FlowGraph("S", "T");
            graph.AddEdge("S", "T", 42);
            var edmondsKarp = new EdmondsKarp(graph);

            // Act
            var maxFlow = edmondsKarp.FindMaxFlow(graph.Source, graph.Sink);

            // Assert
            Assert.AreEqual(42, maxFlow);
        }

        [TestMethod]
        public void FindMaxFlow_ZeroCapacity_ReturnsZero()
        {
            // Arrange: Zero capacity edge
            var graph = new FlowGraph("S", "T");
            graph.AddEdge("S", "T", 0);
            var edmondsKarp = new EdmondsKarp(graph);

            // Act
            var maxFlow = edmondsKarp.FindMaxFlow(graph.Source, graph.Sink);

            // Assert
            Assert.AreEqual(0, maxFlow);
        }

        [TestMethod]
        public void FindMaxFlow_ParallelPaths_SumsAllPaths()
        {
            // Arrange: Multiple independent paths
            var graph = new FlowGraph("S", "T");
            graph.AddEdge("S", "A", 10);
            graph.AddEdge("A", "T", 10);
            graph.AddEdge("S", "B", 15);
            graph.AddEdge("B", "T", 15);
            var edmondsKarp = new EdmondsKarp(graph);

            // Act
            var maxFlow = edmondsKarp.FindMaxFlow(graph.Source, graph.Sink);

            // Assert
            Assert.AreEqual(25, maxFlow);
        }

        [TestMethod]
        public void FindMaxFlow_BottleneckInMiddle_LimitedByBottleneck()
        {
            // Arrange: Bottleneck in the middle
            var graph = new FlowGraph("S", "T");
            graph.AddEdge("S", "A", 100);
            graph.AddEdge("A", "B", 5);
            graph.AddEdge("B", "T", 100);
            var edmondsKarp = new EdmondsKarp(graph);

            // Act
            var maxFlow = edmondsKarp.FindMaxFlow(graph.Source, graph.Sink);

            // Assert
            Assert.AreEqual(5, maxFlow);
        }

        [TestMethod]
        public void FindMaxFlow_GraphWithCycle_HandlesCorrectly()
        {
            // Arrange: Graph with cycle (tests that algorithm doesn't infinitely loop)
            var graph = new FlowGraph("S", "T");
            graph.AddEdge("S", "A", 10);
            graph.AddEdge("A", "B", 8);
            graph.AddEdge("B", "A", 5);
            graph.AddEdge("B", "T", 10);
            var edmondsKarp = new EdmondsKarp(graph);

            // Act
            var maxFlow = edmondsKarp.FindMaxFlow(graph.Source, graph.Sink);

            // Assert
            Assert.AreEqual(8, maxFlow);
        }

        [TestMethod]
        public void FindMaxFlow_MultipleEdgesSameNodes_SumsCapacities()
        {
            // Arrange: Multiple edges between the same pair of nodes
            var graph = new FlowGraph("S", "T");
            graph.AddEdge("S", "A", 10);
            graph.AddEdge("S", "A", 5);
            graph.AddEdge("A", "T", 20);
            var edmondsKarp = new EdmondsKarp(graph);

            // Act
            var maxFlow = edmondsKarp.FindMaxFlow(graph.Source, graph.Sink);

            // Assert
            Assert.AreEqual(15, maxFlow);
        }

        [TestMethod]
        public void FindMaxFlow_AfterResetFlow_ReturnsOriginalFlow()
        {
            // Arrange
            var graph = new FlowGraph("S", "T");
            graph.AddEdge("S", "A", 10);
            graph.AddEdge("A", "T", 10);
            var algorithm1 = new EdmondsKarp(graph);

            // Act
            var maxFlow1 = algorithm1.FindMaxFlow(graph.Source, graph.Sink);
            graph.ResetFlow();
            var algorithm2 = new EdmondsKarp(graph);
            var maxFlow2 = algorithm2.FindMaxFlow(graph.Source, graph.Sink);

            // Assert
            Assert.AreEqual(10, maxFlow1);
            Assert.AreEqual(10, maxFlow2);
        }

        [TestMethod]
        public void FindMaxFlow_BipartiteMatching_ReturnsMaxMatching()
        {
            // Arrange: Classic max-flow application (bipartite matching)
            var graph = new FlowGraph("S", "T");
            graph.AddEdge("S", "L1", 1);
            graph.AddEdge("S", "L2", 1);
            graph.AddEdge("S", "L3", 1);
            graph.AddEdge("L1", "R1", 1);
            graph.AddEdge("L1", "R2", 1);
            graph.AddEdge("L2", "R2", 1);
            graph.AddEdge("L3", "R3", 1);
            graph.AddEdge("R1", "T", 1);
            graph.AddEdge("R2", "T", 1);
            graph.AddEdge("R3", "T", 1);
            var edmondsKarp = new EdmondsKarp(graph);

            // Act
            var maxFlow = edmondsKarp.FindMaxFlow(graph.Source, graph.Sink);

            // Assert
            Assert.AreEqual(3, maxFlow);
        }
    }
}
