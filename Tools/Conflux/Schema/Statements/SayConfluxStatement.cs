using Conflux.NodeCanvas.Nodes;
using Conflux.Schema.Context;
using Conflux.Schema.References;
using Conflux.Schema.Serialization;
using YamlDotNet.Serialization;

namespace Conflux.Schema.Statements
{
    [ConfluxDerived("say")]
    internal class SayConfluxStatement : ConfluxStatement
    {
        [ConfluxMainProperty]
        [YamlMember(Alias = "statement")]
        public ConfluxStatementReference? Statement { get; set; }

        [YamlMember(Alias = "actor")]
        public ConfluxActorReference? Actor { get; set; }

        public override ConfluxGraph BuildGraph(NodeCanvasGraphContext context)
        {
            var actor = Actor?.BuildActor(context);
            var node = new StatementNodeExt
            {
                Statement = Statement?.BuildNodeCanvasObject(context),
                ActorName = actor?.Key ?? StatementNodeExt.DefaultActorName,
                ActorParameterID = actor?.ID?.ToString(),
            };
            return ConfluxGraph.CreateFromNode(context, node);
        }
    }
}
