using System.Collections.Generic;
using NodeCanvas.Framework;
using OutwardArchipelago.Archipelago;

namespace OutwardArchipelago.Dialogue.Actions
{
    internal class Action_CompleteLocationCheck : ActionTask
    {
        public IReadOnlyList<long> LocationIds { get; private set; }

        public Action_CompleteLocationCheck(IReadOnlyList<long> locationIds) => this.LocationIds = locationIds;

        public Action_CompleteLocationCheck(long locationId) : this(new[] { locationId }) { }

        public override string info => $"Complete Archipelago location check: {string.Join(", ", LocationIds)}";

        public override void OnExecute()
        {
            foreach (var locationIds in LocationIds)
            {
                ArchipelagoConnector.Instance.CompleteLocationCheck(locationIds);
            }

            base.EndAction();
        }
    }
}
