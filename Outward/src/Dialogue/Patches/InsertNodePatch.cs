using System.Collections.Generic;
using NodeCanvas.Framework;
using OutwardArchipelago.Dialogue.Builders.Nodes;

namespace OutwardArchipelago.Dialogue.Patches
{
    internal class InsertNodePatch : IDialoguePatch
    {
        public int ReplaceNodeID { get; set; }

        public INodeBuilder Node { get; set; }

        public void ApplyPatch(IDialoguePatchContext context)
        {
            var originalNode = context.NodesByID[ReplaceNodeID];
            var connectionsToRedirect = new List<Connection>(originalNode.inConnections);
            originalNode.inConnections.Clear();

            var newNode = Node.BuildNode(context);

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
