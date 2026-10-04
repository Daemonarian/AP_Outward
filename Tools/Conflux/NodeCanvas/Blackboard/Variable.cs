using System.ComponentModel;
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
        [DefaultValue(false)]
        public bool IsProtected { get; set; } = false;

        [JsonProperty("_value")]
        public UnityObject? Value { get; set; }

        public ConfluxLocalBlackboardVariable BuildConfluxLocalBlackboardVariable(Graph graph)
        {
            if (Type is null)
            {
                throw new ConfluxValueException("Variable type is not specified.");
            }

            if (Name is null)
            {
                throw new ConfluxValueException("Variable name is not specified.");
            }

            if (!ConfluxLocalBlackboardVariable.TypeAliasReverseMapping.TryGetValue(Type, out var typeAlias))
            {
                throw new ConfluxValueException($"Unknown variable type: {Type}.");
            }

            return new()
            {
                Type = typeAlias,
                Name = Name,
                ID = string.IsNullOrEmpty(ID) ? null : Guid.Parse(ID),
                IsProtected = IsProtected,
                Index = Value?.SideCarIndex,
            };
        }
    }
}
