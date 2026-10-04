using Conflux.Schema.Blackboards;
using Newtonsoft.Json;

namespace Conflux.NodeCanvas.Blackboard
{
    internal class BlackboardSource
    {
        [JsonProperty("_name")]
        public string? Name { get; set; }

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
