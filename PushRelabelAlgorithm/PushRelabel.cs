using Algorithm.Common;

namespace PushRelabelAlgorithm;

public class PushRelabel(FlowGraph graph) : IMaxFlowAlgorithm
{
    readonly Dictionary<Node, int> height = [];
    readonly Dictionary<Node, int> excess = [];

    public int FindMaxFlow()
    {
        InitialisePreflow();

        var queue = new Queue<Node>();
        var nodesInQueueHashSet = new HashSet<Node>();

        foreach (var node in graph.GetNodes())
        {
            if (node != graph.Source && node != graph.Sink && excess[node] > 0)
            {
                queue.Enqueue(node);
                nodesInQueueHashSet.Add(node);
            }
        }

        while (queue.Count > 0)
        {
            var u = queue.Dequeue();
            nodesInQueueHashSet.Remove(u);

            Discharge(u);

            if (excess[u] > 0)
            {
                queue.Enqueue(u);
                nodesInQueueHashSet.Add(u);
            }

            foreach (var edge in graph.GetEdges(u))
            {
                if (edge.To != graph.Source && edge.To != graph.Sink
                    && excess[edge.To] > 0 && nodesInQueueHashSet.Add(edge.To)) // use nodesInQueueHashSet here to avoid duplicates and for O(1) contains check
                {
                    queue.Enqueue(edge.To);
                }
            }
        }

        return excess[graph.Sink];
    }

    public List<Node> GetMinCut()
    {
        var visited = new HashSet<Node>();
        var stack = new Stack<Node>();
        stack.Push(graph.Source);

        while (stack.Count > 0)
        {
            var node = stack.Pop();
            if (!visited.Add(node)) continue;

            foreach (var edge in graph.GetEdges(node))
            {
                if (edge.ResidualCapacity > 0 && !visited.Contains(edge.To))
                {
                    stack.Push(edge.To);
                }
            }
        }

        return [.. visited];
    }

    void InitialisePreflow()
    {
        var nodeCount = graph.GetNodes().Count;

        foreach (var node in graph.GetNodes())
        {
            height[node] = 0;
            excess[node] = 0;
        }

        height[graph.Source] = nodeCount;

        foreach (var edge in graph.GetEdges(graph.Source))
        {
            if (edge.ResidualCapacity <= 0) continue;

            var pushAmount = edge.ResidualCapacity;
            edge.Flow += pushAmount;
            edge.ReverseEdge!.Flow -= pushAmount;

            excess[edge.To] += pushAmount;
            excess[graph.Source] -= pushAmount;
        }
    }

    void Discharge(Node u)
    {
        var edges = graph.GetEdges(u);

        foreach (var edge in edges)
        {
            if (excess[u] <= 0)
            {
                break;
            }

            if (edge.ResidualCapacity > 0 && height[u] == height[edge.To] + 1)
            {
                Push(u, edge);
            }
        }

        if (excess[u] > 0)
        {
            Relabel(u);
        }
    }

    void Push(Node u, FlowEdge edge)
    {
        var pushAmount = Math.Min(excess[u], edge.ResidualCapacity);
        edge.Flow += pushAmount;
        edge.ReverseEdge!.Flow -= pushAmount;

        excess[u] -= pushAmount;
        excess[edge.To] += pushAmount;
    }

    void Relabel(Node u)
    {
        var minHeight = int.MaxValue;

        foreach (var edge in graph.GetEdges(u))
        {
            if (edge.ResidualCapacity > 0)
            {
                minHeight = Math.Min(minHeight, height[edge.To]);
            }
        }

        if (minHeight < int.MaxValue)
        {
            height[u] = minHeight + 1;
        }
    }
}
