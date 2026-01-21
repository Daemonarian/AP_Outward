using NodeCanvas.Framework;
using OutwardArchipelago.Archipelago.Data;
using OutwardArchipelago.Dialogue.Conditions;

namespace OutwardArchipelago.Dialogue.Patches
{
    internal class DialoguePatchLocationCheckConditionFactory : IDialoguePatchConditionFactory
    {
        public ArchipelagoLocationData Location { get; private set; }

        public bool IsInverted { get; private set; }

        public DialoguePatchLocationCheckConditionFactory(ArchipelagoLocationData location, bool isInverted = false)
        {
            Location = location;
            IsInverted = isInverted;
        }

        public ConditionTask CreateCondition(IDialoguePatchContext context)
        {
            return new Condition_LocationCheck(Location, IsInverted);
        }
    }
}
