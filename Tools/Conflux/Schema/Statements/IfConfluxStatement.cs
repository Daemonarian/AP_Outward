using Conflux.NodeCanvas;
using Conflux.NodeCanvas.Nodes;
using Conflux.Schema.Conditions;
using Conflux.Schema.Context;
using Conflux.Schema.Nodes;
using Conflux.Schema.Serialization;
using YamlDotNet.Serialization;

namespace Conflux.Schema.Statements
{
    [ConfluxDerived("if")]
    internal class IfConfluxStatement : ConfluxStatement
    {
        [YamlMember(Alias = "condition")]
        public ConfluxCondition? Condition { get; set; }

        [YamlIgnore]
        public ConfluxBlock Then { get; set; } = new();

        [YamlMember(Alias = "then")]
        public ConfluxBlock? ThenProxy
        {
            get => Then.IsEmpty() ? null : Then;
            set => Then = value ?? new();
        }

        [YamlIgnore]
        public ConfluxBlock Else { get; set; } = new();

        [YamlMember(Alias = "else")]
        public ConfluxBlock? ElseProxy
        {
            get => Else.IsEmpty() ? null : Else;
            set => Else = value ?? new();
        }

        public override ConfluxGraph BuildGraph(NodeCanvasGraphContext context)
        {
            if (Condition is null)
            {
                throw new Exception("Condition must not be null.");
            }

            var condition = Condition.BuildCondition(context);
            Node ifNode = context.GraphType switch
            {
                Graph.GraphType.BehaviourTree => new BinarySelectorNode
                {
                    Condition = condition,
                },
                Graph.GraphType.DialogueTree => new ConditionNode
                {
                    Condition = condition,
                },
                _ => throw new NotImplementedException($"If statements for graph type {context.GraphType} is not implemented."),
            };

            var ifGraph = ConfluxGraph.CreateFromNode(context, ifNode);
            var thenLeaf = (LeafNode)ifGraph.GetNextNode(ifNode, 0);
            var elseLeaf = (LeafNode)ifGraph.GetNextNode(ifNode, 1);

            var thenGraph = Then.BuildGraph(context);
            ifGraph = ifGraph.Attach(thenGraph, thenLeaf);

            var elseGraph = Else.BuildGraph(context);
            ifGraph = ifGraph.Attach(elseGraph, elseLeaf);

            return ifGraph;
        }
    }
}
