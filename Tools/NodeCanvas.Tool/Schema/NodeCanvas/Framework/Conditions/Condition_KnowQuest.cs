using System.Text.Json.Serialization;

namespace NodeCanvasRenderer.Schema.NodeCanvas.Framework.Conditions
{
    internal class Condition_KnowQuest : ConditionTask
    {
        [JsonPropertyName("quest")]
        public BBParameter<UnityObject>? Quest { get; set; }

        public override string GetGraphVizShortName() => "KnowQuest";

        public override string GetGraphVizContent() => Quest?.ToGraphVizLabel() ?? string.Empty;
    }
}
