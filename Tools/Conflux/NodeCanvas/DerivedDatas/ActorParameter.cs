using Conflux.NodeCanvas.Serialization;
using Newtonsoft.Json;

namespace Conflux.NodeCanvas.DerivedDatas
{
    [NodeCanvasType("NodeCanvas.DialogueTrees.DialogueTree+ActorParameter")]
    internal class ActorParameter
    {
        [JsonProperty("_keyName")]
        public string? Key { get; set; } = null;

        [JsonProperty("_id")]
        public string? ID { get; set; } = null;

        [JsonProperty("_actorObject")]
        public UnityObject? Object { get; set; } = null;
    }
}
