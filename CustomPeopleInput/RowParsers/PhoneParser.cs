using CustomPeopleInput.Interfaces;
using CustomPeopleInput.Services;
using Microsoft.Extensions.Logging;

namespace CustomPeopleInput.RowParsers;

public class PhoneParser(ILogger<PhoneParser>? logger, IOpenNodeService openNodeService)
    : BaseParser(openNodeService, logger), IRowParser
{
    public static readonly string ParserToken = "T";
    public override string Token => ParserToken;
    protected override string FriendlyName => "Phone";

    protected override Dictionary<int, string> DataIndexToName { get; } = new()
    {
        { 0, "Mobile" },
        { 1, "Landline" },
    };
}