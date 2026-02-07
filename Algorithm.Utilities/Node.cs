namespace Algorithm.Utilities;

public class Node
{
    public required int Id { get; init; }
    
    public string? Label { get; init; } = string.Empty;

    public static bool operator ==(Node a, Node b) => a.Id == b.Id;

    public static bool operator !=(Node a, Node b) => !(a == b);
}