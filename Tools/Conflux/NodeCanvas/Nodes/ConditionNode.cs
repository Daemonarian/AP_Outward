using Conflux.NodeCanvas.Conditions;
using Conflux.NodeCanvas.Serialization;
using Conflux.Schema;
using Conflux.Schema.Exceptions;
using Conflux.Schema.Statements;
using Newtonsoft.Json;

namespace Conflux.NodeCanvas.Nodes
{
    [NodeCanvasType("NodeCanvas.DialogueTrees.ConditionNode")]
    internal class ConditionNode : DTNode
    {
        [JsonProperty("_condition")]
        public ConditionTask? Condition { get; set; }

        public override int OutConnectionCount => 2;

        public override string GetGraphVizShortName() => "If";

        public override string GetGraphVizContent() => Condition?.ToGraphVizLabel() ?? string.Empty;

        public override string? GetGraphVizOutConnectionLabel(int index) => index switch
        {
            0 => "yes",
            1 => "no",
            _ => null
        };

        public override ConfluxBlock BuildConfluxBlock(Graph graph, List<ConfluxBlock> children)
        {
            if (children.Count > OutConnectionCount)
            {
                throw new ConfluxException($"ConditionNode can only have {OutConnectionCount} children.");
            }

            var statement = new IfConfluxStatement
            {
                Condition = Condition?.BuildConfluxCondition(graph) ?? throw new ConfluxException("ConditionNode requires a condition."),
                Then = children.Count > 0 ? children[0] : new ConfluxBlock(),
                Else = children.Count > 1 ? children[1] : new ConfluxBlock(),
            };

            return new ConfluxBlock
            {
                Statements = [
                    new IfConfluxStatement
                    {
                        Condition = Condition?.BuildConfluxCondition(graph) ?? throw new ConfluxException("ConditionNode requires a condition."),
                        Then = children.Count > 0 ? children[0] : new ConfluxBlock(),
                        Else = children.Count > 1 ? children[1] : new ConfluxBlock(),
                    },
                ],
            };
        }
    }
}
