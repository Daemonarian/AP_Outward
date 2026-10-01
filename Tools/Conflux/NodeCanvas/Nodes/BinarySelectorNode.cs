using System.Text;
using Conflux.NodeCanvas.Conditions;
using Conflux.NodeCanvas.Serialization;
using Newtonsoft.Json;

namespace Conflux.NodeCanvas.Nodes
{
    [NodeCanvasType("NodeCanvas.BehaviourTrees.BinarySelector")]
    internal class BinarySelectorNode : BTNode
    {
        [JsonProperty("_condition")]
        public ConditionTask? Condition { get; set; } = null;

        [JsonProperty("dynamic")]
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
    }
}
