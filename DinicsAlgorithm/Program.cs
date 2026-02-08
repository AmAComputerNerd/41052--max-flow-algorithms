using DinicsAlgorithm.Graph;

namespace DinicsAlgorithm;

internal static class Program
{
    public static void Main()
    {
        var graph = CreateGraph();
        var algorithm = new Algorithm(graph);
        var maxFlow = algorithm.FindMaxFlow();
        
        Console.WriteLine(maxFlow);
    }

    private static FlowGraph CreateGraph()
    {
        var source = new Node("S");
        var sink = new Node("T");
        
        var a = new Node("A");
        var b = new Node("B");
        var c = new Node("C");
        var d = new Node("D");
        
        var graph = new FlowGraph(source, sink);
        
        graph.AddEdge(source, a, 10);
        graph.AddEdge(source, b, 10);
        graph.AddEdge(a, b, 2);
        graph.AddEdge(a, c, 4);
        graph.AddEdge(a, d, 8);
        graph.AddEdge(b, d, 9);
        graph.AddEdge(c, sink, 10);
        graph.AddEdge(d, c, 6);
        graph.AddEdge(d, sink, 10);

        return graph;
    }
}