using System.Text;
using Conflux.NodeCanvas.References;
using Conflux.NodeCanvas.Serialization;
using Newtonsoft.Json;

namespace Conflux.NodeCanvas.Conditions
{
    [NodeCanvasType("NodeCanvas.Tasks.Conditions.Condition_CheckQuestEventExpiry")]
    internal class QuestEventExpiredCondition : ConditionTask
    {
        [JsonProperty("QuestEventRef")]
        public QuestEventReference? Quest { get; set; } = null;

        [JsonProperty("ExpiryTime")]
        public int ExpiryTime = 1;

        public override string GetGraphVizShortName() => "IsQuestEventExpired";

        public override string GetGraphVizContent()
        {
            var content = new StringBuilder();
            content.AppendLine(Quest?.ToGraphVizLabel() ?? string.Empty);
            content.AppendLine($"ExpiryTime: {ExpiryTime}");
            return content.ToString().TrimEnd();
        }
    }
}
