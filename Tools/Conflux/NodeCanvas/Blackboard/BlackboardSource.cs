using Conflux.Schema.Blackboards;
using Newtonsoft.Json;
using YamlDotNet.Serialization;

namespace Conflux.NodeCanvas.Blackboard
{
    internal class BlackboardSource
    {
        [YamlMember(Alias = "name")]
        [JsonProperty("_name")]
        public string? Name { get; set; } = null;

        [YamlMember(Alias = "variables")]
        [JsonProperty("_variables")]
        public Dictionary<string, Variable> Variables { get; set; } = [];

        public ConfluxLocalBlackboard BuildConfluxLocalBlackboard(Graph graph) => new()
        {
            Variables = Variables.Values
                .Select(v => v.BuildConfluxLocalBlackboardVariable(graph))
                .ToDictionary(v => v.Name),
        };
    }
}
