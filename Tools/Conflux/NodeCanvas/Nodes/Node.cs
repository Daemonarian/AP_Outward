using System.Text;
using Conflux.GraphViz;
using Conflux.Schema;
using Conflux.Schema.Exceptions;
using Conflux.Schema.Statements;
using Newtonsoft.Json;

namespace Conflux.NodeCanvas.Nodes
{
    internal abstract class Node : IGraphVizLabelable
    {
        [JsonIgnore]
        [GraphVizIgnore]
        public abstract int OutConnectionCount { get; }

        public virtual string GetGraphVizShortName() => GraphVizConverter.BuildGraphVizName(this);

        public virtual string GetGraphVizContent() => GraphVizConverter.BuildGraphVizContent(this);

        public virtual string? GetGraphVizOutConnectionLabel(int index) => null;

        public virtual ConfluxBlock BuildConfluxBlock(Graph graph, List<ConfluxBlock> children)
        {
            var statement = BuildConfluxStatement(graph);
            if (statement.AcceptsChildBlocks)
            {
                statement.ChildBlocks = children;
                return new()
                {
                    Statements = [statement],
                };
            }

            if (children.Count > 1)
            {
                throw new ConfluxException($"Node ({GetType().Name}) must have at most one out-going connection; not {children.Count}.");
            }

            var childBlock = children.Count > 0 ? children[0] : new ConfluxBlock();
            return new ConfluxBlock
            {
                Statements = [statement, .. childBlock.Statements],
            };
        }

        protected virtual ConfluxStatement BuildConfluxStatement(Graph graph) => throw new NotImplementedException($"{nameof(BuildConfluxStatement)} is not implemented for {nameof(Node)} of type {GetType().Name}.");

        public virtual string ToGraphVizLabel()
        {
            var label = new StringBuilder();
            label.AppendLine(GetGraphVizShortName().Trim());
            label.AppendLine(GetGraphVizContent().Trim());
            return label.ToString().TrimEnd();
        }
    }
}
