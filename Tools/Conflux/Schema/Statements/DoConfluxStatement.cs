using Conflux.Schema.Actions;
using Conflux.Schema.Context;
using Conflux.Schema.Deserializer;
using YamlDotNet.Serialization;

namespace Conflux.Schema.Statements
{
    [ConfluxDerived("do")]
    internal class DoConfluxStatement : ConfluxStatement
    {
        [YamlMember(Alias = "action")]
        [ConfluxMainProperty]
        public ConfluxAction? Action { get; set; }

        public override ConfluxGraph BuildGraph(INodeCanvasGraphContext context)
        {
            if (Action is null)
            {
                throw new Exception("Action must not be null.");
            }

            var action = Action.BuildAction(context);
            var doNode = context.BuildDoNode(action);
            return ConfluxGraph.CreateFromNode(context, doNode);
        }
    }
}
