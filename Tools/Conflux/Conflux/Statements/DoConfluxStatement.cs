using NodeCanvas.Tool.Conflux.Actions;
using NodeCanvas.Tool.Conflux.Context;
using NodeCanvas.Tool.Conflux.Deserializer;
using YamlDotNet.Serialization;

namespace NodeCanvas.Tool.Conflux.Statements
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
