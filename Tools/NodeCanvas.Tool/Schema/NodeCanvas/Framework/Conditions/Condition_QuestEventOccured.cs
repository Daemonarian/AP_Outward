using System.Text;
using System.Text.Json.Serialization;

namespace NodeCanvas.Tool.Schema.NodeCanvas.Framework.Conditions
{
    internal class Condition_QuestEventOccured : ConditionTask
    {
        [JsonPropertyName("QuestEventRef")]
        public QuestEventReference? QuestEventRef { get; set; }

        [JsonPropertyName("MinStack")]
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