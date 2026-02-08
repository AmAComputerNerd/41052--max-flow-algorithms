namespace DinicsAlgorithm.Graph;

public class Edge(Node from, Node to, int capacity)
{
    public Node From { get; } = from;

    public Node To { get; } = to;

    public int Capacity { get; } = capacity;
    
    public int Flow { get; set; }

    public Edge? ReverseEdge { get; set; }
        
    public int ResidualCapacity => Capacity - Flow;
}