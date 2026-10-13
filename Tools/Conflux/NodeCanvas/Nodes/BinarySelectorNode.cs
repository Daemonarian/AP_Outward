using System.ComponentModel;
using Conflux.GraphViz;
using Conflux.NodeCanvas.Conditions;
using Conflux.NodeCanvas.Serialization;
using Conflux.Schema;
using Conflux.Schema.Exceptions;
using Conflux.Schema.Statements;
using Newtonsoft.Json;

namespace Conflux.NodeCanvas.Nodes
{
    [NodeCanvasType("NodeCanvas.BehaviourTrees.BinarySelector")]
    [GraphVizObject(Alias = "If")]
    internal class BinarySelectorNode : BTNode
    {
        [JsonProperty("_condition")]
        [GraphVizProperty(IsMainProperty = true)]
        public NodeCanvasCondition? Condition { get; set; } = null;

        [JsonProperty("dynamic")]
        [DefaultValue(false)]
        public bool IsDynamic { get; set; } = false;

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
                throw new ConfluxException($"BinarySelectorNode can only have {OutConnectionCount} children.");
            }

            if (IsDynamic)
            {
                throw new ConfluxException("BinarySelectorNode does not support dynamic behavior.");
            }

            return new ConfluxBlock
            {
                Statements = [
                    new IfConfluxStatement
                    {
                        Condition = Condition?.BuildConfluxCondition(graph),
                        Then = children.Count > 0 ? children[0] : new ConfluxBlock(),
                        Else = children.Count > 1 ? children[1] : new ConfluxBlock(),
                    },
                ],
            };
        }
    }
}
