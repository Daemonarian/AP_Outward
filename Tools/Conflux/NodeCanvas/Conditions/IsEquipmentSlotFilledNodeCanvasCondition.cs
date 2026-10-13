using System.ComponentModel;
using Conflux.GraphViz;
using Conflux.NodeCanvas.Blackboard;
using Conflux.NodeCanvas.References;
using Conflux.NodeCanvas.Serialization;
using Conflux.Outward;
using Conflux.Schema.Conditions;
using Conflux.Schema.References;
using Newtonsoft.Json;

namespace Conflux.NodeCanvas.Conditions
{
    [NodeCanvasType("NodeCanvas.Tasks.Conditions.Condition_IsEquipmentSlotFilled")]
    [GraphVizObject(Alias = "IsEquipmentSlotFilled")]
    internal class IsEquipmentSlotFilledNodeCanvasCondition : NodeCanvasCondition
    {
        [JsonProperty("character")]
        public BBParameter<UnityObject>? Character { get; set; }

        [JsonProperty("slot")]
        [GraphVizProperty(IsMainProperty = true)]
        public BBParameter<OutwardEquipmentSlot.EquipmentSlotIDs>? Slot { get; set; }

        [JsonProperty("mustBeEnchanted")]
        [DefaultValue(false)]
        public BBParameter<bool> MustBeEnchanted { get; set; } = false;

        protected override IsEquipmentSlotFilledConfluxCondition BuildSubConfluxCondition(Graph graph) => new()
        {
            Character = Character?.BuildConfluxBlackboardVariableReference<ConfluxUnityObjectReference>(graph),
            Slot = Slot?.BuildConfluxBlackboardVariableReference(graph),
            MustBeEnchanted = MustBeEnchanted.BuildConfluxBlackboardVariableReference(graph),
        };
    }
}
