using dLabConverter.Interfaces;
using dLabConverter.Models;

namespace RawConsoleOutput;

public class ConsoleOutput : IOutput
{
    public Task PreProcess()
    {
        Console.WriteLine("Starting processing");
        return Task.FromResult(0);
    }

    public Task ProcessNodeAsync(Node node)
    {
        Console.WriteLine(node);
        return Task.FromResult(0);
    }

    public Task PostProcess()
    {
        Console.WriteLine("Completed processing");
        return Task.FromResult(0);
    }
}