using Conflux.NodeCanvas.Actions;
using Conflux.NodeCanvas.Serialization;
using Conflux.Schema.Exceptions;
using Conflux.Schema.Statements;
using Newtonsoft.Json;

namespace Conflux.NodeCanvas.Nodes
{
    [NodeCanvasType("NodeCanvas.BehaviourTrees.ActionNode")]
    internal class BTActionNode : BTNode
    {
        [JsonProperty("_action")]
        public ActionTask? Action { get; set; }

        public override int OutConnectionCount => 1;

        public override string GetGraphVizShortName() => "Do";

        public override string GetGraphVizContent() => Action?.ToGraphVizLabel() ?? string.Empty;

        protected override DoConfluxStatement BuildConfluxStatement(Graph graph) => new()
        {
            Action = Action?.BuildConfluxAction(graph) ?? throw new ConfluxException("BTActionNode requires an action."),
        };
    }
}
