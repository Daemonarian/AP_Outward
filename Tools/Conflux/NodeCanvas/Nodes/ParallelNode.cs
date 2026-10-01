using System.Runtime.Serialization;
using System.Text;
using Conflux.NodeCanvas.Serialization;
using Conflux.Schema;
using Conflux.Schema.Exceptions;
using Conflux.Schema.Statements;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Conflux.NodeCanvas.Nodes
{
    [NodeCanvasType("NodeCanvas.BehaviourTrees.Parallel")]
    internal class ParallelNode(int outConnectionCount = -1) : BTNode
    {
        [JsonProperty("policy")]
        public ParallelPolicy Policy { get; set; } = ParallelPolicy.FirstFailure;

        [JsonProperty("dynamic")]
        public bool IsDynamic { get; set; } = false;

        private readonly int _outConnectionCount = outConnectionCount;

        public override int OutConnectionCount => _outConnectionCount;

        public override string GetGraphVizShortName() => "Parallel";

        public override string GetGraphVizContent()
        {
            var content = new StringBuilder();

            if (Policy != ParallelPolicy.FirstFailure)
            {
                content.AppendLine($"Policy: {Policy}");
            }

            if (IsDynamic)
            {
                content.AppendLine("IsDynamic: yes");
            }

            return content.ToString();
        }

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
