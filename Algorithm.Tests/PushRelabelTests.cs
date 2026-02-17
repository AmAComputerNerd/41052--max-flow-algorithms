using Algorithm.Common;
using PushRelabelAlgorithm;

namespace Algorithm.Tests;

[TestClass]
public sealed class PushRelabelTests : MaxFlowTestCase
{
    protected override IMaxFlowAlgorithm CreateAlgorithm(FlowGraph graph)
    {
        return new PushRelabel(graph);
    }

    [TestMethod]
    public void FindMaxFlow_WhenExcessTrapped_RelabelsNodeRepeatedlyWhenRequired()
    {
        // Arrange - force the algorithm to relabel node A multiple times by creating a bottleneck at B.
        var graph = new FlowGraph("S", "T");
        graph.AddEdge("S", "A", 10);
        graph.AddEdge("A", "B", 1);
        graph.AddEdge("B", "C", 1);
        graph.AddEdge("C", "T", 10);

        var algorithm = CreateAlgorithm(graph);

        // Act
        var maxFlow = algorithm.FindMaxFlow();

        // Assert
        Assert.AreEqual(1, maxFlow);
    }

    [TestMethod]
    public void FindMaxFlow_WhenFlowRequiresBackwardPush_CanPushBackwardOnResidualEdges()
    {
        // Arrange - force the algorithm to push flow backward.
        var graph = new FlowGraph("S", "T");
        graph.AddEdge("S", "A", 10);
        graph.AddEdge("A", "B", 10);
        graph.AddEdge("B", "T", 1);
        graph.AddEdge("A", "T", 9);

        var algorithm = CreateAlgorithm(graph);

        // Act
        var maxFlow = algorithm.FindMaxFlow();

        // Assert
        Assert.AreEqual(10, maxFlow);
    }

    [TestMethod]
    public void FindMaxFlow_WhenGraphHasActiveNodesButNoResidualPathToSink_TerminatesCorrectly()
    {
        // Arrange - create a flow scenario where node A and B will never be able to push flow to T, but they will still be active and require relabeling.
        var graph = new FlowGraph("S", "T");
        graph.AddEdge("S", "A", 10);
        graph.AddEdge("A", "B", 10); // dead end
        graph.AddEdge("S", "T", 3);

        var algorithm = CreateAlgorithm(graph);

        // Act
        var maxFlow = algorithm.FindMaxFlow();

        // Assert
        Assert.AreEqual(3, maxFlow);
    }
}
