using NodeCanvas.Tool.Conflux.Context;
using NodeCanvas.Tool.Conflux.Deserializer;
using NodeCanvas.Tool.Conflux.Statements;
using YamlDotNet.Serialization;

namespace NodeCanvas.Tool.Conflux
{
    internal class ConfluxBlock
    {
        [YamlMember(Alias = "statements")]
        [ConfluxMainProperty]
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
