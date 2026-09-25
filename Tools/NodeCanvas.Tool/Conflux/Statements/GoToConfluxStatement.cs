using NodeCanvas.Tool.Conflux.Context;
using NodeCanvas.Tool.Conflux.Deserializer;
using NodeCanvas.Tool.Conflux.Nodes;
using YamlDotNet.Serialization;

namespace NodeCanvas.Tool.Conflux.Statements
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
