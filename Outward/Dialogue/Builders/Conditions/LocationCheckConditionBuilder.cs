using NodeCanvas.Framework;
using OutwardArchipelago.Dialogue.Conditions;

namespace OutwardArchipelago.Dialogue.Builders.Conditions
{
    internal class LocationCheckConditionBuilder : IConditionBuilder
    {
        public long LocationId { get; set; }

        public bool IsInverted { get; set; } = false;

        public ConditionTask BuildCondition(IDialoguePatchContext context) => new Condition_LocationCheck(LocationId) { invert = IsInverted };
    }
}
