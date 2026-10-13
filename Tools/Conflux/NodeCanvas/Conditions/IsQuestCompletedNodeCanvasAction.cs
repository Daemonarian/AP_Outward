using Conflux.GraphViz;
using Conflux.NodeCanvas.Blackboard;
using Conflux.NodeCanvas.References;
using Conflux.NodeCanvas.Serialization;
using Conflux.Schema.Conditions;
using Conflux.Schema.Exceptions;
using Conflux.Schema.References;
using Newtonsoft.Json;

namespace Conflux.NodeCanvas.Conditions
{
    [NodeCanvasType("NodeCanvas.Tasks.Conditions.Condition_IsQuestCompleted")]
    [GraphVizObject(Alias = "IsQuestCompleted")]
    internal class IsQuestCompletedNodeCanvasAction : NodeCanvasCondition
    {
        [JsonProperty("questRef")]
        [GraphVizProperty(IsMainProperty = true)]
        public BBParameter<QuestReference>? Quest { get; set; }

        protected override IsQuestCompleteConfluxCondition BuildSubConfluxCondition(Graph graph) => new()
        {
            Quest = Quest?.BuildConfluxBlackboardVariableReference<ConfluxQuestReference>(graph) ?? throw new ConfluxValueException("Condition_IsQuestCompleted requires a QuestRef."),
        };
    }
}