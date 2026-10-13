using System.ComponentModel;
using Conflux.GraphViz;
using Conflux.NodeCanvas.Blackboard;
using Conflux.NodeCanvas.Serialization;
using Conflux.Schema.Statements;
using Newtonsoft.Json;

namespace Conflux.NodeCanvas.Nodes
{
    [NodeCanvasType("NodeCanvas.BehaviourTrees.ProbabilitySelector")]
    [GraphVizObject(Alias = "ProbabilitySelector")]
    internal class ProbabilitySelectorNode : BTNode
    {
        [JsonIgnore]
        [GraphVizIgnore]
        public List<BBParameter<float>> ChildWeights { get; set; } = [];

        [JsonProperty("childWeights")]
        [GraphVizProperty(Alias = "Weights")]
        public List<BBParameter<float>>? ChildWeightsProxy
        {
            get => ChildWeights.Count == 0 ? null : ChildWeights;
            set => ChildWeights = value ?? [];
        }

        [JsonProperty("failChance")]
        [GraphVizProperty(Alias = "FailChance")]
        [DefaultValue(0f)]
        public BBParameter<float> FailChance { get; set; } = 0f;

        public override int OutConnectionCount => ChildWeights.Count;

        public override string? GetGraphVizOutConnectionLabel(int index) => $"{index}";

        protected override RandomChoiceConfluxStatement BuildConfluxStatement(Graph graph) => new()
        {
            Choices = [.. ChildWeights.Select(w => new RandomChoiceConfluxStatement.Choice
                {
                    Weight = w.BuildConfluxBlackboardVariableReference(graph),
                })],
            FailChance = FailChance.BuildConfluxBlackboardVariableReference(graph),
        };
    }
}
