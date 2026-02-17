using Algorithm.Common;

namespace DinicsAlgorithm;

public class Dinics(FlowGraph graph) : IMaxFlowAlgorithm
{
    public int FindMaxFlow()
    {
        var maxFlow = 0;
        
        while (true)
        {
            var levels = BuildLevelGraph(graph, graph.Source);
            if (!levels.ContainsKey(graph.Sink)) break;
            
            var nextEdgeIndex = new Dictionary<Node, int>();
            while (true)
            {
                var bottleneckCapacity = GetBottleneckCapacity(graph, graph.Source, graph.Sink, int.MaxValue, levels, nextEdgeIndex);
                if (bottleneckCapacity == 0) break;

                maxFlow += bottleneckCapacity;
            }
        }
        
        return maxFlow;
    }
    
    private static Dictionary<Node, int> BuildLevelGraph(FlowGraph graph, Node source)
    {
        var levels = new Dictionary<Node, int>();
        var queue = new Queue<Node>();

        levels[source] = 0;
        queue.Enqueue(source);

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            foreach (var edge in graph.GetEdges(current).Where(edge => edge.ResidualCapacity > 0 && !levels.ContainsKey(edge.To)))
            {
                levels[edge.To] = levels[current] + 1;
                queue.Enqueue(edge.To);
            }
        }

        return levels;
    }

    private static int GetBottleneckCapacity(
        FlowGraph graph,
        Node current,
        Node sink,
        int flow,
        Dictionary<Node, int> levels,
        Dictionary<Node, int> nextEdgeIndex)
    {
        if (current == sink)
        {
            return flow;
        }

        var edges = graph.GetEdges(current);

        nextEdgeIndex.TryAdd(current, 0);
        for (var i = nextEdgeIndex[current]; i < edges.Count; i++)
        {
            var edge = edges[i];

            if (edge.ResidualCapacity > 0 && levels.TryGetValue(edge.To, out var value) && value == levels[current] + 1)
            {
                var bottleneck = Math.Min(flow, edge.ResidualCapacity);
                var pushedFlow = GetBottleneckCapacity(graph, edge.To, sink, bottleneck, levels, nextEdgeIndex);

                if (pushedFlow > 0)
                {
                    edge.Flow += pushedFlow;
                    edge.ReverseEdge!.Flow -= pushedFlow;
                    nextEdgeIndex[current] = i;
                    return pushedFlow;
                }
            }

            nextEdgeIndex[current] = i + 1;
        }

        return 0;
    }
}