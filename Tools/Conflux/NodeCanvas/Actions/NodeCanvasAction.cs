using System.Text;
using Conflux.GraphViz;
using Conflux.Schema.Actions;

namespace Conflux.NodeCanvas.Actions
{
    internal class NodeCanvasAction : IGraphVizLabelable
    {
        public virtual string GetGraphVizShortName() => GraphVizConverter.BuildGraphVizName(this);

        public virtual string GetGraphVizContent() => GraphVizConverter.BuildGraphVizContent(this);

        public virtual ConfluxAction BuildConfluxAction(Graph graph) => throw new NotImplementedException($"BuildConfluxAction is not implemented for {GetType().Name}");

        public virtual string ToGraphVizLabel()
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
