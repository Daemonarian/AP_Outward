using NodeCanvas.Framework;
using OutwardArchipelago.Archipelago;
using UnityEngine;

namespace OutwardArchipelago.Graphs.Actions
{
    public class Action_CompleteLocationCheck2 : ActionTask
    {
        [SerializeField]
        private readonly APWorld.LocationRef _location;

        public override string info => $"Complete Archipelago location check: {Location}";

        internal APWorld.Location Location => _location.Location;

        public override void OnExecute()
        {
            ArchipelagoConnector.Instance.Locations.Complete(Location);
            base.EndAction();
        }
    }
}
