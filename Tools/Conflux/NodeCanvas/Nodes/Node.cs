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
        public abstract int OutConnectionCount { get; }

        public virtual string GetGraphVizShortName() => GetType().Name;

        public virtual string GetGraphVizContent() => JsonConvert.SerializeObject(this, GraphVizConverter.DefaultSerializerSettings);

        public virtual string? GetGraphVizOutConnectionLabel(int index) => null;

        public virtual ConfluxBlock BuildConfluxBlock(Graph graph, List<ConfluxBlock> children)
        {
            if (OutConnectionCount < 0 || OutConnectionCount > 1)
            {
                throw new NotImplementedException($"BuildConfluxBlock is not implemented for {GetType().Name} with OutConnectionCount {OutConnectionCount}");
            }

            if (children.Count > OutConnectionCount)
            {
                throw new ConfluxException($"Node can have at most {OutConnectionCount} child(ren), not {children.Count}.");
            }

            var statement = BuildConfluxStatement(graph);
            var childBlock = children.Count > 0 ? children[0] : new ConfluxBlock();
            return new ConfluxBlock
            {
                Statements = [statement, .. childBlock.Statements],
            };
        }

        protected virtual ConfluxStatement BuildConfluxStatement(Graph graph) => throw new NotImplementedException($"BuildConfluxStatement is not implemented for {GetType().Name}");

        public string ToGraphVizLabel()
        {
            var label = new StringBuilder();
            label.AppendLine(GetGraphVizShortName().Trim());
            label.AppendLine(GetGraphVizContent());
            return label.ToString().TrimEnd();
        }
    }
}
