using System.Text;
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
    internal class OwnsItemNodeCanvasCondition : ConditionTask
    {
        [JsonProperty("character")]
        public BBParameter<UnityObject>? Character { get; set; }

        [JsonProperty("item")]
        public BBParameter<ItemReference>? Item { get; set; }

        [JsonProperty("minAmount")]
        public BBParameter<int>? MinAmount { get; set; }

        [JsonProperty("itemMustBeEquiped")]
        public BBParameter<bool>? ItemMustBeEquipped { get; set; }

        [JsonProperty("SaveMatchingContainerVariable")]
        public string? SaveMatchingContainerVariable { get; set; }

        public override string GetGraphVizShortName() => "OwnsItem";

        public override string GetGraphVizContent()
        {
            var content = new StringBuilder();
            content.AppendLine();

            if (Character is not null)
            {
                content.AppendLine($"Character: {GraphVizConverter.IndentLines(Character.ToGraphVizLabel())}");
            }

            if (Item is not null)
            {
                content.AppendLine($"Item: {GraphVizConverter.IndentLines(Item.ToGraphVizLabel())}");
            }

            if (MinAmount is not null)
            {
                content.AppendLine($"MinAmount: {GraphVizConverter.IndentLines(MinAmount.ToGraphVizLabel())}");
            }

            if (ItemMustBeEquipped is not null)
            {
                content.AppendLine($"ItemMustBeEquipped: {GraphVizConverter.IndentLines(ItemMustBeEquipped.ToGraphVizLabel())}");
            }

            if (SaveMatchingContainerVariable is not null)
            {
                content.AppendLine($"SaveMatchingContainerVariable: {GraphVizConverter.IndentLines(SaveMatchingContainerVariable)}");
            }

            return content.ToString().TrimEnd();
        }

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
