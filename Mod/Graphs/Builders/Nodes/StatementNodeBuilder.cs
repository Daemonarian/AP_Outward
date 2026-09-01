using NodeCanvas.DialogueTrees;
using NodeCanvas.Framework;
using OutwardArchipelago.Graphs.Builders.Statements;

namespace OutwardArchipelago.Graphs.Builders.Nodes
{
    internal class StatementNodeBuilder : INodeBuilder
    {
        public string ActorName { get; set; }

        public string ActorParameterID { get; set; }

        public IStatementBuilder Statement { get; set; }

        public INodeBuilder NextNode { get; set; }

        public Node BuildNode(IGraphPatchContext context)
        {

            var node = context.Graph.AddNode<StatementNodeExt>();
            node._actorName = ActorName ?? (context.Graph as DialogueTree)?.actorParameters.FirstOrDefault()?.name ?? "Speaker";
            node._actorParameterID = ActorParameterID;

            if (Statement != null)
            {
                node.statement = Statement.BuildStatement(context);
            }

            var nextNode = NextNode?.BuildNode(context);
            if (nextNode != null)
            {
                Connection.Create(node, nextNode);
            }

            return node;
        }
    }
}
