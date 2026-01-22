using OutwardArchipelago.Dialogue.Builders.Conditions;
using OutwardArchipelago.Dialogue.Builders.Nodes;
using OutwardArchipelago.Dialogue.Builders.Statements;

namespace OutwardArchipelago.Dialogue.Patches
{
    internal class QuestLicenseGatekeepPatch : IDialoguePatch
    {
        public int ReplaceNodeID { get; set; }

        public int MinimumQuestLevel { get; set; }

        public string LocalizationKey { get; set; }

        public string ActorName { get; set; }

        public void ApplyPatch(IDialoguePatchContext context)
        {
            new InsertNodePatch
            {
                ReplaceNodeID = ReplaceNodeID,
                Node = new ConditionNodeBuilder
                {
                    Condition = new QuestLicenseConditionBuilder { MinimumQuestLevel = MinimumQuestLevel },
                    OnSuccess = new OriginalNodeBuilder { NodeID = ReplaceNodeID },
                    OnFailure = new StatementNodeBuilder
                    {
                        ActorName = ActorName,
                        Statement = new StatementBuilder { LocalizationKey = LocalizationKey },
                        NextNode = new FinishNodeBuilder { },
                    },
                },
            }.ApplyPatch(context);
        }
    }
}
