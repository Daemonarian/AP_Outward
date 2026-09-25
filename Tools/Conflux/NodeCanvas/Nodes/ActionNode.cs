using Conflux.NodeCanvas.Actions;
using Conflux.NodeCanvas.Serialization;
using Newtonsoft.Json;

namespace Conflux.NodeCanvas.Nodes
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
