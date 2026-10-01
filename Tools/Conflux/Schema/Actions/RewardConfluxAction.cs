using System.ComponentModel;
using Conflux.NodeCanvas.Actions;
using Conflux.NodeCanvas.References;
using Conflux.Schema.Blackboards;
using Conflux.Schema.Context;
using Conflux.Schema.Exceptions;
using Conflux.Schema.References;
using Conflux.Schema.Serialization;
using YamlDotNet.Serialization;

namespace Conflux.Schema.Actions
{
    [ConfluxDerived("reward")]
    internal class RewardConfluxAction : ConfluxAction
    {
        [ConfluxMainProperty]
        [YamlMember(Alias = "items")]
        public List<ItemReward> ItemRewards { get; set; } = [];

        [YamlMember(Alias = "silver")]
        public ConfluxBlackboardVariableReference<int> SilverAmount { get; set; } = new() { Value = 0 };

        [YamlMember(Alias = "xp")]
        public ConfluxBlackboardVariableReference<int> XPAmount { get; set; } = new() { Value = 0 };

        [YamlMember(Alias = "to")]
        [DefaultValue(RewardReceiver.Host)]
        public RewardReceiver Receiver { get; set; } = RewardReceiver.Host;

        public override GiveReward BuildAction(NodeCanvasGraphContext context) => new()
        {
            RewardReceiver = GetNodeCanvasReceiver(context, Receiver),
            SilverAmount = SilverAmount.BuildBBParameter(context),
            XpAmount = XPAmount.BuildBBParameter(context),
            ItemRewards = [.. ItemRewards.Select(r => r.BuildItemQuantity(context))],
        };

        public enum RewardReceiver
        {
            Host,
            Instigator,
            Everyone,
        }

        public static GiveReward.Receiver GetNodeCanvasReceiver(NodeCanvasGraphContext context, RewardReceiver receiver) => receiver switch
        {
            RewardReceiver.Host => GiveReward.Receiver.Host,
            RewardReceiver.Instigator => GiveReward.Receiver.Instigator,
            RewardReceiver.Everyone => GiveReward.Receiver.Everyone,
            _ => throw new ConfluxValueException($"Unexpected reward reciever: {receiver}."),
        };

        public class ItemReward
        {
            [YamlMember(Alias = "item")]
            public ConfluxBlackboardVariableReference<ItemReference, ConfluxItemReference> Item { get; set; } = new();

            [YamlMember(Alias = "quantity")]
            public ConfluxBlackboardVariableReference<int> Quantity { get; set; } = new() { Value = 1 };

            [YamlMember(Alias = "equip")]
            public ConfluxBlackboardVariableReference<bool> TryToEquip { get; set; } = new() { Value = false };

            public GiveReward.ItemQuantity BuildItemQuantity(NodeCanvasGraphContext context) => new()
            {
                Item = Item.BuildBBParameter(context),
                Quantity = Quantity.BuildBBParameter(context),
                TryToEquip = TryToEquip.BuildBBParameter(context),
            };
        }
    }
}
