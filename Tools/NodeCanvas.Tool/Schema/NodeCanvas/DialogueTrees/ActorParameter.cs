using Newtonsoft.Json;
using YamlDotNet.Serialization;

namespace NodeCanvas.Tool.Schema.NodeCanvas.DialogueTrees
{
    internal class ActorParameter
    {
        [YamlMember(Alias = "key")]
        [JsonProperty("_keyName")]
        public string Key { get; set; } = string.Empty;

        [YamlMember(Alias = "id")]
        [JsonProperty("_id")]
        public string ID { get; set; } = string.Empty;

        [YamlMember(Alias = "object")]
        [JsonProperty("_actorObject")]
        public int Object { get; set; } = 0;
    }
}
