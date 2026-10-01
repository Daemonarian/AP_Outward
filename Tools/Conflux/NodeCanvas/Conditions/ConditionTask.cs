using System.Text;
using System.Text.Json;
using Conflux.GraphViz;
using Conflux.Schema.Conditions;
using Newtonsoft.Json;

namespace Conflux.NodeCanvas.Conditions
{
    internal class ConditionTask : IGraphVizLabelable
    {
        [JsonProperty("_invert")]
        public bool Invert { get; set; }

        public virtual string GetGraphVizShortName() => GetType().Name;

        public virtual string GetGraphVizContent() => "\n" + JsonConvert.SerializeObject(this, GraphVizConverter.DefaultSerializerSettings);

        public virtual ConfluxCondition BuildConfluxCondition(Graph graph) => throw new NotImplementedException($"BuildConfluxCondition is not implemented for {GetType().Name}");

        public string ToGraphVizLabel()
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
