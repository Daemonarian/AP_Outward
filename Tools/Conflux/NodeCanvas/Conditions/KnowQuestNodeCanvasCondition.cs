using Conflux.GraphViz;
using Conflux.NodeCanvas.References;
using Conflux.NodeCanvas.Serialization;
using Conflux.Schema.Conditions;
using Conflux.Schema.Exceptions;
using Newtonsoft.Json;

namespace Conflux.NodeCanvas.Conditions
{
    [NodeCanvasType("OutwardArchipelago.Graphs.Conditions.Condition_KnowQuest")]
    [GraphVizObject(Alias = "KnowQuest")]
    internal class KnowQuestNodeCanvasCondition : NodeCanvasCondition
    {
        [JsonProperty("_quest")]
        [GraphVizProperty(IsMainProperty = true)]
        public QuestReference? Quest { get; set; }

        protected override KnowQuestConfluxCondition BuildSubConfluxCondition(Graph graph) => new()
        {
            Quest = Quest?.BuildConfluxObject(graph) ?? throw new ConfluxException("Condition_CustomKnowQuest requires a quest reference."),
        };
    }
}
