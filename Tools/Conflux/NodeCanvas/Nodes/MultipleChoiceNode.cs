using System.Text;
using Conflux.GraphViz;
using Conflux.NodeCanvas.Conditions;
using Conflux.NodeCanvas.References;
using Conflux.NodeCanvas.Serialization;
using Conflux.Schema;
using Conflux.Schema.Exceptions;
using Conflux.Schema.Statements;
using Newtonsoft.Json;

namespace Conflux.NodeCanvas.Nodes
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

        public override string? GetGraphVizOutConnectionLabel(int index) => $"{index}";

        public override ConfluxBlock BuildConfluxBlock(Graph graph, List<ConfluxBlock> children)
        {
            if (children.Count > OutConnectionCount)
            {
                throw new ConfluxException($"MultipleChoiceNode can only have {OutConnectionCount} children.");
            }

            return new ConfluxBlock
            {
                Statements = [new ChoiceConfluxStatement
                {
                    Choices = [.. Choices.Select((choice, i) => new ChoiceConfluxStatement.Choice
                    {
                        Statement = choice.Statement?.BuildConfluxStatementReference(graph) ?? throw new ConfluxException("Choice statement is required"),
                        Condition = choice.Condition?.BuildConfluxCondition(graph),
                        IsUnfolded = choice.IsUnfolded,
                        Then = i < children.Count ? children[i] : new ConfluxBlock(),
                    })],
                    AvailableTime = AvailableTime,
                    SaySelection = SaySelection,
                }],
            };
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
