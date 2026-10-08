using Conflux.NodeCanvas.Actions;
using Conflux.NodeCanvas.References;
using Conflux.Schema.Blackboards;
using Conflux.Schema.Context;
using Conflux.Schema.References;
using Conflux.Schema.Serialization;
using Newtonsoft.Json;

namespace Conflux.Schema.Actions
{
    [ConfluxDerived("removeItem")]
    internal class RemoveItemConfluxAction : ConfluxAction
    {
        [JsonProperty("fromCharacter")]
        public ConfluxBlackboardVariableReference<UnityObject, ConfluxUnityObjectReference>? FromCharacter { get; set; }

        [JsonProperty("Items")]
        public List<ConfluxBlackboardVariableReference<ItemReference, ConfluxItemReference>>? Items { get; set; }

        [JsonProperty("Amount")]
        public List<ConfluxBlackboardVariableReference<int>>? Amount { get; set; }

        public override RemoveItemNodeCanvasAction BuildAction(NodeCanvasGraphContext context) => new()
        {
            FromCharacter = FromCharacter?.BuildBBParameter(context),
            Items = Items?.Select(i => i.BuildBBParameter(context)).ToList(),
            Amount = Amount?.Select(a => a.BuildBBParameter(context)).ToList(),
        };
    }
}
