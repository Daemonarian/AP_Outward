using System.ComponentModel;
using Conflux.NodeCanvas.Nodes;
using Conflux.Schema.Conditions;
using Conflux.Schema.Context;
using Conflux.Schema.Nodes;
using Conflux.Schema.References;
using Conflux.Schema.Serialization;
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
        [DefaultValue(0f)]
        public float AvailableTime { get; set; } = 0f;

        [YamlMember(Alias = "saySelection")]
        [DefaultValue(false)]
        public bool SaySelection { get; set; } = false;

        public override ConfluxGraph BuildGraph(NodeCanvasGraphContext context)
        {
            var node = new MultipleChoiceNode
            {
                AvailableTime = AvailableTime,
                SaySelection = SaySelection,
                Choices = [.. Choices.Select(choice => choice.BuildChoice(context))],
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
            public ConfluxStatementReference Statement { get; set; } = new();

            [YamlMember(Alias = "condition")]
            public ConfluxCondition? Condition { get; set; } = null;

            [YamlMember(Alias = "unfolded")]
            public bool IsUnfolded { get; set; } = true;

            [YamlMember(Alias = "then")]
            public ConfluxBlock Then { get; set; } = new();

            public MultipleChoiceNode.Choice BuildChoice(NodeCanvasGraphContext context) => new()
            {
                Statement = Statement.BuildNodeCanvasObject(context),
                Condition = Condition?.BuildCondition(context),
                IsUnfolded = IsUnfolded
            };
        }
    }
}
