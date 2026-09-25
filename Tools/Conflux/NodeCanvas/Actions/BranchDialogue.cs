using Conflux.NodeCanvas.Blackboard;
using Conflux.NodeCanvas.Serialization;
using Newtonsoft.Json;

namespace Conflux.NodeCanvas.Actions
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
