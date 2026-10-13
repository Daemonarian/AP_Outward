using System.ComponentModel;
using System.Text;
using System.Text.Json;
using Conflux.GraphViz;
using Conflux.Schema.Conditions;
using Newtonsoft.Json;

namespace Conflux.NodeCanvas.Conditions
{
    internal abstract class NodeCanvasCondition : IGraphVizLabelable
    {
        [JsonProperty("_invert")]
        [GraphVizIgnore]
        [DefaultValue(false)]
        public bool Invert { get; set; } = false;

        public virtual string GetGraphVizShortName() => GraphVizConverter.BuildGraphVizName(this);

        public virtual string GetGraphVizContent() => GraphVizConverter.BuildGraphVizContent(this);

        protected abstract ConfluxCondition BuildSubConfluxCondition(Graph graph);

        public ConfluxCondition BuildConfluxCondition(Graph graph)
        {
            var condition = BuildSubConfluxCondition(graph);

            if (Invert)
            {
                condition = new NotConfluxCondition
                {
                    Condition = condition,
                };
            }

            return condition;
        }

        public virtual string ToGraphVizLabel()
        {
            var label = new StringBuilder();

            if (Invert)
            {
                label.Append("not ");
            }

            label.Append(GetGraphVizShortName().Trim());

            var content = GetGraphVizContent();
            if (!string.IsNullOrWhiteSpace(content))
            {
                content = GraphVizConverter.IndentLines(content, indentFirstLine: false);
                label.Append(": ").Append(content);
            }

            return label.ToString().TrimEnd();
        }
    }
}
