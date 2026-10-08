using Conflux.NodeCanvas.Conditions;
using Conflux.NodeCanvas.References;
using Conflux.Schema.Blackboards;
using Conflux.Schema.Context;
using Conflux.Schema.References;
using Conflux.Schema.Serialization;
using YamlDotNet.Serialization;

namespace Conflux.Schema.Conditions
{
    [ConfluxDerived("ownsItem")]
    internal class OwnsItemConfluxCondition : ConfluxCondition
    {
        [YamlMember(Alias = "character")]
        public ConfluxBlackboardVariableReference<UnityObject, ConfluxUnityObjectReference>? Character { get; set; }

        [ConfluxMainProperty]
        [YamlMember(Alias = "item")]
        public ConfluxBlackboardVariableReference<ItemReference, ConfluxItemReference>? Item { get; set; }

        [YamlMember(Alias = "minAmount")]
        public ConfluxBlackboardVariableReference<int>? MinAmount { get; set; }

        [YamlMember(Alias = "requireEquipped")]
        public ConfluxBlackboardVariableReference<bool>? ItemMustBeEquipped { get; set; }

        [YamlMember(Alias = "saveVariable")]
        public string? SaveMatchingContainerVariable { get; set; }

        public override OwnsItemNodeCanvasCondition BuildCondition(NodeCanvasGraphContext context) => new()
        {
            Character = Character?.BuildBBParameter(context),
            Item = Item?.BuildBBParameter(context),
            MinAmount = MinAmount?.BuildBBParameter(context),
            ItemMustBeEquipped = ItemMustBeEquipped?.BuildBBParameter(context),
            SaveMatchingContainerVariable = SaveMatchingContainerVariable,
        };
    }
}
