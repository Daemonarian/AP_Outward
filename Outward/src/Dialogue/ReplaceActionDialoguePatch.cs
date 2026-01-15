using NodeCanvas.DialogueTrees;
using NodeCanvas.Framework;

namespace OutwardArchipelago.Dialogue
{
    internal class ReplaceActionDialoguePatch : BaseDialoguePatch
    {
        /// <summary>
        /// The ID of the action node with the action to replace.
        /// </summary>
        public int ActionNodeID { get; private set; }

        /// <summary>
        /// The new action.
        /// </summary>
        public ActionTask Action { get; private set; }

        public ReplaceActionDialoguePatch(DialogueTreeID treeID, int actionNodeID, ActionTask action) : base(treeID)
        {
            ActionNodeID = actionNodeID;
            Action = action;
        }

        public override void ApplyPatch(IDialoguePatchContext context)
        {
            if (!context.NodesByID.TryGetValue(ActionNodeID, out var node) || node == null)
            {
                OutwardArchipelagoMod.Log.LogError($"Failed to patch dialogue tree: could not find node {ActionNodeID} in dialogue tree {context.TreeID}");
                return;
            }

            var newNode = context.Tree.AddNode<ActionNode>();
            newNode.action = Action;

            foreach (var connection in node.inConnections)
            {
                connection.targetNode = newNode;
                newNode.inConnections.Add(connection);
            }

            node.inConnections.Clear();

            foreach (var connection in node.outConnections)
            {
                Connection.Create(newNode, connection.targetNode);
            }

            if (context.Tree.primeNode.UID == node.UID)
            {
                context.Tree.primeNode = newNode;
            }
        }
    }
}
