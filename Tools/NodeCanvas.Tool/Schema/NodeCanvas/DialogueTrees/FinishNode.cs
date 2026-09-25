using System.Text;
using Newtonsoft.Json;
using NodeCanvas.Tool.Schema.NodeCanvas.Serialization;

namespace NodeCanvas.Tool.Schema.NodeCanvas.DialogueTrees
{
    [NodeCanvasType("NodeCanvas.DialogueTrees.FinishNode")]
    internal class FinishNode : DTNode
    {
        [JsonProperty("finishState")]
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

        internal enum CompactStatus
        {
            Failure,
            Success
        }
    }
}
