namespace Algorithm.Utilities;

public class FlowGraph
{
    public int VertexCount { get; }

    private readonly List<Edge>[] _adjacencyList;
    
    public FlowGraph(int vertexCount)
    {
        VertexCount = vertexCount;
        _adjacencyList = new List<Edge>[vertexCount];

        for (var i = 0; i < vertexCount; i++)
        {
            _adjacencyList[i] = [];
        }
    }

    public void AddEdge(int from, int to, int capacity)
    {
        ValidateVertex(from);
        ValidateVertex(to);
        ArgumentOutOfRangeException.ThrowIfNegative(capacity);

        var forward = new Edge
        {
            From = from,
            To = to,
            Capacity = capacity
        };
        var backward = new Edge
        {
            From = to,
            To = from,
            Capacity = 0
        };
        forward.ReverseIndex = _adjacencyList[to].Count;
        backward.ReverseIndex = _adjacencyList[from].Count;
        
        _adjacencyList[from].Add(forward);
        _adjacencyList[to].Add(backward);
    }

    public void PushFlow(Edge edge, int flow)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(flow);
        if (flow > edge.ResidualCapacity)
        {
            throw new ArgumentException("Flow is greater than capacity");
        }
        
        edge.Flow += flow;
        GetReverseEdge(edge).Flow -= flow;
    }

    public void ResetFlow()
    {
        for (var i = 0; i < VertexCount; i++)
        {
            foreach (var edge in _adjacencyList[i])
            {
                edge.Flow = 0;
            }
        }
    }

    public List<Edge> GetAllEdges() => _adjacencyList
        .SelectMany(edges => edges)
        .Where(edge => edge.Capacity > 0)
        .ToList();
    
    public Edge GetReverseEdge(Edge edge) => _adjacencyList[edge.To][edge.ReverseIndex];

    private void ValidateVertex(int vertex)
    {
        if (vertex < 0 || vertex >= VertexCount)
        {
            throw new ArgumentOutOfRangeException(nameof(vertex));
        }
    }
}