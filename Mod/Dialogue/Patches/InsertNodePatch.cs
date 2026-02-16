using System.Collections.Generic;
using NodeCanvas.Framework;
using OutwardArchipelago.Dialogue.Builders.Nodes;

namespace OutwardArchipelago.Dialogue.Patches
{
    internal class InsertNodePatch : IDialoguePatch
    {
        public INodeBuilder ReplaceNode { get; set; }

        public INodeBuilder NewNode { get; set; }

        public void ApplyPatch(IDialoguePatchContext context)
        {
            var originalNode = ReplaceNode.BuildNode(context);
            var connectionsToRedirect = new List<Connection>(originalNode.inConnections);
            originalNode.inConnections.Clear();

            var newNode = NewNode.BuildNode(context);

            foreach (var connection in connectionsToRedirect)
            {
                if (connection != null)
                {
                    connection.targetNode = newNode;
                    newNode.inConnections.Add(connection);
                }
            }

            if (context.Tree.primeNode == originalNode)
            {
                context.Tree.primeNode = newNode;
            }
        }
    }
}
