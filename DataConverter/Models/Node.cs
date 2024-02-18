namespace dLabConverter.Models;

public record Node
{
    public required string SourceId;
    public required string Name;
    public required NodeType Type;
    public string? Value;
    public List<NodeAttribute>? Attributes;

    public Node Copy(NodeType? newType) => this with
    {
        Type = newType ?? Type, Attributes = Attributes != null ? new List<NodeAttribute>(Attributes) : null
    };
}