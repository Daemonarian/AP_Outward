using System.Text.Json.Serialization;
using NodeCanvas.Tool.Schema;

namespace NodeCanvas.Tool.Schema.NodeCanvas.Framework.Conditions
{
    internal class Condition_IsQuestCompleted : ConditionTask
    {
        [JsonPropertyName("questRef")]
        public BBParameter<QuestReference>? QuestRef { get; set; }

        public override string GetGraphVizShortName() => "IsQuestCompleted";

        public override string GetGraphVizContent() => QuestRef?.ToGraphVizLabel() ?? string.Empty;
    }
}