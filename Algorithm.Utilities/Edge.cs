namespace Algorithm.Utilities;

public class Edge
{
    public required Node From { get; init; }
    
    public required Node To { get; init; }
    
    public required int Capacity { get; init; }
    
    public int Flow { get; init; }
    
    public int ResidualCapacity => Capacity - Flow;
}