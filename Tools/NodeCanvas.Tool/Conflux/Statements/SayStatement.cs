using NodeCanvas.Tool.Conflux.Context;
using NodeCanvas.Tool.Conflux.Deserializer;
using NodeCanvas.Tool.Schema.NodeCanvas.DialogueTrees;
using YamlDotNet.Serialization;

namespace NodeCanvas.Tool.Conflux.Statements
{
    [ConfluxDerived("say")]
    internal class SayStatement : ConfluxStatement
    {
        [YamlMember(Alias = "text")]
        [ConfluxMainProperty]
        public string Text { get; set; } = string.Empty;

        [YamlMember(Alias = "meta")]
        public string Meta { get; set; } = string.Empty;

        [YamlMember(Alias = "actor")]
        public string Actor { get; set; } = string.Empty;

        public override ConfluxGraph BuildGraph(INodeCanvasGraphContext context)
        {
            ActorParameter? actor = null;
            if (!string.IsNullOrEmpty(Actor))
            {
                var actors = context.Script.DerivedData.ActorParameters;
                actor = actors.First(a => string.Equals(a.Key, Actor, StringComparison.Ordinal));
            }

            return ConfluxGraph.CreateFromNode(context, new StatementNodeExt
            {
                Statement = new Statement
                {
                    Text = Text,
                    Meta = Meta,
                },
                ActorName = actor?.Key,
                ActorParameterID = actor?.ID,
            });
        }
    }
}
