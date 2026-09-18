using System.Text;
using System.Text.Json.Serialization;

namespace NodeCanvas.Tool.Schema.NodeCanvas.Framework.Actions
{
    internal class SendQuestEvent : ActionTask
    {
        [JsonPropertyName("QuestEventRef")]
        public QuestEventReference QuestEventRef { get; set; } = new();

        [JsonPropertyName("StackAmount")]
        public int StackAmount { get; set; } = 1;

        [JsonPropertyName("IgnoreNetworkSync")]
        public bool IgnoreNetworkSync { get; set; }

        public override string GetGraphVizShortName() => "SendQuestEvent";

        public override string GetGraphVizContent()
        {
            var content = new StringBuilder();

            content.AppendLine(QuestEventRef.ToGraphVizLabel());

            if (StackAmount != 1)
            {
                content.AppendLine($"StackAmount: {StackAmount}");
            }

            if (IgnoreNetworkSync)
            {
                content.AppendLine($"IgnoreNetworkSync: yes");
            }

            return content.ToString().TrimEnd();
        }
    }
}
