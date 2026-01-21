using NodeCanvas.Framework;
using OutwardArchipelago.Archipelago;
using OutwardArchipelago.Archipelago.Data;

namespace OutwardArchipelago.Dialogue.Conditions
{
    internal class Condition_LocationCheck : ConditionTask
    {
        public ArchipelagoLocationData Location { get; private set; }

        public bool IsInverted { get; private set; }

        public Condition_LocationCheck(ArchipelagoLocationData location, bool isInverted)
        {
            Location = location;
            IsInverted = isInverted;
        }

        public override string info
        {
            get
            {
                var invertedPart = IsInverted ? " not" : "";
                return $"Has{invertedPart} completed location check: {Location}";
            }
        }

        public override bool OnCheck()
        {
            var check = ArchipelagoConnector.Instance.IsLocationCheckCompleted(Location);
            if (IsInverted)
            {
                check = !check;
            }

            OutwardArchipelagoMod.Log.LogDebug($"Condition_LocationCheck::OnCheck Location={Location} IsInverted={IsInverted} return {check}");
            return check;
        }
    }
}
