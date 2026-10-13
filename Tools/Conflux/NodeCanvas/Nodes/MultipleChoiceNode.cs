using System.ComponentModel;
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
    [GraphVizObject(Alias = "Choice")]
    internal class MultipleChoiceNode : DTNode
    {
        [JsonProperty("availableTime")]
        [DefaultValue(0f)]
        public float AvailableTime { get; set; } = 0f;

        [JsonProperty("saySelection")]
        [DefaultValue(false)]
        public bool SaySelection { get; set; } = false;

        [JsonProperty("availableChoices")]
        [GraphVizProperty(IsMainProperty = true)]
        public List<Choice> Choices { get; set; } = [];

        public override int OutConnectionCount => Choices.Count;

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
            [DefaultValue(true)]
            public bool IsUnfolded { get; set; } = true;

            [JsonProperty("statement")]
            public Statement? Statement { get; set; }

            [JsonProperty("condition")]
            public NodeCanvasCondition? Condition { get; set; }

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
