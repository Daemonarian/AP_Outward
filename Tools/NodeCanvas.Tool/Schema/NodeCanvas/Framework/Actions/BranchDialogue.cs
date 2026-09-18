using System.Text.Json.Serialization;

namespace NodeCanvasRenderer.Schema.NodeCanvas.Framework.Actions
{
    internal class BranchDialogue : ActionTask
    {
        [JsonPropertyName("dialogueStarter")]
        public BBParameter<int>? DialogueStarter { get; set; }

        public override string GetGraphVizShortName() => "Branch";

        public override string GetGraphVizContent() => DialogueStarter?.ToGraphVizLabel() ?? string.Empty;
    }
}
