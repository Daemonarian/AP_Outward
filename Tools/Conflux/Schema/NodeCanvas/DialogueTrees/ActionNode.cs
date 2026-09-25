using Newtonsoft.Json;
using NodeCanvas.Tool.Schema.NodeCanvas.Framework;
using NodeCanvas.Tool.Schema.NodeCanvas.Serialization;

namespace NodeCanvas.Tool.Schema.NodeCanvas.DialogueTrees
{
    [NodeCanvasType("NodeCanvas.DialogueTrees.ActionNode")]
    internal class ActionNode : DTNode
    {
        [JsonProperty("_action")]
        public ActionTask? Action { get; set; }

        public override int OutConnectionCount => 1;

        public override string GetGraphVizShortName() => "Do";

        public override string GetGraphVizContent() => Action?.ToGraphVizLabel() ?? string.Empty;
    }
}
