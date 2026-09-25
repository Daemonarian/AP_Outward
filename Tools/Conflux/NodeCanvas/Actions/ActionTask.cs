using System.Text;
using Conflux.GraphViz;
using Newtonsoft.Json;

namespace Conflux.NodeCanvas.Actions
{
    internal class ActionTask : IGraphVizLabelable
    {
        public virtual string GetGraphVizShortName() => GetType().Name;

        public virtual string GetGraphVizContent() => "\n" + JsonConvert.SerializeObject(this, GraphVizConverter.DefaultSerializerSettings);

        public string ToGraphVizLabel()
        {
            var label = new StringBuilder();

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
