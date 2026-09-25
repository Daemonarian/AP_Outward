using Newtonsoft.Json;
using YamlDotNet.Serialization;

namespace NodeCanvas.Tool.Schema.NodeCanvas.Framework.Internal
{
    internal class BlackboardSource
    {
        [YamlMember(Alias = "name")]
        [JsonProperty("_name")]
        public string? Name { get; set; } = null;

        [YamlMember(Alias = "variables")]
        [JsonProperty("_variables")]
        public Dictionary<string, Variable> Variables { get; set; } = [];
    }
}
