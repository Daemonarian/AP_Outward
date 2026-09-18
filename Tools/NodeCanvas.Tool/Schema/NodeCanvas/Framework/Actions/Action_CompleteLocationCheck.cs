using System.Text.Json.Serialization;

namespace NodeCanvas.Tool.Schema.NodeCanvas.Framework.Actions
{
    internal class Action_CompleteLocationCheck : ActionTask
    {
        [JsonPropertyName("_location")]
        public APWorldLocationReference Location { get; set; } = new();

        public override string GetGraphVizShortName() => "CompleteLocationCheck";

        public override string GetGraphVizContent() => Location.ToGraphVizLabel();
    }
}
