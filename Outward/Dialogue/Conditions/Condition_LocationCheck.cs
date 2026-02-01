using NodeCanvas.Framework;
using OutwardArchipelago.Archipelago;

namespace OutwardArchipelago.Dialogue.Conditions
{
    internal class Condition_LocationCheck : ConditionTask
    {
        private readonly APWorld.Location _location;
        public APWorld.Location Location => _location;

        public Condition_LocationCheck(APWorld.Location location)
        {
            _location = location;
        }

        public override string info => $"Has completed location check: {Location}";

        public override bool OnCheck() => ArchipelagoConnector.Instance.Locations.IsComplete(Location);
    }
}
