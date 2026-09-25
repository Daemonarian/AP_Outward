using Newtonsoft.Json;
using NodeCanvas.Tool.Schema.NodeCanvas.Serialization;

namespace NodeCanvas.Tool.Schema.NodeCanvas.Framework.Conditions
{
    [NodeCanvasType("OutwardArchipelago.Graphs.Actions.Condition_CustomKnowQuest")]
    internal class Condition_CustomKnowQuest : ConditionTask
    {
        [JsonProperty("quest")]
        public QuestReference? Quest { get; set; }

        public override string GetGraphVizShortName() => "KnowQuest";

        public override string GetGraphVizContent() => Quest?.ToGraphVizLabel() ?? string.Empty;
    }
}
