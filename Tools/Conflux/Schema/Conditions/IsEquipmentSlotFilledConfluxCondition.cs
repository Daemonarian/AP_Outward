using System.ComponentModel;
using Conflux.NodeCanvas.Conditions;
using Conflux.NodeCanvas.References;
using Conflux.Outward;
using Conflux.Schema.Blackboards;
using Conflux.Schema.Context;
using Conflux.Schema.References;
using Conflux.Schema.Serialization;
using YamlDotNet.Serialization;

namespace Conflux.Schema.Conditions
{
    [ConfluxDerived("isEquipmentSlotFilled")]
    internal class IsEquipmentSlotFilledConfluxCondition : ConfluxCondition
    {
        [YamlMember(Alias = "character")]
        public ConfluxBlackboardVariableReference<ConfluxUnityObjectReference>? Character { get; set; }

        [ConfluxMainProperty]
        [YamlMember(Alias = "slot")]
        public ConfluxBlackboardVariableReference<OutwardEquipmentSlot.EquipmentSlotIDs>? Slot { get; set; }

        [YamlMember(Alias = "requireEnchanted")]
        [DefaultValue(false)]
        public ConfluxBlackboardVariableReference<bool> MustBeEnchanted { get; set; } = false;

        public override IsEquipmentSlotFilledNodeCanvasCondition BuildCondition(NodeCanvasGraphContext context) => new()
        {
            Character = Character?.BuildBBParameter<UnityObject>(context),
            Slot = Slot?.BuildBBParameter(context),
            MustBeEnchanted = MustBeEnchanted.BuildBBParameter(context),
        };
    }
}
