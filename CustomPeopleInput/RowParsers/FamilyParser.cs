using CustomPeopleInput.Interfaces;
using CustomPeopleInput.Services;
using Microsoft.Extensions.Logging;

namespace CustomPeopleInput.RowParsers;

public class FamilyParser(ILogger<FamilyParser>? logger, IOpenNodeService openNodeService)
    : BaseParser(openNodeService, logger), IRowParser
{
    public static readonly string ParserToken = "F";
    public override string Token => ParserToken;
    protected override string[] Closes => [ParserToken];
    protected override string FriendlyName => "Family";
    protected override bool LeaveOpen => true;
    protected override Dictionary<int, string> DataIndexToName { get; } = new()
    {
        { 0, "Name" },
        { 1, "Born" },
    };
}