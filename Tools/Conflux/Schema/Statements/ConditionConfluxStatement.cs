using Conflux.NodeCanvas.Nodes;
using Conflux.Schema.Conditions;
using Conflux.Schema.Context;
using Conflux.Schema.Serialization;
using YamlDotNet.Serialization;

namespace Conflux.Schema.Statements
{
    [ConfluxDerived("condition")]
    internal class ConditionConfluxStatement : ConfluxStatement
    {
        [ConfluxMainProperty]
        [YamlMember(Alias = "condition")]
        public ConfluxCondition? Condition { get; set; }

        public override ConfluxGraph BuildGraph(NodeCanvasGraphContext context)
        {
            return ConfluxGraph.CreateFromNode(context, new ConditionBTNode
            {
                Condition = Condition?.BuildCondition(context),
            });
        }
    }
}
