using System.ComponentModel;
using Conflux.GraphViz;
using Conflux.NodeCanvas.References;
using Conflux.NodeCanvas.Serialization;
using Conflux.Schema.Conditions;
using Newtonsoft.Json;

namespace Conflux.NodeCanvas.Conditions
{
    [NodeCanvasType("NodeCanvas.Tasks.Conditions.Condition_QuestEventOccured")]
    [GraphVizObject(Alias = "HasQuestEvent")]
    internal class QuestEventOccuredNodeCanvasCondition : NodeCanvasCondition
    {
        [JsonProperty("QuestEventRef")]
        [GraphVizProperty(IsMainProperty = true)]
        public QuestEventReference? QuestEvent { get; set; }

        [JsonProperty("MinStack")]
        [DefaultValue(1)]
        public int MinStack { get; set; } = 1;

        protected override HasQuestEventConfluxCondition BuildSubConfluxCondition(Graph graph) => new()
        {
            QuestEvent = QuestEvent?.BuildConfluxObject(graph),
            Count = MinStack,
        };
    }
}