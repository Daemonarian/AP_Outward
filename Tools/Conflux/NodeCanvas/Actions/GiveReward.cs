using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;
using Conflux.GraphViz;
using Conflux.NodeCanvas.Blackboard;
using Conflux.NodeCanvas.References;
using Conflux.NodeCanvas.Serialization;
using Conflux.Schema.Actions;
using Conflux.Schema.References;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Conflux.NodeCanvas.Actions
{
    [NodeCanvasType("NodeCanvas.Tasks.Actions.GiveReward")]
    internal class GiveReward : ActionTask
    {
        [JsonProperty("RewardReceiver")]
        [DefaultValue(Receiver.Host)]
        public Receiver RewardReceiver { get; set; } = Receiver.Host;

        [JsonProperty("XpAmount")]
        public BBParameter<int> XpAmount { get; set; } = new();

        [JsonProperty("SilverAmount")]
        public BBParameter<int> SilverAmount { get; set; } = new();

        [JsonProperty("ItemReward")]
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

            if (XpAmount.IsReference())
            {
                content.AppendLine($"XP: {XpAmount}");
            }

            if (SilverAmount.IsReference())
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

        public override RewardConfluxAction BuildConfluxAction(Graph graph) => new()
        {
            Receiver = GetConfluxRewardReceiver(graph, RewardReceiver),
            XPAmount = XpAmount.BuildConfluxBlackboardVariableReference(graph),
            SilverAmount = SilverAmount.BuildConfluxBlackboardVariableReference(graph),
            ItemRewards = [.. ItemRewards.Select(r => r.BuildConfluxItemReward(graph))],
        };

        private static RewardConfluxAction.RewardReceiver GetConfluxRewardReceiver(Graph graph, Receiver receiver) => receiver switch
        {
            Receiver.Host => RewardConfluxAction.RewardReceiver.Host,
            Receiver.Instigator => RewardConfluxAction.RewardReceiver.Instigator,
            Receiver.Everyone => RewardConfluxAction.RewardReceiver.Everyone,
            _ => throw new ArgumentOutOfRangeException(nameof(receiver), receiver, null)
        };

        [JsonConverter(typeof(StringEnumConverter))]
        public enum Receiver
        {
            [EnumMember(Value = "Host")]
            Host,

            [EnumMember(Value = "Instigator")]
            Instigator,

            [EnumMember(Value = "Everyone")]
            Everyone,
        }

        public class ItemQuantity : IGraphVizLabelable
        {
            [JsonProperty("Item")]
            public BBParameter<ItemReference> Item { get; set; } = new();

            [JsonProperty("Quantity")]
            public BBParameter<int> Quantity { get; set; } = new();

            [JsonProperty("TryToEquip")]
            public BBParameter<bool> TryToEquip { get; set; } = new();

            public string ToGraphVizLabel()
            {
                var label = new StringBuilder();

                label.AppendLine($"{Item.ToGraphVizLabel()}: {Quantity.ToGraphVizLabel()}");

                if (TryToEquip.IsReference())
                {
                    label.AppendLine($"TryToEquip: {TryToEquip}");
                }

                return label.ToString().TrimEnd();
            }

            public RewardConfluxAction.ItemReward BuildConfluxItemReward(Graph graph) => new()
            {
                Item = Item.BuildConfluxBlackboardVariableReference<ConfluxItemReference>(graph),
                Quantity = Quantity.BuildConfluxBlackboardVariableReference(graph),
                TryToEquip = TryToEquip.BuildConfluxBlackboardVariableReference(graph),
            };
        }
    }
}
