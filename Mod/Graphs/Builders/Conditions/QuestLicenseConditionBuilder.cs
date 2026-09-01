using NodeCanvas.Framework;
using OutwardArchipelago.Graphs.Conditions;

namespace OutwardArchipelago.Graphs.Builders.Conditions
{

    internal class QuestLicenseConditionBuilder : IConditionBuilder
    {
        public int MinimumQuestLevel { get; set; } = 0;

        public bool IsInverted { get; set; } = false;

        public ConditionTask BuildCondition(IGraphPatchContext context) => new Condition_QuestLicense(MinimumQuestLevel, IsInverted);
    }
}
