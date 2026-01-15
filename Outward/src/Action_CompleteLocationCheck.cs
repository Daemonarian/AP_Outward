using NodeCanvas.Framework;

namespace OutwardArchipelago
{
    internal class Action_CompleteLocationCheck : ActionTask
    {
        public long LocationID { get; private set; }

        public Action_CompleteLocationCheck(long locationID)
        {
            LocationID = locationID;
        }

        public override string info => $"Complete Archipelago location check: {LocationID}";

        public override void OnExecute()
        {
            ArchipelagoConnector.Instance.CompleteLocationCheck(LocationID);
            base.EndAction();
        }
    }
}
