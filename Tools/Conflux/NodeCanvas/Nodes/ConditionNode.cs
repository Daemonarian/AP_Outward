using Conflux.GraphViz;
using Conflux.NodeCanvas.Conditions;
using Conflux.NodeCanvas.Serialization;
using Conflux.Schema;
using Conflux.Schema.Exceptions;
using Conflux.Schema.Statements;
using Newtonsoft.Json;

namespace Conflux.NodeCanvas.Nodes
{
    [NodeCanvasType("NodeCanvas.DialogueTrees.ConditionNode")]
    [GraphVizObject(Alias = "If")]
    internal class ConditionNode : DTNode
    {
        [JsonProperty("_condition")]
        [GraphVizProperty(IsMainProperty = true)]
        public NodeCanvasCondition? Condition { get; set; }

        public override int OutConnectionCount => 2;

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
                Condition = Condition?.BuildConfluxCondition(graph),
                Then = children.Count > 0 ? children[0] : new ConfluxBlock(),
                Else = children.Count > 1 ? children[1] : new ConfluxBlock(),
            };

            return new ConfluxBlock
            {
                Statements = [statement],
            };
        }
    }
}
