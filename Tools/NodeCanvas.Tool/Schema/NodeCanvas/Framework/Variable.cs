using Newtonsoft.Json;
using NodeCanvas.Tool.Schema.NodeCanvas.Serialization;
using YamlDotNet.Serialization;

namespace NodeCanvas.Tool.Schema.NodeCanvas.Framework
{
    [JsonConverter(typeof(VariableJsonConverter))]
    internal class Variable
    {
        [YamlMember(Alias = "name")]
        [JsonProperty("_name")]
        public string? Name { get; set; }

        [YamlMember(Alias = "id")]
        [JsonProperty("_id")]
        public string? ID { get; set; }

        [YamlMember(Alias = "protected")]
        [JsonProperty("_protected")]
        public bool Protected { get; set; }

        [YamlMember(Alias = "path")]
        [JsonProperty("_propertyPath")]
        public string? PropertyPath { get; set; }

        [YamlMember(Alias = "value")]
        [JsonProperty("_value")]
        public object? Value { get; set; }

        [YamlMember(Alias = "type")]
        [JsonProperty("$type")]
        public string? Type { get; set; }
    }
}
