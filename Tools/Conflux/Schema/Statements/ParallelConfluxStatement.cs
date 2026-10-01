using System.ComponentModel;
using Conflux.NodeCanvas.Nodes;
using Conflux.Schema.Context;
using Conflux.Schema.Nodes;
using Conflux.Schema.Serialization;
using YamlDotNet.Serialization;

namespace Conflux.Schema.Statements
{
    [ConfluxDerived("parallel")]
    internal class ParallelConfluxStatement : ConfluxStatement
    {
        [YamlMember(Alias = "policy")]
        [DefaultValue(ParallelPolicy.FirstFailure)]
        public ParallelPolicy Policy { get; init; } = ParallelPolicy.FirstFailure;

        [ConfluxMainProperty]
        [YamlMember(Alias = "tasks")]
        public List<ConfluxBlock> Tasks { get; init; } = [];

        public override ConfluxGraph BuildGraph(NodeCanvasGraphContext context)
        {
            var node = new ParallelNode(Tasks.Count)
            {
                Policy = Policy switch
                {
                    ParallelPolicy.FirstFailure => ParallelNode.ParallelPolicy.FirstFailure,
                    ParallelPolicy.FirstSuccess => ParallelNode.ParallelPolicy.FirstSuccess,
                    ParallelPolicy.FirstSuccessOrFailure => ParallelNode.ParallelPolicy.FirstSuccessOrFailure,
                    _ => throw new Exception($"Unexpected parallel policy: {Policy}"),
                },
            };

            var graph = ConfluxGraph.CreateFromNode(context, node);
            for (var i = 0; i < Tasks.Count; i++)
            {
                var task = Tasks[i];
                var taskLeaf = (LeafNode)graph.GetNextNode(node, i);
                var taskGraph = task.BuildGraph(context);
                graph = graph.Attach(taskGraph, taskLeaf);
            }

            return graph;
        }

        public enum ParallelPolicy
        {
            FirstFailure,
            FirstSuccess,
            FirstSuccessOrFailure,
        }
    }
}
