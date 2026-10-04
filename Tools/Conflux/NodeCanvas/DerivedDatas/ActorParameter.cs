using Conflux.NodeCanvas.References;
using Conflux.NodeCanvas.Serialization;
using Conflux.Schema.DerivedDatas;
using Newtonsoft.Json;

namespace Conflux.NodeCanvas.DerivedDatas
{
    [NodeCanvasType("NodeCanvas.DialogueTrees.DialogueTree+ActorParameter")]
    internal class ActorParameter : IConfluxObjectBuilder<ConfluxActor>
    {
        [JsonProperty("_keyName")]
        public string? Key { get; set; } = null;

        [JsonProperty("_id")]
        public string? ID { get; set; } = null;

        [JsonProperty("_actorObject")]
        public UnityObject? Object { get; set; } = null;

        public ConfluxActor BuildConfluxObject(Graph graph) => new()
        {
            Key = Key?.Trim(),
            ID = string.IsNullOrEmpty(ID) ? null : Guid.Parse(ID),
            Object = Object?.BuildConfluxObject(graph),
        };
    }
}
