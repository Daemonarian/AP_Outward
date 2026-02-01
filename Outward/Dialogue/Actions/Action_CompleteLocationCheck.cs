using System.Collections.Generic;
using NodeCanvas.Framework;
using OutwardArchipelago.Archipelago;

namespace OutwardArchipelago.Dialogue.Actions
{
    internal class Action_CompleteLocationCheck : ActionTask
    {
        private readonly IReadOnlyList<long> _locationIds;
        public IReadOnlyList<long> LocationIds => _locationIds;

        public Action_CompleteLocationCheck(IReadOnlyList<long> locationIds) => _locationIds = locationIds;

        public Action_CompleteLocationCheck(long locationId) : this(new[] { locationId }) { }

        public override string info => $"Complete Archipelago location check: {string.Join(", ", LocationIds)}";

        public override void OnExecute()
        {
            foreach (var locationId in LocationIds)
            {
                ArchipelagoConnector.Instance.Locations.Complete(locationId);
            }

            base.EndAction();
        }
    }
}
