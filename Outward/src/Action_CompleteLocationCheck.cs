using NodeCanvas.Framework;
using OutwardArchipelago.Archipelago;
using OutwardArchipelago.Archipelago.Data;

namespace OutwardArchipelago
{
    internal class Action_CompleteLocationCheck : ActionTask
    {
        public ArchipelagoLocationData Location { get; private set; }

        public Action_CompleteLocationCheck(ArchipelagoLocationData Location)
        {
            this.Location = Location;
        }

        public override string info => $"Complete Archipelago location check: {Location}";

        public override void OnExecute()
        {
            ArchipelagoConnector.Instance.CompleteLocationCheck(Location);
            base.EndAction();
        }
    }
}
