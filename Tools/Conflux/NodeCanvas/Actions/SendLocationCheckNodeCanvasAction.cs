using Conflux.GraphViz;
using Conflux.NodeCanvas.References;
using Conflux.NodeCanvas.Serialization;
using Conflux.Schema.Actions;
using Newtonsoft.Json;

namespace Conflux.NodeCanvas.Actions
{
    [NodeCanvasType("OutwardArchipelago.Graphs.Actions.Action_CompleteLocationCheck")]
    [GraphVizObject(Alias = "SendLocationCheck")]
    internal class SendLocationCheckNodeCanvasAction : NodeCanvasAction
    {
        [JsonProperty("_location")]
        [GraphVizProperty(IsMainProperty = true)]
        public APWorldLocationReference Location { get; set; } = new();

        public override SendLocationCheckConfluxAction BuildConfluxAction(Graph graph) => new()
        {
            Location = Location.BuildConfluxObject(graph),
        };
    }
}
