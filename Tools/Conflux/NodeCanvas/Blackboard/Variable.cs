using Conflux.NodeCanvas.References;
using Conflux.NodeCanvas.Serialization;
using Conflux.Schema.Blackboards;
using Conflux.Schema.Exceptions;
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

        public ConfluxLocalBlackboardVariable BuildConfluxLocalBlackboardVariable(Graph graph) => new()
        {
            Type = Type ?? throw new ConfluxValueException("Variable type is not specified."),
            Name = Name ?? throw new ConfluxValueException("Variable name is not specified."),
            ID = string.IsNullOrEmpty(ID) ? null : Guid.Parse(ID),
            IsProtected = IsProtected ?? false,
            Index = Value?.SideCarIndex,
        };
    }
}
