using Conflux.GraphViz;
using Conflux.NodeCanvas.Serialization;
using Newtonsoft.Json;

namespace Conflux.NodeCanvas.Nodes
{
    [NodeCanvasType("NodeCanvas.DialogueTrees.GoToNode")]
    [GraphVizObject(Alias = "Go-To")]
    internal class GoToNode : DTNode
    {
        [JsonProperty("_targetNode")]
        [GraphVizIgnore]
        public Node? TargetNode { get; set; }

        public override int OutConnectionCount => 0;
    }
}
