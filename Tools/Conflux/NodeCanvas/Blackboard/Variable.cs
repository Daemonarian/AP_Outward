using Conflux.NodeCanvas.Serialization;
using Newtonsoft.Json;

namespace Conflux.NodeCanvas.Blackboard
{
    [JsonConverter(typeof(VariableJsonConverter))]
    internal class Variable
    {
        [JsonProperty("$type")]
        public string? Type { get; set; }

        [JsonProperty("_name")]
        public string? Name { get; set; }

        [JsonProperty("_id")]
        public string? ID { get; set; }

        [JsonProperty("_protected")]
        public bool? IsProtected { get; set; }

        [JsonProperty("_value")]
        public UnityObject? Value { get; set; }
    }
}
