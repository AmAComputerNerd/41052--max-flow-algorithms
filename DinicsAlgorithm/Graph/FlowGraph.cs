namespace DinicsAlgorithm.Graph;

public class FlowGraph(Node source, Node sink)
{
    public Node Source { get; } = source;

    public Node Sink { get; } = sink;
    
    public IReadOnlyCollection<Node> Nodes => _nodes;
    
    private readonly Dictionary<Node, List<Edge>> _adjacencyList = new();

    public void AddNode(Node node)
    {
        if (!_nodes.Add(node)) return;
        _adjacencyList[node] = [];
    }

    private readonly HashSet<Node> _nodes = [];

    public void AddEdge(Node from, Node to, int capacity)
    {
        AddNode(from);
        AddNode(to);

        var forwardEdge = new Edge(from, to, capacity);
        var reverseEdge = new Edge(from, to, capacity);

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

    public List<Edge> GetEdges(Node node) => _adjacencyList.TryGetValue(node, out var value) ? value : [];
}