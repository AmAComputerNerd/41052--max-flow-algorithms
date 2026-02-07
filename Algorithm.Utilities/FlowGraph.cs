namespace Algorithm.Utilities;

public class FlowGraph
{
    public List<Node> Nodes { get; init; } = [];
    
    public List<Edge> Edges { get; init; } = [];

    public Node AddNode(int id, string? label = null)
    {
        var node = new Node { Id = id, Label = label };
        Nodes.Add(node);
        return node;
    }

    public Edge AddEdge(Node from, Node to, int capacity)
    {
        var edge = new Edge
        {
            From = from,
            To = to,
            Capacity = capacity
        };
        Edges.Add(edge);
        return edge;
    }
}