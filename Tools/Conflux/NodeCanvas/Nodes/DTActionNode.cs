using Conflux.GraphViz;
using Conflux.NodeCanvas.Actions;
using Conflux.NodeCanvas.Serialization;
using Conflux.Schema.Exceptions;
using Conflux.Schema.Statements;
using Newtonsoft.Json;

namespace Conflux.NodeCanvas.Nodes
{
    [NodeCanvasType("NodeCanvas.DialogueTrees.ActionNode")]
    [GraphVizObject(Alias = "Do")]
    internal class DTActionNode : DTNode
    {
        [JsonProperty("_action")]
        [GraphVizProperty(IsMainProperty = true)]
        public NodeCanvasAction? Action { get; set; }

        public override int OutConnectionCount => 1;

        protected override DoConfluxStatement BuildConfluxStatement(Graph graph) => new()
        {
            Action = Action?.BuildConfluxAction(graph) ?? throw new ConfluxException("DTActionNode requires an action."),
        };
    }
}
