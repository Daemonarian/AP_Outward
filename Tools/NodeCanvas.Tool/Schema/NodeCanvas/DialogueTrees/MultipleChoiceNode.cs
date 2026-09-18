using System.Text;
using System.Text.Json.Serialization;
using NodeCanvas.Tool;
using NodeCanvas.Tool.Schema.NodeCanvas.Framework;

namespace NodeCanvas.Tool.Schema.NodeCanvas.DialogueTrees
{
    internal class MultipleChoiceNode : Node
    {
        [JsonPropertyName("availableTime")]
        public float AvailableTime { get; set; }

        [JsonPropertyName("saySelection")]
        public bool SaySelection { get; set; }

        [JsonPropertyName("availableChoices")]
        public List<Choice> Choices { get; set; } = [];

        public override string GetGraphVizShortName() => "Choice";

        public override string GetGraphVizContent()
        {
            var content = new StringBuilder();

            if (AvailableTime != 0f)
            {
                content.AppendLine($"AvailableTime: {AvailableTime}");
            }

            if (SaySelection)
            {
                content.AppendLine("SaySelection: yes");
            }

            foreach (var choice in Choices)
            {
                var choiceLabel = choice.ToGraphVizLabel();
                choiceLabel = GraphVizConverter.IndentLines(choiceLabel, indentFirstLine: false);
                content.AppendLine($"- {choiceLabel}");
            }

            return content.ToString().TrimEnd();
        }

        internal class Choice : IGraphVizLabelable
        {
            [JsonPropertyName("isUnfolded")]
            public bool IsUnfolded { get; set; } = true;

            [JsonPropertyName("statement")]
            public Statement? Statement { get; set; } = null;

            [JsonPropertyName("condition")]
            public ConditionTask? Condition { get; set; } = null;

            public string ToGraphVizLabel()
            {
                var label = new StringBuilder();

                label.AppendLine(Statement?.ToGraphVizLabel());

                if (!IsUnfolded)
                {
                    label.AppendLine($"IsUnfolded: no");
                }

                if (Condition is not null)
                {
                    var conditionLabel = Condition.ToGraphVizLabel();
                    conditionLabel = GraphVizConverter.IndentLines(conditionLabel, indentFirstLine: false);
                    label.AppendLine($"condition: {conditionLabel}");
                }

                return label.ToString().Trim();
            }
        }
    }
}
