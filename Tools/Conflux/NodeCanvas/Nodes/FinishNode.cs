using System.ComponentModel;
using System.Runtime.Serialization;
using Conflux.GraphViz;
using Conflux.NodeCanvas.Serialization;
using Conflux.Schema;
using Conflux.Schema.Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Conflux.NodeCanvas.Nodes
{
    [NodeCanvasType("NodeCanvas.DialogueTrees.FinishNode")]
    [GraphVizObject(Alias = "Finish")]
    internal class FinishNode : DTNode
    {
        [JsonProperty("finishState")]
        [GraphVizProperty(IsMainProperty = true)]
        [DefaultValue(CompactStatus.Success)]
        public CompactStatus FinishState { get; set; } = CompactStatus.Success;

        public override int OutConnectionCount => 0;

        public override ConfluxBlock BuildConfluxBlock(Graph graph, List<ConfluxBlock> children)
        {
            if (children.Count > OutConnectionCount)
            {
                throw new ConfluxException($"FinishNode can only have {OutConnectionCount} children.");
            }

            return new ConfluxBlock
            {
                Statements = [],
            };
        }

        [JsonConverter(typeof(StringEnumConverter))]
        internal enum CompactStatus
        {
            [EnumMember(Value = "Failure")]
            Failure,

            [EnumMember(Value = "Success")]
            Success,
        }
    }
}
