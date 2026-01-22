using NodeCanvas.DialogueTrees;
using NodeCanvas.Framework;
using OutwardArchipelago.Dialogue.Builders.Statements;

namespace OutwardArchipelago.Dialogue.Builders.Nodes
{
    internal class StatementNodeBuilder : INodeBuilder
    {
        public string ActorName { get; set; }

        public string ActorParameterID { get; set; }

        public IStatementBuilder Statement { get; set; }

        public INodeBuilder NextNode { get; set; }

        public Node BuildNode(IDialoguePatchContext context)
        {
            var node = context.Tree.AddNode<StatementNodeExt>();
            node._actorName = ActorName ?? context.Tree.actorParameters.FirstOrDefault()?.name ?? "Speaker";
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
