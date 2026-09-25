using Conflux.Schema.Conditions;
using Conflux.Schema.Context;
using Conflux.Schema.Deserializer;
using Conflux.Schema.Nodes;
using YamlDotNet.Serialization;

namespace Conflux.Schema.Statements
{
    [ConfluxDerived("if")]
    internal class IfStatement : ConfluxStatement
    {
        [YamlMember(Alias = "condition")]
        public ConfluxCondition? Condition { get; set; }

        [YamlMember(Alias = "then")]
        public ConfluxBlock Then { get; set; } = new();

        [YamlMember(Alias = "else")]
        public ConfluxBlock Else { get; set; } = new();

        public override ConfluxGraph BuildGraph(INodeCanvasGraphContext context)
        {
            if (Condition is null)
            {
                throw new Exception("Condition must not be null.");
            }

            var condition = Condition.BuildCondition(context);
            var ifNode = context.BuildIfNode(condition);
            var thenGraph = Then.BuildGraph(context);
            var elseGraph = Else.BuildGraph(context);

            var ifGraph = ConfluxGraph.CreateFromNode(context, ifNode);
            var thenLeaf = (LeafNode)ifGraph.GetNextNode(ifNode, 0);
            var elseLeaf = (LeafNode)ifGraph.GetNextNode(ifNode, 1);

            return ifGraph.Attach(thenGraph, thenLeaf).Attach(elseGraph, elseLeaf);
        }
    }
}
