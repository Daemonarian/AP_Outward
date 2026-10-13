using System.ComponentModel;
using Conflux.GraphViz;
using Conflux.NodeCanvas.References;
using Conflux.NodeCanvas.Serialization;
using Conflux.Schema.Conditions;
using Conflux.Schema.Exceptions;
using Newtonsoft.Json;

namespace Conflux.NodeCanvas.Conditions
{
    [NodeCanvasType("NodeCanvas.Tasks.Conditions.Condition_CheckQuestEventExpiry")]
    [GraphVizObject(Alias = "IsQuestEventExpired")]
    internal class QuestEventExpiredNodeCanvasCondition : NodeCanvasCondition
    {
        [JsonProperty("QuestEventRef")]
        [GraphVizProperty(IsMainProperty = true)]
        public QuestEventReference? QuestEvent { get; set; }

        [JsonProperty("ExpiryTime")]
        [DefaultValue(1)]
        public int ExpiryTime { get; set; } = 1;

        protected override IsQuestEventExpiredConfluxCondition BuildSubConfluxCondition(Graph graph) => new()
        {
            QuestEvent = QuestEvent?.BuildConfluxObject(graph) ?? throw new ConfluxValueException("QuestEventExpiredCondition.QuestEvent must be specified."),
            ExpiryTime = ExpiryTime,
        };
    }
}
