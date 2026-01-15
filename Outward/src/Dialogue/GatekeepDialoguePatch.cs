using NodeCanvas.DialogueTrees;
using NodeCanvas.Framework;

namespace OutwardArchipelago.Dialogue
{
    internal class GatekeepDialoguePatch : BaseDialoguePatch
    {
        /// <summary>
        /// The ID of the node to protect with a gatekeeper.
        /// </summary>
        public int GatekeptNodeID { get; private set; }

        /// <summary>
        /// The condition that determines whether the gatekeeper should allow access to the protected node.
        /// </summary>
        public ConditionTask Condition { get; private set; }

        /// <summary>
        /// The name of the actor who should speak the rejection message.
        /// </summary>
        public string ActorName { get; private set; }

        /// <summary>
        /// The localization key of the message to display when the gatekeeper blocks access.
        /// </summary>
        public string MessageKey { get; private set; }

        public GatekeepDialoguePatch(DialogueTreeID treeID, int gatekeptNodeID, ConditionTask condition, string messageKey, string actorName = null) : base(treeID)
        {
            GatekeptNodeID = gatekeptNodeID;
            Condition = condition;
            MessageKey = messageKey;
            ActorName = actorName;
        }

        public override void ApplyPatch(IDialoguePatchContext context)
        {
            if (!context.NodesByID.TryGetValue(GatekeptNodeID, out var gatekeptNode) || gatekeptNode == null)
            {
                OutwardArchipelagoMod.Log.LogError($"Failed to patch dialogue tree: could not find node {GatekeptNodeID} in dialogue tree {context.TreeID}");
                return;
            }

            var actorName = ActorName;
            if (string.IsNullOrEmpty(actorName))
            {
                actorName = context.Tree.actorParameters.FirstOrDefault()?.name ?? "Speaker";
            }

            var message = OutwardArchipelagoMod.Instance.GetLocalizedModString(MessageKey);

            var gatekeeperNode = context.Tree.AddNode<ConditionNode>();
            gatekeeperNode.condition = Condition;

            var rejectionNode = context.Tree.AddNode<StatementNodeExt>();
            rejectionNode.actorName = actorName;
            rejectionNode.statement = new Statement(message);

            foreach (var connection in gatekeptNode.inConnections)
            {
                connection.targetNode = gatekeeperNode;
                gatekeeperNode.inConnections.Add(connection);
            }

            gatekeptNode.inConnections.Clear();

            Connection.Create(gatekeeperNode, gatekeptNode);
            Connection.Create(gatekeeperNode, rejectionNode);

            if (context.Tree.primeNode.UID == gatekeptNode.UID)
            {
                context.Tree.primeNode = gatekeeperNode;
            }
        }
    }
}
