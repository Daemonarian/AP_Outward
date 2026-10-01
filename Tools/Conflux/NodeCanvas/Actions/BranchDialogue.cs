using System.Text;
using Conflux.NodeCanvas.Blackboard;
using Conflux.NodeCanvas.Serialization;
using Newtonsoft.Json;

namespace Conflux.NodeCanvas.Actions
{
    [NodeCanvasType("NodeCanvas.Tasks.Actions.BranchDialogue")]
    internal class BranchDialogue : ActionTask
    {
        [JsonProperty("dialogueStarter")]
        public BBParameter<UnityObject>? DialogueStarter { get; set; }

        [JsonProperty("waitActionFinish")]
        public bool WaitActionFinish { get; set; } = true;

        public override string GetGraphVizShortName() => "Branch";

        public override string GetGraphVizContent()
        {
            var content = new StringBuilder();

            content.AppendLine(DialogueStarter?.ToGraphVizLabel() ?? string.Empty);

            if (!WaitActionFinish)
            {
                content.AppendLine($"WaitActionFinish: no");
            }

            return content.ToString().TrimEnd();
        }
    }
}
