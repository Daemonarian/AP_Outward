using NodeCanvas.Framework;
using OutwardArchipelago.Archipelago;
using OutwardArchipelago.Archipelago.Data;
using System.Collections.Generic;

namespace OutwardArchipelago.Dialogue.Actions
{
    internal class Action_CompleteLocationCheck : ActionTask
    {
        public IReadOnlyList<ArchipelagoLocationData> Locations { get; private set; }

        public Action_CompleteLocationCheck(IReadOnlyList<ArchipelagoLocationData> locations)
        {
            this.Locations = locations;
        }

        public Action_CompleteLocationCheck(ArchipelagoLocationData location) : this(new[] { location }) { }

        public override string info => $"Complete Archipelago location check: {Locations}";

        public override void OnExecute()
        {
            foreach (var location in Locations)
            {
                ArchipelagoConnector.Instance.CompleteLocationCheck(location);
            }

            base.EndAction();
        }
    }
}
