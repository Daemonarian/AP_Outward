using Conflux.NodeCanvas.References;
using Conflux.NodeCanvas.Serialization;
using Newtonsoft.Json;

namespace Conflux.NodeCanvas.Conditions
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
