using Conflux.NodeCanvas.Serialization;
using Newtonsoft.Json;
using YamlDotNet.Serialization;

namespace Conflux.NodeCanvas.DerivedData
{
    [NodeCanvasType("NodeCanvas.DialogueTrees.DialogueTree+DerivedSerializationData")]
    internal class DTDerivedData : DerivedData
    {
        [YamlMember(Alias = "actors")]
        [JsonProperty("actorParameters")]
        public List<ActorParameter> ActorParameters { get; set; } = [];
    }
}
