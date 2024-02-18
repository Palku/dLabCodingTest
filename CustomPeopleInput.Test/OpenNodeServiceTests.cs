using CustomPeopleInput.Services;
using dLabConverter.Models;
using Xunit;

namespace CustomPeopleInput.Test;

public class OpenNodeServiceTests
{
    [Theory]
    [InlineData(new[] { "one", "two", "three" }, new[] { "three", "two", "one" })]
    [InlineData(new[] { "a", "b" }, new[] { "b", "a" })]
    [InlineData(new[] { "ö" }, new[] { "ö" })]
    public void RevereClosing(string[] tokens, string[] expectedResult)
    {
        var service = new OpenNodeService();
        foreach (var token in tokens)
            service.AddOpenNode(new Node { Name = $"token-{token}", SourceId = token, Type = NodeType.StartNode },
                token);

        var result = service.CloseAllOpenNodes().ToList();
        Assert.Equal(expectedResult.Length, result.Count);
        for (int i = 0; i < expectedResult.Length; i++)
        {
            Assert.Equal($"token-{expectedResult[i]}", result[i].Name);
            Assert.Equal(expectedResult[i], result[i].SourceId);
            Assert.Equal(NodeType.EndNode, result[i].Type);
        }
    }

    [Theory]
    [InlineData(new[] { "one", "two", "three" }, new[] { "three", "two", "one" })]
    [InlineData(new[] { "one", "two", "three" }, new[] { "three", "one" })]
    [InlineData(new[] { "a", "b" }, new[] { "b", "a" })]
    [InlineData(new[] { "a", "b" }, new[] { "b" })]
    [InlineData(new[] { "ö" }, new[] { "ö" })]
    public void CloseByToken(string[] tokens, string[] closeTokens)
    {
        var service = new OpenNodeService();
        foreach (var token in tokens)
            service.AddOpenNode(new Node { Name = $"token-{token}", SourceId = token, Type = NodeType.StartNode },
                token);
        var result = service.CloseNodesByParserToken(closeTokens);
        Assert.True(result.All(c => closeTokens.Contains(c.SourceId)));
    }
}