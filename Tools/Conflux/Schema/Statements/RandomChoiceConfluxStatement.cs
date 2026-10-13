using System.ComponentModel;
using Conflux.NodeCanvas.Nodes;
using Conflux.Schema.Blackboards;
using Conflux.Schema.Context;
using Conflux.Schema.Exceptions;
using Conflux.Schema.Nodes;
using Conflux.Schema.Serialization;
using YamlDotNet.Serialization;

namespace Conflux.Schema.Statements
{
    [ConfluxDerived("random")]
    internal class RandomChoiceConfluxStatement : ConfluxStatement
    {
        [YamlIgnore]
        public List<Choice> Choices { get; set; } = [];

        [ConfluxMainProperty]
        [YamlMember(Alias = "choices")]
        public List<Choice>? ChoicesProxy
        {
            get => Choices.Count == 0 ? null : Choices;
            set => Choices = value ?? [];
        }

        [YamlMember(Alias = "failChance")]
        [DefaultValue(0f)]
        public ConfluxBlackboardVariableReference<float> FailChance { get; set; } = 0f;

        public override bool AcceptsChildBlocks => true;

        public override IReadOnlyList<ConfluxBlock> ChildBlocks
        {
            get => [.. Choices.Select(c => c.Then)];

            set
            {
                if (Choices.Count < value.Count)
                {
                    throw new ConfluxValueException($"Too many child blocks. Expected {Choices.Count}, instead got {value.Count}.");
                }

                foreach (var (i, choice) in Choices.Index())
                {
                    var childBlock = i < value.Count ? value[i] : new();
                    choice.Then = childBlock;
                }
            }
        }

        public override ConfluxGraph BuildGraph(NodeCanvasGraphContext context)
        {
            var node = new ProbabilitySelectorNode
            {
                ChildWeights = [.. Choices.Select(c => c.Weight.BuildBBParameter(context))],
                FailChance = FailChance.BuildBBParameter(context),
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
            [YamlMember(Alias = "weight")]
            [DefaultValue(1f)]
            public ConfluxBlackboardVariableReference<float> Weight { get; set; } = 1f;

            [YamlIgnore]
            public ConfluxBlock Then { get; set; } = new();

            [YamlMember(Alias = "then")]
            public ConfluxBlock? ThenProxy
            {
                get => Then.IsEmpty() ? null : Then;
                set => Then = value ?? new();
            }
        }
    }
}
