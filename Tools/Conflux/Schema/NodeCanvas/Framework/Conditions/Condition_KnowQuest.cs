using Newtonsoft.Json;
using NodeCanvas.Tool.Schema.NodeCanvas.Serialization;

namespace NodeCanvas.Tool.Schema.NodeCanvas.Framework.Conditions
{
    [NodeCanvasType("NodeCanvas.Tasks.Conditions.Condition_KnowQuest")]
    internal class Condition_KnowQuest : ConditionTask
    {
        [JsonProperty("quest")]
        public BBParameter<UnityObject>? Quest { get; set; }

        public override string GetGraphVizShortName() => "KnowQuest";

        public override string GetGraphVizContent() => Quest?.ToGraphVizLabel() ?? string.Empty;
    }
}
