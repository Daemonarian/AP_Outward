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
    internal class Condition_IsQuestCompleted : ConditionTask
    {
        [JsonProperty("questRef")]
        public BBParameter<QuestReference>? QuestRef { get; set; }

        public override string GetGraphVizShortName() => "IsQuestCompleted";

        public override string GetGraphVizContent() => QuestRef?.ToGraphVizLabel() ?? string.Empty;

        protected override IsQuestCompleteConfluxCondition BuildSubConfluxCondition(Graph graph) => new()
        {
            Quest = QuestRef?.BuildConfluxBlackboardVariableReference<ConfluxQuestReference>(graph) ?? throw new ConfluxValueException("Condition_IsQuestCompleted requires a QuestRef."),
        };
    }
}