using Conflux.NodeCanvas.Conditions;
using Conflux.NodeCanvas.Serialization;
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
    }
}
