using dLabConverter.Inputs;
using dLabConverter.Interfaces;

namespace dLabConverter;

public class DataConverter(IInput input)
{
    private readonly List<IOutput> _outputs = [];
    private readonly NodeProcessor _nodeProcessor = new();

    public DataConverter SetInput(IInput input1)
    {
        input = input1;
        return this;
    }

    public DataConverter AddOutput(IOutput output)
    {
        _outputs.Add(output);
        return this;
    }

    public Task ProcessData(Stream inputData) => _nodeProcessor.ProcessNodesAsync(input.Process(inputData), _outputs);
}