using dLabConverter.Interfaces;
using dLabConverter.Models;

namespace dLabConverter;

public class NodeProcessor
{
    public async Task ProcessNodesAsync(IEnumerable<Node> nodes, IEnumerable<IOutput> outputs)
    {
        var preprocessingTasks = outputs.Select(PreProcessOutputAsync).ToList();
        await Task.WhenAll(preprocessingTasks);

        foreach (var node in nodes)
        {
            var processingTasks = outputs.Select(output => ProcessNodeForOutputAsync(node, output)).ToList();
            await Task.WhenAll(processingTasks);
        }

        var postprocessingTasks = outputs.Select(PostProcessOutputAsync).ToList();
        await Task.WhenAll(postprocessingTasks);
    }

    private async Task ProcessNodeForOutputAsync(Node node, IOutput output) =>
        await output.ProcessNodeAsync(node);


    private Task PreProcessOutputAsync(IOutput output) => output.PreProcess();
    private Task PostProcessOutputAsync(IOutput output) => output.PostProcess();
}