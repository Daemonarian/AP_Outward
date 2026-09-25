using Newtonsoft.Json;
using NodeCanvas.Tool.Schema.NodeCanvas.Serialization;

namespace NodeCanvas.Tool.Schema.NodeCanvas.Framework.Actions
{
    [NodeCanvasType("NodeCanvas.Tasks.Actions.BranchDialogue")]
    internal class BranchDialogue : ActionTask
    {
        [JsonProperty("dialogueStarter")]
        public BBParameter<int>? DialogueStarter { get; set; }

        public override string GetGraphVizShortName() => "Branch";

        public override string GetGraphVizContent() => DialogueStarter?.ToGraphVizLabel() ?? string.Empty;
    }
}
