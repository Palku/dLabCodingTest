using dLabConverter.Models;

namespace CustomPeopleInput.Services;

/// <summary>
/// This is used to keep track of dangling nodes and when to close them as the custom format 
/// uses relations between tokens to decide when a node should be closed.
/// </summary>
public class OpenNodeService : IOpenNodeService
{
    private readonly List<(Node Node, string Token)> _openNodes = [];

    /// <summary>
    /// Adds an open node to the chain.
    /// </summary>
    /// <param name="node">The start node that is left dangling</param>
    /// <param name="token">The token from the RowParser that left the node open</param>
    public void AddOpenNode(Node node, string token) => _openNodes.Add((node, token));

    /// <summary>
    /// Iterates all open nodes and closes all that is matching.
    /// Currently the format doesnt support nesting of the same type so multiple nodes with the same
    /// token shouldn't exist
    /// </summary>
    /// <param name="tokens">All the tokens that should be closed, will be processed in received order</param>
    /// <returns></returns>
    public IEnumerable<Node> CloseNodesByParserToken(string[] tokens)
    {
        foreach (var token in tokens)
        {
            foreach (var element in _openNodes
                         .ToArray()
                         .Where(c => c.Token == token))
            {
                yield return element.Node.Copy(NodeType.EndNode);
                _openNodes.Remove(element);
            }
        }
    }
    /// <summary>
    /// Closes all remaining dangling nodes in reverse order to keep correct nesting.
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Node> CloseAllOpenNodes()
    {
        foreach (var node in _openNodes
                     .Select(c => c.Node)
                     .Reverse())
        {
            yield return node.Copy(NodeType.EndNode);
        }

        _openNodes.Clear();
    }
}

public interface IOpenNodeService
{
    void AddOpenNode(Node node, string key);
    IEnumerable<Node> CloseNodesByParserToken(string[] tokens);
    IEnumerable<Node> CloseAllOpenNodes();
}