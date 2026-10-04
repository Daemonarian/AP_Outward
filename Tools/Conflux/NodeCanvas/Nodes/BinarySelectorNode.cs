using System.ComponentModel;
using System.Text;
using Conflux.NodeCanvas.Conditions;
using Conflux.NodeCanvas.Serialization;
using Conflux.Schema;
using Conflux.Schema.Exceptions;
using Conflux.Schema.Statements;
using Newtonsoft.Json;

namespace Conflux.NodeCanvas.Nodes
{
    [NodeCanvasType("NodeCanvas.BehaviourTrees.BinarySelector")]
    internal class BinarySelectorNode : BTNode
    {
        [JsonProperty("_condition")]
        public ConditionTask? Condition { get; set; } = null;

        [JsonProperty("dynamic")]
        [DefaultValue(false)]
        public bool IsDynamic { get; set; } = false;

        public override int OutConnectionCount => 2;

        public override string GetGraphVizShortName() => "If";

        public override string GetGraphVizContent()
        {
            var content = new StringBuilder();

            content.AppendLine(Condition?.ToGraphVizLabel() ?? string.Empty);

            if (IsDynamic)
            {
                content.AppendLine("IsDynamic: yes");
            }

            return content.ToString().TrimEnd();
        }

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
                        Condition = Condition?.BuildConfluxCondition(graph) ?? throw new ConfluxException("BinarySelectorNode requires a condition."),
                        Then = children.Count > 0 ? children[0] : new ConfluxBlock(),
                        Else = children.Count > 1 ? children[1] : new ConfluxBlock(),
                    },
                ],
            };
        }
    }
}
