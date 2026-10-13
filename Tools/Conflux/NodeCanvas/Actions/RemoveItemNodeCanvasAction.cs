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
    [GraphVizObject(Alias = "RemoveItem")]
    internal class RemoveItemNodeCanvasAction : NodeCanvasAction
    {
        [JsonProperty("fromCharacter")]
        public BBParameter<UnityObject>? FromCharacter { get; set; }

        [JsonProperty("Items")]
        public List<BBParameter<ItemReference>>? Items { get; set; }

        [JsonProperty("Amount")]
        public List<BBParameter<int>>? Amount { get; set; }

        public override RemoveItemConfluxAction BuildConfluxAction(Graph graph) => new()
        {
            FromCharacter = FromCharacter?.BuildConfluxBlackboardVariableReference<ConfluxUnityObjectReference>(graph),
            Items = Items?.Select(i => i.BuildConfluxBlackboardVariableReference<ConfluxItemReference>(graph)).ToList(),
            Amount = Amount?.Select(a => a.BuildConfluxBlackboardVariableReference(graph)).ToList(),
        };
    }
}
