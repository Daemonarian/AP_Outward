using NodeCanvas.Framework;
using OutwardArchipelago.Archipelago;
using OutwardArchipelago.Dialogue.Conditions;

namespace OutwardArchipelago.Dialogue.Builders.Conditions
{
    internal class LocationCheckConditionBuilder : IConditionBuilder
    {
        public APWorld.Location Location { get; set; }

        public bool IsInverted { get; set; } = false;

        public ConditionTask BuildCondition(IDialoguePatchContext context) => new Condition_LocationCheck(Location) { invert = IsInverted };
    }
}
