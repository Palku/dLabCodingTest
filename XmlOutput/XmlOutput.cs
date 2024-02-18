using System.Xml;
using dLabConverter.Interfaces;
using dLabConverter.Models;

namespace XmlOutput;

public class XmlOutput(string filename) : IOutput
{
    private XmlWriter _xmlWriter = null!;

    private XmlWriter CreateWriter()
    {
        XmlWriterSettings settings = new XmlWriterSettings
        {
            Indent = true,
            Async = true
        };
        FileStream fileStream = new FileStream(filename, FileMode.Create);
        XmlWriter xmlWriter = XmlWriter.Create(fileStream, settings);
        return xmlWriter;
    }

    public async Task PreProcess()
    {
        _xmlWriter = CreateWriter();
        await _xmlWriter.WriteStartDocumentAsync();
        await _xmlWriter.WriteStartElementAsync(null, "people", null);
    }

    public async Task ProcessNodeAsync(Node node)
    {
        switch (node.Type)
        {
            case NodeType.StartNode:
                await _xmlWriter.WriteStartElementAsync(null, node.NameFullyTrimmedLowerCase(), null);
                break;
            case NodeType.EndNode:
                await _xmlWriter.WriteEndElementAsync();
                break;
            case NodeType.ValueNode:
                await _xmlWriter.WriteElementStringAsync(null, node.NameFullyTrimmedLowerCase(), null, node.Value);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public async Task PostProcess()
    {
        await _xmlWriter.WriteEndElementAsync();
        await _xmlWriter.WriteEndDocumentAsync();
        await _xmlWriter.DisposeAsync();
    }
}