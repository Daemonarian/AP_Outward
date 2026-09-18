using System.Text.Json.Serialization;
using NodeCanvas.Tool.Schema.NodeCanvas.Framework;

namespace NodeCanvas.Tool.Schema.NodeCanvas.DialogueTrees
{
    internal class ActionNode : Node
    {
        [JsonPropertyName("_action")]
        public ActionTask? Action { get; set; }

        public override string GetGraphVizShortName() => "Do";

        public override string GetGraphVizContent() => Action?.ToGraphVizLabel() ?? string.Empty;
    }
}
