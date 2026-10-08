using System.ComponentModel;
using System.Text;
using Conflux.NodeCanvas.Serialization;
using Conflux.Schema;
using Conflux.Schema.Statements;
using Newtonsoft.Json;

namespace Conflux.NodeCanvas.Nodes
{
    [NodeCanvasType("NodeCanvas.BehaviourTrees.Sequencer")]
    internal class SequencerNodeCanvasNode(int outConnectionCount = -1) : BTNode
    {
        [JsonProperty("dynamic")]
        [DefaultValue(false)]
        public bool IsDynamic { get; set; } = false;

        [JsonProperty("random")]
        [DefaultValue(false)]
        public bool IsRandom { get; set; } = false;

        private readonly int _outConnectionCount = outConnectionCount;

        public override int OutConnectionCount => _outConnectionCount;

        public override string GetGraphVizShortName() => "Sequence";

        public override string GetGraphVizContent()
        {
            var content = new StringBuilder();

            if (IsDynamic)
            {
                content.AppendLine("IsDynamic: yes");
            }

            if (IsRandom)
            {
                content.AppendLine($"IsRandom: yes");
            }

            return content.ToString();
        }

        public override string? GetGraphVizOutConnectionLabel(int index) => $"{index}";

        public override ConfluxBlock BuildConfluxBlock(Graph graph, List<ConfluxBlock> children) => new()
        {
            Statements = [new SequenceConfluxStatement{
                IsDynamic = IsDynamic,
                IsRandom = IsRandom,
                Tasks = [.. children],
            }],
        };
    }
}
