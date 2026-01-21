using NodeCanvas.Framework;
using OutwardArchipelago.Dialogue.Conditions;

namespace OutwardArchipelago.Dialogue.Patches
{
    internal class DialoguePatchQuestLicenseConditionFactory : IDialoguePatchConditionFactory
    {
        public int MinimumQuestLevel { get; private set; }

        public bool IsInverted { get; private set; }

        public DialoguePatchQuestLicenseConditionFactory(int minimumQuestLevel, bool isInverted = false)
        {
            MinimumQuestLevel = minimumQuestLevel;
            IsInverted = isInverted;
        }

        public ConditionTask CreateCondition(IDialoguePatchContext context)
        {
            return new Condition_QuestLicense(MinimumQuestLevel, IsInverted);
        }
    }
}
