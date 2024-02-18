using dLabConverter.Models;

namespace XmlOutput;

public static class NodeExtensions
{
    public static string NameFullyTrimmedLowerCase(this Node node) => node.Name.Replace(" ", "").ToLower();
}