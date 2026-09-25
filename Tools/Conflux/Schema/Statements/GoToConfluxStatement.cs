using Conflux.Schema.Context;
using Conflux.Schema.Deserializer;
using Conflux.Schema.Nodes;
using YamlDotNet.Serialization;

namespace Conflux.Schema.Statements
{
    [ConfluxDerived("goto")]
    internal class GoToConfluxStatement : ConfluxStatement
    {
        [ConfluxMainProperty]
        [YamlMember(Alias = "label")]
        public string Label { get; set; } = string.Empty;

        public override ConfluxGraph BuildGraph(INodeCanvasGraphContext context)
        {
            var node = new GoToLabelNode(Label);
            return ConfluxGraph.CreateFromNode(context, node);
        }
    }
}
