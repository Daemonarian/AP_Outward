using Conflux.NodeCanvas;
using Conflux.NodeCanvas.Nodes;
using Conflux.Schema.Conditions;
using Conflux.Schema.Context;
using Conflux.Schema.Deserializer;
using Conflux.Schema.Nodes;
using YamlDotNet.Serialization;

namespace Conflux.Schema.Statements
{
    [ConfluxDerived("choice")]
    internal class ChoiceConfluxStatement : ConfluxStatement
    {
        [ConfluxMainProperty]
        [YamlMember(Alias = "choices")]
        public List<Choice> Choices { get; set; } = [];

        [YamlMember(Alias = "availableTime")]
        public float AvailableTime { get; set; } = 0f;

        [YamlMember(Alias = "saySelection")]
        public bool SaySelection { get; set; } = false;

        public override ConfluxGraph BuildGraph(INodeCanvasGraphContext context)
        {
            var node = new MultipleChoiceNode
            {
                AvailableTime = AvailableTime,
                SaySelection = SaySelection,
                Choices = [.. Choices.Select(choice => new MultipleChoiceNode.Choice
                {
                    Statement = choice.Statement,
                    Condition = choice.Condition?.BuildCondition(context),
                    IsUnfolded = choice.IsUnfolded,
                })],
            };

            var graph = ConfluxGraph.CreateFromNode(context, node);
            for (var i = 0; i < Choices.Count; i++)
            {
                var choice = Choices[i];
                var leaf = (LeafNode)graph.GetNextNode(node, i);
                var thenGraph = choice.Then.BuildGraph(context);
                graph = graph.Attach(thenGraph, leaf);
            }

            return graph;
        }

        public class Choice
        {
            [YamlMember(Alias = "statement")]
            public Statement Statement { get; set; } = new();

            [YamlMember(Alias = "condition")]
            public ConfluxCondition? Condition { get; set; } = null;

            [YamlMember(Alias = "unfolded")]
            public bool IsUnfolded { get; set; } = true;

            [YamlMember(Alias = "then")]
            public ConfluxBlock Then { get; set; } = new();
        }
    }
}
