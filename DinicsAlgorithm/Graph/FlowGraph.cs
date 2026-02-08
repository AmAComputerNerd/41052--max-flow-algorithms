namespace DinicsAlgorithm.Graph;

public class FlowGraph(Node source, Node sink)
{
    public Node Source { get; } = source;

    public Node Sink { get; } = sink;

    private readonly Dictionary<Node, List<FlowEdge>> _adjacencyList = new();

    private readonly HashSet<Node> _nodes = [];

    public void AddEdge(Node from, Node to, int capacity)
    {
        AddNode(from);
        AddNode(to);

        var forwardEdge = new FlowEdge(to, capacity);
        var reverseEdge = new FlowEdge(from, 0);

        forwardEdge.ReverseEdge = reverseEdge;
        reverseEdge.ReverseEdge = forwardEdge;
        
        _adjacencyList[from].Add(forwardEdge);
        _adjacencyList[to].Add(reverseEdge);
    }

    public void ResetFlow()
    {
        foreach (var edge in _adjacencyList.Values.SelectMany(edges => edges))
        {
            edge.Flow = 0;
        }
    }

    public List<FlowEdge> GetEdges(Node node) => _adjacencyList.TryGetValue(node, out var value) ? value : [];
    
    private void AddNode(Node node)
    {
        if (!_nodes.Add(node)) return;
        _adjacencyList[node] = [];
    }
}