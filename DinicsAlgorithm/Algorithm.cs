using DinicsAlgorithm.Graph;
using DinicsAlgorithm.Utils;

namespace DinicsAlgorithm;

public class Algorithm(FlowGraph graph)
{
    public int FindMaxFlow()
    {
        var maxFlow = 0;

        while (true)
        {
            var levels = TraversalUtils.BuildLevelGraph(graph, graph.Source);
            if (!levels.ContainsKey(graph.Sink))
            {
                break;
            }
            
            var nextEdgeIndex = new Dictionary<Node, int>();

            while (true)
            {
                var bottleneckCapacity = TraversalUtils.GetBottleneckCapacity(graph, graph.Source, graph.Sink, int.MaxValue, levels, nextEdgeIndex);
                if (bottleneckCapacity == 0)
                {
                    break;
                }

                maxFlow += bottleneckCapacity;
            }
        }
        
        return maxFlow;
    }

    public List<Edge> GetFlowEdges() => (from node in graph.Nodes from edge in graph.GetEdges(node) where edge.Capacity > 0 select edge).ToList();
}