using Conflux.NodeCanvas;
using Conflux.NodeCanvas.Nodes;
using Conflux.Schema.Actions;
using Conflux.Schema.Context;
using Conflux.Schema.Deserializer;
using YamlDotNet.Serialization;

namespace Conflux.Schema.Statements
{
    [ConfluxDerived("do")]
    internal class DoConfluxStatement : ConfluxStatement
    {
        [ConfluxMainProperty(Force = true)]
        [YamlMember(Alias = "action")]
        public ConfluxAction? Action { get; set; }

        public override ConfluxGraph BuildGraph(NodeCanvasGraphContext context)
        {
            if (Action is null)
            {
                throw new Exception("Action must not be null.");
            }

            var action = Action.BuildAction(context);

            Node doNode = context.GraphType switch
            {
                Graph.GraphType.BehaviourTree => new BTActionNode
                {
                    Action = action,
                },
                Graph.GraphType.DialogueTree => new DTActionNode
                {
                    Action = action,
                },
                _ => throw new NotImplementedException($"Do nodes for graph type {context.GraphType} have not been implemented."),
            };

            return ConfluxGraph.CreateFromNode(context, doNode);
        }
    }
}
