using Newtonsoft.Json;
using NodeCanvas.Tool.Schema.NodeCanvas.Serialization;
using YamlDotNet.Serialization;

namespace NodeCanvas.Tool.Schema.NodeCanvas.DialogueTrees
{
    [NodeCanvasType("NodeCanvas.DialogueTrees.DialogueTree+DerivedSerializationData")]
    internal class DTDerivedSerializationData : DerivedSerializationData
    {
        [YamlMember(Alias = "actors")]
        [JsonProperty("actorParameters")]
        public List<ActorParameter> ActorParameters { get; set; } = [];
    }
}
