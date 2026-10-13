using Conflux.GraphViz;
using Conflux.NodeCanvas.Blackboard;
using Conflux.NodeCanvas.References;
using Conflux.NodeCanvas.Serialization;
using Conflux.Schema.Conditions;
using Conflux.Schema.References;
using Newtonsoft.Json;

namespace Conflux.NodeCanvas.Conditions
{
    [NodeCanvasType("NodeCanvas.Tasks.Conditions.Condition_OwnsItem")]
    [GraphVizObject(Alias = "OwnsItem")]
    internal class OwnsItemNodeCanvasCondition : NodeCanvasCondition
    {
        [JsonProperty("character")]
        public BBParameter<UnityObject>? Character { get; set; }

        [JsonProperty("item")]
        [GraphVizProperty(IsMainProperty = true)]
        public BBParameter<ItemReference>? Item { get; set; }

        [JsonProperty("minAmount")]
        public BBParameter<int>? MinAmount { get; set; }

        [JsonProperty("itemMustBeEquiped")]
        public BBParameter<bool>? ItemMustBeEquipped { get; set; }

        [JsonProperty("SaveMatchingContainerVariable")]
        public string? SaveMatchingContainerVariable { get; set; }

        protected override OwnsItemConfluxCondition BuildSubConfluxCondition(Graph graph) => new()
        {
            Character = Character?.BuildConfluxBlackboardVariableReference<ConfluxUnityObjectReference>(graph),
            Item = Item?.BuildConfluxBlackboardVariableReference<ConfluxItemReference>(graph),
            MinAmount = MinAmount?.BuildConfluxBlackboardVariableReference(graph),
            ItemMustBeEquipped = ItemMustBeEquipped?.BuildConfluxBlackboardVariableReference(graph),
            SaveMatchingContainerVariable = SaveMatchingContainerVariable,
        };
    }
}
