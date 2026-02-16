using OutwardArchipelago.Archipelago;
using OutwardArchipelago.Dialogue.Builders.Conditions;
using OutwardArchipelago.Dialogue.Builders.Nodes;
using OutwardArchipelago.Dialogue.Builders.Statements;

namespace OutwardArchipelago.Dialogue.Patches
{
    /// <summary>
    /// A dialogue patch that conditionally adds a gatekeeper patch for specific faction Pact skills,
    /// only if the faction pact feature is enabled.
    /// </summary>
    internal class FactionPactGatekeepPatch : IDialoguePatch
    {
        /// <summary>
        /// The node to replace.
        /// </summary>
        public INodeBuilder ReplaceNode { get; set; }

        /// <summary>
        /// The original ID of the node to replace.
        /// </summary>
        /// <remarks>
        /// Can be used in place of <see cref="ReplaceNode"/>.
        /// </remarks>
        public int ReplaceNodeID { set => ReplaceNode = new OriginalNodeBuilder { NodeID = value }; }

        /// <summary>
        /// The required factions.
        /// This patch will allow the player to pass if they have a faction pact for any of the specified factions.
        /// </summary>
        public APWorld.Faction Faction { get; set; } = APWorld.Faction.None;

        /// <summary>
        /// The statement that should be shown if the player is rejected by the gatekeeper.
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
        /// The name of the actor that should say the rejection stateemnt.
        /// </summary>
        public string ActorName { get; set; }

        public void ApplyPatch(IDialoguePatchContext context)
        {
            if (OutwardArchipelagoMod.Instance.IsArchipelagoEnabled && ArchipelagoConnector.Instance.SlotData.IsFactionPactEnabled)
            {
                new GatekeepPatch
                {
                    ReplaceNode = ReplaceNode,
                    Condition = new FactionPactConditionBuilder { Faction = Faction },
                    Statement = Statement,
                    ActorName = ActorName,
                }.ApplyPatch(context);
            }
        }
    }
}
