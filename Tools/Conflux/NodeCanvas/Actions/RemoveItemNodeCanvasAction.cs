using System.Text;
using Conflux.GraphViz;
using Conflux.NodeCanvas.Blackboard;
using Conflux.NodeCanvas.References;
using Conflux.NodeCanvas.Serialization;
using Conflux.Schema.Actions;
using Conflux.Schema.References;
using Newtonsoft.Json;

namespace Conflux.NodeCanvas.Actions
{
    [NodeCanvasType("NodeCanvas.Tasks.Actions.RemoveItem")]
    internal class RemoveItemNodeCanvasAction : ActionTask
    {
        [JsonProperty("fromCharacter")]
        public BBParameter<UnityObject>? FromCharacter { get; set; }

        [JsonProperty("Items")]
        public List<BBParameter<ItemReference>>? Items { get; set; }

        [JsonProperty("Amount")]
        public List<BBParameter<int>>? Amount { get; set; }

        public override string GetGraphVizShortName() => "RemoveItem";

        public override string GetGraphVizContent()
        {
            var content = new StringBuilder();

            if (FromCharacter is not null)
            {
                content.AppendLine($"FromCharacter: {GraphVizConverter.IndentLines(FromCharacter.ToGraphVizLabel())}");
            }

            if (Items is not null)
            {
                for (var i = 0; i < Items.Count; i++)
                {
                    var item = Items[i];
                    content.AppendLine($"- Item: {GraphVizConverter.IndentLines(item.ToGraphVizLabel())}");

                    if (Amount is not null && i < Amount.Count)
                    {
                        var amount = Amount[i];
                        content.AppendLine($"  Amount: {GraphVizConverter.IndentLines(amount.ToGraphVizLabel())}");
                    }
                }
            }

            return content.ToString().TrimEnd();
        }

        public override RemoveItemConfluxAction BuildConfluxAction(Graph graph) => new()
        {
            FromCharacter = FromCharacter?.BuildConfluxBlackboardVariableReference<ConfluxUnityObjectReference>(graph),
            Items = Items?.Select(i => i.BuildConfluxBlackboardVariableReference<ConfluxItemReference>(graph)).ToList(),
            Amount = Amount?.Select(a => a.BuildConfluxBlackboardVariableReference(graph)).ToList(),
        };
    }
}
