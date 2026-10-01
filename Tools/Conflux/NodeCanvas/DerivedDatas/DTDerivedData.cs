using Conflux.NodeCanvas.Serialization;
using Newtonsoft.Json;

namespace Conflux.NodeCanvas.DerivedDatas
{
    [NodeCanvasType("NodeCanvas.DialogueTrees.DialogueTree+DerivedSerializationData")]
    internal class DTDerivedData : DerivedData
    {
        [JsonProperty("actorParameters")]
        public List<ActorParameter> ActorParameters { get; set; } = [];
    }
}
