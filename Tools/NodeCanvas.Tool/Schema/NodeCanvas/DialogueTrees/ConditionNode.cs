using Newtonsoft.Json;
using NodeCanvas.Tool.Schema.NodeCanvas.Framework;
using NodeCanvas.Tool.Schema.NodeCanvas.Serialization;

namespace NodeCanvas.Tool.Schema.NodeCanvas.DialogueTrees
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
