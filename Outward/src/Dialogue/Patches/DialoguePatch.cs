using NodeCanvas.Framework;
using System.Collections.Generic;

namespace OutwardArchipelago.Dialogue.Patches
{
    internal class DialoguePatch : IDialoguePatch
    {
        public int ReplaceNodeID { get; private set; }

        public IDialoguePatchNodeFactory Factory { get; private set; }

        public DialoguePatch(int replaceNodeID, IDialoguePatchNodeFactory factory)
        {
            ReplaceNodeID = replaceNodeID;
            Factory = factory;
        }

        public void ApplyPatch(IDialoguePatchContext context)
        {
            var replaceNode = context.NodesByID[ReplaceNodeID];
            var connectionsToRedirect = new List<Connection>(replaceNode.inConnections);
            replaceNode.inConnections.Clear();

            var newNode = Factory.CreateNode(context);

            foreach (var connection in connectionsToRedirect)
            {
                if (connection != null)
                {
                    connection.targetNode = newNode;
                    newNode.inConnections.Add(connection);
                }
            }

            if (context.Tree.primeNode == replaceNode)
            {
                context.Tree.primeNode = newNode;
            }
        }
    }
}
