using CustomPeopleInput.Interfaces;
using CustomPeopleInput.Services;
using Microsoft.Extensions.Logging;

namespace CustomPeopleInput.RowParsers;

public class AddressParser(ILogger<AddressParser>? logger, IOpenNodeService openNodeService)
    : BaseParser(openNodeService, logger), IRowParser
{
    public static readonly string ParserToken = "A";
    public override string Token => ParserToken;
    protected override string FriendlyName => "Address";

    protected override Dictionary<int, string> DataIndexToName { get; } = new()
    {
        { 0, "Street" },
        { 1, "City" },
        { 2, "Postal Code" }
    };
}