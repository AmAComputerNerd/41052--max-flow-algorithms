namespace DinicsAlgorithm.Graph;

public class FlowEdge(Node to, int capacity)
{
    public Node To { get; } = to;

    private int Capacity { get; } = capacity;
    
    public int Flow { get; set; }

    public FlowEdge? ReverseEdge { get; set; }
        
    public int ResidualCapacity => Capacity - Flow;
}