using Newtonsoft.Json;
using NodeCanvas.Tool.Schema.NodeCanvas.Serialization;

namespace NodeCanvas.Tool.Schema.NodeCanvas.Framework.Conditions
{
    [NodeCanvasType("NodeCanvas.Tasks.Conditions.Condition_IsQuestCompleted")]
    internal class Condition_IsQuestCompleted : ConditionTask
    {
        [JsonProperty("questRef")]
        public BBParameter<QuestReference>? QuestRef { get; set; }

        public override string GetGraphVizShortName() => "IsQuestCompleted";

        public override string GetGraphVizContent() => QuestRef?.ToGraphVizLabel() ?? string.Empty;
    }
}