using System.Text;
using Autofac;
using CustomPeopleInput.Interfaces;
using CustomPeopleInput.Services;
using dLabConverter.Inputs;
using dLabConverter.Models;
using Microsoft.Extensions.Logging;

namespace CustomPeopleInput;

public class PeopleInput : IInput
{
    private readonly string _tokenDelimiter = "|";
    private readonly Dictionary<string, IRowParser> _tokenParsers;
    private readonly OpenNodeService _openNodeService = new();

    public PeopleInput()
    {
        var builder = new ContainerBuilder();
        //Injecting Microsoft's Logging parts, also a console implementation 
        builder.RegisterGeneric(typeof(Logger<>)).As(typeof(ILogger<>)).SingleInstance();
        builder.Register(c => LoggerFactory.Create(factory => { factory.AddConsole(); })).As<ILoggerFactory>()
            .SingleInstance();
        //Services
        builder.RegisterInstance(_openNodeService).As<IOpenNodeService>().SingleInstance();
        //Creating instances of all the token parsers in the project
        builder.RegisterAssemblyTypes(typeof(IRowParser).Assembly)
            .Where(t => typeof(IRowParser).IsAssignableFrom(t) && !t.IsInterface)
            .AsImplementedInterfaces();

        var container = builder.Build();
        _tokenParsers = container.Resolve<IEnumerable<IRowParser>>().ToDictionary(c => c.Token);
    }

    public IEnumerable<Node> Process(Stream dataStream)
    {
        foreach (var line in ReadLines(dataStream))
        {
            var parts = line.Split(_tokenDelimiter, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 0) continue;

            var parser = GetParser(parts[0]);

            foreach (var node in parser.Process(parts[1..]))
                yield return node;
        }

        foreach (var command in _openNodeService.CloseAllOpenNodes())
            yield return command;
    }

    private IRowParser GetParser(string token)
    {
        if (!_tokenParsers.TryGetValue(token, out IRowParser? parser))
            throw new KeyNotFoundException($"No token parser implemented for [{token}]");
        return parser;
    }

    private IEnumerable<string> ReadLines(Stream dataStream)
    {
        using var reader = new StreamReader(dataStream, Encoding.UTF8);
        while (reader.ReadLine() is { } line)
            yield return line;
    }
}