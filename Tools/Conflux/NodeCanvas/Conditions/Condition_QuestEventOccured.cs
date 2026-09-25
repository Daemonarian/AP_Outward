using System.Text;
using Conflux.NodeCanvas.References;
using Conflux.NodeCanvas.Serialization;
using Newtonsoft.Json;

namespace Conflux.NodeCanvas.Conditions
{
    [NodeCanvasType("NodeCanvas.Tasks.Conditions.Condition_QuestEventOccured")]
    internal class Condition_QuestEventOccured : ConditionTask
    {
        [JsonProperty("QuestEventRef")]
        public QuestEventReference? QuestEventRef { get; set; }

        [JsonProperty("MinStack")]
        public int MinStack { get; set; } = 1;

        public override string GetGraphVizShortName() => "QuestEventOccurred";

        public override string GetGraphVizContent()
        {
            var content = new StringBuilder();

            content.AppendLine(QuestEventRef?.ToGraphVizLabel() ?? string.Empty);

            if (MinStack != 1)
            {
                content.AppendLine($"MinStack: {MinStack}");
            }

            return content.ToString().TrimEnd();
        }
    }
}