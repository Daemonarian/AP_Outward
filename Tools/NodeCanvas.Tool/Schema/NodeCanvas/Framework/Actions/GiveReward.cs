using System.Text;
using System.Text.Json.Serialization;
using NodeCanvas.Tool;

namespace NodeCanvas.Tool.Schema.NodeCanvas.Framework.Actions
{
    internal class GiveReward : ActionTask
    {
        [JsonPropertyName("RewardReceiver")]
        public Receiver RewardReceiver { get; set; } = Receiver.Host;

        [JsonPropertyName("XpAmount")]
        public BBParameter<int> XpAmount { get; set; } = new();

        [JsonPropertyName("SilverAmount")]
        public BBParameter<int> SilverAmount { get; set; } = new();

        [JsonPropertyName("ItemReward")]
        public List<ItemQuantity> ItemRewards { get; set; } = [];

        public override string GetGraphVizShortName() => "Reward";

        public override string GetGraphVizContent()
        {
            var content = new StringBuilder();

            content.AppendLine();

            if (RewardReceiver != Receiver.Host)
            {
                content.AppendLine($"To: {RewardReceiver}");
            }

            if (XpAmount.HasValue())
            {
                content.AppendLine($"XP: {XpAmount}");
            }

            if (SilverAmount.HasValue())
            {
                content.AppendLine($"Silver: {SilverAmount}");
            }

            foreach (var reward in ItemRewards)
            {
                var rewardLabel = reward.ToGraphVizLabel();
                rewardLabel = GraphVizConverter.IndentLines(rewardLabel, indentFirstLine: false);
                content.AppendLine($"- {rewardLabel}");
            }

            return content.ToString().TrimEnd();
        }

        public enum Receiver
        {
            Host,
            Instigator,
            Everyone
        }

        public class ItemQuantity : IGraphVizLabelable
        {
            [JsonPropertyName("Item")]
            public BBParameter<ItemReference> Item { get; set; } = new();

            [JsonPropertyName("Quantity")]
            public BBParameter<int> Quantity { get; set; } = new();

            [JsonPropertyName("TryToEquip")]
            public BBParameter<bool> TryToEquip { get; set; } = new();

            public string ToGraphVizLabel()
            {
                var label = new StringBuilder();

                label.AppendLine($"{Item.ToGraphVizLabel()}: {Quantity.ToGraphVizLabel()}");

                if (TryToEquip.HasValue())
                {
                    label.AppendLine($"TryToEquip: {TryToEquip}");
                }

                return label.ToString().TrimEnd();
            }
        }
    }
}
