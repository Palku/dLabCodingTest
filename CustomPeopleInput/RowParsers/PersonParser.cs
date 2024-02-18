using CustomPeopleInput.Interfaces;
using CustomPeopleInput.Services;
using Microsoft.Extensions.Logging;

namespace CustomPeopleInput.RowParsers;

public class PersonParser(ILogger<PersonParser>? logger, IOpenNodeService openNodeService)
    : BaseParser(openNodeService, logger), IRowParser
{
    public static readonly string ParserToken = "P";
    public override string Token => ParserToken;
    protected override string[] Closes => [FamilyParser.ParserToken, ParserToken];
    protected override string FriendlyName => "Person";
    protected override bool LeaveOpen => true;

    protected override Dictionary<int, string> DataIndexToName { get; } = new()
    {
        { 0, "First Name" },
        { 1, "Last Name" },
    };
}