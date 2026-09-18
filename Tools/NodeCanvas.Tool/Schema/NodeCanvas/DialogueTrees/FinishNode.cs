using System.Text;
using System.Text.Json.Serialization;
using NodeCanvas.Tool.Schema.NodeCanvas.Framework;

namespace NodeCanvas.Tool.Schema.NodeCanvas.DialogueTrees
{
    internal class FinishNode : Node
    {
        [JsonPropertyName("finishState")]
        public CompactStatus FinishState = CompactStatus.Success;

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
