using Newtonsoft.Json;
using NodeCanvas.Tool.Schema.NodeCanvas.Serialization;

namespace NodeCanvas.Tool.Schema.NodeCanvas.Framework.Actions
{
    [NodeCanvasType("OutwardArchipelago.Graphs.Actions.Action_CompleteLocationCheck")]
    internal class Action_CompleteLocationCheck : ActionTask
    {
        [JsonProperty("_location")]
        public APWorldLocationReference Location { get; set; } = new();

        public override string GetGraphVizShortName() => "CompleteLocationCheck";

        public override string GetGraphVizContent() => Location.ToGraphVizLabel();
    }
}
