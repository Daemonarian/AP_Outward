using NodeCanvas.Framework;
using OutwardArchipelago.Archipelago;
using OutwardArchipelago.Graphs.Conditions;

namespace OutwardArchipelago.Graphs.Builders.Conditions
{
    internal class LocationCheckConditionBuilder : IConditionBuilder
    {
        public APWorld.Location Location { get; set; }

        public bool IsInverted { get; set; } = false;

        public ConditionTask BuildCondition(IGraphPatchContext context) => new Condition_LocationCheck(Location) { invert = IsInverted };
    }
}
