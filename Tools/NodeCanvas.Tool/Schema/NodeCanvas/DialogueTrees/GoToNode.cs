using System.Text.Json.Serialization;
using NodeCanvas.Tool.Schema.NodeCanvas.Framework;

namespace NodeCanvas.Tool.Schema.NodeCanvas.DialogueTrees
{
    internal class GoToNode : Node
    {
        [JsonPropertyName("_targetNode")]
        public Node? TargetNode { get; set; } = null;

        public override string GetGraphVizShortName() => "Go-To";

        public override string GetGraphVizContent() => string.Empty;
    }
}
