using OutwardArchipelago.Graphs.Builders.Conditions;
using OutwardArchipelago.Graphs.Builders.Nodes;
using OutwardArchipelago.Graphs.Builders.Statements;

namespace OutwardArchipelago.Graphs.Patches
{
    /// <summary>
    /// Patches a dialogue tree with a gatekeeper that prevents access without the corresponding quest license.
    /// </summary>
    internal class QuestLicenseGatekeepPatch : IGraphPatch
    {
        /// <summary>
        /// The node to gatekeep.
        /// </summary>
        public INodeBuilder ReplaceNode { get; set; }

        /// <summary>
        /// The ID of the node to gatekeep.
        /// </summary>
        /// <remarks>
        /// Can be used in place of <see cref="ReplaceNode"/>.
        /// </remarks>
        public int ReplaceNodeID { set => ReplaceNode = new OriginalNodeBuilder { NodeID = value }; }

        /// <summary>
        /// The minimum quest license level required to pass the gatekeeper.
        /// </summary>
        public int MinimumQuestLevel { get; set; }

        /// <summary>
        /// The statement that should be said when the player is rejected by the gatekeeper.
        /// </summary>
        public IStatementBuilder Statement { get; set; }

        /// <summary>
        /// The mod localization key of the message to be said when the player is rejected.
        /// </summary>
        /// <remarks>
        /// Can be used in place of <see cref="Statement"/>.
        /// </remarks>
        public string LocalizationKey { set => Statement = new StatementBuilder { LocalizationKey = value }; }

        /// <summary>
        /// The name of the actor who will say the rejection message.
        /// </summary>
        public string ActorName { get; set; }

        public void ApplyPatch(IGraphPatchContext context)
        {
            new GatekeepPatch
            {
                ReplaceNode = ReplaceNode,
                Condition = new QuestLicenseConditionBuilder { MinimumQuestLevel = MinimumQuestLevel },
                Statement = Statement,
                ActorName = ActorName,
            }.ApplyPatch(context);
        }
    }
}
