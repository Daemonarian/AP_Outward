using System.ComponentModel;
using System.Text;
using Conflux.NodeCanvas.Blackboard;
using Conflux.NodeCanvas.References;
using Conflux.NodeCanvas.Serialization;
using Conflux.Schema.Actions;
using Conflux.Schema.Exceptions;
using Conflux.Schema.References;
using Newtonsoft.Json;

namespace Conflux.NodeCanvas.Actions
{
    [NodeCanvasType("NodeCanvas.Tasks.Actions.BranchDialogue")]
    internal class BranchDialogue : ActionTask
    {
        [JsonProperty("dialogueStarter")]
        public BBParameter<UnityObject>? DialogueStarter { get; set; }

        [JsonProperty("waitActionFinish")]
        [DefaultValue(true)]
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

        public override BranchConfluxAction BuildConfluxAction(Graph graph) => new()
        {
            DialogueStarter = DialogueStarter?.BuildConfluxBlackboardVariableReference<ConfluxUnityObjectReference>(graph) ?? throw new ConfluxException("DialogueStarter is required"),
            DoWait = WaitActionFinish,
        };
    }
}
