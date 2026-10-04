using System.ComponentModel;
using System.Text;
using Conflux.NodeCanvas.References;
using Conflux.NodeCanvas.Serialization;
using Conflux.Schema.Conditions;
using Conflux.Schema.Exceptions;
using Newtonsoft.Json;

namespace Conflux.NodeCanvas.Conditions
{
    [NodeCanvasType("NodeCanvas.Tasks.Conditions.Condition_CheckQuestEventExpiry")]
    internal class QuestEventExpiredCondition : ConditionTask
    {
        [JsonProperty("QuestEventRef")]
        public QuestEventReference? QuestEvent { get; set; }

        [JsonProperty("ExpiryTime")]
        [DefaultValue(1)]
        public int ExpiryTime { get; set; } = 1;

        public override string GetGraphVizShortName() => "IsQuestEventExpired";

        public override string GetGraphVizContent()
        {
            var content = new StringBuilder();
            content.AppendLine(QuestEvent?.ToGraphVizLabel() ?? string.Empty);
            content.AppendLine($"ExpiryTime: {ExpiryTime}");
            return content.ToString().TrimEnd();
        }

        protected override IsQuestEventExpiredConfluxCondition BuildSubConfluxCondition(Graph graph) => new()
        {
            QuestEvent = QuestEvent?.BuildConfluxObject(graph) ?? throw new ConfluxValueException("QuestEventExpiredCondition.QuestEvent must be specified."),
            ExpiryTime = ExpiryTime,
        };
    }
}
