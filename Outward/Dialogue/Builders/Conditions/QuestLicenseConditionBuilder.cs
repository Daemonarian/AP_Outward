using NodeCanvas.Framework;
using OutwardArchipelago.Dialogue.Conditions;

namespace OutwardArchipelago.Dialogue.Builders.Conditions
{

    internal class QuestLicenseConditionBuilder : IConditionBuilder
    {
        public int MinimumQuestLevel { get; set; } = 0;

        public bool IsInverted { get; set; } = false;

        public ConditionTask BuildCondition(IDialoguePatchContext context) => new Condition_QuestLicense(MinimumQuestLevel, IsInverted);
    }
}
