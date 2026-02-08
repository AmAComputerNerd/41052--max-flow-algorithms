namespace Algorithm.Utilities;

public class Edge
{
    public required int From { get; init; }
    
    public required int To { get; init; }
    
    public required int Capacity { get; init; }
    
    public int Flow { get; set; }
    
    public int ReverseIndex { get; set; }
    
    public int ResidualCapacity => Capacity - Flow;
}