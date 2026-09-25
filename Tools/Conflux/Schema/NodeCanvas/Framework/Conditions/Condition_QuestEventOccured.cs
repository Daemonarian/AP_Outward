using System.Text;
using Newtonsoft.Json;
using NodeCanvas.Tool.Schema.NodeCanvas.Serialization;

namespace NodeCanvas.Tool.Schema.NodeCanvas.Framework.Conditions
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