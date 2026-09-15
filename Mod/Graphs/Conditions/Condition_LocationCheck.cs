using NodeCanvas.Framework;
using OutwardArchipelago.Archipelago;
using UnityEngine;

namespace OutwardArchipelago.Graphs.Conditions
{
    internal class Condition_LocationCheck : ConditionTask
    {
        [SerializeField]
        private readonly APWorld.LocationRef _location = new();

        public APWorld.Location Location => _location.Location;

        public override string info => $"Has completed location check: {Location}";

        public override bool OnCheck() => ArchipelagoConnector.Instance.Locations.IsComplete(Location);
    }
}
