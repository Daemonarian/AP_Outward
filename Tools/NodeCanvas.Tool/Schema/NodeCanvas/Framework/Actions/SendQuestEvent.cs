using System.Text;
using Newtonsoft.Json;
using NodeCanvas.Tool.Schema.NodeCanvas.Serialization;

namespace NodeCanvas.Tool.Schema.NodeCanvas.Framework.Actions
{
    [NodeCanvasType("NodeCanvas.Tasks.Actions.SendQuestEvent")]
    internal class SendQuestEvent : ActionTask
    {
        [JsonProperty("QuestEventRef")]
        public QuestEventReference QuestEventRef { get; set; } = new();

        [JsonProperty("StackAmount")]
        public int StackAmount { get; set; } = 1;

        [JsonProperty("IgnoreNetworkSync")]
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
