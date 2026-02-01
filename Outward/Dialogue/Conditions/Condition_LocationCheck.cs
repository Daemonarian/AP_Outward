using NodeCanvas.Framework;
using OutwardArchipelago.Archipelago;

namespace OutwardArchipelago.Dialogue.Conditions
{
    internal class Condition_LocationCheck : ConditionTask
    {
        private readonly long _locationId;
        public long LocationId => _locationId;

        public Condition_LocationCheck(long locationId)
        {
            _locationId = locationId;
        }

        public override string info => $"Has completed location check: {LocationId}";

        public override bool OnCheck() => ArchipelagoConnector.Instance.Locations.IsComplete(LocationId);
    }
}
