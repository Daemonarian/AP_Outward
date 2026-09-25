using Conflux.NodeCanvas.Actions;
using Conflux.NodeCanvas.Blackboard;
using Conflux.NodeCanvas.References;
using Conflux.Outward;
using Conflux.Schema.Context;
using Conflux.Schema.Deserializer;
using YamlDotNet.Serialization;

namespace Conflux.Schema.Actions
{
    [ConfluxDerived("reward")]
    internal class RewardConfluxAction : ConfluxAction
    {
        private const string SilverKey = "silver";

        private const string XPKey = "xp";

        [ConfluxMainProperty]
        [YamlMember(Alias = "items")]
        public Dictionary<string, int> ItemAmounts { get; set; } = [];

        [YamlMember(Alias = "silver")]
        public int? SilverAmount { get; set; }

        [YamlMember(Alias = "xp")]
        public int? XPAmount { get; set; }

        [YamlMember(Alias = "to")]
        public GiveReward.Receiver Receiver { get; set; } = GiveReward.Receiver.Host;

        public override ActionTask BuildAction(INodeCanvasGraphContext context)
        {
            var silverAmount = new BBParameter<int>
            {
                Value = SilverAmount ?? ItemAmounts.GetValueOrDefault(SilverKey, 0),
            };

            var xpAmount = new BBParameter<int>
            {
                Value = XPAmount ?? ItemAmounts.GetValueOrDefault(XPKey, 0),
            };

            var itemRewards = ItemAmounts
                .Where(pair => pair.Key != SilverKey && pair.Key != XPKey)
                .Select(pair => new GiveReward.ItemQuantity
                {
                    Item = new BBParameter<ItemReference>
                    {
                        Value = new ItemReference
                        {
                            ItemID = Item.ByKey[pair.Key].ID,
                        },
                    },
                    Quantity = new BBParameter<int>
                    {
                        Value = pair.Value,
                    },
                    TryToEquip = new BBParameter<bool>
                    {
                        Value = false,
                    },
                }).ToList();

            return new GiveReward
            {
                RewardReceiver = Receiver,
                SilverAmount = silverAmount,
                XpAmount = xpAmount,
                ItemRewards = itemRewards,
            };
        }
    }
}
