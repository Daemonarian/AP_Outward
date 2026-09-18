using System.Text.Json.Serialization;
using NodeCanvasRenderer.Schema.NodeCanvas.Framework;

namespace NodeCanvasRenderer.Schema.NodeCanvas.DialogueTrees
{
    internal class ActionNode : Node
    {
        [JsonPropertyName("_action")]
        public ActionTask? Action { get; set; }

        public override string GetGraphVizShortName() => "Do";

        public override string GetGraphVizContent() => Action?.ToGraphVizLabel() ?? string.Empty;
    }
}
