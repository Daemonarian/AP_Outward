using NodeCanvas.Framework;
using OutwardArchipelago.Archipelago.Data;
using OutwardArchipelago.Dialogue.Conditions;

namespace OutwardArchipelago.Dialogue.Builders.Conditions
{
    internal class LocationCheckConditionBuilder : IConditionBuilder
    {
        public ArchipelagoLocationData Location { get; set; }

        public bool IsInverted { get; set; } = false;

        public ConditionTask BuildCondition(IDialoguePatchContext context) => new Condition_LocationCheck(Location, IsInverted);
    }
}
