using CustomPeopleInput.Services;
using dLabConverter.Models;
using Microsoft.Extensions.Logging;

namespace CustomPeopleInput.RowParsers;

public abstract class BaseParser(IOpenNodeService openNodeService, ILogger? logger)
{
    protected virtual string[] Closes => [];
    protected abstract Dictionary<int, string> DataIndexToName { get; }
    public abstract string Token { get; }
    protected abstract string FriendlyName { get; }
    protected virtual bool LeaveOpen { get; } = false;

    public virtual IEnumerable<Node> Process(string[] data)
    {
        foreach (var node in CloseOpenNodes(Closes))
            yield return node;

        var primaryNode = new Node { Name = FriendlyName, Value = null, Type = NodeType.StartNode, SourceId = Token };
        yield return primaryNode;

        foreach (var node in ParseData(data))
            yield return node;

        if (LeaveOpen)
            openNodeService.AddOpenNode(primaryNode, Token);
        else
            yield return primaryNode.Copy(NodeType.EndNode);
    }

    private IEnumerable<Node> CloseOpenNodes(string[] closes)
    {
        foreach (var node in openNodeService.CloseNodesByParserToken(closes))
            yield return node;
    }

    private IEnumerable<Node> ParseData(string[] data)
    {
        for (var x = 0; x < data.Length; x++)
        {
            if (DataIndexToName.TryGetValue(x, out var key))
                yield return new Node { Name = key, Value = data[x], Type = NodeType.ValueNode, SourceId = Token };
            else
                logger?.Log(LogLevel.Warning, "Unexpected argument passed to parser : {argument}", data[x]);
        }
    }
}