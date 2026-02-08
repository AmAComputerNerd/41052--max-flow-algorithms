namespace DinicsAlgorithm.Graph;

public class Node(string id)
{
    public string Id { get; } = id;
    
    protected bool Equals(Node other) => Id == other.Id;

    public override bool Equals(object? obj) => obj is Node node && this == node;
    
    public override int GetHashCode() => Id.GetHashCode();

    public static bool operator ==(Node left, Node right) => left.Id == right.Id;

    public static bool operator !=(Node left, Node right) => !(left == right);
}