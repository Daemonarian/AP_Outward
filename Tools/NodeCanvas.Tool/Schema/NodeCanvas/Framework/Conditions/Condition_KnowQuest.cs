using System.Text.Json.Serialization;

namespace NodeCanvas.Tool.Schema.NodeCanvas.Framework.Conditions
{
    internal class Condition_KnowQuest : ConditionTask
    {
        [JsonPropertyName("quest")]
        public BBParameter<UnityObject>? Quest { get; set; }

        public override string GetGraphVizShortName() => "KnowQuest";

        public override string GetGraphVizContent() => Quest?.ToGraphVizLabel() ?? string.Empty;
    }
}
