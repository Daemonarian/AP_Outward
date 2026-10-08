using System.ComponentModel;
using Conflux.NodeCanvas.Nodes;
using Conflux.Schema.Context;
using Conflux.Schema.Nodes;
using Conflux.Schema.Serialization;
using YamlDotNet.Serialization;

namespace Conflux.Schema.Statements
{
    [ConfluxDerived("sequence")]
    internal class SequenceConfluxStatement : ConfluxStatement
    {
        [YamlMember(Alias = "dynamic")]
        [DefaultValue(false)]
        public bool IsDynamic { get; set; } = false;

        [YamlMember(Alias = "random")]
        [DefaultValue(false)]
        public bool IsRandom { get; set; } = false;

        [ConfluxMainProperty]
        [YamlMember(Alias = "tasks")]
        public List<ConfluxBlock> Tasks { get; init; } = [];

        public override ConfluxGraph BuildGraph(NodeCanvasGraphContext context)
        {
            var node = new SequencerNodeCanvasNode(Tasks.Count)
            {
                IsDynamic = IsDynamic,
                IsRandom = IsRandom,
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
    }
}
