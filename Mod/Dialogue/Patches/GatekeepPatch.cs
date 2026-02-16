using OutwardArchipelago.Dialogue.Builders.Conditions;
using OutwardArchipelago.Dialogue.Builders.Nodes;
using OutwardArchipelago.Dialogue.Builders.Statements;

namespace OutwardArchipelago.Dialogue.Patches
{
    /// <summary>
    /// Represents a dialogue patch that adds a gatekeeper node to a dialogue tree.
    /// </summary>
    internal class GatekeepPatch : IDialoguePatch
    {
        /// <summary>
        /// The node to gatekeep.
        /// </summary>
        public INodeBuilder ReplaceNode { get; set; } = null;

        /// <summary>
        /// The gatekeeping condition.
        /// </summary>
        public IConditionBuilder Condition { get; set; } = null;

        /// <summary>
        /// The statement to say when the gatekeeper rejects.
        /// </summary>
        public IStatementBuilder Statement { get; set; } = null;

        /// <summary>
        /// The actor name of who should say the statement.
        /// </summary>
        public string ActorName { get; set; } = null;

        public void ApplyPatch(IDialoguePatchContext context)
        {
            new InsertNodePatch
            {
                ReplaceNode = ReplaceNode,
                NewNode = new ConditionNodeBuilder
                {
                    Condition = Condition,
                    OnSuccess = ReplaceNode,
                    OnFailure = new StatementNodeBuilder
                    {
                        ActorName = ActorName,
                        Statement = Statement,
                        NextNode = new FinishNodeBuilder { },
                    },
                },
            }.ApplyPatch(context);
        }
    }
}
