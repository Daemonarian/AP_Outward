using Conflux.NodeCanvas.Actions;
using Conflux.NodeCanvas.Serialization;
using Newtonsoft.Json;

namespace Conflux.NodeCanvas.Nodes
{
    [NodeCanvasType("NodeCanvas.BehaviourTrees.ActionNode")]
    internal class BTActionNode : BTNode
    {
        [JsonProperty("_action")]
        public ActionTask? Action { get; set; } = null;

        public override int OutConnectionCount => 1;

        public override string GetGraphVizShortName() => "Do";

        public override string GetGraphVizContent() => Action?.ToGraphVizLabel() ?? string.Empty;
    }
}
