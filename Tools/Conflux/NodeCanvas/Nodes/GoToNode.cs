using Conflux.NodeCanvas.Serialization;
using Newtonsoft.Json;

namespace Conflux.NodeCanvas.Nodes
{
    [NodeCanvasType("NodeCanvas.DialogueTrees.GoToNode")]
    internal class GoToNode : DTNode
    {
        [JsonProperty("_targetNode")]
        public Node? TargetNode { get; set; } = null;

        public override int OutConnectionCount => 0;

        public override string GetGraphVizShortName() => "Go-To";

        public override string GetGraphVizContent() => string.Empty;
    }
}
