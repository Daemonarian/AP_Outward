using System;
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
            if (ReplaceNode is null)
            {
                throw new ArgumentNullException($"{nameof(InsertNodePatch)}.{nameof(ApplyPatch)}: '{nameof(ReplaceNode)}' should not be null");
            }

            if (NewNode is null)
            {
                throw new ArgumentNullException($"{nameof(InsertNodePatch)}.{nameof(ApplyPatch)}: '{nameof(NewNode)}' should not be null");
            }

            var originalNode = ReplaceNode.BuildNode(context)
                ?? throw new ArgumentNullException($"{nameof(InsertNodePatch)}.{nameof(ApplyPatch)}: '{nameof(ReplaceNode)}' should not build null");
            var connectionsToRedirect = new List<Connection>(originalNode.inConnections);
            originalNode.inConnections.Clear();

            var newNode = NewNode.BuildNode(context)
                ?? throw new ArgumentNullException($"{nameof(InsertNodePatch)}.{nameof(ApplyPatch)}: '{nameof(NewNode)}' should not build null");
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
