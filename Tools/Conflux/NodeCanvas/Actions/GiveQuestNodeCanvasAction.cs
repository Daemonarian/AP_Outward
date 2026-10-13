using Conflux.GraphViz;
using Conflux.NodeCanvas.Blackboard;
using Conflux.NodeCanvas.References;
using Conflux.NodeCanvas.Serialization;
using Conflux.Schema.Actions;
using Conflux.Schema.References;
using Newtonsoft.Json;

namespace Conflux.NodeCanvas.Actions
{
    [NodeCanvasType("NodeCanvas.Tasks.Actions.GiveQuest")]
    [GraphVizObject(Alias = "GiveQuest")]
    internal class GiveQuestNodeCanvasAction : NodeCanvasAction
    {
        [JsonProperty("quest")]
        public BBParameter<UnityObject>? Quest { get; set; }

        [JsonProperty("questRef")]
        public BBParameter<QuestReference>? QuestRef { get; set; }

        public override GiveQuestConfluxAction BuildConfluxAction(Graph graph) => new()
        {
            QuestObject = Quest?.BuildConfluxBlackboardVariableReference<ConfluxUnityObjectReference>(graph),
            Quest = QuestRef?.BuildConfluxBlackboardVariableReference<ConfluxQuestReference>(graph),
        };
    }
}
