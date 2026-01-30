using NodeCanvas.Framework;
using OutwardArchipelago.Archipelago;

namespace OutwardArchipelago.Dialogue.Conditions
{
    internal class Condition_LocationCheck : ConditionTask
    {
        public long LocationId { get; private set; }

        public bool IsInverted { get; private set; }

        public Condition_LocationCheck(long locationId, bool isInverted)
        {
            LocationId = locationId;
            IsInverted = isInverted;
        }

        public override string info
        {
            get
            {
                var invertedPart = IsInverted ? " not" : "";
                return $"Has{invertedPart} completed location check: {LocationId}";
            }
        }

        public override bool OnCheck()
        {
            var check = ArchipelagoConnector.Instance.IsLocationCheckCompleted(LocationId);
            if (IsInverted)
            {
                check = !check;
            }

            OutwardArchipelagoMod.Log.LogDebug($"Condition_LocationCheck::OnCheck Location={LocationId} IsInverted={IsInverted} return {check}");
            return check;
        }
    }
}
