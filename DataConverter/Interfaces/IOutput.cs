using dLabConverter.Models;

namespace dLabConverter.Interfaces;

public interface IOutput
{
    Task PreProcess();
    Task ProcessNodeAsync(Node node);
    Task PostProcess();
}