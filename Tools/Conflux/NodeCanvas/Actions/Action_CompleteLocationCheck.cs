using Conflux.NodeCanvas.References;
using Conflux.NodeCanvas.Serialization;
using Conflux.Schema.Actions;
using Newtonsoft.Json;

namespace Conflux.NodeCanvas.Actions
{
    [NodeCanvasType("OutwardArchipelago.Graphs.Actions.Action_CompleteLocationCheck")]
    internal class Action_CompleteLocationCheck : ActionTask
    {
        [JsonProperty("_location")]
        public APWorldLocationReference Location { get; set; } = new();

        public override string GetGraphVizShortName() => "CompleteLocationCheck";

        public override string GetGraphVizContent() => Location.ToGraphVizLabel();

        public override SendLocationCheckConfluxAction BuildConfluxAction(Graph graph) => new()
        {
            Location = Location.BuildConfluxObject(graph),
        };
    }
}
