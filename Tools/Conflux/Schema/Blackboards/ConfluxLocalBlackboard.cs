using Conflux.NodeCanvas.Blackboard;
using Conflux.Schema.Context;
using Conflux.Schema.Serialization;
using YamlDotNet.Serialization;

namespace Conflux.Schema.Blackboards
{
    internal class ConfluxLocalBlackboard
    {
        public const string DefaultName = "Local Blackboard";

        [YamlIgnore]
        public Dictionary<string, ConfluxLocalBlackboardVariable> Variables { get; set; } = [];

        [ConfluxMainProperty(Force = true)]
        [YamlMember(Alias = "variables")]
        public Dictionary<string, ConfluxLocalBlackboardVariable> VariablesProxy
        {
            get => Variables;

            set
            {
                foreach (var (name, variable) in value)
                {
                    variable.Name = name;
                }

                Variables = value;
            }
        }

        public BlackboardSource BuildBlackboardSource(NodeCanvasGraphContext context)
        {
            var variables = Variables.Values
                .Select(v => v.BuildVariable(context))
                .ToDictionary(v => v.Name ?? string.Empty);
            return new BlackboardSource
            {
                Name = DefaultName,
                Variables = variables,
            };
        }

        public bool IsEmpty() => Variables.Count == 0;
    }
}
