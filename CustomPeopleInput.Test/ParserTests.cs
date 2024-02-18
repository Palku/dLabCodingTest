using CustomPeopleInput.RowParsers;
using CustomPeopleInput.Services;
using dLabConverter.Models;

namespace CustomPeopleInput.Test;

public class ParserTests
{
    [Theory]
    [InlineData("A|Drottningholms slott|Stockholm|10001\n", 3)]
    [InlineData("A|Drottningholms slott|Stockholm\n", 2)]
    [InlineData("A|Drottningholms slott\n", 1)]
    public void AddressParserDataParsing(string input, int expectedValueCount)
    {
        var parser = new AddressParser(null, new OpenNodeService());
        var createdValueCount = parser
            .Process(input.Split("|")[1..])
            .Count(c => c.Type == NodeType.ValueNode && !string.IsNullOrEmpty(c.Value));
        Assert.Equal(expectedValueCount, createdValueCount);
    }
}