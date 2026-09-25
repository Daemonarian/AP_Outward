using System.Text;
using Newtonsoft.Json;
using NodeCanvas.Tool.Schema.NodeCanvas.Framework;
using NodeCanvas.Tool.Schema.NodeCanvas.Serialization;

namespace NodeCanvas.Tool.Schema.NodeCanvas.DialogueTrees
{
    [NodeCanvasType("NodeCanvas.DialogueTrees.MultipleChoiceNodeExt")]
    internal class MultipleChoiceNode : DTNode
    {
        [JsonProperty("availableTime")]
        public float AvailableTime { get; set; }

        [JsonProperty("saySelection")]
        public bool SaySelection { get; set; }

        [JsonProperty("availableChoices")]
        public List<Choice> Choices { get; set; } = [];

        public override int OutConnectionCount => Choices.Count;

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
            [JsonProperty("isUnfolded")]
            public bool IsUnfolded { get; set; } = true;

            [JsonProperty("statement")]
            public Statement? Statement { get; set; } = null;

            [JsonProperty("condition")]
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
