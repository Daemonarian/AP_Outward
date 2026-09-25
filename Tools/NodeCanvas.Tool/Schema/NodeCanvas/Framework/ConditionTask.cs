using System.Text;
using System.Text.Json;
using Newtonsoft.Json;

namespace NodeCanvas.Tool.Schema.NodeCanvas.Framework
{
    internal class ConditionTask : IGraphVizLabelable
    {
        [JsonProperty("_invert")]
        public bool Invert { get; set; }

        public virtual string GetGraphVizShortName() => GetType().Name;

        public virtual string GetGraphVizContent() => "\n" + JsonConvert.SerializeObject(this, GraphVizConverter.DefaultSerializerSettings);

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
