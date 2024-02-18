using dLabConverter.Models;

namespace dLabConverter.Inputs;

public interface IInput
{
    IEnumerable<Node> Process(Stream dataStream);
}