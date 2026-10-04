using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;
using Conflux.NodeCanvas.Serialization;
using Conflux.Schema;
using Conflux.Schema.Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Conflux.NodeCanvas.Nodes
{
    [NodeCanvasType("NodeCanvas.DialogueTrees.FinishNode")]
    internal class FinishNode : DTNode
    {
        [JsonProperty("finishState")]
        [DefaultValue(CompactStatus.Success)]
        public CompactStatus FinishState = CompactStatus.Success;

        public override int OutConnectionCount => 0;

        public override string GetGraphVizShortName() => "Finish";

        public override string GetGraphVizContent()
        {
            var content = new StringBuilder();

            if (FinishState != CompactStatus.Success)
            {
                content.AppendLine($"FinishState: {FinishState}");
            }

            return content.ToString();
        }

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
