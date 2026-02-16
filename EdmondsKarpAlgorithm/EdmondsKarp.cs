using Algorithm.Common;

namespace EdmondsKarpAlgorithm
{
    public class EdmondsKarp
    {
        private readonly FlowGraph _graph;

        public EdmondsKarp(FlowGraph graph)
        {
            _graph = graph;
        }

        public int FindMaxFlow(Node source, Node sink)
        {
            int maxFlow = 0;

            var augmentingPath = FindAugmentingPath(source, sink);

            while (augmentingPath != null)
            {
                int bottleneck = ComputeBottleneckCapacity(augmentingPath, source, sink);

                UpdateFlow(augmentingPath, source, sink, bottleneck);

                maxFlow += bottleneck;

                augmentingPath = FindAugmentingPath(source, sink);
            }

            return maxFlow;
        }

        private Dictionary<Node, FlowEdge?>? FindAugmentingPath(Node source, Node sink)
        {
            var augmentingPath = new Dictionary<Node, FlowEdge?>();
            var BFSQueue = new Queue<Node>();
            BFSQueue.Enqueue(source);
            augmentingPath[source] = null;

            while (BFSQueue.Count > 0 && !augmentingPath.ContainsKey(sink))
            {
                var u = BFSQueue.Dequeue();

                foreach (var edge in _graph.GetEdges(u))
                {
                    var v = edge.To;
                    if (!augmentingPath.ContainsKey(v) && edge.ResidualCapacity > 0)
                    {
                        augmentingPath[v] = edge;
                        BFSQueue.Enqueue(v);
                    }
                }
            }

            return !augmentingPath.ContainsKey(sink) ? null : augmentingPath;
        }

        private int ComputeBottleneckCapacity(Dictionary<Node, FlowEdge?> parent, Node source, Node sink)
        {
            int bottleneck = int.MaxValue;
            for (var v = sink; v != source; v = parent[v]!.ReverseEdge!.To)
            {
                bottleneck = Math.Min(bottleneck, parent[v]!.ResidualCapacity);
            }
            return bottleneck;
        }

        private void UpdateFlow(Dictionary<Node, FlowEdge?> parent, Node source, Node sink, int bottleneck)
        {
            for (var v = sink; v != source; v = parent[v]!.ReverseEdge!.To)
            {
                var edge = parent[v]!;
                edge.Flow += bottleneck;
                edge.ReverseEdge!.Flow -= bottleneck;
            }
        }
    }
}
