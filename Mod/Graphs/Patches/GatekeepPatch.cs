using OutwardArchipelago.Graphs.Builders.Conditions;
using OutwardArchipelago.Graphs.Builders.Nodes;
using OutwardArchipelago.Graphs.Builders.Statements;

namespace OutwardArchipelago.Graphs.Patches
{
    /// <summary>
    /// Represents a dialogue patch that adds a gatekeeper node to a dialogue tree.
    /// </summary>
    internal class GatekeepPatch : IGraphPatch
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

        public void ApplyPatch(IGraphPatchContext context)
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
