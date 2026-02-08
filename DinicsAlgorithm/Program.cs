using DinicsAlgorithm.Graph;

namespace DinicsAlgorithm;

internal static class Program
{
    public static void Main()
    {
        var graph = CreateGraph();
        var algorithm = new Dinics(graph);
        var maxFlow = algorithm.FindMaxFlow();
        
        Console.WriteLine(maxFlow);
    }

    private static FlowGraph CreateGraph()
    {
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

        return graph;
    }
}