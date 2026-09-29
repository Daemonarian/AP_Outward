using Conflux.NodeCanvas.References;
using Conflux.NodeCanvas.Serialization;
using Newtonsoft.Json;

namespace Conflux.NodeCanvas.Conditions
{
    [NodeCanvasType("OutwardArchipelago.Graphs.Conditions.Condition_KnowQuest")]
    internal class Condition_CustomKnowQuest : ConditionTask
    {
        [JsonProperty("_quest")]
        public QuestReference? Quest { get; set; }

        public override string GetGraphVizShortName() => "KnowQuest";

        public override string GetGraphVizContent() => Quest?.ToGraphVizLabel() ?? string.Empty;
    }
}
