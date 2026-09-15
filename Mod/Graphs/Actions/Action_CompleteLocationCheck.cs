using NodeCanvas.Framework;
using OutwardArchipelago.Archipelago;
using UnityEngine;

namespace OutwardArchipelago.Graphs.Actions
{
    internal class Action_CompleteLocationCheck : ActionTask
    {
        [SerializeField]
        private readonly APWorld.LocationRef _location = new();

        public APWorld.Location Location => _location.Location;

        public override string info => $"Complete Archipelago location checks: {Location}";

        public override void OnExecute()
        {
            ArchipelagoConnector.Instance.Locations.Complete(Location);

            base.EndAction();
        }
    }
}
