using System.Collections.Generic;
using NodeCanvas.Framework;
using OutwardArchipelago.Archipelago;

namespace OutwardArchipelago.Dialogue.Actions
{
    internal class Action_CompleteLocationCheck : ActionTask
    {
        private readonly IReadOnlyList<APWorld.Location> _locations;
        public IReadOnlyList<APWorld.Location> Locations => _locations;

        public Action_CompleteLocationCheck(IReadOnlyList<APWorld.Location> locations) => _locations = locations;

        public Action_CompleteLocationCheck(APWorld.Location location) : this(new[] { location }) { }

        public override string info => $"Complete Archipelago location checks: {string.Join(", ", Locations)}";

        public override void OnExecute()
        {
            foreach (var location in Locations)
            {
                ArchipelagoConnector.Instance.Locations.Complete(location);
            }

            base.EndAction();
        }
    }
}
