using System.Text.Json.Serialization;
using NodeCanvasRenderer.Schema.NodeCanvas.Framework;

namespace NodeCanvasRenderer.Schema.NodeCanvas.DialogueTrees
{
    internal class ConditionNode : Node
    {
        [JsonPropertyName("_condition")]
        public ConditionTask? Condition { get; set; }

        public override string GetGraphVizShortName() => "If";

        public override string GetGraphVizContent() => Condition?.ToGraphVizLabel() ?? string.Empty;
    }
}
