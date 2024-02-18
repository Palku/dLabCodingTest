using dLabConverter.Models;

namespace CustomPeopleInput.Interfaces;

public interface IRowParser
{
    string Token { get; }
    IEnumerable<Node> Process(string[] data);
}