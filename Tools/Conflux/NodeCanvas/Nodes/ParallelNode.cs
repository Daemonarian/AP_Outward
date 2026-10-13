using System.ComponentModel;
using System.Runtime.Serialization;
using Conflux.GraphViz;
using Conflux.NodeCanvas.Serialization;
using Conflux.Schema;
using Conflux.Schema.Exceptions;
using Conflux.Schema.Statements;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Conflux.NodeCanvas.Nodes
{
    [NodeCanvasType("NodeCanvas.BehaviourTrees.Parallel")]
    [GraphVizObject(Alias = "Parallel")]
    internal class ParallelNode(int outConnectionCount = -1) : BTNode
    {
        [JsonProperty("policy")]
        [DefaultValue(ParallelPolicy.FirstFailure)]
        public ParallelPolicy Policy { get; set; } = ParallelPolicy.FirstFailure;

        [JsonProperty("dynamic")]
        [DefaultValue(false)]
        public bool IsDynamic { get; set; } = false;

        private readonly int _outConnectionCount = outConnectionCount;

        public override int OutConnectionCount => _outConnectionCount;

        public override string? GetGraphVizOutConnectionLabel(int index) => $"{index}";

        public override ConfluxBlock BuildConfluxBlock(Graph graph, List<ConfluxBlock> children)
        {
            return new ConfluxBlock
            {
                Statements = [new ParallelConfluxStatement{
                    Policy = Policy switch
                    {
                        ParallelPolicy.FirstFailure => ParallelConfluxStatement.ParallelPolicy.FirstFailure,
                        ParallelPolicy.FirstSuccess => ParallelConfluxStatement.ParallelPolicy.FirstSuccess,
                        ParallelPolicy.FirstSuccessOrFailure => ParallelConfluxStatement.ParallelPolicy.FirstSuccessOrFailure,
                        _ => throw new ConfluxException($"ParallelNode policy {Policy} is not supported."),
                    },
                    Tasks = [.. children],
                }],
            };
        }

        [JsonConverter(typeof(StringEnumConverter))]
        public enum ParallelPolicy
        {
            [EnumMember(Value = "FirstFailure")]
            FirstFailure,

            [EnumMember(Value = "FirstSuccess")]
            FirstSuccess,

            [EnumMember(Value = "FirstSuccessOrFailure")]
            FirstSuccessOrFailure,
        }
    }
}
