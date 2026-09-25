using Conflux.Schema.Context;
using Conflux.Schema.Deserializer;
using Conflux.Schema.Statements;
using YamlDotNet.Serialization;

namespace Conflux.Schema
{
    internal class ConfluxBlock
    {
        [ConfluxMainProperty]
        [YamlMember(Alias = "statements")]
        public List<ConfluxStatement> Statements { get; set; } = [];

        public ConfluxGraph BuildGraph(INodeCanvasGraphContext context)
        {
            var graph = ConfluxGraph.CreateEmpty(context);
            foreach (var statement in Statements)
            {
                graph = graph.Concat(statement.BuildGraph(context));
            }

            return graph;
        }
    }
}
