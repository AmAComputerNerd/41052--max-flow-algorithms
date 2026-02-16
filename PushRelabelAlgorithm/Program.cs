using Algorithm.Common;

namespace PushRelabelAlgorithm;

internal static class Program
{
    public static void Main()
    {
        var graph = CreateGraph();
        var algorithm = new PushRelabel(graph);
        var maxFlow = algorithm.FindMaxFlow();

        Console.WriteLine($"Maximum Flow: {maxFlow}");

        var minCut = algorithm.GetMinCut();
        Console.WriteLine($"\nMinimum Cut (nodes reachable from source): {string.Join(", ", minCut)}");
    }

    private static FlowGraph CreateGraph()
    {
        var graph = new FlowGraph("s", "t");
        
        graph.AddEdge("s", "1", 16);
        graph.AddEdge("s", "2", 13);
        graph.AddEdge("1", "2", 10);
        graph.AddEdge("2", "1", 4);
        graph.AddEdge("1", "3", 12);
        graph.AddEdge("2", "4", 14);
        graph.AddEdge("3", "2", 9);
        graph.AddEdge("3", "t", 20);
        graph.AddEdge("4", "3", 7);
        graph.AddEdge("4", "t", 4);

        return graph;
    }
}
