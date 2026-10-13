using System.ComponentModel;
using Conflux.GraphViz;
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
    [GraphVizObject(Alias = "Branch")]
    internal class BranchNodeCanvasAction : NodeCanvasAction
    {
        [JsonProperty("dialogueStarter")]
        [GraphVizProperty(IsMainProperty = true)]
        public BBParameter<UnityObject>? DialogueStarter { get; set; }

        [JsonProperty("waitActionFinish")]
        [DefaultValue(true)]
        public bool WaitActionFinish { get; set; } = true;

        public override BranchConfluxAction BuildConfluxAction(Graph graph) => new()
        {
            DialogueStarter = DialogueStarter?.BuildConfluxBlackboardVariableReference<ConfluxUnityObjectReference>(graph) ?? throw new ConfluxException("DialogueStarter is required"),
            DoWait = WaitActionFinish,
        };
    }
}
